﻿// IronMeta Copyright © The IronMeta Developers

using System;
using System.Collections.Concurrent;
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

        /// <summary>
        /// Called before a match begins.
        /// </summary>
        protected event Action<MatchState<TInput, TResult>, IEnumerable<TInput>, Action<MatchState<TInput, TResult>, int, IEnumerable<MatchItem<TInput, TResult>>>> BeforeMatch;

        /// <summary>
        /// Called after a match ends.
        /// </summary>
        protected event Action<MatchResult<TInput, TResult>> AfterMatch;

        #endregion

        #region Properties

        /// <summary>
        /// Whether or not the matcher should detect and process left-recursive rules correctly.
        /// Setting this to false will make matching run faster, but go into an infinite loop if there is a left-recursive rule in your grammar.
        /// </summary>
        public bool HandleLeftRecursion { get; private set; }

        protected ISet<string> Terminals { get; set; }

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
        /// <param name="index">Index at which to start the match.</param>
        /// <returns>The result of the match.</returns>
        public virtual MatchResult<TInput, TResult> GetMatch(
            IEnumerable<TInput> input,
            Action<MatchState<TInput, TResult>, int, IEnumerable<MatchItem<TInput, TResult>>> production,
            int index = 0)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            var state = new MatchState<TInput, TResult>(input);
            return GetMatch(state, production, index);
        }

        public virtual MatchResult<TInput, TResult> GetMatch(
            MatchState<TInput, TResult> state,
            Action<MatchState<TInput, TResult>, int, IEnumerable<MatchItem<TInput, TResult>>> production,
            int index)
        {
            if (state == null)
                throw new ArgumentNullException("state");
            if (production == null)
                throw new ArgumentNullException("production");

            MatchItem<TInput, TResult> result = null;

            if (BeforeMatch != null)
                BeforeMatch(state, state.InputList, production);

            try
            {
                if (Terminals == null)
                    Terminals = new HashSet<string>();

                result = _MemoCall(state, production.Method.Name, index, production, null);
            }
            catch (MatcherException me)
            {
                state.ClearErrors();
                state.AddError(me.Index, me.Message);
            }
            catch (Exception e)
            {
                state.ClearErrors();
                state.AddError(0, e.Message
#if DEBUG
                    + "\n" + e.StackTrace
#endif
                );
            }

            state.ClearMemoTable(); // allow memo tables to be gc'd

            string GetLastError() => state.LastError;

            var match_result = result != null
                ? new MatchResult<TInput, TResult>(this, state, true, result.StartIndex, result.NextIndex, result.Results, GetLastError, state.LastErrorIndex)
                : new MatchResult<TInput, TResult>(this, state, false, -1, -1, null, GetLastError, state.LastErrorIndex);

            if (AfterMatch != null)
                AfterMatch(match_result);

            return match_result;
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
        protected MatchItem<TInput, TResult> _MemoCall
        (
            MatchState<TInput, TResult> memo,
            string ruleName,
            int index,
            Action<MatchState<TInput, TResult>, int, IEnumerable<MatchItem<TInput, TResult>>> production,
            IEnumerable<MatchItem<TInput, TResult>> args
        )
        {
            MatchItem<TInput, TResult> result;

            var expansion = new Expansion
            {
                Name = args == null ? ruleName : ruleName + string.Join(", ", args.Select(arg => arg.ToString()).ToArray()),
                Num = 0
            };

            // if we have a memo record, use that
            if (memo.TryGetMemo(expansion, index, out result))
            {
                memo.Results.Push(result);
                return result;
            }

            // if we are not handling left recursion, just call the production directly.
            if (!HandleLeftRecursion || Terminals.Contains(ruleName))
            {
                production(memo, index, args);
                result = memo.Results.Peek();

                memo.Memoize(expansion, index, result);

                if (result == null)
                    memo.AddError(index, () => "expected " + ruleName);

                return result;
            }

            // check for left-recursion
            LRRecord<MatchItem<TInput, TResult>> record;
            if (memo.TryGetLRRecord(expansion, index, out record))
            {
                record.LRDetected = true;

                var involved = memo.CallStack
                    .TakeWhile(rec => rec.CurrentExpansion.Name != expansion.Name)
                    .Select(rec => rec.CurrentExpansion.Name);

                if (record.InvolvedRules != null)
                    record.InvolvedRules.UnionWith(involved);
                else
                    record.InvolvedRules = new HashSet<string>(involved);

                if (!memo.TryGetMemo(record.CurrentExpansion, index, out result))
                    throw new MatcherException(index, "Problem with expansion " + record.CurrentExpansion);
                memo.Results.Push(result);
            }
            // no lr information
            else
            {
                record = new LRRecord<MatchItem<TInput, TResult>>();
                record.LRDetected = false;
                record.NumExpansions = 1;
                record.CurrentExpansion = new Expansion { Name = expansion.Name, Num = record.NumExpansions };
                record.CurrentNextIndex = -1;

                memo.Memoize(record.CurrentExpansion, index, null);
                memo.StartLRRecord(expansion, index, record);

                memo.CallStack.Push(record);

                while (true)
                {
                    production(memo, index, args);
                    result = memo.Results.Pop();

                    // do we need to keep trying the expansions?
                    if (record.LRDetected && result != null && result.NextIndex > record.CurrentNextIndex)
                    {
                        record.NumExpansions = record.NumExpansions + 1;
                        record.CurrentExpansion = new Expansion { Name = expansion.Name, Num = record.NumExpansions };
                        record.CurrentNextIndex = result != null ? result.NextIndex : 0;
                        memo.Memoize(record.CurrentExpansion, index, result);

                        record.CurrentResult = result;
                    }
                    // we are done trying to expand
                    else
                    {
                        if (record.LRDetected)
                            result = record.CurrentResult;

                        memo.ForgetLRRecord(expansion, index);
                        memo.Results.Push(result);

                        // if we are not involved in any left-recursion expansions above us, memoize
                        memo.CallStack.Pop();

                        bool found_lr = memo.CallStack.Any(rec => rec.InvolvedRules != null && rec.InvolvedRules.Contains(expansion.Name));
                        if (!found_lr)
                            memo.Memoize(expansion, index, result);

                        if (result == null)
                            memo.AddError(index, () => "expected " + expansion.Name);
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
        protected MatchItem<TInput, TResult> _ParseLiteralString(MatchState<TInput, TResult> memo, ref int index, string str)
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
            //memo.AddError(index, () => "expected \"" + Regex.Escape(str) + "\"");
            return null;
        }

        /// <summary>
        /// Matches a literal char (only used with matchers on a <c>char</c> enumerable).
        /// </summary>
        /// <param name="memo">Memo.</param>
        /// <param name="index">Index.</param>
        /// <param name="ch">Character to match.</param>
        protected MatchItem<TInput, TResult> _ParseLiteralChar(MatchState<TInput, TResult> memo, ref int index, char ch)
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
            //memo.AddError(index, () => string.Format("expected '{0}'", Regex.Escape(new string(ch, 1))));
            return null;
        }

        /// <summary>
        /// Matches a literal object.
        /// </summary>
        /// <param name="memo">Memo.</param>
        /// <param name="index">Index.</param>
        /// <param name="obj">Object to match.</param>
        protected MatchItem<TInput, TResult> _ParseLiteralObj(MatchState<TInput, TResult> memo, ref int index, object obj)
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
            //memo.AddError(index, () => "expected " + obj);
            return null;
        }

        /// <summary>
        /// Checks for anonymous literals in the rule before matching against input object.
        /// </summary>
        /// <param name="expected">Expected object.  May be an anonymously-typed object.</param>
        /// <param name="encountered">Object encountered in the input.</param>
        /// <returns></returns>
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
        protected MatchItem<TInput, TResult> _ParseLiteralArgs(MatchState<TInput, TResult> memo, ref int item_index, ref int input_index, object obj, IEnumerable<MatchItem<TInput, TResult>> args)
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
            //memo.AddError(input_index, () => "expected " + obj);
            return null;
        }

        #endregion

        #region REGEXP

        protected MatchItem<TInput, TResult> _ParseRegexp(MatchState<TInput, TResult> memo, ref int index, Verophyle.Regexp.StringRegexp re)
        {
            int cur_index = index;
            int succ_index = -1;

            try
            {
                re.Reset();
                do
                {
                    re.ProcessInput(memo.InputString[cur_index]);
                    if (re.Succeeded)
                        succ_index = cur_index;
                }
                while (++cur_index < memo.InputString.Length && !re.Failed);
            }
            catch
            {
                succ_index = -1;
            }

            if (succ_index >= 0)
            {
                var result = new MatchItem<TInput, TResult>
                {
                    StartIndex = index,
                    NextIndex = succ_index + 1,
                    InputEnumerable = memo.InputEnumerable
                };
                index = succ_index + 1;
                memo.Results.Push(result);
                return result;
            }
            else
            {
                memo.Results.Push(null);
                return null;
            }
        }

        #endregion

        #region INPUTCLASS

        /// <summary>
        /// Matches a set of characters.  Only used for matchers over <c>char</c> enumerables.
        /// </summary>
        /// <param name="memo">Memo.</param>
        /// <param name="index">Index.</param>
        /// <param name="chars">Characters to match.</param>
        protected MatchItem<TInput, TResult> _ParseInputClass(MatchState<TInput, TResult> memo, ref int index, params char[] chars)
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
            //memo.AddError(index, () => "expected one of [" + Regex.Escape(new string(chars)) + "]");
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
        protected MatchItem<TInput, TResult> _ParseInputClassArgs(MatchState<TInput, TResult> memo, ref int item_index, ref int input_index, IEnumerable<MatchItem<TInput, TResult>> args, params char[] chars)
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
            //memo.AddError(input_index, () => "expected " + args);
            return null;
        }

        #endregion

        #region ANY

#pragma warning disable 0219
        /// <summary>
        /// Matches any input.
        /// </summary>
        /// <param name="memo">Memo.</param>
        /// <param name="index">Index.</param>
        protected MatchItem<TInput, TResult> _ParseAny(MatchState<TInput, TResult> memo, ref int index)
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
#pragma warning restore 0219

        /// <summary>
        /// Matches any input in an argument stream.
        /// </summary>
        /// <param name="memo">Memo.</param>
        /// <param name="item_index">Item index.</param>
        /// <param name="input_index">Input index.</param>
        /// <param name="args">Argument stream.</param>
        protected MatchItem<TInput, TResult> _ParseAnyArgs(MatchState<TInput, TResult> memo, ref int item_index, ref int input_index, IEnumerable<MatchItem<TInput, TResult>> args)
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

        protected MatchItem<TInput, TResult> _ParseAnyArgs(MatchState<TInput, TResult> memo, ref int arg_index, IEnumerable<MatchItem<TInput, TResult>> args)
        {
            if (args != null)
            {
                var item = args.ElementAt(arg_index);
                memo.ArgResults.Push(item);
                ++arg_index;
                return item;
            }

            memo.ArgResults.Push(null);
            memo.AddError(arg_index, () => "not enough arguments");
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

    /// <summary>
    /// Extension class to determine if a type is anonymous; cribbed from http://stackoverflow.com/questions/1650681/determining-whether-a-type-is-an-anonymous-type
    /// </summary>
    public static class TypeExtension
    {
        private static readonly ConcurrentDictionary<Type, bool> typeCache = new ConcurrentDictionary<Type, bool>();

        /// <summary>
        /// Heuristic test to determine whether a type is anonymous.
        /// Uses cache to quickly identify already checked types
        /// Thread safe
        /// </summary>
        /// <param name="type">The type in question.</param>
        public static bool IsAnonymousType(this Type type)
        {
            if (typeCache.TryGetValue(type, out bool isAnonymous))
            {
                return isAnonymous;
            }

            bool hasCompilerGeneratedAttribute = type.GetCustomAttributes(typeof(CompilerGeneratedAttribute), false).Any();
            if (!hasCompilerGeneratedAttribute)
            {
                typeCache.AddOrUpdate(type, false, (key, oldValue) => false);
                return false;
            }

            bool nameContainsAnonymousType = (type.FullName.Contains("AnonymousType") || type.FullName.Contains("AnonType"))
                                             && (type.FullName.StartsWith("<>") || type.FullName.StartsWith("VB$"));
            typeCache.AddOrUpdate(type, nameContainsAnonymousType, (key, oldValue) => nameContainsAnonymousType);
            return nameContainsAnonymousType;
        }
    }
}
