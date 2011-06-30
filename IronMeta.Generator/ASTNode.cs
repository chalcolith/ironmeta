//////////////////////////////////////////////////////////////////////
// $Id$
//
// Copyright (C) 2009-2011, The IronMeta Project
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
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace IronMeta.Generator.AST
{

    /// <summary>
    /// Base class for Abstract Syntax Tree nodes for the IronMeta parser.
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public class ASTNode<TItem>
        where TItem : IronMeta.Matcher.MatchItem<char, ASTNode<TItem>, TItem>
    {
        /// <summary>
        /// Children of this node.
        /// </summary>
        public List<ASTNode<TItem>> Children { get; protected set; }

        /// <summary>
        /// Match results for this node.
        /// </summary>
        public List<TItem> Items { get; protected set; }

        /// <summary>
        /// Get the text that this node covers.
        /// </summary>
        public string GetText()
        {
            StringBuilder sb = new StringBuilder();
            if (Items != null)
            {
                foreach (TItem item in Items)
                {
                    string input_string = item.InputEnumerable as string;
                    if (input_string != null)
                    {
                        sb.Append(input_string.Substring(item.StartIndex, item.NextIndex - item.StartIndex));
                    }
                    else
                    {
                        foreach (char ch in item.Inputs)
                            sb.Append(ch);
                    }
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Get the text covered by the match item.
        /// </summary>
        /// <param name="item">Match item.</param>
        public string GetText(TItem item)
        {
            StringBuilder sb = new StringBuilder();
            if (item != null)
            {
                string input_string = item.InputEnumerable as string;
                if (input_string != null)
                {
                    sb.Append(input_string.Substring(item.StartIndex, item.NextIndex - item.StartIndex));
                }
                else
                {
                    foreach (char ch in item.Inputs)
                        sb.Append(ch);
                }
            }
            return sb.ToString();
        }
    }

    class Fail<TItem> : ASTNode<TItem>
        where TItem : IronMeta.Matcher.MatchItem<char, ASTNode<TItem>, TItem>
    {
        public string Message { get; protected set; }

        public Fail(ASTNode<TItem> message)
        {
            if (message != null)
                this.Message = message.GetText().Trim();
        }
    }

    class Any<TItem> : ASTNode<TItem>
        where TItem : IronMeta.Matcher.MatchItem<char, ASTNode<TItem>, TItem>
    {
    }

    class Look<TItem> : ASTNode<TItem> 
        where TItem : IronMeta.Matcher.MatchItem<char, ASTNode<TItem>, TItem>
    {
        public ASTNode<TItem> Body { get; protected set; }

        public Look(ASTNode<TItem> body)
        {
            this.Body = body;

            Children = new List<ASTNode<TItem>> { body };
        }
    }

    class Not<TItem> : ASTNode<TItem>
        where TItem : IronMeta.Matcher.MatchItem<char, ASTNode<TItem>, TItem>
    {
        public ASTNode<TItem> Body { get; protected set; }

        public Not(ASTNode<TItem> body)
        {
            this.Body = body;

            Children = new List<ASTNode<TItem>> { body };
        }
    }

    class Or<TItem> : ASTNode<TItem>
        where TItem : IronMeta.Matcher.MatchItem<char, ASTNode<TItem>, TItem>
    {
        public ASTNode<TItem> Left { get; protected set; }
        public ASTNode<TItem> Right { get; protected set; }

        public Or(ASTNode<TItem> left, ASTNode<TItem> right)
        {
            this.Left = left;
            this.Right = right;

            Children = new List<ASTNode<TItem>> { left, right };
        }
    }

    class And<TItem> : ASTNode<TItem>
        where TItem : IronMeta.Matcher.MatchItem<char, ASTNode<TItem>, TItem>
    {
        public ASTNode<TItem> Left { get; protected set; }
        public ASTNode<TItem> Right { get; protected set; }

        public And(ASTNode<TItem> left, ASTNode<TItem> right)
        {
            this.Left = left;
            this.Right = right;

            Children = new List<ASTNode<TItem>> { left, right };
        }
    }

    class Star<TItem> : ASTNode<TItem>
        where TItem : IronMeta.Matcher.MatchItem<char, ASTNode<TItem>, TItem>
    {
        public ASTNode<TItem> Body { get; protected set; }

        public Star(ASTNode<TItem> body)
        {
            this.Body = body;

            Children = new List<ASTNode<TItem>> { body };
        }
    }

    class Plus<TItem> : ASTNode<TItem>
        where TItem : IronMeta.Matcher.MatchItem<char, ASTNode<TItem>, TItem>
    {
        public ASTNode<TItem> Body { get; protected set; }

        public Plus(ASTNode<TItem> body)
        {
            this.Body = body;

            Children = new List<ASTNode<TItem>> { body };
        }
    }

    class Ques<TItem> : ASTNode<TItem>
        where TItem : IronMeta.Matcher.MatchItem<char, ASTNode<TItem>, TItem>
    {
        public ASTNode<TItem> Body { get; protected set; }

        public Ques(ASTNode<TItem> body)
        {
            this.Body = body;

            Children = new List<ASTNode<TItem>> { body };
        }
    }

    class CallOrVar<TItem> : ASTNode<TItem>
        where TItem : IronMeta.Matcher.MatchItem<char, ASTNode<TItem>, TItem>
    {
        public TItem Name { get; protected set; }

        public CallOrVar(TItem name)
        {
            this.Name = name;

            Items = new List<TItem> { name };
        }
    }

    class Call<TItem> : ASTNode<TItem>
        where TItem : IronMeta.Matcher.MatchItem<char, ASTNode<TItem>, TItem>
    {
        public TItem Rule { get; protected set; }
        public IEnumerable<ASTNode<TItem>> Params { get; protected set; }

        public Call(TItem rule, IEnumerable<ASTNode<TItem>> parms)
        {
            this.Rule = rule;
            this.Params = parms;

            Items = new List<TItem> { rule };
        }
    }

    class Idfr<TItem> : ASTNode<TItem>
        where TItem : IronMeta.Matcher.MatchItem<char, ASTNode<TItem>, TItem>
    {
        public TItem Text { get; protected set; }
        public TItem GenericParams { get; protected set; }

        public Idfr(TItem text)
        {
            this.Text = text;

            Items = new List<TItem> { text };
        }

        public Idfr(TItem text, TItem gparams)
        {
            this.Text = text;
            this.GenericParams = gparams;

            Items = new List<TItem> { text, gparams };
        }
    }

    class Code<TItem> : ASTNode<TItem>
        where TItem : IronMeta.Matcher.MatchItem<char, ASTNode<TItem>, TItem>
    {
        public TItem Text { get; protected set; }

        public Code(TItem text)
        {
            this.Text = text;

            Items = new List<TItem> { text };
        }
    }

    class InputClass<TItem> : ASTNode<TItem>
        where TItem : IronMeta.Matcher.MatchItem<char, ASTNode<TItem>, TItem>
    {
        public IEnumerable<ASTNode<TItem>> Inputs { get; protected set; }
        public List<string> Chars { get; protected set; }

        public InputClass(IEnumerable<ASTNode<TItem>> inputs)
        {
            this.Inputs = inputs;
            this.Chars = new List<string>();
        }
    }

    class ClassRange<TItem> : ASTNode<TItem>
        where TItem : IronMeta.Matcher.MatchItem<char, ASTNode<TItem>, TItem>
    {
        public TItem Item { get; protected set; }
        public IEnumerable<char> Inputs { get; protected set; }

        public ClassRange(TItem item, IEnumerable<char> inputs)
        {
            this.Item = item;
            Items = new List<TItem> { item };

            this.Inputs = inputs;
        }

        public static char GetChar(ASTNode<TItem> node)
        {
            if (node != null)
            {
                string str = node.GetText().Trim('\'').Trim();
                str = Regex.Unescape(str);
                foreach (char ch in str)
                    return ch;
            }
            return (char)0;
        }
    }

    class Bind<TItem> : ASTNode<TItem>
        where TItem : IronMeta.Matcher.MatchItem<char, ASTNode<TItem>, TItem>
    {
        public ASTNode<TItem> Body { get; protected set; }
        public TItem VarName { get; protected set; }

        public Bind(ASTNode<TItem> body, TItem varname)
        {
            this.Body = body;
            this.VarName = varname;

            Children = new List<ASTNode<TItem>> { body };
            Items = new List<TItem> { varname };
        }
    }

    class Cond<TItem> : ASTNode<TItem>
        where TItem : IronMeta.Matcher.MatchItem<char, ASTNode<TItem>, TItem>
    {
        public ASTNode<TItem> Body { get; protected set; }
        public TItem Expression { get; protected set; }

        public Cond(ASTNode<TItem> body, TItem exp)
        {
            this.Body = body;
            this.Expression = exp;

            Children = new List<ASTNode<TItem>> { body };
            Items = new List<TItem> { exp };
        }
    }

    class Act<TItem> : ASTNode<TItem>
        where TItem : IronMeta.Matcher.MatchItem<char, ASTNode<TItem>, TItem>
    {
        public ASTNode<TItem> Body { get; protected set; }
        public TItem Expression { get; protected set; }

        public Act(ASTNode<TItem> body, TItem exp)
        {
            this.Body = body;
            this.Expression = exp;

            Children = new List<ASTNode<TItem>> { body };
            Items = new List<TItem> { exp };
        }
    }

    class Args<TItem> : ASTNode<TItem>
        where TItem : IronMeta.Matcher.MatchItem<char, ASTNode<TItem>, TItem>
    {
        public ASTNode<TItem> Parms { get; protected set; }
        public ASTNode<TItem> Body { get; protected set; }

        public Args(ASTNode<TItem> parms, ASTNode<TItem> body)
        {
            this.Parms = parms;
            this.Body = body;

            Children = new List<ASTNode<TItem>> { body };
        }
    }

    class Rule<TItem> : ASTNode<TItem>
        where TItem : IronMeta.Matcher.MatchItem<char, ASTNode<TItem>, TItem>
    {
        public TItem Name { get; protected set; }
        public ASTNode<TItem> Body { get; protected set; }
        public bool Override { get; protected set; }

        public Rule(TItem name, ASTNode<TItem> body, bool ovr)
        {
            this.Name = name;
            this.Body = body;
            this.Override = ovr;

            Items = new List<TItem> { name };
            Children = new List<ASTNode<TItem>> { body };
        }
    }

    class Grammar<TItem> : ASTNode<TItem>
        where TItem : IronMeta.Matcher.MatchItem<char, ASTNode<TItem>, TItem>
    {
        public TItem Name { get; protected set; }
        public TItem TInput { get; protected set; }
        public TItem TResult { get; protected set; }
        public TItem Base { get; protected set; }
        public IEnumerable<Rule<TItem>> Rules { get; protected set; }

        public Grammar(TItem name, TItem tinput, TItem tresult, TItem baseclass, IEnumerable<ASTNode<TItem>> rules)
        {
            this.Name = name;
            this.TInput = tinput;
            this.TResult = tresult;
            this.Base = baseclass;
            this.Rules = rules.Cast<Rule<TItem>>();

            Items = new List<TItem> { name, tinput, tresult, baseclass };
            Children = new List<ASTNode<TItem>>(rules);
        }
    }

    class Using<TItem> : ASTNode<TItem>
        where TItem : IronMeta.Matcher.MatchItem<char, ASTNode<TItem>, TItem>
    {
        public TItem Name { get; protected set; }

        public Using(TItem name)
        {
            this.Name = name;

            Items = new List<TItem> { name };
        }
    }

    class Preamble<TItem> : ASTNode<TItem>
        where TItem : IronMeta.Matcher.MatchItem<char, ASTNode<TItem>, TItem>
    {
        public IEnumerable<Using<TItem>> Usings { get; protected set; }

        public Preamble(IEnumerable<ASTNode<TItem>> usings)
        {
            this.Usings = usings.Cast<Using<TItem>>();
        }
    }

    class GrammarFile<TItem> : ASTNode<TItem>
        where TItem : IronMeta.Matcher.MatchItem<char, ASTNode<TItem>, TItem>
    {
        public Preamble<TItem> Preamble { get; protected set; }
        public Grammar<TItem> Grammar { get; protected set; }

        public GrammarFile(ASTNode<TItem> preamble, ASTNode<TItem> grammar)
        {
            this.Preamble = preamble as Preamble<TItem>;
            this.Grammar = grammar as Grammar<TItem>;

            Children = new List<ASTNode<TItem>> { grammar };
        }
    }

} // namespace IronMeta.Generator.AST
