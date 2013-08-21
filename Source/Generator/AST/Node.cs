//////////////////////////////////////////////////////////////////////
//
// Copyright © 2013 Verophyle Informatics
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


namespace IronMeta.AST
{

    using TItem = Matcher.MatchItem<char, Node>;

    /// <summary>
    /// Base class for Abstract Syntax Tree nodes for the IronMeta parser.
    /// </summary>
    public class Node
    {

        /// <summary>
        /// Children of this node.
        /// </summary>
        public List<Node> Children { get; protected set; }

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

    class Fail : Node
    {
        public string Message { get; protected set; }

        public Fail(Node message)
        {
            if (message != null)
                this.Message = message.GetText().Trim();
        }
    }

    class Any : Node
    {
    }

    class Look : Node
    {
        public Node Body { get; protected set; }

        public Look(Node body)
        {
            this.Body = body;

            Children = new List<Node> { body };
        }
    }

    class Not : Node
    {
        public Node Body { get; protected set; }

        public Not(Node body)
        {
            this.Body = body;

            Children = new List<Node> { body };
        }
    }

    class Or : Node
    {
        public Node Left { get; protected set; }
        public Node Right { get; protected set; }

        public Or(Node left, Node right)
        {
            this.Left = left;
            this.Right = right;

            Children = new List<Node> { left, right };
        }
    }

    class And : Node
    {
        public Node Left { get; protected set; }
        public Node Right { get; protected set; }

        public And(Node left, Node right)
        {
            this.Left = left;
            this.Right = right;

            Children = new List<Node> { left, right };
        }
    }

    class Star : Node
    {
        public Node Body { get; protected set; }

        public Star(Node body)
        {
            this.Body = body;

            Children = new List<Node> { body };
        }
    }

    class Plus : Node
    {
        public Node Body { get; protected set; }

        public Plus(Node body)
        {
            this.Body = body;

            Children = new List<Node> { body };
        }
    }

    class Ques : Node
    {
        public Node Body { get; protected set; }

        public Ques(Node body)
        {
            this.Body = body;

            Children = new List<Node> { body };
        }
    }

    class CallOrVar : Node
    {
        public TItem Name { get; protected set; }

        public CallOrVar(TItem name)
        {
            this.Name = name;

            Items = new List<TItem> { name };
        }
    }

    class Call : Node
    {
        public TItem Rule { get; protected set; }
        public IEnumerable<Node> Params { get; protected set; }

        public Call(TItem rule, IEnumerable<Node> parms)
        {
            this.Rule = rule;
            this.Params = parms;

            Items = new List<TItem> { rule };
        }
    }

    class Idfr : Node
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

    class Code : Node
    {
        public TItem Text { get; protected set; }

        public Code(TItem text)
        {
            this.Text = text;

            Items = new List<TItem> { text };
        }
    }

    class InputClass : Node
    {
        public IEnumerable<Node> Inputs { get; protected set; }
        public List<string> Chars { get; protected set; }

        public InputClass(IEnumerable<Node> inputs)
        {
            this.Inputs = inputs;
            this.Chars = new List<string>();
        }
    }

    class ClassRange : Node
    {
        public TItem Item { get; protected set; }
        public IEnumerable<char> Inputs { get; protected set; }

        public ClassRange(TItem item, IEnumerable<char> inputs)
        {
            this.Item = item;
            Items = new List<TItem> { item };

            this.Inputs = inputs;
        }

        public static char GetChar(Node node)
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

    class Bind : Node
    {
        public Node Body { get; protected set; }
        public TItem VarName { get; protected set; }

        public Bind(Node body, TItem varname)
        {
            this.Body = body;
            this.VarName = varname;

            Children = new List<Node> { body };
            Items = new List<TItem> { varname };
        }
    }

    class Cond : Node
    {
        public Node Body { get; protected set; }
        public TItem Expression { get; protected set; }

        public Cond(Node body, TItem exp)
        {
            this.Body = body;
            this.Expression = exp;

            Children = new List<Node> { body };
            Items = new List<TItem> { exp };
        }
    }

    class Act : Node
    {
        public Node Body { get; protected set; }
        public TItem Expression { get; protected set; }

        public Act(Node body, TItem exp)
        {
            this.Body = body;
            this.Expression = exp;

            Children = new List<Node> { body };
            Items = new List<TItem> { exp };
        }
    }

    class Args : Node
    {
        public Node Parms { get; protected set; }
        public Node Body { get; protected set; }

        public Args(Node parms, Node body)
        {
            this.Parms = parms;
            this.Body = body;

            Children = new List<Node> { body };
        }
    }

    class Rule : Node
    {
        public TItem Name { get; protected set; }
        public Node Body { get; protected set; }
        public string Override { get; protected set; }

        public Rule(TItem name, Node body, string ovr)
        {
            this.Name = name;
            this.Body = body;
            this.Override = ovr;

            Items = new List<TItem> { name };
            Children = new List<Node> { body };
        }
    }

    class Grammar : Node
    {
        public TItem Name { get; protected set; }
        public TItem TInput { get; protected set; }
        public TItem TResult { get; protected set; }
        public TItem Base { get; protected set; }
        public IEnumerable<Rule> Rules { get; protected set; }

        public Grammar(TItem name, TItem tinput, TItem tresult, TItem baseclass, IEnumerable<Node> rules)
        {
            this.Name = name;
            this.TInput = tinput;
            this.TResult = tresult;
            this.Base = baseclass;
            this.Rules = rules.Cast<Rule>();

            Items = new List<TItem> { name, tinput, tresult, baseclass };
            Children = new List<Node>(rules);
        }
    }

    class Using : Node
    {
        public TItem Name { get; protected set; }

        public Using(TItem name)
        {
            this.Name = name;

            Items = new List<TItem> { name };
        }
    }

    class Preamble : Node
    {
        public IEnumerable<Using> Usings { get; protected set; }

        public Preamble(IEnumerable<Node> usings)
        {
            this.Usings = usings.Cast<Using>();
        }
    }

    class GrammarFile : Node
    {
        public Preamble Preamble { get; protected set; }
        public Grammar Grammar { get; protected set; }

        public GrammarFile(Node preamble, Node grammar)
        {
            this.Preamble = preamble as Preamble;
            this.Grammar = grammar as Grammar;

            Children = new List<Node> { grammar };
        }
    }

} // namespace IronMeta.Generator.AST
