//
// IronMeta Combine1 Parser; Generated 2016-03-30 04:29:18Z UTC
//

using System;
using System.Collections.Generic;
using System.Linq;

using IronMeta.Matcher;

#pragma warning disable 0219
#pragma warning disable 1591

namespace IronMeta.UnitTests.Matcher
{

    using _Combine1_Inputs = IEnumerable<char>;
    using _Combine1_Results = IEnumerable<int>;
    using _Combine1_Item = IronMeta.Matcher.MatchItem<char, int>;
    using _Combine1_Args = IEnumerable<IronMeta.Matcher.MatchItem<char, int>>;
    using _Combine1_Memo = IronMeta.Matcher.MatchState<char, int>;
    using _Combine1_Rule = System.Action<IronMeta.Matcher.MatchState<char, int>, int, IEnumerable<IronMeta.Matcher.MatchItem<char, int>>>;
    using _Combine1_Base = IronMeta.Matcher.Matcher<char, int>;

    public partial class Combine1 : Matcher<char, int>
    {
        public Combine1()
            : base()
        {
            _setTerminals();
        }

        public Combine1(bool handle_left_recursion)
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
                "VirtualRule",
            };
        }


        public void Rule1(_Combine1_Memo _memo, int _index, _Combine1_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // LITERAL "abc"
            _ParseLiteralString(_memo, ref _index, "abc");

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _Combine1_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return 1; }, _r0), true) );
            }

        }


        public void Rule2(_Combine1_Memo _memo, int _index, _Combine1_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // LITERAL "def"
            _ParseLiteralString(_memo, ref _index, "def");

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _Combine1_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return 2; }, _r0), true) );
            }

        }


        public virtual  void VirtualRule(_Combine1_Memo _memo, int _index, _Combine1_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // LITERAL "virtual"
            _ParseLiteralString(_memo, ref _index, "virtual");

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _Combine1_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return 42; }, _r0), true) );
            }

        }


    } // class Combine1

} // namespace IronMeta.UnitTests.Matcher

