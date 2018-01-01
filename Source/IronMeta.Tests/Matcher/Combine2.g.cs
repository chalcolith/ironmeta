//
// IronMeta Combine2 Parser; Generated 2016-03-30 04:29:18Z UTC
//

using System;
using System.Collections.Generic;
using System.Linq;

using IronMeta.Matcher;

#pragma warning disable 0219
#pragma warning disable 1591

namespace IronMeta.UnitTests.Matcher
{

    using _Combine2_Inputs = IEnumerable<char>;
    using _Combine2_Results = IEnumerable<int>;
    using _Combine2_Item = IronMeta.Matcher.MatchItem<char, int>;
    using _Combine2_Args = IEnumerable<IronMeta.Matcher.MatchItem<char, int>>;
    using _Combine2_Memo = IronMeta.Matcher.MatchState<char, int>;
    using _Combine2_Rule = System.Action<IronMeta.Matcher.MatchState<char, int>, int, IEnumerable<IronMeta.Matcher.MatchItem<char, int>>>;
    using _Combine2_Base = IronMeta.Matcher.Matcher<char, int>;

    public partial class Combine2 : Combine1
    {
        public Combine2()
            : base()
        {
            _setTerminals();
        }

        public Combine2(bool handle_left_recursion)
            : base(handle_left_recursion)
        {
            _setTerminals();
        }

        void _setTerminals()
        {
            this.Terminals = new HashSet<string>()
            {
                "Rule1",
                "Rule2",
                "Rule5",
                "Rule6",
                "VirtualRule",
            };
        }


        public new  void Rule1(_Combine2_Memo _memo, int _index, _Combine2_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // LITERAL "ghi"
            _ParseLiteralString(_memo, ref _index, "ghi");

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _Combine2_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return 3; }, _r0), true) );
            }

        }


        public new  void Rule2(_Combine2_Memo _memo, int _index, _Combine2_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // LITERAL "jkl"
            _ParseLiteralString(_memo, ref _index, "jkl");

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _Combine2_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return 4; }, _r0), true) );
            }

        }


        public void Rule5(_Combine2_Memo _memo, int _index, _Combine2_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // CALLORVAR OtherCombine.Rule1
            _Combine2_Item _r0;

            _r0 = _MemoCall(_memo, "OtherCombine.Rule1", _index, OtherCombine.Rule1, null);

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void Rule6(_Combine2_Memo _memo, int _index, _Combine2_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // CALLORVAR OtherCombine.Rule2
            _Combine2_Item _r0;

            _r0 = _MemoCall(_memo, "OtherCombine.Rule2", _index, OtherCombine.Rule2, null);

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public override  void VirtualRule(_Combine2_Memo _memo, int _index, _Combine2_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // LITERAL "override"
            _ParseLiteralString(_memo, ref _index, "override");

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _Combine2_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return 314; }, _r0), true) );
            }

        }


    } // class Combine2

} // namespace IronMeta.UnitTests.Matcher

