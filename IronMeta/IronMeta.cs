// IronMeta Generated IronMeta: 6/3/2009 12:00:24 AM UTC

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

            public IronMetaMatcherItem(MatchItem mi)
                : base(mi)
            {
            }

            public static implicit operator char(IronMetaMatcherItem item) { return item.Inputs.LastOrDefault(); }
            public static implicit operator List<char>(IronMetaMatcherItem item) { return item.Inputs.ToList(); }
            public static implicit operator IronMeta.SyntaxNode(IronMetaMatcherItem item) { return item.Results.LastOrDefault(); }
            public static implicit operator List<IronMeta.SyntaxNode>(IronMetaMatcherItem item) { return item.Results.ToList(); }
        }

        protected virtual IEnumerable<MatchItem> IronMetaFile(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _IronMetaFile_Body_ = null;

            Combinator _disj_0_ = null;
            {
                var pre = new IronMetaMatcherItem();
                var parsers = new IronMetaMatcherItem();
                _disj_0_ = _ACTION(_AND(_CALL(SP), _VAR(_QUES(_CALL(FilePreamble)), pre), _VAR(_STAR(_CALL(IronMetaParser)), parsers), _CALL(EOF)), (_IM_Result_MI_) => { var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 41 "IronMeta.ironmeta"
                   { return new IronMetaFileNode(_IM_StartIndex, _IM_NextIndex, pre, parsers); }
#line default
});
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
                    _disj_0_ = _PLUS(_CALL(UsingStatement));
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
                var id = new IronMetaMatcherItem();
                _disj_0_ = _ACTION(_AND(_LITERAL("using"), _CALL(SP), _VAR(_CALL(QualifiedIdentifier), id), _CALL(SP), _OR(_LITERAL(","), _LITERAL(";")), _CALL(SP)), (_IM_Result_MI_) => { var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 46 "IronMeta.ironmeta"
                     { return new UsingStatementNode(_IM_StartIndex, _IM_NextIndex, id); }
#line default
});
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
                var decl = new IronMetaMatcherItem();
                var body = new IronMetaMatcherItem();
                _disj_0_ = _ACTION(_AND(_LITERAL("ironMeta"), _CALL(SP), _VAR(_CALL(ParserDeclaration), decl), _VAR(_CALL(ParserBody), body)), (_IM_Result_MI_) => { var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 49 "IronMeta.ironmeta"
                     { return new ParserNode(_IM_StartIndex, _IM_NextIndex, decl, body); }
#line default
});
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
                var name = new IronMetaMatcherItem();
                var bc = new IronMetaMatcherItem();
                _disj_0_ = _ACTION(_AND(_VAR(_CALL(GenericIdentifier), name), _CALL(SP), _VAR(_QUES(_CALL(BaseClassDeclaration)), bc)), (_IM_Result_MI_) => { var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 52 "IronMeta.ironmeta"
                        { return new ParserDeclarationNode(_IM_StartIndex, _IM_NextIndex, name, bc); }
#line default
});
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
                var id = new IronMetaMatcherItem();
                _disj_0_ = _ACTION(_AND(_LITERAL(":"), _CALL(SP), _VAR(_CALL(GenericIdentifier), id), _CALL(SP)), (_IM_Result_MI_) => { 
#line 55 "IronMeta.ironmeta"
                           { return id; }
#line default
});
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
                var rules = new IronMetaMatcherItem();
                _disj_0_ = _ACTION(_AND(_LITERAL("{"), _CALL(SP), _VAR(_STAR(_CALL(Rule)), rules), _LITERAL("}"), _CALL(SP)), (_IM_Result_MI_) => { var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 60 "IronMeta.ironmeta"
                 { return new ParserBodyNode(_IM_StartIndex, _IM_NextIndex, rules); }
#line default
});
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
                var ovr = new IronMetaMatcherItem();
                var name = new IronMetaMatcherItem();
                var parms = new IronMetaMatcherItem();
                var body = new IronMetaMatcherItem();
                _disj_0_ = _ACTION(_AND(_VAR(_QUES(_AND(_LITERAL("override"), _CALL(SP))), ovr), _VAR(_CALL(Identifier), name), _CALL(SP), _VAR(_QUES(_CALL(Disjunction)), parms), _OR(_LITERAL("::="), _LITERAL("=")), _CALL(SP), _VAR(_CALL(Disjunction), body), _OR(_LITERAL(","), _LITERAL(";")), _CALL(SP)), (_IM_Result_MI_) => { var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 63 "IronMeta.ironmeta"
           {
                        bool isOverride = ovr.Results.Any();
                        SyntaxNode pNode = parms.Results.Any() ? (SyntaxNode)parms : null;
                        return new RuleNode(_IM_StartIndex, _IM_NextIndex, isOverride, name, pNode, body);
                    }
#line default
});
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
                    _disj_0_ = _ACTION(_AND(_CALL(ActionExpression), _STAR(_AND(_LITERAL("|"), _CALL(SP), _CALL(ActionExpression)))), (_IM_Result_MI_) => { var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 70 "IronMeta.ironmeta"
                  { return new DisjunctionExpNode(_IM_StartIndex, _IM_NextIndex, _IM_Result.Results.Where(node => node is ExpNode)); }
#line default
});
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
                var exp = new IronMetaMatcherItem();
                var action = new IronMetaMatcherItem();
                _disj_0_ = _OR(_ACTION(_AND(_VAR(_CALL(SequenceExpression), exp), _OR(_LITERAL("->"), _LITERAL("=>")), _CALL(SP), _LOOK(_LITERAL('{')), _VAR(_CALL(CSharpCode), action), _CALL(SP)), (_IM_Result_MI_) => { var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 73 "IronMeta.ironmeta"
                       { return new ActionExpNode(_IM_StartIndex, _IM_NextIndex, exp, action); }
#line default
}), _CALL(SequenceExpression), _CALL(FailExpression));
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
                var str = new IronMetaMatcherItem();
                _disj_0_ = _ACTION(_AND(_LITERAL("!"), _QUES(_AND(_LOOK(_LITERAL('\"')), _VAR(_CALL(CSharpCode), str))), _CALL(SP)), (_IM_Result_MI_) => { var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 78 "IronMeta.ironmeta"
                     { return new FailExpNode(_IM_StartIndex, _IM_NextIndex, str); }
#line default
});
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
                    _disj_0_ = _ACTION(_PLUS(_CALL(ConditionExpression)), (_IM_Result_MI_) => { var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 81 "IronMeta.ironmeta"
                      { return new SequenceExpNode(_IM_StartIndex, _IM_NextIndex, _IM_Result.Results.Where(node => node is ExpNode)); }
#line default
});
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
                var exp = new IronMetaMatcherItem();
                var cond = new IronMetaMatcherItem();
                _disj_0_ = _OR(_ACTION(_AND(_VAR(_CALL(BoundTerm), exp), _AND(_LITERAL('?'), _LOOK(_LITERAL('('))), _VAR(_CALL(CSharpCode), cond), _CALL(SP)), (_IM_Result_MI_) => { var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 84 "IronMeta.ironmeta"
                          { return new ConditionExpNode(_IM_StartIndex, _IM_NextIndex, exp, cond); }
#line default
}), _CALL(BoundTerm));
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
                var exp = new IronMetaMatcherItem();
                var id = new IronMetaMatcherItem();
                _disj_0_ = _OR(_ACTION(_AND(_VAR(_CALL(PrefixedTerm), exp), _LITERAL(":"), _VAR(_CALL(Identifier), id), _CALL(SP)), (_IM_Result_MI_) => { var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 88 "IronMeta.ironmeta"
                { return new BoundExpNode(_IM_StartIndex, _IM_NextIndex, exp, id); }
#line default
}), _ACTION(_AND(_LITERAL(":"), _VAR(_CALL(Identifier), id), _CALL(SP)), (_IM_Result_MI_) => { var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 90 "IronMeta.ironmeta"
                { return new BoundExpNode(_IM_StartIndex, _IM_NextIndex, new AnyExpNode(_IM_StartIndex, _IM_NextIndex), id); }
#line default
}), _ACTION(_AND(_VAR(_CALL(PrefixedTerm), exp), _CALL(SP)), (_IM_Result_MI_) => { 
#line 92 "IronMeta.ironmeta"
                { return exp; }
#line default
}));
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
                    _disj_0_ = _OR(_CALL(AndTerm), _CALL(NotTerm), _CALL(PostfixedTerm));
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
                var exp = new IronMetaMatcherItem();
                _disj_0_ = _ACTION(_AND(_LITERAL("&"), _VAR(_CALL(PostfixedTerm), exp)), (_IM_Result_MI_) => { var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 98 "IronMeta.ironmeta"
              { return new PrefixedExpNode(_IM_StartIndex, _IM_NextIndex, exp, "LOOK"); }
#line default
});
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
                var exp = new IronMetaMatcherItem();
                _disj_0_ = _ACTION(_AND(_LITERAL("~"), _VAR(_CALL(PrefixedTerm), exp)), (_IM_Result_MI_) => { var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 101 "IronMeta.ironmeta"
              { return new PrefixedExpNode(_IM_StartIndex, _IM_NextIndex, exp, "NOT"); }
#line default
});
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
                    _disj_0_ = _OR(_CALL(StarTerm), _CALL(PlusTerm), _CALL(QuestionTerm), _CALL(Term));
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
                var exp = new IronMetaMatcherItem();
                _disj_0_ = _ACTION(_AND(_VAR(_CALL(PostfixedTerm), exp), _LITERAL("*")), (_IM_Result_MI_) => { var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 107 "IronMeta.ironmeta"
               { return new PostfixedExpNode(_IM_StartIndex, _IM_NextIndex, exp, "STAR"); }
#line default
});
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
                var exp = new IronMetaMatcherItem();
                _disj_0_ = _ACTION(_AND(_VAR(_CALL(PostfixedTerm), exp), _LITERAL("+")), (_IM_Result_MI_) => { var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 110 "IronMeta.ironmeta"
               { return new PostfixedExpNode(_IM_StartIndex, _IM_NextIndex, exp, "PLUS"); }
#line default
});
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
                var exp = new IronMetaMatcherItem();
                _disj_0_ = _ACTION(_AND(_VAR(_CALL(PostfixedTerm), exp), _AND(_LITERAL('?'), _NOT(_LITERAL('(')))), (_IM_Result_MI_) => { var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 113 "IronMeta.ironmeta"
                   { return new PostfixedExpNode(_IM_StartIndex, _IM_NextIndex, exp, "QUES"); }
#line default
});
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
                    _disj_0_ = _OR(_CALL(ParenTerm), _CALL(AnyTerm), _CALL(RuleCall), _CALL(CallOrVar), _CALL(Literal));
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
                var exp = new IronMetaMatcherItem();
                _disj_0_ = _ACTION(_AND(_LITERAL("("), _CALL(SP), _VAR(_CALL(Disjunction), exp), _LITERAL(")")), (_IM_Result_MI_) => { 
#line 119 "IronMeta.ironmeta"
                { return exp; }
#line default
});
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
                    _disj_0_ = _ACTION(_LITERAL("."), (_IM_Result_MI_) => { var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 122 "IronMeta.ironmeta"
              { return new AnyExpNode(_IM_StartIndex, _IM_NextIndex); }
#line default
});
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
                var name = new IronMetaMatcherItem();
                var p = new IronMetaMatcherItem();
                _disj_0_ = _ACTION(_AND(_VAR(_CALL(QualifiedIdentifier), name), _LITERAL("("), _CALL(SP), _VAR(_QUES(_CALL(ParameterList)), p), _LITERAL(")")), (_IM_Result_MI_) => { var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 125 "IronMeta.ironmeta"
               { return new RuleCallExpNode(_IM_StartIndex, _IM_NextIndex, name, p); }
#line default
});
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
                    _disj_0_ = _ACTION(_AND(_CALL(Parameter), _STAR(_AND(_LITERAL(","), _CALL(SP), _CALL(Parameter)))), (_IM_Result_MI_) => { var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 128 "IronMeta.ironmeta"
                    { return _IM_Result.Results.Where(child => child is ExpNode); }
#line default
});
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
                var p = new IronMetaMatcherItem();
                _disj_0_ = _ACTION(_AND(_VAR(_OR(_CALL(CallOrVar), _CALL(Literal)), p), _CALL(SP)), (_IM_Result_MI_) => { 
#line 131 "IronMeta.ironmeta"
                { return p; }
#line default
});
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
                    _disj_0_ = _ACTION(_CALL(QualifiedIdentifier), (_IM_Result_MI_) => { var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 134 "IronMeta.ironmeta"
                { return new CallOrVarExpNode(_IM_StartIndex, _IM_NextIndex, _IM_Result); }
#line default
});
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
                    _disj_0_ = _ACTION(_AND(_LOOK(_OR(_LITERAL('\"'), _LITERAL('\''), _LITERAL('{'))), _CALL(CSharpCode)), (_IM_Result_MI_) => { var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 137 "IronMeta.ironmeta"
              { return new LiteralExpNode(_IM_StartIndex, _IM_NextIndex, _IM_Result.Results); }
#line default
});
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
                    _disj_0_ = _ACTION(_CALL(CSharpCodeItem), (_IM_Result_MI_) => { var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 141 "IronMeta.ironmeta"
                 { return new CSharpNode(_IM_StartIndex, _IM_NextIndex); }
#line default
});
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
                    _disj_0_ = _OR(_AND(_LITERAL('{'), _STAR(_AND(_NOT(_LITERAL('}')), _OR(_CALL(CSharpCodeItem), _CALL(Comment), _CALL(EOL), _ANY()))), _LITERAL('}')), _AND(_LITERAL('('), _STAR(_AND(_NOT(_LITERAL(')')), _OR(_CALL(CSharpCodeItem), _CALL(Comment), _CALL(EOL), _ANY()))), _LITERAL(')')), _AND(_LITERAL('\"'), _STAR(_OR(_AND(_LITERAL('\x5c'), _LITERAL('\x5c')), _AND(_LITERAL('\x5c'), _LITERAL('\"')), _AND(_NOT(_LITERAL('\"')), _OR(_CALL(EOL), _ANY())))), _LITERAL('\"')), _AND(_LITERAL('\''), _STAR(_OR(_AND(_LITERAL('\x5c'), _LITERAL('\x5c')), _AND(_LITERAL('\x5c'), _LITERAL('\'')), _AND(_NOT(_LITERAL('\'')), _OR(_CALL(EOL), _ANY())))), _LITERAL('\'')));
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
                var id = new IronMetaMatcherItem();
                var p = new IronMetaMatcherItem();
                _disj_0_ = _ACTION(_AND(_VAR(_CALL(QualifiedIdentifier), id), _QUES(_AND(_LITERAL("<"), _CALL(SP), _VAR(_AND(_CALL(GenericIdentifier), _CALL(SP), _STAR(_AND(_LITERAL(","), _CALL(SP), _CALL(GenericIdentifier), _CALL(SP)))), p), _LITERAL(">")))), (_IM_Result_MI_) => { var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 150 "IronMeta.ironmeta"
                        {
					var idn = (IdentifierNode) id;
					return new IdentifierNode(_IM_StartIndex, _IM_NextIndex, idn, idn.Qualifiers, p.Results.Where(node => node is IdentifierNode));
                }
#line default
});
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
                var quals = new IronMetaMatcherItem();
                var name = new IronMetaMatcherItem();
                _disj_0_ = _ACTION(_AND(_VAR(_STAR(_AND(_CALL(Identifier), _LITERAL("."))), quals), _VAR(_CALL(Identifier), name)), (_IM_Result_MI_) => { var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 156 "IronMeta.ironmeta"
                          {
					return new IdentifierNode(_IM_StartIndex, _IM_NextIndex, name, quals.Results.Where(node => node is IdentifierNode), null);
                }
#line default
});
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
                    _disj_0_ = _ACTION(_AND(_CONDITION(_ANY(), (_IM_Result_MI_) => { var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; return (
#line 161 "IronMeta.ironmeta"
                  (_IM_Result == '_' || System.Char.IsLetter(_IM_Result))
#line default
);}), _STAR(_CONDITION(_ANY(), (_IM_Result_MI_) => { var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; return (
#line 162 "IronMeta.ironmeta"
                  (_IM_Result == '_' || System.Char.IsLetterOrDigit(_IM_Result))
#line default
);}))), (_IM_Result_MI_) => { var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 161 "IronMeta.ironmeta"
                 { return new IdentifierNode(_IM_StartIndex, _IM_NextIndex, null, null, null); }
#line default
});
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
                    _disj_0_ = _ACTION(_STAR(_OR(_CALL(Comment), _CALL(Whitespace))), (_IM_Result_MI_) => { var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 165 "IronMeta.ironmeta"
         { return new SpacingNode(_IM_StartIndex, _IM_NextIndex, _IM_Result.Results); }
#line default
});
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
                    _disj_0_ = _ACTION(_OR(_AND(_LITERAL('/'), _LITERAL('/'), _STAR(_AND(_NOT(_OR(_LITERAL('\r'), _LITERAL('\n'))), _ANY())), _OR(_CALL(EOL), _CALL(EOF))), _AND(_LITERAL('/'), _LITERAL('*'), _STAR(_AND(_NOT(_AND(_LITERAL('*'), _LITERAL('/'))), _OR(_CALL(EOL), _ANY()))), _LITERAL('*'), _LITERAL('/'))), (_IM_Result_MI_) => { var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 167 "IronMeta.ironmeta"
              { return new CommentNode(_IM_StartIndex, _IM_NextIndex); }
#line default
});
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
                    _disj_0_ = _OR(_ACTION(_OR(_AND(_LITERAL('\r'), _LITERAL('\n')), _LITERAL('\n'), _AND(_LITERAL('\r'), _NOT(_LITERAL('\n')))), (_IM_Result_MI_) => { var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 171 "IronMeta.ironmeta"
                          { _IM_LineBeginPositions.Add(_IM_NextIndex); return new TokenNode(_IM_StartIndex, _IM_NextIndex, TokenNode.TokenType.EOL); }
#line default
}), _ACTION(_CONDITION(_ANY(), (_IM_Result_MI_) => { var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; return (
#line 174 "IronMeta.ironmeta"
                          (System.Char.IsWhiteSpace(_IM_Result))
#line default
);}), (_IM_Result_MI_) => { var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 174 "IronMeta.ironmeta"
                          { return new TokenNode(_IM_StartIndex, _IM_NextIndex, TokenNode.TokenType.WHITESPACE); }
#line default
}));
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
                    _disj_0_ = _ACTION(_OR(_AND(_LITERAL('\r'), _LITERAL('\n')), _LITERAL('\n'), _AND(_LITERAL('\r'), _NOT(_LITERAL('\n')))), (_IM_Result_MI_) => { var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 177 "IronMeta.ironmeta"
                   { _IM_LineBeginPositions.Add(_IM_NextIndex); return new TokenNode(_IM_StartIndex, _IM_NextIndex, TokenNode.TokenType.EOL); }
#line default
});
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
                    _disj_0_ = _ACTION(_NOT(_ANY()), (_IM_Result_MI_) => { var _IM_Result = new IronMetaMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 180 "IronMeta.ironmeta"
                   { _IM_LineBeginPositions.Add(_IM_StartIndex); return new TokenNode(_IM_StartIndex, _IM_NextIndex, TokenNode.TokenType.EOF); }
#line default
});
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

