//////////////////////////////////////////////////////////////////////
// $Id$
//
// Copyright (c) The IronMeta Project 2009
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
//////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronMeta
{

    public abstract class CharacterMatcher<TResult> : Matcher<char, TResult>
    {
        /// \internal
        /// <summary>
        /// A set of positions of the beginnings of lines in the item stream.
        /// </summary>
        protected HashSet<int> _IM_LineBeginPositions { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public CharacterMatcher(Func<char, TResult> convertItem, bool strictPEG)
            : base(convertItem, strictPEG)
        {
            _IM_LineBeginPositions = new HashSet<int>();
            _IM_LineBeginPositions.Add(0);
        } // CharacterMatcher()

        #region Character Combinators

        // Whitespace = (EOL | :c ?? (System.Char.IsSpace(c)))
        /// \internal
        /// <summary>Matches any whitespace character.</summary>
        protected virtual IEnumerable<MatchItem> Whitespace(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            if (_Whitespace_Body_ == null)
                _Whitespace_Body_ = _OR(_CALL(EOL), _CONDITION(_ANY(), (_mi_) => (System.Char.IsWhiteSpace(_mi_.Inputs.Last()))));

            foreach (MatchItem _res in _Whitespace_Body_.Match(_indent, _inputs, _index, null, _memo))
                yield return _res;
        }

        private static Combinator _Whitespace_Body_ = null;

        /// \internal
        /// <summary>
        /// Matches end-of-line, and records the start position of the next line.
        /// </summary>
        protected virtual IEnumerable<MatchItem> EOL(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            if (_EOL_Body_ == null)
                _EOL_Body_ = _ACTION(_OR(_OR(_AND(_LITERAL('\r'), _LITERAL('\n')), _LITERAL('\n')), _AND(_LITERAL('\r'), _NOT(_LITERAL('\n')))), (_IM_Result) => { _IM_LineBeginPositions.Add(_IM_Result.NextIndex); return default(TResult); });

            foreach (MatchItem _res in _EOL_Body_.Match(_indent, _inputs, _index, null, _memo))
                yield return _res;
        }

        private static Combinator _EOL_Body_ = null;

        /// \internal
        /// <summary>
        /// Matches the end of input.
        /// </summary>
        protected virtual IEnumerable<MatchItem> EOF(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            if (_EOF_Body_ == null)
                _EOF_Body_ = _ACTION(_NOT(_ANY()), (_IM_Result) => { _IM_LineBeginPositions.Add(_IM_Result.StartIndex); return default(TResult); });

            foreach (MatchItem _res in _EOF_Body_.Match(_indent, _inputs, _index, null, _memo))
                yield return _res;
        }

        private static Combinator _EOF_Body_ = null;

        #endregion

        #region Utility Functions

        private int[] LineBeginsArray = null;

        /// <summary>
        /// Get the line number of the given index.  This will only work if you have used CharacterMatcher.EOL as your end-of-line rule.
        /// </summary>
        /// <param name="index">The index in the input stream.</param>
        /// <param name="offset">The offset in the line.</param>
        public int GetLineNumber(int index, out int offset)
        {
            // assume we're all done parsing
            if (LineBeginsArray == null)
            {
                LineBeginsArray = _IM_LineBeginPositions.ToArray();
                Array.Sort(LineBeginsArray);
            }

            // find line
            int foundPos = Array.BinarySearch(LineBeginsArray, index);

            if (foundPos >= 0)
            {
                offset = 0;
                return foundPos + 1;
            }
            else if (~foundPos < LineBeginsArray.Length)
            {
                int firstLarger = ~foundPos;
                offset = firstLarger > 0 ? index - LineBeginsArray[firstLarger-1] : index;
                return firstLarger;
            }

            offset = index - LineBeginsArray[LineBeginsArray.Length - 1];
            return LineBeginsArray.Length - 1;
        }

        public int GetLineNumber(int index)
        {
            int offset;
            return GetLineNumber(index, out offset);
        }

        /// <summary>
        /// Gets a line of text from your input stream.  This will only work properly if you have used CharacterMatcher.EOL as your end-of-line rule.
        /// </summary>
        public string GetLine(IEnumerable<char> stream, int lineNumber)
        {
            // assume we're all done parsing
            if (LineBeginsArray == null)
            {
                LineBeginsArray = _IM_LineBeginPositions.ToArray();
                Array.Sort(LineBeginsArray);
            }

            if (lineNumber < 0 || lineNumber > LineBeginsArray.Length - 1)
                return "";

            // find line
            int startIndex = LineBeginsArray[lineNumber];
            int nextIndex = (lineNumber + 1) < LineBeginsArray.Length ? LineBeginsArray[lineNumber + 1] : stream.Count();

            StringBuilder sb = new StringBuilder();

            for (int i = startIndex; i < nextIndex; ++i)
            {
                char ch = stream.ElementAt(i);
                if (ch == '\n' || ch == '\r')
                    break;

                sb.Append(ch);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Can be used in an IronMeta condition or action to get a string corresponding to the input matched by an expression.
        /// </summary>
        protected string _IM_GetText(MatchItem item)
        {
            return string.Join("", item.Inputs.Select(i => i.ToString()).ToArray());
        }

        #endregion

    } // class CharacterMatcher

} // namespace IronMeta
