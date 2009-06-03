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
            lineNumber = matcher.GetLineNumber(start, out lineOffset);

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
        public KeywordNode(int start, int next)
            : base(start, next, TokenType.KEYWORD)
        {
        }

        public override string Generate(int indent, GenerateInfo info)
        {
            return GetText(info.InputStream);
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

        public override string Generate(int indent, GenerateInfo info)
        {
            string lit = GetText(info.InputStream);

            if (lit.StartsWith("{") && lit.EndsWith("}"))
                lit = lit.Substring(1, lit.Length - 2);

            return string.Format("_LITERAL({0})", lit);
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

        public override string Generate(int indent, GenerateInfo info)
        {
            if (info.RuleNames.Contains(varName))
                return string.Format("_CALL({0})", varName);
            else
                return string.Format("_REF({0}, \"{0}\", this)", varName);
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

        public override string Generate(int indent, GenerateInfo info)
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

                return string.Format("_CALL({0}, new List<MatchItem> {{ {1} }})", ruleName, string.Join(", ", pList.ToArray()));
            }
            else
            {
                return string.Format("_CALL({0})", ruleName);
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

        public PostfixedExpNode(int start, int next, SyntaxNode child, string op)
            : base(start, next)
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

        public PrefixedExpNode(int start, int next, SyntaxNode child, string op)
            : base(start, next)
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

        public override string Generate(int indent, GenerateInfo info)
        {
            return string.Format("_VAR({0}, {1})", Children.First().Generate(indent, info), variable.GetText(info.InputStream));
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

        public override string Generate(int indent, GenerateInfo info)
        {
            if (LineNumber == 0)
                throw new Exception("Line numbers have not been assigned.");

            string cText = condition.GetText(info.InputStream);

            if (cText.Contains("_IM_") || cText.Contains("_IM_Start") || cText.Contains("_IM_Next"))
                return string.Format("_CONDITION({0}, (_IM_Result_MI_) => {{ var _IM_Result = new {1}(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; return (\n#line {3} \"{4}\"\n{5}{2}\n#line default\n);}})",
                    Children.First().Generate(indent, info), info.MatchItemClass, cText, LineNumber, info.InputFileName, SingleIndent(LineOffset));
            else
                return string.Format("_CONDITION({0}, (_IM_Result_MI_) => {{ return (\n#line {3} \"{4}\"\n{5}{2}\n#line default\n);}})",
                    Children.First().Generate(indent, info), info.MatchItemClass, cText, LineNumber, info.InputFileName, SingleIndent(LineOffset));
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

        public override string Generate(int indent, GenerateInfo info)
        {
            string result = null;

            if (Children.Count() == 1)
                result = Children.First().Generate(indent, info);
            else
                result = string.Format("_AND({0})", string.Join(", ", Children.Select(n => n.Generate(indent, info)).ToArray()));

            return result;
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

        public override string Generate(int indent, GenerateInfo info)
        {
            if (LineNumber == 0)
                throw new Exception("Line numbers have not been assigned.");

            string aText = action.GetText(info.InputStream);

            if (aText.Contains("_IM_Result") || aText.Contains("_IM_Start") || aText.Contains("_IM_Next"))
                return string.Format("_ACTION({0}, (_IM_Result_MI_) => {{ var _IM_Result = new {1}(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; \n#line {3} \"{4}\"\n{5}{2}\n#line default\n}})",
                    Children.First().Generate(indent, info), info.MatchItemClass, aText, LineNumber, info.InputFileName, SingleIndent(LineOffset));
            else
                return string.Format("_ACTION({0}, (_IM_Result_MI_) => {{ \n#line {3} \"{4}\"\n{5}{2}\n#line default\n}})",
                    Children.First().Generate(indent, info), info.MatchItemClass, aText, LineNumber, info.InputFileName, SingleIndent(LineOffset));
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

        public override string Generate(int indent, GenerateInfo info)
        {
            return string.Format("_FAIL({0})", message.GetText(info.InputStream));
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


        public override string Generate(int indent, GenerateInfo info)
        {
            string result = null;

            if (Children.Count() == 1)
                result = Children.First().Generate(indent, info);
            else
                result = string.Format("_OR({0})", string.Join(", ", Children.Select(n => n.Generate(indent, info)).ToArray()));

            return result;
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

        public override string Generate(int indent, GenerateInfo info)
        {
            return string.Format("{0} : {1}", className, baseName);
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

        public override string Generate(int indent, GenerateInfo info)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(Indent(indent, "public partial class {0}", decl.Generate(indent, info)));
            sb.AppendLine(Indent(indent, "{{"));
            sb.AppendLine();

            // constructors
            sb.AppendLine(Indent(indent + 1, "/// <summary>Default Constructor.</summary>"));
            sb.AppendLine(Indent(indent + 1, "public {0}()", info.ClassName));
            sb.AppendLine(Indent(indent + 2, ": base(a => default({0}), true)", info.ResultType));
            sb.AppendLine(Indent(indent + 1, "{{"));
            sb.AppendLine(Indent(indent + 1, "}}"));
            sb.AppendLine();

            sb.AppendLine(Indent(indent + 1, "/// <summary>Constructor.</summary>"));
            sb.AppendLine(Indent(indent + 1, "public {0}(Func<{1},{2}> conv, bool strictPEG)", info.ClassName, info.InputType, info.ResultType));
            sb.AppendLine(Indent(indent + 2, ": base(conv, strictPEG)"));
            sb.AppendLine(Indent(indent + 1, "{{"));
            sb.AppendLine(Indent(indent + 1, "}}"));
            sb.AppendLine();

            // match item class
            GetMatchItemClass(sb, indent+1, info);

            // body
            sb.AppendLine(body.Generate(indent + 1, info));
            sb.AppendLine(Indent(indent, "}} // class {0}", info.ClassName));

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
        public SyntaxNode Identifier { get; private set; }

        public UsingStatementNode(int start, int next, SyntaxNode identifier)
            : base(start, next)
        {
            this.Identifier = identifier;
        }

        public override string Generate(int indent, GenerateInfo info)
        {
            return string.Format("using {0};", Identifier.GetText(info.InputStream));
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
