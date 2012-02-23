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

using System.Collections.Generic;
using System.Linq;

namespace IronMeta.Matcher
{

    /// <summary>
    /// Holds the results of trying to parse an input stream.
    /// </summary>
    public class MatchResult<TInput, TResult>
    {
        Matcher<TInput, TResult> matcher = null;
        Memo<TInput, TResult> memo = null;

        bool success = false;
        int start = -1;
        int next = -1;
        IEnumerable<TResult> result;
        string error;
        int errorIndex;

        /// <summary>
        /// Constructor.
        /// </summary>
        internal MatchResult()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        internal MatchResult(Matcher<TInput, TResult> matcher, Memo<TInput, TResult> memo, bool success, int start, int next, IEnumerable<TResult> result, string error, int errorIndex)
        {
            this.matcher = matcher;
            this.memo = memo;
            this.success = success;
            this.start = start;
            this.next = next;
            this.result = result;
            this.error = error;
            this.errorIndex = errorIndex;
        }

        /// <summary>
        /// The matcher that generated this result.
        /// </summary>
        public Matcher<TInput, TResult> Matcher { get { return matcher; } } 

        /// <summary>
        /// The memo object that holds the match state.
        /// </summary>
        public Memo<TInput, TResult> Memo { get { return memo; } }

        /// <summary>
        /// Whether or not the match succeeded.
        /// </summary>
        public bool Success { get { return success; } }

        /// <summary>
        /// The index in the input stream at which the match started (usually 0).
        /// </summary>
        public int StartIndex { get { return start; } }

        /// <summary>
        /// The index in the input stream after the last item matched.
        /// </summary>
        public int NextIndex { get { return next; } }

        /// <summary>
        /// The result of the match; possibly as a list.
        /// Will be null if the match did not succeed.
        /// </summary>
        public IEnumerable<TResult> Results { get { return result; } }

        /// <summary>
        /// The last result in the result list.  Will throw if the match did not succeed.
        /// </summary>
        public TResult Result { get { return result.LastOrDefault(); } }

        /// <summary>
        /// The error that caused the match to fail, if it failed.
        /// </summary>
        public string Error { get { return error; } }

        /// <summary>
        /// The index in the input stream at which the error occurred.
        /// </summary>
        public int ErrorIndex { get { return errorIndex; } }

    } // class MatchResult

} // namespace Matcher
