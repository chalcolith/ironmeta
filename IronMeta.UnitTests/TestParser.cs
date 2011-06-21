//
// IronMeta TestParser Parser; Generated 21/06/2011 3:37:12 AM UTC
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
    using _TestParser_Memo = Memo<char, int, _TestParser_Item>;
    using _TestParser_Rule = System.Action<Memo<char, int, _TestParser_Item>, int, IEnumerable<_TestParser_Item>>;
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

        public void Literal(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // LITERAL 'a'
            _ParseLiteralChar(_memo, ref _index, 'a');

        }


        public void LiteralString(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // LITERAL "abc"
            _ParseLiteralString(_memo, ref _index, "abc");

        }


        public void Class(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // INPUT CLASS
            _ParseInputClass(_memo, ref _index, 'a', 'b', 'c');

        }


        public void Class2(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // INPUT CLASS
            _ParseInputClass(_memo, ref _index, '\u0001', '\u0002', '\u0003');

        }


        public void AndLiteral(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // LITERAL 'a'
            _ParseLiteralChar(_memo, ref _index, 'a');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // LITERAL 'b'
            _ParseLiteralChar(_memo, ref _index, 'b');

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _TestParser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void OrLiteral(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // LITERAL 'a'
            _ParseLiteralChar(_memo, ref _index, 'a');

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // LITERAL 'b'
            _ParseLiteralChar(_memo, ref _index, 'b');

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void AndString(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // LITERAL "ab"
            _ParseLiteralString(_memo, ref _index, "ab");

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // LITERAL "cd"
            _ParseLiteralString(_memo, ref _index, "cd");

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _TestParser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void OrString(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // LITERAL "ab"
            _ParseLiteralString(_memo, ref _index, "ab");

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // LITERAL "cd"
            _ParseLiteralString(_memo, ref _index, "cd");

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void Fail(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // FAIL
            _memo.Results.Push(null);
            _memo.ClearErrors(_index);
            _memo.AddError(_index, () => "This is a fail.");

        }


        public void Any(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // ANY
            _ParseAny(_memo, ref _index);

        }


        public void Look(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // AND 1
            int _start_i1 = _index;

            // LITERAL 'a'
            _ParseLiteralChar(_memo, ref _index, 'a');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // LOOK 3
            int _start_i3 = _index;

            // LITERAL 'b'
            _ParseLiteralChar(_memo, ref _index, 'b');

            // LOOK 3
            var _r3 = _memo.Results.Pop();
            _memo.Results.Push( _r3 != null ? new _TestParser_Item(_start_i3, _memo.InputEnumerable) : null );
            _index = _start_i3;

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _TestParser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i1;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // LITERAL "bc"
            _ParseLiteralString(_memo, ref _index, "bc");

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _TestParser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void Not(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // AND 1
            int _start_i1 = _index;

            // LITERAL 'a'
            _ParseLiteralChar(_memo, ref _index, 'a');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // NOT 3
            int _start_i3 = _index;

            // LITERAL 'b'
            _ParseLiteralChar(_memo, ref _index, 'b');

            // NOT 3
            var _r3 = _memo.Results.Pop();
            _memo.Results.Push( _r3 == null ? new _TestParser_Item(_start_i3, _memo.InputEnumerable) : null);
            _index = _start_i3;

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _TestParser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
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
                _memo.Results.Push( new _TestParser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void Star1(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // LITERAL 'a'
            _ParseLiteralChar(_memo, ref _index, 'a');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // STAR 2
            int _start_i2 = _index;
            var _res2 = Enumerable.Empty<int>();
        label2:

            // LITERAL 'b'
            _ParseLiteralChar(_memo, ref _index, 'b');

            // STAR 2
            var _r2 = _memo.Results.Pop();
            if (_r2 != null)
            {
                _res2 = _res2.Concat(_r2.Results);
                goto label2;
            }
            else
            {
                _memo.Results.Push(new _TestParser_Item(_start_i2, _index, _memo.InputEnumerable, _res2.Where(_NON_NULL), true));
            }

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _TestParser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void Star2(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // AND 1
            int _start_i1 = _index;

            // LITERAL 'a'
            _ParseLiteralChar(_memo, ref _index, 'a');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // STAR 3
            int _start_i3 = _index;
            var _res3 = Enumerable.Empty<int>();
        label3:

            // AND 4
            int _start_i4 = _index;

            // NOT 5
            int _start_i5 = _index;

            // LITERAL 'c'
            _ParseLiteralChar(_memo, ref _index, 'c');

            // NOT 5
            var _r5 = _memo.Results.Pop();
            _memo.Results.Push( _r5 == null ? new _TestParser_Item(_start_i5, _memo.InputEnumerable) : null);
            _index = _start_i5;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label4; }

            // ANY
            _ParseAny(_memo, ref _index);

        label4: // AND
            var _r4_2 = _memo.Results.Pop();
            var _r4_1 = _memo.Results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _memo.Results.Push( new _TestParser_Item(_start_i4, _index, _memo.InputEnumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i4;
            }

            // STAR 3
            var _r3 = _memo.Results.Pop();
            if (_r3 != null)
            {
                _res3 = _res3.Concat(_r3.Results);
                goto label3;
            }
            else
            {
                _memo.Results.Push(new _TestParser_Item(_start_i3, _index, _memo.InputEnumerable, _res3.Where(_NON_NULL), true));
            }

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _TestParser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
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
                _memo.Results.Push( new _TestParser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void Plus1(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // LITERAL 'a'
            _ParseLiteralChar(_memo, ref _index, 'a');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // PLUS 2
            int _start_i2 = _index;
            var _res2 = Enumerable.Empty<int>();
        label2:

            // LITERAL 'b'
            _ParseLiteralChar(_memo, ref _index, 'b');

            // PLUS 2
            var _r2 = _memo.Results.Pop();
            if (_r2 != null)
            {
                _res2 = _res2.Concat(_r2.Results);
                goto label2;
            }
            else
            {
                if (_index > _start_i2)
                    _memo.Results.Push(new _TestParser_Item(_start_i2, _index, _memo.InputEnumerable, _res2.Where(_NON_NULL), true));
                else
                    _memo.Results.Push(null);
            }

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _TestParser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void Plus2(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // AND 1
            int _start_i1 = _index;

            // LITERAL 'a'
            _ParseLiteralChar(_memo, ref _index, 'a');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // PLUS 3
            int _start_i3 = _index;
            var _res3 = Enumerable.Empty<int>();
        label3:

            // AND 4
            int _start_i4 = _index;

            // NOT 5
            int _start_i5 = _index;

            // LITERAL 'c'
            _ParseLiteralChar(_memo, ref _index, 'c');

            // NOT 5
            var _r5 = _memo.Results.Pop();
            _memo.Results.Push( _r5 == null ? new _TestParser_Item(_start_i5, _memo.InputEnumerable) : null);
            _index = _start_i5;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label4; }

            // ANY
            _ParseAny(_memo, ref _index);

        label4: // AND
            var _r4_2 = _memo.Results.Pop();
            var _r4_1 = _memo.Results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _memo.Results.Push( new _TestParser_Item(_start_i4, _index, _memo.InputEnumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i4;
            }

            // PLUS 3
            var _r3 = _memo.Results.Pop();
            if (_r3 != null)
            {
                _res3 = _res3.Concat(_r3.Results);
                goto label3;
            }
            else
            {
                if (_index > _start_i3)
                    _memo.Results.Push(new _TestParser_Item(_start_i3, _index, _memo.InputEnumerable, _res3.Where(_NON_NULL), true));
                else
                    _memo.Results.Push(null);
            }

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _TestParser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
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
                _memo.Results.Push( new _TestParser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void Ques(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
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

            // QUES
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _memo.Results.Push(new _TestParser_Item(_index, _memo.InputEnumerable)); }

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _TestParser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
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
                _memo.Results.Push( new _TestParser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void Cond(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            _TestParser_Item c = null;

            // AND 0
            int _start_i0 = _index;

            // AND 1
            int _start_i1 = _index;

            // LITERAL 'a'
            _ParseLiteralChar(_memo, ref _index, 'a');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // COND 3
            int _start_i3 = _index;

            // ANY
            _ParseAny(_memo, ref _index);

            // BIND c
            c = _memo.Results.Peek();

            // COND
            if (_memo.Results.Peek() == null || !((char)c == 'b')) { _memo.Results.Pop(); _memo.Results.Push(null); _index = _start_i3; }

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _TestParser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
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
                _memo.Results.Push( new _TestParser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void Cond2(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // AND 1
            int _start_i1 = _index;

            // LITERAL 'a'
            _ParseLiteralChar(_memo, ref _index, 'a');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // COND 3
            int _start_i3 = _index;

            // ANY
            _ParseAny(_memo, ref _index);

            // COND
            Func<_TestParser_Item, bool> lambda3 = (_IM_Result) => { return (char)_IM_Result == 'b'; };
            if (_memo.Results.Peek() == null || !lambda3(_memo.Results.Peek())) { _memo.Results.Pop(); _memo.Results.Push(null); _index = _start_i3; }

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _TestParser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
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
                _memo.Results.Push( new _TestParser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void Action(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // LITERAL 'a'
            _ParseLiteralChar(_memo, ref _index, 'a');

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // LITERAL 'b'
            _ParseLiteralChar(_memo, ref _index, 'b');

            // ACT
            var _r2 = _memo.Results.Peek();
            if (_r2 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _TestParser_Item(_r2.StartIndex, _r2.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return 123; }, _r2), true) );
            }

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void Call1(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // OR 1
            int _start_i1 = _index;

            // CALL AndLiteral
            var _start_i2 = _index;
            _TestParser_Item _r2;
            _r2 = _MemoCall(_memo, "AndLiteral", _index, AndLiteral, null);

            if (_r2 != null) _index = _r2.NextIndex;

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i1; } else goto label1;

            // LITERAL 'a'
            _ParseLiteralChar(_memo, ref _index, 'a');

        label1: // OR
            int _dummy_i1 = _index; // no-op for label

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // LITERAL 'c'
            _ParseLiteralChar(_memo, ref _index, 'c');

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _TestParser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void Call2(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // AND 1
            int _start_i1 = _index;

            // LITERAL 'x'
            _ParseLiteralChar(_memo, ref _index, 'x');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // CALLORVAR OrLiteral
            _TestParser_Item _r3;

            _r3 = _MemoCall(_memo, "OrLiteral", _index, OrLiteral, null);

            if (_r3 != null) _index = _r3.NextIndex;

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _TestParser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i1;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // LITERAL 'y'
            _ParseLiteralChar(_memo, ref _index, 'y');

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _TestParser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void SubCall(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // ARGS 0
            _arg_index = 0;
            _arg_input_index = 0;

            // LITERAL 'a'
            _ParseLiteralArgs(_memo, ref _arg_index, ref _arg_input_index, 'a', _args);

            if (_memo.ArgResults.Pop() == null)
            {
                _memo.Results.Push(null);
                goto label0;
            }

            // AND 2
            int _start_i2 = _index;

            // LITERAL 'x'
            _ParseLiteralChar(_memo, ref _index, 'x');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label2; }

            // LITERAL 'y'
            _ParseLiteralChar(_memo, ref _index, 'y');

        label2: // AND
            var _r2_2 = _memo.Results.Pop();
            var _r2_1 = _memo.Results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _memo.Results.Push( new _TestParser_Item(_start_i2, _index, _memo.InputEnumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i2;
            }

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void Call3(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // CALL SubCall
            var _start_i0 = _index;
            _TestParser_Item _r0;
            var _arg0_0 = 'a';
            var _arg0_1 = 'b';
            var _arg0_2 = 'c';
            var _arg0_3 = new System.Char();

            _r0 = _MemoCall(_memo, "SubCall", _index, SubCall, new _TestParser_Item[] { new _TestParser_Item(_arg0_0), new _TestParser_Item(_arg0_1), new _TestParser_Item(_arg0_2), new _TestParser_Item(_arg0_3) });

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void Call4(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // CALLORVAR SubCall
            _TestParser_Item _r0;

            _r0 = _MemoCall(_memo, "SubCall", _index, SubCall, null);

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void SubCall2(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // ARGS 0
            _arg_index = 0;
            _arg_input_index = 0;

            // AND 1
            int _start_i1 = _arg_index;

            // LITERAL 'a'
            _ParseLiteralArgs(_memo, ref _arg_index, ref _arg_input_index, 'a', _args);

            // AND shortcut
            if (_memo.ArgResults.Peek() == null) { _memo.ArgResults.Push(null); goto label1; }

            // LITERAL 'b'
            _ParseLiteralArgs(_memo, ref _arg_index, ref _arg_input_index, 'b', _args);

        label1: // AND
            var _r1_2 = _memo.ArgResults.Pop();
            var _r1_1 = _memo.ArgResults.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.ArgResults.Push(new _TestParser_Item(_start_i1, _arg_index, _r1_1.Inputs.Concat(_r1_2.Inputs), _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), false));
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

            // AND 4
            int _start_i4 = _index;

            // LITERAL 'x'
            _ParseLiteralChar(_memo, ref _index, 'x');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label4; }

            // LITERAL 'y'
            _ParseLiteralChar(_memo, ref _index, 'y');

        label4: // AND
            var _r4_2 = _memo.Results.Pop();
            var _r4_1 = _memo.Results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _memo.Results.Push( new _TestParser_Item(_start_i4, _index, _memo.InputEnumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i4;
            }

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void Call5(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // CALL SubCall
            var _start_i0 = _index;
            _TestParser_Item _r0;
            var _arg0_0 = 'a';
            var _arg0_1 = 'b';

            _r0 = _MemoCall(_memo, "SubCall", _index, SubCall, new _TestParser_Item[] { new _TestParser_Item(_arg0_0), new _TestParser_Item(_arg0_1) });

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void Call6(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // CALL SubCall
            var _start_i0 = _index;
            _TestParser_Item _r0;
            var _arg0_0 = 'z';
            var _arg0_1 = 'w';

            _r0 = _MemoCall(_memo, "SubCall", _index, SubCall, new _TestParser_Item[] { new _TestParser_Item(_arg0_0), new _TestParser_Item(_arg0_1) });

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void Call7(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // CALL SubCall
            var _start_i0 = _index;
            _TestParser_Item _r0;
            var _arg0_0 = "ab";

            _r0 = _MemoCall(_memo, "SubCall", _index, SubCall, new _TestParser_Item[] { new _TestParser_Item(_arg0_0) });

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void SubCallFail(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // ARGS 0
            _arg_index = 0;
            _arg_input_index = 0;

            // FAIL
            _memo.ArgResults.Push(null);

            if (_memo.ArgResults.Pop() == null)
            {
                _memo.Results.Push(null);
                goto label0;
            }

            // LITERAL 'a'
            _ParseLiteralChar(_memo, ref _index, 'a');

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void CallFail(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // CALLORVAR SubCallFail
            _TestParser_Item _r0;

            _r0 = _MemoCall(_memo, "SubCallFail", _index, SubCallFail, null);

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void SubCallClass(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // ARGS 0
            _arg_index = 0;
            _arg_input_index = 0;

            // INPUT CLASS
            _ParseInputClassArgs(_memo, ref _arg_index, ref _arg_input_index, _args, 'a', 'b');

            if (_memo.ArgResults.Pop() == null)
            {
                _memo.Results.Push(null);
                goto label0;
            }

            // AND 2
            int _start_i2 = _index;

            // LITERAL 'x'
            _ParseLiteralChar(_memo, ref _index, 'x');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label2; }

            // LITERAL 'y'
            _ParseLiteralChar(_memo, ref _index, 'y');

        label2: // AND
            var _r2_2 = _memo.Results.Pop();
            var _r2_1 = _memo.Results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _memo.Results.Push( new _TestParser_Item(_start_i2, _index, _memo.InputEnumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i2;
            }

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void CallClass(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // LOOK 1
            int _start_i1 = _index;

            // CALL SubCallClass
            var _start_i2 = _index;
            _TestParser_Item _r2;
            var _arg2_0 = 'a';

            _r2 = _MemoCall(_memo, "SubCallClass", _index, SubCallClass, new _TestParser_Item[] { new _TestParser_Item(_arg2_0) });

            if (_r2 != null) _index = _r2.NextIndex;

            // LOOK 1
            var _r1 = _memo.Results.Pop();
            _memo.Results.Push( _r1 != null ? new _TestParser_Item(_start_i1, _memo.InputEnumerable) : null );
            _index = _start_i1;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // CALL SubCallClass
            var _start_i3 = _index;
            _TestParser_Item _r3;
            var _arg3_0 = 'b';

            _r3 = _MemoCall(_memo, "SubCallClass", _index, SubCallClass, new _TestParser_Item[] { new _TestParser_Item(_arg3_0) });

            if (_r3 != null) _index = _r3.NextIndex;

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _TestParser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void SubCallAny(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // ARGS 0
            _arg_index = 0;
            _arg_input_index = 0;

            // ANY
            _ParseAnyArgs(_memo, ref _arg_index, ref _arg_input_index, _args);

            if (_memo.ArgResults.Pop() == null)
            {
                _memo.Results.Push(null);
                goto label0;
            }

            // AND 2
            int _start_i2 = _index;

            // LITERAL 'x'
            _ParseLiteralChar(_memo, ref _index, 'x');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label2; }

            // LITERAL 'y'
            _ParseLiteralChar(_memo, ref _index, 'y');

        label2: // AND
            var _r2_2 = _memo.Results.Pop();
            var _r2_1 = _memo.Results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _memo.Results.Push( new _TestParser_Item(_start_i2, _index, _memo.InputEnumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i2;
            }

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void CallAny(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // CALL SubCallAny
            var _start_i0 = _index;
            _TestParser_Item _r0;
            var _arg0_0 = 'a';

            _r0 = _MemoCall(_memo, "SubCallAny", _index, SubCallAny, new _TestParser_Item[] { new _TestParser_Item(_arg0_0) });

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void CallAny2(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // CALLORVAR SubCallAny
            _TestParser_Item _r0;

            _r0 = _MemoCall(_memo, "SubCallAny", _index, SubCallAny, null);

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void SubCallLook(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
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
            _ParseLiteralArgs(_memo, ref _arg_index, ref _arg_input_index, 'a', _args);

            // LOOK 2
            var _r2 = _memo.ArgResults.Pop();
            _memo.ArgResults.Push(_r2);
            _arg_index = _start_i2;

            // AND shortcut
            if (_memo.ArgResults.Peek() == null) { _memo.ArgResults.Push(null); goto label1; }

            // ANY
            _ParseAnyArgs(_memo, ref _arg_index, ref _arg_input_index, _args);

        label1: // AND
            var _r1_2 = _memo.ArgResults.Pop();
            var _r1_1 = _memo.ArgResults.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.ArgResults.Push(new _TestParser_Item(_start_i1, _arg_index, _r1_1.Inputs.Concat(_r1_2.Inputs), _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), false));
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

            // AND 5
            int _start_i5 = _index;

            // LITERAL 'x'
            _ParseLiteralChar(_memo, ref _index, 'x');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label5; }

            // LITERAL 'y'
            _ParseLiteralChar(_memo, ref _index, 'y');

        label5: // AND
            var _r5_2 = _memo.Results.Pop();
            var _r5_1 = _memo.Results.Pop();

            if (_r5_1 != null && _r5_2 != null)
            {
                _memo.Results.Push( new _TestParser_Item(_start_i5, _index, _memo.InputEnumerable, _r5_1.Results.Concat(_r5_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i5;
            }

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void CallLook(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // CALL SubCallLook
            var _start_i0 = _index;
            _TestParser_Item _r0;
            var _arg0_0 = 'a';

            _r0 = _MemoCall(_memo, "SubCallLook", _index, SubCallLook, new _TestParser_Item[] { new _TestParser_Item(_arg0_0) });

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void SubCallNot(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
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
            _ParseLiteralArgs(_memo, ref _arg_index, ref _arg_input_index, 'a', _args);

            // NOT 2
            var _r2 = _memo.ArgResults.Pop();
            _memo.ArgResults.Push(_r2 == null ? new _TestParser_Item(_arg_index, _arg_index, _memo.InputEnumerable, Enumerable.Empty<int>(), false) : null);
            _arg_index = _start_i2;

            // AND shortcut
            if (_memo.ArgResults.Peek() == null) { _memo.ArgResults.Push(null); goto label1; }

            // LITERAL 'b'
            _ParseLiteralArgs(_memo, ref _arg_index, ref _arg_input_index, 'b', _args);

        label1: // AND
            var _r1_2 = _memo.ArgResults.Pop();
            var _r1_1 = _memo.ArgResults.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.ArgResults.Push(new _TestParser_Item(_start_i1, _arg_index, _r1_1.Inputs.Concat(_r1_2.Inputs), _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), false));
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

            // AND 5
            int _start_i5 = _index;

            // LITERAL 'x'
            _ParseLiteralChar(_memo, ref _index, 'x');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label5; }

            // LITERAL 'y'
            _ParseLiteralChar(_memo, ref _index, 'y');

        label5: // AND
            var _r5_2 = _memo.Results.Pop();
            var _r5_1 = _memo.Results.Pop();

            if (_r5_1 != null && _r5_2 != null)
            {
                _memo.Results.Push( new _TestParser_Item(_start_i5, _index, _memo.InputEnumerable, _r5_1.Results.Concat(_r5_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i5;
            }

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void CallNot(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // CALL SubCallNot
            var _start_i0 = _index;
            _TestParser_Item _r0;
            var _arg0_0 = 'b';

            _r0 = _MemoCall(_memo, "SubCallNot", _index, SubCallNot, new _TestParser_Item[] { new _TestParser_Item(_arg0_0) });

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void CallNot2(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // CALL SubCallNot
            var _start_i0 = _index;
            _TestParser_Item _r0;
            var _arg0_0 = 'a';

            _r0 = _MemoCall(_memo, "SubCallNot", _index, SubCallNot, new _TestParser_Item[] { new _TestParser_Item(_arg0_0) });

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void SubCallOr(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // ARGS 0
            _arg_index = 0;
            _arg_input_index = 0;

            // OR 1
            int _start_i1 = _arg_index;

            // LITERAL 'a'
            _ParseLiteralArgs(_memo, ref _arg_index, ref _arg_input_index, 'a', _args);

            // OR shortcut
            if (_memo.ArgResults.Peek() == null) { _memo.ArgResults.Pop(); _arg_index = _start_i1; } else goto label1;

            // LITERAL 'b'
            _ParseLiteralArgs(_memo, ref _arg_index, ref _arg_input_index, 'b', _args);

        label1: // OR
            int _dummy_i1 = _index; // no-op for label

            if (_memo.ArgResults.Pop() == null)
            {
                _memo.Results.Push(null);
                goto label0;
            }

            // AND 4
            int _start_i4 = _index;

            // LITERAL 'x'
            _ParseLiteralChar(_memo, ref _index, 'x');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label4; }

            // LITERAL 'y'
            _ParseLiteralChar(_memo, ref _index, 'y');

        label4: // AND
            var _r4_2 = _memo.Results.Pop();
            var _r4_1 = _memo.Results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _memo.Results.Push( new _TestParser_Item(_start_i4, _index, _memo.InputEnumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i4;
            }

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void CallOr(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // CALL SubCallOr
            var _start_i0 = _index;
            _TestParser_Item _r0;
            var _arg0_0 = 'a';

            _r0 = _MemoCall(_memo, "SubCallOr", _index, SubCallOr, new _TestParser_Item[] { new _TestParser_Item(_arg0_0) });

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void CallOr2(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // CALL SubCallOr
            var _start_i0 = _index;
            _TestParser_Item _r0;
            var _arg0_0 = 'b';

            _r0 = _MemoCall(_memo, "SubCallOr", _index, SubCallOr, new _TestParser_Item[] { new _TestParser_Item(_arg0_0) });

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void CallOr3(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // CALL SubCallOr
            var _start_i0 = _index;
            _TestParser_Item _r0;
            var _arg0_0 = 'c';

            _r0 = _MemoCall(_memo, "SubCallOr", _index, SubCallOr, new _TestParser_Item[] { new _TestParser_Item(_arg0_0) });

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void SubCallAnd(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // ARGS 0
            _arg_index = 0;
            _arg_input_index = 0;

            // AND 1
            int _start_i1 = _arg_index;

            // LITERAL 'a'
            _ParseLiteralArgs(_memo, ref _arg_index, ref _arg_input_index, 'a', _args);

            // AND shortcut
            if (_memo.ArgResults.Peek() == null) { _memo.ArgResults.Push(null); goto label1; }

            // LITERAL 'b'
            _ParseLiteralArgs(_memo, ref _arg_index, ref _arg_input_index, 'b', _args);

        label1: // AND
            var _r1_2 = _memo.ArgResults.Pop();
            var _r1_1 = _memo.ArgResults.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.ArgResults.Push(new _TestParser_Item(_start_i1, _arg_index, _r1_1.Inputs.Concat(_r1_2.Inputs), _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), false));
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

            // AND 4
            int _start_i4 = _index;

            // LITERAL 'x'
            _ParseLiteralChar(_memo, ref _index, 'x');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label4; }

            // LITERAL 'y'
            _ParseLiteralChar(_memo, ref _index, 'y');

        label4: // AND
            var _r4_2 = _memo.Results.Pop();
            var _r4_1 = _memo.Results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _memo.Results.Push( new _TestParser_Item(_start_i4, _index, _memo.InputEnumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i4;
            }

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void CallAnd(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // CALL SubCallAnd
            var _start_i0 = _index;
            _TestParser_Item _r0;
            var _arg0_0 = 'a';
            var _arg0_1 = 'b';

            _r0 = _MemoCall(_memo, "SubCallAnd", _index, SubCallAnd, new _TestParser_Item[] { new _TestParser_Item(_arg0_0), new _TestParser_Item(_arg0_1) });

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void CallAnd2(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // CALL SubCallAnd
            var _start_i0 = _index;
            _TestParser_Item _r0;
            var _arg0_0 = 'w';
            var _arg0_1 = 'z';

            _r0 = _MemoCall(_memo, "SubCallAnd", _index, SubCallAnd, new _TestParser_Item[] { new _TestParser_Item(_arg0_0), new _TestParser_Item(_arg0_1) });

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void SubCallStar(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
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
            _ParseLiteralArgs(_memo, ref _arg_index, ref _arg_input_index, 'a', _args);

            // STAR 1
            var _r1 = _memo.ArgResults.Pop();
            if (_r1 != null)
            {
                _inp1 = _inp1.Concat(_r1.Inputs);
                _res1 = _res1.Concat(_r1.Results);
                goto label1;
            }
            else
            {
                _memo.ArgResults.Push(new _TestParser_Item(_start_i1, _arg_index, _inp1, _res1.Where(_NON_NULL), false));
            }

            if (_memo.ArgResults.Pop() == null)
            {
                _memo.Results.Push(null);
                goto label0;
            }

            // AND 3
            int _start_i3 = _index;

            // LITERAL 'x'
            _ParseLiteralChar(_memo, ref _index, 'x');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label3; }

            // LITERAL 'y'
            _ParseLiteralChar(_memo, ref _index, 'y');

        label3: // AND
            var _r3_2 = _memo.Results.Pop();
            var _r3_1 = _memo.Results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _memo.Results.Push( new _TestParser_Item(_start_i3, _index, _memo.InputEnumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i3;
            }

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void CallStar(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // CALLORVAR SubCallStar
            _TestParser_Item _r0;

            _r0 = _MemoCall(_memo, "SubCallStar", _index, SubCallStar, null);

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void CallStar2(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // CALL SubCallStar
            var _start_i0 = _index;
            _TestParser_Item _r0;
            var _arg0_0 = 'a';

            _r0 = _MemoCall(_memo, "SubCallStar", _index, SubCallStar, new _TestParser_Item[] { new _TestParser_Item(_arg0_0) });

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void SubCallPlus(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
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
            _ParseLiteralArgs(_memo, ref _arg_index, ref _arg_input_index, 'a', _args);

            // PLUS 1
            var _r1 = _memo.ArgResults.Pop();
            if (_r1 != null)
            {
                _inp1 = _inp1.Concat(_r1.Inputs);
                _res1 = _res1.Concat(_r1.Results);
                goto label1;
            }
            else
            {
                if (_arg_index > _start_i1)
                    _memo.ArgResults.Push(new _TestParser_Item(_start_i1, _arg_index, _inp1, _res1.Where(_NON_NULL), false));
                else
                    _memo.ArgResults.Push(null);
            }

            if (_memo.ArgResults.Pop() == null)
            {
                _memo.Results.Push(null);
                goto label0;
            }

            // AND 3
            int _start_i3 = _index;

            // LITERAL 'x'
            _ParseLiteralChar(_memo, ref _index, 'x');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label3; }

            // LITERAL 'y'
            _ParseLiteralChar(_memo, ref _index, 'y');

        label3: // AND
            var _r3_2 = _memo.Results.Pop();
            var _r3_1 = _memo.Results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _memo.Results.Push( new _TestParser_Item(_start_i3, _index, _memo.InputEnumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i3;
            }

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void CallPlus(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // CALL SubCallPlus
            var _start_i0 = _index;
            _TestParser_Item _r0;
            var _arg0_0 = 'a';

            _r0 = _MemoCall(_memo, "SubCallPlus", _index, SubCallPlus, new _TestParser_Item[] { new _TestParser_Item(_arg0_0) });

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void CallPlus2(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // CALLORVAR SubCallPlus
            _TestParser_Item _r0;

            _r0 = _MemoCall(_memo, "SubCallPlus", _index, SubCallPlus, null);

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void SubCallQues(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // ARGS 0
            _arg_index = 0;
            _arg_input_index = 0;

            // LITERAL 'a'
            _ParseLiteralArgs(_memo, ref _arg_index, ref _arg_input_index, 'a', _args);

            // QUES
            if (_memo.ArgResults.Peek() == null) { _memo.ArgResults.Pop(); _memo.ArgResults.Push(new _TestParser_Item(_arg_index, _memo.InputEnumerable)); }

            if (_memo.ArgResults.Pop() == null)
            {
                _memo.Results.Push(null);
                goto label0;
            }

            // AND 3
            int _start_i3 = _index;

            // LITERAL 'x'
            _ParseLiteralChar(_memo, ref _index, 'x');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label3; }

            // LITERAL 'y'
            _ParseLiteralChar(_memo, ref _index, 'y');

        label3: // AND
            var _r3_2 = _memo.Results.Pop();
            var _r3_1 = _memo.Results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _memo.Results.Push( new _TestParser_Item(_start_i3, _index, _memo.InputEnumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i3;
            }

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void CallQues(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // CALL SubCallQues
            var _start_i0 = _index;
            _TestParser_Item _r0;
            var _arg0_0 = 'a';

            _r0 = _MemoCall(_memo, "SubCallQues", _index, SubCallQues, new _TestParser_Item[] { new _TestParser_Item(_arg0_0) });

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void CallQues2(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // CALLORVAR SubCallQues
            _TestParser_Item _r0;

            _r0 = _MemoCall(_memo, "SubCallQues", _index, SubCallQues, null);

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void SubCallCond(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
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
            _ParseAnyArgs(_memo, ref _arg_index, ref _arg_input_index, _args);

            // BIND v
            v = _memo.ArgResults.Peek();

            // COND
            if (_memo.ArgResults.Peek() == null || !((char)v == 'a')) { _memo.ArgResults.Pop(); _memo.ArgResults.Push(null); _arg_index = _start_i1; }

            if (_memo.ArgResults.Pop() == null)
            {
                _memo.Results.Push(null);
                goto label0;
            }

            // AND 4
            int _start_i4 = _index;

            // LITERAL 'x'
            _ParseLiteralChar(_memo, ref _index, 'x');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label4; }

            // LITERAL 'y'
            _ParseLiteralChar(_memo, ref _index, 'y');

        label4: // AND
            var _r4_2 = _memo.Results.Pop();
            var _r4_1 = _memo.Results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _memo.Results.Push( new _TestParser_Item(_start_i4, _index, _memo.InputEnumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i4;
            }

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void CallCond(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // CALL SubCallCond
            var _start_i0 = _index;
            _TestParser_Item _r0;
            var _arg0_0 = 'a';

            _r0 = _MemoCall(_memo, "SubCallCond", _index, SubCallCond, new _TestParser_Item[] { new _TestParser_Item(_arg0_0) });

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void CallCond2(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // CALLORVAR SubCallCond
            _TestParser_Item _r0;

            _r0 = _MemoCall(_memo, "SubCallCond", _index, SubCallCond, null);

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void VarInput(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            _TestParser_Item a = null;

            // AND 0
            int _start_i0 = _index;

            // LITERAL 'a'
            _ParseLiteralChar(_memo, ref _index, 'a');

            // BIND a
            a = _memo.Results.Peek();

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // CALLORVAR a
            _TestParser_Item _r3;

            if (a.Production != null)
            {
                var _p3 = (System.Action<_TestParser_Memo, int, IEnumerable<_TestParser_Item>>)(object)a.Production; // what type safety?
                _r3 = _MemoCall(_memo, a.Production.Method.Name, _index, _p3, null);
            }
            else
            {
                _r3 = _ParseLiteralObj(_memo, ref _index, a.Inputs);
            }

            if (_r3 != null) _index = _r3.NextIndex;

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _TestParser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void SubCallAct(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            _TestParser_Item a = null;

            // ARGS 0
            _arg_index = 0;
            _arg_input_index = 0;

            // LITERAL 'a'
            _ParseLiteralArgs(_memo, ref _arg_index, ref _arg_input_index, 'a', _args);

            // ACT
            var _r2 = _memo.ArgResults.Peek();
            if (_r2 != null)
            {
                _memo.ArgResults.Pop();
                _memo.ArgResults.Push( new _TestParser_Item(_r2.StartIndex, _r2.NextIndex, _r2.Inputs, _Thunk(_IM_Result => { return 42; }, _r2), false) );
            }

            // BIND a
            a = _memo.ArgResults.Peek();

            if (_memo.ArgResults.Pop() == null)
            {
                _memo.Results.Push(null);
                goto label0;
            }

            // CALLORVAR a
            _TestParser_Item _r4;

            if (a.Production != null)
            {
                var _p4 = (System.Action<_TestParser_Memo, int, IEnumerable<_TestParser_Item>>)(object)a.Production; // what type safety?
                _r4 = _MemoCall(_memo, a.Production.Method.Name, _index, _p4, null);
            }
            else
            {
                _r4 = _ParseLiteralObj(_memo, ref _index, a.Inputs);
            }

            if (_r4 != null) _index = _r4.NextIndex;

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void CallAct(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // CALL SubCallAct
            var _start_i0 = _index;
            _TestParser_Item _r0;
            var _arg0_0 = 'a';

            _r0 = _MemoCall(_memo, "SubCallAct", _index, SubCallAct, new _TestParser_Item[] { new _TestParser_Item(_arg0_0) });

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void CallAct2(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // CALLORVAR SubCallAct
            _TestParser_Item _r0;

            _r0 = _MemoCall(_memo, "SubCallAct", _index, SubCallAct, null);

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void SubCallVar(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
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
            _ParseLiteralArgs(_memo, ref _arg_index, ref _arg_input_index, 'a', _args);

            // BIND a
            a = _memo.ArgResults.Peek();

            // AND shortcut
            if (_memo.ArgResults.Peek() == null) { _memo.ArgResults.Push(null); goto label1; }

            // CALLORVAR a
            var _r4 = _ParseLiteralArgs(_memo, ref _arg_index, ref _arg_input_index, a.Inputs, _args);
            if (_r4 != null) _arg_index = _r4.NextIndex;

        label1: // AND
            var _r1_2 = _memo.ArgResults.Pop();
            var _r1_1 = _memo.ArgResults.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.ArgResults.Push(new _TestParser_Item(_start_i1, _arg_index, _r1_1.Inputs.Concat(_r1_2.Inputs), _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), false));
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

            // AND 5
            int _start_i5 = _index;

            // LITERAL 'x'
            _ParseLiteralChar(_memo, ref _index, 'x');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label5; }

            // LITERAL 'y'
            _ParseLiteralChar(_memo, ref _index, 'y');

        label5: // AND
            var _r5_2 = _memo.Results.Pop();
            var _r5_1 = _memo.Results.Pop();

            if (_r5_1 != null && _r5_2 != null)
            {
                _memo.Results.Push( new _TestParser_Item(_start_i5, _index, _memo.InputEnumerable, _r5_1.Results.Concat(_r5_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i5;
            }

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void CallCallVar(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // CALL SubCallVar
            var _start_i0 = _index;
            _TestParser_Item _r0;
            var _arg0_0 = "aa";

            _r0 = _MemoCall(_memo, "SubCallVar", _index, SubCallVar, new _TestParser_Item[] { new _TestParser_Item(_arg0_0) });

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void CallCallVar2(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // CALLORVAR SubCallVar
            _TestParser_Item _r0;

            _r0 = _MemoCall(_memo, "SubCallVar", _index, SubCallVar, null);

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void SubCallRule(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            _TestParser_Item a = null;

            // ARGS 0
            _arg_index = 0;
            _arg_input_index = 0;

            // ANY
            _ParseAnyArgs(_memo, ref _arg_index, ref _arg_input_index, _args);

            // BIND a
            a = _memo.ArgResults.Peek();

            if (_memo.ArgResults.Pop() == null)
            {
                _memo.Results.Push(null);
                goto label0;
            }

            // AND 3
            int _start_i3 = _index;

            // CALLORVAR a
            _TestParser_Item _r4;

            if (a.Production != null)
            {
                var _p4 = (System.Action<_TestParser_Memo, int, IEnumerable<_TestParser_Item>>)(object)a.Production; // what type safety?
                _r4 = _MemoCall(_memo, a.Production.Method.Name, _index, _p4, null);
            }
            else
            {
                _r4 = _ParseLiteralObj(_memo, ref _index, a.Inputs);
            }

            if (_r4 != null) _index = _r4.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label3; }

            // CALL a
            var _start_i5 = _index;
            _TestParser_Item _r5;
            _r5 = _MemoCall(_memo, a.ProductionName, _index, a.Production, null);

            if (_r5 != null) _index = _r5.NextIndex;

        label3: // AND
            var _r3_2 = _memo.Results.Pop();
            var _r3_1 = _memo.Results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _memo.Results.Push( new _TestParser_Item(_start_i3, _index, _memo.InputEnumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i3;
            }

        label0: // ARGS 0
            _arg_input_index = _arg_index; // no-op for label

        }


        public void XOrY(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // LITERAL 'x'
            _ParseLiteralChar(_memo, ref _index, 'x');

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // LITERAL 'y'
            _ParseLiteralChar(_memo, ref _index, 'y');

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void CallCallRule(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // CALL SubCallRule
            var _start_i0 = _index;
            _TestParser_Item _r0;

            _r0 = _MemoCall(_memo, "SubCallRule", _index, SubCallRule, new _TestParser_Item[] { new _TestParser_Item(XOrY) });

            if (_r0 != null) _index = _r0.NextIndex;

        }


        public void ChoiceLR(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // OR 1
            int _start_i1 = _index;

            // CALLORVAR ChoiceA
            _TestParser_Item _r2;

            _r2 = _MemoCall(_memo, "ChoiceA", _index, ChoiceA, null);

            if (_r2 != null) _index = _r2.NextIndex;

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i1; } else goto label1;

            // CALLORVAR ChoiceB
            _TestParser_Item _r3;

            _r3 = _MemoCall(_memo, "ChoiceB", _index, ChoiceB, null);

            if (_r3 != null) _index = _r3.NextIndex;

        label1: // OR
            int _dummy_i1 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // LITERAL 'x'
            _ParseLiteralChar(_memo, ref _index, 'x');

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void ChoiceA(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // CALLORVAR ChoiceLR
            _TestParser_Item _r1;

            _r1 = _MemoCall(_memo, "ChoiceLR", _index, ChoiceLR, null);

            if (_r1 != null) _index = _r1.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // LITERAL 'y'
            _ParseLiteralChar(_memo, ref _index, 'y');

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _TestParser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void ChoiceB(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // CALLORVAR ChoiceLR
            _TestParser_Item _r1;

            _r1 = _MemoCall(_memo, "ChoiceLR", _index, ChoiceLR, null);

            if (_r1 != null) _index = _r1.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // LITERAL 'z'
            _ParseLiteralChar(_memo, ref _index, 'z');

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _TestParser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void DirectLR(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // AND 1
            int _start_i1 = _index;

            // CALLORVAR DirectLR
            _TestParser_Item _r2;

            _r2 = _MemoCall(_memo, "DirectLR", _index, DirectLR, null);

            if (_r2 != null) _index = _r2.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // LITERAL 'y'
            _ParseLiteralChar(_memo, ref _index, 'y');

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _TestParser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i1;
            }

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // LITERAL 'x'
            _ParseLiteralChar(_memo, ref _index, 'x');

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void IndirectLR_A(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // AND 1
            int _start_i1 = _index;

            // CALLORVAR IndirectLR_B
            _TestParser_Item _r2;

            _r2 = _MemoCall(_memo, "IndirectLR_B", _index, IndirectLR_B, null);

            if (_r2 != null) _index = _r2.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // LITERAL 'y'
            _ParseLiteralChar(_memo, ref _index, 'y');

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _TestParser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i1;
            }

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // LITERAL 'x'
            _ParseLiteralChar(_memo, ref _index, 'x');

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void IndirectLR_B(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // CALLORVAR IndirectLR_A
            _TestParser_Item _r1;

            _r1 = _MemoCall(_memo, "IndirectLR_A", _index, IndirectLR_A, null);

            if (_r1 != null) _index = _r1.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // LITERAL 'z'
            _ParseLiteralChar(_memo, ref _index, 'z');

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _TestParser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void PathalogicalA(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // AND 1
            int _start_i1 = _index;

            // CALLORVAR PathalogicalA
            _TestParser_Item _r2;

            _r2 = _MemoCall(_memo, "PathalogicalA", _index, PathalogicalA, null);

            if (_r2 != null) _index = _r2.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // LITERAL 'a'
            _ParseLiteralChar(_memo, ref _index, 'a');

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _TestParser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i1;
            }

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // CALLORVAR PathalogicalB
            _TestParser_Item _r4;

            _r4 = _MemoCall(_memo, "PathalogicalB", _index, PathalogicalB, null);

            if (_r4 != null) _index = _r4.NextIndex;

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void PathalogicalB(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // OR 1
            int _start_i1 = _index;

            // AND 2
            int _start_i2 = _index;

            // CALLORVAR PathalogicalB
            _TestParser_Item _r3;

            _r3 = _MemoCall(_memo, "PathalogicalB", _index, PathalogicalB, null);

            if (_r3 != null) _index = _r3.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label2; }

            // LITERAL 'b'
            _ParseLiteralChar(_memo, ref _index, 'b');

        label2: // AND
            var _r2_2 = _memo.Results.Pop();
            var _r2_1 = _memo.Results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _memo.Results.Push( new _TestParser_Item(_start_i2, _index, _memo.InputEnumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i2;
            }

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i1; } else goto label1;

            // CALLORVAR PathalogicalA
            _TestParser_Item _r5;

            _r5 = _MemoCall(_memo, "PathalogicalA", _index, PathalogicalA, null);

            if (_r5 != null) _index = _r5.NextIndex;

        label1: // OR
            int _dummy_i1 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // CALLORVAR PathalogicalC
            _TestParser_Item _r6;

            _r6 = _MemoCall(_memo, "PathalogicalC", _index, PathalogicalC, null);

            if (_r6 != null) _index = _r6.NextIndex;

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void PathalogicalC(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // OR 1
            int _start_i1 = _index;

            // AND 2
            int _start_i2 = _index;

            // CALLORVAR PathalogicalC
            _TestParser_Item _r3;

            _r3 = _MemoCall(_memo, "PathalogicalC", _index, PathalogicalC, null);

            if (_r3 != null) _index = _r3.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label2; }

            // LITERAL 'c'
            _ParseLiteralChar(_memo, ref _index, 'c');

        label2: // AND
            var _r2_2 = _memo.Results.Pop();
            var _r2_1 = _memo.Results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _memo.Results.Push( new _TestParser_Item(_start_i2, _index, _memo.InputEnumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i2;
            }

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i1; } else goto label1;

            // CALLORVAR PathalogicalB
            _TestParser_Item _r5;

            _r5 = _MemoCall(_memo, "PathalogicalB", _index, PathalogicalB, null);

            if (_r5 != null) _index = _r5.NextIndex;

        label1: // OR
            int _dummy_i1 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // LITERAL 'd'
            _ParseLiteralChar(_memo, ref _index, 'd');

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void TestBuildTasks(_TestParser_Memo _memo, int _index, _TestParser_Args _args)
        {

            // LITERAL "testBuildTask9"
            _ParseLiteralString(_memo, ref _index, "testBuildTask9");

        }

    } // class TestParser

} // namespace IronMeta.UnitTests

