// IronMeta Copyright © Gordon Tisher 2018

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace IronMeta.Generator.AST
{
    using Verophyle.Regexp;
    using TItem = Matcher.MatchItem<char, AstNode>;

    /// <summary>
    /// Base class for Abstract Syntax Tree nodes for the IronMeta parser.
    /// </summary>
    public class AstNode
    {
        /// <summary>
        /// Children of this node.
        /// </summary>
        public List<AstNode> Children { get; protected set; }

        /// <summary>
        /// Match results for this node.
        /// </summary>
        public List<TItem> Items { get; protected set; }

        /// <summary>
        /// Get the text that this node covers.
        /// </summary>
        public virtual string GetText()
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

        public IEnumerable<AstNode> GetAllChildren()
        {
            return Children != null
                ? Children.SelectMany(child => new[] { child }.Concat(child.GetAllChildren()))
                : Enumerable.Empty<AstNode>();
        }

        public IEnumerable<T> OfType<T>() where T : AstNode
        {
            var t = this as T;
            if (t != null) yield return t;
            if (Children != null)
            {
                foreach (var child in Children.OfType<T>())
                    yield return child;
            }
        }
    }

    class Fail : AstNode
    {
        public string Message { get; protected set; }

        public Fail(AstNode message)
        {
            if (message != null)
                this.Message = message.GetText().Trim();
        }
    }

    class Any : AstNode
    {
    }

    class Look : AstNode
    {
        public AstNode Body { get; protected set; }

        public Look(AstNode body)
        {
            this.Body = body;

            Children = new List<AstNode> { body };
        }
    }

    class Not : AstNode
    {
        public AstNode Body { get; protected set; }

        public Not(AstNode body)
        {
            this.Body = body;

            Children = new List<AstNode> { body };
        }
    }

    class Or : AstNode
    {
        public AstNode Left { get; protected set; }
        public AstNode Right { get; protected set; }

        public Or(AstNode left, AstNode right)
        {
            this.Left = left;
            this.Right = right;

            Children = new List<AstNode> { left, right };
        }
    }

    class And : AstNode
    {
        public AstNode Left { get; protected set; }
        public AstNode Right { get; protected set; }

        public And(AstNode left, AstNode right)
        {
            this.Left = left;
            this.Right = right;

            Children = new List<AstNode> { left, right };
        }
    }

    class Star : AstNode
    {
        public AstNode Body { get; protected set; }

        public Star(AstNode body)
        {
            this.Body = body;

            Children = new List<AstNode> { body };
        }
    }

    class Plus : AstNode
    {
        public AstNode Body { get; protected set; }

        public Plus(AstNode body)
        {
            this.Body = body;

            Children = new List<AstNode> { body };
        }
    }

    class Ques : AstNode
    {
        public AstNode Body { get; protected set; }

        public Ques(AstNode body)
        {
            this.Body = body;

            Children = new List<AstNode> { body };
        }
    }

    class CallOrVar : AstNode
    {
        public TItem Name { get; protected set; }

        public CallOrVar(TItem name)
        {
            this.Name = name;

            Items = new List<TItem> { name };
        }
    }

    class Call : AstNode
    {
        public TItem Rule { get; protected set; }
        public IEnumerable<AstNode> Params { get; set; }
        public IEnumerable<TItem> Captures { get; set; }

        public Call(TItem rule, IEnumerable<AstNode> parms)
        {
            this.Rule = rule;
            this.Params = parms;

            Items = new List<TItem> { rule };
        }
    }

    class Idfr : AstNode
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

    class Code : AstNode
    {
        public TItem Text { get; protected set; }

        public Code(TItem text)
        {
            this.Text = text;

            Items = new List<TItem> { text };
        }
    }

    class Regexp : AstNode
    {
        StringRegexp regexp;

        public TItem Text { get; protected set; }

        public StringRegexp Re
        {
            get
            {
                if (regexp == null)
                    regexp = new StringRegexp(this.GetText().Trim('/'));
                return regexp;
            }
        }

        public bool IsValid
        {
            get
            {
                try
                {
                    return Re != null;
                }
                catch
                {
                    return false;
                }
            }
        }

        public Regexp(TItem text)
        {
            this.Text = text;
            Items = new List<TItem> { text };
        }
    }

    class InputClass : AstNode
    {
        public IEnumerable<AstNode> Inputs { get; protected set; }
        public List<string> Chars { get; protected set; }

        public InputClass(IEnumerable<AstNode> inputs)
        {
            this.Inputs = inputs;
            this.Chars = new List<string>();
        }
    }

    class ClassRange : AstNode
    {
        public TItem Item { get; protected set; }
        public IEnumerable<char> Inputs { get; protected set; }

        public ClassRange(TItem item, IEnumerable<char> inputs)
        {
            this.Item = item;
            Items = new List<TItem> { item };

            this.Inputs = inputs;
        }

        public static char GetChar(AstNode node)
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

    class Bind : AstNode
    {
        public AstNode Body { get; protected set; }
        public TItem VarName { get; protected set; }

        public Bind(AstNode body, TItem varname)
        {
            this.Body = body;
            this.VarName = varname;

            Children = new List<AstNode> { body };
            Items = new List<TItem> { varname };
        }
    }

    class Cond : AstNode
    {
        public AstNode Body { get; protected set; }
        public TItem Expression { get; protected set; }

        public Cond(AstNode body, TItem exp)
        {
            this.Body = body;
            this.Expression = exp;

            Children = new List<AstNode> { body };
            Items = new List<TItem> { exp };
        }
    }

    class Act : AstNode
    {
        public AstNode Body { get; protected set; }
        public TItem Expression { get; protected set; }

        public Act(AstNode body, TItem exp)
        {
            this.Body = body;
            this.Expression = exp;

            Children = new List<AstNode> { body };
            Items = new List<TItem> { exp };
        }
    }

    class Args : AstNode
    {
        public AstNode Parms { get; protected set; }
        public AstNode Body { get; protected set; }

        public Args(AstNode parms, AstNode body)
        {
            this.Parms = parms;
            this.Body = body;

            Children = new List<AstNode> { body };
        }
    }

    class Rule : AstNode
    {
        string name = null;

        public TItem Name { get; protected set; }
        public AstNode Body { get; protected set; }
        public string Override { get; protected set; }

        public Rule(TItem name, AstNode body, string ovr)
        {
            this.Name = name;
            this.Body = body;
            this.Override = ovr;

            Items = new List<TItem> { name };
            Children = new List<AstNode> { body };
        }

        internal Rule(string name, AstNode body)
        {
            this.name = name;
            this.Body = body;

            Children = new List<AstNode> { body };
        }

        public string GetName()
        {
            return name ?? (name = this.GetText().Trim());
        }
    }

    class Grammar : AstNode
    {
        public TItem Name { get; protected set; }
        public TItem TInput { get; protected set; }
        public TItem TResult { get; protected set; }
        public TItem Base { get; protected set; }
        public IEnumerable<Rule> Rules { get; protected set; }

        public Grammar(TItem name, TItem tinput, TItem tresult, TItem baseclass, IEnumerable<AstNode> rules)
        {
            this.Name = name;
            this.TInput = tinput;
            this.TResult = tresult;
            this.Base = baseclass;
            this.Rules = rules.Cast<Rule>();

            Items = new List<TItem> { name, tinput, tresult, baseclass };
            Children = new List<AstNode>(rules);
        }
    }

    class Using : AstNode
    {
        public TItem Name { get; protected set; }

        public Using(TItem name)
        {
            this.Name = name;

            Items = new List<TItem> { name };
        }
    }

    class Preamble : AstNode
    {
        public IEnumerable<Using> Usings { get; protected set; }

        public Preamble(IEnumerable<AstNode> usings)
        {
            this.Usings = usings.Cast<Using>();
        }
    }

    class GrammarFile : AstNode
    {
        public Preamble Preamble { get; protected set; }
        public Grammar Grammar { get; protected set; }

        public GrammarFile(AstNode preamble, AstNode grammar)
        {
            this.Preamble = preamble as Preamble;
            this.Grammar = grammar as Grammar;

            Children = new List<AstNode> { grammar };
        }
    }
}
