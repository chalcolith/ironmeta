//
// IronMeta MemoizeParser Parser; Generated 2014-05-30 23:38:49Z UTC
//

using System;
using System.Collections.Generic;
using System.Linq;
using IronMeta.Matcher;

#pragma warning disable 0219
#pragma warning disable 1591

namespace IronMeta.Tests.Matcher.Memoization
{

    using _MemoizeParser_Inputs = IEnumerable<char>;
    using _MemoizeParser_Results = IEnumerable<Node>;
    using _MemoizeParser_Item = IronMeta.Matcher.MatchItem<char, Node>;
    using _MemoizeParser_Args = IEnumerable<IronMeta.Matcher.MatchItem<char, Node>>;
    using _MemoizeParser_Memo = Memo<char, Node>;
    using _MemoizeParser_Rule = System.Action<Memo<char, Node>, int, IEnumerable<IronMeta.Matcher.MatchItem<char, Node>>>;
    using _MemoizeParser_Base = IronMeta.Matcher.Matcher<char, Node>;

    public partial class MemoizeParser : CharMatcher<Node>
    {
        public MemoizeParser()
            : base()
        {
            _setTerminals();
        }

        public MemoizeParser(bool handle_left_recursion)
            : base(handle_left_recursion)
        {
            _setTerminals();
        }

        void _setTerminals()
        {
            this.Terminals = new HashSet<_MemoizeParser_Rule>()
            {
                Char,
                EOF,
            };
        }


        public void AlternateEOF(_MemoizeParser_Memo _memo, int _index, _MemoizeParser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // CALLORVAR Alternate
            _MemoizeParser_Item _r1;

            _r1 = _MemoCall(_memo, "Alternate", _index, Alternate, null);

            if (_r1 != null) _index = _r1.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // CALLORVAR EOF
            _MemoizeParser_Item _r2;

            _r2 = _MemoCall(_memo, "EOF", _index, EOF, null);

            if (_r2 != null) _index = _r2.NextIndex;

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _MemoizeParser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void SequenceEOF(_MemoizeParser_Memo _memo, int _index, _MemoizeParser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // CALLORVAR Sequence
            _MemoizeParser_Item _r1;

            _r1 = _MemoCall(_memo, "Sequence", _index, Sequence, null);

            if (_r1 != null) _index = _r1.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // CALLORVAR EOF
            _MemoizeParser_Item _r2;

            _r2 = _MemoCall(_memo, "EOF", _index, EOF, null);

            if (_r2 != null) _index = _r2.NextIndex;

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _MemoizeParser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void Alternate(_MemoizeParser_Memo _memo, int _index, _MemoizeParser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // AND 1
            int _start_i1 = _index;

            // AND 2
            int _start_i2 = _index;

            // CALLORVAR Alternate
            _MemoizeParser_Item _r3;

            _r3 = _MemoCall(_memo, "Alternate", _index, Alternate, null);

            if (_r3 != null) _index = _r3.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label2; }

            // LITERAL " | "
            _ParseLiteralString(_memo, ref _index, " | ");

        label2: // AND
            var _r2_2 = _memo.Results.Pop();
            var _r2_1 = _memo.Results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _memo.Results.Push( new _MemoizeParser_Item(_start_i2, _index, _memo.InputEnumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i2;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // CALLORVAR Sequence
            _MemoizeParser_Item _r5;

            _r5 = _MemoCall(_memo, "Sequence", _index, Sequence, null);

            if (_r5 != null) _index = _r5.NextIndex;

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _MemoizeParser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i1;
            }

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // CALLORVAR Sequence
            _MemoizeParser_Item _r6;

            _r6 = _MemoCall(_memo, "Sequence", _index, Sequence, null);

            if (_r6 != null) _index = _r6.NextIndex;

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void Sequence(_MemoizeParser_Memo _memo, int _index, _MemoizeParser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // AND 1
            int _start_i1 = _index;

            // AND 2
            int _start_i2 = _index;

            // CALLORVAR Sequence
            _MemoizeParser_Item _r3;

            _r3 = _MemoCall(_memo, "Sequence", _index, Sequence, null);

            if (_r3 != null) _index = _r3.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label2; }

            // LITERAL " "
            _ParseLiteralString(_memo, ref _index, " ");

        label2: // AND
            var _r2_2 = _memo.Results.Pop();
            var _r2_1 = _memo.Results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _memo.Results.Push( new _MemoizeParser_Item(_start_i2, _index, _memo.InputEnumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i2;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // CALLORVAR Single
            _MemoizeParser_Item _r5;

            _r5 = _MemoCall(_memo, "Single", _index, Single, null);

            if (_r5 != null) _index = _r5.NextIndex;

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _MemoizeParser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i1;
            }

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // CALLORVAR Single
            _MemoizeParser_Item _r6;

            _r6 = _MemoCall(_memo, "Single", _index, Single, null);

            if (_r6 != null) _index = _r6.NextIndex;

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void Single(_MemoizeParser_Memo _memo, int _index, _MemoizeParser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // CALLORVAR Category
            _MemoizeParser_Item _r1;

            _r1 = _MemoCall(_memo, "Category", _index, Category, null);

            if (_r1 != null) _index = _r1.NextIndex;

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // CALLORVAR Char
            _MemoizeParser_Item _r2;

            _r2 = _MemoCall(_memo, "Char", _index, Char, null);

            if (_r2 != null) _index = _r2.NextIndex;

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void Category(_MemoizeParser_Memo _memo, int _index, _MemoizeParser_Args _args)
        {

            _MemoizeParser_Item s = null;

            // AND 1
            int _start_i1 = _index;

            // AND 2
            int _start_i2 = _index;

            // LITERAL "["
            _ParseLiteralString(_memo, ref _index, "[");

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label2; }

            // CALLORVAR Sequence
            _MemoizeParser_Item _r5;

            _r5 = _MemoCall(_memo, "Sequence", _index, Sequence, null);

            if (_r5 != null) _index = _r5.NextIndex;

            // BIND s
            s = _memo.Results.Peek();

        label2: // AND
            var _r2_2 = _memo.Results.Pop();
            var _r2_1 = _memo.Results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _memo.Results.Push( new _MemoizeParser_Item(_start_i2, _index, _memo.InputEnumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i2;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // LITERAL "]"
            _ParseLiteralString(_memo, ref _index, "]");

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _MemoizeParser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
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
                _memo.Results.Push( new _MemoizeParser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { categoryCount++; return new CategoryNode { Children = s.Results }; }, _r0), true) );
            }

        }


        public void Char(_MemoizeParser_Memo _memo, int _index, _MemoizeParser_Args _args)
        {

            _MemoizeParser_Item c = null;

            // INPUT CLASS
            _ParseInputClass(_memo, ref _index, '\u0061', '\u0062', '\u0063', '\u0064', '\u0065', '\u0066', '\u0067', '\u0068', '\u0069', '\u006a', '\u006b', '\u006c', '\u006d', '\u006e', '\u006f', '\u0070', '\u0071', '\u0072', '\u0073', '\u0074', '\u0075', '\u0076', '\u0077', '\u0078', '\u0079', '\u007a');

            // BIND c
            c = _memo.Results.Peek();

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _MemoizeParser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { charCount++; return new CharNode { Value = c }; }, _r0), true) );
            }

        }


        public void EOF(_MemoizeParser_Memo _memo, int _index, _MemoizeParser_Args _args)
        {

            // NOT 0
            int _start_i0 = _index;

            // ANY
            _ParseAny(_memo, ref _index);

            // NOT 0
            var _r0 = _memo.Results.Pop();
            _memo.Results.Push( _r0 == null ? new _MemoizeParser_Item(_start_i0, _memo.InputEnumerable) : null);
            _index = _start_i0;

        }

    } // class MemoizeParser

} // namespace Memoization

