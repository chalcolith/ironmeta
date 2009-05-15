// IronMeta Generated Calc: 15/05/2009 12:15:12 AM UTC

using System;
using System.Collections.Generic;
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

        protected virtual IEnumerable<MatchItem> Expression(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _Expression_Body_ = null;

            Combinator _disj_0_ = null;
            {
                _disj_0_ = _CALL(Additive);
            }

            _Expression_Body_ = _disj_0_;

            foreach (var _res_ in _Expression_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> Additive(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _Additive_Body_ = null;

            Combinator _disj_0_ = null;
            {
                _disj_0_ = _OR(_OR(_CALL(Add), _CALL(Sub)), _CALL(Multiplicative));
            }

            _Additive_Body_ = _disj_0_;

            foreach (var _res_ in _Additive_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> Add(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _Add_Body_ = null;

            Combinator _disj_0_ = null;
            {
                _disj_0_ = _ACTION(_CALL(BinaryOp, new List<MatchItem> { new MatchItem(Additive), new MatchItem('+', CONV), new MatchItem(Multiplicative) }), (_IM_Result_MI_) => { var _IM_Result = new CalcMatcherMatchItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 33 "Calc.ironmeta"
       { return _IM_Result.Results.Aggregate((total, n) => total + n); }
#line default
});
            }

            _Add_Body_ = _disj_0_;

            foreach (var _res_ in _Add_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> Sub(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _Sub_Body_ = null;

            Combinator _disj_0_ = null;
            {
                _disj_0_ = _ACTION(_CALL(BinaryOp, new List<MatchItem> { new MatchItem(Additive), new MatchItem('-', CONV), new MatchItem(Multiplicative) }), (_IM_Result_MI_) => { var _IM_Result = new CalcMatcherMatchItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 34 "Calc.ironmeta"
       { return _IM_Result.Results.Aggregate((total, n) => total - n); }
#line default
});
            }

            _Sub_Body_ = _disj_0_;

            foreach (var _res_ in _Sub_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> Multiplicative(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _Multiplicative_Body_ = null;

            Combinator _disj_0_ = null;
            {
                _disj_0_ = _OR(_CALL(Multiply), _CALL(Divide));
            }
            Combinator _disj_1_ = null;
            {
                _disj_1_ = _CALL(Number);
            }

            _Multiplicative_Body_ = _OR(_disj_0_, _disj_1_);

            foreach (var _res_ in _Multiplicative_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> Multiply(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _Multiply_Body_ = null;

            Combinator _disj_0_ = null;
            {
                _disj_0_ = _ACTION(_CALL(BinaryOp, new List<MatchItem> { new MatchItem(Multiplicative), new MatchItem('*', CONV), new MatchItem(Number) }), (_IM_Result_MI_) => { var _IM_Result = new CalcMatcherMatchItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 39 "Calc.ironmeta"
            { return _IM_Result.Results.Aggregate((p, n) => p * n); }
#line default
});
            }

            _Multiply_Body_ = _disj_0_;

            foreach (var _res_ in _Multiply_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> Divide(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _Divide_Body_ = null;

            Combinator _disj_0_ = null;
            {
                _disj_0_ = _ACTION(_CALL(BinaryOp, new List<MatchItem> { new MatchItem(Multiplicative), new MatchItem('/', CONV), new MatchItem(Number) }), (_IM_Result_MI_) => { var _IM_Result = new CalcMatcherMatchItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 40 "Calc.ironmeta"
          { return _IM_Result.Results.Aggregate((q, n) => q / n); }
#line default
});
            }

            _Divide_Body_ = _disj_0_;

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
                var a = new CalcMatcherMatchItem();
                var b = new CalcMatcherMatchItem();
                _disj_0_ = _ARGS(_AND(_AND(_VAR(_ANY(), first), _VAR(_ANY(), op)), _VAR(_ANY(), second)), _args, _ACTION(_AND(_AND(_VAR(_REF(first, "first", this), a), _CALL(KW, new List<MatchItem> { op })), _VAR(_REF(second, "second", this), b)), (_IM_Result_MI_) => { var _IM_Result = new CalcMatcherMatchItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 42 "Calc.ironmeta"
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
                var digits = new CalcMatcherMatchItem();
                _disj_0_ = _ACTION(_AND(_VAR(_PLUS(_CALL(Digit)), digits), _STAR(_CALL(Whitespace))), (_IM_Result_MI_) => { var _IM_Result = new CalcMatcherMatchItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 44 "Calc.ironmeta"
          { return digits.Results.Aggregate(0, (sum, n) => sum*10 + n); }
#line default
});
            }

            _Number_Body_ = _disj_0_;

            foreach (var _res_ in _Number_Body_.Match(_indent+1, _inputs, _index, null, _memo))
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
                var res = new CalcMatcherMatchItem();
                _disj_0_ = _ARGS(_VAR(_STAR(_ANY()), str), _args, _AND(_VAR(_REF(str, "str", this), res), _STAR(_CALL(Whitespace))));
            }

            _KW_Body_ = _disj_0_;

            foreach (var _res_ in _KW_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> Digit(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _Digit_Body_ = null;

            Combinator _disj_0_ = null;
            {
                _disj_0_ = _ACTION(_OR(_OR(_OR(_OR(_OR(_OR(_OR(_OR(_OR(_LITERAL('0'), _LITERAL('1')), _LITERAL('2')), _LITERAL('3')), _LITERAL('4')), _LITERAL('5')), _LITERAL('6')), _LITERAL('7')), _LITERAL('8')), _LITERAL('9')), (_IM_Result_MI_) => { var _IM_Result = new CalcMatcherMatchItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 48 "Calc.ironmeta"
         { return (int)_IM_Result - '0'; }
#line default
});
            }

            _Digit_Body_ = _disj_0_;

            foreach (var _res_ in _Digit_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

    } // class CalcMatcher

} // namespace Calc

