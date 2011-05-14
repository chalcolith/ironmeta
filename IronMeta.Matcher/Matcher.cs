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

        protected static readonly Func<TResult, bool> _NON_NULL = r => r != null;

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
        public MatchResult<TInput, TResult, TItem> GetMatch(IEnumerable<TInput> input, Action<Memo<TInput, TResult, TItem>, int, IEnumerable<TItem>> production)
        {
            var memo = new Memo<TInput, TResult, TItem>(input);
            var result = _MemoCall(memo, production.Method.Name, 0, production, null);

            if (result != null)
                return new MatchResult<TInput, TResult, TItem>(this, memo, true, result.StartIndex, result.NextIndex, result.Results, memo.LastError.Message, memo.LastError.Pos);
            else
                return new MatchResult<TInput, TResult, TItem>(this, memo, false, -1, -1, null, memo.LastError.Message, memo.LastError.Pos);
        }

        #region Internal Parser Functions

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
        static protected TItem _MemoCall(Memo<TInput, TResult, TItem> memo, string ruleName, int index, Action<Memo<TInput, TResult, TItem>, int, IEnumerable<TItem>> production, IEnumerable<TItem> args)
        {
            string ruleKey = args == null ? ruleName
                : ruleName + string.Join(", ", args.Select(arg => arg.ToString()).ToArray());

            TItem result;

            // if we have a memo record, use that
            if (memo.TryGetMemo(ruleKey, index, out result))
            {
                memo.Results.Push(result);
                return result;
            }

            // check for left-recursion
            LRRecord<TItem> record;
            if (memo.TryGetLRRecord(ruleKey, index, out record))
            {
                record.LRDetected = true;

                if (!memo.TryGetMemo(record.CurrentExpansion, index, out result))
                    throw new Exception("Problem with expansion " + record.CurrentExpansion);
                memo.Results.Push(result);
            }
            // no lr information
            else
            {
                record = new LRRecord<TItem>();
                record.LRDetected = false;
                record.NumExpansions = 0;
                record.CurrentExpansion = ruleKey + "_lrexp_" + record.NumExpansions;
                record.CurrentNextIndex = -1;
                memo.Memoize(record.CurrentExpansion, index, null);
                memo.StartLRRecord(ruleKey, index, record);

                memo.CallStack.Push(record);

                while (true)
                {
                    production(memo, index, args);
                    result = memo.Results.Pop();

                    // do we need to keep trying the expansions?
                    if (record.LRDetected && result != null && result.NextIndex > record.CurrentNextIndex)
                    {
                        record.NumExpansions = record.NumExpansions + 1;
                        record.CurrentExpansion = ruleKey + "_lrexp_" + record.NumExpansions;
                        record.CurrentNextIndex = result != null ? result.NextIndex : 0;
                        memo.Memoize(record.CurrentExpansion, index, result);

                        record.CurrentResult = result;
                    }
                    // we are done trying to expand
                    else
                    {
                        if (record.LRDetected)
                            result = record.CurrentResult;

                        memo.ForgetLRRecord(ruleKey, index);
                        memo.Results.Push(result);

                        // if there are no LR-processing rules at or above us in the stack, memoize
                        bool found_lr = false;
                        foreach (var rec in memo.CallStack)
                        {
                            if (rec.LRDetected)
                            {
                                found_lr = true;
                                break;
                            }
                        }

                        if (!found_lr)
                            memo.Memoize(ruleKey, index, result);

                        if (result == null)
                            memo.AddError(index, () => "expected " + ruleKey);

                        break;
                    }
                }

                memo.CallStack.Pop();
            }

            return result;
        }

        #region LITERAL

        static protected TItem _ParseLiteralString(Memo<TInput, TResult, TItem> memo, ref int index, string str)
        {
            int cur_index = index;
            bool failed = false;

            try
            {
                foreach (var ch in str)
                {
                    if (memo.InputString != null && cur_index >= memo.InputString.Length)
                    {
                        failed = true;
                        break;
                    }

                    char cur_ch = memo.InputString != null ? memo.InputString[cur_index] : (char)(object)memo.InputEnumerable.ElementAt(cur_index);
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
                    InputEnumerable = memo.InputEnumerable,
                };
                index = cur_index;
                memo.Results.Push(result);
                return result;
            }

            memo.Results.Push(null);
            memo.AddError(index, () => "expected \"" + Regex.Escape(str) + "\"");
            return null;
        }

        static protected TItem _ParseLiteralChar(Memo<TInput, TResult, TItem> memo, ref int index, char ch)
        {
            if (!(memo.InputString != null && index >= memo.InputString.Length))
            {
                try
                {
                    char cur_ch = memo.InputString != null ? memo.InputString[index] : (char)(object)memo.InputEnumerable.ElementAt(index);
                    if (cur_ch == ch)
                    {
                        TItem result = new TItem()
                        {
                            StartIndex = index,
                            NextIndex = index + 1,
                            InputEnumerable = memo.InputEnumerable,
                        };

                        ++index;
                        memo.Results.Push(result);
                        return result;
                    }
                }
                catch { }
            }

            memo.Results.Push(null);
            memo.AddError(index, () => "expected '" + ch + "'");

            return null;
        }

        static protected TItem _ParseLiteralObj(Memo<TInput, TResult, TItem> memo, ref int index, object obj)
        {
            if (obj is IEnumerable<TInput>)
            {
                int cur_index = index;
                bool failed = false;

                try
                {
                    foreach (var input in (IEnumerable<TInput>)obj)
                    {
                        TInput cur_input = memo.InputList != null ? memo.InputList[cur_index] : memo.InputEnumerable.ElementAt(cur_index);
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
                        InputEnumerable = memo.InputEnumerable,
                    };
                    memo.Results.Push(result);
                    index = cur_index;

                    return result;
                }
            }
            else
            {
                try
                {
                    TInput cur_input = memo.InputList != null ? memo.InputList[index] : memo.InputEnumerable.ElementAt(index);
                    if (cur_input.Equals(obj))
                    {
                        TItem result = new TItem()
                        {
                            StartIndex = index,
                            NextIndex = index + 1,
                            InputEnumerable = memo.InputEnumerable,
                        };

                        memo.Results.Push(result);
                        ++index;

                        return result;
                    }
                }
                catch { }
            }

            memo.Results.Push(null);
            memo.AddError(index, () => "expected " + obj);
            return null;
        }

        static protected TItem _ParseLiteralArgs(Memo<TInput, TResult, TItem> memo, ref int item_index, ref int input_index, object obj, IEnumerable<TItem> args)
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

                    memo.ArgResults.Push(result);
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

                        memo.ArgResults.Push(result);
                        return result;
                    }
                }
            }
            catch { }

            memo.ArgResults.Push(null);
            memo.AddError(input_index, () => "expected " + obj);
            return null;
        }

        #endregion

        #region INPUTCLASS

        static protected TItem _ParseInputClass(Memo<TInput, TResult, TItem> memo, ref int index, params char[] chars)
        {
            if (!(memo.InputString != null && index >= memo.InputString.Length))
            {
                try
                {
                    TInput input = memo.InputList != null ? memo.InputList[index] : memo.InputEnumerable.ElementAt(index);
                    if (Array.IndexOf(chars, input) != -1)
                    {
                        TItem result = new TItem()
                        {
                            StartIndex = index,
                            NextIndex = index+1,
                            InputEnumerable = memo.InputEnumerable,
                        };
                        ++index;
                        memo.Results.Push(result);
                        return result;
                    }
                }
                catch { }
            }

            memo.Results.Push(null);
            memo.AddError(index, () => "expected one of [" + Regex.Escape(new string(chars)) + "]");
            return null;
        }

        static protected TItem _ParseInputClassArgs(Memo<TInput, TResult, TItem> memo, ref int item_index, ref int input_index, IEnumerable<TItem> args, params char[] chars)
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

                    memo.ArgResults.Push(result);
                    return result;
                }
            }
            catch { }

            memo.ArgResults.Push(null);
            memo.AddError(input_index, () => "expected " + args);
            return null;
        }

        #endregion

        #region ANY

        static protected TItem _ParseAny(Memo<TInput, TResult, TItem> memo, ref int index)
        {
            if (!(memo.InputString != null && index >= memo.InputString.Length))
            {
                try
                {
                    var _temp = memo.InputList != null ? memo.InputList[index] : memo.InputEnumerable.ElementAt(index);
                    TItem result = new TItem()
                    {
                        StartIndex = index,
                        NextIndex = index+1,
                        InputEnumerable = memo.InputEnumerable,
                    };
                    ++index;
                    memo.Results.Push(result);
                    return result;
                }
                catch { }
            }

            memo.Results.Push(null);
            memo.AddError(index, () => "unexpected end of file");
            return null;
        }

        static protected TItem _ParseAnyArgs(Memo<TInput, TResult, TItem> memo, ref int item_index, ref int input_index, IEnumerable<TItem> args)
        {
            try
            {
                if (input_index == 0)
                {
                    var _temp = args.ElementAt(item_index);
                    ++item_index;
                    memo.ArgResults.Push(_temp);
                    return _temp;
                }
            }
            catch { }

            memo.ArgResults.Push(null);
            memo.AddError(input_index, () => "not enough arguments");
            return null;
        }

        #endregion

        #endregion

    } // class Matcher

} // namespace Matcher
