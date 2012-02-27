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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace IronMeta.Matcher
{

    /// <summary>
    /// Base class for IronMeta grammars.
    /// </summary>
    /// <typeparam name="TInput">The type of inputs to the grammar.</typeparam>
    /// <typeparam name="TResult">The type of results of grammar rules.</typeparam>
    public abstract class Matcher<TInput, TResult>
    {

        #region Members

        /// <summary>
        /// A utility delegate for filtering an enumerable to return only non-null items.
        /// </summary>
        protected static readonly Func<TResult, bool> _NON_NULL = r => r != null;

        #endregion

        #region Properties

        /// <summary>
        /// Whether or not the matcher should detect and process left-recursive rules correctly.
        /// Setting this to false will make matching run faster, but go into an infinite loop if there is a left-recursive rule in your grammar.
        /// </summary>
        public bool HandleLeftRecursion { get; private set; }

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        public Matcher()
        {
            HandleLeftRecursion = true;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="handle_left_recursion">Whether or not the matcher should handle left-recursion.</param>
        public Matcher(bool handle_left_recursion)
        {
            HandleLeftRecursion = handle_left_recursion;
        }

        /// <summary>
        /// Try to match the input.
        /// </summary>
        /// <param name="input">The input to be matched.</param>
        /// <param name="production">The top-level grammar production (rule) of the generated parser class to use.</param>
        /// <returns>The result of the match.</returns>
        public MatchResult<TInput, TResult> GetMatch(IEnumerable<TInput> input, Action<Memo<TInput, TResult>, int, IEnumerable<MatchItem<TInput, TResult>>> production)
        {
            var memo = new Memo<TInput, TResult>(input);
            MatchItem<TInput, TResult> result = null;

            try
            {
                result = _MemoCall(memo, production.Method.Name, 0, production, null);
            }
            catch (MatcherException me)
            {
                memo.ClearErrors();
                memo.AddError(me.Index, me.Message);
            }
            catch (Exception e)
            {
                memo.ClearErrors();
                memo.AddError(0, e.Message);
            }

            memo.ClearMemoTable(); // allow memo tables to be gc'd

            if (result != null)
                return new MatchResult<TInput, TResult>(this, memo, true, result.StartIndex, result.NextIndex, result.Results, memo.LastError, memo.LastErrorIndex);
            else
                return new MatchResult<TInput, TResult>(this, memo, false, -1, -1, null, memo.LastError, memo.LastErrorIndex);
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
        /// <param name="memo">The memo for the current match.</param>
        /// <param name="ruleName">The name of the production.</param>
        /// <param name="index">The current index in the input stream.</param>
        /// <param name="production">The production itself.</param>
        /// <param name="args">Arguments to the production (can be null).</param>
        /// <returns>The result of the production at the given input index.</returns>
        protected MatchItem<TInput, TResult> _MemoCall(Memo<TInput, TResult> memo, string ruleName, int index, Action<Memo<TInput, TResult>, int, IEnumerable<MatchItem<TInput, TResult>>> production, IEnumerable<MatchItem<TInput, TResult>> args)
        {
            MatchItem<TInput, TResult> result;

            // if we are not handling left recursion, just call the production directly.
            if (!HandleLeftRecursion)
            {
                production(memo, index, args);
                result = memo.Results.Peek();

                if (result == null)
                    memo.AddError(index, () => "expected " + ruleName);

                return result;
            }

            string ruleKey = args == null ? ruleName
                : ruleName + string.Join(", ", args.Select(arg => arg.ToString()).ToArray());

            // if we have a memo record, use that
            if (memo.TryGetMemo(ruleKey, index, out result))
            {
                memo.Results.Push(result);
                return result;
            }

            // check for left-recursion
            LRRecord<MatchItem<TInput, TResult>> record;
            if (memo.TryGetLRRecord(ruleKey, index, out record))
            {
                record.LRDetected = true;

                if (!memo.TryGetMemo(record.CurrentExpansion, index, out result))
                    throw new MatcherException(index, "Problem with expansion " + record.CurrentExpansion);
                memo.Results.Push(result);
            }
            // no lr information
            else
            {
                record = new LRRecord<MatchItem<TInput, TResult>>();
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
                        memo.CallStack.Pop();

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
            }

            return result;
        }

        #region LITERAL

        /// <summary>
        /// Matches a literal string (only used with matchers on a <c>char</c> enumerable).
        /// </summary>
        /// <param name="memo">Memo.</param>
        /// <param name="index">Index.</param>
        /// <param name="str">String to match.</param>
        protected MatchItem<TInput, TResult> _ParseLiteralString(Memo<TInput, TResult> memo, ref int index, string str)
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
                var result = new MatchItem<TInput, TResult>
                {
                    StartIndex = index,
                    NextIndex = cur_index,
                    InputEnumerable = memo.InputEnumerable
                };
                index = cur_index;
                memo.Results.Push(result);
                return result;
            }

            memo.Results.Push(null);
            memo.AddError(index, () => "expected \"" + Regex.Escape(str) + "\"");
            return null;
        }

        /// <summary>
        /// Matches a literal char (only used with matchers on a <c>char</c> enumerable).
        /// </summary>
        /// <param name="memo">Memo.</param>
        /// <param name="index">Index.</param>
        /// <param name="ch">Character to match.</param>
        protected MatchItem<TInput, TResult> _ParseLiteralChar(Memo<TInput, TResult> memo, ref int index, char ch)
        {
            if (!(memo.InputString != null && index >= memo.InputString.Length))
            {
                try
                {
                    char cur_ch = memo.InputString != null ? memo.InputString[index] : (char)(object)memo.InputEnumerable.ElementAt(index);
                    if (cur_ch == ch)
                    {
                        var result = new MatchItem<TInput, TResult>
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
            memo.AddError(index, () => string.Format("expected '{0}'", Regex.Escape(new string(ch, 1))));

            return null;
        }

        /// <summary>
        /// Matches a literal object.
        /// </summary>
        /// <param name="memo">Memo.</param>
        /// <param name="index">Index.</param>
        /// <param name="obj">Object to match.</param>
        protected MatchItem<TInput, TResult> _ParseLiteralObj(Memo<TInput, TResult> memo, ref int index, object obj)
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

                        if (!ObjectEquals(input, cur_input))
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
                    var result = new MatchItem<TInput, TResult>
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

                    if (ObjectEquals(obj, cur_input))
                    {
                        var result = new MatchItem<TInput, TResult>
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

        protected bool ObjectEquals(object expected, object encountered)
        {
            var expectedType = expected.GetType();
            var encounteredType = encountered.GetType();

            if (expectedType.IsAnonymousType())
            {
                // go through public properties
                foreach (var expectedProperty in expectedType.GetProperties())
                {
                    var encounteredProperty = encounteredType.GetProperty(expectedProperty.Name);
                    if (encounteredProperty == null)
                        return false;

                    var expectedVal = expectedProperty.GetValue(expected, null);
                    var encounteredVal = encounteredProperty.GetValue(encountered, null);

                    if (expectedVal == null)
                    {
                        if (encounteredVal == null)
                            continue;
                        else
                            return false;
                    }

                    if (!expectedVal.Equals(encounteredVal))
                        return false;
                }

                return true;
            }
            else
            {
                return expected.Equals(encountered);
            }
        }

        /// <summary>
        /// Matches a literal object in an argument stream.
        /// </summary>
        /// <param name="memo">Memo.</param>
        /// <param name="item_index">Item index.</param>
        /// <param name="input_index">Input index.</param>
        /// <param name="obj">Object to match.</param>
        /// <param name="args">Argument stream.</param>
        protected MatchItem<TInput, TResult> _ParseLiteralArgs(Memo<TInput, TResult> memo, ref int item_index, ref int input_index, object obj, IEnumerable<MatchItem<TInput, TResult>> args)
        {
            if (args != null)
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
                            var cur_item = args.ElementAt(cur_item_index);
                            TInput cur_input = cur_item.Inputs.ElementAt(cur_input_index);

                            if (ObjectEquals(input, cur_input))
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
                            }
                        }

                        //
                        item_index = cur_item_index;
                        input_index = cur_input_index;

                        var result = new MatchItem<TInput, TResult>
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

                        var cur_item = args.ElementAt(item_index);
                        TInput cur_input = cur_item.Inputs.ElementAt(input_index);

                        if (ObjectEquals(obj, cur_input))
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
                            var result = new MatchItem<TInput, TResult>
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
            }

            memo.ArgResults.Push(null);
            memo.AddError(input_index, () => "expected " + obj);
            return null;
        }

        #endregion

        #region INPUTCLASS

        /// <summary>
        /// Matches a set of characters.  Only used for matchers over <c>char</c> enumerables.
        /// </summary>
        /// <param name="memo">Memo.</param>
        /// <param name="index">Index.</param>
        /// <param name="chars">Characters to match.</param>
        protected MatchItem<TInput, TResult> _ParseInputClass(Memo<TInput, TResult> memo, ref int index, params char[] chars)
        {
            if (!(memo.InputString != null && index >= memo.InputString.Length))
            {
                try
                {
                    TInput input = memo.InputList != null ? memo.InputList[index] : memo.InputEnumerable.ElementAt(index);
                    if (Array.IndexOf(chars, input) != -1)
                    {
                        var result = new MatchItem<TInput, TResult>
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
            memo.AddError(index, () => "expected one of [" + Regex.Escape(new string(chars)) + "]");
            return null;
        }

        /// <summary>
        /// Matches a set of characters in an argument stream.
        /// </summary>
        /// <param name="memo">Memo.</param>
        /// <param name="item_index">Item index.</param>
        /// <param name="input_index">Input index.</param>
        /// <param name="args">Argument stream.</param>
        /// <param name="chars">Characters to match.</param>
        protected MatchItem<TInput, TResult> _ParseInputClassArgs(Memo<TInput, TResult> memo, ref int item_index, ref int input_index, IEnumerable<MatchItem<TInput, TResult>> args, params char[] chars)
        {
            try
            {
                int cur_item_index = item_index;
                int cur_input_index = input_index;

                var cur_item = args.ElementAt(cur_item_index);
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

                    var result = new MatchItem<TInput, TResult>
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

        /// <summary>
        /// Matches any input.
        /// </summary>
        /// <param name="memo">Memo.</param>
        /// <param name="index">Index.</param>
        protected MatchItem<TInput, TResult> _ParseAny(Memo<TInput, TResult> memo, ref int index)
        {
            if (!(memo.InputString != null && index >= memo.InputString.Length))
            {
                try
                {
                    var _temp = memo.InputList != null ? memo.InputList[index] : memo.InputEnumerable.ElementAt(index);
                    var result = new MatchItem<TInput, TResult>
                    {
                        StartIndex = index,
                        NextIndex = index + 1,
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

        /// <summary>
        /// Matches any input in an argument stream.
        /// </summary>
        /// <param name="memo">Memo.</param>
        /// <param name="item_index">Item index.</param>
        /// <param name="input_index">Input index.</param>
        /// <param name="args">Argument stream.</param>
        protected MatchItem<TInput, TResult> _ParseAnyArgs(Memo<TInput, TResult> memo, ref int item_index, ref int input_index, IEnumerable<MatchItem<TInput, TResult>> args)
        {
            if (args != null)
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
            }

            memo.ArgResults.Push(null);
            memo.AddError(input_index, () => "not enough arguments");
            return null;
        }

        #endregion

        #endregion

        /// <summary>
        /// Exception class for extraordinary parsing errors.
        /// </summary>
        public class MatcherException : Exception
        {
            /// <summary>
            /// Position in the input at which the error happened.
            /// </summary>
            public int Index { get; private set; }

            /// <summary>
            /// Constructor.
            /// </summary>
            /// <param name="message">Message.</param>
            public MatcherException(string message)
                : base(message)
            {
                Index = 0;
            }

            /// <summary>
            /// Constructor.
            /// </summary>
            /// <param name="index">Index.</param>
            /// <param name="message">Message.</param>
            public MatcherException(int index, string message)
                : this(message)
            {
                Index = index;
            }
        }

    } // class Matcher

    // extension to determine if a type is anonymous; cribbed from http://stackoverflow.com/questions/1650681/determining-whether-a-type-is-an-anonymous-type
    public static class TypeExtension
    {
        public static bool IsAnonymousType(this Type type)
        {
            bool hasCompilerGeneratedAttribute = type.GetCustomAttributes(typeof(CompilerGeneratedAttribute), false).Count() > 0;
            if (!hasCompilerGeneratedAttribute)
                return false;

            bool nameContainsAnonymousType = type.FullName.Contains("AnonymousType");
            if (!nameContainsAnonymousType)
                return false;

            return true;
        }
    }

} // namespace Matcher
