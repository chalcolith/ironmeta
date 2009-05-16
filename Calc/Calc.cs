// IronMeta Generated Calc: 16/05/2009 8:46:03 AM UTC

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
        private class CalcMatcherMatchItem : MatchItem
        {
            public CalcMatcherMatchItem() : base() { }

            public CalcMatcherMatchItem(MatchItem mi)
                : base(mi)
            {
            }

            public static implicit operator char(CalcMatcherMatchItem item) { return item.Inputs.LastOrDefault(); }
            public static implicit operator List<char>(CalcMatcherMatchItem item) { return item.Inputs.ToList(); }
            public static implicit operator int(CalcMatcherMatchItem item) { return item.Results.LastOrDefault(); }
            public static implicit operator List<int>(CalcMatcherMatchItem item) { return item.Results.ToList(); }
        }

        private int _Expression_Body__Index_ = -1;

        protected virtual IEnumerable<MatchItem> Expression(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _Expression_Body_ = null;

            if (_Expression_Body__Index_ == -1 || PredefinedCombinators[_Expression_Body__Index_] == null)
            {
                if (_Expression_Body__Index_ == -1)
                {
                    _Expression_Body__Index_ = PredefinedCombinators.Count;
                    PredefinedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _CALL(Additive);
                }

                PredefinedCombinators[_Expression_Body__Index_] = _disj_0_;
            }

            _Expression_Body_ = PredefinedCombinators[_Expression_Body__Index_];


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

            if (_Additive_Body__Index_ == -1 || PredefinedCombinators[_Additive_Body__Index_] == null)
            {
                if (_Additive_Body__Index_ == -1)
                {
                    _Additive_Body__Index_ = PredefinedCombinators.Count;
                    PredefinedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _OR(_OR(_CALL(Add), _CALL(Sub)), _CALL(Multiplicative));
                }

                PredefinedCombinators[_Additive_Body__Index_] = _disj_0_;
            }

            _Additive_Body_ = PredefinedCombinators[_Additive_Body__Index_];


            foreach (var _res_ in _Additive_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> DecimalDigit(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _DecimalDigit_Body_ = null;

            Combinator _disj_0_ = null;
            {
                var c = new CalcMatcherMatchItem();
                _disj_0_ = _ACTION(_CONDITION(_VAR(_ANY(), c), (_IM_Result_MI_) => { var _IM_Result = new CalcMatcherMatchItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; return (
#line 35 "Calc.ironmeta"
                   ( c >= '0' && c <= '9' )
#line default
);}), (_IM_Result_MI_) => { var _IM_Result = new CalcMatcherMatchItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 35 "Calc.ironmeta"
                   { return (int)c - '0'; }
#line default
});
            }

            _DecimalDigit_Body_ = _disj_0_;

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

            if (_Add_Body__Index_ == -1 || PredefinedCombinators[_Add_Body__Index_] == null)
            {
                if (_Add_Body__Index_ == -1)
                {
                    _Add_Body__Index_ = PredefinedCombinators.Count;
                    PredefinedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _ACTION(_CALL(BinaryOp, new List<MatchItem> { new MatchItem(Additive), new MatchItem('+', CONV), new MatchItem(Multiplicative) }), (_IM_Result_MI_) => { var _IM_Result = new CalcMatcherMatchItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 37 "Calc.ironmeta"
          { return _IM_Result.Results.Aggregate((total, n) => total + n); }
#line default
});
                }

                PredefinedCombinators[_Add_Body__Index_] = _disj_0_;
            }

            _Add_Body_ = PredefinedCombinators[_Add_Body__Index_];


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

            if (_Sub_Body__Index_ == -1 || PredefinedCombinators[_Sub_Body__Index_] == null)
            {
                if (_Sub_Body__Index_ == -1)
                {
                    _Sub_Body__Index_ = PredefinedCombinators.Count;
                    PredefinedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _ACTION(_CALL(BinaryOp, new List<MatchItem> { new MatchItem(Additive), new MatchItem('-', CONV), new MatchItem(Multiplicative) }), (_IM_Result_MI_) => { var _IM_Result = new CalcMatcherMatchItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 38 "Calc.ironmeta"
          { return _IM_Result.Results.Aggregate((total, n) => total - n); }
#line default
});
                }

                PredefinedCombinators[_Sub_Body__Index_] = _disj_0_;
            }

            _Sub_Body_ = PredefinedCombinators[_Sub_Body__Index_];


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

            if (_Multiplicative_Body__Index_ == -1 || PredefinedCombinators[_Multiplicative_Body__Index_] == null)
            {
                if (_Multiplicative_Body__Index_ == -1)
                {
                    _Multiplicative_Body__Index_ = PredefinedCombinators.Count;
                    PredefinedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _OR(_OR(_CALL(Multiply), _CALL(Divide)), _CALL(Number, new List<MatchItem> { new MatchItem(DecimalDigit) }));
                }

                PredefinedCombinators[_Multiplicative_Body__Index_] = _disj_0_;
            }

            _Multiplicative_Body_ = PredefinedCombinators[_Multiplicative_Body__Index_];


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

            if (_Multiply_Body__Index_ == -1 || PredefinedCombinators[_Multiply_Body__Index_] == null)
            {
                if (_Multiply_Body__Index_ == -1)
                {
                    _Multiply_Body__Index_ = PredefinedCombinators.Count;
                    PredefinedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _ACTION(_CALL(BinaryOp, new List<MatchItem> { new MatchItem(Multiplicative), new MatchItem("*", CONV), new MatchItem(Number), new MatchItem(DecimalDigit) }), (_IM_Result_MI_) => { var _IM_Result = new CalcMatcherMatchItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 42 "Calc.ironmeta"
               { return _IM_Result.Results.Aggregate((p, n) => p * n); }
#line default
});
                }

                PredefinedCombinators[_Multiply_Body__Index_] = _disj_0_;
            }

            _Multiply_Body_ = PredefinedCombinators[_Multiply_Body__Index_];


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

            if (_Divide_Body__Index_ == -1 || PredefinedCombinators[_Divide_Body__Index_] == null)
            {
                if (_Divide_Body__Index_ == -1)
                {
                    _Divide_Body__Index_ = PredefinedCombinators.Count;
                    PredefinedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _ACTION(_CALL(BinaryOp, new List<MatchItem> { new MatchItem(Multiplicative), new MatchItem("/", CONV), new MatchItem(Number), new MatchItem(DecimalDigit) }), (_IM_Result_MI_) => { var _IM_Result = new CalcMatcherMatchItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 43 "Calc.ironmeta"
             { return _IM_Result.Results.Aggregate((q, n) => q / n); }
#line default
});
                }

                PredefinedCombinators[_Divide_Body__Index_] = _disj_0_;
            }

            _Divide_Body_ = PredefinedCombinators[_Divide_Body__Index_];


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
                var first = new CalcMatcherMatchItem();
                var op = new CalcMatcherMatchItem();
                var second = new CalcMatcherMatchItem();
                var type = new CalcMatcherMatchItem();
                var a = new CalcMatcherMatchItem();
                var b = new CalcMatcherMatchItem();
                _disj_0_ = _ARGS(_AND(_AND(_AND(_VAR(_ANY(), first), _VAR(_ANY(), op)), _VAR(_ANY(), second)), _VAR(_QUES(_ANY()), type)), _args, _ACTION(_AND(_AND(_VAR(_REF(first, "first", this), a), _CALL(KW, new List<MatchItem> { op })), _VAR(_CALL(second, new List<MatchItem> { type }), b)), (_IM_Result_MI_) => { var _IM_Result = new CalcMatcherMatchItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 45 "Calc.ironmeta"
                                          { return new List<int> { a, b }; }
#line default
}));
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
                var type = new CalcMatcherMatchItem();
                var digits = new CalcMatcherMatchItem();
                _disj_0_ = _ARGS(_VAR(_ANY(), type), _args, _ACTION(_AND(_VAR(_PLUS(_REF(type, "type", this)), digits), _STAR(_CALL(Whitespace))), (_IM_Result_MI_) => { var _IM_Result = new CalcMatcherMatchItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 47 "Calc.ironmeta"
                   { return digits.Results.Aggregate(0, (sum, n) => sum*10 + n); }
#line default
}));
            }

            _Number_Body_ = _disj_0_;

            foreach (var _res_ in _Number_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        private int _DecimalNumber_Body__Index_ = -1;

        protected virtual IEnumerable<MatchItem> DecimalNumber(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _DecimalNumber_Body_ = null;

            if (_DecimalNumber_Body__Index_ == -1 || PredefinedCombinators[_DecimalNumber_Body__Index_] == null)
            {
                if (_DecimalNumber_Body__Index_ == -1)
                {
                    _DecimalNumber_Body__Index_ = PredefinedCombinators.Count;
                    PredefinedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _CALL(Number, new List<MatchItem> { new MatchItem(DecimalDigit) });
                }

                PredefinedCombinators[_DecimalNumber_Body__Index_] = _disj_0_;
            }

            _DecimalNumber_Body_ = PredefinedCombinators[_DecimalNumber_Body__Index_];


            foreach (var _res_ in _DecimalNumber_Body_.Match(_indent+1, _inputs, _index, null, _memo))
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
                var str = new CalcMatcherMatchItem();
                _disj_0_ = _ARGS(_VAR(_STAR(_ANY()), str), _args, _AND(_REF(str, "str", this), _STAR(_CALL(Whitespace))));
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

            if (_Zero_Body__Index_ == -1 || PredefinedCombinators[_Zero_Body__Index_] == null)
            {
                if (_Zero_Body__Index_ == -1)
                {
                    _Zero_Body__Index_ = PredefinedCombinators.Count;
                    PredefinedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _AND(_LITERAL("ze"), _LITERAL("ro"));
                }

                PredefinedCombinators[_Zero_Body__Index_] = _disj_0_;
            }

            _Zero_Body_ = PredefinedCombinators[_Zero_Body__Index_];


            foreach (var _res_ in _Zero_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

    } // class CalcMatcher

} // namespace Calc

