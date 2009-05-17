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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronMeta
{

    /// <summary>
    /// An exception class for errors during parsing.
    /// </summary>
    public class ParseException : Exception
    {
        public int Index { get; set; }

        public ParseException(int index, string message)
            : base(message)
        {
            this.Index = index;
        }
    }

    public class SemanticException : ParseException
    {
        public SemanticException(int index, string message)
            : base(index, message)
        {
        }
    }

    public class SyntaxException : ParseException
    {
        public SyntaxException(int index, string message)
            : base(index, message)
        {
        }
    }

    /// <summary>
    /// Holds information used for code generation.
    /// </summary>
    public class GenerateInfo
    {
        public string InputFile { get; set; }

        /// <summary>Needs to be set before analysis.</summary>
        public IronMetaMatcher Matcher { get; set; }

        /// <summary>Needs to be set before analysis.</summary>
        public string NameSpace { get; set; }

        /// <summary>Needs to be set before individual rules are analyzed.</summary>
        public HashSet<string> RuleNames { get; set; }
        
        /// <summary>Used by individual rules to temporarily store their variables during analysis.</summary>
        public HashSet<string> VariableNames { get; set; }

        public string InputType { get; set; }
        public string ResultType { get; set; }
        public string BaseClass { get; set; }

        /// <summary>Needs to be set before generation.</summary>
        public string MatchItemClass { get; set; }

        public GenerateInfo(string inputFile, IronMetaMatcher matcher, string nameSpace)
        {
            InputFile = inputFile;
            Matcher = matcher;
            NameSpace = nameSpace;
            RuleNames = new HashSet<string>();
            MatchItemClass = "!~ MatchItemClass ~!";
        }
    }

    /// <summary>
    /// Base class for IronMeta AST nodes.
    /// </summary>
    public class SyntaxNode
    {
        protected string text;

        IEnumerable<SyntaxNode> children = null;
        protected int index;
        int lineNumber, lineOffset;

        public string Text { get { return text != null ? text : ""; } }
        
        public IEnumerable<SyntaxNode> Children
        {
            get
            {
                if (children == null)
                    return Enumerable.Empty<SyntaxNode>();
                else
                    return children;
            }

            internal set
            {
                children = value;
            }
        }

        public int LineNumber { get { return lineNumber; } }
        public int LineOffset { get { return lineOffset; } }

        public SyntaxNode()
        {
            this.index = -1;
        }

        public SyntaxNode(int index)
        {
            this.index = index;
        }

        public virtual void Analyze(GenerateInfo info)
        {
            foreach (SyntaxNode child in Children)
                child.Analyze(info);
        }

        public virtual string Generate(int indent, GenerateInfo info)
        {
            return text ?? "<?>";
        }

        public override string ToString()
        {
            return this.GetType().Name;
        }

        public void AssignLineNumbers(CharacterMatcher<SyntaxNode> matcher)
        {
            lineNumber = matcher.GetLineNumber(index, out lineOffset);

            foreach (SyntaxNode child in Children)
                child.AssignLineNumbers(matcher);
        }

        protected string Indent(int indent, string format, params object[] args)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < indent; ++i)
                sb.Append("    ");
            sb.Append(string.Format(format, args));
            return sb.ToString();
        }

        protected string SingleIndent(int indent)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < indent; ++i)
                sb.Append(" ");
            return sb.ToString();
        }
    }

    /// <summary>
    /// Holds a string from the input file.
    /// </summary>
    public class TokenNode : SyntaxNode
    {
        public enum TokenType
        {
            None = 0,
            EOF,
            EOL,
            WHITESPACE,
            KEYWORD,
            COMMENT,
            IDENTIFIER
        }

        public TokenType Type { get; set; }

        public TokenNode(int index, TokenType type)
            : base(index)
        {
            this.Type = type;
        }

        public override string Generate(int indent, GenerateInfo info)
        {
            return "<" + Type.ToString() + ">";
        }
    }

    /// <summary>
    /// Holds tokens that are keywords.
    /// </summary>
    public class KeywordNode : TokenNode
    {
        public KeywordNode(int index, string text)
            : base(index, TokenType.KEYWORD)
        {
            this.text = text;
        }

        public override string Generate(int indent, GenerateInfo info)
        {
            return text;
        }
    }

    /// <summary>
    /// Holds an identifier.
    /// </summary>
    public class IdentifierNode : TokenNode
    {
        public string Name { get; internal set; }
        public List<string> Qualifiers { get; internal set; }
        public List<string> Parameters { get; internal set; }

        public IdentifierNode(int index, string name)
            : base(index, TokenType.IDENTIFIER)
        {
            Name = name;
            Qualifiers = new List<string>();
            Parameters = new List<string>();

            this.text = GetText();
        }

        public IdentifierNode(int index, string name, List<string> qualifiers, List<string> parameters)
            : base(index, TokenType.IDENTIFIER)
        {
            Name = name;
            Qualifiers = qualifiers ?? new List<string>();
            Parameters = parameters ?? new List<string>();

            this.text = GetText();
        }

        private string GetText()
        {
            StringBuilder sb = new StringBuilder();

            foreach (string qual in Qualifiers)
                sb.AppendFormat("{0}.", qual);

            sb.Append(Name);

            if (Parameters.Count > 0)
                sb.AppendFormat("<{0}>", string.Join(", ", Parameters.ToArray()));

            return sb.ToString();
        }
    }

    /// <summary>
    /// Holds code comments.
    /// </summary>
    public class CommentNode : TokenNode
    {
        public CommentNode(int index, string text)
            : base(index, TokenType.COMMENT)
        {
            this.text = text;
        }
    }

    /// <summary>
    /// Holds a span of whitespace.
    /// </summary>
    public class SpacingNode : SyntaxNode
    {
        public SpacingNode(int index, List<SyntaxNode> children)
            : base(index)
        {
            Children = children.Where(child => child != null);
        }

        public override string Generate(int indent, GenerateInfo info)
        {
            StringBuilder sb = new StringBuilder();

            foreach (SyntaxNode child in Children)
            {
                if (child is CommentNode)
                    sb.AppendLine(Indent(indent, "{0}", child.Generate(indent, info)));
            }

            return sb.ToString();
        }
    }

    /// <summary>
    /// Holds a section of literal C# code.
    /// </summary>
    public class CSharpNode : SyntaxNode
    {
        public CSharpNode(int index, string text)
            : base(index)
        {
            this.text = text;
        }
    }

    /// <summary>
    /// Base class for expression terms.
    /// </summary>
    public abstract class ExpNode : SyntaxNode
    {
        public ExpNode(int index)
            : base(index)
        {
        }
    }


    /// <summary>
    /// Holds a CSharp code literal for use as a term in the match.
    /// </summary>
    public class LiteralExpNode : ExpNode
    {
        public LiteralExpNode(int index, List<SyntaxNode> children)
            : base(index)
        {
            this.text = string.Join("", children.Select(child => child.Text).ToArray());
        }

        public override string Generate(int indent, GenerateInfo info)
        {
            return string.Format("_LITERAL({0})", text);
        }
    }

    /// <summary>
    /// Holds a bare identifier that could be either a variable or a rule call.
    /// </summary>
    public class CallOrVarExpNode : ExpNode
    {
        public CallOrVarExpNode(int index, SyntaxNode identifier)
            : base(index)
        {
            this.text = identifier.Text;
        }

        public override void Analyze(GenerateInfo info)
        {
            if (text.Contains('.'))
            {
                if (text.StartsWith("base."))
                    text = text.Substring(5);
                info.RuleNames.Add(text);
            }
            else
            {
                info.VariableNames.Add(text);
            }

            base.Analyze(info);
        }

        public override string Generate(int indent, GenerateInfo info)
        {
            if (info.RuleNames.Contains(text))
                return string.Format("_CALL({0})", text);
            else
                return string.Format("_REF({0}, \"{0}\", this)", text);
        }
    }

    /// <summary>
    /// Holds a rule call with a parameter list.
    /// </summary>
    public class RuleCallExpNode : ExpNode
    {
        public string RuleName { get; internal set; }
        public List<SyntaxNode> Parameters { get; internal set; }

        public RuleCallExpNode(int index, string name, List<SyntaxNode> parameters)
            : base(index)
        {
            this.RuleName = name;
            this.Parameters = parameters;

            Children = parameters;
        }

        public override void Analyze(GenerateInfo info)
        {
            if (RuleName.StartsWith("base."))
                RuleName = RuleName.Substring(5);

            base.Analyze(info);
        }

        public override string Generate(int indent, GenerateInfo info)
        {
            if (Parameters != null && Parameters.Count > 0)
            {
                var pList = new List<string>();
                foreach (var p in Parameters)
                {
                    if (info.RuleNames.Contains(p.Text))
                        pList.Add(string.Format("new MatchItem({0})", p.Text));
                    else if (info.VariableNames.Contains(p.Text))
                        pList.Add(p.Text);
                    else
                        pList.Add(string.Format("new MatchItem({0}, CONV)", p.Text));
                }

                return string.Format("_CALL({0}, new List<MatchItem> {{ {1} }})", RuleName, string.Join(", ", pList.ToArray()));
            }
            else
            {
                return string.Format("_CALL({0})", RuleName);
            }
        }
    }

    /// <summary>
    /// Holds the "dot" term.
    /// </summary>
    public class AnyExpNode : ExpNode
    {
        public AnyExpNode(int index)
            : base(index)
        {
        }

        public override string Generate(int indent, GenerateInfo info)
        {
            return "_ANY()";
        }
    }

    /// <summary>
    /// Holds a QUES, PLUS or STAR expression.
    /// </summary>
    public class PostfixedExpNode : ExpNode
    {
        string op;

        public PostfixedExpNode(int index, SyntaxNode child, string op)
            : base(index)
        {
            this.op = op;
            Children = new List<SyntaxNode> { child };
        }

        public override string Generate(int indent, GenerateInfo info)
        {
            return string.Format("_{0}({1})", op, Children.First().Generate(indent, info));
        }
    }

    /// <summary>
    /// Holds an AND or NOT expression.
    /// </summary>
    public class PrefixedExpNode : ExpNode
    {
        string op;

        public PrefixedExpNode(int index, SyntaxNode child, string op)
            : base(index)
        {
            this.op = op;
            Children = new List<SyntaxNode> { child };
        }

        public override string Generate(int indent, GenerateInfo info)
        {
            return string.Format("_{0}({1})", op, Children.First().Generate(indent, info));
        }
    }

    /// <summary>
    /// Holds an expression bound to a variable.
    /// </summary>
    public class BoundExpNode : ExpNode
    {
        string variable;

        public BoundExpNode(int index, SyntaxNode child, SyntaxNode variable)
            : base(index)
        {
            this.variable = variable.Text;
            Children = new List<SyntaxNode> { child };
        }

        public override void Analyze(GenerateInfo info)
        {
            info.VariableNames.Add(variable);
            base.Analyze(info);
        }

        public override string Generate(int indent, GenerateInfo info)
        {
            return string.Format("_VAR({0}, {1})", Children.First().Generate(indent, info), variable);
        }
    }

    /// <summary>
    /// Holds a conditional expression.
    /// </summary>
    public class ConditionExpNode : ExpNode
    {
        string condition;

        public ConditionExpNode(int index, SyntaxNode child, SyntaxNode condition)
            : base(index)
        {
            this.condition = condition.Text;
            Children = new List<SyntaxNode> { child };
        }

        public override string Generate(int indent, GenerateInfo info)
        {
            if (LineNumber == 0)
                throw new Exception("Line numbers have not been assigned.");

            return string.Format("_CONDITION({0}, (_IM_Result_MI_) => {{ var _IM_Result = new {1}(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; return (\n#line {3} \"{4}\"\n{5}{2}\n#line default\n);}})", Children.First().Generate(indent, info), info.MatchItemClass, condition, LineNumber, info.InputFile, SingleIndent(LineOffset));
        }
    }

    /// <summary>
    /// Holds a sequence expression.
    /// </summary>
    public class SequenceExpNode : ExpNode
    {
        public SequenceExpNode(int index, SyntaxNode a, SyntaxNode b)
            : base(index)
        {
            Children = new List<SyntaxNode> { a, b };
        }

        public override string Generate(int indent, GenerateInfo info)
        {
            string result = null;

            foreach (SyntaxNode child in Children)
            {
                if (result != null)
                    result = string.Format("_AND({0}, {1})", result, child.Generate(indent, info));
                else
                    result = child.Generate(indent, info);
            }

            return result;
        }
    }

    /// <summary>
    /// Holds an expression with an action.
    /// </summary>
    public class ActionExpNode : ExpNode
    {
        string action;

        public ActionExpNode(int index, SyntaxNode exp, SyntaxNode action)
            : base(index)
        {
            this.action = action.Text;
            Children = new List<SyntaxNode> { exp };
        }

        public override string Generate(int indent, GenerateInfo info)
        {
            if (LineNumber == 0)
                throw new Exception("Line numbers have not been assigned.");

            return string.Format("_ACTION({0}, (_IM_Result_MI_) => {{ var _IM_Result = new {1}(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; \n#line {3} \"{4}\"\n{5}{2}\n#line default\n}})", Children.First().Generate(indent, info), info.MatchItemClass, action, LineNumber, info.InputFile, SingleIndent(LineOffset));
        }
    }

    /// <summary>
    /// Holds a failure node.
    /// </summary>
    public class FailExpNode : ExpNode
    {
        string message;

        public FailExpNode(int index, SyntaxNode message)
            : base(index)
        {
            this.message = message.Text;
        }

        public override string Generate(int indent, GenerateInfo info)
        {
            return string.Format("_FAIL({0})", message);
        }
    }

    /// <summary>
    /// Holds a disjunction expression.
    /// </summary>
    public class DisjunctionExpNode : ExpNode
    {
        public DisjunctionExpNode(int index, SyntaxNode a, SyntaxNode b)
            : base(index)
        {
            Children = new List<SyntaxNode> { a, b };
        }

        public override string Generate(int indent, GenerateInfo info)
        {
            string result = null;

            foreach (SyntaxNode child in Children)
            {
                if (result != null)
                    result = string.Format("_OR({0}, {1})", result, child.Generate(indent, info));
                else
                    result = child.Generate(indent, info);
            }

            return result;
        }
    }

    /// <summary>
    /// Holds a top-level rule.
    /// </summary>
    public class RuleNode : SyntaxNode
    {
        public bool IsOverride { get; private set; }
        public string Name { get; private set; }
        public SyntaxNode Parms { get; private set; }
        public SyntaxNode Body { get; private set; }

        public HashSet<string> VariableNames { get; private set; }

        public RuleNode(int index, bool isOverride, SyntaxNode name, SyntaxNode parms, SyntaxNode body)
            : base(index)
        {
            IsOverride = isOverride;
            Name = name.Text;
            Parms = parms;
            Body = body;

            var children = new List<SyntaxNode>();
            if (parms != null)
                children.Add(parms);
            children.Add(body);
            Children = children;
        }

        public override void Analyze(GenerateInfo info)
        {
            VariableNames = new HashSet<string>();
            info.VariableNames = VariableNames;

            base.Analyze(info);

            info.VariableNames = null;
        }

        public override string Generate(int indent, GenerateInfo info)
        {
            info.VariableNames = VariableNames;

            if (Parms != null)
                return string.Format("_ARGS({0}, _args, {1})", Parms.Generate(indent, info), Body.Generate(indent, info));
            else
                return Body.Generate(indent, info);
        }
    }

    /// <summary>
    /// Collects all the top-level rules.
    /// </summary>
    public class ParserBodyNode : SyntaxNode
    {
        public Dictionary<string, List<RuleNode>> rules = new Dictionary<string, List<RuleNode>>();

        public ParserBodyNode(int indent, List<SyntaxNode> children)
            : base(indent)
        {
            Children = children;
        }

        public override void Analyze(GenerateInfo info)
        {
            CollectRules(this, info);
            base.Analyze(info);
        }

        public override string Generate(int indent, GenerateInfo info)
        {
            var ruleStrings = new List<string>();

            foreach (var ruleName in rules.Keys)
            {
                ruleStrings.Add(GenerateRule(indent, ruleName, info));
            }

            return string.Join("\n", ruleStrings.ToArray());
        }

        /// <summary>
        /// Generates the code for a rule.
        /// </summary>
        private string GenerateRule(int indent, string ruleName, GenerateInfo info)
        {
            // check for override and whether or not we can have a static top combinator
            bool isOverride = false;
            bool cachedCombinator = true;

            foreach (RuleNode rule in rules[ruleName])
            {
                if (rule.IsOverride)
                    isOverride = true;

                foreach (var vn in rule.VariableNames)
                    if (!info.RuleNames.Contains(vn))
                        cachedCombinator = false;
            }

            // build the function
            string topCombinator = string.Format("_{0}_Body_", ruleName);
            StringBuilder sb = new StringBuilder();

            if (cachedCombinator)
            {
                sb.AppendLine(Indent(indent, "private int {0}_Index_ = -1;", topCombinator));
                sb.AppendLine();
            }

            sb.AppendLine(Indent(indent, "protected {0} IEnumerable<MatchItem> {1}(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)", isOverride ? "override" : "virtual", ruleName));
            sb.AppendLine(Indent(indent, "{{"));
            
            sb.AppendLine(Indent(indent + 1, "Combinator {0} = null;", topCombinator));
            sb.AppendLine();

            if (cachedCombinator)
            {
                sb.AppendLine(Indent(indent + 1, "if ({0}_Index_ == -1 || CachedCombinators[{0}_Index_] == null)", topCombinator));
                sb.AppendLine(Indent(indent + 1, "{{"));
                sb.AppendLine(Indent(indent + 2, "if ({0}_Index_ == -1)", topCombinator));
                sb.AppendLine(Indent(indent + 2, "{{"));
                sb.AppendLine(Indent(indent + 3, "{0}_Index_ = CachedCombinators.Count;", topCombinator));
                sb.AppendLine(Indent(indent + 3, "CachedCombinators.Add(null);"));
                sb.AppendLine(Indent(indent + 2, "}}"));
                sb.AppendLine();

                ++indent;
            }

            // build each disjunct
            int num = 0;
            foreach (RuleNode rule in rules[ruleName])
            {
                string disjCombinator = string.Format("_disj_{0}_", num++);

                sb.AppendLine(Indent(indent+1, "Combinator {0} = null;", disjCombinator));
                sb.AppendLine(Indent(indent+1, "{{"));

                foreach (string varName in rule.VariableNames.Distinct())
                    if (!info.RuleNames.Contains(varName))
                        sb.AppendLine(Indent(indent+2, "var {0} = new {1}();", varName, info.MatchItemClass));

                sb.AppendLine(Indent(indent+2, "{0} = {1};", disjCombinator, rule.Generate(indent, info)));

                sb.AppendLine(Indent(indent+1, "}}"));
            }
            sb.AppendLine();

            // combine disjuncts
            string topDisjunct = null;
            for (int i = 0; i < num; ++i)
            {
                string disj = string.Format("_disj_{0}_", i);

                if (topDisjunct != null)
                    topDisjunct = string.Format("_OR({0}, {1})", topDisjunct, disj);
                else
                    topDisjunct = disj;
            }

            if (cachedCombinator)
            {
                --indent;

                sb.AppendLine(Indent(indent + 2, "CachedCombinators[{0}_Index_] = {1};", topCombinator, topDisjunct));
                sb.AppendLine(Indent(indent + 1, "}}"));
                sb.AppendLine();
                sb.AppendLine(Indent(indent + 1, "{0} = CachedCombinators[{0}_Index_];", topCombinator));
                sb.AppendLine();
            }
            else
            {
                sb.AppendLine(Indent(indent+1, "{0} = {1};", topCombinator, topDisjunct));
            }

            sb.AppendLine();

            // match
            sb.AppendLine(Indent(indent+1, "foreach (var _res_ in {0}.Match(_indent+1, _inputs, _index, null, _memo))", topCombinator));
            sb.AppendLine(Indent(indent+1, "{{"));
            sb.AppendLine(Indent(indent+2, "yield return _res_;"));
            sb.AppendLine();
            sb.AppendLine(Indent(indent+2, "if (StrictPEG) yield break;"));
            sb.AppendLine(Indent(indent+1, "}}"));

            sb.AppendLine(Indent(indent, "}}"));

            return sb.ToString();
        }

        /// <summary>
        /// Traverses the tree looking for rule names.
        /// </summary>
        private void CollectRules(SyntaxNode node, GenerateInfo info)
        {
            if (node is RuleNode)
            {
                RuleNode rule = node as RuleNode;
                info.RuleNames.Add(rule.Name);
                
                if (!rules.ContainsKey(rule.Name))
                    rules.Add(rule.Name, new List<RuleNode>());

                rules[rule.Name].Add(rule);
            }
            else
            {
                foreach (var child in node.Children)
                    CollectRules(child, info);
            }
        }
    } // class ParserBodyNode

    /// <summary>
    /// Holds the parser's name and base class, if any.
    /// </summary>
    public class ParserDeclarationNode : SyntaxNode
    {
        SyntaxNode nameNode;
        string name;
        string baseClass;

        public string Name { get { return name; } }

        public ParserDeclarationNode(int index, SyntaxNode nameNode, SyntaxNode baseClass)
            : base(index)
        {
            this.nameNode = nameNode;
            this.baseClass = baseClass.Text;
        }

        public override void Analyze(GenerateInfo info)
        {
            IdentifierNode idNode = nameNode as IdentifierNode;

            if (idNode != null && idNode.Parameters.Count == 2)
            {
                name = idNode.Name + "Matcher";

                info.InputType = idNode.Parameters[0];
                info.ResultType = idNode.Parameters[1];
                info.BaseClass = baseClass;
            }
            else
            {
                throw new SemanticException(this.index, string.Format("{0}: a parser declaration must include input and output types (even if the base class includes them).", nameNode.Text));
            }

            base.Analyze(info);
        }

        public override string Generate(int indent, GenerateInfo info)
        {
            if (!string.IsNullOrEmpty(baseClass))
                return string.Format("{0} : {1}", name, baseClass);
            else
                return string.Format("{0} : IronMeta.Matcher<{1},{2}>", name, info.InputType, info.ResultType);
        }
    } // class ParserDeclarationNode

    public class ParserNode : SyntaxNode
    {
        ParserDeclarationNode decl;
        ParserBodyNode body;

        public ParserNode(int index, SyntaxNode dNode, SyntaxNode bNode)
            : base(index)
        {
            decl = (ParserDeclarationNode)dNode;
            body = (ParserBodyNode)bNode;

            Children = new List<SyntaxNode> { bNode };
        }

        public override void Analyze(GenerateInfo info)
        {
            decl.Analyze(info);
            info.MatchItemClass = decl.Name + "MatchItem";
            base.Analyze(info);
        }

        public override string Generate(int indent, GenerateInfo info)
        {
            StringBuilder sb = new StringBuilder();

            string parserName = decl.Name;

            sb.AppendLine(Indent(indent, "public partial class {0}", decl.Generate(indent, info)));
            sb.AppendLine(Indent(indent, "{{"));
            sb.AppendLine();

            // constructors
            sb.AppendLine(Indent(indent + 1, "/// <summary>Default Constructor.</summary>"));
            sb.AppendLine(Indent(indent + 1, "public {0}()", parserName));
            sb.AppendLine(Indent(indent + 2, ": base(a => default({0}), true)", info.ResultType));
            sb.AppendLine(Indent(indent + 1, "{{"));
            sb.AppendLine(Indent(indent + 1, "}}"));
            sb.AppendLine();

            sb.AppendLine(Indent(indent + 1, "/// <summary>Constructor.</summary>"));
            sb.AppendLine(Indent(indent + 1, "public {0}(Func<{1},{2}> conv, bool strictPEG)", parserName, info.InputType, info.ResultType));
            sb.AppendLine(Indent(indent + 2, ": base(conv, strictPEG)"));
            sb.AppendLine(Indent(indent + 1, "{{"));
            sb.AppendLine(Indent(indent + 1, "}}"));
            sb.AppendLine();

            // match item class
            GetMatchItemClass(sb, indent+1, info);

            // body
            sb.AppendLine(body.Generate(indent + 1, info));
            sb.AppendLine(Indent(indent, "}} // class {0}", parserName));

            return sb.ToString();
        }

        private void GetMatchItemClass(StringBuilder sb, int indent, GenerateInfo info)
        {
            sb.AppendLine(Indent(indent, "/// <summary>Utility class for referencing variables in conditions and actions.</summary>"));
            sb.AppendLine(Indent(indent, "private class {0} : MatchItem", info.MatchItemClass));
            sb.AppendLine(Indent(indent, "{{"));

            sb.AppendLine(Indent(indent + 1, "public {0}() : base() {{ }}", info.MatchItemClass));
            sb.AppendLine();

            sb.AppendLine(Indent(indent + 1, "public {0}(MatchItem mi)", info.MatchItemClass));
            sb.AppendLine(Indent(indent + 2, ": base(mi)"));
            sb.AppendLine(Indent(indent + 1, "{{"));
            sb.AppendLine(Indent(indent + 1, "}}"));
            sb.AppendLine();

            sb.AppendLine(Indent(indent + 1, "public static implicit operator {0}({1} item) {{ return item.Inputs.LastOrDefault(); }}", info.InputType, info.MatchItemClass));
            sb.AppendLine(Indent(indent + 1, "public static implicit operator List<{0}>({1} item) {{ return item.Inputs.ToList(); }}", info.InputType, info.MatchItemClass));

            if (!info.InputType.Equals(info.ResultType))
            {
                sb.AppendLine(Indent(indent + 1, "public static implicit operator {0}({1} item) {{ return item.Results.LastOrDefault(); }}", info.ResultType, info.MatchItemClass));
                sb.AppendLine(Indent(indent + 1, "public static implicit operator List<{0}>({1} item) {{ return item.Results.ToList(); }}", info.ResultType, info.MatchItemClass));
            }

            sb.AppendLine(Indent(indent, "}}"));
            sb.AppendLine();
        }
    }

    public class UsingStatementNode : SyntaxNode
    {
        public string Identifier { get; set; }

        public UsingStatementNode(int index, string identifier)
            : base(index)
        {
            Identifier = identifier;
            this.text = string.Format("using {0};", identifier);
        }
    }

    public class IronMetaFileNode : SyntaxNode
    {
        List<SyntaxNode> preambleNodes;
        List<SyntaxNode> parserNodes;

        List<string> usingStatements = new List<string>();

        public IronMetaFileNode(int index, List<SyntaxNode> preambleNodes, List<SyntaxNode> parserNodes)
            : base(index)
        {
            this.preambleNodes = preambleNodes;
            this.parserNodes = parserNodes;

            Children = parserNodes;
        }

        public override void Analyze(GenerateInfo info)
        {
            bool usingSystem = false;
            bool usingGeneric = false;
            bool usingLinq = false;

            foreach (var node in preambleNodes)
            {
                var usn = node as UsingStatementNode;
                if (usn != null)
                {
                    if (usn.Text == "using System;")
                        usingSystem = true;
                    else if (usn.Text == "using System.Collections.Generic;")
                        usingGeneric = true;
                    else if (usn.Text == "using System.Linq;")
                        usingLinq = true;
                }
            }

            if (!usingSystem)
                usingStatements.Add("using System;");
            if (!usingGeneric)
                usingStatements.Add("using System.Collections.Generic;");
            if (!usingLinq)
                usingStatements.Add("using System.Linq;");

            base.Analyze(info);
        }

        public override string Generate(int indent, GenerateInfo info)
        {
            if (string.IsNullOrEmpty(info.NameSpace))
                throw new Exception("Calling code must assign a namespace before generating.");

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(Indent(indent, "// IronMeta Generated {0}: {1} UTC", info.NameSpace, DateTime.UtcNow));
            sb.AppendLine();

            // preamble
            foreach (string us in usingStatements)
                sb.AppendLine(Indent(indent, "{0}", us));
            foreach (var pn in preambleNodes)
                sb.AppendLine(Indent(indent, "{0}", pn.Generate(indent, info)));
            sb.AppendLine();

            // namespace
            sb.AppendLine(Indent(indent, "namespace {0}", info.NameSpace));
            sb.AppendLine(Indent(indent, "{{"));
            sb.AppendLine();

            foreach (var child in parserNodes)
                sb.AppendLine(child.Generate(indent + 1, info));

            sb.AppendLine(Indent(indent, "}} // namespace {0}", info.NameSpace));

            return sb.ToString();
        }
    }

} // namespace IronMeta
