using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace IronMeta.Utils.Slices
{
    internal readonly struct StringSliceAdapter : IList<char>, IReadOnlyList<char>
    {
        private readonly string source_string;
        private readonly int offset;
        private readonly int end_index;

        public int Count => end_index - offset;

        public int Offset => offset;

        public string SourceString => source_string;

        public int EndIndex => end_index;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="source">The source enumerable.</param>
        /// <param name="offset">The start position.</param>
        /// <param name="count">The number of items in the slice.</param>
        public StringSliceAdapter(string source, int offset, int count)
        {
            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset), "Non-negative number required.");
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), "Non-negative number required.");

            end_index = offset + count;
            this.offset = offset;
            source_string = source;

            if (offset > source.Length || end_index > source_string.Length)
                throw new Exception("Offset and length were out of bounds for the string or count is greater than the number of characters from offset to the end of the source string.");
        }

        public StringSliceAdapter(StringSliceAdapter source, int offset, int? count)
            : this(source.source_string, offset + source.Offset, count ?? source.end_index - (offset + source.Offset))
        { }

        public StringSliceAdapter(string source)
            : this(source, 0, source.Length)
        { }

        public char this[int index]
        {
            get
            {
                int source_index = offset + index;
                if (source_index < 0 || source_index >= end_index)
                    throw new IndexOutOfRangeException("index is out of range of this segment.");
                return source_string[source_index];
            }

            set => throw GetReadOnlyException();
        }

        public bool IsReadOnly => true;

        public bool Contains(char item) => IndexOf(item) != -1;

        public void CopyTo(char[] array, int arrayIndex) => source_string.CopyTo(offset, array, arrayIndex, Count);

        public IEnumerator<char> GetEnumerator()
        {
            for (int i = offset; i < end_index; i++)
                yield return source_string[i];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int IndexOf(char item) => source_string.IndexOf(item, offset, Count);

        public void Insert(int index, char item) => throw GetReadOnlyException();

        public bool Remove(char item) => throw GetReadOnlyException();

        public void RemoveAt(int index) => throw GetReadOnlyException();

        public void Add(char item) => throw GetReadOnlyException();

        public void Clear() => throw GetReadOnlyException();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Exception GetReadOnlyException() => new InvalidOperationException("Slices are read only.");

        public override string ToString()
        {
            return source_string;
        }
    }
}
