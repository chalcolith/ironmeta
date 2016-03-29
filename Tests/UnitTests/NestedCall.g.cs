//
// IronMeta NestedCall Parser; Generated 2016-03-29 23:46:59Z UTC
//

using System;
using System.Collections.Generic;
using System.Linq;

using IronMeta.Matcher;

#pragma warning disable 0219
#pragma warning disable 1591

namespace IronMeta.UnitTests
{

    using _NestedCall_Inputs = IEnumerable<char>;
    using _NestedCall_Results = IEnumerable<int>;
    using _NestedCall_Item = IronMeta.Matcher.MatchItem<char, int>;
    using _NestedCall_Args = IEnumerable<IronMeta.Matcher.MatchItem<char, int>>;
    using _NestedCall_Memo = IronMeta.Matcher.MatchState<char, int>;
    using _NestedCall_Rule = System.Action<IronMeta.Matcher.MatchState<char, int>, int, IEnumerable<IronMeta.Matcher.MatchItem<char, int>>>;
    using _NestedCall_Base = IronMeta.Matcher.Matcher<char, int>;

    public partial class NestedCall : Matcher<char, int>
    {
        public NestedCall()
            : base()
        {
            _setTerminals();
        }

        public NestedCall(bool handle_left_recursion)
            : base(handle_left_recursion)
        {
            _setTerminals();
        }

        void _setTerminals()
        {
            this.Terminals = new HashSet<string>()
            {
                "__nested_rule_0",
                "__nested_rule_2",
                "A",
                "B",
                "D",
                "E",
                "F",
                "G",
                "I",
                "One",
                "Two",
            };
        }


        public void One(_NestedCall_Memo _memo, int _index, _NestedCall_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // LITERAL "one"
            _ParseLiteralString(_memo, ref _index, "one");

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _NestedCall_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return 1; }, _r0), true) );
            }

        }


        public void Two(_NestedCall_Memo _memo, int _index, _NestedCall_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // LITERAL "two"
            _ParseLiteralString(_memo, ref _index, "two");

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _NestedCall_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return 2; }, _r0), true) );
            }

        }


        public void Alpha(_NestedCall_Memo _memo, int _index, _NestedCall_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            _NestedCall_Item x = null;
            _NestedCall_Item a = null;

            // ARGS 0
            _arg_index = 0;
            _arg_input_index = 0;

            // ANY
            _ParseAnyArgs(_memo, ref _arg_index, ref _arg_input_index, _args);

            // BIND x
            x = _memo.ArgResults.Peek();

            if (_memo.ArgResults.Pop() == null)
            {
                _memo.Results.Push(null);
                goto label0;
            }

            // AND 4
            int _start_i4 = _index;

            // CALLORVAR x
            _NestedCall_Item _r6;

            if (x.Production != null)
            {
                var _p6 = (System.Action<_NestedCall_Memo, int, IEnumerable<_NestedCall_Item>>)(object)x.Production;
                _r6 = _MemoCall(_memo, x.Production.Method.Name, _index, _p6, _args != null ? _args.Skip(_arg_index) : null);
            }
            else
            {
                _r6 = _ParseLiteralObj(_memo, ref _index, x.Inputs);
            }

            if (_r6 != null) _index = _r6.NextIndex;

            // BIND a
            a = _memo.Results.Peek();

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label4; }

            // LITERAL "a"
            _ParseLiteralString(_memo, ref _index, "a");

        label4: // AND
            var _r4_2 = _memo.Results.Pop();
            var _r4_1 = _memo.Results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _memo.Results.Push( new _NestedCall_Item(_start_i4, _index, _memo.InputEnumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i4;
            }

            // ACT
            var _r3 = _memo.Results.Peek();
            if (_r3 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _NestedCall_Item(_r3.StartIndex, _r3.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return a; }, _r3), true) );
            }

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void A(_NestedCall_Memo _memo, int _index, _NestedCall_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // CALL Alpha
            var _start_i0 = _index;
            _NestedCall_Item _r0;

            _NestedCall_Args _actual_args0 = new _NestedCall_Item[] { new _NestedCall_Item(One) };
            if (_args != null) _actual_args0 = _actual_args0.Concat(_args.Skip(_arg_index));
            _r0 = _MemoCall(_memo, "Alpha", _index, Alpha, _actual_args0);

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void B(_NestedCall_Memo _memo, int _index, _NestedCall_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // CALL Alpha
            var _start_i0 = _index;
            _NestedCall_Item _r0;

            _NestedCall_Args _actual_args0 = new _NestedCall_Item[] { new _NestedCall_Item(__nested_rule_0) };
            if (_args != null) _actual_args0 = _actual_args0.Concat(_args.Skip(_arg_index));
            _r0 = _MemoCall(_memo, "Alpha", _index, Alpha, _actual_args0);

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void __nested_rule_0(_NestedCall_Memo _memo, int _index, _NestedCall_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // AND 0
            int _start_i0 = _index;

            // CALLORVAR Two
            _NestedCall_Item _r1;

            _r1 = _MemoCall(_memo, "Two", _index, Two, null);

            if (_r1 != null) _index = _r1.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // LITERAL "b"
            _ParseLiteralString(_memo, ref _index, "b");

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _NestedCall_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void C(_NestedCall_Memo _memo, int _index, _NestedCall_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            _NestedCall_Item c = null;

            // ARGS 0
            _arg_index = 0;
            _arg_input_index = 0;

            // ANY
            _ParseAnyArgs(_memo, ref _arg_index, ref _arg_input_index, _args);

            // BIND c
            c = _memo.ArgResults.Peek();

            if (_memo.ArgResults.Pop() == null)
            {
                _memo.Results.Push(null);
                goto label0;
            }

            // CALL Alpha
            var _start_i3 = _index;
            _NestedCall_Item _r3;

            _NestedCall_Args _actual_args3 = new _NestedCall_Item[] { new _NestedCall_Item(__nested_rule_1), c };
            if (_args != null) _actual_args3 = _actual_args3.Concat(_args.Skip(_arg_index));
            _r3 = _MemoCall(_memo, "Alpha", _index, Alpha, _actual_args3);

            if (_r3 != null) _index = _r3.NextIndex;

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void __nested_rule_1(_NestedCall_Memo _memo, int _index, _NestedCall_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            _NestedCall_Item c = null;

            // ARGS 0
            _arg_index = 0;
            _arg_input_index = 0;

            // ANY
            _ParseAnyArgs(_memo, ref _arg_index, ref _arg_input_index, _args);

            // BIND c
            c = _memo.ArgResults.Peek();

            if (_memo.ArgResults.Pop() == null)
            {
                _memo.Results.Push(null);
                goto label0;
            }

            // AND 3
            int _start_i3 = _index;

            // CALLORVAR c
            _NestedCall_Item _r4;

            if (c.Production != null)
            {
                var _p4 = (System.Action<_NestedCall_Memo, int, IEnumerable<_NestedCall_Item>>)(object)c.Production;
                _r4 = _MemoCall(_memo, c.Production.Method.Name, _index, _p4, _args != null ? _args.Skip(_arg_index) : null);
            }
            else
            {
                _r4 = _ParseLiteralObj(_memo, ref _index, c.Inputs);
            }

            if (_r4 != null) _index = _r4.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label3; }

            // LITERAL "c"
            _ParseLiteralString(_memo, ref _index, "c");

        label3: // AND
            var _r3_2 = _memo.Results.Pop();
            var _r3_1 = _memo.Results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _memo.Results.Push( new _NestedCall_Item(_start_i3, _index, _memo.InputEnumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i3;
            }

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void D(_NestedCall_Memo _memo, int _index, _NestedCall_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // CALL C
            var _start_i0 = _index;
            _NestedCall_Item _r0;

            _NestedCall_Args _actual_args0 = new _NestedCall_Item[] { new _NestedCall_Item(One) };
            if (_args != null) _actual_args0 = _actual_args0.Concat(_args.Skip(_arg_index));
            _r0 = _MemoCall(_memo, "C", _index, C, _actual_args0);

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void E(_NestedCall_Memo _memo, int _index, _NestedCall_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // CALL Alpha
            var _start_i0 = _index;
            _NestedCall_Item _r0;

            _NestedCall_Args _actual_args0 = new _NestedCall_Item[] { new _NestedCall_Item(__nested_rule_2) };
            if (_args != null) _actual_args0 = _actual_args0.Concat(_args.Skip(_arg_index));
            _r0 = _MemoCall(_memo, "Alpha", _index, Alpha, _actual_args0);

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void __nested_rule_2(_NestedCall_Memo _memo, int _index, _NestedCall_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // AND 0
            int _start_i0 = _index;

            // LITERAL "e"
            _ParseLiteralString(_memo, ref _index, "e");

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // LITERAL "f"
            _ParseLiteralString(_memo, ref _index, "f");

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _NestedCall_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void F(_NestedCall_Memo _memo, int _index, _NestedCall_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // CALL C
            var _start_i0 = _index;
            _NestedCall_Item _r0;
            var _arg0_0 = "a";

            _NestedCall_Args _actual_args0 = new _NestedCall_Item[] { new _NestedCall_Item(_arg0_0) };
            if (_args != null) _actual_args0 = _actual_args0.Concat(_args.Skip(_arg_index));
            _r0 = _MemoCall(_memo, "C", _index, C, _actual_args0);

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void G(_NestedCall_Memo _memo, int _index, _NestedCall_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // CALL C
            var _start_i0 = _index;
            _NestedCall_Item _r0;

            _NestedCall_Args _actual_args0 = new _NestedCall_Item[] { new _NestedCall_Item(Alpha) };
            if (_args != null) _actual_args0 = _actual_args0.Concat(_args.Skip(_arg_index));
            _r0 = _MemoCall(_memo, "C", _index, C, _actual_args0);

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void H(_NestedCall_Memo _memo, int _index, _NestedCall_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            _NestedCall_Item h = null;

            // ARGS 0
            _arg_index = 0;
            _arg_input_index = 0;

            // ANY
            _ParseAnyArgs(_memo, ref _arg_index, ref _arg_input_index, _args);

            // BIND h
            h = _memo.ArgResults.Peek();

            if (_memo.ArgResults.Pop() == null)
            {
                _memo.Results.Push(null);
                goto label0;
            }

            // CALL C
            var _start_i3 = _index;
            _NestedCall_Item _r3;

            _NestedCall_Args _actual_args3 = new _NestedCall_Item[] { new _NestedCall_Item(Alpha) };
            if (_args != null) _actual_args3 = _actual_args3.Concat(_args.Skip(_arg_index));
            _r3 = _MemoCall(_memo, "C", _index, C, _actual_args3);

            if (_r3 != null) _index = _r3.NextIndex;

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void I(_NestedCall_Memo _memo, int _index, _NestedCall_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // CALL H
            var _start_i0 = _index;
            _NestedCall_Item _r0;
            var _arg0_0 = "i";

            _NestedCall_Args _actual_args0 = new _NestedCall_Item[] { new _NestedCall_Item(_arg0_0) };
            if (_args != null) _actual_args0 = _actual_args0.Concat(_args.Skip(_arg_index));
            _r0 = _MemoCall(_memo, "H", _index, H, _actual_args0);

            if (_r0 != null) _index = _r0.NextIndex;

        }


    } // class NestedCall

} // namespace IronMeta.UnitTests

