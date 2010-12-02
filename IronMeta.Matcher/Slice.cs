//////////////////////////////////////////////////////////////////////
// $Id$
//
// Copyright (C) 2009-2010, The IronMeta Project
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
    /// </summary>
    /// <typeparam name="T">The enumerable's data type.</typeparam>
    public class Slice<T> : IList<T>
    {
        IList<T> list;
        IEnumerable<T> enumerable;
        int start, count;

        string str = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="data">The underling data.</param>
        /// <param name="start">The start position.</param>
        /// <param name="count">The number of items in the slice.</param>
        public Slice(IEnumerable<T> data, int start, int count)
        {
            this.list = data as IList<T>;
            this.enumerable = data;
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
                sb.Append(e.Current.ToString());
            return str = sb.ToString();
        }

        #region IList<T> Members

        public int IndexOf(T item)
        {
            for (int i = 0; i < count; ++i)
            {
                if (enumerable.ElementAt(i + start).Equals(item))
                    return i;
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            if (list == null)
                throw new NotImplementedException();

            list.Insert(index + start, item);
            ++count;
            str = null;
        }

        public void RemoveAt(int index)
        {
            if (list == null)
                throw new NotImplementedException();

            list.RemoveAt(index + start);
            --count;
            str = null;
        }

        public T this[int index]
        {
            get
            {
                if (list != null)
                    return list[index + start];
                else
                    return enumerable.ElementAt(index + start);
            }
            set
            {
                if (list == null)
                    throw new NotImplementedException();
                list[index + start] = value;
                str = null;
            }
        }

        #endregion

        #region ICollection<T> Members

        public void Add(T item)
        {
            if (list == null)
                throw new NotImplementedException();
            list.Insert(start + count++, item);
            str = null;
        }

        public void Clear()
        {
            if (list == null)
                throw new NotImplementedException();

            for (int i = 0; i < count; ++i)
                list.RemoveAt(start);
            count = 0;
            str = null;
        }

        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            int max = (array.Length - arrayIndex) > count ? array.Length - arrayIndex : count;
            for (int i = 0; i < max; ++i)
                array[i + arrayIndex] = enumerable.ElementAt(i + start);
        }

        public int Count
        {
            get { return count; }
        }

        public bool IsReadOnly
        {
            get { if (list != null) return list.IsReadOnly; else return true; }
        }

        public bool Remove(T item)
        {
            if (list == null)
                return false;

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

        public IEnumerator<T> GetEnumerator()
        {
            return new SliceEnumerator(this);
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        private class SliceEnumerator : IEnumerator<T>
        {
            Slice<T> slice;
            int pos = -1;

            public SliceEnumerator(Slice<T> slice)
            {
                this.slice = slice;
            }

            #region IEnumerator<T> Members

            public T Current
            {
                get
                {
                    if (pos >= 0 && pos < slice.count)
                    {
                        return slice[pos];
                    }
                    else
                        throw new InvalidOperationException("Invalid enumerator position.");
                }
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                slice = null;
            }

            #endregion

            #region IEnumerator Members

            object System.Collections.IEnumerator.Current
            {
                get { return Current; }
            }

            public bool MoveNext()
            {
                return ++pos < slice.count;
            }

            public void Reset()
            {
                pos = 0;
            }

            #endregion
        }
    } // class Slice

} // namespace Matcher
