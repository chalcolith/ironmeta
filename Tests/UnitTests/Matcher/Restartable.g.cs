//
// IronMeta Restartable Parser; Generated 2016-01-11 00:43:20Z UTC
//

using System;
using System.Collections.Generic;
using System.Linq;

using IronMeta.Matcher;

#pragma warning disable 0219
#pragma warning disable 1591

namespace IronMeta.UnitTests.Matcher
{

    using _Restartable_Inputs = IEnumerable<char>;
    using _Restartable_Results = IEnumerable<int>;
    using _Restartable_Item = IronMeta.Matcher.MatchItem<char, int>;
    using _Restartable_Args = IEnumerable<IronMeta.Matcher.MatchItem<char, int>>;
    using _Restartable_Memo = IronMeta.Matcher.MatchState<char, int>;
    using _Restartable_Rule = System.Action<IronMeta.Matcher.MatchState<char, int>, int, IEnumerable<IronMeta.Matcher.MatchItem<char, int>>>;
    using _Restartable_Base = IronMeta.Matcher.Matcher<char, int>;

    public partial class Restartable : IronMeta.Matcher.Matcher<char, int>
    {
        public Restartable()
            : base()
        {
            _setTerminals();
        }

        public Restartable(bool handle_left_recursion)
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
            };
        }


        public void Rule1(_Restartable_Memo _memo, int _index, _Restartable_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // AND 1
            int _start_i1 = _index;

            // LITERAL 'a'
            _ParseLiteralChar(_memo, ref _index, 'a');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // LITERAL 'b'
            _ParseLiteralChar(_memo, ref _index, 'b');

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _Restartable_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i1;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // LITERAL 'c'
            _ParseLiteralChar(_memo, ref _index, 'c');

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _Restartable_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void Rule2(_Restartable_Memo _memo, int _index, _Restartable_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // AND 1
            int _start_i1 = _index;

            // LITERAL 'd'
            _ParseLiteralChar(_memo, ref _index, 'd');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // LITERAL 'e'
            _ParseLiteralChar(_memo, ref _index, 'e');

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _Restartable_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i1;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // LITERAL 'f'
            _ParseLiteralChar(_memo, ref _index, 'f');

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _Restartable_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


    } // class Restartable

} // namespace IronMeta.UnitTests.Matcher

