//////////////////////////////////////////////////////////////////////
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

namespace IronMeta.Matcher
{

    /// \internal
    /// <summary>
    /// Used internally in matchers in several situations:
    ///  - As the result of a production, stores a range of input, and the results of the match.
    ///  - As a parameter to a production, can hold an item of input, a range of input, or a production.
    /// </summary>
    public class MatchItem<TInput, TResult>
    {
        static readonly IEnumerable<TInput> emptyInputs = Enumerable.Empty<TInput>();
        static readonly IEnumerable<TResult> emptyResults = Enumerable.Empty<TResult>();

        IEnumerable<TInput> input_enumerable = emptyInputs;
        IEnumerable<TInput> input_slice = null;

        int start_index = -1;
        int next_index = -1;
        int input_start = -1;
        int input_next = -1;

        private string id = null;

        #region Properties

        /// <summary>
        /// The production that this item is passing.
        /// </summary>
        public Action<Memo<TInput, TResult>, int, IEnumerable<MatchItem<TInput, TResult>>> Production = null;

        /// <summary>
        /// The name of the production that this item is passing.
        /// </summary>
        public string ProductionName = null;

        /// <summary>
        /// The starting index in the match (not necessarily in the item's particular input).
        /// </summary>
        public int StartIndex
        {
            get { return start_index; }
            set { start_index = input_start = value; }
        }

        /// <summary>
        /// The next index in the match (not necessarily in the item's particular input).
        /// </summary>
        public int NextIndex
        {
            get { return next_index; }
            set { next_index = input_next = value; }
        }

        /// <summary>
        /// Returns an enumerable representing only this item's inputs (possibly a subset of InputEnumerable).
        /// </summary>
        public IEnumerable<TInput> Inputs
        {
            get
            {
                return input_slice ?? (input_slice = new Slice<TInput>(input_enumerable, input_start, input_next - input_start));
            }

            set
            {
                input_slice = value;
                input_enumerable = value;
            }
        }

        /// <summary>
        /// The entire input enumerable that this item takes its input from.
        /// </summary>
        public IEnumerable<TInput> InputEnumerable
        {
            get { return input_enumerable; }

            set
            {
                input_slice = null;
                input_enumerable = value;
            }
        }

        /// <summary>
        /// The results that this item holds.
        /// </summary>
        public IEnumerable<TResult> Results = emptyResults;

        #endregion

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MatchItem()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="input">A single input to hold.</param>
        public MatchItem(TInput input)
        {
            input_enumerable = input_slice = new TInput[] { input };
            input_start = 0;
            input_next = 1;
            Results = Enumerable.Empty<TResult>();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="input">A single input to hold.</param>
        /// <param name="result">The result of the input.</param>
        public MatchItem(TInput input, TResult result)
        {
            input_enumerable = input_slice = new TInput[] { input };
            input_start = 0;
            input_next = 1;
            Results = new TResult[] { result };
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="inputs">Inputs to hold.</param>
        public MatchItem(IEnumerable<TInput> inputs)
        {
            Inputs = inputs;
            Results = Enumerable.Empty<TResult>();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="inputs">Inputs to hold.</param>
        /// <param name="results">The corresponding results.</param>
        public MatchItem(IEnumerable<TInput> inputs, IEnumerable<TResult> results)
        {
            Inputs = inputs;
            Results = results;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="start">Start position in the match (not necessarily in the given inputs).</param>
        /// <param name="next">Next position in the match (not necessarily in the given inputs).</param>
        /// <param name="inputs">Input enumerable.</param>
        /// <param name="results">Result enumerable.</param>
        /// <param name="relative">Whether or not the start and next parameters are relative to the given input enumerable, or independent of it.</param>
        public MatchItem(int start, int next, IEnumerable<TInput> inputs, IEnumerable<TResult> results, bool relative)
        {
            input_enumerable = inputs;

            StartIndex = start;
            NextIndex = next;

            if (relative)
            {
                input_start = start;
                input_next = next;
            }
            else
            {
                input_start = 0;
                input_next = inputs.Count();
            }

            Results = results;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="start">Start position in the match.</param>
        /// <param name="inputs">Inputs to hold.</param>
        public MatchItem(int start, IEnumerable<TInput> inputs)
            : this(start, start, inputs, Enumerable.Empty<TResult>(), true)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="start">Start position in the match.</param>
        /// <param name="next">Next position in the match.</param>
        /// <param name="input">A single input to hold.</param>
        /// <param name="result">A single result to hold.</param>
        public MatchItem(int start, int next, TInput input, TResult result)
        {
            StartIndex = start;
            NextIndex = next;
            input_enumerable = input_slice = new TInput[] { input };
            input_start = 0;
            input_next = 1;
            Results = new TResult[] { result };
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="p">Production to pass.</param>
        public MatchItem(Action<Memo<TInput, TResult>, int, IEnumerable<MatchItem<TInput, TResult>>> p)
        {
            this.Production = p;
            this.ProductionName = p.Method.Name;
        }

        /// <summary>
        /// String representation.  This is used to memoize rules with variable arguments.
        /// </summary>
        public override string ToString()
        {
            if (id == null)
            {
                if (Production != null)
                {
                    id = string.Format("{{ {0} }}", Production.Method.Name);
                }
                else
                {
                    try
                    {
                        string inputs = string.Join(",", Inputs.Select(i => i != null ? i.ToString() : "<null>").ToArray());
                        string results = string.Join(",", Results.Select(r => r != null ? r.ToString() : "<null>").ToArray());

                        id = string.Format("{0}-{1} [{2}] -> [{3}]", StartIndex, NextIndex, inputs, results);
                    }
                    catch
                    {
                        throw;
                    }
                }
            }

            return id;
        }

        /// <summary>
        /// Implicit conversion to input type.
        /// </summary>
        /// <param name="item">Item.</param>
        public static implicit operator TInput(MatchItem<TInput, TResult> item) { return item != null ? item.Inputs.LastOrDefault() : default(TInput); }

        /// <summary>
        /// Implicit conversion to result type.
        /// </summary>
        /// <param name="item">Item.</param>
        public static implicit operator TResult(MatchItem<TInput, TResult> item) { return item != null ? item.Results.LastOrDefault() : default(TResult); }


    } // class MatchItem

} // namespace MatchItem
