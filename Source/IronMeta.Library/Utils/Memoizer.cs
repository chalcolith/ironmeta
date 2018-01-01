// IronMeta Copyright © Gordon Tisher 2018

using System;
using System.Collections.Generic;

namespace IronMeta.Utils
{
    /// <summary>
    /// An enumerable that memoizes its input.
    /// </summary>
    /// <typeparam name="T">Element type.</typeparam>
    public class Memoizer<T> : IList<T>
    {
        IList<T> memoList = new List<T>();
        readonly IEnumerator<T> innerEnumerator;

        public Memoizer(IEnumerable<T> inner)
        {
            this.innerEnumerator = inner.GetEnumerator();
        }

        void ReadToIndex(int index)
        {
            while (index >= memoList.Count)
            {
                if (!innerEnumerator.MoveNext())
                    throw new IndexOutOfRangeException();
                memoList.Add(innerEnumerator.Current);
            }
        }

        void ReadAll()
        {
            while (innerEnumerator.MoveNext())
            {
                memoList.Add(innerEnumerator.Current);
            }
        }

        #region IList<T> Members

        public int IndexOf(T item)
        {
            ReadAll();
            return memoList.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            ReadToIndex(index);
            memoList.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            ReadToIndex(index);
            memoList.RemoveAt(index);
        }

        public T this[int index]
        {
            get
            {
                ReadToIndex(index);
                return memoList[index];
            }
            set
            {
                ReadToIndex(index);
                memoList[index] = value;
            }
        }

        #endregion

        #region ICollection<T> Members

        public void Add(T item)
        {
            ReadAll();
            memoList.Add(item);
        }

        public void Clear()
        {
            ReadAll();
            memoList.Clear();
        }

        public bool Contains(T item)
        {
            ReadAll();
            return memoList.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            ReadAll();
            memoList.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { ReadAll(); return memoList.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            ReadAll();
            int index = IndexOf(item);
            if (index >= 0)
            {
                RemoveAt(index);
                return true;
            }
            return false;
        }

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return new MemoizingEnumerator<T>(this);
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        class MemoizingEnumerator<TE> : IEnumerator<TE>
        {
            int index;
            Memoizer<TE> inner;

            public MemoizingEnumerator(Memoizer<TE> inner)
            {
                this.inner = inner;
                Reset();
            }

            #region IEnumerator<TE> Members

            public TE Current
            {
                get { return inner.memoList[index]; }
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                inner = null;
            }

            #endregion

            #region IEnumerator Members

            object System.Collections.IEnumerator.Current
            {
                get { return this.Current; }
            }

            public bool MoveNext()
            {
                index++;
                if (index <= inner.memoList.Count - 1)
                    return true;
                if (inner.innerEnumerator.MoveNext())
                {
                    inner.memoList.Add(inner.innerEnumerator.Current);
                    return true;
                }

                return false;
            }

            public void Reset()
            {
                index = -1;
            }

            #endregion
        }
    }
}
