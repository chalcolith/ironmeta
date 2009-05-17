//////////////////////////////////////////////////////////////////////
// $Id$
//
// Copyright (c) 2009, The IronMeta Project
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
            Combinator _Whitespace_Body_ = null;

            if (_Whitespace_Body__Index_ == -1 || CachedCombinators[_Whitespace_Body__Index_] == null)
            {
                if (_Whitespace_Body__Index_ == -1)
                {
                    _Whitespace_Body__Index_ = CachedCombinators.Count;
                    CachedCombinators.Add(null);
                }

                CachedCombinators[_Whitespace_Body__Index_] = _OR(_CALL(EOL), _CONDITION(_ANY(), (_mi_) => (System.Char.IsWhiteSpace(_mi_.Inputs.Last()))));
            }

            _Whitespace_Body_ = CachedCombinators[_Whitespace_Body__Index_];

            foreach (MatchItem _res in _Whitespace_Body_.Match(_indent, _inputs, _index, null, _memo))
                yield return _res;
        }

        private int _Whitespace_Body__Index_ = -1;

        /// \internal
        /// <summary>
        /// Matches end-of-line, and records the start position of the next line.
        /// </summary>
        protected virtual IEnumerable<MatchItem> EOL(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _EOL_Body_ = null;

            if (_EOL_Body__Index_ == -1 || CachedCombinators[_EOL_Body__Index_] == null)
            {
                if (_EOL_Body__Index_ == -1)
                {
                    _EOL_Body__Index_ = CachedCombinators.Count;
                    CachedCombinators.Add(null);
                }

                CachedCombinators[_EOL_Body__Index_] = _ACTION(_OR(_OR(_AND(_LITERAL('\r'), _LITERAL('\n')), _LITERAL('\n')), _AND(_LITERAL('\r'), _NOT(_LITERAL('\n')))), (_IM_Result) => { _IM_LineBeginPositions.Add(_IM_Result.NextIndex); return default(TResult); });
            }

            _EOL_Body_ = CachedCombinators[_EOL_Body__Index_];

            foreach (MatchItem _res in _EOL_Body_.Match(_indent, _inputs, _index, null, _memo))
                yield return _res;
        }

        private int _EOL_Body__Index_ = -1;

        /// \internal
        /// <summary>
        /// Matches the end of input.
        /// </summary>
        protected virtual IEnumerable<MatchItem> EOF(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _EOF_Body_ = null;

            if (_EOF_Body__Index_ == -1 || CachedCombinators[_EOF_Body__Index_] == null)
            {
                if (_EOF_Body__Index_ == -1)
                {
                    _EOF_Body__Index_ = CachedCombinators.Count;
                    CachedCombinators.Add(null);
                }

                CachedCombinators[_EOF_Body__Index_] = _ACTION(_NOT(_ANY()), (_IM_Result) => { _IM_LineBeginPositions.Add(_IM_Result.StartIndex); return default(TResult); });
            }

            _EOF_Body_ = CachedCombinators[_EOF_Body__Index_];

            foreach (MatchItem _res in _EOF_Body_.Match(_indent, _inputs, _index, null, _memo))
                yield return _res;
        }

        private int _EOF_Body__Index_ = -1;

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
