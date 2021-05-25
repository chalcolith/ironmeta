// IronMeta Copyright © Gordon Tisher 2019

using System;
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
        MatchState<TInput, TResult> state = null;

        bool success = false;
        int start = -1;
        int next = -1;
        IEnumerable<TResult> result;
        Func<string> error_func;
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
        internal MatchResult(Matcher<TInput, TResult> matcher, MatchState<TInput, TResult> memo, 
            bool success, int start, int next, IEnumerable<TResult> result, Func<string> error_func, int errorIndex)
        {
            this.matcher = matcher;
            this.state = memo;
            this.success = success;
            this.start = start;
            this.next = next;
            this.result = result;
            this.error_func = error_func;
            //this.error = error_func();
            this.errorIndex = errorIndex;
        }

        /// <summary>
        /// The matcher that generated this result.
        /// </summary>
        public Matcher<TInput, TResult> Matcher { get { return matcher; } } 

        /// <summary>
        /// The memo object that holds the match state.
        /// </summary>
        public MatchState<TInput, TResult> MatchState { get { return state; } }

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
        public string Error
        {
            get
            {
                return error ??= error_func();
            }
        }

        /// <summary>
        /// The index in the input stream at which the error occurred.
        /// </summary>
        public int ErrorIndex { get { return errorIndex; } }
    }
}
