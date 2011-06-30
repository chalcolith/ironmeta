//////////////////////////////////////////////////////////////////////
// $Id: Matcher.cs 125 2010-11-10 23:45:07Z kulibali $
//
// Copyright (C) 2009-2011, The IronMeta Project
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
    /// <typeparam name="TItem">Item type (internal).</typeparam>
    public abstract class CharMatcher<TResult, TItem> : Matcher<char, TResult, TItem>
        where TItem : MatchItem<char, TResult, TItem>, new()
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public CharMatcher()
            : base()
        {
        }

        /// <summary>
        /// Gets the line containing a particular index in the input.
        /// </summary>
        /// <param name="memo">The memo object from the match (can be obtained from the match result object's <c>Memo</c> property).</param>
        /// <param name="pos">The index in the input.</param>
        /// <param name="offset">The offset of the position after the beginning of the line.</param>
        /// <returns>The line containing a particular input index.</returns>
        public static string GetLine(Memo<char, TResult, TItem> memo, int pos, out int offset)
        {
            var line_begins = MakeLines(memo);
            offset = 0;

            int index = GetLineNumber(memo, pos) - 1;
            int start = line_begins[index];
            offset = pos - start;

            int len = index + 1 < line_begins.Count ? line_begins[index + 1] - line_begins[index] : line_begins.Count - line_begins[index];

            if (memo.InputString != null)
            {
                return memo.InputString.Substring(start, len);
            }
            else
            {
                IEnumerable<char> result = memo.InputEnumerable.Skip(start).Take(len).Cast<char>();
                return new string(result.ToArray());
            }
        }

        /// <summary>
        /// Gets the line number of the line that contains a particular index in the input.
        /// </summary>
        /// <param name="memo">The memo used for the match.</param>
        /// <param name="pos">The index in the input.</param>
        /// <returns>The number of the line that contains the index.</returns>
        public static int GetLineNumber(Memo<char, TResult, TItem> memo, int pos)
        {
            var line_begins = MakeLines(memo);

            int low = 0, high = line_begins.Count - 1;
            int index = low + (high - low) / 2;
            while (line_begins[index] != pos && low < high)
            {
                if (line_begins[index] > pos)
                    high = index - 1;
                else
                    low = index + 1;
                index = low + (high - low) / 2;
            }

            return index > 0 ? index : 1;
        }

        static readonly Regex EOL = new Regex(@"\r\n|\n|\r", RegexOptions.Compiled);

        /// <summary>
        /// Finds line endings.
        /// </summary>
        static List<int> MakeLines(Memo<char, TResult, TItem> memo)
        {
            object obj;
            if (memo.Properties.TryGetValue("_line_begins", out obj) && obj is List<int>)
                return (List<int>)obj;

            var line_begins = new List<int>();
            line_begins.Add(0);

            var str = memo.InputString ?? new string(memo.Input.ToArray());

            var matches = EOL.Matches(str);
            foreach (Match match in matches)
            {
                line_begins.Add(match.Index + match.Length);
            }

            line_begins.Add(memo.InputString.Length);

            memo.Properties["_line_begins"] = line_begins;
            return line_begins;
        }

    }  // class CharMatcher

} // namespace IronMeta.Matcher
