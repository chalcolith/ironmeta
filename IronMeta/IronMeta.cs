// IronMeta Generated IronMeta: 11/13/2009 1:09:36 AM UTC

using System;
using System.Collections.Generic;
using System.Linq;

namespace IronMeta
{

    public partial class IronMetaMatcher : IronMeta.CharacterMatcher<IronMeta.SyntaxNode>
    {

        /// <summary>Default Constructor.</summary>
        public IronMetaMatcher()
            : base(a => default(IronMeta.SyntaxNode), true)
        {
        }

        /// <summary>Constructor.</summary>
        public IronMetaMatcher(Func<char,IronMeta.SyntaxNode> conv, bool strictPEG)
            : base(conv, strictPEG)
        {
        }

        /// <summary>Utility class for referencing variables in conditions and actions.</summary>
        private class IronMetaMatcherItem : MatchItem
        {
            public IronMetaMatcherItem() : base() { }
            public IronMetaMatcherItem(string name) : base(name) { }
            public IronMetaMatcherItem(MatchItem mi) : base(mi) { }

            public static implicit operator IronMeta.SyntaxNode(IronMetaMatcherItem item) { return item.Results.LastOrDefault(); }
            public static implicit operator List<IronMeta.SyntaxNode>(IronMetaMatcherItem item) { return item.Results.ToList(); }
            public static implicit operator char(IronMetaMatcherItem item) { return item.Inputs.LastOrDefault(); }
            public static implicit operator List<char>(IronMetaMatcherItem item) { return item.Inputs.ToList(); }
        }

        protected virtual IEnumerable<MatchItem> IronMetaFile(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _IronMetaFile_Body_ = null;

            Combinator _disj_0_ = null;
            {
                var pre = new IronMetaMatcherItem("pre");
                var parsers = new IronMetaMatcherItem("parsers");
                _disj_0_ = _ACTION(_AND(_CALL(SP, true), _VAR(_QUES(_CALL(FilePreamble, true)), pre), _VAR(_STAR(_CALL(IronMetaParser, true)), parsers), _CALL(EOF, true)), (_IM_Result_MI_) => {{ var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 42 "IronMeta.ironmeta"
                   { return new IronMetaFileNode(_IM_StartIndex, _IM_NextIndex, pre, parsers); }
#line default
}});
            }

            _IronMetaFile_Body_ = _disj_0_;

            foreach (var _res_ in _IronMetaFile_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        private int _FilePreamble_Body__Index_ = -1;

        protected virtual IEnumerable<MatchItem> FilePreamble(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _FilePreamble_Body_ = null;

            if (_FilePreamble_Body__Index_ == -1 || CachedCombinators[_FilePreamble_Body__Index_] == null)
            {
                if (_FilePreamble_Body__Index_ == -1)
                {
                    _FilePreamble_Body__Index_ = CachedCombinators.Count;
                    CachedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _PLUS(_CALL(UsingStatement, true));
                }

                CachedCombinators[_FilePreamble_Body__Index_] = _disj_0_;
            }

            _FilePreamble_Body_ = CachedCombinators[_FilePreamble_Body__Index_];


            foreach (var _res_ in _FilePreamble_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> UsingStatement(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _UsingStatement_Body_ = null;

            Combinator _disj_0_ = null;
            {
                var id = new IronMetaMatcherItem("id");
                _disj_0_ = _ACTION(_AND(_LITERAL("using"), _CALL(SP, true), _VAR(_CALL(QualifiedIdentifier, true), id), _CALL(SP, true), _OR(_LITERAL(","), _LITERAL(";")), _CALL(SP, true)), (_IM_Result_MI_) => {{ var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 47 "IronMeta.ironmeta"
                   { return new UsingStatementNode(_IM_StartIndex, _IM_NextIndex, id); }
#line default
}});
            }

            _UsingStatement_Body_ = _disj_0_;

            foreach (var _res_ in _UsingStatement_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> IronMetaParser(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _IronMetaParser_Body_ = null;

            Combinator _disj_0_ = null;
            {
                var decl = new IronMetaMatcherItem("decl");
                var body = new IronMetaMatcherItem("body");
                _disj_0_ = _ACTION(_AND(_LITERAL("ironMeta"), _CALL(SP, true), _VAR(_CALL(ParserDeclaration, true), decl), _VAR(_CALL(ParserBody, true), body)), (_IM_Result_MI_) => {{ var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 50 "IronMeta.ironmeta"
                   { return new ParserNode(_IM_StartIndex, _IM_NextIndex, decl, body); }
#line default
}});
            }

            _IronMetaParser_Body_ = _disj_0_;

            foreach (var _res_ in _IronMetaParser_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> ParserDeclaration(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _ParserDeclaration_Body_ = null;

            Combinator _disj_0_ = null;
            {
                var name = new IronMetaMatcherItem("name");
                var bc = new IronMetaMatcherItem("bc");
                _disj_0_ = _ACTION(_AND(_VAR(_CALL(GenericIdentifier, true), name), _CALL(SP, true), _VAR(_QUES(_CALL(BaseClassDeclaration, true)), bc)), (_IM_Result_MI_) => {{ var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 53 "IronMeta.ironmeta"
                   { return new ParserDeclarationNode(_IM_StartIndex, _IM_NextIndex, name, bc); }
#line default
}});
            }

            _ParserDeclaration_Body_ = _disj_0_;

            foreach (var _res_ in _ParserDeclaration_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> BaseClassDeclaration(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _BaseClassDeclaration_Body_ = null;

            Combinator _disj_0_ = null;
            {
                var id = new IronMetaMatcherItem("id");
                _disj_0_ = _ACTION(_AND(_LITERAL(":"), _CALL(SP, true), _VAR(_CALL(GenericIdentifier, true), id), _CALL(SP, true)), (_IM_Result_MI_) => {{ 
#line 56 "IronMeta.ironmeta"
                   { return id; }
#line default
}});
            }

            _BaseClassDeclaration_Body_ = _disj_0_;

            foreach (var _res_ in _BaseClassDeclaration_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> ParserBody(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _ParserBody_Body_ = null;

            Combinator _disj_0_ = null;
            {
                var rules = new IronMetaMatcherItem("rules");
                _disj_0_ = _ACTION(_AND(_LITERAL("{"), _CALL(SP, true), _VAR(_STAR(_CALL(Rule, true)), rules), _LITERAL("}"), _CALL(SP, true)), (_IM_Result_MI_) => {{ var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 61 "IronMeta.ironmeta"
                   { return new ParserBodyNode(_IM_StartIndex, _IM_NextIndex, rules); }
#line default
}});
            }

            _ParserBody_Body_ = _disj_0_;

            foreach (var _res_ in _ParserBody_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> Rule(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _Rule_Body_ = null;

            Combinator _disj_0_ = null;
            {
                var ovr = new IronMetaMatcherItem("ovr");
                var name = new IronMetaMatcherItem("name");
                var parms = new IronMetaMatcherItem("parms");
                var body = new IronMetaMatcherItem("body");
                _disj_0_ = _ACTION(_AND(_VAR(_QUES(_AND(_LITERAL("override"), _CALL(SP, true))), ovr), _VAR(_CALL(Identifier, true), name), _CALL(SP, true), _VAR(_QUES(_CALL(Disjunction, false)), parms), _OR(_LITERAL("::="), _LITERAL("=")), _CALL(SP, true), _VAR(_CALL(Disjunction, false), body), _OR(_LITERAL(","), _LITERAL(";")), _CALL(SP, true)), (_IM_Result_MI_) => {{ var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 64 "IronMeta.ironmeta"
                   {
                        bool isOverride = ovr.Results.Any();
                        SyntaxNode pNode = parms.Results.Any() ? (SyntaxNode)parms : null;
                        return new RuleNode(_IM_StartIndex, _IM_NextIndex, isOverride, name, pNode, body);
                    }
#line default
}});
            }

            _Rule_Body_ = _disj_0_;

            foreach (var _res_ in _Rule_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        private int _Disjunction_Body__Index_ = -1;

        protected virtual IEnumerable<MatchItem> Disjunction(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _Disjunction_Body_ = null;

            if (_Disjunction_Body__Index_ == -1 || CachedCombinators[_Disjunction_Body__Index_] == null)
            {
                if (_Disjunction_Body__Index_ == -1)
                {
                    _Disjunction_Body__Index_ = CachedCombinators.Count;
                    CachedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _ACTION(_AND(_CALL(ActionExpression, false), _STAR(_AND(_LITERAL("|"), _CALL(SP, true), _CALL(ActionExpression, false)))), (_IM_Result_MI_) => {{ var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 71 "IronMeta.ironmeta"
                       { return new DisjunctionExpNode(_IM_StartIndex, _IM_NextIndex, _IM_Result.Results.Where(node => node is ExpNode)); }
#line default
}});
                }

                CachedCombinators[_Disjunction_Body__Index_] = _disj_0_;
            }

            _Disjunction_Body_ = CachedCombinators[_Disjunction_Body__Index_];


            foreach (var _res_ in _Disjunction_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> ActionExpression(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _ActionExpression_Body_ = null;

            Combinator _disj_0_ = null;
            {
                var exp = new IronMetaMatcherItem("exp");
                var action = new IronMetaMatcherItem("action");
                _disj_0_ = _OR(_ACTION(_AND(_VAR(_CALL(SequenceExpression, false), exp), _OR(_LITERAL("=>"), _LITERAL("->")), _CALL(SP, true), _LOOK(_LITERAL('{')), _VAR(_CALL(CSharpCode, true), action), _CALL(SP, true)), (_IM_Result_MI_) => {{ var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 74 "IronMeta.ironmeta"
                       { return new ActionExpNode(_IM_StartIndex, _IM_NextIndex, exp, action); }
#line default
}}), _CALL(SequenceExpression, false), _CALL(FailExpression, true));
            }

            _ActionExpression_Body_ = _disj_0_;

            foreach (var _res_ in _ActionExpression_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> FailExpression(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _FailExpression_Body_ = null;

            Combinator _disj_0_ = null;
            {
                var str = new IronMetaMatcherItem("str");
                _disj_0_ = _ACTION(_AND(_LITERAL("!"), _QUES(_AND(_LOOK(_LITERAL('\"')), _VAR(_CALL(CSharpCode, true), str))), _CALL(SP, true)), (_IM_Result_MI_) => {{ var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 79 "IronMeta.ironmeta"
                       { return new FailExpNode(_IM_StartIndex, _IM_NextIndex, str); }
#line default
}});
            }

            _FailExpression_Body_ = _disj_0_;

            foreach (var _res_ in _FailExpression_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        private int _SequenceExpression_Body__Index_ = -1;

        protected virtual IEnumerable<MatchItem> SequenceExpression(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _SequenceExpression_Body_ = null;

            if (_SequenceExpression_Body__Index_ == -1 || CachedCombinators[_SequenceExpression_Body__Index_] == null)
            {
                if (_SequenceExpression_Body__Index_ == -1)
                {
                    _SequenceExpression_Body__Index_ = CachedCombinators.Count;
                    CachedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _ACTION(_PLUS(_CALL(ConditionExpression, false)), (_IM_Result_MI_) => {{ var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 82 "IronMeta.ironmeta"
                       { return new SequenceExpNode(_IM_StartIndex, _IM_NextIndex, _IM_Result.Results); }
#line default
}});
                }

                CachedCombinators[_SequenceExpression_Body__Index_] = _disj_0_;
            }

            _SequenceExpression_Body_ = CachedCombinators[_SequenceExpression_Body__Index_];


            foreach (var _res_ in _SequenceExpression_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> ConditionExpression(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _ConditionExpression_Body_ = null;

            Combinator _disj_0_ = null;
            {
                var exp = new IronMetaMatcherItem("exp");
                var cond = new IronMetaMatcherItem("cond");
                _disj_0_ = _OR(_ACTION(_AND(_VAR(_CALL(BoundTerm, false), exp), _AND(_LITERAL('?'), _LOOK(_LITERAL('('))), _VAR(_CALL(CSharpCode, true), cond), _CALL(SP, true)), (_IM_Result_MI_) => {{ var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 85 "IronMeta.ironmeta"
                       { return new ConditionExpNode(_IM_StartIndex, _IM_NextIndex, exp, cond); }
#line default
}}), _CALL(BoundTerm, false));
            }

            _ConditionExpression_Body_ = _disj_0_;

            foreach (var _res_ in _ConditionExpression_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> BoundTerm(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _BoundTerm_Body_ = null;

            Combinator _disj_0_ = null;
            {
                var exp = new IronMetaMatcherItem("exp");
                var id = new IronMetaMatcherItem("id");
                _disj_0_ = _OR(_ACTION(_AND(_VAR(_CALL(PrefixedTerm, false), exp), _LITERAL(":"), _VAR(_CALL(Identifier, true), id), _CALL(SP, true)), (_IM_Result_MI_) => {{ var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 89 "IronMeta.ironmeta"
                       { return new BoundExpNode(_IM_StartIndex, _IM_NextIndex, exp, id); }
#line default
}}), _ACTION(_AND(_LITERAL(":"), _VAR(_CALL(Identifier, true), id), _CALL(SP, true)), (_IM_Result_MI_) => {{ var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 91 "IronMeta.ironmeta"
                       { return new BoundExpNode(_IM_StartIndex, _IM_NextIndex, new AnyExpNode(_IM_StartIndex, _IM_NextIndex), id); }
#line default
}}), _ACTION(_AND(_VAR(_CALL(PrefixedTerm, false), exp), _CALL(SP, true)), (_IM_Result_MI_) => {{ 
#line 93 "IronMeta.ironmeta"
                       { return exp; }
#line default
}}));
            }

            _BoundTerm_Body_ = _disj_0_;

            foreach (var _res_ in _BoundTerm_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        private int _PrefixedTerm_Body__Index_ = -1;

        protected virtual IEnumerable<MatchItem> PrefixedTerm(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _PrefixedTerm_Body_ = null;

            if (_PrefixedTerm_Body__Index_ == -1 || CachedCombinators[_PrefixedTerm_Body__Index_] == null)
            {
                if (_PrefixedTerm_Body__Index_ == -1)
                {
                    _PrefixedTerm_Body__Index_ = CachedCombinators.Count;
                    CachedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _OR(_CALL(AndTerm, true), _CALL(NotTerm, true), _CALL(PostfixedTerm, false));
                }

                CachedCombinators[_PrefixedTerm_Body__Index_] = _disj_0_;
            }

            _PrefixedTerm_Body_ = CachedCombinators[_PrefixedTerm_Body__Index_];


            foreach (var _res_ in _PrefixedTerm_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> AndTerm(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _AndTerm_Body_ = null;

            Combinator _disj_0_ = null;
            {
                var exp = new IronMetaMatcherItem("exp");
                _disj_0_ = _ACTION(_AND(_LITERAL("&"), _VAR(_CALL(PostfixedTerm, false), exp)), (_IM_Result_MI_) => {{ var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 99 "IronMeta.ironmeta"
               { return new UnaryExpNode(_IM_StartIndex, _IM_NextIndex, exp, "LOOK"); }
#line default
}});
            }

            _AndTerm_Body_ = _disj_0_;

            foreach (var _res_ in _AndTerm_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> NotTerm(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _NotTerm_Body_ = null;

            Combinator _disj_0_ = null;
            {
                var exp = new IronMetaMatcherItem("exp");
                _disj_0_ = _ACTION(_AND(_LITERAL("~"), _VAR(_CALL(PrefixedTerm, false), exp)), (_IM_Result_MI_) => {{ var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 102 "IronMeta.ironmeta"
               { return new UnaryExpNode(_IM_StartIndex, _IM_NextIndex, exp, "NOT"); }
#line default
}});
            }

            _NotTerm_Body_ = _disj_0_;

            foreach (var _res_ in _NotTerm_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        private int _PostfixedTerm_Body__Index_ = -1;

        protected virtual IEnumerable<MatchItem> PostfixedTerm(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _PostfixedTerm_Body_ = null;

            if (_PostfixedTerm_Body__Index_ == -1 || CachedCombinators[_PostfixedTerm_Body__Index_] == null)
            {
                if (_PostfixedTerm_Body__Index_ == -1)
                {
                    _PostfixedTerm_Body__Index_ = CachedCombinators.Count;
                    CachedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _OR(_CALL(StarTerm, false), _CALL(PlusTerm, false), _CALL(QuestionTerm, false), _CALL(Term, false), _FAIL("expected a pattern"));
                }

                CachedCombinators[_PostfixedTerm_Body__Index_] = _disj_0_;
            }

            _PostfixedTerm_Body_ = CachedCombinators[_PostfixedTerm_Body__Index_];


            foreach (var _res_ in _PostfixedTerm_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> StarTerm(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _StarTerm_Body_ = null;

            Combinator _disj_0_ = null;
            {
                var exp = new IronMetaMatcherItem("exp");
                _disj_0_ = _ACTION(_AND(_VAR(_CALL(PostfixedTerm, false), exp), _LITERAL("*")), (_IM_Result_MI_) => {{ var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 108 "IronMeta.ironmeta"
               { return new UnaryExpNode(_IM_StartIndex, _IM_NextIndex, exp, "STAR"); }
#line default
}});
            }

            _StarTerm_Body_ = _disj_0_;

            foreach (var _res_ in _StarTerm_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> PlusTerm(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _PlusTerm_Body_ = null;

            Combinator _disj_0_ = null;
            {
                var exp = new IronMetaMatcherItem("exp");
                _disj_0_ = _ACTION(_AND(_VAR(_CALL(PostfixedTerm, false), exp), _LITERAL("+")), (_IM_Result_MI_) => {{ var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 111 "IronMeta.ironmeta"
               { return new UnaryExpNode(_IM_StartIndex, _IM_NextIndex, exp, "PLUS"); }
#line default
}});
            }

            _PlusTerm_Body_ = _disj_0_;

            foreach (var _res_ in _PlusTerm_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> QuestionTerm(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _QuestionTerm_Body_ = null;

            Combinator _disj_0_ = null;
            {
                var exp = new IronMetaMatcherItem("exp");
                _disj_0_ = _ACTION(_AND(_VAR(_CALL(PostfixedTerm, false), exp), _AND(_LITERAL('?'), _NOT(_LITERAL('(')))), (_IM_Result_MI_) => {{ var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 114 "IronMeta.ironmeta"
               { return new UnaryExpNode(_IM_StartIndex, _IM_NextIndex, exp, "QUES"); }
#line default
}});
            }

            _QuestionTerm_Body_ = _disj_0_;

            foreach (var _res_ in _QuestionTerm_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        private int _Term_Body__Index_ = -1;

        protected virtual IEnumerable<MatchItem> Term(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _Term_Body_ = null;

            if (_Term_Body__Index_ == -1 || CachedCombinators[_Term_Body__Index_] == null)
            {
                if (_Term_Body__Index_ == -1)
                {
                    _Term_Body__Index_ = CachedCombinators.Count;
                    CachedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _OR(_CALL(ParenTerm, true), _CALL(AnyTerm, true), _CALL(RuleCall, true), _CALL(CallOrVar, true), _CALL(Literal, true));
                }

                CachedCombinators[_Term_Body__Index_] = _disj_0_;
            }

            _Term_Body_ = CachedCombinators[_Term_Body__Index_];


            foreach (var _res_ in _Term_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> ParenTerm(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _ParenTerm_Body_ = null;

            Combinator _disj_0_ = null;
            {
                var exp = new IronMetaMatcherItem("exp");
                _disj_0_ = _ACTION(_AND(_LITERAL("("), _CALL(SP, true), _VAR(_CALL(Disjunction, false), exp), _LITERAL(")")), (_IM_Result_MI_) => {{ 
#line 120 "IronMeta.ironmeta"
               { return exp; }
#line default
}});
            }

            _ParenTerm_Body_ = _disj_0_;

            foreach (var _res_ in _ParenTerm_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        private int _AnyTerm_Body__Index_ = -1;

        protected virtual IEnumerable<MatchItem> AnyTerm(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _AnyTerm_Body_ = null;

            if (_AnyTerm_Body__Index_ == -1 || CachedCombinators[_AnyTerm_Body__Index_] == null)
            {
                if (_AnyTerm_Body__Index_ == -1)
                {
                    _AnyTerm_Body__Index_ = CachedCombinators.Count;
                    CachedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _ACTION(_LITERAL("."), (_IM_Result_MI_) => {{ var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 123 "IronMeta.ironmeta"
               { return new AnyExpNode(_IM_StartIndex, _IM_NextIndex); }
#line default
}});
                }

                CachedCombinators[_AnyTerm_Body__Index_] = _disj_0_;
            }

            _AnyTerm_Body_ = CachedCombinators[_AnyTerm_Body__Index_];


            foreach (var _res_ in _AnyTerm_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> RuleCall(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _RuleCall_Body_ = null;

            Combinator _disj_0_ = null;
            {
                var name = new IronMetaMatcherItem("name");
                var p = new IronMetaMatcherItem("p");
                _disj_0_ = _ACTION(_AND(_VAR(_CALL(QualifiedIdentifier, true), name), _LITERAL("("), _CALL(SP, true), _VAR(_QUES(_CALL(ParameterList, true)), p), _LITERAL(")")), (_IM_Result_MI_) => {{ var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 126 "IronMeta.ironmeta"
               { return new RuleCallExpNode(_IM_StartIndex, _IM_NextIndex, name, p); }
#line default
}});
            }

            _RuleCall_Body_ = _disj_0_;

            foreach (var _res_ in _RuleCall_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        private int _ParameterList_Body__Index_ = -1;

        protected virtual IEnumerable<MatchItem> ParameterList(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _ParameterList_Body_ = null;

            if (_ParameterList_Body__Index_ == -1 || CachedCombinators[_ParameterList_Body__Index_] == null)
            {
                if (_ParameterList_Body__Index_ == -1)
                {
                    _ParameterList_Body__Index_ = CachedCombinators.Count;
                    CachedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _ACTION(_AND(_CALL(Parameter, true), _STAR(_AND(_LITERAL(","), _CALL(SP, true), _CALL(Parameter, true)))), (_IM_Result_MI_) => {{ var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 129 "IronMeta.ironmeta"
               { return _IM_Result.Results.Where(child => child is ExpNode); }
#line default
}});
                }

                CachedCombinators[_ParameterList_Body__Index_] = _disj_0_;
            }

            _ParameterList_Body_ = CachedCombinators[_ParameterList_Body__Index_];


            foreach (var _res_ in _ParameterList_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> Parameter(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _Parameter_Body_ = null;

            Combinator _disj_0_ = null;
            {
                var p = new IronMetaMatcherItem("p");
                _disj_0_ = _ACTION(_AND(_VAR(_OR(_CALL(CallOrVar, true), _CALL(Literal, true)), p), _CALL(SP, true)), (_IM_Result_MI_) => {{ 
#line 132 "IronMeta.ironmeta"
               { return p; }
#line default
}});
            }

            _Parameter_Body_ = _disj_0_;

            foreach (var _res_ in _Parameter_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        private int _CallOrVar_Body__Index_ = -1;

        protected virtual IEnumerable<MatchItem> CallOrVar(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _CallOrVar_Body_ = null;

            if (_CallOrVar_Body__Index_ == -1 || CachedCombinators[_CallOrVar_Body__Index_] == null)
            {
                if (_CallOrVar_Body__Index_ == -1)
                {
                    _CallOrVar_Body__Index_ = CachedCombinators.Count;
                    CachedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _ACTION(_CALL(QualifiedIdentifier, true), (_IM_Result_MI_) => {{ var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 135 "IronMeta.ironmeta"
               { return new CallOrVarExpNode(_IM_StartIndex, _IM_NextIndex, _IM_Result); }
#line default
}});
                }

                CachedCombinators[_CallOrVar_Body__Index_] = _disj_0_;
            }

            _CallOrVar_Body_ = CachedCombinators[_CallOrVar_Body__Index_];


            foreach (var _res_ in _CallOrVar_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        private int _Literal_Body__Index_ = -1;

        protected virtual IEnumerable<MatchItem> Literal(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _Literal_Body_ = null;

            if (_Literal_Body__Index_ == -1 || CachedCombinators[_Literal_Body__Index_] == null)
            {
                if (_Literal_Body__Index_ == -1)
                {
                    _Literal_Body__Index_ = CachedCombinators.Count;
                    CachedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _ACTION(_AND(_LOOK(_OR(_LITERAL('\"'), _LITERAL('\''), _LITERAL('{'))), _CALL(CSharpCode, true)), (_IM_Result_MI_) => {{ var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 138 "IronMeta.ironmeta"
               { return new LiteralExpNode(_IM_StartIndex, _IM_NextIndex, _IM_Result.Results); }
#line default
}});
                }

                CachedCombinators[_Literal_Body__Index_] = _disj_0_;
            }

            _Literal_Body_ = CachedCombinators[_Literal_Body__Index_];


            foreach (var _res_ in _Literal_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        private int _CSharpCode_Body__Index_ = -1;

        protected virtual IEnumerable<MatchItem> CSharpCode(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _CSharpCode_Body_ = null;

            if (_CSharpCode_Body__Index_ == -1 || CachedCombinators[_CSharpCode_Body__Index_] == null)
            {
                if (_CSharpCode_Body__Index_ == -1)
                {
                    _CSharpCode_Body__Index_ = CachedCombinators.Count;
                    CachedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _ACTION(_CALL(CSharpCodeItem, true), (_IM_Result_MI_) => {{ var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 142 "IronMeta.ironmeta"
               { return new CSharpNode(_IM_StartIndex, _IM_NextIndex); }
#line default
}});
                }

                CachedCombinators[_CSharpCode_Body__Index_] = _disj_0_;
            }

            _CSharpCode_Body_ = CachedCombinators[_CSharpCode_Body__Index_];


            foreach (var _res_ in _CSharpCode_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        private int _CSharpCodeItem_Body__Index_ = -1;

        protected virtual IEnumerable<MatchItem> CSharpCodeItem(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _CSharpCodeItem_Body_ = null;

            if (_CSharpCodeItem_Body__Index_ == -1 || CachedCombinators[_CSharpCodeItem_Body__Index_] == null)
            {
                if (_CSharpCodeItem_Body__Index_ == -1)
                {
                    _CSharpCodeItem_Body__Index_ = CachedCombinators.Count;
                    CachedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _OR(_AND(_LITERAL('{'), _STAR(_AND(_NOT(_LITERAL('}')), _OR(_CALL(CSharpCodeItem, true), _CALL(Comment, true), _CALL(EOL, true), _ANY()))), _LITERAL('}')), _AND(_LITERAL('('), _STAR(_AND(_NOT(_LITERAL(')')), _OR(_CALL(CSharpCodeItem, true), _CALL(Comment, true), _CALL(EOL, true), _ANY()))), _LITERAL(')')), _AND(_LITERAL('\"'), _STAR(_OR(_LITERAL("\x5c\x5c"), _LITERAL("\x5c\""), _AND(_NOT(_LITERAL('\"')), _OR(_CALL(EOL, true), _ANY())))), _LITERAL('\"')), _AND(_LITERAL('\''), _STAR(_OR(_LITERAL("\x5c\x5c"), _LITERAL("\x5c\'"), _AND(_NOT(_LITERAL('\'')), _OR(_CALL(EOL, true), _ANY())))), _LITERAL('\'')));
                }

                CachedCombinators[_CSharpCodeItem_Body__Index_] = _disj_0_;
            }

            _CSharpCodeItem_Body_ = CachedCombinators[_CSharpCodeItem_Body__Index_];


            foreach (var _res_ in _CSharpCodeItem_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> GenericIdentifier(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _GenericIdentifier_Body_ = null;

            Combinator _disj_0_ = null;
            {
                var id = new IronMetaMatcherItem("id");
                var p = new IronMetaMatcherItem("p");
                _disj_0_ = _ACTION(_AND(_VAR(_CALL(QualifiedIdentifier, true), id), _QUES(_AND(_LITERAL("<"), _CALL(SP, true), _VAR(_AND(_CALL(GenericIdentifier, true), _CALL(SP, true), _STAR(_AND(_LITERAL(","), _CALL(SP, true), _CALL(GenericIdentifier, true), _CALL(SP, true)))), p), _LITERAL(">")))), (_IM_Result_MI_) => {{ var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 151 "IronMeta.ironmeta"
               {
                    var idn = (IdentifierNode) id;
                    return new IdentifierNode(_IM_StartIndex, _IM_NextIndex, idn, idn.Qualifiers, p.Results.Where(node => node is IdentifierNode));
                }
#line default
}});
            }

            _GenericIdentifier_Body_ = _disj_0_;

            foreach (var _res_ in _GenericIdentifier_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> QualifiedIdentifier(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _QualifiedIdentifier_Body_ = null;

            Combinator _disj_0_ = null;
            {
                var quals = new IronMetaMatcherItem("quals");
                var name = new IronMetaMatcherItem("name");
                _disj_0_ = _ACTION(_AND(_VAR(_STAR(_AND(_CALL(Identifier, true), _LITERAL("."))), quals), _VAR(_CALL(Identifier, true), name)), (_IM_Result_MI_) => {{ var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 157 "IronMeta.ironmeta"
               { return new IdentifierNode(_IM_StartIndex, _IM_NextIndex, name, quals.Results.Where(node => node is IdentifierNode), null); }
#line default
}});
            }

            _QualifiedIdentifier_Body_ = _disj_0_;

            foreach (var _res_ in _QualifiedIdentifier_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        private int _Identifier_Body__Index_ = -1;

        protected virtual IEnumerable<MatchItem> Identifier(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _Identifier_Body_ = null;

            if (_Identifier_Body__Index_ == -1 || CachedCombinators[_Identifier_Body__Index_] == null)
            {
                if (_Identifier_Body__Index_ == -1)
                {
                    _Identifier_Body__Index_ = CachedCombinators.Count;
                    CachedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _ACTION(_AND(_CALL(IdentBegin, true), _STAR(_CALL(IdentChar, true))), (_IM_Result_MI_) => {{ var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 160 "IronMeta.ironmeta"
               { return new IdentifierNode(_IM_StartIndex, _IM_NextIndex, null, null, null); }
#line default
}});
                }

                CachedCombinators[_Identifier_Body__Index_] = _disj_0_;
            }

            _Identifier_Body_ = CachedCombinators[_Identifier_Body__Index_];


            foreach (var _res_ in _Identifier_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        private int _IdentBegin_Body__Index_ = -1;

        protected virtual IEnumerable<MatchItem> IdentBegin(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _IdentBegin_Body_ = null;

            if (_IdentBegin_Body__Index_ == -1 || CachedCombinators[_IdentBegin_Body__Index_] == null)
            {
                if (_IdentBegin_Body__Index_ == -1)
                {
                    _IdentBegin_Body__Index_ = CachedCombinators.Count;
                    CachedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _OR(_LITERAL('_'), _CONDITION(_ANY(), (_IM_Result_MI_) => { var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; return (
#line 162 "IronMeta.ironmeta"
                           (System.Char.IsLetter(_IM_Result))
#line default
);}));
                }

                CachedCombinators[_IdentBegin_Body__Index_] = _disj_0_;
            }

            _IdentBegin_Body_ = CachedCombinators[_IdentBegin_Body__Index_];


            foreach (var _res_ in _IdentBegin_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        private int _IdentChar_Body__Index_ = -1;

        protected virtual IEnumerable<MatchItem> IdentChar(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _IdentChar_Body_ = null;

            if (_IdentChar_Body__Index_ == -1 || CachedCombinators[_IdentChar_Body__Index_] == null)
            {
                if (_IdentChar_Body__Index_ == -1)
                {
                    _IdentChar_Body__Index_ = CachedCombinators.Count;
                    CachedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _OR(_LITERAL('_'), _CONDITION(_ANY(), (_IM_Result_MI_) => { var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; return (
#line 164 "IronMeta.ironmeta"
                          (System.Char.IsLetterOrDigit(_IM_Result))
#line default
);}));
                }

                CachedCombinators[_IdentChar_Body__Index_] = _disj_0_;
            }

            _IdentChar_Body_ = CachedCombinators[_IdentChar_Body__Index_];


            foreach (var _res_ in _IdentChar_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        private int _SP_Body__Index_ = -1;

        protected virtual IEnumerable<MatchItem> SP(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _SP_Body_ = null;

            if (_SP_Body__Index_ == -1 || CachedCombinators[_SP_Body__Index_] == null)
            {
                if (_SP_Body__Index_ == -1)
                {
                    _SP_Body__Index_ = CachedCombinators.Count;
                    CachedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _ACTION(_STAR(_OR(_CALL(Comment, true), _CALL(Whitespace, true))), (_IM_Result_MI_) => {{ var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 166 "IronMeta.ironmeta"
                                    { return new SpacingNode(_IM_StartIndex, _IM_NextIndex, _IM_Result.Results); }
#line default
}});
                }

                CachedCombinators[_SP_Body__Index_] = _disj_0_;
            }

            _SP_Body_ = CachedCombinators[_SP_Body__Index_];


            foreach (var _res_ in _SP_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        private int _Comment_Body__Index_ = -1;

        protected virtual IEnumerable<MatchItem> Comment(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _Comment_Body_ = null;

            if (_Comment_Body__Index_ == -1 || CachedCombinators[_Comment_Body__Index_] == null)
            {
                if (_Comment_Body__Index_ == -1)
                {
                    _Comment_Body__Index_ = CachedCombinators.Count;
                    CachedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _ACTION(_OR(_AND(_LITERAL("//"), _STAR(_CALL(EOLCommentChar, true)), _OR(_CALL(EOL, true), _CALL(EOF, true))), _AND(_LITERAL("/*"), _STAR(_CALL(BracketCommentChar, true)), _LITERAL("*/"))), (_IM_Result_MI_) => {{ var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 169 "IronMeta.ironmeta"
               { return new CommentNode(_IM_StartIndex, _IM_NextIndex); }
#line default
}});
                }

                CachedCombinators[_Comment_Body__Index_] = _disj_0_;
            }

            _Comment_Body_ = CachedCombinators[_Comment_Body__Index_];


            foreach (var _res_ in _Comment_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        private int _EOLCommentChar_Body__Index_ = -1;

        protected virtual IEnumerable<MatchItem> EOLCommentChar(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _EOLCommentChar_Body_ = null;

            if (_EOLCommentChar_Body__Index_ == -1 || CachedCombinators[_EOLCommentChar_Body__Index_] == null)
            {
                if (_EOLCommentChar_Body__Index_ == -1)
                {
                    _EOLCommentChar_Body__Index_ = CachedCombinators.Count;
                    CachedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _AND(_NOT(_OR(_LITERAL('\r'), _LITERAL('\n'), _LITERAL('\x0085'), _LITERAL('\x000c'), _LITERAL('\x2028'), _LITERAL('\x2029'))), _ANY());
                }

                CachedCombinators[_EOLCommentChar_Body__Index_] = _disj_0_;
            }

            _EOLCommentChar_Body_ = CachedCombinators[_EOLCommentChar_Body__Index_];


            foreach (var _res_ in _EOLCommentChar_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        private int _BracketCommentChar_Body__Index_ = -1;

        protected virtual IEnumerable<MatchItem> BracketCommentChar(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _BracketCommentChar_Body_ = null;

            if (_BracketCommentChar_Body__Index_ == -1 || CachedCombinators[_BracketCommentChar_Body__Index_] == null)
            {
                if (_BracketCommentChar_Body__Index_ == -1)
                {
                    _BracketCommentChar_Body__Index_ = CachedCombinators.Count;
                    CachedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _AND(_NOT(_LITERAL("*/")), _OR(_CALL(EOL, true), _ANY()));
                }

                CachedCombinators[_BracketCommentChar_Body__Index_] = _disj_0_;
            }

            _BracketCommentChar_Body_ = CachedCombinators[_BracketCommentChar_Body__Index_];


            foreach (var _res_ in _BracketCommentChar_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        private int _Whitespace_Body__Index_ = -1;

        protected override IEnumerable<MatchItem> Whitespace(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _Whitespace_Body_ = null;

            if (_Whitespace_Body__Index_ == -1 || CachedCombinators[_Whitespace_Body__Index_] == null)
            {
                if (_Whitespace_Body__Index_ == -1)
                {
                    _Whitespace_Body__Index_ = CachedCombinators.Count;
                    CachedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _OR(_CALL(EOL, true), _ACTION(_CONDITION(_ANY(), (_IM_Result_MI_) => { var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; return (
#line 174 "IronMeta.ironmeta"
                                   (System.Char.IsWhiteSpace(_IM_Result))
#line default
);}), (_IM_Result_MI_) => {{ var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 175 "IronMeta.ironmeta"
                                   { return new TokenNode(_IM_StartIndex, _IM_NextIndex, TokenNode.TokenType.WHITESPACE); }
#line default
}}));
                }

                CachedCombinators[_Whitespace_Body__Index_] = _disj_0_;
            }

            _Whitespace_Body_ = CachedCombinators[_Whitespace_Body__Index_];


            foreach (var _res_ in _Whitespace_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        private int _EOL_Body__Index_ = -1;

        protected override IEnumerable<MatchItem> EOL(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _EOL_Body_ = null;

            if (_EOL_Body__Index_ == -1 || CachedCombinators[_EOL_Body__Index_] == null)
            {
                if (_EOL_Body__Index_ == -1)
                {
                    _EOL_Body__Index_ = CachedCombinators.Count;
                    CachedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _ACTION(_OR(_LITERAL("\x0d\x0a"), _LITERAL('\x0a'), _AND(_LITERAL('\x0d'), _NOT(_LITERAL('\x0a'))), _LITERAL('\x0085'), _LITERAL('\x000c'), _LITERAL('\x2028'), _LITERAL('\x2029')), (_IM_Result_MI_) => {{ var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 178 "IronMeta.ironmeta"
               { _IM_LineBeginPositions.Add(_IM_NextIndex); return new TokenNode(_IM_StartIndex, _IM_NextIndex, TokenNode.TokenType.EOL); }
#line default
}});
                }

                CachedCombinators[_EOL_Body__Index_] = _disj_0_;
            }

            _EOL_Body_ = CachedCombinators[_EOL_Body__Index_];


            foreach (var _res_ in _EOL_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        private int _EOF_Body__Index_ = -1;

        protected override IEnumerable<MatchItem> EOF(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _EOF_Body_ = null;

            if (_EOF_Body__Index_ == -1 || CachedCombinators[_EOF_Body__Index_] == null)
            {
                if (_EOF_Body__Index_ == -1)
                {
                    _EOF_Body__Index_ = CachedCombinators.Count;
                    CachedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _ACTION(_NOT(_ANY()), (_IM_Result_MI_) => {{ var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 181 "IronMeta.ironmeta"
               { _IM_LineBeginPositions.Add(_IM_StartIndex); return new TokenNode(_IM_StartIndex, _IM_NextIndex, TokenNode.TokenType.EOF); }
#line default
}});
                }

                CachedCombinators[_EOF_Body__Index_] = _disj_0_;
            }

            _EOF_Body_ = CachedCombinators[_EOF_Body__Index_];


            foreach (var _res_ in _EOF_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

    } // class IronMetaMatcher

} // namespace IronMeta
