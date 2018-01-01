//
// IronMeta CommentTests Parser; Generated 2016-03-30 04:29:19Z UTC
//

using System;
using System.Collections.Generic;
using System.Linq;

using IronMeta.Matcher;

#pragma warning disable 0219
#pragma warning disable 1591

namespace IronMeta.UnitTests.Matcher
{

    using _CommentTests_Inputs = IEnumerable<char>;
    using _CommentTests_Results = IEnumerable<bool>;
    using _CommentTests_Item = IronMeta.Matcher.MatchItem<char, bool>;
    using _CommentTests_Args = IEnumerable<IronMeta.Matcher.MatchItem<char, bool>>;
    using _CommentTests_Memo = IronMeta.Matcher.MatchState<char, bool>;
    using _CommentTests_Rule = System.Action<IronMeta.Matcher.MatchState<char, bool>, int, IEnumerable<IronMeta.Matcher.MatchItem<char, bool>>>;
    using _CommentTests_Base = IronMeta.Matcher.Matcher<char, bool>;

    public partial class CommentTests : IronMeta.Matcher.Matcher<char, bool>
    {
        public CommentTests()
            : base()
        {
            _setTerminals();
        }

        public CommentTests(bool handle_left_recursion)
            : base(handle_left_recursion)
        {
            _setTerminals();
        }

        void _setTerminals()
        {
            this.Terminals = new HashSet<string>()
            {
                "r1",
                "r2",
                "s",
                "s2",
                "s3",
            };
        }


        public void s(_CommentTests_Memo _memo, int _index, _CommentTests_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // OR 0
            int _start_i0 = _index;

            // CALLORVAR r1
            _CommentTests_Item _r1;

            _r1 = _MemoCall(_memo, "r1", _index, r1, null);

            if (_r1 != null) _index = _r1.NextIndex;

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // CALLORVAR r2
            _CommentTests_Item _r2;

            _r2 = _MemoCall(_memo, "r2", _index, r2, null);

            if (_r2 != null) _index = _r2.NextIndex;

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void r1(_CommentTests_Memo _memo, int _index, _CommentTests_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // LITERAL 'a'
            _ParseLiteralChar(_memo, ref _index, 'a');

        }


        public void r2(_CommentTests_Memo _memo, int _index, _CommentTests_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // LITERAL 'b'
            _ParseLiteralChar(_memo, ref _index, 'b');

        }


        public void s2(_CommentTests_Memo _memo, int _index, _CommentTests_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // OR 0
            int _start_i0 = _index;

            // CALLORVAR r1
            _CommentTests_Item _r1;

            _r1 = _MemoCall(_memo, "r1", _index, r1, null);

            if (_r1 != null) _index = _r1.NextIndex;

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // CALLORVAR r2
            _CommentTests_Item _r2;

            _r2 = _MemoCall(_memo, "r2", _index, r2, null);

            if (_r2 != null) _index = _r2.NextIndex;

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void s3(_CommentTests_Memo _memo, int _index, _CommentTests_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // AND 0
            int _start_i0 = _index;

            // CALLORVAR r1
            _CommentTests_Item _r1;

            _r1 = _MemoCall(_memo, "r1", _index, r1, null);

            if (_r1 != null) _index = _r1.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // CALLORVAR r2
            _CommentTests_Item _r2;

            _r2 = _MemoCall(_memo, "r2", _index, r2, null);

            if (_r2 != null) _index = _r2.NextIndex;

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _CommentTests_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


    } // class CommentTests

} // namespace IronMeta.UnitTests.Matcher

