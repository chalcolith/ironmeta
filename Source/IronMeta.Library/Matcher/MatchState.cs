// IronMeta Copyright © Gordon Tisher 2019

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using IronMeta.Utils;

namespace IronMeta.Matcher
{
    /// <summary>
    /// Holds memoization and left-recursion state during a parse.
    /// </summary>
    /// <typeparam name="TInput">The input type.</typeparam>
    /// <typeparam name="TResult">The result type.</typeparam>
    public class MatchState<TInput, TResult>
    {
        IDictionary<string, object> properties = new Dictionary<string, object>();

        // rulename -> expansion -> index -> item
        IDictionary<string, Dictionary<int, Dictionary<int, MatchItem<TInput, TResult>>>> memo_table 
            = new Dictionary<string, Dictionary<int, Dictionary<int, MatchItem<TInput, TResult>>>>();

        // rulename -> index -> lrrecord
        IDictionary<string, Dictionary<int, LRRecord<MatchItem<TInput, TResult>>>> current_recursions 
            = new Dictionary<string, Dictionary<int, LRRecord<MatchItem<TInput, TResult>>>>();

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

        /// <summary>
        /// The input enumerable for this match.
        /// </summary>
        public IEnumerable<TInput> InputEnumerable { get; set; }

        /// <summary>
        /// The input enumerable for this match (<see cref="InputEnumerable"/>) as an <c>IList&lt;&gt;</c>.
        /// Is <c>null</c> if the input enumerable is not an <c>IList&lt;&gt;</c>.
        /// </summary>
        public IList<TInput> InputList { get; set; }

        /// <summary>
        /// The input enumerable for this match (<see cref="InputEnumerable"/>) as a <c>string</c>.
        /// Is <c>null</c> if the input enumerable is not a <see cref="string"/>.
        /// </summary>
        public string InputString { get; set; }

        /// <summary>
        /// The result stack used while matching.
        /// </summary>
        public Stack<MatchItem<TInput, TResult>> Results { get; set; }

        /// <summary>
        /// The result stack used while matching arguments.
        /// </summary>
        public Stack<MatchItem<TInput, TResult>> ArgResults { get; set; }

        /// <summary>
        /// The call stack used while matching.
        /// </summary>
        public Stack<LRRecord<MatchItem<TInput, TResult>>> CallStack { get; set; }

        /// <summary>
        /// Used by calling code to store special positions, e.g. line numbers.
        /// </summary>
        public ISet<int> Positions { get; set; }

        /// <summary>
        /// Used to pass properties specific to derived matcher classes.
        /// </summary>
        public IDictionary<string, object> Properties { get { return properties; } }

        int last_error_pos = -1;
        IList<Func<string>> error_msgs = new List<Func<string>>();
        string last_error = null;

        static readonly Regex EXPECTED = new Regex(@"expected\s+(.*)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        /// The error message from the rightmost error encountered while matching.
        /// Is <c>null</c> if there are no errors.
        /// <seealso cref="LastErrorIndex"/>
        /// </summary>
        public string LastError
        {
            get
            {
                if (last_error == null && error_msgs.Count > 0)
                {
                    var strings = error_msgs.Select(f => f()).Distinct();
                    var results = new List<string>();
                    var expected = new List<string>();

                    foreach (var str in strings)
                    {
                        var match = EXPECTED.Match(str);
                        if (match.Success)
                            expected.Add(match.Groups[1].Value);
                        else
                            results.Add(str);
                    }

                    if (expected.Count > 0)
                    {
                        var sb = new StringBuilder();
                        sb.Append("expected ");
                        if (expected.Count > 1)
                        {
                            sb.Append(string.Join(", ", expected.Take(expected.Count - 1).ToArray()));
                            sb.Append(" or ");
                        }
                        sb.Append(expected.Last());

                        results.Add(sb.ToString());
                    }

                    last_error = string.Join(";", results.ToArray());
                }
                return last_error;
            }
        }

        /// <summary>
        /// The position in the input enumerable of the rightmost error encountered while matching.
        /// <seealso cref="LastError"/>
        /// </summary>
        public int LastErrorIndex
        {
            get { return last_error_pos; }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="input">Input to be matched.</param>
        public MatchState(IEnumerable<TInput> input)
        {
            Input = (input is string || input is IList<TInput>) ? input : new Memoizer<TInput>(input);
            Results = new Stack<MatchItem<TInput, TResult>>();
            ArgResults = new Stack<MatchItem<TInput, TResult>>();
            CallStack = new Stack<LRRecord<MatchItem<TInput, TResult>>>();
            Positions = new HashSet<int>();
        }

        #region Memo Methods

        /// <summary>
        /// Allows the memo tables and left-recursion records to be garbage collect.
        /// </summary>
        internal void ClearMemoTable()
        {
            memo_table.Clear();
            current_recursions.Clear();
        }

        /// <summary>
        /// Memoize the result of a production at a given index.
        /// </summary>
        /// <param name="expansion">The production expansion.</param>
        /// <param name="index">The input position.</param>
        /// <param name="item">The result of the parse.</param>
        public void Memoize(Expansion expansion, int index, MatchItem<TInput, TResult> item)
        {
            Dictionary<int, Dictionary<int, MatchItem<TInput, TResult>>> expansion_dict;
            if (!memo_table.TryGetValue(expansion.Name, out expansion_dict))
            {
                expansion_dict = new Dictionary<int, Dictionary<int, MatchItem<TInput, TResult>>>();
                memo_table.Add(expansion.Name, expansion_dict);
            }

            Dictionary<int, MatchItem<TInput, TResult>> rule_dict;
            if (!expansion_dict.TryGetValue(expansion.Num, out rule_dict))
            {
                rule_dict = new Dictionary<int, MatchItem<TInput, TResult>>();
                expansion_dict.Add(expansion.Num, rule_dict);
            }

            rule_dict[index] = item;
        }

        /// <summary>
        /// Forget a memoized result.
        /// </summary>
        /// <param name="expansion">The production expansion.</param>
        /// <param name="index">The input position.</param>
        public void ForgetMemo(Expansion expansion, int index)
        {
            Dictionary<int, Dictionary<int, MatchItem<TInput, TResult>>> expansion_dict;
            if (!memo_table.TryGetValue(expansion.Name, out expansion_dict))
                return;

            Dictionary<int, MatchItem<TInput, TResult>> rule_dict;
            if (expansion_dict.TryGetValue(expansion.Num, out rule_dict))
            {
                rule_dict.Remove(index);
            }
        }

        /// <summary>
        /// Find the memo of a given rule call.
        /// </summary>
        /// <param name="expansion">The production expansion.</param>
        /// <param name="index">The input position.</param>
        /// <param name="item">The result.</param>
        /// <returns>True if there is a memo record for the rule at the index.</returns>
        public bool TryGetMemo(Expansion expansion, int index, out MatchItem<TInput, TResult> item)
        {
            Dictionary<int, Dictionary<int, MatchItem<TInput, TResult>>> expansion_dict;
            Dictionary<int, MatchItem<TInput, TResult>> rule_dict;
            if (memo_table.TryGetValue(expansion.Name, out expansion_dict)
                && expansion_dict.TryGetValue(expansion.Num, out rule_dict)
                && rule_dict.TryGetValue(index, out item))
                return true;

            item = default(MatchItem<TInput, TResult>);
            return false;
        }

        #endregion

        #region Left-Recursion Handling

        /// <summary>
        /// Start a left-recursion record for a rule at a given index.
        /// </summary>
        /// <param name="expansion">The production expansion.</param>
        /// <param name="index">The input position.</param>
        /// <param name="record">The new left-recursion record.</param>
        public void StartLRRecord(Expansion expansion, int index, LRRecord<MatchItem<TInput, TResult>> record)
        {
            Dictionary<int, LRRecord<MatchItem<TInput, TResult>>> record_dict;
            if (!current_recursions.TryGetValue(expansion.Name, out record_dict))
            {
                record_dict = new Dictionary<int, LRRecord<MatchItem<TInput, TResult>>>();
                current_recursions.Add(expansion.Name, record_dict);
            }

            record_dict[index] = record;
        }

        /// <summary>
        /// Discard a left-recursion record for a rule at a given index.
        /// </summary>
        /// <param name="expansion">The production expansion.</param>
        /// <param name="index">The input position.</param>
        public void ForgetLRRecord(Expansion expansion, int index)
        {
            Dictionary<int, LRRecord<MatchItem<TInput, TResult>>> record_dict;
            if (current_recursions.TryGetValue(expansion.Name, out record_dict))
                record_dict.Remove(index);
        }

        /// <summary>
        /// Get the left-recursion record for a rule at a given index.
        /// </summary>
        /// <param name="expansion">The production expansion.</param>
        /// <param name="index">The input position.</param>
        /// <param name="record">The left-recursion record.</param>
        /// <returns>True if there is a left-recursion record for the rule at the index.</returns>
        public bool TryGetLRRecord(Expansion expansion, int index, out LRRecord<MatchItem<TInput, TResult>> record)
        {
            Dictionary<int, LRRecord<MatchItem<TInput, TResult>>> record_dict;
            if (current_recursions.TryGetValue(expansion.Name, out record_dict) 
                && record_dict.TryGetValue(index, out record))
                return true;

            record = null;
            return false;
        }

        #endregion

        #region Error Handling

        /// <summary>
        /// Sets the current error, if it is beyond or equal to the previous error.
        /// </summary>
        /// <param name="pos">Position of the error.</param>
        /// <param name="message">Function to generate the message (deferred until the end for better performance).</param>
        public void AddError(int pos, Func<string> message)
        {
            if (pos > last_error_pos)
            {
                error_msgs.Add(message);
                last_error_pos = pos;
            }
        }

        /// <summary>
        /// Sets the current error if it is beyond or equal to the previous error.
        /// </summary>
        /// <param name="pos">Position of the error.</param>
        /// <param name="message">Message.</param>
        public void AddError(int pos, string message)
        {
            AddError(pos, () => message);
        }

        /// <summary>
        /// Clears all errors for a given position.
        /// </summary>
        public void ClearErrors()
        {            
            error_msgs.Clear();
            last_error_pos = -1;
        }

        #endregion

        #region Utilities

        int[] sortedPositions;

        /// <summary>
        /// Gets the "line" as defined by the Positions array.  This assumes that the positions
        /// mark the beginning of the "lines".
        /// </summary>
        /// <param name="index">The index in question in the input.</param>
        /// <param name="lineNum">The 1-based line number in which the index is found.</param>
        /// <param name="lineOffset">The offset of the index from the beginning of the line.</param>
        /// <returns></returns>
        public IEnumerable<TInput> GetLine(int index, out int lineNum, out int lineOffset)
        {
            if (sortedPositions == null || sortedPositions.Length != Positions.Count)
            {
                if (Positions.Count == 0 || !Positions.Contains(0))
                    Positions.Add(0);

                sortedPositions = Positions.OrderBy(n => n).ToArray();
            }

            int start, next;
            var srch = Array.BinarySearch(sortedPositions, index);
            var srchOffset = sortedPositions[0] == 0 ? 1 : 2;

            // we are on the the beginning of the line
            if (srch >= 0)
            {
                start = sortedPositions[srch];
                if (srch + 1 < sortedPositions.Length)
                    next = sortedPositions[srch + 1];
                else
                    next = int.MaxValue;

                lineNum = srch + srchOffset;
                lineOffset = 0;
            }
            else
            {
                // srch is the index of the next largest position
                srch = ~srch;

                if (srch == 0)
                {
                    start = 0;
                    next = sortedPositions[srch];
                }
                else if (srch < sortedPositions.Length)
                {
                    start = sortedPositions[srch - 1];
                    next = sortedPositions[srch];
                }
                else
                {
                    start = sortedPositions[sortedPositions.Length - 1];
                    next = int.MaxValue;
                }

                lineNum = srch + (srchOffset - 1);
                lineOffset = index - start;
            }

            // get subsequence
            if (InputString != null)
            {
                int li = start;
                while (li < next && li < InputString.Length 
                    && InputString[li] != '\r' && InputString[li] != '\n')
                    ++li;
                return (IEnumerable<TInput>)(object)InputString.Substring(start, li - start);
            }
            else if (InputList != null)
            {
                next = Math.Min(InputList.Count, next - start);
                return InputList.Skip(start).Take(next - start);
            }
            else
            {
                return InputEnumerable
                    .Skip(start)
                    .TakeWhile((item, i) => i + start < next);
            }
        }

        #endregion
    }

    /// <summary>
    /// Records the production name and current expansion for left-recursion handling.
    /// </summary>
    public struct Expansion
    {
        /// <summary>
        /// The name of the production.
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// The current expansion number.
        /// </summary>
        public int Num { get; set; }
    }

    /// <summary>
    /// A record of the current state of left-recursion handling for a rule.
    /// </summary>
    public class LRRecord<TItem>
    {
        /// <summary>
        /// Whether or not left-recursion has been detected for this production.
        /// </summary>
        public bool LRDetected { get; set; }

        /// <summary>
        /// How many expansions we have generated.
        /// </summary>
        public int NumExpansions { get; set; }

        /// <summary>
        /// The current expansion.
        /// </summary>
        public Expansion CurrentExpansion { get; set; }

        /// <summary>
        /// The farthest extent of the match.
        /// </summary>
        public int CurrentNextIndex { get; set; }

        /// <summary>
        /// The result of the last expansion.
        /// </summary>
        public TItem CurrentResult { get; set; }

        /// <summary>
        /// Rules that are involved in expanding this left-recursion.
        /// </summary>
        public ISet<string> InvolvedRules { get; set; }

    }

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
}
