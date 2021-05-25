// IronMeta Copyright © Gordon Tisher 2019

using System;
using System.Collections.Generic;
using System.Linq;

using IronMeta.Utils;

namespace IronMeta.Matcher
{
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
        public Action<MatchState<TInput, TResult>, int, IEnumerable<MatchItem<TInput, TResult>>> Production = null;

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
                if (input_slice == null)
                {
                    if (input_start >= 0 && input_next > input_start)
                    {
                        TInput[] input_array = input_enumerable as TInput[];
                        if (input_array != null)
                            input_slice = new ArraySegment<TInput>(input_array, input_start, input_next - input_start);
                        else
                            input_slice = new Slice<TInput>(input_enumerable, input_start, input_next - input_start);
                    }
                    else
                    {
                        input_slice = emptyInputs;
                    }
                }
                return input_slice;
            }

            set
            {
                input_enumerable = input_slice = value;
            }
        }

        /// <summary>
        /// The entire input enumerable that this item takes its input from.
        /// </summary>
        public IEnumerable<TInput> InputEnumerable
        {
            get
            {
                return input_enumerable;
            }

            set
            {
                input_enumerable = value;
                input_slice = null;
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
            input_enumerable = input_slice = new [] { input };
            input_start = 0;
            input_next = 1;
            Results = emptyResults;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="input">A single input to hold.</param>
        /// <param name="result">The result of the input.</param>
        public MatchItem(TInput input, TResult result)
        {
            input_enumerable = input_slice = new [] { input };
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
            Results = emptyResults;
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
            input_enumerable = input_slice = new [] { input };
            input_start = 0;
            input_next = 1;
            Results = new [] { result };
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="p">Production to pass.</param>
        public MatchItem(Action<MatchState<TInput, TResult>, int, IEnumerable<MatchItem<TInput, TResult>>> p)
        {
            Production = p;
            ProductionName = p.Method.Name;
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
                        string inputs = string.Join(",", 
                            Inputs.Select(i => i != null ? i.ToString() : "<null>").ToArray());
                        string results = string.Join(",", 
                            Results.Select(r => r != null ? r.ToString() : "<null>").ToArray());

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
    }
}
