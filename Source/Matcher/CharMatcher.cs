//////////////////////////////////////////////////////////////////////
//
// Copyright © 2014 Gordon Tisher
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
        /// Get the line in the input for a given index.
        /// </summary>
        /// <param name="memo">Memo to use.</param>
        /// <param name="index">Index in the input.</param>
        /// <param name="line">The line number (1-based) of the line.</param>
        /// <param name="offset">The offset in the line of the given index.</param>
        /// <returns>The line for a given index.</returns>
        public static string GetLine(Memo<char, TResult> memo, int index, out int line, out int offset)
        {
            int[] begins;

            object prop;
            if (memo.Properties.TryGetValue("lineBeginnings", out prop) && prop is int[])
            {
                begins = (int[])prop;
            }
            else
            {
                memo.Positions.Add(0);
                begins = memo.Positions.OrderBy(n => n).ToArray();
                memo.Properties.Add("lineBeginnings", begins);
            }

            int inputIndex, inputNext = int.MaxValue;
            
            int arrayIndex = Array.BinarySearch<int>(begins, index);
            if (arrayIndex >= 0)
            {
                line = arrayIndex + 1;
                offset = 0;

                inputIndex = begins[arrayIndex];
                if ((arrayIndex + 1) < begins.Length)
                    inputNext = begins[arrayIndex + 1];
            }
            else
            {
                int nextLargestArrayIndex = ~arrayIndex;

                line = nextLargestArrayIndex;
                offset = index - begins[nextLargestArrayIndex - 1];

                inputIndex = begins[nextLargestArrayIndex - 1];
                if (nextLargestArrayIndex < begins.Length)
                    inputNext = begins[nextLargestArrayIndex];
            }

            return new string(memo.InputEnumerable.Skip(inputIndex).Take(inputNext - inputIndex).TakeWhile(ch => ch != '\r' && ch != '\n').ToArray());
        }

    }  // class CharMatcher

} // namespace IronMeta.Matcher
