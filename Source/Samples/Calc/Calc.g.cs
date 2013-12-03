//
// IronMeta Calc Parser; Generated 2013-04-10 02:07:14Z UTC
//

using System;
using System.Collections.Generic;
using System.Linq;
using IronMeta.Matcher;

#pragma warning disable 0219
#pragma warning disable 1591

namespace Calc
{

    using _Calc_Inputs = IEnumerable<char>;
    using _Calc_Results = IEnumerable<int>;
    using _Calc_Item = IronMeta.Matcher.MatchItem<char, int>;
    using _Calc_Args = IEnumerable<IronMeta.Matcher.MatchItem<char, int>>;
    using _Calc_Memo = Memo<char, int>;
    using _Calc_Rule = System.Action<Memo<char, int>, int, IEnumerable<IronMeta.Matcher.MatchItem<char, int>>>;
    using _Calc_Base = IronMeta.Matcher.Matcher<char, int>;

    public partial class Calc : IronMeta.Matcher.CharMatcher<int>
    {
        public Calc()
            : base()
        { }

        public Calc(bool handle_left_recursion)
            : base(handle_left_recursion)
        { }

        public void Expression(_Calc_Memo _memo, int _index, _Calc_Args _args)
        {

            // CALLORVAR Additive
            _Calc_Item _r0;

            _r0 = _MemoCall(_memo, "Additive", _index, Additive, null);

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void Additive(_Calc_Memo _memo, int _index, _Calc_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // OR 1
            int _start_i1 = _index;

            // CALLORVAR Add
            _Calc_Item _r2;

            _r2 = _MemoCall(_memo, "Add", _index, Add, null);

            if (_r2 != null) _index = _r2.NextIndex;

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i1; } else goto label1;

            // CALLORVAR Sub
            _Calc_Item _r3;

            _r3 = _MemoCall(_memo, "Sub", _index, Sub, null);

            if (_r3 != null) _index = _r3.NextIndex;

        label1: // OR
            int _dummy_i1 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // CALLORVAR Multiplicative
            _Calc_Item _r4;

            _r4 = _MemoCall(_memo, "Multiplicative", _index, Multiplicative, null);

            if (_r4 != null) _index = _r4.NextIndex;

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void Add(_Calc_Memo _memo, int _index, _Calc_Args _args)
        {

            // CALL BinaryOp
            var _start_i1 = _index;
            _Calc_Item _r1;
            var _arg1_0 = '+';

            _r1 = _MemoCall(_memo, "BinaryOp", _index, BinaryOp, new _Calc_Item[] { new _Calc_Item(Additive), new _Calc_Item(_arg1_0), new _Calc_Item(Multiplicative) });

            if (_r1 != null) _index = _r1.NextIndex;

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _Calc_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return _IM_Result.Results.Aggregate((total, n) => total + n); }, _r0), true) );
            }

        }


        public void Sub(_Calc_Memo _memo, int _index, _Calc_Args _args)
        {

            // CALL BinaryOp
            var _start_i1 = _index;
            _Calc_Item _r1;
            var _arg1_0 = '-';

            _r1 = _MemoCall(_memo, "BinaryOp", _index, BinaryOp, new _Calc_Item[] { new _Calc_Item(Additive), new _Calc_Item(_arg1_0), new _Calc_Item(Multiplicative) });

            if (_r1 != null) _index = _r1.NextIndex;

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _Calc_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return _IM_Result.Results.Aggregate((total, n) => total - n); }, _r0), true) );
            }

        }


        public void Multiplicative(_Calc_Memo _memo, int _index, _Calc_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // OR 1
            int _start_i1 = _index;

            // CALLORVAR Multiply
            _Calc_Item _r2;

            _r2 = _MemoCall(_memo, "Multiply", _index, Multiply, null);

            if (_r2 != null) _index = _r2.NextIndex;

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i1; } else goto label1;

            // CALLORVAR Divide
            _Calc_Item _r3;

            _r3 = _MemoCall(_memo, "Divide", _index, Divide, null);

            if (_r3 != null) _index = _r3.NextIndex;

        label1: // OR
            int _dummy_i1 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // CALL Number
            var _start_i4 = _index;
            _Calc_Item _r4;

            _r4 = _MemoCall(_memo, "Number", _index, Number, new _Calc_Item[] { new _Calc_Item(DecimalDigit) });

            if (_r4 != null) _index = _r4.NextIndex;

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void Multiply(_Calc_Memo _memo, int _index, _Calc_Args _args)
        {

            // CALL BinaryOp
            var _start_i1 = _index;
            _Calc_Item _r1;
            var _arg1_0 = "*";

            _r1 = _MemoCall(_memo, "BinaryOp", _index, BinaryOp, new _Calc_Item[] { new _Calc_Item(Multiplicative), new _Calc_Item(_arg1_0), new _Calc_Item(Number), new _Calc_Item(DecimalDigit) });

            if (_r1 != null) _index = _r1.NextIndex;

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _Calc_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return _IM_Result.Results.Aggregate((p, n) => p * n); }, _r0), true) );
            }

        }


        public void Divide(_Calc_Memo _memo, int _index, _Calc_Args _args)
        {

            // CALL BinaryOp
            var _start_i1 = _index;
            _Calc_Item _r1;
            var _arg1_0 = "/";

            _r1 = _MemoCall(_memo, "BinaryOp", _index, BinaryOp, new _Calc_Item[] { new _Calc_Item(Multiplicative), new _Calc_Item(_arg1_0), new _Calc_Item(Number), new _Calc_Item(DecimalDigit) });

            if (_r1 != null) _index = _r1.NextIndex;

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _Calc_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return _IM_Result.Results.Aggregate((q, n) => q / n); }, _r0), true) );
            }

        }


        public void BinaryOp(_Calc_Memo _memo, int _index, _Calc_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            _Calc_Item first = null;
            _Calc_Item op = null;
            _Calc_Item second = null;
            _Calc_Item type = null;
            _Calc_Item a = null;
            _Calc_Item b = null;

            // ARGS 0
            _arg_index = 0;
            _arg_input_index = 0;

            // AND 1
            int _start_i1 = _arg_index;

            // AND 2
            int _start_i2 = _arg_index;

            // AND 3
            int _start_i3 = _arg_index;

            // ANY
            _ParseAnyArgs(_memo, ref _arg_index, ref _arg_input_index, _args);

            // BIND first
            first = _memo.ArgResults.Peek();

            // AND shortcut
            if (_memo.ArgResults.Peek() == null) { _memo.ArgResults.Push(null); goto label3; }

            // ANY
            _ParseAnyArgs(_memo, ref _arg_index, ref _arg_input_index, _args);

            // BIND op
            op = _memo.ArgResults.Peek();

        label3: // AND
            var _r3_2 = _memo.ArgResults.Pop();
            var _r3_1 = _memo.ArgResults.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _memo.ArgResults.Push(new _Calc_Item(_start_i3, _arg_index, _r3_1.Inputs.Concat(_r3_2.Inputs), _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), false));
            }
            else
            {
                _memo.ArgResults.Push(null);
                _arg_index = _start_i3;
            }

            // AND shortcut
            if (_memo.ArgResults.Peek() == null) { _memo.ArgResults.Push(null); goto label2; }

            // ANY
            _ParseAnyArgs(_memo, ref _arg_index, ref _arg_input_index, _args);

            // BIND second
            second = _memo.ArgResults.Peek();

        label2: // AND
            var _r2_2 = _memo.ArgResults.Pop();
            var _r2_1 = _memo.ArgResults.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _memo.ArgResults.Push(new _Calc_Item(_start_i2, _arg_index, _r2_1.Inputs.Concat(_r2_2.Inputs), _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), false));
            }
            else
            {
                _memo.ArgResults.Push(null);
                _arg_index = _start_i2;
            }

            // AND shortcut
            if (_memo.ArgResults.Peek() == null) { _memo.ArgResults.Push(null); goto label1; }

            // ANY
            _ParseAnyArgs(_memo, ref _arg_index, ref _arg_input_index, _args);

            // QUES
            if (_memo.ArgResults.Peek() == null) { _memo.ArgResults.Pop(); _memo.ArgResults.Push(new _Calc_Item(_arg_index, _memo.InputEnumerable)); }

            // BIND type
            type = _memo.ArgResults.Peek();

        label1: // AND
            var _r1_2 = _memo.ArgResults.Pop();
            var _r1_1 = _memo.ArgResults.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.ArgResults.Push(new _Calc_Item(_start_i1, _arg_index, _r1_1.Inputs.Concat(_r1_2.Inputs), _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), false));
            }
            else
            {
                _memo.ArgResults.Push(null);
                _arg_index = _start_i1;
            }

            if (_memo.ArgResults.Pop() == null)
            {
                _memo.Results.Push(null);
                goto label0;
            }

            // AND 14
            int _start_i14 = _index;

            // AND 15
            int _start_i15 = _index;

            // CALLORVAR first
            _Calc_Item _r17;

            if (first.Production != null)
            {
                var _p17 = (System.Action<_Calc_Memo, int, IEnumerable<_Calc_Item>>)(object)first.Production; // what type safety?
                _r17 = _MemoCall(_memo, first.Production.Method.Name, _index, _p17, null);
            }
            else
            {
                _r17 = _ParseLiteralObj(_memo, ref _index, first.Inputs);
            }

            if (_r17 != null) _index = _r17.NextIndex;

            // BIND a
            a = _memo.Results.Peek();

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label15; }

            // CALL KW
            var _start_i18 = _index;
            _Calc_Item _r18;

            _r18 = _MemoCall(_memo, "KW", _index, KW, new _Calc_Item[] { op });

            if (_r18 != null) _index = _r18.NextIndex;

        label15: // AND
            var _r15_2 = _memo.Results.Pop();
            var _r15_1 = _memo.Results.Pop();

            if (_r15_1 != null && _r15_2 != null)
            {
                _memo.Results.Push( new _Calc_Item(_start_i15, _index, _memo.InputEnumerable, _r15_1.Results.Concat(_r15_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i15;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label14; }

            // CALL second
            var _start_i20 = _index;
            _Calc_Item _r20;

            _r20 = _MemoCall(_memo, second.ProductionName, _index, second.Production, new _Calc_Item[] { type });

            if (_r20 != null) _index = _r20.NextIndex;

            // BIND b
            b = _memo.Results.Peek();

        label14: // AND
            var _r14_2 = _memo.Results.Pop();
            var _r14_1 = _memo.Results.Pop();

            if (_r14_1 != null && _r14_2 != null)
            {
                _memo.Results.Push( new _Calc_Item(_start_i14, _index, _memo.InputEnumerable, _r14_1.Results.Concat(_r14_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i14;
            }

            // ACT
            var _r13 = _memo.Results.Peek();
            if (_r13 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _Calc_Item(_r13.StartIndex, _r13.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return new List<int> { a, b }; }, _r13), true) );
            }

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void Number(_Calc_Memo _memo, int _index, _Calc_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            _Calc_Item type = null;
            _Calc_Item n = null;

            // ARGS 0
            _arg_index = 0;
            _arg_input_index = 0;

            // ANY
            _ParseAnyArgs(_memo, ref _arg_index, ref _arg_input_index, _args);

            // BIND type
            type = _memo.ArgResults.Peek();

            if (_memo.ArgResults.Pop() == null)
            {
                _memo.Results.Push(null);
                goto label0;
            }

            // AND 4
            int _start_i4 = _index;

            // CALL Digits
            var _start_i6 = _index;
            _Calc_Item _r6;

            _r6 = _MemoCall(_memo, "Digits", _index, Digits, new _Calc_Item[] { type });

            if (_r6 != null) _index = _r6.NextIndex;

            // BIND n
            n = _memo.Results.Peek();

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label4; }

            // STAR 7
            int _start_i7 = _index;
            var _res7 = Enumerable.Empty<int>();
        label7:

            // CALLORVAR WS
            _Calc_Item _r8;

            _r8 = _MemoCall(_memo, "WS", _index, WS, null);

            if (_r8 != null) _index = _r8.NextIndex;

            // STAR 7
            var _r7 = _memo.Results.Pop();
            if (_r7 != null)
            {
                _res7 = _res7.Concat(_r7.Results);
                goto label7;
            }
            else
            {
                _memo.Results.Push(new _Calc_Item(_start_i7, _index, _memo.InputEnumerable, _res7.Where(_NON_NULL), true));
            }

        label4: // AND
            var _r4_2 = _memo.Results.Pop();
            var _r4_1 = _memo.Results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _memo.Results.Push( new _Calc_Item(_start_i4, _index, _memo.InputEnumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
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
                _memo.Results.Push( new _Calc_Item(_r3.StartIndex, _r3.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return n; }, _r3), true) );
            }

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void Digits(_Calc_Memo _memo, int _index, _Calc_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            _Calc_Item type = null;
            _Calc_Item a = null;
            _Calc_Item b = null;

            // OR 0
            int _start_i0 = _index;

            // ARGS 1
            _arg_index = 0;
            _arg_input_index = 0;

            // ANY
            _ParseAnyArgs(_memo, ref _arg_index, ref _arg_input_index, _args);

            // BIND type
            type = _memo.ArgResults.Peek();

            if (_memo.ArgResults.Pop() == null)
            {
                _memo.Results.Push(null);
                goto label1;
            }

            // AND 5
            int _start_i5 = _index;

            // CALL Digits
            var _start_i7 = _index;
            _Calc_Item _r7;

            _r7 = _MemoCall(_memo, "Digits", _index, Digits, new _Calc_Item[] { type });

            if (_r7 != null) _index = _r7.NextIndex;

            // BIND a
            a = _memo.Results.Peek();

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label5; }

            // CALLORVAR type
            _Calc_Item _r9;

            if (type.Production != null)
            {
                var _p9 = (System.Action<_Calc_Memo, int, IEnumerable<_Calc_Item>>)(object)type.Production; // what type safety?
                _r9 = _MemoCall(_memo, type.Production.Method.Name, _index, _p9, null);
            }
            else
            {
                _r9 = _ParseLiteralObj(_memo, ref _index, type.Inputs);
            }

            if (_r9 != null) _index = _r9.NextIndex;

            // BIND b
            b = _memo.Results.Peek();

        label5: // AND
            var _r5_2 = _memo.Results.Pop();
            var _r5_1 = _memo.Results.Pop();

            if (_r5_1 != null && _r5_2 != null)
            {
                _memo.Results.Push( new _Calc_Item(_start_i5, _index, _memo.InputEnumerable, _r5_1.Results.Concat(_r5_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i5;
            }

            // ACT
            var _r4 = _memo.Results.Peek();
            if (_r4 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _Calc_Item(_r4.StartIndex, _r4.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return a*10 + b; }, _r4), true) );
            }

        label1: // ARGS 1
            _arg_input_index = _arg_index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // ARGS 10
            _arg_index = 0;
            _arg_input_index = 0;

            // ANY
            _ParseAnyArgs(_memo, ref _arg_index, ref _arg_input_index, _args);

            // BIND type
            type = _memo.ArgResults.Peek();

            if (_memo.ArgResults.Pop() == null)
            {
                _memo.Results.Push(null);
                goto label10;
            }

            // CALLORVAR type
            _Calc_Item _r13;

            if (type.Production != null)
            {
                var _p13 = (System.Action<_Calc_Memo, int, IEnumerable<_Calc_Item>>)(object)type.Production; // what type safety?
                _r13 = _MemoCall(_memo, type.Production.Method.Name, _index, _p13, null);
            }
            else
            {
                _r13 = _ParseLiteralObj(_memo, ref _index, type.Inputs);
            }

            if (_r13 != null) _index = _r13.NextIndex;

        label10: // ARGS 10
            _arg_input_index = _arg_index; // no-op for label

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void DecimalDigit(_Calc_Memo _memo, int _index, _Calc_Args _args)
        {

            _Calc_Item c = null;

            // INPUT CLASS
            _ParseInputClass(_memo, ref _index, '\u0030', '\u0031', '\u0032', '\u0033', '\u0034', '\u0035', '\u0036', '\u0037', '\u0038', '\u0039');

            // BIND c
            c = _memo.Results.Peek();

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _Calc_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return (char)c - '0'; }, _r0), true) );
            }

        }


        public void KW(_Calc_Memo _memo, int _index, _Calc_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            _Calc_Item str = null;

            // ARGS 0
            _arg_index = 0;
            _arg_input_index = 0;

            // ANY
            _ParseAnyArgs(_memo, ref _arg_index, ref _arg_input_index, _args);

            // BIND str
            str = _memo.ArgResults.Peek();

            if (_memo.ArgResults.Pop() == null)
            {
                _memo.Results.Push(null);
                goto label0;
            }

            // AND 3
            int _start_i3 = _index;

            // CALLORVAR str
            _Calc_Item _r4;

            if (str.Production != null)
            {
                var _p4 = (System.Action<_Calc_Memo, int, IEnumerable<_Calc_Item>>)(object)str.Production; // what type safety?
                _r4 = _MemoCall(_memo, str.Production.Method.Name, _index, _p4, null);
            }
            else
            {
                _r4 = _ParseLiteralObj(_memo, ref _index, str.Inputs);
            }

            if (_r4 != null) _index = _r4.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label3; }

            // STAR 5
            int _start_i5 = _index;
            var _res5 = Enumerable.Empty<int>();
        label5:

            // CALLORVAR WS
            _Calc_Item _r6;

            _r6 = _MemoCall(_memo, "WS", _index, WS, null);

            if (_r6 != null) _index = _r6.NextIndex;

            // STAR 5
            var _r5 = _memo.Results.Pop();
            if (_r5 != null)
            {
                _res5 = _res5.Concat(_r5.Results);
                goto label5;
            }
            else
            {
                _memo.Results.Push(new _Calc_Item(_start_i5, _index, _memo.InputEnumerable, _res5.Where(_NON_NULL), true));
            }

        label3: // AND
            var _r3_2 = _memo.Results.Pop();
            var _r3_1 = _memo.Results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _memo.Results.Push( new _Calc_Item(_start_i3, _index, _memo.InputEnumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i3;
            }

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void WS(_Calc_Memo _memo, int _index, _Calc_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // OR 1
            int _start_i1 = _index;

            // OR 2
            int _start_i2 = _index;

            // LITERAL ' '
            _ParseLiteralChar(_memo, ref _index, ' ');

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i2; } else goto label2;

            // LITERAL '\n'
            _ParseLiteralChar(_memo, ref _index, '\n');

        label2: // OR
            int _dummy_i2 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i1; } else goto label1;

            // LITERAL '\r'
            _ParseLiteralChar(_memo, ref _index, '\r');

        label1: // OR
            int _dummy_i1 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // LITERAL '\t'
            _ParseLiteralChar(_memo, ref _index, '\t');

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }

    } // class Calc

} // namespace Calc

