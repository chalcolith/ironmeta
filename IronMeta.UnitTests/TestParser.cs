//
// IronMeta TestParser Parser; Generated 12/04/2011 10:36:27 PM UTC
//

using System;
using System.Collections.Generic;
using System.Linq;
using IronMeta.Matcher;

namespace IronMeta.UnitTests
{

    using _TestParser_Inputs = IEnumerable<char>;
    using _TestParser_Results = IEnumerable<int>;
    using _TestParser_Args = IEnumerable<_TestParser_Item>;
    using _TestParser_Rule = System.Action<int, IEnumerable<_TestParser_Item>>;
    using _TestParser_Base = IronMeta.Matcher.Matcher<char, int, _TestParser_Item>;


    public class _TestParser_Item : IronMeta.Matcher.MatchItem<char, int, _TestParser_Item>
    {
        public _TestParser_Item() { }
        public _TestParser_Item(char input) : base(input) { }
        public _TestParser_Item(char input, int result) : base(input, result) { }
        public _TestParser_Item(_TestParser_Inputs inputs) : base(inputs) { }
        public _TestParser_Item(_TestParser_Inputs inputs, _TestParser_Results results) : base(inputs, results) { }
        public _TestParser_Item(int start, int next, _TestParser_Inputs inputs, _TestParser_Results results, bool relative) : base(start, next, inputs, results, relative) { }
        public _TestParser_Item(int start, _TestParser_Inputs inputs) : base(start, start, inputs, Enumerable.Empty<int>(), true) { }
        public _TestParser_Item(_TestParser_Rule production) : base(production) { }

        public static implicit operator List<char>(_TestParser_Item item) { return item != null ? item.Inputs.ToList() : new List<char>(); }
        public static implicit operator char(_TestParser_Item item) { return item != null ? item.Inputs.LastOrDefault() : default(char); }
        public static implicit operator List<int>(_TestParser_Item item) { return item != null ? item.Results.ToList() : new List<int>(); }
        public static implicit operator int(_TestParser_Item item) { return item != null ? item.Results.LastOrDefault() : default(int); }
    }

    public partial class TestParser : IronMeta.Matcher.CharMatcher<int, _TestParser_Item>
    {
        public TestParser()
            : base()
        { }


        public void Literal(int _index, _TestParser_Args _args)
        {

            // LITERAL 'a'
            _ParseLiteralChar(ref _index, 'a');

        }


        public void LiteralString(int _index, _TestParser_Args _args)
        {

            // LITERAL "abc"
            _ParseLiteralString(ref _index, "abc");

        }


        public void Class(int _index, _TestParser_Args _args)
        {

            // INPUT CLASS
            _ParseInputClass(ref _index, 'a', 'b', 'c');

        }


        public void Class2(int _index, _TestParser_Args _args)
        {

            // INPUT CLASS
            _ParseInputClass(ref _index, '\x01', '\x02', '\x03');

        }


        public void AndLiteral(int _index, _TestParser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // LITERAL 'a'
            _ParseLiteralChar(ref _index, 'a');

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label0; }

            // LITERAL 'b'
            _ParseLiteralChar(ref _index, 'b');

        label0: // AND
            var _r0_2 = _results.Pop();
            var _r0_1 = _results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i0, _index, _input_enumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i0;
            }

        }


        public void OrLiteral(int _index, _TestParser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // LITERAL 'a'
            _ParseLiteralChar(ref _index, 'a');

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i0; } else goto label0;

            // LITERAL 'b'
            _ParseLiteralChar(ref _index, 'b');

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void AndString(int _index, _TestParser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // LITERAL "ab"
            _ParseLiteralString(ref _index, "ab");

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label0; }

            // LITERAL "cd"
            _ParseLiteralString(ref _index, "cd");

        label0: // AND
            var _r0_2 = _results.Pop();
            var _r0_1 = _results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i0, _index, _input_enumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i0;
            }

        }


        public void OrString(int _index, _TestParser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // LITERAL "ab"
            _ParseLiteralString(ref _index, "ab");

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i0; } else goto label0;

            // LITERAL "cd"
            _ParseLiteralString(ref _index, "cd");

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void Fail(int _index, _TestParser_Args _args)
        {

            // FAIL
            _results.Push( null );
            _AddError(_index, () => "This is a fail.");

        }


        public void Any(int _index, _TestParser_Args _args)
        {

            // ANY
            _ParseAny(ref _index);

        }


        public void Look(int _index, _TestParser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // AND 1
            int _start_i1 = _index;

            // LITERAL 'a'
            _ParseLiteralChar(ref _index, 'a');

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label1; }

            // LOOK 3
            int _start_i3 = _index;

            // LITERAL 'b'
            _ParseLiteralChar(ref _index, 'b');

            // LOOK 3
            var _r3 = _results.Pop();
            _results.Push( _r3 != null ? new _TestParser_Item(_start_i3, _input_enumerable) : null );
            _index = _start_i3;

        label1: // AND
            var _r1_2 = _results.Pop();
            var _r1_1 = _results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i1, _index, _input_enumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i1;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label0; }

            // LITERAL "bc"
            _ParseLiteralString(ref _index, "bc");

        label0: // AND
            var _r0_2 = _results.Pop();
            var _r0_1 = _results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i0, _index, _input_enumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i0;
            }

        }


        public void Not(int _index, _TestParser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // AND 1
            int _start_i1 = _index;

            // LITERAL 'a'
            _ParseLiteralChar(ref _index, 'a');

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label1; }

            // NOT 3
            int _start_i3 = _index;

            // LITERAL 'b'
            _ParseLiteralChar(ref _index, 'b');

            // NOT 3
            var _r3 = _results.Pop();
            _results.Push( _r3 == null ? new _TestParser_Item(_start_i3, _input_enumerable) : null);
            _index = _start_i3;

        label1: // AND
            var _r1_2 = _results.Pop();
            var _r1_1 = _results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i1, _index, _input_enumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i1;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label0; }

            // LITERAL 'c'
            _ParseLiteralChar(ref _index, 'c');

        label0: // AND
            var _r0_2 = _results.Pop();
            var _r0_1 = _results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i0, _index, _input_enumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i0;
            }

        }


        public void Star1(int _index, _TestParser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // LITERAL 'a'
            _ParseLiteralChar(ref _index, 'a');

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label0; }

            // STAR 2
            int _start_i2 = _index;
            var _res2 = Enumerable.Empty<int>();
        label2:

            // LITERAL 'b'
            _ParseLiteralChar(ref _index, 'b');

            // STAR 2
            var _r2 = _results.Pop();
            if (_r2 != null)
            {
                _res2 = _res2.Concat(_r2.Results);
                goto label2;
            }
            else
            {
                _results.Push(new _TestParser_Item(_start_i2, _index, _input_enumerable, _res2.Where(_NON_NULL), true));
            }

        label0: // AND
            var _r0_2 = _results.Pop();
            var _r0_1 = _results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i0, _index, _input_enumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i0;
            }

        }


        public void Star2(int _index, _TestParser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // AND 1
            int _start_i1 = _index;

            // LITERAL 'a'
            _ParseLiteralChar(ref _index, 'a');

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label1; }

            // STAR 3
            int _start_i3 = _index;
            var _res3 = Enumerable.Empty<int>();
        label3:

            // AND 4
            int _start_i4 = _index;

            // NOT 5
            int _start_i5 = _index;

            // LITERAL 'c'
            _ParseLiteralChar(ref _index, 'c');

            // NOT 5
            var _r5 = _results.Pop();
            _results.Push( _r5 == null ? new _TestParser_Item(_start_i5, _input_enumerable) : null);
            _index = _start_i5;

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label4; }

            // ANY
            _ParseAny(ref _index);

        label4: // AND
            var _r4_2 = _results.Pop();
            var _r4_1 = _results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i4, _index, _input_enumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i4;
            }

            // STAR 3
            var _r3 = _results.Pop();
            if (_r3 != null)
            {
                _res3 = _res3.Concat(_r3.Results);
                goto label3;
            }
            else
            {
                _results.Push(new _TestParser_Item(_start_i3, _index, _input_enumerable, _res3.Where(_NON_NULL), true));
            }

        label1: // AND
            var _r1_2 = _results.Pop();
            var _r1_1 = _results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i1, _index, _input_enumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i1;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label0; }

            // LITERAL 'c'
            _ParseLiteralChar(ref _index, 'c');

        label0: // AND
            var _r0_2 = _results.Pop();
            var _r0_1 = _results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i0, _index, _input_enumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i0;
            }

        }


        public void Plus1(int _index, _TestParser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // LITERAL 'a'
            _ParseLiteralChar(ref _index, 'a');

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label0; }

            // PLUS 2
            int _start_i2 = _index;
            var _res2 = Enumerable.Empty<int>();
        label2:

            // LITERAL 'b'
            _ParseLiteralChar(ref _index, 'b');

            // PLUS 2
            var _r2 = _results.Pop();
            if (_r2 != null)
            {
                _res2 = _res2.Concat(_r2.Results);
                goto label2;
            }
            else
            {
                if (_index > _start_i2)
                    _results.Push(new _TestParser_Item(_start_i2, _index, _input_enumerable, _res2.Where(_NON_NULL), true));
                else
                    _results.Push(null);
            }

        label0: // AND
            var _r0_2 = _results.Pop();
            var _r0_1 = _results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i0, _index, _input_enumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i0;
            }

        }


        public void Plus2(int _index, _TestParser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // AND 1
            int _start_i1 = _index;

            // LITERAL 'a'
            _ParseLiteralChar(ref _index, 'a');

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label1; }

            // PLUS 3
            int _start_i3 = _index;
            var _res3 = Enumerable.Empty<int>();
        label3:

            // AND 4
            int _start_i4 = _index;

            // NOT 5
            int _start_i5 = _index;

            // LITERAL 'c'
            _ParseLiteralChar(ref _index, 'c');

            // NOT 5
            var _r5 = _results.Pop();
            _results.Push( _r5 == null ? new _TestParser_Item(_start_i5, _input_enumerable) : null);
            _index = _start_i5;

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label4; }

            // ANY
            _ParseAny(ref _index);

        label4: // AND
            var _r4_2 = _results.Pop();
            var _r4_1 = _results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i4, _index, _input_enumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i4;
            }

            // PLUS 3
            var _r3 = _results.Pop();
            if (_r3 != null)
            {
                _res3 = _res3.Concat(_r3.Results);
                goto label3;
            }
            else
            {
                if (_index > _start_i3)
                    _results.Push(new _TestParser_Item(_start_i3, _index, _input_enumerable, _res3.Where(_NON_NULL), true));
                else
                    _results.Push(null);
            }

        label1: // AND
            var _r1_2 = _results.Pop();
            var _r1_1 = _results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i1, _index, _input_enumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i1;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label0; }

            // LITERAL 'c'
            _ParseLiteralChar(ref _index, 'c');

        label0: // AND
            var _r0_2 = _results.Pop();
            var _r0_1 = _results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i0, _index, _input_enumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i0;
            }

        }


        public void Ques(int _index, _TestParser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // AND 1
            int _start_i1 = _index;

            // LITERAL 'a'
            _ParseLiteralChar(ref _index, 'a');

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label1; }

            // LITERAL 'b'
            _ParseLiteralChar(ref _index, 'b');

            // QUES
            if (_results.Peek() == null) { _results.Pop(); _results.Push(new _TestParser_Item(_index, _input_enumerable)); }

        label1: // AND
            var _r1_2 = _results.Pop();
            var _r1_1 = _results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i1, _index, _input_enumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i1;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label0; }

            // LITERAL 'c'
            _ParseLiteralChar(ref _index, 'c');

        label0: // AND
            var _r0_2 = _results.Pop();
            var _r0_1 = _results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i0, _index, _input_enumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i0;
            }

        }


        public void Cond(int _index, _TestParser_Args _args)
        {

            _TestParser_Item c = null;

            // AND 0
            int _start_i0 = _index;

            // AND 1
            int _start_i1 = _index;

            // LITERAL 'a'
            _ParseLiteralChar(ref _index, 'a');

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label1; }

            // COND 3
            int _start_i3 = _index;

            // ANY
            _ParseAny(ref _index);

            // BIND c
            c = _results.Peek();

            // COND
            if (!((char)c == 'b')) { _results.Pop(); _results.Push(null); _index = _start_i3; }
        label1: // AND
            var _r1_2 = _results.Pop();
            var _r1_1 = _results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i1, _index, _input_enumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i1;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label0; }

            // LITERAL 'c'
            _ParseLiteralChar(ref _index, 'c');

        label0: // AND
            var _r0_2 = _results.Pop();
            var _r0_1 = _results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i0, _index, _input_enumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i0;
            }

        }


        public void Cond2(int _index, _TestParser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // AND 1
            int _start_i1 = _index;

            // LITERAL 'a'
            _ParseLiteralChar(ref _index, 'a');

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label1; }

            // COND 3
            int _start_i3 = _index;

            // ANY
            _ParseAny(ref _index);

            // COND
            Func<_TestParser_Item, bool> lambda3 = (_IM_Result) => { return (char)_IM_Result == 'b'; };
            if (!lambda3(_results.Peek())) { _results.Pop(); _results.Push(null); _index = _start_i3; }
        label1: // AND
            var _r1_2 = _results.Pop();
            var _r1_1 = _results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i1, _index, _input_enumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i1;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label0; }

            // LITERAL 'c'
            _ParseLiteralChar(ref _index, 'c');

        label0: // AND
            var _r0_2 = _results.Pop();
            var _r0_1 = _results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i0, _index, _input_enumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i0;
            }

        }


        public void Action(int _index, _TestParser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // LITERAL 'a'
            _ParseLiteralChar(ref _index, 'a');

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i0; } else goto label0;

            // LITERAL 'b'
            _ParseLiteralChar(ref _index, 'b');

            // ACT
            var _r2 = _results.Peek();
            if (_r2 != null)
            {
                _results.Pop();
                _results.Push( new _TestParser_Item(_r2.StartIndex, _r2.NextIndex, _input_enumerable, _Thunk(_IM_Result => { return 123; }, _r2), true) );
            }

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void Call1(int _index, _TestParser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // OR 1
            int _start_i1 = _index;

            // CALL AndLiteral
            var _start_i2 = _index;
            _TestParser_Item _r2;
            _r2 = _MemoCall("AndLiteral", _index, AndLiteral, null);

            if (_r2 != null) _index = _r2.NextIndex;

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i1; } else goto label1;

            // LITERAL 'a'
            _ParseLiteralChar(ref _index, 'a');

        label1: // OR
            int _dummy_i1 = _index; // no-op for label

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label0; }

            // LITERAL 'c'
            _ParseLiteralChar(ref _index, 'c');

        label0: // AND
            var _r0_2 = _results.Pop();
            var _r0_1 = _results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i0, _index, _input_enumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i0;
            }

        }


        public void Call2(int _index, _TestParser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // AND 1
            int _start_i1 = _index;

            // LITERAL 'x'
            _ParseLiteralChar(ref _index, 'x');

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label1; }

            // CALLORVAR OrLiteral
            _TestParser_Item _r3;

            _r3 = _MemoCall("OrLiteral", _index, OrLiteral, null);

            if (_r3 != null) _index = _r3.NextIndex;

        label1: // AND
            var _r1_2 = _results.Pop();
            var _r1_1 = _results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i1, _index, _input_enumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i1;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label0; }

            // LITERAL 'y'
            _ParseLiteralChar(ref _index, 'y');

        label0: // AND
            var _r0_2 = _results.Pop();
            var _r0_1 = _results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i0, _index, _input_enumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i0;
            }

        }


        public void SubCall(int _index, _TestParser_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // ARGS 0
            _arg_index = 0;
            _arg_input_index = 0;

            // LITERAL 'a'
            _ParseLiteralArgs(ref _arg_index, ref _arg_input_index, 'a', _args);

            if (_arg_results.Pop() == null)
            {
                _results.Push(null);
                goto label0;
            }

            // AND 2
            int _start_i2 = _index;

            // LITERAL 'x'
            _ParseLiteralChar(ref _index, 'x');

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label2; }

            // LITERAL 'y'
            _ParseLiteralChar(ref _index, 'y');

        label2: // AND
            var _r2_2 = _results.Pop();
            var _r2_1 = _results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i2, _index, _input_enumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i2;
            }

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void Call3(int _index, _TestParser_Args _args)
        {

            // CALL SubCall
            var _start_i0 = _index;
            _TestParser_Item _r0;
            var _arg0_0 = 'a';
            var _arg0_1 = 'b';
            var _arg0_2 = 'c';
            var _arg0_3 = new System.Char();

            _r0 = _MemoCall("SubCall", _index, SubCall, new _TestParser_Item[] { new _TestParser_Item(_arg0_0), new _TestParser_Item(_arg0_1), new _TestParser_Item(_arg0_2), new _TestParser_Item(_arg0_3) });

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void Call4(int _index, _TestParser_Args _args)
        {

            // CALLORVAR SubCall
            _TestParser_Item _r0;

            _r0 = _MemoCall("SubCall", _index, SubCall, null);

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void SubCall2(int _index, _TestParser_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // ARGS 0
            _arg_index = 0;
            _arg_input_index = 0;

            // AND 1
            int _start_i1 = _arg_index;

            // LITERAL 'a'
            _ParseLiteralArgs(ref _arg_index, ref _arg_input_index, 'a', _args);

            // AND shortcut
            if (_arg_results.Peek() == null) { _arg_results.Push(null); goto label1; }

            // LITERAL 'b'
            _ParseLiteralArgs(ref _arg_index, ref _arg_input_index, 'b', _args);

        label1: // AND
            var _r1_2 = _arg_results.Pop();
            var _r1_1 = _arg_results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _arg_results.Push(new _TestParser_Item(_start_i1, _arg_index, _r1_1.Inputs.Concat(_r1_2.Inputs), _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), false));
            }
            else
            {
                _arg_results.Push(null);
                _arg_index = _start_i1;
            }

            if (_arg_results.Pop() == null)
            {
                _results.Push(null);
                goto label0;
            }

            // AND 4
            int _start_i4 = _index;

            // LITERAL 'x'
            _ParseLiteralChar(ref _index, 'x');

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label4; }

            // LITERAL 'y'
            _ParseLiteralChar(ref _index, 'y');

        label4: // AND
            var _r4_2 = _results.Pop();
            var _r4_1 = _results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i4, _index, _input_enumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i4;
            }

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void Call5(int _index, _TestParser_Args _args)
        {

            // CALL SubCall
            var _start_i0 = _index;
            _TestParser_Item _r0;
            var _arg0_0 = 'a';
            var _arg0_1 = 'b';

            _r0 = _MemoCall("SubCall", _index, SubCall, new _TestParser_Item[] { new _TestParser_Item(_arg0_0), new _TestParser_Item(_arg0_1) });

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void Call6(int _index, _TestParser_Args _args)
        {

            // CALL SubCall
            var _start_i0 = _index;
            _TestParser_Item _r0;
            var _arg0_0 = 'z';
            var _arg0_1 = 'w';

            _r0 = _MemoCall("SubCall", _index, SubCall, new _TestParser_Item[] { new _TestParser_Item(_arg0_0), new _TestParser_Item(_arg0_1) });

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void Call7(int _index, _TestParser_Args _args)
        {

            // CALL SubCall
            var _start_i0 = _index;
            _TestParser_Item _r0;
            var _arg0_0 = "ab";

            _r0 = _MemoCall("SubCall", _index, SubCall, new _TestParser_Item[] { new _TestParser_Item(_arg0_0) });

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void SubCallFail(int _index, _TestParser_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // ARGS 0
            _arg_index = 0;
            _arg_input_index = 0;

            // FAIL
            _arg_results.Push(null);

            if (_arg_results.Pop() == null)
            {
                _results.Push(null);
                goto label0;
            }

            // LITERAL 'a'
            _ParseLiteralChar(ref _index, 'a');

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void CallFail(int _index, _TestParser_Args _args)
        {

            // CALLORVAR SubCallFail
            _TestParser_Item _r0;

            _r0 = _MemoCall("SubCallFail", _index, SubCallFail, null);

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void SubCallClass(int _index, _TestParser_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // ARGS 0
            _arg_index = 0;
            _arg_input_index = 0;

            // INPUT CLASS
            _ParseInputClassArgs(ref _arg_index, ref _arg_input_index, _args, 'a', 'b');

            if (_arg_results.Pop() == null)
            {
                _results.Push(null);
                goto label0;
            }

            // AND 2
            int _start_i2 = _index;

            // LITERAL 'x'
            _ParseLiteralChar(ref _index, 'x');

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label2; }

            // LITERAL 'y'
            _ParseLiteralChar(ref _index, 'y');

        label2: // AND
            var _r2_2 = _results.Pop();
            var _r2_1 = _results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i2, _index, _input_enumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i2;
            }

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void CallClass(int _index, _TestParser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // LOOK 1
            int _start_i1 = _index;

            // CALL SubCallClass
            var _start_i2 = _index;
            _TestParser_Item _r2;
            var _arg2_0 = 'a';

            _r2 = _MemoCall("SubCallClass", _index, SubCallClass, new _TestParser_Item[] { new _TestParser_Item(_arg2_0) });

            if (_r2 != null) _index = _r2.NextIndex;

            // LOOK 1
            var _r1 = _results.Pop();
            _results.Push( _r1 != null ? new _TestParser_Item(_start_i1, _input_enumerable) : null );
            _index = _start_i1;

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label0; }

            // CALL SubCallClass
            var _start_i3 = _index;
            _TestParser_Item _r3;
            var _arg3_0 = 'b';

            _r3 = _MemoCall("SubCallClass", _index, SubCallClass, new _TestParser_Item[] { new _TestParser_Item(_arg3_0) });

            if (_r3 != null) _index = _r3.NextIndex;

        label0: // AND
            var _r0_2 = _results.Pop();
            var _r0_1 = _results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i0, _index, _input_enumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i0;
            }

        }


        public void SubCallAny(int _index, _TestParser_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // ARGS 0
            _arg_index = 0;
            _arg_input_index = 0;

            // ANY
            _ParseAnyArgs(ref _arg_index, ref _arg_input_index, _args);

            if (_arg_results.Pop() == null)
            {
                _results.Push(null);
                goto label0;
            }

            // AND 2
            int _start_i2 = _index;

            // LITERAL 'x'
            _ParseLiteralChar(ref _index, 'x');

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label2; }

            // LITERAL 'y'
            _ParseLiteralChar(ref _index, 'y');

        label2: // AND
            var _r2_2 = _results.Pop();
            var _r2_1 = _results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i2, _index, _input_enumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i2;
            }

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void CallAny(int _index, _TestParser_Args _args)
        {

            // CALL SubCallAny
            var _start_i0 = _index;
            _TestParser_Item _r0;
            var _arg0_0 = 'a';

            _r0 = _MemoCall("SubCallAny", _index, SubCallAny, new _TestParser_Item[] { new _TestParser_Item(_arg0_0) });

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void CallAny2(int _index, _TestParser_Args _args)
        {

            // CALLORVAR SubCallAny
            _TestParser_Item _r0;

            _r0 = _MemoCall("SubCallAny", _index, SubCallAny, null);

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void SubCallLook(int _index, _TestParser_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // ARGS 0
            _arg_index = 0;
            _arg_input_index = 0;

            // AND 1
            int _start_i1 = _arg_index;

            // LOOK 2
            int _start_i2 = _arg_index;

            // LITERAL 'a'
            _ParseLiteralArgs(ref _arg_index, ref _arg_input_index, 'a', _args);

            // LOOK 2
            var _r2 = _arg_results.Pop();
            _arg_results.Push(_r2);
            _arg_index = _start_i2;

            // AND shortcut
            if (_arg_results.Peek() == null) { _arg_results.Push(null); goto label1; }

            // ANY
            _ParseAnyArgs(ref _arg_index, ref _arg_input_index, _args);

        label1: // AND
            var _r1_2 = _arg_results.Pop();
            var _r1_1 = _arg_results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _arg_results.Push(new _TestParser_Item(_start_i1, _arg_index, _r1_1.Inputs.Concat(_r1_2.Inputs), _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), false));
            }
            else
            {
                _arg_results.Push(null);
                _arg_index = _start_i1;
            }

            if (_arg_results.Pop() == null)
            {
                _results.Push(null);
                goto label0;
            }

            // AND 5
            int _start_i5 = _index;

            // LITERAL 'x'
            _ParseLiteralChar(ref _index, 'x');

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label5; }

            // LITERAL 'y'
            _ParseLiteralChar(ref _index, 'y');

        label5: // AND
            var _r5_2 = _results.Pop();
            var _r5_1 = _results.Pop();

            if (_r5_1 != null && _r5_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i5, _index, _input_enumerable, _r5_1.Results.Concat(_r5_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i5;
            }

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void CallLook(int _index, _TestParser_Args _args)
        {

            // CALL SubCallLook
            var _start_i0 = _index;
            _TestParser_Item _r0;
            var _arg0_0 = 'a';

            _r0 = _MemoCall("SubCallLook", _index, SubCallLook, new _TestParser_Item[] { new _TestParser_Item(_arg0_0) });

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void SubCallNot(int _index, _TestParser_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // ARGS 0
            _arg_index = 0;
            _arg_input_index = 0;

            // AND 1
            int _start_i1 = _arg_index;

            // NOT 2
            int _start_i2 = _arg_index;

            // LITERAL 'a'
            _ParseLiteralArgs(ref _arg_index, ref _arg_input_index, 'a', _args);

            // NOT 2
            var _r2 = _arg_results.Pop();
            _arg_results.Push(_r2 == null ? new _TestParser_Item(_arg_index, _arg_index, _input_enumerable, Enumerable.Empty<int>(), false) : null);
            _arg_index = _start_i2;

            // AND shortcut
            if (_arg_results.Peek() == null) { _arg_results.Push(null); goto label1; }

            // LITERAL 'b'
            _ParseLiteralArgs(ref _arg_index, ref _arg_input_index, 'b', _args);

        label1: // AND
            var _r1_2 = _arg_results.Pop();
            var _r1_1 = _arg_results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _arg_results.Push(new _TestParser_Item(_start_i1, _arg_index, _r1_1.Inputs.Concat(_r1_2.Inputs), _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), false));
            }
            else
            {
                _arg_results.Push(null);
                _arg_index = _start_i1;
            }

            if (_arg_results.Pop() == null)
            {
                _results.Push(null);
                goto label0;
            }

            // AND 5
            int _start_i5 = _index;

            // LITERAL 'x'
            _ParseLiteralChar(ref _index, 'x');

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label5; }

            // LITERAL 'y'
            _ParseLiteralChar(ref _index, 'y');

        label5: // AND
            var _r5_2 = _results.Pop();
            var _r5_1 = _results.Pop();

            if (_r5_1 != null && _r5_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i5, _index, _input_enumerable, _r5_1.Results.Concat(_r5_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i5;
            }

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void CallNot(int _index, _TestParser_Args _args)
        {

            // CALL SubCallNot
            var _start_i0 = _index;
            _TestParser_Item _r0;
            var _arg0_0 = 'b';

            _r0 = _MemoCall("SubCallNot", _index, SubCallNot, new _TestParser_Item[] { new _TestParser_Item(_arg0_0) });

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void CallNot2(int _index, _TestParser_Args _args)
        {

            // CALL SubCallNot
            var _start_i0 = _index;
            _TestParser_Item _r0;
            var _arg0_0 = 'a';

            _r0 = _MemoCall("SubCallNot", _index, SubCallNot, new _TestParser_Item[] { new _TestParser_Item(_arg0_0) });

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void SubCallOr(int _index, _TestParser_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // ARGS 0
            _arg_index = 0;
            _arg_input_index = 0;

            // OR 1
            int _start_i1 = _arg_index;

            // LITERAL 'a'
            _ParseLiteralArgs(ref _arg_index, ref _arg_input_index, 'a', _args);

            // OR shortcut
            if (_arg_results.Peek() == null) { _arg_results.Pop(); _arg_index = _start_i1; } else goto label1;

            // LITERAL 'b'
            _ParseLiteralArgs(ref _arg_index, ref _arg_input_index, 'b', _args);

        label1: // OR
            int _dummy_i1 = _index; // no-op for label

            if (_arg_results.Pop() == null)
            {
                _results.Push(null);
                goto label0;
            }

            // AND 4
            int _start_i4 = _index;

            // LITERAL 'x'
            _ParseLiteralChar(ref _index, 'x');

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label4; }

            // LITERAL 'y'
            _ParseLiteralChar(ref _index, 'y');

        label4: // AND
            var _r4_2 = _results.Pop();
            var _r4_1 = _results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i4, _index, _input_enumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i4;
            }

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void CallOr(int _index, _TestParser_Args _args)
        {

            // CALL SubCallOr
            var _start_i0 = _index;
            _TestParser_Item _r0;
            var _arg0_0 = 'a';

            _r0 = _MemoCall("SubCallOr", _index, SubCallOr, new _TestParser_Item[] { new _TestParser_Item(_arg0_0) });

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void CallOr2(int _index, _TestParser_Args _args)
        {

            // CALL SubCallOr
            var _start_i0 = _index;
            _TestParser_Item _r0;
            var _arg0_0 = 'b';

            _r0 = _MemoCall("SubCallOr", _index, SubCallOr, new _TestParser_Item[] { new _TestParser_Item(_arg0_0) });

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void CallOr3(int _index, _TestParser_Args _args)
        {

            // CALL SubCallOr
            var _start_i0 = _index;
            _TestParser_Item _r0;
            var _arg0_0 = 'c';

            _r0 = _MemoCall("SubCallOr", _index, SubCallOr, new _TestParser_Item[] { new _TestParser_Item(_arg0_0) });

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void SubCallAnd(int _index, _TestParser_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // ARGS 0
            _arg_index = 0;
            _arg_input_index = 0;

            // AND 1
            int _start_i1 = _arg_index;

            // LITERAL 'a'
            _ParseLiteralArgs(ref _arg_index, ref _arg_input_index, 'a', _args);

            // AND shortcut
            if (_arg_results.Peek() == null) { _arg_results.Push(null); goto label1; }

            // LITERAL 'b'
            _ParseLiteralArgs(ref _arg_index, ref _arg_input_index, 'b', _args);

        label1: // AND
            var _r1_2 = _arg_results.Pop();
            var _r1_1 = _arg_results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _arg_results.Push(new _TestParser_Item(_start_i1, _arg_index, _r1_1.Inputs.Concat(_r1_2.Inputs), _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), false));
            }
            else
            {
                _arg_results.Push(null);
                _arg_index = _start_i1;
            }

            if (_arg_results.Pop() == null)
            {
                _results.Push(null);
                goto label0;
            }

            // AND 4
            int _start_i4 = _index;

            // LITERAL 'x'
            _ParseLiteralChar(ref _index, 'x');

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label4; }

            // LITERAL 'y'
            _ParseLiteralChar(ref _index, 'y');

        label4: // AND
            var _r4_2 = _results.Pop();
            var _r4_1 = _results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i4, _index, _input_enumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i4;
            }

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void CallAnd(int _index, _TestParser_Args _args)
        {

            // CALL SubCallAnd
            var _start_i0 = _index;
            _TestParser_Item _r0;
            var _arg0_0 = 'a';
            var _arg0_1 = 'b';

            _r0 = _MemoCall("SubCallAnd", _index, SubCallAnd, new _TestParser_Item[] { new _TestParser_Item(_arg0_0), new _TestParser_Item(_arg0_1) });

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void CallAnd2(int _index, _TestParser_Args _args)
        {

            // CALL SubCallAnd
            var _start_i0 = _index;
            _TestParser_Item _r0;
            var _arg0_0 = 'w';
            var _arg0_1 = 'z';

            _r0 = _MemoCall("SubCallAnd", _index, SubCallAnd, new _TestParser_Item[] { new _TestParser_Item(_arg0_0), new _TestParser_Item(_arg0_1) });

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void SubCallStar(int _index, _TestParser_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // ARGS 0
            _arg_index = 0;
            _arg_input_index = 0;

            // STAR 1
            int _start_i1 = _arg_index;
            var _inp1 = Enumerable.Empty<char>();
            var _res1 = Enumerable.Empty<int>();
        label1:

            // LITERAL 'a'
            _ParseLiteralArgs(ref _arg_index, ref _arg_input_index, 'a', _args);

            // STAR 1
            var _r1 = _arg_results.Pop();
            if (_r1 != null)
            {
                _inp1 = _inp1.Concat(_r1.Inputs);
                _res1 = _res1.Concat(_r1.Results);
                goto label1;
            }
            else
            {
                _arg_results.Push(new _TestParser_Item(_start_i1, _arg_index, _inp1, _res1.Where(_NON_NULL), false));
            }

            if (_arg_results.Pop() == null)
            {
                _results.Push(null);
                goto label0;
            }

            // AND 3
            int _start_i3 = _index;

            // LITERAL 'x'
            _ParseLiteralChar(ref _index, 'x');

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label3; }

            // LITERAL 'y'
            _ParseLiteralChar(ref _index, 'y');

        label3: // AND
            var _r3_2 = _results.Pop();
            var _r3_1 = _results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i3, _index, _input_enumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i3;
            }

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void CallStar(int _index, _TestParser_Args _args)
        {

            // CALLORVAR SubCallStar
            _TestParser_Item _r0;

            _r0 = _MemoCall("SubCallStar", _index, SubCallStar, null);

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void CallStar2(int _index, _TestParser_Args _args)
        {

            // CALL SubCallStar
            var _start_i0 = _index;
            _TestParser_Item _r0;
            var _arg0_0 = 'a';

            _r0 = _MemoCall("SubCallStar", _index, SubCallStar, new _TestParser_Item[] { new _TestParser_Item(_arg0_0) });

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void SubCallPlus(int _index, _TestParser_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // ARGS 0
            _arg_index = 0;
            _arg_input_index = 0;

            // PLUS 1
            int _start_i1 = _arg_index;
            var _inp1 = Enumerable.Empty<char>();
            var _res1 = Enumerable.Empty<int>();
        label1:

            // LITERAL 'a'
            _ParseLiteralArgs(ref _arg_index, ref _arg_input_index, 'a', _args);

            // PLUS 1
            var _r1 = _arg_results.Pop();
            if (_r1 != null)
            {
                _inp1 = _inp1.Concat(_r1.Inputs);
                _res1 = _res1.Concat(_r1.Results);
                goto label1;
            }
            else
            {
                if (_arg_index > _start_i1)
                    _arg_results.Push(new _TestParser_Item(_start_i1, _arg_index, _inp1, _res1.Where(_NON_NULL), false));
                else
                    _arg_results.Push(null);
            }

            if (_arg_results.Pop() == null)
            {
                _results.Push(null);
                goto label0;
            }

            // AND 3
            int _start_i3 = _index;

            // LITERAL 'x'
            _ParseLiteralChar(ref _index, 'x');

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label3; }

            // LITERAL 'y'
            _ParseLiteralChar(ref _index, 'y');

        label3: // AND
            var _r3_2 = _results.Pop();
            var _r3_1 = _results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i3, _index, _input_enumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i3;
            }

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void CallPlus(int _index, _TestParser_Args _args)
        {

            // CALL SubCallPlus
            var _start_i0 = _index;
            _TestParser_Item _r0;
            var _arg0_0 = 'a';

            _r0 = _MemoCall("SubCallPlus", _index, SubCallPlus, new _TestParser_Item[] { new _TestParser_Item(_arg0_0) });

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void CallPlus2(int _index, _TestParser_Args _args)
        {

            // CALLORVAR SubCallPlus
            _TestParser_Item _r0;

            _r0 = _MemoCall("SubCallPlus", _index, SubCallPlus, null);

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void SubCallQues(int _index, _TestParser_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // ARGS 0
            _arg_index = 0;
            _arg_input_index = 0;

            // LITERAL 'a'
            _ParseLiteralArgs(ref _arg_index, ref _arg_input_index, 'a', _args);

            // QUES
            if (_arg_results.Peek() == null) { _arg_results.Pop(); _arg_results.Push(new _TestParser_Item(_arg_index, _input_enumerable)); }

            if (_arg_results.Pop() == null)
            {
                _results.Push(null);
                goto label0;
            }

            // AND 3
            int _start_i3 = _index;

            // LITERAL 'x'
            _ParseLiteralChar(ref _index, 'x');

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label3; }

            // LITERAL 'y'
            _ParseLiteralChar(ref _index, 'y');

        label3: // AND
            var _r3_2 = _results.Pop();
            var _r3_1 = _results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i3, _index, _input_enumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i3;
            }

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void CallQues(int _index, _TestParser_Args _args)
        {

            // CALL SubCallQues
            var _start_i0 = _index;
            _TestParser_Item _r0;
            var _arg0_0 = 'a';

            _r0 = _MemoCall("SubCallQues", _index, SubCallQues, new _TestParser_Item[] { new _TestParser_Item(_arg0_0) });

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void CallQues2(int _index, _TestParser_Args _args)
        {

            // CALLORVAR SubCallQues
            _TestParser_Item _r0;

            _r0 = _MemoCall("SubCallQues", _index, SubCallQues, null);

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void SubCallCond(int _index, _TestParser_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            _TestParser_Item v = null;

            // ARGS 0
            _arg_index = 0;
            _arg_input_index = 0;

            // COND 1
            int _start_i1 = _arg_index;

            // ANY
            _ParseAnyArgs(ref _arg_index, ref _arg_input_index, _args);

            // BIND v
            v = _arg_results.Peek();

            // COND
            if (!((char)v == 'a')) { _arg_results.Pop(); _arg_results.Push(null); _arg_index = _start_i1; }
            if (_arg_results.Pop() == null)
            {
                _results.Push(null);
                goto label0;
            }

            // AND 4
            int _start_i4 = _index;

            // LITERAL 'x'
            _ParseLiteralChar(ref _index, 'x');

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label4; }

            // LITERAL 'y'
            _ParseLiteralChar(ref _index, 'y');

        label4: // AND
            var _r4_2 = _results.Pop();
            var _r4_1 = _results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i4, _index, _input_enumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i4;
            }

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void CallCond(int _index, _TestParser_Args _args)
        {

            // CALL SubCallCond
            var _start_i0 = _index;
            _TestParser_Item _r0;
            var _arg0_0 = 'a';

            _r0 = _MemoCall("SubCallCond", _index, SubCallCond, new _TestParser_Item[] { new _TestParser_Item(_arg0_0) });

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void CallCond2(int _index, _TestParser_Args _args)
        {

            // CALLORVAR SubCallCond
            _TestParser_Item _r0;

            _r0 = _MemoCall("SubCallCond", _index, SubCallCond, null);

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void VarInput(int _index, _TestParser_Args _args)
        {

            _TestParser_Item a = null;

            // AND 0
            int _start_i0 = _index;

            // LITERAL 'a'
            _ParseLiteralChar(ref _index, 'a');

            // BIND a
            a = _results.Peek();

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label0; }

            // CALLORVAR a
            _TestParser_Item _r3;

            if (a.Production != null)
            {
                var _p3 = (System.Action<int, IEnumerable<_TestParser_Item>>)(object)a.Production; // what type safety?
                _r3 = _MemoCall(a.Production.Method.Name, _index, _p3, null);
            }
            else
            {
                _r3 = _ParseLiteralObj(ref _index, a.Inputs);
            }

            if (_r3 != null) _index = _r3.NextIndex;

        label0: // AND
            var _r0_2 = _results.Pop();
            var _r0_1 = _results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i0, _index, _input_enumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i0;
            }

        }


        public void SubCallAct(int _index, _TestParser_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            _TestParser_Item a = null;

            // ARGS 0
            _arg_index = 0;
            _arg_input_index = 0;

            // LITERAL 'a'
            _ParseLiteralArgs(ref _arg_index, ref _arg_input_index, 'a', _args);

            // ACT
            var _r2 = _arg_results.Peek();
            if (_r2 != null)
            {
                _arg_results.Pop();
                _arg_results.Push( new _TestParser_Item(_r2.StartIndex, _r2.NextIndex, _r2.Inputs, _Thunk(_IM_Result => { return 42; }, _r2), false) );
            }

            // BIND a
            a = _arg_results.Peek();

            if (_arg_results.Pop() == null)
            {
                _results.Push(null);
                goto label0;
            }

            // CALLORVAR a
            _TestParser_Item _r4;

            if (a.Production != null)
            {
                var _p4 = (System.Action<int, IEnumerable<_TestParser_Item>>)(object)a.Production; // what type safety?
                _r4 = _MemoCall(a.Production.Method.Name, _index, _p4, null);
            }
            else
            {
                _r4 = _ParseLiteralObj(ref _index, a.Inputs);
            }

            if (_r4 != null) _index = _r4.NextIndex;

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void CallAct(int _index, _TestParser_Args _args)
        {

            // CALL SubCallAct
            var _start_i0 = _index;
            _TestParser_Item _r0;
            var _arg0_0 = 'a';

            _r0 = _MemoCall("SubCallAct", _index, SubCallAct, new _TestParser_Item[] { new _TestParser_Item(_arg0_0) });

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void CallAct2(int _index, _TestParser_Args _args)
        {

            // CALLORVAR SubCallAct
            _TestParser_Item _r0;

            _r0 = _MemoCall("SubCallAct", _index, SubCallAct, null);

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void SubCallVar(int _index, _TestParser_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            _TestParser_Item a = null;

            // ARGS 0
            _arg_index = 0;
            _arg_input_index = 0;

            // AND 1
            int _start_i1 = _arg_index;

            // LITERAL 'a'
            _ParseLiteralArgs(ref _arg_index, ref _arg_input_index, 'a', _args);

            // BIND a
            a = _arg_results.Peek();

            // AND shortcut
            if (_arg_results.Peek() == null) { _arg_results.Push(null); goto label1; }

            // CALLORVAR a
            var _r4 = _ParseLiteralArgs(ref _arg_index, ref _arg_input_index, a.Inputs, _args);
            if (_r4 != null) _arg_index = _r4.NextIndex;

        label1: // AND
            var _r1_2 = _arg_results.Pop();
            var _r1_1 = _arg_results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _arg_results.Push(new _TestParser_Item(_start_i1, _arg_index, _r1_1.Inputs.Concat(_r1_2.Inputs), _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), false));
            }
            else
            {
                _arg_results.Push(null);
                _arg_index = _start_i1;
            }

            if (_arg_results.Pop() == null)
            {
                _results.Push(null);
                goto label0;
            }

            // AND 5
            int _start_i5 = _index;

            // LITERAL 'x'
            _ParseLiteralChar(ref _index, 'x');

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label5; }

            // LITERAL 'y'
            _ParseLiteralChar(ref _index, 'y');

        label5: // AND
            var _r5_2 = _results.Pop();
            var _r5_1 = _results.Pop();

            if (_r5_1 != null && _r5_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i5, _index, _input_enumerable, _r5_1.Results.Concat(_r5_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i5;
            }

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void CallCallVar(int _index, _TestParser_Args _args)
        {

            // CALL SubCallVar
            var _start_i0 = _index;
            _TestParser_Item _r0;
            var _arg0_0 = "aa";

            _r0 = _MemoCall("SubCallVar", _index, SubCallVar, new _TestParser_Item[] { new _TestParser_Item(_arg0_0) });

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void CallCallVar2(int _index, _TestParser_Args _args)
        {

            // CALLORVAR SubCallVar
            _TestParser_Item _r0;

            _r0 = _MemoCall("SubCallVar", _index, SubCallVar, null);

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void SubCallRule(int _index, _TestParser_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            _TestParser_Item a = null;

            // ARGS 0
            _arg_index = 0;
            _arg_input_index = 0;

            // ANY
            _ParseAnyArgs(ref _arg_index, ref _arg_input_index, _args);

            // BIND a
            a = _arg_results.Peek();

            if (_arg_results.Pop() == null)
            {
                _results.Push(null);
                goto label0;
            }

            // AND 3
            int _start_i3 = _index;

            // CALLORVAR a
            _TestParser_Item _r4;

            if (a.Production != null)
            {
                var _p4 = (System.Action<int, IEnumerable<_TestParser_Item>>)(object)a.Production; // what type safety?
                _r4 = _MemoCall(a.Production.Method.Name, _index, _p4, null);
            }
            else
            {
                _r4 = _ParseLiteralObj(ref _index, a.Inputs);
            }

            if (_r4 != null) _index = _r4.NextIndex;

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label3; }

            // CALL a
            var _start_i5 = _index;
            _TestParser_Item _r5;
            _r5 = _MemoCall(a.ProductionName, _index, a.Production, null);

            if (_r5 != null) _index = _r5.NextIndex;

        label3: // AND
            var _r3_2 = _results.Pop();
            var _r3_1 = _results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i3, _index, _input_enumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i3;
            }

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void XOrY(int _index, _TestParser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // LITERAL 'x'
            _ParseLiteralChar(ref _index, 'x');

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i0; } else goto label0;

            // LITERAL 'y'
            _ParseLiteralChar(ref _index, 'y');

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void CallCallRule(int _index, _TestParser_Args _args)
        {

            // CALL SubCallRule
            var _start_i0 = _index;
            _TestParser_Item _r0;

            _r0 = _MemoCall("SubCallRule", _index, SubCallRule, new _TestParser_Item[] { new _TestParser_Item(XOrY) });

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void ChoiceLR(int _index, _TestParser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // OR 1
            int _start_i1 = _index;

            // CALLORVAR ChoiceA
            _TestParser_Item _r2;

            _r2 = _MemoCall("ChoiceA", _index, ChoiceA, null);

            if (_r2 != null) _index = _r2.NextIndex;

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i1; } else goto label1;

            // CALLORVAR ChoiceB
            _TestParser_Item _r3;

            _r3 = _MemoCall("ChoiceB", _index, ChoiceB, null);

            if (_r3 != null) _index = _r3.NextIndex;

        label1: // OR
            int _dummy_i1 = _index; // no-op for label

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i0; } else goto label0;

            // LITERAL 'x'
            _ParseLiteralChar(ref _index, 'x');

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void ChoiceA(int _index, _TestParser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // CALLORVAR ChoiceLR
            _TestParser_Item _r1;

            _r1 = _MemoCall("ChoiceLR", _index, ChoiceLR, null);

            if (_r1 != null) _index = _r1.NextIndex;

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label0; }

            // LITERAL 'y'
            _ParseLiteralChar(ref _index, 'y');

        label0: // AND
            var _r0_2 = _results.Pop();
            var _r0_1 = _results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i0, _index, _input_enumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i0;
            }

        }


        public void ChoiceB(int _index, _TestParser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // CALLORVAR ChoiceLR
            _TestParser_Item _r1;

            _r1 = _MemoCall("ChoiceLR", _index, ChoiceLR, null);

            if (_r1 != null) _index = _r1.NextIndex;

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label0; }

            // LITERAL 'z'
            _ParseLiteralChar(ref _index, 'z');

        label0: // AND
            var _r0_2 = _results.Pop();
            var _r0_1 = _results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i0, _index, _input_enumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i0;
            }

        }


        public void DirectLR(int _index, _TestParser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // AND 1
            int _start_i1 = _index;

            // CALLORVAR DirectLR
            _TestParser_Item _r2;

            _r2 = _MemoCall("DirectLR", _index, DirectLR, null);

            if (_r2 != null) _index = _r2.NextIndex;

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label1; }

            // LITERAL 'y'
            _ParseLiteralChar(ref _index, 'y');

        label1: // AND
            var _r1_2 = _results.Pop();
            var _r1_1 = _results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i1, _index, _input_enumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i1;
            }

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i0; } else goto label0;

            // LITERAL 'x'
            _ParseLiteralChar(ref _index, 'x');

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void IndirectLR_A(int _index, _TestParser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // AND 1
            int _start_i1 = _index;

            // CALLORVAR IndirectLR_B
            _TestParser_Item _r2;

            _r2 = _MemoCall("IndirectLR_B", _index, IndirectLR_B, null);

            if (_r2 != null) _index = _r2.NextIndex;

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label1; }

            // LITERAL 'y'
            _ParseLiteralChar(ref _index, 'y');

        label1: // AND
            var _r1_2 = _results.Pop();
            var _r1_1 = _results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i1, _index, _input_enumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i1;
            }

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i0; } else goto label0;

            // LITERAL 'x'
            _ParseLiteralChar(ref _index, 'x');

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void IndirectLR_B(int _index, _TestParser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // CALLORVAR IndirectLR_A
            _TestParser_Item _r1;

            _r1 = _MemoCall("IndirectLR_A", _index, IndirectLR_A, null);

            if (_r1 != null) _index = _r1.NextIndex;

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label0; }

            // LITERAL 'z'
            _ParseLiteralChar(ref _index, 'z');

        label0: // AND
            var _r0_2 = _results.Pop();
            var _r0_1 = _results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i0, _index, _input_enumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i0;
            }

        }


        public void PathalogicalA(int _index, _TestParser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // AND 1
            int _start_i1 = _index;

            // CALLORVAR PathalogicalA
            _TestParser_Item _r2;

            _r2 = _MemoCall("PathalogicalA", _index, PathalogicalA, null);

            if (_r2 != null) _index = _r2.NextIndex;

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label1; }

            // LITERAL 'a'
            _ParseLiteralChar(ref _index, 'a');

        label1: // AND
            var _r1_2 = _results.Pop();
            var _r1_1 = _results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i1, _index, _input_enumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i1;
            }

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i0; } else goto label0;

            // CALLORVAR PathalogicalB
            _TestParser_Item _r4;

            _r4 = _MemoCall("PathalogicalB", _index, PathalogicalB, null);

            if (_r4 != null) _index = _r4.NextIndex;

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void PathalogicalB(int _index, _TestParser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // OR 1
            int _start_i1 = _index;

            // AND 2
            int _start_i2 = _index;

            // CALLORVAR PathalogicalB
            _TestParser_Item _r3;

            _r3 = _MemoCall("PathalogicalB", _index, PathalogicalB, null);

            if (_r3 != null) _index = _r3.NextIndex;

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label2; }

            // LITERAL 'b'
            _ParseLiteralChar(ref _index, 'b');

        label2: // AND
            var _r2_2 = _results.Pop();
            var _r2_1 = _results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i2, _index, _input_enumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i2;
            }

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i1; } else goto label1;

            // CALLORVAR PathalogicalA
            _TestParser_Item _r5;

            _r5 = _MemoCall("PathalogicalA", _index, PathalogicalA, null);

            if (_r5 != null) _index = _r5.NextIndex;

        label1: // OR
            int _dummy_i1 = _index; // no-op for label

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i0; } else goto label0;

            // CALLORVAR PathalogicalC
            _TestParser_Item _r6;

            _r6 = _MemoCall("PathalogicalC", _index, PathalogicalC, null);

            if (_r6 != null) _index = _r6.NextIndex;

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void PathalogicalC(int _index, _TestParser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // OR 1
            int _start_i1 = _index;

            // AND 2
            int _start_i2 = _index;

            // CALLORVAR PathalogicalC
            _TestParser_Item _r3;

            _r3 = _MemoCall("PathalogicalC", _index, PathalogicalC, null);

            if (_r3 != null) _index = _r3.NextIndex;

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label2; }

            // LITERAL 'c'
            _ParseLiteralChar(ref _index, 'c');

        label2: // AND
            var _r2_2 = _results.Pop();
            var _r2_1 = _results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _results.Push( new _TestParser_Item(_start_i2, _index, _input_enumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i2;
            }

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i1; } else goto label1;

            // CALLORVAR PathalogicalB
            _TestParser_Item _r5;

            _r5 = _MemoCall("PathalogicalB", _index, PathalogicalB, null);

            if (_r5 != null) _index = _r5.NextIndex;

        label1: // OR
            int _dummy_i1 = _index; // no-op for label

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i0; } else goto label0;

            // LITERAL 'd'
            _ParseLiteralChar(ref _index, 'd');

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void TestBuildTasks(int _index, _TestParser_Args _args)
        {

            // LITERAL "testBuildTask9"
            _ParseLiteralString(ref _index, "testBuildTask9");

        }

    } // class TestParser

} // namespace IronMeta.UnitTests

