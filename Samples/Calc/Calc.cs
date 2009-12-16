// IronMeta Generated Calc: 12/15/2009 11:27:12 PM UTC

using System.Collections.Generic;
using System;
using System.Linq;

namespace Calc
{

    public partial class CalcMatcher : IronMeta.CharacterMatcher<int>
    {

        /// <summary>Default Constructor.</summary>
        public CalcMatcher()
            : base(a => default(int), true)
        {
        }

        /// <summary>Constructor.</summary>
        public CalcMatcher(Func<char,int> conv, bool strictPEG)
            : base(conv, strictPEG)
        {
        }

        /// <summary>Utility class for referencing variables in conditions and actions.</summary>
        private class CalcMatcherItem : MatchItem
        {
            public CalcMatcherItem() : base() { }
            public CalcMatcherItem(string name) : base(name) { }
            public CalcMatcherItem(MatchItem mi) : base(mi) { }

            public static implicit operator int(CalcMatcherItem item) { return item.Results.LastOrDefault(); }
            public static implicit operator List<int>(CalcMatcherItem item) { return item.Results.ToList(); }
            public static implicit operator char(CalcMatcherItem item) { return item.Inputs.LastOrDefault(); }
            public static implicit operator List<char>(CalcMatcherItem item) { return item.Inputs.ToList(); }
        }

        private int _Expression_Body__Index_ = -1;

        protected virtual IEnumerable<MatchItem> Expression(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _Expression_Body_ = null;

            if (_Expression_Body__Index_ == -1 || CachedCombinators[_Expression_Body__Index_] == null)
            {
                if (_Expression_Body__Index_ == -1)
                {
                    _Expression_Body__Index_ = CachedCombinators.Count;
                    CachedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _CALL(Additive, false);
                }

                CachedCombinators[_Expression_Body__Index_] = _disj_0_;
            }

            _Expression_Body_ = CachedCombinators[_Expression_Body__Index_];


            foreach (var _res_ in _Expression_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        private int _Additive_Body__Index_ = -1;

        protected virtual IEnumerable<MatchItem> Additive(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _Additive_Body_ = null;

            if (_Additive_Body__Index_ == -1 || CachedCombinators[_Additive_Body__Index_] == null)
            {
                if (_Additive_Body__Index_ == -1)
                {
                    _Additive_Body__Index_ = CachedCombinators.Count;
                    CachedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _OR(_CALL(Add, false), _CALL(Sub, false), _CALL(Multiplicative, false));
                }

                CachedCombinators[_Additive_Body__Index_] = _disj_0_;
            }

            _Additive_Body_ = CachedCombinators[_Additive_Body__Index_];


            foreach (var _res_ in _Additive_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        private int _DecimalDigit_Body__Index_ = -1;

        protected virtual IEnumerable<MatchItem> DecimalDigit(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _DecimalDigit_Body_ = null;

            if (_DecimalDigit_Body__Index_ == -1 || CachedCombinators[_DecimalDigit_Body__Index_] == null)
            {
                if (_DecimalDigit_Body__Index_ == -1)
                {
                    _DecimalDigit_Body__Index_ = CachedCombinators.Count;
                    CachedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _CATEGORY(new List<MatchItem> { new MatchItem('0', CONV), new MatchItem('1', CONV), new MatchItem('2', CONV), new MatchItem('3', CONV), new MatchItem('4', CONV), new MatchItem('5', CONV), new MatchItem('6', CONV), new MatchItem('7', CONV), new MatchItem('8', CONV), new MatchItem('9', CONV) });
                }

                CachedCombinators[_DecimalDigit_Body__Index_] = _disj_0_;
            }

            _DecimalDigit_Body_ = CachedCombinators[_DecimalDigit_Body__Index_];


            foreach (var _res_ in _DecimalDigit_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        private int _Add_Body__Index_ = -1;

        protected virtual IEnumerable<MatchItem> Add(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _Add_Body_ = null;

            if (_Add_Body__Index_ == -1 || CachedCombinators[_Add_Body__Index_] == null)
            {
                if (_Add_Body__Index_ == -1)
                {
                    _Add_Body__Index_ = CachedCombinators.Count;
                    CachedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _ACTION(_CALL(BinaryOp, (new List<MatchItem> { new MatchItem(Additive), new MatchItem('+', CONV), new MatchItem(Multiplicative)}), false), (_IM_Result_MI_) => {{ var _IM_Result = new CalcMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 52 "Calc.ironmeta"
                                                     { return _IM_Result.Results.Aggregate((total, n) => total + n); }
#line default
}});
                }

                CachedCombinators[_Add_Body__Index_] = _disj_0_;
            }

            _Add_Body_ = CachedCombinators[_Add_Body__Index_];


            foreach (var _res_ in _Add_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        private int _Sub_Body__Index_ = -1;

        protected virtual IEnumerable<MatchItem> Sub(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _Sub_Body_ = null;

            if (_Sub_Body__Index_ == -1 || CachedCombinators[_Sub_Body__Index_] == null)
            {
                if (_Sub_Body__Index_ == -1)
                {
                    _Sub_Body__Index_ = CachedCombinators.Count;
                    CachedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _ACTION(_CALL(BinaryOp, (new List<MatchItem> { new MatchItem(Additive), new MatchItem('-', CONV), new MatchItem(Multiplicative)}), false), (_IM_Result_MI_) => {{ var _IM_Result = new CalcMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 53 "Calc.ironmeta"
                                                     { return _IM_Result.Results.Aggregate((total, n) => total - n); }
#line default
}});
                }

                CachedCombinators[_Sub_Body__Index_] = _disj_0_;
            }

            _Sub_Body_ = CachedCombinators[_Sub_Body__Index_];


            foreach (var _res_ in _Sub_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        private int _Multiplicative_Body__Index_ = -1;

        protected virtual IEnumerable<MatchItem> Multiplicative(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _Multiplicative_Body_ = null;

            if (_Multiplicative_Body__Index_ == -1 || CachedCombinators[_Multiplicative_Body__Index_] == null)
            {
                if (_Multiplicative_Body__Index_ == -1)
                {
                    _Multiplicative_Body__Index_ = CachedCombinators.Count;
                    CachedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _OR(_CALL(Multiply, false), _CALL(Divide, false));
                }
                Combinator _disj_1_ = null;
                {
                    _disj_1_ = _CALL(Number, (new List<MatchItem> { new MatchItem(DecimalDigit)}), false);
                }

                CachedCombinators[_Multiplicative_Body__Index_] = _OR(_disj_0_, _disj_1_);
            }

            _Multiplicative_Body_ = CachedCombinators[_Multiplicative_Body__Index_];


            foreach (var _res_ in _Multiplicative_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        private int _Multiply_Body__Index_ = -1;

        protected virtual IEnumerable<MatchItem> Multiply(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _Multiply_Body_ = null;

            if (_Multiply_Body__Index_ == -1 || CachedCombinators[_Multiply_Body__Index_] == null)
            {
                if (_Multiply_Body__Index_ == -1)
                {
                    _Multiply_Body__Index_ = CachedCombinators.Count;
                    CachedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _ACTION(_CALL(BinaryOp, (new List<MatchItem> { new MatchItem(Multiplicative), new MatchItem("*", CONV), new MatchItem(Number), new MatchItem(DecimalDigit)}), false), (_IM_Result_MI_) => {{ var _IM_Result = new CalcMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 58 "Calc.ironmeta"
                                                                      { return _IM_Result.Results.Aggregate((p, n) => p * n); }
#line default
}});
                }

                CachedCombinators[_Multiply_Body__Index_] = _disj_0_;
            }

            _Multiply_Body_ = CachedCombinators[_Multiply_Body__Index_];


            foreach (var _res_ in _Multiply_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        private int _Divide_Body__Index_ = -1;

        protected virtual IEnumerable<MatchItem> Divide(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _Divide_Body_ = null;

            if (_Divide_Body__Index_ == -1 || CachedCombinators[_Divide_Body__Index_] == null)
            {
                if (_Divide_Body__Index_ == -1)
                {
                    _Divide_Body__Index_ = CachedCombinators.Count;
                    CachedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _ACTION(_CALL(BinaryOp, (new List<MatchItem> { new MatchItem(Multiplicative), new MatchItem("/", CONV), new MatchItem(Number), new MatchItem(DecimalDigit)}), false), (_IM_Result_MI_) => {{ var _IM_Result = new CalcMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 59 "Calc.ironmeta"
                                                                    { return _IM_Result.Results.Aggregate((q, n) => q / n); }
#line default
}});
                }

                CachedCombinators[_Divide_Body__Index_] = _disj_0_;
            }

            _Divide_Body_ = CachedCombinators[_Divide_Body__Index_];


            foreach (var _res_ in _Divide_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> BinaryOp(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _BinaryOp_Body_ = null;

            Combinator _disj_0_ = null;
            {
                var first = new CalcMatcherItem("first");
                var op = new CalcMatcherItem("op");
                var second = new CalcMatcherItem("second");
                var type = new CalcMatcherItem("type");
                var a = new CalcMatcherItem("a");
                var b = new CalcMatcherItem("b");
                _disj_0_ = _ARGS(_AND(_VAR(_ANY(), first), _VAR(_ANY(), op), _VAR(_ANY(), second), _VAR(_QUES(_ANY()), type)), _args, _ACTION(_AND(_VAR(_REF(first, this), a), _CALL(KW, (new List<MatchItem> { op}), false), _VAR(_CALL(second, (new List<MatchItem> { type}), false), b)), (_IM_Result_MI_) => {{ 
#line 61 "Calc.ironmeta"
                                                                           { return new List<int> { a, b }; }
#line default
}}));
            }

            _BinaryOp_Body_ = _disj_0_;

            foreach (var _res_ in _BinaryOp_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> Number(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _Number_Body_ = null;

            Combinator _disj_0_ = null;
            {
                var type = new CalcMatcherItem("type");
                var n = new CalcMatcherItem("n");
                var Whitespace = new CalcMatcherItem("Whitespace");
                _disj_0_ = _ARGS(_VAR(_ANY(), type), _args, _ACTION(_AND(_VAR(_CALL(Digits, (new List<MatchItem> { type}), false), n), _STAR(_REF(Whitespace, this))), (_IM_Result_MI_) => {{ 
#line 63 "Calc.ironmeta"
                                                 { return n; }
#line default
}}));
            }

            _Number_Body_ = _disj_0_;

            foreach (var _res_ in _Number_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> Digits(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _Digits_Body_ = null;

            Combinator _disj_0_ = null;
            {
                var type = new CalcMatcherItem("type");
                var a = new CalcMatcherItem("a");
                var b = new CalcMatcherItem("b");
                _disj_0_ = _ARGS(_VAR(_ANY(), type), _args, _ACTION(_AND(_VAR(_CALL(Digits, (new List<MatchItem> { type}), false), a), _VAR(_REF(type, this), b)), (_IM_Result_MI_) => {{ 
#line 66 "Calc.ironmeta"
                                            { return a*10 + b; }
#line default
}}));
            }
            Combinator _disj_1_ = null;
            {
                var type = new CalcMatcherItem("type");
                _disj_1_ = _ARGS(_VAR(_ANY(), type), _args, _REF(type, this));
            }

            _Digits_Body_ = _OR(_disj_0_, _disj_1_);

            foreach (var _res_ in _Digits_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> KW(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _KW_Body_ = null;

            Combinator _disj_0_ = null;
            {
                var str = new CalcMatcherItem("str");
                var Whitespace = new CalcMatcherItem("Whitespace");
                _disj_0_ = _ARGS(_VAR(_ANY(), str), _args, _AND(_REF(str, this), _STAR(_REF(Whitespace, this))));
            }

            _KW_Body_ = _disj_0_;

            foreach (var _res_ in _KW_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        private int _Zero_Body__Index_ = -1;

        protected virtual IEnumerable<MatchItem> Zero(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _Zero_Body_ = null;

            if (_Zero_Body__Index_ == -1 || CachedCombinators[_Zero_Body__Index_] == null)
            {
                if (_Zero_Body__Index_ == -1)
                {
                    _Zero_Body__Index_ = CachedCombinators.Count;
                    CachedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _AND(_LITERAL("ze"), _LITERAL(new List<char>{ 'r', 'o' }));
                }

                CachedCombinators[_Zero_Body__Index_] = _disj_0_;
            }

            _Zero_Body_ = CachedCombinators[_Zero_Body__Index_];


            foreach (var _res_ in _Zero_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        private int _Zero2_Body__Index_ = -1;

        protected virtual IEnumerable<MatchItem> Zero2(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _Zero2_Body_ = null;

            if (_Zero2_Body__Index_ == -1 || CachedCombinators[_Zero2_Body__Index_] == null)
            {
                if (_Zero2_Body__Index_ == -1)
                {
                    _Zero2_Body__Index_ = CachedCombinators.Count;
                    CachedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _CALL(Zero, true);
                }

                CachedCombinators[_Zero2_Body__Index_] = _disj_0_;
            }

            _Zero2_Body_ = CachedCombinators[_Zero2_Body__Index_];


            foreach (var _res_ in _Zero2_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        private int _A_Body__Index_ = -1;

        protected virtual IEnumerable<MatchItem> A(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _A_Body_ = null;

            if (_A_Body__Index_ == -1 || CachedCombinators[_A_Body__Index_] == null)
            {
                if (_A_Body__Index_ == -1)
                {
                    _A_Body__Index_ = CachedCombinators.Count;
                    CachedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _OR(_AND(_CALL(A, false), _LITERAL('a')), _CALL(B, false));
                }

                CachedCombinators[_A_Body__Index_] = _disj_0_;
            }

            _A_Body_ = CachedCombinators[_A_Body__Index_];


            foreach (var _res_ in _A_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        private int _B_Body__Index_ = -1;

        protected virtual IEnumerable<MatchItem> B(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _B_Body_ = null;

            if (_B_Body__Index_ == -1 || CachedCombinators[_B_Body__Index_] == null)
            {
                if (_B_Body__Index_ == -1)
                {
                    _B_Body__Index_ = CachedCombinators.Count;
                    CachedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _OR(_AND(_CALL(B, false), _LITERAL('b')), _CALL(A, false), _CALL(C, false));
                }

                CachedCombinators[_B_Body__Index_] = _disj_0_;
            }

            _B_Body_ = CachedCombinators[_B_Body__Index_];


            foreach (var _res_ in _B_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        private int _C_Body__Index_ = -1;

        protected virtual IEnumerable<MatchItem> C(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _C_Body_ = null;

            if (_C_Body__Index_ == -1 || CachedCombinators[_C_Body__Index_] == null)
            {
                if (_C_Body__Index_ == -1)
                {
                    _C_Body__Index_ = CachedCombinators.Count;
                    CachedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _OR(_AND(_CALL(C, false), _LITERAL('c')), _CALL(B, false), _LITERAL('d'));
                }

                CachedCombinators[_C_Body__Index_] = _disj_0_;
            }

            _C_Body_ = CachedCombinators[_C_Body__Index_];


            foreach (var _res_ in _C_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

    } // class CalcMatcher

} // namespace Calc
