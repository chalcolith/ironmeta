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

//#define ENABLE_TRACING
#define ENABLE_ERROR_HANDLING

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Reflection;

namespace IronMeta
{

    /// <summary>
    /// Base class for IronMeta matchers.
    /// </summary>
    /// <typeparam name="TInput">The type of input to the matcher.</typeparam>
    /// <typeparam name="TResult">The type each match will result in.</typeparam>
    public abstract class Matcher<TInput, TResult>
    {

        /// \internal
        /// <summary>
        /// A function that converts from the item type to the output type.
        /// </summary>
        protected Func<TInput, TResult> CONV;

        /// \internal
        /// <summary>
        /// Controls the degree of backtracking.
        /// </summary>
        private bool strictPEG = true;

        /// <summary>
        /// Determines whether or not the matcher will use strict Parsing Expression Grammar semantics.
        /// </summary>
        public bool StrictPEG
        {
            get { return strictPEG; }
            set { strictPEG = value; }
        }

        /// <summary>
        /// Construct a new Matcher object.
        /// </summary>
        /// <param name="convertItem">A delegate that holds a function that converts from the input type to the output type.</param>
        /// <param name="strictPEG">Whether or not to use strict Parsing Expression Grammar semantics.</param>
        protected Matcher(Func<TInput, TResult> convertItem, bool strictPEG)
        {
            this.CONV = convertItem;
            this.strictPEG = strictPEG;
        } // Matcher()


        //////////////////////////////////////////////////////////////

        /// <summary>
        /// Holds a match result from applying a parser to an input stream.
        /// </summary>
        public class MatchResult
        {
            bool success = false;
            int start = -1;
            int next = -1;
            IEnumerable<TResult> result;
            string error;
            int errorIndex;

            internal MatchResult()
            {
            }

            internal MatchResult(bool success, int start, int next, IEnumerable<TResult> result, string error, int errorIndex)
            {
                this.success = success;
                this.start = start;
                this.next = next;
                this.result = result;
                this.error = error;
                this.errorIndex = errorIndex;
            }

            /// <summary>
            /// Indicates whether or not the match succeeded.
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
            /// Will throw if the match did not succeed.
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
        }


        /// <summary>
        /// Returns the result of matching a given parser production against a stream of input.
        /// If the parser is using strict PEG matching, there will only be one result.
        /// If not, use <see cref="AllMatches()">AllMatches()</see> to get all the possible parses.
        /// </summary>
        /// <param name="inputStream">The input to the parser.</param>
        /// <param name="productionName">The production (i.e. rule) of the parser to use.</param>
        public MatchResult Match(IEnumerable<TInput> inputStream, string productionName)
        {
            return AllMatches(inputStream, productionName).First();
        }


        /// <summary>
        /// Returns the results of matching a given production against a stream of input.
        /// If the parser is using strict PEG matching, there will only be one result.  If not, there may be a set of results.
        /// </summary>
        /// <param name="inputStream">The input to the parser.</param>
        /// <param name="productionName">The production (i.e. rule) of the parser to use.</param>
        public IEnumerable<MatchResult> AllMatches(IEnumerable<TInput> inputStream, string productionName)
        {
            // clear the predefined combinators
            for (int i = 0; i < cachedCombinators.Count; ++i)
                cachedCombinators[i] = null;

            // find the production
            Production production = null;
            try
            {
                production = FindProduction(productionName);

                if (production == null)
                    throw new ArgumentException();
            }
            catch (ArgumentException ex)
            {
                throw new InvalidCastException(string.Format("'{0}' is not a properly defined production.", productionName), ex);
            }

            // set up the match item stream and memo
            MatchItemStream matchItemStream = new MatchItemStream(inputStream, CONV);
            Memo memo = new Memo(strictPEG);

            // use _CALL to start memoization off the top
            Combinator call = _CALL(production);

            bool matched = false;

            foreach (MatchItem res in call.Match(1, matchItemStream, 0, null, memo))
            {
                matched = true;
                int index;
                string error = memo.GetLastError(out index);

                yield return new MatchResult(true, res.StartIndex, res.NextIndex, res.Results, error, index);
            }

            if (!matched)
            {
                int index;
                string error = memo.GetLastError(out index);
                yield return new MatchResult(false, -1, -1, null, error, index);
            }
        } // AllMatches()


        /// \internal
        /// <summary>
        /// Find a production with a given name.
        /// </summary>
        /// <param name="name">The name of the production.</param>
        protected Production FindProduction(string name)
        {
            return (Production)Delegate.CreateDelegate(typeof(Production), this, name);
        } // FindProduction()


        //////////////////////////////////////////////////////////////

        #region Parser Combinators

        private static int FUNC_ID = 1;

        /// \internal
        [Conditional("ENABLE_TRACING")]
        public static void WriteIndent(int index, int indent, int iter, string format, params object[] args)
        {
            for (int i = 0; i < indent; ++i)
                Console.Write("  ");
            if (index >= 0)
                Console.Write("{0}", index);
            if (iter >= 0)
                Console.Write("/{0}", iter);
            else
                Console.Write("  ");
            Console.Write(": ");

            string s = string.Format(format, args).Replace("\r", "\\r").Replace("\n", "\\n");
            Console.WriteLine(s);
        }


        /// \internal
        /// <summary>
        /// A Production is a type of function that tries to match an item stream.
        /// The iterator that it returns is used to give all solutions to the parse.
        /// A failed parse will return an iterator with NO items in it.
        /// </summary>
        /// <param name="indent">For internal use: tracing indent level.</param>
        /// <param name="_inputs">The item stream to match against.</param>
        /// <param name="_index">The current index in the item stream.</param>
        /// <param name="_args">The stream of parameters passed to the rule.</param>
        /// <param name="_memo">Information about previous matches.</param>
        /// <returns>A series of match items containing the possible results of the production's match action (if there is no action, simply returns the conversion of the last item).</returns>
        protected delegate IEnumerable<MatchItem> Production(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo);


        /// \internal
        /// <summary>
        /// Used in several situations:
        ///  - As input to productions, stores a single item of input.
        ///  - As the result of a production, stores a range of input, and the results of the match.
        ///  - As a parameter to a production, can hold an item of input, a range of input, or a production.
        /// </summary>
        protected class MatchItem
        {
            public MatchItem()
            {
            }

            public MatchItem(IEnumerable<MatchItem> inputStream, int index)
            {
                InputStream = inputStream;
                results = null;
                StartIndex = index;
                NextIndex = index;
            }

            public MatchItem(TInput input, Func<TInput, TResult> conv)
                : this(input, conv(input))
            {
            }

            public MatchItem(TInput input, TResult result)
            {
                Inputs = new List<TInput> { input };
                Results = new List<TResult> { result };
                StartIndex = -1;
                NextIndex = -1;
            }

            public MatchItem(IEnumerable<TInput> inputs, Func<TInput, TResult> conv)
            {
                Inputs = inputs;
                results = inputs.Select(item => conv(item));
                StartIndex = -1;
                NextIndex = -1;
            }

            public MatchItem(MatchItem item)
            {
                CopyFrom(item);
            }

            public MatchItem(Production p)
            {
                inputStream = null;
                inputItems = null;
                results = null;
                StartIndex = -1;
                NextIndex = -1;
                Production = p;
            }

            /// <summary>
            /// The input stream that we are matching against.
            /// </summary>
            public IEnumerable<MatchItem> InputStream 
            {
                set
                {
                    inputStream = value;
                    inputItems = null;
                }
            }

            private IEnumerable<MatchItem> inputStream = null;
            private IEnumerable<TInput> cachedFromStream = null;

            /// <summary>
            /// The inputs that this item matches, if any.
            /// </summary>
            public IEnumerable<TInput> Inputs
            {
                get
                {
                    if (inputItems != null)
                    {
                        return inputItems;
                    }
                    else if (inputStream != null)
                    {
                        if (cachedFromStream == null)
                        {
                            var cached = new List<TInput>();
                            for (int i = StartIndex; i < NextIndex; ++i)
                                cached.AddRange(inputStream.ElementAt(i).Inputs);
                            cachedFromStream = cached;
                        }

                        return cachedFromStream;
                    }
                    else
                    {
                        return Enumerable.Empty<TInput>();
                    }
                }

                set
                {
                    InputStream = null;
                    inputItems = value;
                }
            }

            private IEnumerable<TInput> inputItems = null;

            /// <summary>
            /// The list of outputs corresponding to the inputs.
            /// </summary>
            public IEnumerable<TResult> Results 
            {
                get
                {
                    return results ?? Enumerable.Empty<TResult>();
                }

                set
                {
                    results = value;
                }
            }

            private IEnumerable<TResult> results = null;

            /// <summary>
            /// The index of the first item.
            /// </summary>
            public int StartIndex { get; set; }

            /// <summary>
            /// The index after the lat item.
            /// </summary>
            public int NextIndex { get; set; }

            /// <summary>
            /// Sometimes we're passing a production in arguments.
            /// </summary>
            public Production Production { get; set; }

            public void CopyFrom(MatchItem item)
            {
                inputStream = item.inputStream;
                inputItems = item.inputItems;
                results = item.results;
                StartIndex = item.StartIndex;
                NextIndex = item.NextIndex;
                Production = item.Production;
            }

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
                        string inputs = string.Join(",", Inputs.Select(item => item.ToString()).ToArray());
                        string results = string.Join(",", Results.Select(r => r != null ? r.ToString() : "<null>").ToArray());

                        id = string.Format("{0}-{1} [{2}] -> [{3}]", StartIndex, NextIndex, inputs, results);
                    }
                }

                return id;
            }

            private string id = null;

        } // class MatchItem


        /// \internal
        /// <summary>
        /// A utility class used to construct a stream of match items from a stream of input items.
        /// We match against MatchItems instead of just inputs in order to be able to pass productions as parameters.
        /// </summary>
        private class MatchItemStream : IList<MatchItem>
        {
            private IEnumerable<TInput> inputs;
            private Func<TInput, TResult> conv;

            private List<MatchItem> items = new List<MatchItem>();

            public MatchItemStream(IEnumerable<TInput> inputs, Func<TInput, TResult> conv)
            {
                this.inputs = inputs;
                this.conv = conv;

                if (this.inputs == null)
                    throw new NullReferenceException("You must provide an input enumerable.");

                if (this.conv == null)
                    throw new NullReferenceException("You must provide a converter function.");
            }

            private void FillItems(int index)
            {
                while (items.Count <= index)
                {
                    TInput input = inputs.ElementAt(items.Count);

                    var mi = new MatchItem
                        {
                            Inputs = new List<TInput> { input },
                            Results = new List<TResult> { conv(input) },
                            StartIndex = items.Count,
                            NextIndex = items.Count + 1
                        };

                    items.Add(mi);
                }
            }

            #region IList<MatchItem> Members

            public int IndexOf(MatchItem item)
            {
                throw new NotImplementedException();
            }

            public void Insert(int index, MatchItem item)
            {
                throw new NotImplementedException();
            }

            public void RemoveAt(int index)
            {
                throw new NotImplementedException();
            }

            public MatchItem this[int index]
            {
                get
                {
                    FillItems(index);
                    return items[index];
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            #endregion

            #region ICollection<MatchItem> Members

            public void Add(MatchItem item)
            {
                throw new NotImplementedException();
            }

            public void Clear()
            {
                throw new NotImplementedException();
            }

            public bool Contains(MatchItem item)
            {
                throw new NotImplementedException();
            }

            public void CopyTo(MatchItem[] array, int arrayIndex)
            {
                throw new NotImplementedException();
            }

            public int Count
            {
                get { return inputs.Count(); }
            }

            public bool IsReadOnly
            {
                get { return true; }
            }

            public bool Remove(MatchItem item)
            {
                throw new NotImplementedException();
            }

            #endregion

            #region IEnumerable<MatchItem> Members

            public IEnumerator<MatchItem> GetEnumerator()
            {
                throw new NotImplementedException();
            }

            #endregion

            #region IEnumerable Members

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            #endregion
        } // class MatchItemStream


        /// \internal
        /// <summary>
        /// Stores memoization and error-handling information.
        /// Implements Warth and Millstein's algorithm for handling left-recursion.
        /// </summary>
        protected class Memo
        {

            public class HeadInfo
            {
                public string CallSignature { get; set; }
                public HashSet<string> InvolvedSet { get; set; }
                public HashSet<string> EvalSet { get; set; }

                public HeadInfo(string callSignature)
                {
                    CallSignature = callSignature;
                    InvolvedSet = new HashSet<string>();
                    EvalSet = new HashSet<string>();
                }

                public override string ToString()
                {
                    return string.Format("H//{0}: {{{1}}} / {{{2}}}", CallSignature, string.Join(", ", EvalSet.ToArray()), string.Join(", ", InvolvedSet.ToArray()));
                }
            }

            public class LRInfo
            {
                public string CallSignature { get; set; }
                public HeadInfo Head { get; set; }

                public override string ToString()
                {
                    return string.Format("LR({0}) {1}", CallSignature, Head != null ? Head.ToString() : "<null head>");
                }
            }

            public class MemoResult
            {
                public IEnumerable<MatchItem> Result { get; set; }
                public LRInfo LR { get; set; }

                public override string ToString()
                {
                    var rstr = string.Join(" || ", Result.Select(r => r.ToString()).ToArray());

                    if (LR != null)
                        return string.Format("M // LR ({0}): {1}", LR, rstr);
                    else
                        return string.Format("M // {0}", rstr);
                }
            }

            /// <summary>
            /// Stores a dictionary of positions and productions to memo results.
            /// </summary>
            private Dictionary<int, Dictionary<string, MemoResult>> data = new Dictionary<int, Dictionary<string, MemoResult>>();
            
            public MemoResult this[int index, string call_signature]
            {
                get
                {
                    if (data.ContainsKey(index))
                    {
                        var subData = data[index];

                        if (subData.ContainsKey(call_signature))
                            return data[index][call_signature];
                    }

                    return null;
                }

                set
                {
                    if (value == null)
                        throw new ArgumentNullException("Cannot memoize a null value!");

                    Dictionary<string, MemoResult> subData;

                    if (data.ContainsKey(index))
                    {
                        subData = data[index];
                    }
                    else
                    {
                        subData = new Dictionary<string, MemoResult>();
                        data.Add(index, subData);
                    }

                    if (subData.ContainsKey(call_signature))
                    {
                        subData[call_signature] = value;
                    }
                    else
                    {
                        subData.Add(call_signature, value);
                    }
                }
            }

            /// <summary>
            /// Stores information about the LR seeds being grown at a particular position.
            /// </summary>
            public Dictionary<int, HeadInfo> Heads { get { return heads; } }
            private Dictionary<int, HeadInfo> heads = new Dictionary<int, HeadInfo>();

            /// <summary>
            /// Stores the rule stack for growing LR seeds.
            /// </summary>
            public Stack<LRInfo> LRStack { get { return lrStack; } }
            private Stack<LRInfo> lrStack = new Stack<LRInfo>();

            /// <summary>
            /// Stores information about errors encountered during a parse.
            /// </summary>
            public SortedDictionary<int, List<string>> Errors { get { return errors; } }
            private SortedDictionary<int, List<string>> errors = new SortedDictionary<int, List<string>>();

            /// <summary>
            /// If this is true, then the parser will implement strict PEG matching.
            /// If false, then it will backtrack more, e.g. Kleene star will backtrack.
            /// </summary>
            public bool StrictPEG { get; set; }

            /// <summary>
            /// Constructor.
            /// </summary>
            public Memo(bool strictPEG)
            {
                this.StrictPEG = strictPEG;
            }

            /// <summary>
            /// Adds an error to the global error table.
            /// </summary>
            [Conditional("ENABLE_ERROR_HANDLING")]
            public void AddError(int index, string error)
            {
                if (!errors.ContainsKey(index))
                    errors.Add(index, new List<string>());

                errors[index].Add(error);
            }

            /// <summary>
            /// Gets the right-most error.
            /// </summary>
            public string GetLastError(out int index)
            {
                if (errors.Count > 0)
                {
                    int lastKey = errors.Keys.Last();
                    List<string> msgs = errors[lastKey];
                    if (msgs != null && msgs.Count > 0)
                    {
                        index = lastKey;
                        return msgs.Last();
                    }
                }

                index = -1;
                return null;
            }

            public string GetLastError()
            {
                int index;
                return GetLastError(out index);
            }

        } // class Memo

        
        //////////////////////////////////////////

        /// \internal
        /// <summary>
        /// A combinator contains a production delegate that is used to match against an item stream.
        /// The only reason we use this method and can't use lambda functions directly is that lambda functions cannot be iterators.
        /// </summary>
        protected abstract class Combinator
        {
            public abstract IEnumerable<MatchItem> Match(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo);
        } // class Combinator


        private List<Combinator> cachedCombinators = new List<Combinator>();

        protected List<Combinator> CachedCombinators { get { return cachedCombinators; } }


        //////////////////////////////////////////

        /// \internal
        protected static Combinator _EMPTY() { return new EmptyCombinator(); }

        /// \internal
        private class EmptyCombinator : Combinator
        {
            public override IEnumerable<MatchItem> Match(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
            {
                int func_id = FUNC_ID++;

                MatchItem res = new MatchItem(_inputs, _index);
                WriteIndent(_index, indent, func_id, "_EMPTY(): {0}", res);
                yield return res;
            }
        }


        //////////////////////////////////////////

        /// \internal
        protected static Combinator _FAIL() { return new FailCombinator(null); }

        /// \internal
        protected static Combinator _FAIL(string message) { return new FailCombinator(message); }

        /// \internal
        private class FailCombinator : Combinator
        {
            string message;

            public FailCombinator(string message)
                : base()
            {
                this.message = message;
            }

            public override IEnumerable<MatchItem> Match(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
            {
                int func_id = FUNC_ID++;

                if (_memo != null && !string.IsNullOrEmpty(message))
                    _memo.AddError(_index, message);

                WriteIndent(_index, indent, func_id, "_FAIL()");
                yield break;
            }
        }


        //////////////////////////////////////////

        /// \internal
        protected static Combinator _ANY() { return new AnyCombinator(); }

        /// \internal
        private class AnyCombinator : Combinator
        {
            public override IEnumerable<MatchItem> Match(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
            {
                int func_id = FUNC_ID++;

                MatchItem res = null;

                try 
                {
                    if (_inputs != null)
                        res = _inputs.ElementAt(_index);
                }
                catch (ArgumentOutOfRangeException) 
                {
                    res = null;
                }

                if (res != null)
                {
                    var newRes = new MatchItem
                        {
                            InputStream = _inputs,
                            Results = res.Results,
                            StartIndex = _index,
                            NextIndex = _index + 1,
                            Production = res.Production
                        };

                    WriteIndent(_index, indent, func_id, "_ANY(): {0}", newRes);

                    yield return newRes;
                }
                else
                {
                    if (_memo != null)
                        _memo.AddError(_index, "Expected input");

                    WriteIndent(_index, indent, func_id, "_ANY(): FAIL");
                    yield break;
                }
            }
        }


        //////////////////////////////////////////

        /// \internal
        protected static Combinator _LITERAL(TInput item) { return new LiteralCombinator(item); }

        
        /// \internal
        protected static Combinator _LITERAL(IEnumerable<TInput> items) { return new LiteralCombinator(items); }

        /// \internal
        private class LiteralCombinator : Combinator
        {
            IEnumerable<TInput> items;

            public LiteralCombinator(TInput item)
            {
                this.items = new List<TInput> { item };
            }

            public LiteralCombinator(IEnumerable<TInput> items)
            {
                this.items = items;
            }

            public override IEnumerable<MatchItem> Match(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
            {
                int func_id = FUNC_ID++;

                MatchItem res = null;

                try
                {
                    if (_inputs != null)
                    {
                        bool matches = true;
                        IEnumerable<TResult> results = Enumerable.Empty<TResult>();

                        int inputIndex = _index;

                        IEnumerator<TInput> curItem = items.GetEnumerator();
                        while (matches && curItem.MoveNext())
                        {
                            MatchItem inputItem = _inputs.ElementAt(inputIndex++);

                            IEnumerator<TInput> curInput = inputItem.Inputs.GetEnumerator();

                            matches = curInput.MoveNext();
                            while (matches)
                            {
                                matches = curItem.Current.Equals(curInput.Current);

                                if (curInput.MoveNext())
                                {
                                    if (!(matches = curItem.MoveNext()))
                                        break;
                                }
                                else
                                {
                                    break;
                                }
                            }

                            if (matches)
                                results = results.Concat(inputItem.Results);
                        }

                        if (matches)
                        {
                            res = new MatchItem
                                {
                                    InputStream = _inputs,
                                    Results = results,
                                    StartIndex = _index,
                                    NextIndex = inputIndex
                                };
                        }
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    res = null;
                }

                if (res != null)
                {
                    WriteIndent(_index, indent, func_id, "_LITERAL({0}): {1}", string.Join("", items.Select(i => i.ToString()).ToArray()), res);
                    yield return res;
                }
                else
                {
                    if (_memo != null)
                        _memo.AddError(_index, string.Format("Expected {0}", string.Join("", items.Select(i => i.ToString()).ToArray())));

                    WriteIndent(_index, indent, func_id, "_LITERAL({0}): FAIL", string.Join("", items.Select(i => i.ToString()).ToArray()));
                    yield break;
                }
            }
        }


        //////////////////////////////////////////

        /// \internal
        protected static Combinator _AND(params Combinator[] combinators) { return new AndCombinator(combinators); }

        /// \internal
        private class AndCombinator : Combinator
        {
            Combinator[] combinators;

            public AndCombinator(params Combinator[] combinators)
            {
                this.combinators = combinators;
            }

            private class AndRecord
            {
                public IEnumerator<MatchItem> Enumerator;
                public MatchItem LastResult;

                public AndRecord(IEnumerator<MatchItem> enumerator)
                {
                    this.Enumerator = enumerator;
                }
            }

            public override IEnumerable<MatchItem> Match(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
            {
                int func_id = FUNC_ID++;

                WriteIndent(_index, indent, func_id, "_AND()");

                var resultStack = new Stack<AndRecord>();

                int curIndex = _index;
                bool getNewMatch = true;

                int curCombinator = 0;
                IEnumerator<MatchItem> curEnumerator = null;

                if (curCombinator < combinators.Length)
                {
                    bool done = false;

                    while (true)
                    {
                        // get the next result from our current combinator
                        if (getNewMatch)
                        {
                            if (curCombinator < combinators.Length)
                            {
                                IEnumerable<MatchItem> curResults = combinators[curCombinator++].Match(indent + 1, _inputs, curIndex, null, _memo);
                                curEnumerator = curResults.GetEnumerator();
                                resultStack.Push(new AndRecord(curEnumerator));
                            }
                            else
                            {
                                done = true;
                            }
                        }
                        else
                        {
                            if (resultStack.Count > 0)
                                curEnumerator = resultStack.Peek().Enumerator;
                            else
                                break;
                        }

                        // do we have a successful match?
                        if (done)
                        {
                            var resultList = Enumerable.Empty<TResult>();
                            foreach (var prev in resultStack)
                            {
                                resultList = prev.LastResult.Results.Concat(resultList);
                            }

                            MatchItem res = new MatchItem
                            {
                                InputStream = _inputs,
                                Results = resultList,
                                StartIndex = _index,
                                NextIndex = curIndex
                            };

                            yield return res;

                            if (_memo.StrictPEG)
                                yield break;
                        }

                        // do we have a match for our current combinator?
                        if (curEnumerator.MoveNext())
                        {
                            getNewMatch = true;

                            resultStack.Peek().LastResult = curEnumerator.Current;
                            curIndex = curEnumerator.Current.NextIndex;
                        }
                        else
                        {
                            // backtrack
                            done = false;
                            resultStack.Pop();
                            getNewMatch = false;
                            curCombinator--;

                            if (resultStack.Count > 0)
                                curIndex = resultStack.Peek().LastResult.StartIndex;
                            else
                                break;
                        }
                    } // while true
                }
            } // Match()

            //public override IEnumerable<MatchItem> MatchOld(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
            //{
            //    int func_id = FUNC_ID++;

            //    WriteIndent(_index, indent, func_id, "_AND()");

            //    foreach (MatchItem res_a in a.Match(indent + 1, _inputs, _index, null, _memo))
            //    {
            //        foreach (MatchItem res_b in b.Match(indent + 1, _inputs, res_a.NextIndex, null, _memo))
            //        {
            //            var newRes = new MatchItem
            //            {
            //                InputStream = _inputs,
            //                Results = res_a.Results.Concat(res_b.Results),
            //                StartIndex = _index, NextIndex = res_b.NextIndex
            //            };

            //            WriteIndent(_index, indent, func_id, " AND(): {0}", newRes);
            //            yield return newRes;

            //            if (_memo.StrictPEG)
            //                yield break;
            //        }

            //        if (_memo.StrictPEG)
            //            yield break;
            //    }
            //}
        }


        //////////////////////////////////////////

        /// \internal
        protected static Combinator _OR(params Combinator[] combinators) { return new OrCombinator(combinators); }

        /// \internal
        private class OrCombinator : Combinator
        {
            Combinator[] combinators;

            public OrCombinator(params Combinator[] combinators)
            {
                this.combinators = combinators;
            }

            public override IEnumerable<MatchItem> Match(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
            {
                int func_id = FUNC_ID++;

                WriteIndent(_index, indent, func_id, "_OR()");

                // ordered choice; must try A totally first instead of interleaving...
                foreach (Combinator c in combinators)
                {
                    foreach (MatchItem res in c.Match(indent + 1, _inputs, _index, null, _memo))
                    {
                        WriteIndent(_index, indent, func_id, " OR(): {0}", res);
                        yield return res;

                        if (_memo.StrictPEG)
                            yield break;
                    }
                }
            }
        }


        //////////////////////////////////////////

        /// \internal
        protected static Combinator _STAR(Combinator a) { return new StarCombinator(a); }

        /// \internal
        private class StarCombinator : Combinator
        {
            Combinator a;

            public StarCombinator(Combinator a)
            {
                this.a = a;
            }

            private class StarRecord
            {
                public IEnumerator<MatchItem> Enumerator;
                public MatchItem LastResult;

                public StarRecord(IEnumerator<MatchItem> enumerator)
                {
                    this.Enumerator = enumerator;
                }
            }

            public override IEnumerable<MatchItem> Match(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
            {
                int func_id = FUNC_ID++;

                WriteIndent(_index, indent, func_id, "_STAR()");

                var resultStack = new Stack<StarRecord>();

                int curIndex = _index;
                bool getNewMatch = true;

                IEnumerator<MatchItem> curEnumerator = null;

                while (true)
                {
                    if (getNewMatch)
                    {
                        IEnumerable<MatchItem> curResults = a.Match(indent + 1, _inputs, curIndex, null, _memo);
                        curEnumerator = curResults.GetEnumerator();
                        resultStack.Push(new StarRecord(curEnumerator));
                    }
                    else
                    {
                        if (resultStack.Count > 0)
                            curEnumerator = resultStack.Peek().Enumerator;
                        else
                            break;
                    }

                    // do we have a match?
                    if (curEnumerator.MoveNext())
                    {
                        getNewMatch = true;

                        resultStack.Peek().LastResult = curEnumerator.Current;
                        curIndex = curEnumerator.Current.NextIndex;
                    }
                    else
                    {
                        // our work here is done
                        resultStack.Pop();
                        getNewMatch = false;

                        // assemble result from the rest of the stack
                        var resultList = Enumerable.Empty<TResult>();

                        foreach (var prev in resultStack)
                        {
                            resultList = prev.LastResult.Results.Concat(resultList);
                        }

                        MatchItem res = new MatchItem
                            {
                                InputStream = _inputs,
                                Results = resultList,
                                StartIndex = _index,
                                NextIndex = curIndex
                            };

                        yield return res;

                        // pop or break
                        if (_memo.StrictPEG)
                            yield break;

                        if (resultStack.Count > 0)
                            curIndex = resultStack.Peek().LastResult.StartIndex;
                        else
                            break;
                    }
                } // while true
            }

        } // class StarCombinator()


        //////////////////////////////////////////

        /// \internal
        protected static Combinator _PLUS(Combinator a) { return _AND(a, _STAR(a)); }

        /// \internal
        protected static Combinator _QUES(Combinator a) { return _OR(a, _EMPTY()); }


        //////////////////////////////////////////

        /// \internal
        protected static Combinator _LOOK(Combinator a) { return new LookCombinator(a); }

        /// \internal
        private class LookCombinator : Combinator
        {
            Combinator a;

            public LookCombinator(Combinator a)
            {
                this.a = a;
            }

            public override IEnumerable<MatchItem> Match(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
            {
                int func_id = FUNC_ID++;

                WriteIndent(_index, indent, func_id, "_LOOK()");

                foreach (MatchItem res in a.Match(indent + 1, _inputs, _index, null, _memo))
                {
                    var newRes = new MatchItem(_inputs, _index);
                    WriteIndent(_index, indent, func_id, " LOOK(): {0}", newRes);
                    yield return newRes;

                    if (_memo.StrictPEG)
                        yield break;
                }

                WriteIndent(_index, indent, func_id, " LOOK(): FAIL");
            }
        }


        //////////////////////////////////////////

        /// \internal
        protected static Combinator _NOT(Combinator a) { return new NotCombinator(a); }

        /// \internal
        private class NotCombinator : Combinator
        {
            Combinator a;

            public NotCombinator(Combinator a)
            {
                this.a = a;
            }

            public override IEnumerable<MatchItem> Match(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
            {
                int func_id = FUNC_ID++;

                WriteIndent(_index, indent, func_id, "_NOT()");

                bool matched = false;
                foreach (MatchItem res in a.Match(indent+1, _inputs, _index, null, _memo))
                {
                    matched = true;

                    if (_memo != null)
                        _memo.AddError(_index, string.Format("Unexpected ", string.Join("", res.Results.Select(item => item != null ? item.ToString() : "").ToArray())));

                    WriteIndent(_index, indent, func_id, " NOT(): matched; FAIL");
                    yield break;
                }

                if (!matched)
                {
                    var newRes = new MatchItem(_inputs, _index);
                    WriteIndent(_index, indent, func_id, " NOT(): {0}", newRes);
                    yield return newRes;
                }
            }
        }


        //////////////////////////////////////////

        /// \internal
        protected static Combinator _VAR(Combinator a, MatchItem v) { return new VarCombinator(a, v); }

        /// \internal
        private class VarCombinator : Combinator
        {
            Combinator a;
            MatchItem v;

            public VarCombinator(Combinator a, MatchItem v)
            {
                this.a = a;
                this.v = v;
            }

            public override IEnumerable<MatchItem> Match(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
            {
                int func_id = FUNC_ID++;

                WriteIndent(_index, indent, func_id, "_VAR()");

                foreach (MatchItem res in a.Match(indent + 1, _inputs, _index, null, _memo))
                {
                    v.CopyFrom(res);

                    WriteIndent(_index, indent, func_id, " VAR(): {0}", res);

                    yield return res;

                    if (_memo.StrictPEG)
                        yield break;
                }
            }
        }


        //////////////////////////////////////////

        /// \internal
        protected static Combinator _CONDITION(Combinator a, Func<MatchItem, bool> condition) { return new ConditionCombinator(a, condition); }

        /// \internal
        private class ConditionCombinator : Combinator
        {
            Combinator a;
            Func<MatchItem, bool> condition;

            public ConditionCombinator(Combinator a, Func<MatchItem, bool> condition)
            {
                this.a = a;
                this.condition = condition;
            }

            public override IEnumerable<MatchItem> Match(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
            {
                int func_id = FUNC_ID++;

                WriteIndent(_index, indent, func_id, "_CONDITION()");

                foreach (MatchItem res in a.Match(indent + 1, _inputs, _index, null, _memo))
                {
                    if (condition(res))
                    {
                        WriteIndent(_index, indent, func_id, " CONDITION(): {0}", res);
                        yield return res;
                    }

                    if (_memo.StrictPEG)
                        yield break;
                }
            }
        }

        
        //////////////////////////////////////////

        /// \internal
        protected static Combinator _ACTION(Combinator a, Func<MatchItem, TResult> action) { return new ActionCombinatorSingle(a, action); }

        /// \internal
        private class ActionCombinatorSingle : Combinator
        {
            Combinator a;
            Func<MatchItem, TResult> action;

            public ActionCombinatorSingle(Combinator a, Func<MatchItem, TResult> action)
            {
                this.a = a;
                this.action = action;
            }

            public override IEnumerable<MatchItem> Match(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
            {
                int func_id = FUNC_ID++;

                WriteIndent(_index, indent, func_id, "_ACTION()");

                foreach (MatchItem res in a.Match(indent + 1, _inputs, _index, null, _memo))
                {
                    var newRes = new MatchItem
                        {
                            InputStream = _inputs,
                            Results = new List<TResult> { action(res) },
                            StartIndex = res.StartIndex,
                            NextIndex = res.NextIndex,
                            Production = res.Production
                        };

                    WriteIndent(_index, indent, func_id, " ACTION(): {0}", newRes);
                    yield return newRes;

                    if (_memo.StrictPEG)
                        yield break;
                }
            }
        }

        /// \internal
        protected static Combinator _ACTION(Combinator a, Func<MatchItem, IEnumerable<TResult>> action) { return new ActionCombinatorList(a, action); }

        /// \internal
        private class ActionCombinatorList : Combinator
        {
            Combinator a;
            Func<MatchItem, IEnumerable<TResult>> action;

            public ActionCombinatorList(Combinator a, Func<MatchItem, IEnumerable<TResult>> action)
            {
                this.a = a;
                this.action = action;
            }

            public override IEnumerable<MatchItem> Match(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
            {
                int func_id = FUNC_ID++;

                WriteIndent(_index, indent, func_id, "_ACTION(list)");

                foreach (MatchItem res in a.Match(indent + 1, _inputs, _index, null, _memo))
                {
                    var newRes = new MatchItem
                        {
                            InputStream = _inputs,
                            Results = action(res),
                            StartIndex = res.StartIndex,
                            NextIndex = res.NextIndex,
                            Production = res.Production
                        };

                    WriteIndent(_index, indent, func_id, " ACTION(list): {0}", newRes);
                    yield return newRes;

                    if (_memo.StrictPEG)
                        yield break;
                }
            }
        }


        //////////////////////////////////////////

        /// \internal
        protected static Combinator _REF(MatchItem v, string name, Matcher<TInput, TResult> matcher) { return new RefCombinator(v, name, matcher); }

        /// \internal
        protected static Combinator _REF(MatchItem v, string name) { return new RefCombinator(v, name, null); }

        /// \internal
        protected static Combinator _REF(MatchItem v) { return new RefCombinator(v, "", null); }

        /// \internal
        private class RefCombinator : Combinator
        {
            MatchItem v;
            string name;
            Matcher<TInput, TResult> matcher;
            Combinator call;
            bool needLookup;

            public RefCombinator(MatchItem v, string name, Matcher<TInput, TResult> matcher)
            {
                this.v = v;
                this.name = name;
                this.matcher = matcher;
                this.call = null;
                needLookup = true;
            }

            public override IEnumerable<MatchItem> Match(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
            {
                int func_id = FUNC_ID++;

                // if we have a production, 
                if (v.Production != null && call == null)
                {
                    call = _CALL(v.Production);
                }
                else if (needLookup)
                {
                    // try to look up our name as a production
                    if (!string.IsNullOrEmpty(name) && matcher != null)
                    {
                        try
                        {
                            Production p = Delegate.CreateDelegate(typeof(Production), matcher, name) as Production;

                            if (p != null)
                                call = _CALL(p);
                        }
                        catch (AmbiguousMatchException) { }
                        catch (ArgumentException) { }
                    }

                    needLookup = false;
                }

                // either call the production or match the input in the variable
                if (call != null)
                {
                    foreach (var res in call.Match(indent, _inputs, _index, _args, _memo))
                    {
                        yield return res;

                        if (_memo.StrictPEG)
                            yield break;
                    }
                }
                else
                {
                    bool matched = true;
                    int nextIndex = _index;

                    try
                    {
                        if (_inputs != null && v.Inputs.Any())
                        {
                            foreach (TInput var_item in v.Inputs)
                            {
                                MatchItem input_item = _inputs.ElementAt(nextIndex++);

                                if (!input_item.Inputs.Last().Equals(var_item))
                                {
                                    matched = false;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            matched = false;
                        }

                        if (!matched)
                            _memo.AddError(_index, "Expected " + string.Join("", v.Inputs.Select(i => i.ToString()).ToArray()));
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        matched = false;
                    }

                    if (matched)
                    {
                        var res = new MatchItem(_inputs, _index);
                        res.Results = v.Results;
                        res.NextIndex = nextIndex;

                        WriteIndent(_index, indent, func_id, "_REF({1}): {0}", res, name);
                        yield return res;
                    }
                    else
                    {
                        WriteIndent(_index, indent, func_id, "_REF({0}): FAIL", name);
                        yield break;
                    }
                }
            } // Match()

        } // class RefCombinator


        //////////////////////////////////////////

        /// \internal
        protected static Combinator _ARGS(Combinator arg_pattern, IEnumerable<MatchItem> actual_args, Combinator body_pattern)
        {
            return new ArgsCombinator(arg_pattern, actual_args, body_pattern);
        }

        /// \internal
        private class ArgsCombinator : Combinator
        {
            Combinator arg_pattern;
            IEnumerable<MatchItem> actual_args;
            Combinator body_pattern;
            Memo arg_memo;

            public ArgsCombinator(Combinator arg_pattern, IEnumerable<MatchItem> actual_args, Combinator body_pattern)
            {
                this.arg_pattern = arg_pattern;
                this.actual_args = actual_args;
                this.body_pattern = body_pattern;
                this.arg_memo = new Memo(true);
            }

            public override IEnumerable<MatchItem> Match(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
            {
                int func_id = FUNC_ID++;

                WriteIndent(_index, indent, func_id, "_ARGS(args)");

                if (arg_memo != null)
                    arg_memo.StrictPEG = _memo.StrictPEG;

                foreach (MatchItem arg_result in arg_pattern.Match(indent+1, actual_args, 0, null, arg_memo))
                {
                    WriteIndent(_index, indent, func_id, " ARGS(pattern): {0}", arg_result);

                    foreach (MatchItem body_result in body_pattern.Match(indent+1, _inputs, _index, null, _memo))
                    {
                        WriteIndent(_index, indent, func_id, " ARGS(): {0}", body_result);
                        yield return body_result;

                        if (_memo.StrictPEG)
                            yield break;
                    }

                    if (_memo.StrictPEG)
                        yield break;
                }
            }
        }
        
        
        //////////////////////////////////////////

        /// \internal
        protected static Combinator _CALL(Production p, IEnumerable<MatchItem> actual_args) 
        {
            return new CallItemCombinator(new MatchItem { Production = p }, actual_args); 
        }

        /// \internal
        protected static Combinator _CALL(Production p) 
        {
            return new CallItemCombinator(new MatchItem { Production = p }, null); 
        }

        /// \internal
        protected static Combinator _CALL(MatchItem v, IEnumerable<MatchItem> actual_args) 
        { 
            return new CallItemCombinator(v, actual_args); 
        }


        /// \internal
        /// <summary>Implements Warth et al's algorithm for handling left-recursion.</summary>
        private class CallItemCombinator : Combinator
        {
            MatchItem v;
            IEnumerable<MatchItem> actual_args;
            string call_signature;

            // make sure not to call this until the variable is bound
            private string CallSignature
            {
                get
                {
                    if (call_signature == null)
                        call_signature = string.Format("{0}({1})", v.Production.Method.Name, actual_args != null ? string.Join(", ", actual_args.Select(a => a.ToString()).ToArray()) : "");
                    return call_signature;
                }
            }

            public CallItemCombinator(MatchItem v, IEnumerable<MatchItem> actual_args)
                : base()
            {
                this.v = v;
                this.actual_args = actual_args;
            }

            public override IEnumerable<MatchItem> Match(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
            {
                int func_id = FUNC_ID++;

                // recall
                Memo.MemoResult mr = _memo[_index, CallSignature];
                Memo.HeadInfo h = _memo.Heads.ContainsKey(_index) ? _memo.Heads[_index] : null;

                // are we inside a grow?
                if (h != null)
                {
                    // ignore uninvolved rules
                    if (mr == null && !(CallSignature.Equals(h.CallSignature) || h.InvolvedSet.Contains(CallSignature)))
                    {
                        mr = new Memo.MemoResult { Result = Enumerable.Empty<MatchItem>() };
                    }
                    // is this an involved rule?
                    else if (h.EvalSet.Contains(CallSignature))
                    {
                        h.EvalSet.Remove(CallSignature);
                        mr.LR = null;

                        WriteIndent(_index, indent, func_id, "_CALL({0}): eval involved rule", CallSignature);
                        mr.Result = v.Production(indent, _inputs, _index, actual_args, _memo);
                    }
                }

                // have we seen this rule before?
                if (mr == null)
                {
                    var lr = new Memo.LRInfo { CallSignature = CallSignature, Head = null };

                    // if not, add a failing result, and mark the rule as available for growth
                    List<MatchItem> finalResult = new List<MatchItem>();

                    mr = new Memo.MemoResult { LR = lr, Result = finalResult };
                    _memo[_index, CallSignature] = mr;

                    WriteIndent(_index, indent, func_id, "_CALL({0}): NO MEMO: creating MR {1}", CallSignature, mr);

                    // get a result from the body of the rule
                    _memo.LRStack.Push(lr);

                    foreach (MatchItem res in v.Production(indent, _inputs, _index, actual_args, _memo))
                    {
                        // while we are growing, don't try to re-grow
                        mr.LR = null;
                        _memo.LRStack.Pop();

                        // add to seed
                        if (_memo.StrictPEG)
                            finalResult.Clear();
                        finalResult.Insert(0, res);

                        if (lr.Head != null)
                        {
                            if (!lr.Head.CallSignature.Equals(CallSignature))
                            {
                                WriteIndent(_index, indent, func_id, " CALL({0}): not growing involved rule: {1}", CallSignature, res);
                                yield return res;

                                if (_memo.StrictPEG)
                                    yield break;
                            }
                            else
                            {
                                WriteIndent(_index, indent, func_id, " CALL({0}): growing: {1}", CallSignature, res);

                                if (_memo.Heads.ContainsKey(_index))
                                    _memo.Heads[_index] = lr.Head;
                                else
                                    _memo.Heads.Add(_index, lr.Head);

                                bool grown = false;
                                lr.Head.EvalSet = new HashSet<string>(lr.Head.InvolvedSet);
                                foreach (MatchItem growRes in GrowLR(indent + 1, _inputs, _index, _memo, lr.Head, finalResult, res))
                                {
                                    grown = true;
                                    _memo.Heads[_index] = null;

                                    // assign result
                                    yield return growRes;

                                    if (_memo.StrictPEG)
                                        yield break;

                                    _memo.Heads[_index] = lr.Head;
                                    lr.Head.EvalSet = new HashSet<string>(lr.Head.InvolvedSet);
                                }

                                if (!grown)
                                    yield return res;

                                _memo.Heads[_index] = null;
                            }
                        }
                        else
                        {
                            WriteIndent(_index, indent, func_id, " CALL({0}): no lr; not growing; memoized: {1}", CallSignature, res);
                            yield return res;

                            if (_memo.StrictPEG)
                                yield break;
                        }

                        // try to grow again if there's more in the body
                        mr.LR = lr;
                        _memo.LRStack.Push(lr);
                    }

                    WriteIndent(_index, indent, func_id, " CALL({0}): final memoized: {{{1}}}", CallSignature, string.Join(" || ", finalResult.Select(r => r.ToString()).ToArray()));

                    mr.LR = null;
                    _memo.LRStack.Pop();
                }
                else
                {
                    if (mr.LR != null)
                    {
                        WriteIndent(_index, indent, func_id, "_CALL({0}): detected LR in {1}", CallSignature, mr);

                        // detected left-recursion; generate set of involved calls
                        if (mr.LR.Head == null)
                            mr.LR.Head = new Memo.HeadInfo(CallSignature);
                        foreach (var s in _memo.LRStack)
                        {
                            if (s.Head != mr.LR.Head)
                            {
                                s.Head = mr.LR.Head;
                                mr.LR.Head.InvolvedSet.Add(s.CallSignature);
                            }
                            else
                            {
                                break;
                            }
                        }
                        WriteIndent(_index, indent, func_id, " CALL({0}): {1}: need to grow; fail!", CallSignature, mr.LR.Head);

                        yield break;
                    }
                    else
                    {
                        // no left-recursion; return memo result
                        foreach (MatchItem res in mr.Result)
                        {
                            WriteIndent(_index, indent, func_id, " CALL({0}): recorded: {1}", CallSignature, res);

                            yield return res;

                            if (_memo.StrictPEG)
                                yield break;
                        }
                    }
                }
            } // Match()

            
            private IEnumerable<MatchItem> GrowLR(int indent, IEnumerable<MatchItem> _inputs, int _index, Memo _memo, Memo.HeadInfo _head, List<MatchItem> finalResult, MatchItem prevResult)
            {
                int func_id = FUNC_ID++;

                WriteIndent(_index, indent, func_id, ":GrowLR({1}): prev: {0}", prevResult, CallSignature);

                foreach (MatchItem res in v.Production(indent, _inputs, _index, actual_args, _memo))
                {
                    if (prevResult == null || res.NextIndex > prevResult.NextIndex)
                    {
                        WriteIndent(_index, indent, func_id, " GrowLR({1}): memoizing: {0}", res, CallSignature);

                        if (_memo.StrictPEG)
                            finalResult.Clear();
                        finalResult.Insert(0, res);

                        bool grown = false;
                        foreach (MatchItem subRes in GrowLR(indent, _inputs, _index, _memo, _head, finalResult, res))
                        {
                            grown = true;
                            WriteIndent(_index, indent, func_id, " GrowLR({1}): grew to: {0}", subRes, CallSignature);
                            
                            if (_memo.StrictPEG)
                                finalResult.Clear();
                            finalResult.Insert(0, subRes);

                            yield return subRes;

                            if (_memo.StrictPEG)
                                yield break;
                        }

                        if (!grown)
                            yield return res;
                    }
                    else
                    {
                        WriteIndent(_index, indent, func_id, " GrowLR({1}): giving up: {0}", string.Join(" || ", finalResult.Select(r => r.ToString()).ToArray()), CallSignature);
                        yield return prevResult;

                        if (_memo.StrictPEG)
                            yield break;
                    }

                    if (_memo.StrictPEG)
                        yield break;

                    _head.EvalSet = new HashSet<string>(_head.InvolvedSet);
                }
            } // GrowLR()

        } // CallItemCombinator()


        /// \internal
        protected IEnumerable<MatchItem> STR(IEnumerable<TInput> inputs)
        {
            return new MatchItemStream(inputs, CONV);
        }

        
        //////////////////////////////////////////

        #endregion

    } // class Matcher

} // namespace IronMeta
