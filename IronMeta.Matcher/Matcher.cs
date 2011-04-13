//////////////////////////////////////////////////////////////////////
// $Id$
//
// Copyright (C) 2009-2010, The IronMeta Project
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

/// Main IronMeta library functionality.
namespace IronMeta.Matcher
{

    /// <summary>
    /// Base class for IronMeta grammars.
    /// </summary>
    /// <typeparam name="TInput">The type of inputs to the grammar.</typeparam>
    /// <typeparam name="TResult">The type of results of grammar rules.</typeparam>
    /// <typeparam name="TItem">The (internal) type used to track inputs and results.</typeparam>
    public abstract class Matcher<TInput, TResult, TItem>
        where TItem:MatchItem<TInput, TResult, TItem>, new()
    {

        #region Members

        protected readonly Func<TResult, bool> _NON_NULL = r => r != null;

        private Memo<TInput, TResult, TItem> _memo;

        #endregion

        #region Properties

        /// <summary>
        /// Holds the state of a parse.
        /// </summary>
        public Memo<TInput, TResult, TItem> Memo
        {
            get { return _memo ?? (_memo = new Memo<TInput, TResult, TItem>()); }
        }

        /// <summary>
        /// The input stream for the grammar to parse.
        /// </summary>
        public IEnumerable<TInput> Input
        {
            get { return InputEnumerable; }
            
            protected set
            {
                InputEnumerable = value;
                InputList = InputEnumerable as IList<TInput>;
                InputString = InputEnumerable as string;
            }
        }

        protected IEnumerable<TInput> InputEnumerable
        {
            get { return Memo.InputEnumerable; }
            private set { Memo.InputEnumerable = value; }
        }

        protected IList<TInput> InputList
        {
            get { return Memo.InputList; }
            private set { Memo.InputList = value; }
        }

        protected string InputString
        {
            get { return Memo.InputString; }
            private set { Memo.InputString = value; } 
        }

        protected Stack<TItem> Results { get { return Memo.Results; } }
        protected Stack<TItem> ArgResults { get { return Memo.ArgResults; } }

        public ErrorRec LastError { get { return Memo.LastError; } }
        Stack<LRRecord<TItem>> CallStack { get { return Memo.CallStack; } }

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        public Matcher()
        {
        }

        /// <summary>
        /// Try to match the input.
        /// </summary>
        /// <param name="production">The top-level grammar production (rule) to use.</param>
        /// <returns>The result of the match.</returns>
        public MatchResult<TInput, TResult, TItem> GetMatch(IEnumerable<TInput> input, Action<int, IEnumerable<TItem>> production)
        {
            _memo = new Memo<TInput, TResult, TItem>();
            Input = input;
            var result = _MemoCall(production.Method.Name, 0, production, null);

            if (result != null)
                return new MatchResult<TInput, TResult, TItem>(this, true, result.StartIndex, result.NextIndex, result.Results, string.Empty, -1);
            else
                return new MatchResult<TInput, TResult, TItem>(this, false, -1, -1, null, string.Empty, -1);
        }

        #region Internal Parser Functions

        /// <summary>
        /// Sets the current error, if it is beyond or equal to the previous error.
        /// </summary>
        /// <param name="pos">Position of the error.</param>
        /// <param name="message">Function to generate the message (deferred until the end for better performance).</param>
        protected void _AddError(int pos, Func<string> message)
        {
            if (pos >= LastError.Pos)
            {
                LastError.Pos = pos;
                LastError.Func = message;
            }
        }

        /// <summary>
        /// Calls an action that returns a list of results.
        /// </summary>
        protected IEnumerable<TResult> _Thunk<ItemType>(Func<ItemType, IEnumerable<TResult>> func, ItemType item)
        {
            return func(item);
        }

        /// <summary>
        /// Calls an action that returns a single result.
        /// </summary>
        protected IEnumerable<TResult> _Thunk<ItemType>(Func<ItemType, TResult> func, ItemType item)
        {
            return new List<TResult> { func(item) };
        }

        /// <summary>
        /// Call a grammar production, using memoization and handling left-recursion.
        /// </summary>
        /// <param name="ruleName">The name of the production.</param>
        /// <param name="index">The current index in the input stream.</param>
        /// <param name="production">The production itself.</param>
        /// <param name="args">Arguments to the production (can be null).</param>
        /// <returns>The result of the production at the given input index.</returns>
        protected TItem _MemoCall(string ruleName, int index, Action<int, IEnumerable<TItem>> production, IEnumerable<TItem> args)
        {
            string ruleKey = args == null ? ruleName
                : ruleName + string.Join(", ", args.Select(arg => arg.ToString()).ToArray());

            TItem result;

            // if we have a memo record, use that
            if (Memo.TryGetMemo(ruleKey, index, out result))
            {
                Results.Push(result);
                return result;
            }

            // check for left-recursion
            LRRecord<TItem> record;
            if (Memo.TryGetLRRecord(ruleKey, index, out record))
            {
                record.LRDetected = true;

                if (!Memo.TryGetMemo(record.CurrentExpansion, index, out result))
                    throw new Exception("Problem with expansion " + record.CurrentExpansion);
                Results.Push(result);
            }
            // no lr information
            else
            {
                record = new LRRecord<TItem>();
                record.LRDetected = false;
                record.NumExpansions = 0;
                record.CurrentExpansion = ruleKey + "_lrexp_" + record.NumExpansions;
                record.CurrentNextIndex = -1;
                Memo.Memoize(record.CurrentExpansion, index, null);
                Memo.StartLRRecord(ruleKey, index, record);

                CallStack.Push(record);

                while (true)
                {
                    production(index, args);
                    result = Results.Pop();

                    // do we need to keep trying the expansions?
                    if (record.LRDetected && result != null && result.NextIndex > record.CurrentNextIndex)
                    {
                        record.NumExpansions = record.NumExpansions + 1;
                        record.CurrentExpansion = ruleKey + "_lrexp_" + record.NumExpansions;
                        record.CurrentNextIndex = result != null ? result.NextIndex : 0;
                        Memo.Memoize(record.CurrentExpansion, index, result);

                        record.CurrentResult = result;
                    }
                    // we are done trying to expand
                    else
                    {
                        if (record.LRDetected)
                            result = record.CurrentResult;

                        Memo.ForgetLRRecord(ruleKey, index);
                        Results.Push(result);

                        // if there are no LR-processing rules at or above us in the stack, memoize
                        bool found_lr = false;
                        foreach (var rec in CallStack)
                        {
                            if (rec.LRDetected)
                            {
                                found_lr = true;
                                break;
                            }
                        }

                        if (!found_lr)
                            Memo.Memoize(ruleKey, index, result);

                        break;
                    }
                }

                CallStack.Pop();
            }

            return result;
        }

        #region LITERAL

        protected TItem _ParseLiteralString(ref int index, string str)
        {
            int cur_index = index;
            bool failed = false;

            try
            {
                foreach (var ch in str)
                {
                    if (InputString != null && cur_index >= InputString.Length)
                    {
                        failed = true;
                        break;
                    }

                    char cur_ch = InputString != null ? InputString[cur_index] : (char)(object)InputEnumerable.ElementAt(cur_index);
                    ++cur_index;

                    if (cur_ch != ch)
                    {
                        failed = true;
                        break;
                    }
                }
            }
            catch
            {
                failed = true;
            }

            if (!failed)
            {
                TItem result = new TItem()
                {
                    StartIndex = index,
                    NextIndex = cur_index,
                    InputEnumerable = InputEnumerable,
                };
                index = cur_index;
                Results.Push(result);
                return result;
            }

            Results.Push(null);
            _AddError(index, () => "expected \"" + Regex.Escape(str) + "\"");
            return null;
        }

        protected TItem _ParseLiteralChar(ref int index, char ch)
        {
            if (!(InputString != null && index >= InputString.Length))
            {
                try
                {
                    char cur_ch = InputString != null ? InputString[index] : (char)(object)InputEnumerable.ElementAt(index);
                    if (cur_ch == ch)
                    {
                        TItem result = new TItem()
                        {
                            StartIndex = index,
                            NextIndex = index + 1,
                            InputEnumerable = InputEnumerable,
                        };

                        ++index;
                        Results.Push(result);
                        return result;
                    }
                }
                catch { }
            }

            Results.Push(null);
            _AddError(index, () => "expected '" + ch + "'");

            return null;
        }

        protected TItem _ParseLiteralObj(ref int index, object obj)
        {
            if (obj is IEnumerable<TInput>)
            {
                int cur_index = index;
                bool failed = false;

                try
                {
                    foreach (var input in (IEnumerable<TInput>)obj)
                    {
                        TInput cur_input = InputList != null ? InputList[cur_index] : InputEnumerable.ElementAt(cur_index);
                        ++cur_index;

                        if (!cur_input.Equals(input))
                        {
                            failed = true;
                            break;
                        }
                    }
                }
                catch
                {
                    failed = true;
                }

                if (!failed)
                {
                    TItem result = new TItem()
                    {
                        StartIndex = index,
                        NextIndex = cur_index,
                        InputEnumerable = InputEnumerable,
                    };
                    Results.Push(result);
                    index = cur_index;

                    return result;
                }
            }
            else
            {
                try
                {
                    TInput cur_input = InputList != null ? InputList[index] : InputEnumerable.ElementAt(index);
                    if (cur_input.Equals(obj))
                    {
                        TItem result = new TItem()
                        {
                            StartIndex = index,
                            NextIndex = index + 1,
                            InputEnumerable = InputEnumerable,
                        };

                        Results.Push(result);
                        ++index;

                        return result;
                    }
                }
                catch { }
            }

            Results.Push(null);
            _AddError(index, () => "expected " + obj);
            return null;
        }


        protected TItem _ParseLiteralArgs(ref int item_index, ref int input_index, object obj, IEnumerable<TItem> args)
        {
            try
            {
                if (obj is IEnumerable<TInput>)
                {
                    int old_item_index = item_index;
                    int cur_item_index = item_index;
                    int cur_input_index = input_index;

                    var input_list = new List<TInput>();

                    foreach (TInput input in ((IEnumerable<TInput>)obj))
                    {
                        TItem cur_item = args.ElementAt(cur_item_index);
                        TInput cur_input = cur_item.Inputs.ElementAt(cur_input_index);

                        if (cur_input.Equals(input))
                        {
                            input_list.Add(cur_input);

                            if (cur_input_index + 1 >= cur_item.Inputs.Count())
                            {
                                ++cur_item_index;
                                cur_input_index = 0;
                            }
                            else
                            {
                                ++input_index;
                            }
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }

                    //
                    item_index = cur_item_index;
                    input_index = cur_input_index;

                    TItem result = new TItem()
                    {
                        StartIndex = old_item_index,
                        NextIndex = item_index,
                        Inputs = input_list,
                    };

                    ArgResults.Push(result);
                    return result;
                }
                else
                {
                    int old_item_index = item_index;

                    TItem cur_item = args.ElementAt(item_index);
                    TInput cur_input = cur_item.Inputs.ElementAt(input_index);

                    if (cur_input.Equals(obj))
                    {
                        // increment
                        if (input_index + 1 >= cur_item.Inputs.Count())
                        {
                            ++item_index;
                            input_index = 0;
                        }
                        else
                        {
                            ++input_index;
                        }

                        // 
                        TItem result = new TItem()
                        {
                            StartIndex = old_item_index,
                            NextIndex = item_index,
                            Inputs = new List<TInput> { cur_input },
                        };

                        ArgResults.Push(result);
                        return result;
                    }
                }
            }
            catch { }

            ArgResults.Push(null);
            return null;
        }

        #endregion

        #region INPUTCLASS

        protected TItem _ParseInputClass(ref int index, params char[] chars)
        {
            if (!(InputString != null && index >= InputString.Length))
            {
                try
                {
                    TInput input = InputList != null ? InputList[index] : InputEnumerable.ElementAt(index);
                    if (Array.IndexOf(chars, input) != -1)
                    {
                        TItem result = new TItem()
                        {
                            StartIndex = index,
                            NextIndex = index+1,
                            InputEnumerable = InputEnumerable,
                        };
                        ++index;
                        Results.Push(result);
                        return result;
                    }
                }
                catch { }
            }

            Results.Push(null);
            _AddError(index, () => "expected one of [" + Regex.Escape(new string(chars)) + "]");
            return null;
        }

        protected TItem _ParseInputClassArgs(ref int item_index, ref int input_index, IEnumerable<TItem> args, params char[] chars)
        {
            try
            {
                int cur_item_index = item_index;
                int cur_input_index = input_index;

                TItem cur_item = args.ElementAt(cur_item_index);
                TInput cur_input = cur_item.Inputs.ElementAt(cur_input_index);

                if (Array.IndexOf(chars, cur_input) != -1)
                {
                    if (cur_input_index + 1 >= cur_item.Inputs.Count())
                    {
                        ++cur_item_index;
                        cur_input_index = 0;
                    }
                    else
                    {
                        ++cur_input_index;
                    }

                    TItem result = new TItem()
                    {
                        StartIndex = item_index,
                        NextIndex = cur_item_index,
                        Inputs = new List<TInput> { cur_input },
                    };

                    item_index = cur_item_index;
                    input_index = cur_input_index;

                    ArgResults.Push(result);
                    return result;
                }
            }
            catch { }

            ArgResults.Push(null);
            return null;
        }

        #endregion

        #region ANY

        protected TItem _ParseAny(ref int index)
        {
            if (!(InputString != null && index >= InputString.Length))
            {
                try
                {
                    var _temp = InputList != null ? InputList[index] : InputEnumerable.ElementAt(index);
                    TItem result = new TItem()
                    {
                        StartIndex = index,
                        NextIndex = index+1,
                        InputEnumerable = InputEnumerable,
                    };
                    ++index;
                    Results.Push(result);
                    return result;
                }
                catch { }
            }

            Results.Push(null);
            return null;
        }

        protected TItem _ParseAnyArgs(ref int item_index, ref int input_index, IEnumerable<TItem> args)
        {
            try
            {
                if (input_index == 0)
                {
                    var _temp = args.ElementAt(item_index);
                    ++item_index;
                    ArgResults.Push(_temp);
                    return _temp;
                }
            }
            catch { }

            ArgResults.Push(null);
            return null;
        }

        #endregion

        #endregion

    } // class Matcher

    /// <summary>
    /// Stores information about errors.
    /// </summary>
    public class ErrorRec
    {
        string _msg = null;
        Func<string> _func = null;

        /// <summary>
        /// Input index of the error.
        /// </summary>
        public int Pos { get; set; }

        /// <summary>
        /// The function used to generate the error message (use a lambda to defer string processing until the error needs to be printed).
        /// </summary>
        public Func<string> Func
        {
            private get { return _func; }

            set
            {
                if ((_func = value) != null)
                    _msg = null;
            }
        }
        
        /// <summary>
        /// The error message.
        /// </summary>
        public string Message
        {
            get
            {
                return _msg ?? (_msg = (Func != null ? Func() : string.Empty));
            }

            set
            {
                _msg = value;
            }
        }
    }

} // namespace Matcher
