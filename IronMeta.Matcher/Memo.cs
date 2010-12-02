//////////////////////////////////////////////////////////////////////
// $Id: MatchResult.cs 125 2010-11-10 23:45:07Z kulibali $
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

using System.Collections.Generic;

namespace IronMeta.Matcher
{

    /// <summary>
    /// Holds memoization and left-recursion state during a parse.
    /// </summary>
    /// <typeparam name="TInput">The input type.</typeparam>
    /// <typeparam name="TResult">The result type.</typeparam>
    /// <typeparam name="TItem">The (internal) item type.</typeparam>
    public class Memo<TInput, TResult, TItem>
    {
        Dictionary<string, Dictionary<int,TItem>> table = new Dictionary<string, Dictionary<int, TItem>>();

        /// <summary>
        /// Memoize the result of a production at a given index.
        /// </summary>
        /// <param name="rule">The production name.</param>
        /// <param name="index">The input position.</param>
        /// <param name="item">The result of the parse.</param>
        public void Memoize(string rule, int index, TItem item)
        {
            Dictionary<int, TItem> ruleDict;
            if (!table.TryGetValue(rule, out ruleDict))
            {
                ruleDict = new Dictionary<int, TItem>();
                table.Add(rule, ruleDict);
            }

            ruleDict[index] = item;
        }

        /// <summary>
        /// Forget a memoized result.
        /// </summary>
        /// <param name="rule">The production name.</param>
        /// <param name="index">The input position.</param>
        public void ForgetMemo(string rule, int index)
        {
            Dictionary<int, TItem> ruleDict;
            if (table.TryGetValue(rule, out ruleDict))
            {
                ruleDict.Remove(index);
            }
        }

        /// <summary>
        /// Find the memo of a given rule call.
        /// </summary>
        /// <param name="rule">The name of the rule.</param>
        /// <param name="index">The input position.</param>
        /// <param name="item">The result.</param>
        /// <returns>True if there is a memo record for the rule at the index.</returns>
        public bool TryGetMemo(string rule, int index, out TItem item)
        {
            Dictionary<int, TItem> ruleDict;
            if (table.TryGetValue(rule, out ruleDict) && ruleDict.TryGetValue(index, out item))
            {
                return true;
            }
            else
            {
                item = default(TItem);
                return false;
            }
        }

        Dictionary<string, Dictionary<int, LRRecord<TItem>>> currentRecursions = new Dictionary<string, Dictionary<int, LRRecord<TItem>>>();

        /// <summary>
        /// Start a left-recursion record for a rule at a given index.
        /// </summary>
        /// <param name="rule">The name of the rule.</param>
        /// <param name="index">The input position.</param>
        /// <param name="record">The new left-recursion record.</param>
        public void StartLRRecord(string rule, int index, LRRecord<TItem> record)
        {
            Dictionary<int, LRRecord<TItem>> recordDict;
            if (!currentRecursions.TryGetValue(rule, out recordDict))
            {
                recordDict = new Dictionary<int, LRRecord<TItem>>();
                currentRecursions.Add(rule, recordDict);
            }

            recordDict[index] = record;
        }

        /// <summary>
        /// Discard a left-recursion record for a rule at a given index.
        /// </summary>
        /// <param name="rule">The name of the rule.</param>
        /// <param name="index">The input position.</param>
        public void ForgetLRRecord(string rule, int index)
        {
            Dictionary<int, LRRecord<TItem>> recordDict;
            if (currentRecursions.TryGetValue(rule, out recordDict))
                recordDict.Remove(index);
        }

        /// <summary>
        /// Get the left-recursion record for a rule at a given index.
        /// </summary>
        /// <param name="rule">The name of the rule.</param>
        /// <param name="index">The input position.</param>
        /// <param name="record">The left-recursion record.</param>
        /// <returns>True if there is a left-recursion record for the rule at the index.</returns>
        public bool TryGetLRRecord(string rule, int index, out LRRecord<TItem> record)
        {
            Dictionary<int, LRRecord<TItem>> recordDict;
            if (currentRecursions.TryGetValue(rule, out recordDict) && recordDict.TryGetValue(index, out record))
                return true;

            record = null;
            return false;
        }

    } // class Memo

    /// <summary>
    /// A record of the current state of left-recursion handling for a rule.
    /// </summary>
    public class LRRecord<TItem>
    {

        public bool LRDetected { get; set; }

        /// <summary>
        /// How many expansions we have generated.
        /// </summary>
        public int NumExpansions { get; set; }

        /// <summary>
        /// The name of the current expansion.
        /// </summary>
        public string CurrentExpansion { get; set; }

        /// <summary>
        /// The farthest extent of the match.
        /// </summary>
        public int CurrentNextIndex { get; set; }

        /// <summary>
        /// The result of the last expansion.
        /// </summary>
        public TItem CurrentResult { get; set; }

    } // class LRRecord

} // namespace IronMeta.Matcher
