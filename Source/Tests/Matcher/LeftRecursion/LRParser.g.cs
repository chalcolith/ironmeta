//
// IronMeta LRParser Parser; Generated 2014-06-03 18:12:52Z UTC
//

using System;
using System.Collections.Generic;
using System.Linq;
using IronMeta.Matcher;

#pragma warning disable 0219
#pragma warning disable 1591

namespace IronMeta.Tests.Matcher.LeftRecursion
{

    using _LRParser_Inputs = IEnumerable<char>;
    using _LRParser_Results = IEnumerable<string>;
    using _LRParser_Item = IronMeta.Matcher.MatchItem<char, string>;
    using _LRParser_Args = IEnumerable<IronMeta.Matcher.MatchItem<char, string>>;
    using _LRParser_Memo = Memo<char, string>;
    using _LRParser_Rule = System.Action<Memo<char, string>, int, IEnumerable<IronMeta.Matcher.MatchItem<char, string>>>;
    using _LRParser_Base = IronMeta.Matcher.Matcher<char, string>;

    public partial class LRParser : IronMeta.Matcher.CharMatcher<string>
    {
        public LRParser()
            : base()
        {
            _setTerminals();
        }

        public LRParser(bool handle_left_recursion)
            : base(handle_left_recursion)
        {
            _setTerminals();
        }

        void _setTerminals()
        {
            this.Terminals = new HashSet<string>()
            {
                "Character",
                "Digit",
                "HexDigit",
                "HexEscapeCharacter",
                "HexScalarValue",
                "NonLR",
                "NonLR_A",
                "NonLR_B",
                "NonLR_C",
                "Term",
            };
        }


        public void A(_LRParser_Memo _memo, int _index, _LRParser_Args _args)
        {

            _LRParser_Item a = null;
            _LRParser_Item b = null;

            // OR 0
            int _start_i0 = _index;

            // AND 2
            int _start_i2 = _index;

            // CALLORVAR A
            _LRParser_Item _r4;

            _r4 = _MemoCall(_memo, "A", _index, A, null);

            if (_r4 != null) _index = _r4.NextIndex;

            // BIND a
            a = _memo.Results.Peek();

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label2; }

            // CALLORVAR A
            _LRParser_Item _r6;

            _r6 = _MemoCall(_memo, "A", _index, A, null);

            if (_r6 != null) _index = _r6.NextIndex;

            // BIND b
            b = _memo.Results.Peek();

        label2: // AND
            var _r2_2 = _memo.Results.Pop();
            var _r2_1 = _memo.Results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _memo.Results.Push( new _LRParser_Item(_start_i2, _index, _memo.InputEnumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i2;
            }

            // ACT
            var _r1 = _memo.Results.Peek();
            if (_r1 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _LRParser_Item(_r1.StartIndex, _r1.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return "(" + (string)a + (string)b + ")"; }, _r1), true) );
            }

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // LITERAL 'a'
            _ParseLiteralChar(_memo, ref _index, 'a');

            // ACT
            var _r7 = _memo.Results.Peek();
            if (_r7 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _LRParser_Item(_r7.StartIndex, _r7.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return "a"; }, _r7), true) );
            }

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void Exp(_LRParser_Memo _memo, int _index, _LRParser_Args _args)
        {

            _LRParser_Item e = null;
            _LRParser_Item t = null;

            // OR 0
            int _start_i0 = _index;

            // AND 2
            int _start_i2 = _index;

            // AND 3
            int _start_i3 = _index;

            // CALLORVAR Exp
            _LRParser_Item _r5;

            _r5 = _MemoCall(_memo, "Exp", _index, Exp, null);

            if (_r5 != null) _index = _r5.NextIndex;

            // BIND e
            e = _memo.Results.Peek();

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label3; }

            // LITERAL "+"
            _ParseLiteralString(_memo, ref _index, "+");

        label3: // AND
            var _r3_2 = _memo.Results.Pop();
            var _r3_1 = _memo.Results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _memo.Results.Push( new _LRParser_Item(_start_i3, _index, _memo.InputEnumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i3;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label2; }

            // CALLORVAR Term
            _LRParser_Item _r8;

            _r8 = _MemoCall(_memo, "Term", _index, Term, null);

            if (_r8 != null) _index = _r8.NextIndex;

            // BIND t
            t = _memo.Results.Peek();

        label2: // AND
            var _r2_2 = _memo.Results.Pop();
            var _r2_1 = _memo.Results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _memo.Results.Push( new _LRParser_Item(_start_i2, _index, _memo.InputEnumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i2;
            }

            // ACT
            var _r1 = _memo.Results.Peek();
            if (_r1 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _LRParser_Item(_r1.StartIndex, _r1.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return "(" + (string)e + " + " + (string)t + ")"; }, _r1), true) );
            }

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // CALLORVAR Term
            _LRParser_Item _r9;

            _r9 = _MemoCall(_memo, "Term", _index, Term, null);

            if (_r9 != null) _index = _r9.NextIndex;

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void Term(_LRParser_Memo _memo, int _index, _LRParser_Args _args)
        {

            // LITERAL "1"
            _ParseLiteralString(_memo, ref _index, "1");

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _LRParser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return "1"; }, _r0), true) );
            }

        }


        public void NonLR(_LRParser_Memo _memo, int _index, _LRParser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // CALLORVAR NonLR_A
            _LRParser_Item _r1;

            _r1 = _MemoCall(_memo, "NonLR_A", _index, NonLR_A, null);

            if (_r1 != null) _index = _r1.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // CALLORVAR NonLR_B
            _LRParser_Item _r2;

            _r2 = _MemoCall(_memo, "NonLR_B", _index, NonLR_B, null);

            if (_r2 != null) _index = _r2.NextIndex;

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _LRParser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void NonLR_A(_LRParser_Memo _memo, int _index, _LRParser_Args _args)
        {

            // LITERAL "a"
            _ParseLiteralString(_memo, ref _index, "a");

        }


        public void NonLR_B(_LRParser_Memo _memo, int _index, _LRParser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // LITERAL "b"
            _ParseLiteralString(_memo, ref _index, "b");

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // CALLORVAR NonLR_C
            _LRParser_Item _r2;

            _r2 = _MemoCall(_memo, "NonLR_C", _index, NonLR_C, null);

            if (_r2 != null) _index = _r2.NextIndex;

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void NonLR_C(_LRParser_Memo _memo, int _index, _LRParser_Args _args)
        {

            // LITERAL "c"
            _ParseLiteralString(_memo, ref _index, "c");

        }


        public void AAA(_LRParser_Memo _memo, int _index, _LRParser_Args _args)
        {

            _LRParser_Item x = null;
            _LRParser_Item y = null;
            _LRParser_Item z = null;

            // OR 0
            int _start_i0 = _index;

            // AND 2
            int _start_i2 = _index;

            // STAR 4
            int _start_i4 = _index;
            var _res4 = Enumerable.Empty<string>();
        label4:

            // CALLORVAR AAA
            _LRParser_Item _r5;

            _r5 = _MemoCall(_memo, "AAA", _index, AAA, null);

            if (_r5 != null) _index = _r5.NextIndex;

            // STAR 4
            var _r4 = _memo.Results.Pop();
            if (_r4 != null)
            {
                _res4 = _res4.Concat(_r4.Results);
                goto label4;
            }
            else
            {
                _memo.Results.Push(new _LRParser_Item(_start_i4, _index, _memo.InputEnumerable, _res4.Where(_NON_NULL), true));
            }

            // BIND x
            x = _memo.Results.Peek();

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label2; }

            // LITERAL "a"
            _ParseLiteralString(_memo, ref _index, "a");

            // BIND y
            y = _memo.Results.Peek();

        label2: // AND
            var _r2_2 = _memo.Results.Pop();
            var _r2_1 = _memo.Results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _memo.Results.Push( new _LRParser_Item(_start_i2, _index, _memo.InputEnumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i2;
            }

            // ACT
            var _r1 = _memo.Results.Peek();
            if (_r1 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _LRParser_Item(_r1.StartIndex, _r1.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return "(" + (string)x + "a)"; }, _r1), true) );
            }

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // LITERAL "b"
            _ParseLiteralString(_memo, ref _index, "b");

            // BIND z
            z = _memo.Results.Peek();

            // ACT
            var _r8 = _memo.Results.Peek();
            if (_r8 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _LRParser_Item(_r8.StartIndex, _r8.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return "b"; }, _r8), true) );
            }

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void Character(_LRParser_Memo _memo, int _index, _LRParser_Args _args)
        {

            // CALLORVAR HexEscapeCharacter
            _LRParser_Item _r0;

            _r0 = _MemoCall(_memo, "HexEscapeCharacter", _index, HexEscapeCharacter, null);

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void HexEscapeCharacter(_LRParser_Memo _memo, int _index, _LRParser_Args _args)
        {

            _LRParser_Item hex = null;

            // AND 1
            int _start_i1 = _index;

            // LITERAL "#\\x"
            _ParseLiteralString(_memo, ref _index, "#\\x");

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // CALLORVAR HexScalarValue
            _LRParser_Item _r4;

            _r4 = _MemoCall(_memo, "HexScalarValue", _index, HexScalarValue, null);

            if (_r4 != null) _index = _r4.NextIndex;

            // BIND hex
            hex = _memo.Results.Peek();

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _LRParser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _LRParser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { foreach (var input in hex.Inputs)
            Console.WriteLine("{0}", input);
        return ""; }, _r0), true) );
            }

        }


        public void HexScalarValue(_LRParser_Memo _memo, int _index, _LRParser_Args _args)
        {

            // PLUS 0
            int _start_i0 = _index;
            var _res0 = Enumerable.Empty<string>();
        label0:

            // CALLORVAR HexDigit
            _LRParser_Item _r1;

            _r1 = _MemoCall(_memo, "HexDigit", _index, HexDigit, null);

            if (_r1 != null) _index = _r1.NextIndex;

            // PLUS 0
            var _r0 = _memo.Results.Pop();
            if (_r0 != null)
            {
                _res0 = _res0.Concat(_r0.Results);
                goto label0;
            }
            else
            {
                if (_index > _start_i0)
                    _memo.Results.Push(new _LRParser_Item(_start_i0, _index, _memo.InputEnumerable, _res0.Where(_NON_NULL), true));
                else
                    _memo.Results.Push(null);
            }

        }


        public void HexDigit(_LRParser_Memo _memo, int _index, _LRParser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // OR 1
            int _start_i1 = _index;

            // OR 2
            int _start_i2 = _index;

            // OR 3
            int _start_i3 = _index;

            // OR 4
            int _start_i4 = _index;

            // OR 5
            int _start_i5 = _index;

            // OR 6
            int _start_i6 = _index;

            // OR 7
            int _start_i7 = _index;

            // OR 8
            int _start_i8 = _index;

            // OR 9
            int _start_i9 = _index;

            // OR 10
            int _start_i10 = _index;

            // OR 11
            int _start_i11 = _index;

            // CALLORVAR Digit
            _LRParser_Item _r12;

            _r12 = _MemoCall(_memo, "Digit", _index, Digit, null);

            if (_r12 != null) _index = _r12.NextIndex;

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i11; } else goto label11;

            // LITERAL 'a'
            _ParseLiteralChar(_memo, ref _index, 'a');

        label11: // OR
            int _dummy_i11 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i10; } else goto label10;

            // LITERAL 'A'
            _ParseLiteralChar(_memo, ref _index, 'A');

        label10: // OR
            int _dummy_i10 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i9; } else goto label9;

            // LITERAL 'b'
            _ParseLiteralChar(_memo, ref _index, 'b');

        label9: // OR
            int _dummy_i9 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i8; } else goto label8;

            // LITERAL 'B'
            _ParseLiteralChar(_memo, ref _index, 'B');

        label8: // OR
            int _dummy_i8 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i7; } else goto label7;

            // LITERAL 'c'
            _ParseLiteralChar(_memo, ref _index, 'c');

        label7: // OR
            int _dummy_i7 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i6; } else goto label6;

            // LITERAL 'C'
            _ParseLiteralChar(_memo, ref _index, 'C');

        label6: // OR
            int _dummy_i6 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i5; } else goto label5;

            // LITERAL 'd'
            _ParseLiteralChar(_memo, ref _index, 'd');

        label5: // OR
            int _dummy_i5 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i4; } else goto label4;

            // LITERAL 'D'
            _ParseLiteralChar(_memo, ref _index, 'D');

        label4: // OR
            int _dummy_i4 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i3; } else goto label3;

            // LITERAL 'e'
            _ParseLiteralChar(_memo, ref _index, 'e');

        label3: // OR
            int _dummy_i3 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i2; } else goto label2;

            // LITERAL 'E'
            _ParseLiteralChar(_memo, ref _index, 'E');

        label2: // OR
            int _dummy_i2 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i1; } else goto label1;

            // LITERAL 'f'
            _ParseLiteralChar(_memo, ref _index, 'f');

        label1: // OR
            int _dummy_i1 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // LITERAL 'F'
            _ParseLiteralChar(_memo, ref _index, 'F');

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void Digit(_LRParser_Memo _memo, int _index, _LRParser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // OR 1
            int _start_i1 = _index;

            // OR 2
            int _start_i2 = _index;

            // OR 3
            int _start_i3 = _index;

            // OR 4
            int _start_i4 = _index;

            // OR 5
            int _start_i5 = _index;

            // OR 6
            int _start_i6 = _index;

            // OR 7
            int _start_i7 = _index;

            // OR 8
            int _start_i8 = _index;

            // LITERAL '0'
            _ParseLiteralChar(_memo, ref _index, '0');

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i8; } else goto label8;

            // LITERAL '1'
            _ParseLiteralChar(_memo, ref _index, '1');

        label8: // OR
            int _dummy_i8 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i7; } else goto label7;

            // LITERAL '2'
            _ParseLiteralChar(_memo, ref _index, '2');

        label7: // OR
            int _dummy_i7 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i6; } else goto label6;

            // LITERAL '3'
            _ParseLiteralChar(_memo, ref _index, '3');

        label6: // OR
            int _dummy_i6 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i5; } else goto label5;

            // LITERAL '4'
            _ParseLiteralChar(_memo, ref _index, '4');

        label5: // OR
            int _dummy_i5 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i4; } else goto label4;

            // LITERAL '5'
            _ParseLiteralChar(_memo, ref _index, '5');

        label4: // OR
            int _dummy_i4 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i3; } else goto label3;

            // LITERAL '6'
            _ParseLiteralChar(_memo, ref _index, '6');

        label3: // OR
            int _dummy_i3 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i2; } else goto label2;

            // LITERAL '7'
            _ParseLiteralChar(_memo, ref _index, '7');

        label2: // OR
            int _dummy_i2 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i1; } else goto label1;

            // LITERAL '8'
            _ParseLiteralChar(_memo, ref _index, '8');

        label1: // OR
            int _dummy_i1 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // LITERAL '9'
            _ParseLiteralChar(_memo, ref _index, '9');

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }

    } // class LRParser

} // namespace LeftRecursion

