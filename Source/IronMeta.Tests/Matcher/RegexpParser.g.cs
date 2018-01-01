//
// IronMeta RegexpTest Parser; Generated 2017-12-30 00:42:38Z UTC
//

using System;
using System.Collections.Generic;
using System.Linq;

using IronMeta.Matcher;

#pragma warning disable 0219
#pragma warning disable 1591

namespace IronMeta.UnitTests.Matcher
{

    using _RegexpTest_Inputs = IEnumerable<char>;
    using _RegexpTest_Results = IEnumerable<string>;
    using _RegexpTest_Item = IronMeta.Matcher.MatchItem<char, string>;
    using _RegexpTest_Args = IEnumerable<IronMeta.Matcher.MatchItem<char, string>>;
    using _RegexpTest_Memo = IronMeta.Matcher.MatchState<char, string>;
    using _RegexpTest_Rule = System.Action<IronMeta.Matcher.MatchState<char, string>, int, IEnumerable<IronMeta.Matcher.MatchItem<char, string>>>;
    using _RegexpTest_Base = IronMeta.Matcher.Matcher<char, string>;

    public partial class RegexpTest : IronMeta.Matcher.Matcher<char, string>
    {
        public RegexpTest()
            : base()
        {
            _setTerminals();
        }

        public RegexpTest(bool handle_left_recursion)
            : base(handle_left_recursion)
        {
            _setTerminals();
        }

        void _setTerminals()
        {
            this.Terminals = new HashSet<string>()
            {
                "ABCD",
                "Bar",
                "Foo",
                "Ident",
            };
        }


        public void ABCD(_RegexpTest_Memo _memo, int _index, _RegexpTest_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // AND 0
            int _start_i0 = _index;

            // LITERAL 'a'
            _ParseLiteralChar(_memo, ref _index, 'a');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // REGEXP [\+-]?bz?cd+
            _ParseRegexp(_memo, ref _index, _re0);

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _RegexpTest_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void Ident(_RegexpTest_Memo _memo, int _index, _RegexpTest_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // REGEXP _|_[_0-9a-zA-Z]+|[a-zA-Z][_0-9a-zA-Z]*
            _ParseRegexp(_memo, ref _index, _re1);

        }


        public void Foo(_RegexpTest_Memo _memo, int _index, _RegexpTest_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // REGEXP [^\r\n]+
            _ParseRegexp(_memo, ref _index, _re2);

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _RegexpTest_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return new string(_IM_Result.Inputs.ToArray()); }, _r0), true) );
            }

        }


        public void Bar(_RegexpTest_Memo _memo, int _index, _RegexpTest_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // PLUS 1
            int _start_i1 = _index;
            var _res1 = Enumerable.Empty<string>();
        label1:

            // AND 2
            int _start_i2 = _index;

            // NOT 3
            int _start_i3 = _index;

            // OR 4
            int _start_i4 = _index;

            // LITERAL '\r'
            _ParseLiteralChar(_memo, ref _index, '\r');

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i4; } else goto label4;

            // LITERAL '\n'
            _ParseLiteralChar(_memo, ref _index, '\n');

        label4: // OR
            int _dummy_i4 = _index; // no-op for label

            // NOT 3
            var _r3 = _memo.Results.Pop();
            _memo.Results.Push( _r3 == null ? new _RegexpTest_Item(_start_i3, _memo.InputEnumerable) : null);
            _index = _start_i3;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label2; }

            // ANY
            _ParseAny(_memo, ref _index);

        label2: // AND
            var _r2_2 = _memo.Results.Pop();
            var _r2_1 = _memo.Results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _memo.Results.Push( new _RegexpTest_Item(_start_i2, _index, _memo.InputEnumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i2;
            }

            // PLUS 1
            var _r1 = _memo.Results.Pop();
            if (_r1 != null)
            {
                _res1 = _res1.Concat(_r1.Results);
                goto label1;
            }
            else
            {
                if (_index > _start_i1)
                    _memo.Results.Push(new _RegexpTest_Item(_start_i1, _index, _memo.InputEnumerable, _res1.Where(_NON_NULL), true));
                else
                    _memo.Results.Push(null);
            }

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _RegexpTest_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return new string(_IM_Result.Inputs.ToArray()); }, _r0), true) );
            }

        }

        static readonly Verophyle.Regexp.StringRegexp _re0 = new Verophyle.Regexp.StringRegexp(@"[\+-]?bz?cd+");
        static readonly Verophyle.Regexp.StringRegexp _re1 = new Verophyle.Regexp.StringRegexp(@"_|_[_0-9a-zA-Z]+|[a-zA-Z][_0-9a-zA-Z]*");
        static readonly Verophyle.Regexp.StringRegexp _re2 = new Verophyle.Regexp.StringRegexp(@"[^\r\n]+");

    } // class RegexpTest

} // namespace IronMeta.UnitTests.Matcher

