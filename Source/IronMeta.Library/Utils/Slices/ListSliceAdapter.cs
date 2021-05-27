using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace IronMeta.Utils.Slices
{
    internal readonly struct ListSliceAdapter<T> : IEnumerable<T>, IEnumerable, IList<T>, ICollection<T>, IReadOnlyList<T>, IReadOnlyCollection<T>
    {
        private readonly int offset;
        private readonly int end_index;
        private readonly IList<T> source_list;

        public int Offset => offset;
        public int Count => end_index - offset;
        public IList<T> SourceList => source_list;
        public bool IsReadOnly => true;

        public T this[int index]
        {
            get
            {
                int source_index = offset + index;
                if (source_index < 0 || source_index >= end_index)
                    throw new IndexOutOfRangeException("index is out of range of this segment.");
                return source_list[source_index];
            }
            set => throw GetReadOnlyException();
        }

        public ListSliceAdapter(IList<T> source, int offset, int count)
        {
            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset), "Non-negative number required.");
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), "Non-negative number required.");


            source_list = source;
            this.offset = offset;
            end_index = offset + count;
            ValidateRange();
        }

        // Intentionally allowing this segment to include elements from the actual source collection past the other segment's bounds
        public ListSliceAdapter(ListSliceAdapter<T> source, int offset, int count)
            : this(source.SourceList, source.Offset + offset, count)
        { }

        public IEnumerator<T> GetEnumerator()
        {
            ValidateRange();
            // If the source list changes size while we're enumerating, then we'll just potentially crash
            for (int i = offset; i < end_index; i++)
                yield return source_list[i];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ValidateRange()
        {
            if (offset > source_list.Count || end_index > source_list.Count)
                throw new Exception("Offset and length were out of bounds for the IList or count is greater than the number of elements from offset to the end of the source collection.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Exception GetReadOnlyException() => new InvalidOperationException("Slices are read only.");

        public int IndexOf(T item)
        {
            for (int i = offset; i < end_index; i++)
            {
                if (Equals(item, source_list[i]))
                    return i - offset;
            }
            return -1;
        }

        public bool Contains(T item) => IndexOf(item) != -1;

        public void CopyTo(T[] array, int arrayIndex)
        {
            int count = Count;
            for (int i = 0; i < count; i++)
                array[arrayIndex + i] = source_list[offset + i];
        }

        public void Insert(int index, T item) => throw GetReadOnlyException();

        public void RemoveAt(int index) => throw GetReadOnlyException();

        public void Add(T item) => throw GetReadOnlyException();

        public void Clear() => throw GetReadOnlyException();

        public bool Remove(T item) => throw GetReadOnlyException();
    }
}

