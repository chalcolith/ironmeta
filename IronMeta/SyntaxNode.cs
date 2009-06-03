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
        public string InputFileName { get; set; }

        public IEnumerable<char> InputStream { get; set; }

        /// <summary>Needs to be set before analysis.</summary>
        public IronMetaMatcher Matcher { get; set; }

        /// <summary>Needs to be set before analysis.</summary>
        public string NameSpace { get; set; }

        /// <summary>Needs to be set before individual rules are analyzed.</summary>
        public HashSet<string> RuleNames { get; set; }
        
        /// <summary>Used by individual rules to temporarily store their variables during analysis.</summary>
        public HashSet<string> VariableNames { get; set; }

        public string ClassName { get; set; }
        public string InputType { get; set; }
        public string ResultType { get; set; }
        public string BaseClass { get; set; }

        /// <summary>Needs to be set before generation.</summary>
        public string MatchItemClass { get; set; }

        public GenerateInfo(string inputFile, IronMetaMatcher matcher, string nameSpace, IEnumerable<char> inputs)
        {
            InputFileName = inputFile;
            InputStream = inputs;
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
        protected int start, next;

        private int lineNumber, lineOffset;

        IEnumerable<SyntaxNode> children = null;

        private string text = null;
        
        public IEnumerable<SyntaxNode> Children
        {
            get
            {
                return children ?? Enumerable.Empty<SyntaxNode>();
            }

            internal set
            {
                children = value;
            }
        }

        public int StartIndex { get { return start; } }
        public int NextIndex { get { return next; } }
        public int LineNumber { get { return lineNumber; } }
        public int LineOffset { get { return lineOffset; } }

        public SyntaxNode()
        {            
        }

        public SyntaxNode(int start, int next)
        {
            this.start = start;
            this.next = next;
        }

        public string GetText(IEnumerable<char> inputs)
        {
            if (text == null)
            {
                var sb = new StringBuilder();
                for (int i = start; i < next; ++i)
                    sb.Append(inputs.ElementAt(i));
                text = sb.ToString();
            }

            return text;
        }

        public virtual void Analyze(GenerateInfo info)
        {
            foreach (SyntaxNode child in Children)
                child.Analyze(info);
        }

        public virtual void Generate(int indent, StringBuilder sb, GenerateInfo info)
        {
            sb.Append(text ?? "<?>");
        }

        public override string ToString()
        {
            return this.GetType().Name;
        }

        public void AssignLineNumbers(CharacterMatcher<SyntaxNode> matcher)
        {
            lineNumber = matcher.GetLineNumber(start, out lineOffset);

            foreach (SyntaxNode child in Children)
                child.AssignLineNumbers(matcher);
        }

        protected void Indent(int indent, StringBuilder sb)
        {
            sb.Append(' ', indent*4);
        }

        protected void Indent(int indent, char ch, StringBuilder sb)
        {
            sb.Append(ch, indent);
        }


        public static void Optimize(SyntaxNode rootNode)
        {
            FoldOrs(rootNode);
            FoldAnds(rootNode);
        }

        static void FoldAnds(SyntaxNode node)
        {
            foreach (SyntaxNode child in node.Children)
                FoldAnds(child);

            if (node is SequenceExpNode && node.children != null)
            {
                List<SyntaxNode> newChildren = new List<SyntaxNode>();

                foreach (SyntaxNode child in node.children)
                {
                    SequenceExpNode seq = child as SequenceExpNode;
                    if (seq != null)
                        newChildren.AddRange(seq.Children);
                    else
                        newChildren.Add(child);
                }

                node.children = newChildren;
            }
        }

        static void FoldOrs(SyntaxNode node)
        {
            foreach (SyntaxNode child in node.Children)
                FoldOrs(child);

            if (node is DisjunctionExpNode && node.children != null)
            {
                List<SyntaxNode> newChildren = new List<SyntaxNode>();

                foreach (SyntaxNode child in node.children)
                {
                    DisjunctionExpNode disj = child as DisjunctionExpNode;
                    if (disj != null)
                        newChildren.AddRange(disj.children);
                    else
                        newChildren.Add(child);
                }

                node.children = newChildren;
            }
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

        public TokenNode(int start, int next, TokenType type)
            : base(start, next)
        {
            this.Type = type;
        }

        public override void Generate(int indent, StringBuilder sb, GenerateInfo info)
        {
            sb.Append("<");
            sb.Append(Type.ToString());
            sb.Append(">");
        }
    }

    /// <summary>
    /// Holds tokens that are keywords.
    /// </summary>
    public class KeywordNode : TokenNode
    {
        public KeywordNode(int start, int next)
            : base(start, next, TokenType.KEYWORD)
        {
        }

        public override void Generate(int indent, StringBuilder sb, GenerateInfo info)
        {
            sb.Append(GetText(info.InputStream));
        }
    }

    /// <summary>
    /// Holds an identifier.
    /// </summary>
    public class IdentifierNode : TokenNode
    {
        public SyntaxNode Name { get; set; }
        public IEnumerable<SyntaxNode> Qualifiers { get; set; }
        public IEnumerable<SyntaxNode> Parameters { get; set; }

        public IdentifierNode(int start, int next, SyntaxNode name, IEnumerable<SyntaxNode> qualifiers, IEnumerable<SyntaxNode> parameters)
            : base(start, next, TokenType.IDENTIFIER)
        {
            this.Name = name ?? this;
            this.Qualifiers = qualifiers;
            this.Parameters = parameters;
        }
    }

    /// <summary>
    /// Holds code comments.
    /// </summary>
    public class CommentNode : TokenNode
    {
        public CommentNode(int start, int next)
            : base(start, next, TokenType.COMMENT)
        {
        }
    }

    /// <summary>
    /// Holds a span of whitespace.
    /// </summary>
    public class SpacingNode : SyntaxNode
    {
        public SpacingNode(int start, int next, IEnumerable<SyntaxNode> children)
            : base(start, next)
        {
            Children = children.Where(child => child != null);
        }

        public override void Generate(int indent, StringBuilder sb, GenerateInfo info)
        {
            foreach (SyntaxNode child in Children)
            {
                if (child is CommentNode)
                {
                    Indent(indent, sb);
                    child.Generate(indent, sb, info);
                }
            }
        }
    }

    /// <summary>
    /// Holds a section of literal C# code.
    /// </summary>
    public class CSharpNode : SyntaxNode
    {
        public CSharpNode(int start, int next)
            : base(start, next)
        {
        }
    }

    /// <summary>
    /// Base class for expression terms.
    /// </summary>
    public abstract class ExpNode : SyntaxNode
    {
        public ExpNode(int start, int next)
            : base(start, next)
        {
        }

        protected string DefaultVars(string itemClass)
        {
            return string.Format("var _IM_Result = new {0}(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex;", itemClass);
        }
    }


    /// <summary>
    /// Holds a CSharp code literal for use as a term in the match.
    /// </summary>
    public class LiteralExpNode : ExpNode
    {
        public LiteralExpNode(int start, int next, IEnumerable<SyntaxNode> children)
            : base(start, next)
        {
        }

        public override void Generate(int indent, StringBuilder sb, GenerateInfo info)
        {
            string lit = GetText(info.InputStream);

            if (lit.StartsWith("{") && lit.EndsWith("}"))
                lit = lit.Substring(1, lit.Length - 2);

            sb.Append("_LITERAL(" + lit + ")");
        }
    }

    /// <summary>
    /// Holds a bare identifier that could be either a variable or a rule call.
    /// </summary>
    public class CallOrVarExpNode : ExpNode
    {
        SyntaxNode identifier;

        string varName;

        public CallOrVarExpNode(int start, int next, SyntaxNode identifier)
            : base(start, next)
        {
            this.identifier = identifier;
        }

        public override void Analyze(GenerateInfo info)
        {
            varName = GetText(info.InputStream).Trim();

            if (varName.Contains('.'))
            {
                if (varName.ToUpper().StartsWith("BASE."))
                    varName = varName.Substring(5);
                else if (varName.ToUpper().StartsWith("SUPER."))
                    varName = varName.Substring(6);

                info.RuleNames.Add(varName);
            }
            else
            {
                info.VariableNames.Add(varName);
            }

            base.Analyze(info);
        }

        public override void Generate(int indent, StringBuilder sb, GenerateInfo info)
        {
            if (info.RuleNames.Contains(varName))
                sb.AppendFormat("_CALL({0})", varName);
            else
                sb.AppendFormat("_REF({0}, \"{0}\", this)", varName);
        }
    }

    /// <summary>
    /// Holds a rule call with a parameter list.
    /// </summary>
    public class RuleCallExpNode : ExpNode
    {
        SyntaxNode name;
        IEnumerable<SyntaxNode> parameters;

        string ruleName;

        public RuleCallExpNode(int start, int next, SyntaxNode name, List<SyntaxNode> parameters)
            : base(start, next)
        {
            this.name = name;
            this.parameters = parameters;

            Children = parameters;
        }

        public override void Analyze(GenerateInfo info)
        {
            ruleName = name.GetText(info.InputStream);

            if (ruleName.ToUpper().StartsWith("BASE."))
                ruleName = ruleName.Substring(5);
            else if (ruleName.ToUpper().StartsWith("SUPER."))
                ruleName = ruleName.Substring(6);

            base.Analyze(info);
        }

        public override void Generate(int indent, StringBuilder sb, GenerateInfo info)
        {
            if (parameters != null && parameters.Any())
            {
                var pList = new List<string>();
                foreach (var p in parameters)
                {
                    string pText = p.GetText(info.InputStream);

                    if (info.RuleNames.Contains(pText))
                        pList.Add(string.Format("new MatchItem({0})", pText));
                    else if (info.VariableNames.Contains(pText))
                        pList.Add(pText);
                    else
                        pList.Add(string.Format("new MatchItem({0}, CONV)", pText));
                }

                sb.AppendFormat("_CALL({0}, new List<MatchItem> {{ {1} }})", ruleName, string.Join(", ", pList.ToArray()));
            }
            else
            {
                sb.AppendFormat("_CALL({0})", ruleName);
            }
        }
    }

    /// <summary>
    /// Holds the "dot" term.
    /// </summary>
    public class AnyExpNode : ExpNode
    {
        public AnyExpNode(int start, int next)
            : base(start, next)
        {
        }

        public override void Generate(int indent, StringBuilder sb, GenerateInfo info)
        {
            sb.Append("_ANY()");
        }
    }

    /// <summary>
    /// Holds a QUES, PLUS or STAR expression.
    /// </summary>
    public class PostfixedExpNode : ExpNode
    {
        string op;

        public PostfixedExpNode(int start, int next, SyntaxNode child, string op)
            : base(start, next)
        {
            this.op = op;
            Children = new List<SyntaxNode> { child };
        }

        public override void Generate(int indent, StringBuilder sb, GenerateInfo info)
        {
            sb.Append("_" + op + "(");
            Children.First().Generate(indent, sb, info);
            sb.Append(")");
        }
    }

    /// <summary>
    /// Holds an AND or NOT expression.
    /// </summary>
    public class PrefixedExpNode : ExpNode
    {
        string op;

        public PrefixedExpNode(int start, int next, SyntaxNode child, string op)
            : base(start, next)
        {
            this.op = op;
            Children = new List<SyntaxNode> { child };
        }

        public override void Generate(int indent, StringBuilder sb, GenerateInfo info)
        {
            sb.Append("_" + op + "(");
            Children.First().Generate(indent, sb, info);
            sb.Append(")");
        }
    }

    /// <summary>
    /// Holds an expression bound to a variable.
    /// </summary>
    public class BoundExpNode : ExpNode
    {
        SyntaxNode variable;

        public BoundExpNode(int start, int next, SyntaxNode child, SyntaxNode variable)
            : base(start, next)
        {
            this.variable = variable;
            Children = new List<SyntaxNode> { child };
        }

        public override void Analyze(GenerateInfo info)
        {
            info.VariableNames.Add(variable.GetText(info.InputStream).Trim());
            base.Analyze(info);
        }

        public override void Generate(int indent, StringBuilder sb, GenerateInfo info)
        {
            sb.Append("_VAR(");
            Children.First().Generate(indent, sb, info);
            sb.Append(", " + variable.GetText(info.InputStream) + ")");
        }
    }

    /// <summary>
    /// Holds a conditional expression.
    /// </summary>
    public class ConditionExpNode : ExpNode
    {
        SyntaxNode condition;

        public ConditionExpNode(int start, int next, SyntaxNode child, SyntaxNode condition)
            : base(start, next)
        {
            this.condition = condition;
            Children = new List<SyntaxNode> { (ExpNode)child };
        }

        public override void Generate(int indent, StringBuilder sb, GenerateInfo info)
        {
            condition.AssignLineNumbers(info.Matcher);

            string cText = condition.GetText(info.InputStream);

            sb.Append("_CONDITION(");
            Children.First().Generate(indent, sb, info);

            if (cText.Contains("_IM_") || cText.Contains("_IM_Start") || cText.Contains("_IM_Next"))
                sb.AppendFormat(", (_IM_Result_MI_) => {{ {0} return (\n#line {1} \"{2}\"\n", DefaultVars(info.MatchItemClass), condition.LineNumber, info.InputFileName);
            else
                sb.AppendFormat(", (_IM_Result_MI_) => {{ return (\n#line {0} \"{1}\"\n", condition.LineNumber, info.InputFileName);

            Indent(condition.LineOffset, ' ', sb);

            sb.Append(cText);
            sb.AppendFormat("\n#line default\n);}})");
        }
    }

    /// <summary>
    /// Holds a sequence expression.
    /// </summary>
    public class SequenceExpNode : ExpNode
    {
        public SequenceExpNode(int start, int next, IEnumerable<SyntaxNode> children)
            : base(start, next)
        {
            Children = children;
        }

        public override void Generate(int indent, StringBuilder sb, GenerateInfo info)
        {
            if (Children.Count() == 1)
            {
                Children.First().Generate(indent, sb, info);
            }
            else
            {
                sb.Append("_AND(");
                int i = 0;
                foreach (var child in Children)
                {
                    if (i++ > 0)
                        sb.Append(", ");
                    child.Generate(indent, sb, info);
                }
                sb.Append(")");
            }
        }
    }

    /// <summary>
    /// Holds an expression with an action.
    /// </summary>
    public class ActionExpNode : ExpNode
    {
        SyntaxNode action;

        public ActionExpNode(int start, int next, SyntaxNode exp, SyntaxNode action)
            : base(start, next)
        {
            this.action = action;
            Children = new List<SyntaxNode> { exp };
        }

        public override void Generate(int indent, StringBuilder sb, GenerateInfo info)
        {
            action.AssignLineNumbers(info.Matcher);

            string aText = action.GetText(info.InputStream);

            sb.Append("_ACTION(");
            Children.First().Generate(indent, sb, info);
            

            if (aText.Contains("_IM_Result") || aText.Contains("_IM_Start") || aText.Contains("_IM_Next"))
                sb.AppendFormat(", (_IM_Result_MI_) => {{ {0} \n#line {1} \"{2}\"\n", DefaultVars(info.MatchItemClass), action.LineNumber, info.InputFileName);
            else
                sb.AppendFormat(", (_IM_Result_MI_) => {{ \n#line {0} \"{1}\"\n", action.LineNumber, info.InputFileName);

            Indent(action.LineOffset, ' ', sb);

            sb.Append(aText);
            sb.AppendFormat("\n#line default\n}})");
        }
    }

    /// <summary>
    /// Holds a failure node.
    /// </summary>
    public class FailExpNode : ExpNode
    {
        SyntaxNode message;

        public FailExpNode(int start, int next, SyntaxNode message)
            : base(start, next)
        {
            this.message = message;
        }

        public override void Generate(int indent, StringBuilder sb, GenerateInfo info)
        {
            sb.AppendFormat("_FAIL({0})", message.GetText(info.InputStream));
        }
    }

    /// <summary>
    /// Holds a disjunction expression.
    /// </summary>
    public class DisjunctionExpNode : ExpNode
    {
        public DisjunctionExpNode(int start, int next, IEnumerable<SyntaxNode> children)
            : base(start, next)
        {
            Children = children;
        }


        public override void Generate(int indent, StringBuilder sb, GenerateInfo info)
        {
            if (Children.Count() == 1)
            {
                Children.First().Generate(indent, sb, info);
            }
            else
            {
                sb.Append("_OR(");
                int i = 0;
                foreach (var child in Children)
                {
                    if (i++ > 0)
                        sb.Append(", ");
                    child.Generate(indent, sb, info);
                }
                sb.Append(")");
            }
        }
    }

    /// <summary>
    /// Holds a top-level rule.
    /// </summary>
    public class RuleNode : SyntaxNode
    {
        public bool IsOverride { get; private set; }
        public SyntaxNode Name { get; private set; }
        public SyntaxNode Parms { get; private set; }
        public SyntaxNode Body { get; private set; }

        public HashSet<string> VariableNames { get; private set; }

        public RuleNode(int start, int next, bool isOverride, SyntaxNode name, SyntaxNode parms, SyntaxNode body)
            : base(start, next)
        {
            IsOverride = isOverride;
            Name = name;
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

        public override void Generate(int indent, StringBuilder sb, GenerateInfo info)
        {
            info.VariableNames = VariableNames;

            if (Parms != null)
            {
                sb.Append("_ARGS(");
                Parms.Generate(indent, sb, info);
                sb.Append(", _args, ");
                Body.Generate(indent, sb, info);
                sb.Append(")");
            }
            else
            {
                Body.Generate(indent, sb, info);
            }
        }
    }

    /// <summary>
    /// Collects all the top-level rules.
    /// </summary>
    public class ParserBodyNode : SyntaxNode
    {
        public Dictionary<string, List<RuleNode>> rules = new Dictionary<string, List<RuleNode>>();

        public ParserBodyNode(int start, int next, List<SyntaxNode> children)
            : base(start, next)
        {
            Children = children;
        }

        public override void Analyze(GenerateInfo info)
        {
            CollectRules(this, info);
            base.Analyze(info);
        }

        public override void Generate(int indent, StringBuilder sb, GenerateInfo info)
        {
            foreach (var ruleName in rules.Keys)
            {
                GenerateRule(indent, ruleName, sb, info);
            }
        }

        /// <summary>
        /// Generates the code for a rule.
        /// </summary>
        private void GenerateRule(int indent, string ruleName, StringBuilder sb, GenerateInfo info)
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

            if (cachedCombinator)
            {
                Indent(indent, sb); sb.AppendFormat("private int {0}_Index_ = -1;\n\n", topCombinator);
            }

            Indent(indent, sb); sb.AppendFormat("protected {0} IEnumerable<MatchItem> {1}(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)\n", isOverride ? "override" : "virtual", ruleName);
            Indent(indent, sb); sb.AppendFormat("{{\n");

            Indent(indent + 1, sb); sb.AppendFormat("Combinator {0} = null;\n\n", topCombinator);

            if (cachedCombinator)
            {
                Indent(indent + 1, sb); sb.AppendFormat("if ({0}_Index_ == -1 || CachedCombinators[{0}_Index_] == null)\n", topCombinator);
                Indent(indent + 1, sb); sb.AppendFormat("{{\n");
                Indent(indent + 2, sb); sb.AppendFormat("if ({0}_Index_ == -1)\n", topCombinator);
                Indent(indent + 2, sb); sb.AppendFormat("{{\n");
                Indent(indent + 3, sb); sb.AppendFormat("{0}_Index_ = CachedCombinators.Count;\n", topCombinator);
                Indent(indent + 3, sb); sb.AppendFormat("CachedCombinators.Add(null);\n");
                Indent(indent + 2, sb); sb.AppendFormat("}}\n\n");

                ++indent;
            }

            // build each disjunct
            int num = 0;
            foreach (RuleNode rule in rules[ruleName])
            {
                string disjCombinator = string.Format("_disj_{0}_", num++);

                Indent(indent + 1, sb); sb.AppendFormat("Combinator {0} = null;\n", disjCombinator);
                Indent(indent + 1, sb); sb.AppendFormat("{{\n");

                foreach (string varName in rule.VariableNames.Distinct())
                {
                    if (!info.RuleNames.Contains(varName))
                    {
                        Indent(indent + 2, sb); sb.AppendFormat("var {0} = new {1}();\n", varName, info.MatchItemClass);
                    }
                }

                Indent(indent + 2, sb); sb.AppendFormat("{0} = ", disjCombinator);
                rule.Generate(indent, sb, info);
                sb.AppendFormat(";\n");

                Indent(indent + 1, sb); sb.AppendFormat("}}\n");
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

                Indent(indent + 2, sb); sb.AppendFormat("CachedCombinators[{0}_Index_] = {1};\n", topCombinator, topDisjunct);
                Indent(indent + 1, sb); sb.AppendFormat("}}\n\n");
                Indent(indent + 1, sb); sb.AppendFormat("{0} = CachedCombinators[{0}_Index_];\n\n", topCombinator);
            }
            else
            {
                Indent(indent + 1, sb); sb.AppendFormat("{0} = {1};\n", topCombinator, topDisjunct);
            }

            sb.AppendLine();

            // match
            Indent(indent + 1, sb); sb.AppendFormat("foreach (var _res_ in {0}.Match(_indent+1, _inputs, _index, null, _memo))\n", topCombinator);
            Indent(indent + 1, sb); sb.AppendFormat("{{\n");
            Indent(indent + 2, sb); sb.AppendFormat("yield return _res_;\n\n");
            Indent(indent + 2, sb); sb.AppendFormat("if (StrictPEG) yield break;\n");
            Indent(indent + 1, sb); sb.AppendFormat("}}\n");

            Indent(indent, sb); sb.AppendFormat("}}\n\n");
        }

        /// <summary>
        /// Traverses the tree looking for rule names.
        /// </summary>
        private void CollectRules(SyntaxNode node, GenerateInfo info)
        {
            if (node is RuleNode)
            {
                RuleNode rule = node as RuleNode;
                string ruleName = rule.Name.GetText(info.InputStream).Trim();

                info.RuleNames.Add(ruleName);
                
                if (!rules.ContainsKey(ruleName))
                    rules.Add(ruleName, new List<RuleNode>());

                rules[ruleName].Add(rule);
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
        public SyntaxNode Name { get; private set; }
        public SyntaxNode Base { get; private set; }

        string className;
        string baseName;

        public ParserDeclarationNode(int start, int next, SyntaxNode nameNode, SyntaxNode baseNode)
            : base(start, next)
        {
            this.Name = nameNode;
            this.Base = baseNode;
        }

        public override void Analyze(GenerateInfo info)
        {
            IdentifierNode idNode = Name as IdentifierNode;

            if (idNode != null && idNode.Parameters.Count() == 2)
            {
                className = idNode.Name.GetText(info.InputStream) + "Matcher";
                info.ClassName = className;
                info.MatchItemClass = className + "Item";

                info.InputType = idNode.Parameters.ElementAt(0).GetText(info.InputStream);
                info.ResultType = idNode.Parameters.ElementAt(1).GetText(info.InputStream);

                if (Base != null)
                    baseName = Base.GetText(info.InputStream);
                else
                    baseName = string.Format("IronMeta.Matcher<{0}, {1}>", info.InputType, info.ResultType);

                info.BaseClass = baseName;
            }
            else
            {
                throw new SemanticException(this.start, string.Format("{0}: a parser declaration must include input and output types (even if the base class includes them).", className));
            }

            base.Analyze(info);
        }

        public override void Generate(int indent, StringBuilder sb, GenerateInfo info)
        {
            sb.AppendFormat("{0} : {1}", className, baseName);
        }
    } // class ParserDeclarationNode

    public class ParserNode : SyntaxNode
    {
        ParserDeclarationNode decl;
        ParserBodyNode body;

        public ParserNode(int start, int next, SyntaxNode dNode, SyntaxNode bNode)
            : base(start, next)
        {
            decl = (ParserDeclarationNode)dNode;
            body = (ParserBodyNode)bNode;

            Children = new List<SyntaxNode> { bNode };
        }

        public override void Analyze(GenerateInfo info)
        {
            decl.Analyze(info);
            base.Analyze(info);
        }

        public override void Generate(int indent, StringBuilder sb, GenerateInfo info)
        {
            Indent(indent, sb); sb.AppendFormat("public partial class ");
            decl.Generate(indent, sb, info);
            sb.AppendLine();
            Indent(indent, sb); sb.AppendFormat("{{\n\n");

            // constructors
            Indent(indent + 1, sb); sb.AppendFormat("/// <summary>Default Constructor.</summary>\n");
            Indent(indent + 1, sb); sb.AppendFormat("public {0}()\n", info.ClassName);
            Indent(indent + 2, sb); sb.AppendFormat(": base(a => default({0}), true)\n", info.ResultType);
            Indent(indent + 1, sb); sb.AppendFormat("{{\n");
            Indent(indent + 1, sb); sb.AppendFormat("}}\n\n");

            Indent(indent + 1, sb); sb.AppendFormat("/// <summary>Constructor.</summary>\n");
            Indent(indent + 1, sb); sb.AppendFormat("public {0}(Func<{1},{2}> conv, bool strictPEG)\n", info.ClassName, info.InputType, info.ResultType);
            Indent(indent + 2, sb); sb.AppendFormat(": base(conv, strictPEG)\n");
            Indent(indent + 1, sb); sb.AppendFormat("{{\n");
            Indent(indent + 1, sb); sb.AppendFormat("}}\n\n");

            // match item class
            GetMatchItemClass(sb, indent+1, info);

            // body
            body.Generate(indent + 1, sb, info);
            sb.AppendLine();
            Indent(indent, sb); sb.AppendFormat("}} // class {0}\n", info.ClassName);
        }

        private void GetMatchItemClass(StringBuilder sb, int indent, GenerateInfo info)
        {
            Indent(indent, sb); sb.AppendFormat("/// <summary>Utility class for referencing variables in conditions and actions.</summary>\n");
            Indent(indent, sb); sb.AppendFormat("private class {0} : MatchItem\n", info.MatchItemClass);
            Indent(indent, sb); sb.AppendFormat("{{\n");

            Indent(indent + 1, sb); sb.AppendFormat("public {0}() : base() {{ }}\n", info.MatchItemClass);
            sb.AppendLine();

            Indent(indent + 1, sb); sb.AppendFormat("public {0}(MatchItem mi)\n", info.MatchItemClass);
            Indent(indent + 2, sb); sb.AppendFormat(": base(mi)\n");
            Indent(indent + 1, sb); sb.AppendFormat("{{\n");
            Indent(indent + 1, sb); sb.AppendFormat("}}\n");
            sb.AppendLine();

            Indent(indent + 1, sb); sb.AppendFormat("public static implicit operator {0}({1} item) {{ return item.Inputs.LastOrDefault(); }}\n", info.InputType, info.MatchItemClass);
            Indent(indent + 1, sb); sb.AppendFormat("public static implicit operator List<{0}>({1} item) {{ return item.Inputs.ToList(); }}\n", info.InputType, info.MatchItemClass);

            if (!info.InputType.Equals(info.ResultType))
            {
                Indent(indent + 1, sb); sb.AppendFormat("public static implicit operator {0}({1} item) {{ return item.Results.LastOrDefault(); }}\n", info.ResultType, info.MatchItemClass);
                Indent(indent + 1, sb); sb.AppendFormat("public static implicit operator List<{0}>({1} item) {{ return item.Results.ToList(); }}\n", info.ResultType, info.MatchItemClass);
            }

            Indent(indent, sb); sb.AppendFormat("}}\n");
            sb.AppendLine();
        }
    }

    public class UsingStatementNode : SyntaxNode
    {
        public SyntaxNode Identifier { get; private set; }

        public UsingStatementNode(int start, int next, SyntaxNode identifier)
            : base(start, next)
        {
            this.Identifier = identifier;
        }

        public override void Generate(int indent, StringBuilder sb, GenerateInfo info)
        {
            sb.AppendFormat("using {0};", Identifier.GetText(info.InputStream));
        }
    }

    public class IronMetaFileNode : SyntaxNode
    {
        List<SyntaxNode> preambleNodes;
        List<SyntaxNode> parserNodes;

        List<string> usingStatements = new List<string>();

        public IronMetaFileNode(int next, int start, List<SyntaxNode> preambleNodes, List<SyntaxNode> parserNodes)
            : base(next, start)
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
                    string uText = usn.Identifier.GetText(info.InputStream);

                    if (uText == "System")
                        usingSystem = true;
                    else if (uText == "System.Collections.Generic")
                        usingGeneric = true;
                    else if (uText == "System.Linq")
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

        public override void Generate(int indent, StringBuilder sb, GenerateInfo info)
        {
            if (string.IsNullOrEmpty(info.NameSpace))
                throw new Exception("Calling code must assign a namespace before generating.");

            Indent(indent, sb); sb.AppendFormat("// IronMeta Generated {0}: {1} UTC\n", info.NameSpace, DateTime.UtcNow);
            sb.AppendLine();

            // preamble
            foreach (string us in usingStatements)
            {
                Indent(indent, sb); sb.AppendFormat("{0}\n", us);
            }
            foreach (var pn in preambleNodes)
            {
                Indent(indent, sb); pn.Generate(indent, sb, info);
                sb.AppendLine();
            }
            sb.AppendLine();

            // namespace
            Indent(indent, sb); sb.AppendFormat("namespace {0}\n", info.NameSpace);
            Indent(indent, sb); sb.AppendFormat("{{\n");
            sb.AppendLine();

            foreach (var child in parserNodes)
            {
                child.Generate(indent + 1, sb, info);
            }

            Indent(indent, sb); sb.AppendFormat("}} // namespace {0}\n", info.NameSpace);
        }
    }

} // namespace IronMeta
