//////////////////////////////////////////////////////////////////////
// $Id: Matcher.cs 125 2010-11-10 23:45:07Z kulibali $
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
using System.Text.RegularExpressions;

namespace IronMeta.Matcher
{

    /// <summary>
    /// A matcher class for operating on streams of characters.  Provides some utilities for line numbers.
    /// </summary>
    /// <typeparam name="TResult">Result type.</typeparam>
    public abstract class CharMatcher<TResult> : Matcher<char, TResult>
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public CharMatcher()
            : base()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="handle_left_recursion">Whether or not to handle left recursion.</param>
        public CharMatcher(bool handle_left_recursion)
            : base(handle_left_recursion)
        {
        }

        /// <summary>
        /// Utility function for getting the input that matched from a variable.
        /// </summary>
        /// <param name="item">Variable that matched.</param>
        /// <returns>The input that matched the variable, as a string.</returns>
        protected static string Input(MatchItem<char, TResult> item)
        {
            return new string(item.Inputs.ToArray());
        }

        /// <summary>
        /// Utility function for getting the input that matched from a variable.
        /// </summary>
        /// <param name="item">Variable that matched.</param>
        /// <returns>The input that matched the variable, with whitspace trimmed.</returns>
        protected static string Trimmed(MatchItem<char, TResult> item)
        {
            return new string(item.Inputs.ToArray()).Trim();
        }

        /// <summary>
        /// Gets the line containing a particular index in the input.
        /// </summary>
        /// <param name="memo">The memo object from the match (can be obtained from the match result object's <c>Memo</c> property).</param>
        /// <param name="pos">The index in the input.</param>
        /// <param name="offset">The offset of the position after the beginning of the line.</param>
        /// <returns>The line containing a particular input index.</returns>
        public static string GetLine(Memo<char, TResult> memo, int pos, out int offset)
        {
            int num, start, next;

            GetLineInfo(memo, pos, out num, out start, out next);

            offset = pos - start;
            var result = new string(memo.InputEnumerable.Skip(start).Take(next - start).TakeWhile((ch, i) => i < (pos-start) || (ch != '\r' && ch != '\n')).ToArray());
            return result;
        }

        /// <summary>
        /// Gets the line number of the line that contains a particular index in the input.
        /// </summary>
        /// <param name="memo">The memo used for the match.</param>
        /// <param name="pos">The index in the input.</param>
        /// <returns>The number of the line that contains the index.</returns>
        public static int GetLineNumber(Memo<char, TResult> memo, int pos)
        {
            int num, start, next;
            GetLineInfo(memo, pos, out num, out start, out next);
            return start;
        }

        static void GetLineInfo(Memo<char, TResult> memo, int pos, out int num, out int start, out int next)
        {
            int[] line_beginnings = memo.Positions.OrderBy(n => n).ToArray();

            start = -1;
            next = -1;
            num = -1;

            if (line_beginnings == null)
                return;

            int prev = 0;
            for (int i = 0; i < line_beginnings.Length; ++i)
            {
                if (pos < line_beginnings[i])
                {
                    num = i + 1;
                    start = prev;
                    next = line_beginnings[i];
                    break;
                }

                prev = line_beginnings[i];
            }

            if (start == -1)
                start = prev;
            if (next == -1)
                next = int.MaxValue;
        }

    }  // class CharMatcher

} // namespace IronMeta.Matcher
