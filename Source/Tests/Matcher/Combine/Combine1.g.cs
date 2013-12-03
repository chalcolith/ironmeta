//
// IronMeta Combine1 Parser; Generated 2013-08-21 05:02:09Z UTC
//

using System;
using System.Collections.Generic;
using System.Linq;
using IronMeta.Matcher;

#pragma warning disable 1591

namespace IronMeta.Tests.Matcher.Combine
{

    using _Combine1_Inputs = IEnumerable<char>;
    using _Combine1_Results = IEnumerable<int>;
    using _Combine1_Item = IronMeta.Matcher.MatchItem<char, int>;
    using _Combine1_Args = IEnumerable<IronMeta.Matcher.MatchItem<char, int>>;
    using _Combine1_Memo = Memo<char, int>;
    using _Combine1_Rule = System.Action<Memo<char, int>, int, IEnumerable<IronMeta.Matcher.MatchItem<char, int>>>;
    using _Combine1_Base = IronMeta.Matcher.Matcher<char, int>;

    public partial class Combine1 : IronMeta.Matcher.CharMatcher<int>
    {
        public Combine1()
            : base()
        { }

        public Combine1(bool handle_left_recursion)
            : base(handle_left_recursion)
        { }

        public void Rule1(_Combine1_Memo _memo, int _index, _Combine1_Args _args)
        {

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


        public virtual void VirtualRule(_Combine1_Memo _memo, int _index, _Combine1_Args _args)
        {

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

} // namespace IronMeta.Tests.Matcher.Combine

