// IronMeta Copyright © Gordon Tisher

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IronMeta.Generator
{
    /// <summary>
    /// Generates C# code for an IronMeta parser from an abstract syntax tree.
    /// </summary>
    public class CSharpGen : IGenerator
    {
        AST.GrammarFile grammar;
        string gNamespace;
        bool add_timestamp = true;
        string gName, gBase, tInput, tResult, tItem;

        List<AST.Rule> ruleNodes = new List<AST.Rule>();
        Dictionary<string, AST.AstNode> ruleBodies = new Dictionary<string, AST.AstNode>();
        Dictionary<string, string> overrides = new Dictionary<string, string>();
        Dictionary<string, string> regexps = new Dictionary<string, string>();

        public CSharpGen(AST.AstNode topNode, string name_space)
        {
            if (!(topNode is AST.GrammarFile))
                throw new Exception("Unable to generate.");

            this.gNamespace = name_space;
            this.grammar = topNode as AST.GrammarFile;
        }

        public CSharpGen(AST.AstNode topNode, string name_space, bool add_timestamp)
        {
            if (!(topNode is AST.GrammarFile))
                throw new Exception("Unable to generate.");

            this.gNamespace = name_space;
            this.add_timestamp = add_timestamp;
            this.grammar = topNode as AST.GrammarFile;
        }

        public void Generate(string srcFname, TextWriter tw)
        {
            Analyze(grammar, null);
            GenerateGrammarFile(tw);
        }

        void Analyze(AST.AstNode node, AST.Rule currentRule)
        {
            // get grammar name & generic parameters
            if (node is AST.Grammar)
            {
                AST.Grammar gr = node as AST.Grammar;
                tInput = gr.GetText(gr.TInput).Trim();
                tResult = gr.GetText(gr.TResult).Trim();
                gName = gr.GetText(gr.Name).Trim();
                tItem = string.Format("_{0}_Item", gName);
                gBase = gr.GetText(gr.Base).Trim();
                if (string.IsNullOrWhiteSpace(gBase))
                    gBase = string.Format("IronMeta.Matcher.Matcher<{0}, {1}>", tInput, tResult);
            }

            // also analyze arguments (because they are not children)
            else if (node is AST.Args)
            {
                AST.Args args = node as AST.Args;
                if (args.Parms != null)
                    Analyze(args.Parms, currentRule);
            }

            // collect rule bodies
            else if (node is AST.Rule)
            {
                currentRule = node as AST.Rule;
                ruleNodes.Add(currentRule);

                string ruleName = node.GetText().Trim();

                if (!string.IsNullOrEmpty(currentRule.Override))
                    overrides[ruleName] = currentRule.Override;

                AST.AstNode oldBody;
                if (ruleBodies.TryGetValue(ruleName, out oldBody))
                    ruleBodies[ruleName] = new AST.Or(oldBody, currentRule.Body);
                else
                    ruleBodies.Add(ruleName, currentRule.Body);
            }

            // collect input classes
            else if (node is AST.InputClass)
            {
                AST.InputClass input = node as AST.InputClass;

                foreach (AST.AstNode child in input.Inputs)
                {
                    if (child is AST.Code)
                    {
                        input.Chars.Add(TrimBraces(child.GetText().Trim()).Trim());
                    }
                    else if (child is AST.ClassRange)
                    {
                        foreach (char ch in ((AST.ClassRange)child).Inputs)
                        {
                            input.Chars.Add(string.Format("'\\u{0:x4}'", (int)ch));
                        }
                    }
                }
            }

            // hoist rule bodies inside calls
            else if (node is AST.Call)
            {
                HoistCalledDisjunctions(currentRule, node as AST.Call);
            }

            // recurse
            if (node.Children != null)
            {
                foreach (AST.AstNode child in node.Children)
                {
                    if (child != null)
                        Analyze(child, currentRule);
                }
            }
        }

        void GenerateGrammarFile(TextWriter tw)
        {
            tw.WriteLine("//");

            if (add_timestamp)
                tw.WriteLine("// IronMeta {1} Parser; Generated {0} UTC", DateTime.UtcNow.ToString("u"), gName);
            else
                tw.WriteLine("// IronMeta {0} Parser", gName);

            tw.WriteLine("//");
            tw.WriteLine();

            // namespace usings
            GenerateNamespaceUsings(tw);

            // ignore uncommented
            tw.WriteLine("#pragma warning disable 0219");
            tw.WriteLine("#pragma warning disable 1591");
            tw.WriteLine();

            // open namespace
            string indent = string.Empty;

            if (!string.IsNullOrEmpty(gNamespace))
            {
                tw.WriteLine("namespace {0}", gNamespace);
                tw.WriteLine("{");
                tw.WriteLine();
                indent = "    ";
            }

            // using aliases
            GenerateUsingAliases(tw, indent);

            // generate grammar
            GenerateGrammar(tw, indent);

            // close namespace
            if (!string.IsNullOrEmpty(gNamespace))
            {
                tw.WriteLine("}} // namespace {0}", gNamespace);
                tw.WriteLine();
            }
        }

        void GenerateNamespaceUsings(TextWriter tw)
        {
            HashSet<string> usingsNeeded = new HashSet<string>();
            usingsNeeded.Add("System");
            usingsNeeded.Add("System.Collections.Generic");
            usingsNeeded.Add("System.Linq");
            usingsNeeded.Add("");
            usingsNeeded.Add("IronMeta.Matcher");

            if (grammar.Preamble != null && grammar.Preamble.Usings != null)
            {
                foreach (AST.Using use in grammar.Preamble.Usings)
                    usingsNeeded.Add(use.GetText());
            }

            foreach (string name in usingsNeeded)
                if (string.IsNullOrWhiteSpace(name))
                    tw.WriteLine();
                else
                    tw.WriteLine("using {0};", name);

            if (usingsNeeded.Count > 0)
                tw.WriteLine();
        }

        void GenerateUsingAliases(TextWriter tw, string indent)
        {
            tw.Write(indent); tw.WriteLine("using _{0}_Inputs = IEnumerable<{1}>;", gName, tInput);
            tw.Write(indent); tw.WriteLine("using _{0}_Results = IEnumerable<{1}>;", gName, tResult);
            tw.Write(indent); tw.WriteLine("using {0} = IronMeta.Matcher.MatchItem<{1}, {2}>;", tItem, tInput, tResult);
            tw.Write(indent); tw.WriteLine("using _{0}_Args = IEnumerable<IronMeta.Matcher.MatchItem<{1}, {2}>>;", gName, tInput, tResult);
            tw.Write(indent); tw.WriteLine("using _{0}_Memo = IronMeta.Matcher.MatchState<{1}, {2}>;", gName, tInput, tResult);
            tw.Write(indent); tw.WriteLine("using _{0}_Rule = System.Action<IronMeta.Matcher.MatchState<{1}, {2}>, int, IEnumerable<IronMeta.Matcher.MatchItem<{1}, {2}>>>;", gName, tInput, tResult);
            tw.Write(indent); tw.WriteLine("using _{0}_Base = IronMeta.Matcher.Matcher<{1}, {2}>;", gName, tInput, tResult);
            tw.WriteLine();
        }

        void GenerateItemClass(TextWriter tw, string indent)
        {
            tw.WriteLine();
            tw.Write(indent); tw.WriteLine("public class {2} : IronMeta.Matcher.MatchItem<{0}, {1}>", tInput, tResult);
            tw.Write(indent); tw.WriteLine("{");

            tw.Write(indent); tw.WriteLine("    public {0}() : base() {{ }}", tItem);
            tw.Write(indent); tw.WriteLine("    public {0}({1} input) : base(input) {{ }}", tItem, tInput);
            tw.Write(indent); tw.WriteLine("    public {2}({0} input, {1} result) : base(input, result) {{ }}", tInput, tResult, tItem);
            tw.Write(indent); tw.WriteLine("    public {0}(_{1}_Inputs inputs) : base(inputs) {{ }}", tItem, gName);
            tw.Write(indent); tw.WriteLine("    public {0}(_{1}_Inputs inputs, _{1}_Results results) : base(inputs, results) {{ }}", tItem, gName);
            tw.Write(indent); tw.WriteLine("    public {0}(int start, int next, _{1}_Inputs inputs, _{1}_Results results, bool relative) : base(start, next, inputs, results, relative) {{ }}", tItem, gName);
            tw.Write(indent); tw.WriteLine("    public {1}(int start, _{2}_Inputs inputs) : base(start, start, inputs, Enumerable.Empty<{0}>(), true) {{ }}", tResult, tItem, gName);
            tw.Write(indent); tw.WriteLine("    public {0}(_{1}_Rule production) : base(production) {{ }}", tItem, gName, tInput, tResult);
            tw.WriteLine();

            if (tResult != tInput && tInput.ToUpperInvariant() != "OBJECT")
            {
                tw.Write(indent); tw.WriteLine("    public static implicit operator {0}({1} item) {{ return item != null ? item.Inputs.LastOrDefault() : default({0}); }}", tInput, tItem);
            }

            if (tResult.ToUpperInvariant() != "OBJECT")
            {
                tw.Write(indent); tw.WriteLine("    public static implicit operator {0}({1} item) {{ return item != null ? item.Results.LastOrDefault() : default({0}); }}", tResult, tItem);
            }

            tw.Write(indent); tw.WriteLine("}");
            tw.WriteLine();
        }

        void GenerateGrammar(TextWriter tw, string indent)
        {
            // open matcher class
            tw.Write(indent); tw.WriteLine("public partial class {0} : {1}", gName, gBase);
            tw.Write(indent); tw.WriteLine("{");
            string innerIndent = indent + "    ";

            // generate constructor
            tw.Write(innerIndent); tw.WriteLine("public {0}()", gName);
            tw.Write(innerIndent); tw.WriteLine("    : base()");
            tw.Write(innerIndent); tw.WriteLine("{");
            tw.Write(innerIndent); tw.WriteLine("    _setTerminals();");
            tw.Write(innerIndent); tw.WriteLine("}");
            tw.WriteLine();

            tw.Write(innerIndent); tw.WriteLine("public {0}(bool handle_left_recursion)", gName);
            tw.Write(innerIndent); tw.WriteLine("    : base(handle_left_recursion)");
            tw.Write(innerIndent); tw.WriteLine("{");
            tw.Write(innerIndent); tw.WriteLine("    _setTerminals();");
            tw.Write(innerIndent); tw.WriteLine("}");
            tw.WriteLine();

            // set terminals
            var terminalMemo = new Dictionary<string, bool>();
            var terminals = ruleBodies.Keys.Where(ruleName =>
                {
                    var involved = new HashSet<string>();
                    return IsTerminal(ruleName, involved, terminalMemo);
                })
                .ToList();

            tw.Write(innerIndent); tw.WriteLine("void _setTerminals()");
            tw.Write(innerIndent); tw.WriteLine("{");
            tw.Write(innerIndent); tw.WriteLine("    this.Terminals = new HashSet<string>()", gName);
            tw.Write(innerIndent); tw.WriteLine("    {");
            foreach (var terminal in terminals.OrderBy(k => k))
            {
                tw.Write(innerIndent); tw.WriteLine("        \"{0}\",", terminal);
            }
            tw.Write(innerIndent); tw.WriteLine("    };");
            tw.Write(innerIndent); tw.WriteLine("}");
            tw.WriteLine();

            // generate rules
            foreach (var item in ruleBodies)
            {
                GenerateRule(tw, item.Key, item.Value, innerIndent);
            }

            // initialize regexps
            foreach (var kv in regexps)
            {
                tw.Write(innerIndent); tw.WriteLine("static readonly Verophyle.Regexp.StringRegexp {0} = new Verophyle.Regexp.StringRegexp(@\"{1}\");", kv.Value, kv.Key);
            }
            tw.WriteLine();

            // close class
            tw.Write(indent); tw.WriteLine("}} // class {0}", gName);
            tw.WriteLine();
        }

        bool IsTerminal(string ruleName, ISet<string> involved, IDictionary<string, bool> memo)
        {
            bool isTerminal;
            if (memo.TryGetValue(ruleName, out isTerminal))
                return isTerminal;

            if (involved.Contains(ruleName))
                return memo[ruleName] = false;

            involved.Add(ruleName);

            AST.AstNode body;
            if (!ruleBodies.TryGetValue(ruleName, out body))
                return false;

            var calls = body.GetAllChildren()
                .Select(child =>
                {
                    if (child is AST.Call)
                    {
                        return ((AST.Call)child).Rule.Results.Single().GetText();
                    }
                    else if (child is AST.CallOrVar)
                    {
                        return ((AST.CallOrVar)child).Name.Results.Single().GetText();
                    }
                    return null;
                })
                .Where(name => !string.IsNullOrWhiteSpace(name))
                .Distinct();

            foreach (var call in calls)
            {
                if (!IsTerminal(call, involved, memo))
                    return memo[ruleName] = false;
            }

            return memo[ruleName] = true;
        }

        void GenerateRule(TextWriter tw, string ruleName, AST.AstNode body, string indent)
        {
            // generate rule
            tw.WriteLine();
            tw.Write(indent); tw.WriteLine("public {2}void {0}(_{1}_Memo _memo, int _index, _{1}_Args _args)", ruleName, gName, overrides.ContainsKey(ruleName) ? (overrides[ruleName] + " ") : "");
            tw.Write(indent); tw.WriteLine("{");
            tw.WriteLine();

            string innerIndent = indent + "    ";

            // body
            using (StringWriter sw = new StringWriter())
            {
                HashSet<string> variables = new HashSet<string>();
                int n = 0;
                bool use_args = false;
                GenerateBody(sw, variables, body, ref n, ref use_args, false, innerIndent);

                tw.Write(innerIndent); tw.WriteLine("int _arg_index = 0;");
                tw.Write(innerIndent); tw.WriteLine("int _arg_input_index = 0;");
                tw.WriteLine();

                foreach (string v in variables)
                {
                    tw.Write(innerIndent); tw.WriteLine("{1} {0} = null;", v, tItem);
                }

                if (variables.Count > 0)
                    tw.WriteLine();

                tw.Write(sw.ToString());
            }

            // close
            tw.Write(indent); tw.WriteLine("}");
            tw.WriteLine();
        }

        void GenerateBody(TextWriter tw, HashSet<string> vars, AST.AstNode node, ref int n, ref bool use_args, bool match_args, string indent)
        {
            int outer_n = n;

            #region PRE: Giant Switch Statement

            // and/or header
            if (node is AST.And)
            {
                GenerateAndPre(tw, node as AST.And, n, match_args, indent);
            }
            else if (node is AST.Or)
            {
                GenerateOrPre(tw, node as AST.Or, n, match_args, indent);
            }
            else if (node is AST.Look)
            {
                GenerateLookPre(tw, node as AST.Look, n, match_args, indent);
            }
            else if (node is AST.Not)
            {
                GenerateNotPre(tw, node as AST.Not, n, match_args, indent);
            }
            else if (node is AST.Star)
            {
                GenerateStarPre(tw, node as AST.Star, n, match_args, indent);
            }
            else if (node is AST.Plus)
            {
                GeneratePlusPre(tw, node as AST.Plus, n, match_args, indent);
            }
            else if (node is AST.Cond)
            {
                GenerateCondPre(tw, node as AST.Cond, n, match_args, indent);
            }
            else if (node is AST.Args)
            {
                GenerateArgsPre(tw, vars, node as AST.Args, ref n, ref use_args, match_args, indent);
            }

            #endregion

            // generate in post order
            if (node.Children != null)
            {
                foreach (AST.AstNode child in node.Children)
                {
                    int cur_n = n++;

                    // children
                    GenerateBody(tw, vars, child, ref n, ref use_args, match_args, indent);

                    // shortcut and/or
                    if (cur_n == outer_n && node is AST.And)
                    {
                        GenerateAndShortcut(tw, node as AST.And, outer_n, match_args, indent);
                    }
                    else if (cur_n == outer_n && node is AST.Or)
                    {
                        GenerateOrShortcut(tw, node as AST.Or, outer_n, match_args, indent);
                    }
                }
            }

            #region POST: Giant Switch Statement
            //
            if (node is AST.Code)
            {
                GenerateLiteralPost(tw, node as AST.Code, outer_n, match_args, indent);
            }
            else if (node is AST.Regexp)
            {
                GenerateRegexpPost(tw, node as AST.Regexp, outer_n, match_args, indent);
            }
            else if (node is AST.Fail)
            {
                GenerateFailPost(tw, node as AST.Fail, outer_n, match_args, indent);
            }
            else if (node is AST.Any)
            {
                GenerateAnyPost(tw, node as AST.Any, outer_n, match_args, indent);
            }
            else if (node is AST.Look)
            {
                GenerateLookPost(tw, node as AST.Look, outer_n, match_args, indent);
            }
            else if (node is AST.Not)
            {
                GenerateNotPost(tw, node as AST.Not, outer_n, match_args, indent);
            }
            else if (node is AST.Or)
            {
                GenerateOrPost(tw, node as AST.Or, outer_n, match_args, indent);
            }
            else if (node is AST.And)
            {
                GenerateAndPost(tw, node as AST.And, outer_n, match_args, indent);
            }
            else if (node is AST.Star)
            {
                GenerateStarPost(tw, node as AST.Star, outer_n, match_args, indent);
            }
            else if (node is AST.Plus)
            {
                GeneratePlusPost(tw, node as AST.Plus, outer_n, match_args, indent);
            }
            else if (node is AST.Ques)
            {
                GenerateQuesPost(tw, node as AST.Ques, outer_n, match_args, indent);
            }
            else if (node is AST.CallOrVar)
            {
                GenerateCallOrVarPost(tw, vars, node as AST.CallOrVar, outer_n, match_args, indent);
            }
            else if (node is AST.Call)
            {
                GenerateCallPost(tw, vars, node as AST.Call, outer_n, match_args, indent);
            }
            else if (node is AST.Bind)
            {
                GenerateBindPost(tw, vars, node as AST.Bind, outer_n, match_args, indent);
            }
            else if (node is AST.Cond)
            {
                GenerateCondPost(tw, node as AST.Cond, outer_n, match_args, indent);
            }
            else if (node is AST.Act)
            {
                GenerateActPost(tw, node as AST.Act, outer_n, match_args, indent);
            }
            else if (node is AST.InputClass)
            {
                GenerateInputClassPost(tw, node as AST.InputClass, outer_n, match_args, indent);
            }
            else if (node is AST.Args)
            {
                GenerateArgsPost(tw, vars, node as AST.Args, outer_n, match_args, indent);
            }
            else
            {
                throw new Exception("Unknown AST node type: " + node.GetType().FullName);
            }
            #endregion
        }

        #region LITERAL

        void GenerateLiteralPost(TextWriter tw, AST.Code node, int n, bool match_args, string indent)
        {
            string literal = TrimBraces(node.GetText().Trim()).Trim();

            tw.Write(indent); tw.WriteLine("// LITERAL {0}", literal);

            if (match_args)
            {
                tw.Write(indent); tw.WriteLine("_ParseLiteralArgs(_memo, ref _arg_index, ref _arg_input_index, {0}, _args);", literal);
            }
            else
            {
                bool isCharMatcher = tInput == "char" || tInput.EndsWith("Character");

                tw.Write(indent);
                if (isCharMatcher && literal.StartsWith("\""))
                    tw.WriteLine("_ParseLiteralString(_memo, ref _index, {0});", literal);
                else if (isCharMatcher && literal.StartsWith("'"))
                    tw.WriteLine("_ParseLiteralChar(_memo, ref _index, {0});", literal);
                else
                    tw.WriteLine("_ParseLiteralObj(_memo, ref _index, {0});", literal);
            }

            tw.WriteLine();
        }

        #endregion

        #region REGEXP

        void GenerateRegexpPost(TextWriter tw, AST.Regexp node, int n, bool match_args, string indent)
        {
            bool isCharMatcher = tInput == "char" || tInput.EndsWith("Character");
            if (!isCharMatcher) throw new Exception("Can only use regular expressions when matching characters.");
            if (match_args) throw new Exception("Cannot use a regular expression in rule arguments.");

            var text = node.GetText().Trim(' ', '/').Replace("\"", "\"\"");
            string name;

            if (!regexps.TryGetValue(text, out name))
            {
                name = string.Format("_re{0}", regexps.Count);
                regexps.Add(text, name);
            }

            tw.Write(indent); tw.WriteLine("// REGEXP {0}", text);
            tw.Write(indent); tw.WriteLine("_ParseRegexp(_memo, ref _index, {0});", name);
            tw.WriteLine();
        }

        #endregion

        #region INPUT_CLASS

        void GenerateInputClassPost(TextWriter tw, AST.InputClass node, int n, bool match_args, string indent)
        {
            tw.Write(indent); tw.WriteLine("// INPUT CLASS");
            tw.Write(indent);

            if (match_args)
                tw.WriteLine("_ParseInputClassArgs(_memo, ref _arg_index, ref _arg_input_index, _args, {0});", string.Join(", ", node.Chars.ToArray()));
            else
                tw.WriteLine("_ParseInputClass(_memo, ref _index, {0});", string.Join(", ", node.Chars.ToArray()));

            tw.WriteLine();
        }

        #endregion

        #region FAIL

        void GenerateFailPost(TextWriter tw, AST.Fail node, int n, bool match_args, string indent)
        {
            tw.Write(indent); tw.WriteLine("// FAIL");

            tw.Write(indent);
            if (match_args)
                tw.WriteLine("_memo.ArgResults.Push(null);");
            else
                tw.WriteLine("_memo.Results.Push(null);");

            if (!string.IsNullOrEmpty(node.Message))
            {
                var msg = node.Message;
                if (msg.StartsWith("{") && !msg.Contains("return"))
                    msg = "{ return " + msg.Substring(1, msg.Length - 2) + "; }";

                tw.Write(indent); tw.WriteLine("_memo.ClearErrors();");
                tw.Write(indent); tw.WriteLine("_memo.AddError(_index, () => {0});", msg);
            }

            tw.WriteLine();
        }

        #endregion

        #region ANY

        void GenerateAnyPost(TextWriter tw, AST.Any node, int n, bool match_args, string indent)
        {
            tw.Write(indent); tw.WriteLine("// ANY");
            tw.Write(indent);

            if (match_args)
                tw.WriteLine("_ParseAnyArgs(_memo, ref _arg_index, ref _arg_input_index, _args);");
            else
                tw.WriteLine("_ParseAny(_memo, ref _index);");

            tw.WriteLine();
        }

        #endregion

        #region LOOK

        void GenerateLookPre(TextWriter tw, AST.Look node, int n, bool match_args, string indent)
        {
            tw.Write(indent); tw.WriteLine("// LOOK {0}", n);
            tw.Write(indent);

            if (match_args)
                 tw.WriteLine("int _start_i{0} = _arg_index;", n);
            else
                tw.WriteLine("int _start_i{0} = _index;", n);

            tw.WriteLine();
        }

        void GenerateLookPost(TextWriter tw, AST.Look node, int n, bool match_args, string indent)
        {
            tw.Write(indent); tw.WriteLine("// LOOK {0}", n);

            if (match_args)
            {
                tw.Write(indent); tw.WriteLine("var _r{0} = _memo.ArgResults.Pop();", n);
                tw.Write(indent); tw.WriteLine("_memo.ArgResults.Push(_r{0});", n);
                tw.Write(indent); tw.WriteLine("_arg_index = _start_i{0};", n);
            }
            else
            {
                tw.Write(indent); tw.WriteLine("var _r{0} = _memo.Results.Pop();", n);
                tw.Write(indent); tw.WriteLine("_memo.Results.Push( _r{0} != null ? new {1}(_start_i{0}, _memo.InputEnumerable) : null );", n, tItem);
                tw.Write(indent); tw.WriteLine("_index = _start_i{0};", n);
            }

            tw.WriteLine();
        }

        #endregion

        #region NOT

        void GenerateNotPre(TextWriter tw, AST.Not node, int n, bool match_args, string indent)
        {
            tw.Write(indent); tw.WriteLine("// NOT {0}", n);
            tw.Write(indent);

            if (match_args)
                tw.WriteLine("int _start_i{0} = _arg_index;", n);
            else
                tw.WriteLine("int _start_i{0} = _index;", n);

            tw.WriteLine();
        }

        void GenerateNotPost(TextWriter tw, AST.Not node, int n, bool match_args, string indent)
        {
            tw.Write(indent); tw.WriteLine("// NOT {0}", n);

            if (match_args)
            {
                tw.Write(indent); tw.WriteLine("var _r{0} = _memo.ArgResults.Pop();", n);
                tw.Write(indent); tw.WriteLine("_memo.ArgResults.Push(_r{0} == null ? new {1}(_arg_index, _arg_index, _memo.InputEnumerable, Enumerable.Empty<{2}>(), false) : null);", n, tItem, tResult);
                tw.Write(indent); tw.WriteLine("_arg_index = _start_i{0};", n);
            }
            else
            {
                tw.Write(indent); tw.WriteLine("var _r{0} = _memo.Results.Pop();", n);
                tw.Write(indent); tw.WriteLine("_memo.Results.Push( _r{0} == null ? new {1}(_start_i{0}, _memo.InputEnumerable) : null);", n, tItem);
                tw.Write(indent); tw.WriteLine("_index = _start_i{0};", n);
            }

            tw.WriteLine();
        }

        #endregion

        #region OR

        void GenerateOrPre(TextWriter tw, AST.Or node, int n, bool match_args, string indent)
        {
            tw.Write(indent); tw.WriteLine("// OR {0}", n);
            tw.Write(indent);

            if (match_args)
                tw.WriteLine("int _start_i{0} = _arg_index;", n);
            else
                tw.WriteLine("int _start_i{0} = _index;", n);

            tw.WriteLine();
        }

        void GenerateOrShortcut(TextWriter tw, AST.Or node, int n, bool match_args, string indent)
        {
            tw.Write(indent); tw.WriteLine("// OR shortcut");
            tw.Write(indent);

            if (match_args)
                tw.WriteLine("if (_memo.ArgResults.Peek() == null) {{ _memo.ArgResults.Pop(); _arg_index = _start_i{0}; }} else goto label{0};", n);
            else
                tw.WriteLine("if (_memo.Results.Peek() == null) {{ _memo.Results.Pop(); _index = _start_i{0}; }} else goto label{0};", n);

            tw.WriteLine();
        }

        void GenerateOrPost(TextWriter tw, AST.Or node, int n, bool match_args, string indent)
        {
            tw.Write(indent.Substring(4)); tw.WriteLine("label{0}: // OR", n);
            tw.Write(indent); tw.WriteLine("int _dummy_i{0} = _index; // no-op for label", n);
            tw.WriteLine();
        }

        #endregion

        #region AND

        void GenerateAndPre(TextWriter tw, AST.And node, int n, bool match_args, string indent)
        {
            tw.Write(indent); tw.WriteLine("// AND {0}", n);
            tw.Write(indent);

            if (match_args)
                tw.WriteLine("int _start_i{0} = _arg_index;", n);
            else
                tw.WriteLine("int _start_i{0} = _index;", n);

            tw.WriteLine();
        }

        void GenerateAndShortcut(TextWriter tw, AST.And node, int outer_n, bool match_args, string indent)
        {
            tw.Write(indent); tw.WriteLine("// AND shortcut");
            tw.Write(indent);

            if (match_args)
                tw.WriteLine("if (_memo.ArgResults.Peek() == null) {{ _memo.ArgResults.Push(null); goto label{0}; }}", outer_n);
            else
                tw.WriteLine("if (_memo.Results.Peek() == null) {{ _memo.Results.Push(null); goto label{0}; }}", outer_n);

            tw.WriteLine();
        }

        void GenerateAndPost(TextWriter tw, AST.And node, int n, bool match_args, string indent)
        {
            tw.Write(indent.Substring(4)); tw.WriteLine("label{0}: // AND", n);

            if (match_args)
            {
                tw.Write(indent); tw.WriteLine("var _r{0}_2 = _memo.ArgResults.Pop();", n);
                tw.Write(indent); tw.WriteLine("var _r{0}_1 = _memo.ArgResults.Pop();", n);
                tw.WriteLine();
                tw.Write(indent); tw.WriteLine("if (_r{0}_1 != null && _r{0}_2 != null)", n);
                tw.Write(indent); tw.WriteLine("{");
                tw.Write(indent); tw.WriteLine("    _memo.ArgResults.Push(new {1}(_start_i{0}, _arg_index, _r{0}_1.Inputs.Concat(_r{0}_2.Inputs), _r{0}_1.Results.Concat(_r{0}_2.Results).Where(_NON_NULL), false));", n, tItem);
                tw.Write(indent); tw.WriteLine("}");
                tw.Write(indent); tw.WriteLine("else");
                tw.Write(indent); tw.WriteLine("{");
                tw.Write(indent); tw.WriteLine("    _memo.ArgResults.Push(null);");
                tw.Write(indent); tw.WriteLine("    _arg_index = _start_i{0};", n);
                tw.Write(indent); tw.WriteLine("}");
            }
            else
            {
                tw.Write(indent); tw.WriteLine("var _r{0}_2 = _memo.Results.Pop();", n);
                tw.Write(indent); tw.WriteLine("var _r{0}_1 = _memo.Results.Pop();", n);
                tw.WriteLine();
                tw.Write(indent); tw.WriteLine("if (_r{0}_1 != null && _r{0}_2 != null)", n);
                tw.Write(indent); tw.WriteLine("{");
                tw.Write(indent); tw.WriteLine("    _memo.Results.Push( new {1}(_start_i{0}, _index, _memo.InputEnumerable, _r{0}_1.Results.Concat(_r{0}_2.Results).Where(_NON_NULL), true) );", n, tItem);
                tw.Write(indent); tw.WriteLine("}");
                tw.Write(indent); tw.WriteLine("else");
                tw.Write(indent); tw.WriteLine("{");
                tw.Write(indent); tw.WriteLine("    _memo.Results.Push(null);");
                tw.Write(indent); tw.WriteLine("    _index = _start_i{0};", n);
                tw.Write(indent); tw.WriteLine("}");
            }

            tw.WriteLine();
        }

        #endregion

        #region STAR

        void GenerateStarPre(TextWriter tw, AST.Star node, int n, bool match_args, string indent)
        {
            tw.Write(indent); tw.WriteLine("// STAR {0}", n);

            if (match_args)
            {
                tw.Write(indent); tw.WriteLine("int _start_i{0} = _arg_index;", n);
                tw.Write(indent); tw.WriteLine("var _inp{0} = Enumerable.Empty<{1}>();", n, tInput);
                tw.Write(indent); tw.WriteLine("var _res{0} = Enumerable.Empty<{1}>();", n, tResult);
                tw.Write(indent.Substring(4)); tw.WriteLine("label{0}:", n);
            }
            else
            {
                tw.Write(indent); tw.WriteLine("int _start_i{0} = _index;", n);
                tw.Write(indent); tw.WriteLine("var _res{0} = Enumerable.Empty<{1}>();", n, tResult);
                tw.Write(indent.Substring(4)); tw.WriteLine("label{0}:", n);
            }

            tw.WriteLine();
        }

        void GenerateStarPost(TextWriter tw, AST.Star node, int n, bool match_args, string indent)
        {
            tw.Write(indent); tw.WriteLine("// STAR {0}", n);

            if (match_args)
            {
                tw.Write(indent); tw.WriteLine("var _r{0} = _memo.ArgResults.Pop();", n);
                tw.Write(indent); tw.WriteLine("if (_r{0} != null)", n);
                tw.Write(indent); tw.WriteLine("{");
                tw.Write(indent); tw.WriteLine("    _inp{0} = _inp{0}.Concat(_r{0}.Inputs);", n);
                tw.Write(indent); tw.WriteLine("    _res{0} = _res{0}.Concat(_r{0}.Results);", n);
                tw.Write(indent); tw.WriteLine("    goto label{0};", n);
                tw.Write(indent); tw.WriteLine("}");
                tw.Write(indent); tw.WriteLine("else");
                tw.Write(indent); tw.WriteLine("{");
                tw.Write(indent); tw.WriteLine("    _memo.ArgResults.Push(new {1}(_start_i{0}, _arg_index, _inp{0}, _res{0}.Where(_NON_NULL), false));", n, tItem);
                tw.Write(indent); tw.WriteLine("}");
            }
            else
            {
                tw.Write(indent); tw.WriteLine("var _r{0} = _memo.Results.Pop();", n);
                tw.Write(indent); tw.WriteLine("if (_r{0} != null)", n);
                tw.Write(indent); tw.WriteLine("{");
                tw.Write(indent); tw.WriteLine("    _res{0} = _res{0}.Concat(_r{0}.Results);", n);
                tw.Write(indent); tw.WriteLine("    goto label{0};", n);
                tw.Write(indent); tw.WriteLine("}");
                tw.Write(indent); tw.WriteLine("else");
                tw.Write(indent); tw.WriteLine("{");
                tw.Write(indent); tw.WriteLine("    _memo.Results.Push(new {1}(_start_i{0}, _index, _memo.InputEnumerable, _res{0}.Where(_NON_NULL), true));", n, tItem);
                tw.Write(indent); tw.WriteLine("}");
            }

            tw.WriteLine();
        }

        #endregion

        #region PLUS

        void GeneratePlusPre(TextWriter tw, AST.Plus node, int n, bool match_args, string indent)
        {
            tw.Write(indent); tw.WriteLine("// PLUS {0}", n);

            if (match_args)
            {
                tw.Write(indent); tw.WriteLine("int _start_i{0} = _arg_index;", n);
                tw.Write(indent); tw.WriteLine("var _inp{0} = Enumerable.Empty<{1}>();", n, tInput);
                tw.Write(indent); tw.WriteLine("var _res{0} = Enumerable.Empty<{1}>();", n, tResult);
                tw.Write(indent.Substring(4)); tw.WriteLine("label{0}:", n);
            }
            else
            {
                tw.Write(indent); tw.WriteLine("int _start_i{0} = _index;", n);
                tw.Write(indent); tw.WriteLine("var _res{0} = Enumerable.Empty<{1}>();", n, tResult);
                tw.Write(indent.Substring(4)); tw.WriteLine("label{0}:", n);
            }

            tw.WriteLine();
        }

        void GeneratePlusPost(TextWriter tw, AST.Plus node, int n, bool match_args, string indent)
        {
            tw.Write(indent); tw.WriteLine("// PLUS {0}", n);

            if (match_args)
            {
                tw.Write(indent); tw.WriteLine("var _r{0} = _memo.ArgResults.Pop();", n);
                tw.Write(indent); tw.WriteLine("if (_r{0} != null)", n);
                tw.Write(indent); tw.WriteLine("{");
                tw.Write(indent); tw.WriteLine("    _inp{0} = _inp{0}.Concat(_r{0}.Inputs);", n);
                tw.Write(indent); tw.WriteLine("    _res{0} = _res{0}.Concat(_r{0}.Results);", n);
                tw.Write(indent); tw.WriteLine("    goto label{0};", n);
                tw.Write(indent); tw.WriteLine("}");
                tw.Write(indent); tw.WriteLine("else");
                tw.Write(indent); tw.WriteLine("{");
                tw.Write(indent); tw.WriteLine("    if (_arg_index > _start_i{0})", n);
                tw.Write(indent); tw.WriteLine("        _memo.ArgResults.Push(new {1}(_start_i{0}, _arg_index, _inp{0}, _res{0}.Where(_NON_NULL), false));", n, tItem);
                tw.Write(indent); tw.WriteLine("    else");
                tw.Write(indent); tw.WriteLine("        _memo.ArgResults.Push(null);");
                tw.Write(indent); tw.WriteLine("}");
            }
            else
            {
                tw.Write(indent); tw.WriteLine("var _r{0} = _memo.Results.Pop();", n);
                tw.Write(indent); tw.WriteLine("if (_r{0} != null)", n);
                tw.Write(indent); tw.WriteLine("{");
                tw.Write(indent); tw.WriteLine("    _res{0} = _res{0}.Concat(_r{0}.Results);", n);
                tw.Write(indent); tw.WriteLine("    goto label{0};", n);
                tw.Write(indent); tw.WriteLine("}");
                tw.Write(indent); tw.WriteLine("else");
                tw.Write(indent); tw.WriteLine("{");
                tw.Write(indent); tw.WriteLine("    if (_index > _start_i{0})", n);
                tw.Write(indent); tw.WriteLine("        _memo.Results.Push(new {1}(_start_i{0}, _index, _memo.InputEnumerable, _res{0}.Where(_NON_NULL), true));", n, tItem);
                tw.Write(indent); tw.WriteLine("    else");
                tw.Write(indent); tw.WriteLine("        _memo.Results.Push(null);");
                tw.Write(indent); tw.WriteLine("}");
            }

            tw.WriteLine();
        }

        #endregion

        #region QUES

        void GenerateQuesPost(TextWriter tw, AST.Ques node, int n, bool match_args, string indent)
        {
            tw.Write(indent); tw.WriteLine("// QUES");
            tw.Write(indent);

            if (match_args)
                tw.WriteLine("if (_memo.ArgResults.Peek() == null) {{ _memo.ArgResults.Pop(); _memo.ArgResults.Push(new {0}(_arg_index, _memo.InputEnumerable)); }}", tItem);
            else
                tw.WriteLine("if (_memo.Results.Peek() == null) {{ _memo.Results.Pop(); _memo.Results.Push(new {0}(_index, _memo.InputEnumerable)); }}", tItem);

            tw.WriteLine();
        }

        #endregion

        #region COND

        void GenerateCondPre(TextWriter tw, AST.Cond node, int n, bool match_args, string indent)
        {
            tw.Write(indent); tw.WriteLine("// COND {0}", n);
            tw.Write(indent);

            if (match_args)
                tw.WriteLine("int _start_i{0} = _arg_index;", n);
            else
                tw.WriteLine("int _start_i{0} = _index;", n);

            tw.WriteLine();
        }

        void GenerateCondPost(TextWriter tw, AST.Cond node, int n, bool match_args, string indent)
        {
            string condition = TrimBraces(node.GetText().Trim()).Trim();
            condition = condition.Replace("\t", "    ");

            tw.Write(indent); tw.WriteLine("// COND");

            string index = match_args ? "_arg_index" : "_index";
            string results = match_args ? "_memo.ArgResults" : "_memo.Results";

            if (condition.Contains("return") || condition.Contains("_IM_Result"))
            {
                if (!condition.Contains("return"))
                    condition = "return " + condition;
                if (!condition.EndsWith(";"))
                    condition = condition + ";";

                tw.Write(indent); tw.WriteLine("Func<{2}, bool> lambda{0} = (_IM_Result) => {{ {1} }};", n, condition, tItem);
                tw.Write(indent); tw.WriteLine("if ({1}.Peek() == null || !lambda{0}({1}.Peek())) {{ {1}.Pop(); {1}.Push(null); {2} = _start_i{0}; }}", n, results, index);
            }
            else
            {
                tw.Write(indent); tw.WriteLine("if ({2}.Peek() == null || !({1})) {{ {2}.Pop(); {2}.Push(null); {3} = _start_i{0}; }}", n, condition, results, index);
            }

            tw.WriteLine();
        }

        #endregion

        #region ACTION

        string TrimBraces(string s)
        {
            if ((s.StartsWith("(") && s.EndsWith(")")) || (s.StartsWith("{") && s.EndsWith("}")))
                s = s.Substring(1, s.Length - 2);
            return s;
        }

        void GenerateActPost(TextWriter tw, AST.Act node, int n, bool match_args, string indent)
        {
            string action = TrimBraces(node.GetText().Trim()).Trim();
            action = action.Replace("\t", "    ");

            if (!action.Contains("return"))
                action = "return " + action + ";";

            tw.Write(indent); tw.WriteLine("// ACT");

            if (match_args)
            {
                tw.Write(indent); tw.WriteLine("var _r{0} = _memo.ArgResults.Peek();", n);
                tw.Write(indent); tw.WriteLine("if (_r{0} != null)", n);
                tw.Write(indent); tw.WriteLine("{");
                tw.Write(indent); tw.WriteLine("    _memo.ArgResults.Pop();");
                tw.Write(indent); tw.WriteLine("    _memo.ArgResults.Push( new {1}(_r{0}.StartIndex, _r{0}.NextIndex, _r{0}.Inputs, _Thunk(_IM_Result => {{ {2} }}, _r{0}), false) );", n, tItem, action);
                tw.Write(indent); tw.WriteLine("}");
            }
            else
            {
                tw.Write(indent); tw.WriteLine("var _r{0} = _memo.Results.Peek();", n);
                tw.Write(indent); tw.WriteLine("if (_r{0} != null)", n);
                tw.Write(indent); tw.WriteLine("{");
                tw.Write(indent); tw.WriteLine("    _memo.Results.Pop();");
                tw.Write(indent); tw.WriteLine("    _memo.Results.Push( new {2}(_r{0}.StartIndex, _r{0}.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => {{ {1} }}, _r{0}), true) );", n, action, tItem);
                tw.Write(indent); tw.WriteLine("}");
            }

            tw.WriteLine();
        }

        #endregion

        #region CALL

        void GenerateCallPost(TextWriter tw, HashSet<string> vars, AST.Call node, int n, bool match_args, string indent)
        {
            string name = node.Rule.Results.Single().GetText().Trim();

            tw.Write(indent); tw.WriteLine("// CALL {0}", name);

            if (match_args)
            {
                throw new Exception(string.Format("{0}: you are not allowed to call rules in argument patterns.", name));
            }
            else
            {
                tw.Write(indent); tw.WriteLine("var _start_i{0} = _index;", n);
                tw.Write(indent); tw.WriteLine("{1} _r{0};", n, tItem);

                bool isVar = vars.Contains(name);

                if (node.Params != null && node.Params.Any())
                {
                    var actualParams = new List<string>();
                    GenerateActualParams(tw, vars, node, n, name, indent, actualParams);

                    tw.WriteLine();
                    tw.Write(indent); tw.WriteLine("_{3}_Args _actual_args{0} = new {1}[] {{ {2} }};", n, tItem, string.Join(", ", actualParams), gName);
                    tw.Write(indent); tw.WriteLine("if (_args != null) _actual_args{0} = _actual_args{0}.Concat(_args.Skip(_arg_index));", n);

                    tw.Write(indent); tw.WriteLine("_r{3} = _MemoCall(_memo, {0}, _index, {4}, _actual_args{3});",
                        isVar ? name + ".ProductionName" : "\"" + name + "\"",
                        string.Join(", ", actualParams.ToArray()),
                        tItem,
                        n,
                        isVar ? name + ".Production" : name);
                }
                else
                {
                    tw.Write(indent); tw.WriteLine("_r{1} = _MemoCall(_memo, {0}, _index, {2}, _args != null ? _args.Skip(_arg_index) : null);",
                        isVar ? name + ".ProductionName" : "\"" + name + "\"",
                        n,
                        isVar ? name + ".Production" : name);
                }

                tw.WriteLine();
                tw.Write(indent); tw.WriteLine("if (_r{0} != null) _index = _r{0}.NextIndex;", n);
                tw.WriteLine();

            }
        }

        void GenerateActualParams(TextWriter tw, HashSet<string> vars, AST.Call node, int n, string name, string indent, IList<string> actualParams)
        {
            int i = 0;
            foreach (AST.AstNode pnode in node.Params)
            {
                string pstr = pnode.GetText().Trim();

                if (pnode is AST.CallOrVar || pnode is AST.Call)
                {
                    actualParams.Add(vars.Contains(pstr) ? pstr : string.Format("new {0}({1})", tItem, pstr));
                }
                else
                {
                    pstr = TrimBraces(pstr).Trim();
                    if (!string.IsNullOrWhiteSpace(pstr))
                    {
                        actualParams.Add(string.Format("new {2}(_arg{0}_{1})", n, i, tItem));
                        tw.Write(indent); tw.WriteLine("var _arg{0}_{1} = {2};", n, i, pstr);
                    }
                    ++i;
                }
            }
            if (node.Captures != null)
            {
                foreach (var captured in node.Captures)
                    actualParams.Add(new string(captured.Inputs.ToArray()).Trim());
            }

            if (actualParams.Count == 0 && node.Params.Any())
                throw new Exception("Unable to process actual parameters for call to " + name);
        }

        int nextHoistedRuleId = 0;

        void HoistCalledDisjunctions(AST.Rule currentRule, AST.Call callNode)
        {
            var newParams = new List<AST.AstNode>();
            var captures = new List<Matcher.MatchItem<char, AST.AstNode>>();
            callNode.Captures = captures;

            foreach (var oldParam in callNode.Params)
            {
                if (oldParam is AST.And || oldParam is AST.Or)
                {
                    AST.AstNode ruleBody = oldParam;

                    // find callorvars in the nested rule body, and match them to params of this current rule
                    var outerArgs = currentRule.Body as AST.Args;
                    if (outerArgs != null)
                    {
                        var outerVarNames = outerArgs.Parms.OfType<AST.Bind>()
                            .Select(b => new string(b.VarName.Inputs.ToArray()).Trim())
                            .ToList();
                        var closedVarNames = ruleBody.OfType<AST.CallOrVar>()
                            .Where(cov =>
                            {
                                var innerVarName = new string(cov.Name.Inputs.ToArray()).Trim();
                                return outerVarNames.Contains(innerVarName);
                            })
                            .Select(cov => cov.Items.First())
                            .ToList();

                        if (closedVarNames.Any())
                        {
                            AST.AstNode args = new AST.Bind(new AST.Any(), closedVarNames.First());
                            captures.Add(closedVarNames.First());

                            foreach (var varName in closedVarNames.Skip(1))
                            {
                                args = new AST.And(args, new AST.Bind(new AST.Any(), varName));
                                captures.Add(varName);
                            }
                            ruleBody = new AST.Args(args, ruleBody);
                        }
                    }

                    var ruleName = string.Format("__nested_rule_{0}", nextHoistedRuleId++);
                    var newRule = new AST.Rule(ruleName, ruleBody);
                    ruleNodes.Add(newRule);
                    ruleBodies[ruleName] = ruleBody;

                    var nameItem = new Matcher.MatchItem<char, AST.AstNode>(ruleName)
                    {
                        StartIndex = 0,
                        NextIndex = ruleName.Length,
                    };
                    var newCall = new AST.Call(nameItem, new AST.AstNode[0]);
                    newParams.Add(newCall);
                }
                else
                {
                    newParams.Add(oldParam);
                }
            }

            callNode.Params = newParams;
        }

        #endregion CALL

        #region BIND

        void GenerateBindPost(TextWriter tw, HashSet<string> vars, AST.Bind node, int n, bool match_args, string indent)
        {
            string name = node.GetText().Trim();

            tw.Write(indent); tw.WriteLine("// BIND {0}", name);
            tw.Write(indent);

            if (match_args)
                tw.WriteLine("{0} = _memo.ArgResults.Peek();", name);
            else
                tw.WriteLine("{0} = _memo.Results.Peek();", name);
            tw.WriteLine();

            vars.Add(name);
        }

        #endregion

        #region CALLORVAR

        void GenerateCallOrVarPost(TextWriter tw, HashSet<string> vars, AST.CallOrVar node, int n, bool match_args, string indent)
        {
            string name = node.Name.Results.Single().GetText().Trim();

            tw.Write(indent); tw.WriteLine("// CALLORVAR {0}", name);

            if (match_args)
            {
                if (vars.Contains(name))
                {
                    tw.Write(indent); tw.WriteLine("var _r{0} = _ParseLiteralArgs(_memo, ref _arg_index, ref _arg_input_index, {1}.Inputs, _args);", n, name);
                    tw.Write(indent); tw.WriteLine("if (_r{0} != null) _arg_index = _r{0}.NextIndex;", n);
                }
                else
                {
                    throw new Exception(string.Format("{0}: you are not allowed to call rules in argument patterns.", name));
                }
            }
            else
            {
                tw.Write(indent); tw.WriteLine("{1} _r{0};", n, tItem);
                tw.WriteLine();

                if (vars.Contains(name))
                {
                    tw.Write(indent); tw.WriteLine("if ({0}.Production != null)", name);
                    tw.Write(indent); tw.WriteLine("{");
                    tw.Write(indent); tw.WriteLine("    var _p{0} = (System.Action<_{3}_Memo, int, IEnumerable<{1}>>)(object){2}.Production;", n, tItem, name, gName);
                    tw.Write(indent); tw.WriteLine("    _r{0} = _MemoCall(_memo, {1}.Production.Method.Name, _index, _p{0}, _args != null ? _args.Skip(_arg_index) : null);", n, name);
                    tw.Write(indent); tw.WriteLine("}");
                    tw.Write(indent); tw.WriteLine("else");
                    tw.Write(indent); tw.WriteLine("{");
                    tw.Write(indent); tw.WriteLine("    _r{0} = _ParseLiteralObj(_memo, ref _index, {1}.Inputs);", n, name);
                    tw.Write(indent); tw.WriteLine("}");
                }
                else
                {
                    tw.Write(indent); tw.WriteLine("_r{1} = _MemoCall(_memo, \"{0}\", _index, {0}, null);", name, n);
                }

                tw.WriteLine();
                tw.Write(indent); tw.WriteLine("if (_r{0} != null) _index = _r{0}.NextIndex;", n);
            }

            tw.WriteLine();
        }

        #endregion

        #region ARGS

        void GenerateArgsPre(TextWriter tw, HashSet<string> vars, AST.Args node, ref int n, ref bool use_args, bool match_args, string indent)
        {
            if (node.Parms != null)
            {
                use_args = true;

                tw.Write(indent); tw.WriteLine("// ARGS {0}", n);
                tw.Write(indent); tw.WriteLine("_arg_index = 0;");
                tw.Write(indent); tw.WriteLine("_arg_input_index = 0;");
                tw.WriteLine();

                int outer_n = n++;
                GenerateBody(tw, vars, node.Parms, ref n, ref use_args, true, indent);

                tw.Write(indent); tw.WriteLine("if (_memo.ArgResults.Pop() == null)");
                tw.Write(indent); tw.WriteLine("{");
                tw.Write(indent); tw.WriteLine("    _memo.Results.Push(null);");
                tw.Write(indent); tw.WriteLine("    goto label{0};", outer_n);
                tw.Write(indent); tw.WriteLine("}");
                tw.WriteLine();
            }
        }

        void GenerateArgsPost(TextWriter tw, HashSet<string> vars, AST.Args node, int n, bool match_args, string indent)
        {
            if (node.Parms != null)
            {
                tw.Write(indent.Substring(4)); tw.WriteLine("label{0}: // ARGS {0}", n);
                tw.Write(indent); tw.WriteLine("_arg_input_index = _arg_index; // no-op for label", n);
                tw.WriteLine();
            }
        }

        #endregion
    }
}
