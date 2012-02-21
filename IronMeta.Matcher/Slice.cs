//////////////////////////////////////////////////////////////////////
// $Id$
//
// Copyright (C) 2009-2012, The IronMeta Project
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without 
// modification, are permitted provided that the following conditions 
// are met:
// 
//     * Redistributions of source code must retain the above 
//       copyright notice, this list of conditions and the following 
//       disclaimer.
//     * Redistributions in binary form must reproduce the above 
//       copyright notice, this list of conditions and the following 
//       disclaimer in the documentation and/or other materials 
//       provided with the distribution.
//     * Neither the name of the IronMeta Project nor the names of its 
//       contributors may be used to endorse or promote products 
//       derived from this software without specific prior written 
//       permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS 
// "AS IS" AND  ANY EXPRESS OR  IMPLIED WARRANTIES, INCLUDING, BUT NOT 
// LIMITED TO, THE  IMPLIED WARRANTIES OF  MERCHANTABILITY AND FITNESS 
// FOR  A  PARTICULAR  PURPOSE  ARE DISCLAIMED. IN  NO EVENT SHALL THE 
// COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, 
// INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, 
// BUT NOT  LIMITED TO, PROCUREMENT  OF SUBSTITUTE  GOODS  OR SERVICES; 
// LOSS OF USE, DATA, OR  PROFITS; OR  BUSINESS  INTERRUPTION) HOWEVER 
// CAUSED AND ON ANY THEORY OF  LIABILITY, WHETHER IN CONTRACT, STRICT 
// LIABILITY, OR  TORT (INCLUDING NEGLIGENCE  OR OTHERWISE) ARISING IN 
// ANY WAY OUT  OF THE  USE OF THIS SOFTWARE, EVEN  IF ADVISED  OF THE 
// POSSIBILITY OF SUCH DAMAGE.
//
//////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronMeta.Matcher
{

    /// <summary>
    /// A utility class that implements a slice of an enumerable.
    /// Uses copy-on-write semantics; if the slice's data is modified then a copy is taken of the original data and the copy modified instead of the original.
    /// </summary>
    /// <typeparam name="T">The enumerable's data type.</typeparam>
    public class Slice<T> : IList<T>
    {
        IEnumerable<T> enumerable;
        IList<T> list;
        int start, count;
        bool copied = false;
        string str = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="source">The source enumerable.</param>
        /// <param name="start">The start position.</param>
        /// <param name="count">The number of items in the slice.</param>
        public Slice(IEnumerable<T> source, int start, int count)
        {
            this.enumerable = source;
            this.list = source as IList<T>;
            this.start = start;
            this.count = count;
        }

        /// <summary>
        /// String representation (mainly used for debugging).
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return str ?? BuildString();
        }

        string BuildString()
        {
            StringBuilder sb = new StringBuilder();
            var e = GetEnumerator();
            while (e.MoveNext())
            {
                if (sb.Length > 0)
                    sb.Append(" ");
                sb.Append(e.Current.ToString());
            }
            return str = sb.ToString();
        }

        void Detach()
        {
            if (!copied)
            {
                list = this.ToList();
                enumerable = list;
                start = 0;
                count = list.Count;
                copied = true;
            }
        }

        #region IList<T> Members

        /// <summary>
        /// The index of the item in the slice.
        /// </summary>
        /// <param name="item">The item to search for.</param>
        /// <returns>-1 if the item is not found.</returns>
        public int IndexOf(T item)
        {
            if (list != null)
                return list.IndexOf(item);

            int i = 0;
            foreach (T in_list in enumerable)
            {
                if (in_list.Equals(item))
                    return i;
                ++i;
            }

            return -1;
        }

        /// <summary>
        /// Inserts an item into the slice.  Will not modify the source enumerable.
        /// </summary>
        /// <param name="index">The index at which to insert.</param>
        /// <param name="item">The item to insert.</param>
        public void Insert(int index, T item)
        {
            Detach();

            list.Insert(index + start, item);
            ++count;
            str = null;
        }

        /// <summary>
        /// Removes the item at a given index in the slice.  Will not modify the source enumerable.
        /// </summary>
        /// <param name="index">The index of the item to remove.</param>
        public void RemoveAt(int index)
        {
            Detach();

            list.RemoveAt(index + start);
            --count;
            str = null;
        }

        /// <summary>
        /// Indexer.  Will not modify the source enumerable.
        /// </summary>
        /// <param name="index">Index.</param>
        /// <returns>The item at the given index.</returns>
        public T this[int index]
        {
            get
            {
                return list != null ? list[index + start] : enumerable.ElementAt(index + start);
            }
            set
            {
                Detach();
                list[index + start] = value;
                str = null;
            }
        }

        #endregion

        #region ICollection<T> Members

        /// <summary>
        /// Adds an item to the slice.  Will not modify the source enumerable.
        /// </summary>
        /// <param name="item">Item to add.</param>
        public void Add(T item)
        {
            Detach();
            list.Insert(start + count++, item);
            str = null;
        }

        /// <summary>
        /// Clears the slice.  Will not modify the source enumerable.
        /// </summary>
        public void Clear()
        {
            Detach();
            list.Clear();
            count = 0;
            str = null;
        }

        /// <summary>
        /// Whether or not the slice contains a given item.
        /// </summary>
        /// <param name="item">Item.</param>
        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
        }

        /// <summary>
        /// Copies the slice to an array.
        /// </summary>
        /// <param name="array">Array to copy to.</param>
        /// <param name="arrayIndex">Index in the array to start copying at.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            int max = (array.Length - arrayIndex) > count ? array.Length - arrayIndex : count;
            for (int i = 0; i < max; ++i)
                array[i + arrayIndex] = enumerable.ElementAt(i + start);
        }

        /// <summary>
        /// Number of items in the slice.
        /// </summary>
        public int Count
        {
            get { return count; }
        }

        /// <summary>
        /// Whether or not the slice is read-only.
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Removes an item from the slice.  Will not modify the source enumerable.
        /// </summary>
        /// <param name="item">Item to remove.</param>
        /// <returns>Whether or not the item was removed.</returns>
        public bool Remove(T item)
        {
            Detach();

            int index = IndexOf(item);
            if (index != -1)
            {
                list.RemoveAt(index + start);
                --count;
                str = null;
                return true;
            }
            return false;
        }

        #endregion

        #region IEnumerable<T> Members

        /// <summary>
        /// Get an enumerator over the slice.
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            return new SliceEnumerator(this);
        }

        #endregion

        #region IEnumerable Members

        /// <summary>
        /// Gets a non-generic enumerator of the slice.
        /// </summary>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        /// <summary>
        /// Enumerator for slices.
        /// </summary>
        private class SliceEnumerator : IEnumerator<T>
        {
            Slice<T> slice;
            IEnumerator<T> source_enumerator;
            int pos = -1;

            /// <summary>
            /// Constructor.
            /// </summary>
            /// <param name="slice">Slice to enumerate over.</param>
            internal SliceEnumerator(Slice<T> slice)
            {
                this.slice = slice;
                Reset();
            }

            #region IEnumerator<T> Members

            /// <summary>
            /// The current value.
            /// </summary>
            public T Current { get { return source_enumerator.Current; } }

            #endregion

            #region IDisposable Members

            /// <summary>
            /// Dispose.
            /// </summary>
            public void Dispose()
            {
                slice = null;
                source_enumerator = null;
            }

            #endregion

            #region IEnumerator Members

            /// <summary>
            /// The current object.
            /// </summary>
            object System.Collections.IEnumerator.Current
            {
                get { return Current; }
            }

            /// <summary>
            /// Move to the next item in the enumerable.
            /// </summary>
            /// <returns>Whether or not the move succeeded.</returns>
            public bool MoveNext()
            {
                return ++pos < slice.Count && source_enumerator.MoveNext();
            }

            /// <summary>
            /// Reset the enumerator to the beginning of the enumerable.
            /// </summary>
            public void Reset()
            {
                source_enumerator = slice.enumerable.GetEnumerator();
                for (int i = 0; i < slice.start; ++i)
                    source_enumerator.MoveNext();
                pos = -1;
            }

            #endregion
        }
    } // class Slice

} // namespace Matcher
