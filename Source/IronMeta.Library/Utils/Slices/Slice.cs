using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IronMeta.Utils.Slices
{
    public class Slice<T> : IEnumerable<T>, IEnumerable, IList<T>, ICollection<T>, IReadOnlyList<T>, IReadOnlyCollection<T>
    {
        private readonly IList<T> adapter;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="source">The source enumerable.</param>
        /// <param name="start">The start position.</param>
        /// <param name="count">The number of items in the slice.</param>
        public Slice(IEnumerable<T> source, int start, int count)
        {
            adapter = GetAdapterForEnumerable(source, start, count);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="source">The source enumerable.</param>
        public Slice(IEnumerable<T> source)
        {
            adapter = source switch
            {
                string str => new StringSliceAdapter(str) as IList<T>,
                T[] arr => arr,
                Slice<T> slice => slice.adapter,
                IList<T> list => list,
                _ => new Memoizer<T>(source),
            };
        }

        private static IList<T> GetAdapterForEnumerable(IEnumerable<T> source, int start, int count)
        {
            return source switch
            {
                StringSliceAdapter str_adapter => new StringSliceAdapter(str_adapter, start, count) as IList<T>,
                ArraySegment<T> segment => new ArraySegment<T>(segment.Array, start + segment.Offset, count),
                ListSliceAdapter<T> list_adapter => new ListSliceAdapter<T>(list_adapter, start, count),
                Memoizer<T> memo_adapter => new ListSliceAdapter<T>(memo_adapter, start, count),
                Slice<T> slice => GetAdapterForEnumerable(slice.adapter, start, count),

                string str => new StringSliceAdapter(str, start, count) as IList<T>,
                T[] arr when start == 0 && count == arr.Length => arr,
                T[] arr => new ArraySegment<T>(arr, start, count),
                IList<T> list when start == 0 && count == list.Count => list,
                IList<T> list => new ListSliceAdapter<T>(list, start, count),
                _ => new ListSliceAdapter<T>(new Memoizer<T>(source), start, count),
            };
        }

        public T this[int index] { get => adapter[index]; set => throw GetReadOnlyException(); }

        private int Offset
        {
            get
            {
                return adapter switch
                {
                    StringSliceAdapter str_adapter => str_adapter.Offset,
                    ArraySegment<T> segment => segment.Offset,
                    ListSliceAdapter<T> list_adapter => list_adapter.Offset,
                    _ => 0,
                };
            }
        }

        public int Count => adapter.Count;

        public bool IsReadOnly => true;

        public void Add(T item) => throw GetReadOnlyException();

        public void Clear() => throw GetReadOnlyException();

        public bool Contains(T item) => adapter.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) => adapter.CopyTo(array, arrayIndex);

        public IEnumerator<T> GetEnumerator() => adapter.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int IndexOf(T item) => adapter.IndexOf(item);

        public void Insert(int index, T item) => throw GetReadOnlyException();

        public bool Remove(T item) => throw GetReadOnlyException();

        public void RemoveAt(int index) => throw GetReadOnlyException();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Exception GetReadOnlyException() => new InvalidOperationException("Slices are read only.");

        public string GetStringIfCheap()
        {
            return adapter switch
            {
                StringSliceAdapter str => str.SourceString,
                _ => null,
            };
        }
    }
}
