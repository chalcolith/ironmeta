//
// IronMeta AnonObjectParser Parser; Generated 2016-03-30 04:29:18Z UTC
//

using System;
using System.Collections.Generic;
using System.Linq;

using IronMeta.Matcher;

#pragma warning disable 0219
#pragma warning disable 1591

namespace IronMeta.UnitTests.Matcher
{

    using _AnonObjectParser_Inputs = IEnumerable<AnonInputObject>;
    using _AnonObjectParser_Results = IEnumerable<int>;
    using _AnonObjectParser_Item = IronMeta.Matcher.MatchItem<AnonInputObject, int>;
    using _AnonObjectParser_Args = IEnumerable<IronMeta.Matcher.MatchItem<AnonInputObject, int>>;
    using _AnonObjectParser_Memo = IronMeta.Matcher.MatchState<AnonInputObject, int>;
    using _AnonObjectParser_Rule = System.Action<IronMeta.Matcher.MatchState<AnonInputObject, int>, int, IEnumerable<IronMeta.Matcher.MatchItem<AnonInputObject, int>>>;
    using _AnonObjectParser_Base = IronMeta.Matcher.Matcher<AnonInputObject, int>;

    public partial class AnonObjectParser : Matcher<AnonInputObject, int>
    {
        public AnonObjectParser()
            : base()
        {
            _setTerminals();
        }

        public AnonObjectParser(bool handle_left_recursion)
            : base(handle_left_recursion)
        {
            _setTerminals();
        }

        void _setTerminals()
        {
            this.Terminals = new HashSet<string>()
            {
                "ActualObject",
                "ImplicitObject",
            };
        }


        public void ActualObject(_AnonObjectParser_Memo _memo, int _index, _AnonObjectParser_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // AND 0
            int _start_i0 = _index;

            // LITERAL new AnonInputObject { Name = "actual", Value = "one" }
            _ParseLiteralObj(_memo, ref _index, new AnonInputObject { Name = "actual", Value = "one" });

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // LITERAL new AnonInputObject { Name = "actual", Value = "two" }
            _ParseLiteralObj(_memo, ref _index, new AnonInputObject { Name = "actual", Value = "two" });

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _AnonObjectParser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void ImplicitObject(_AnonObjectParser_Memo _memo, int _index, _AnonObjectParser_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // AND 0
            int _start_i0 = _index;

            // LITERAL new { Name = "implicit", Value = "three" }
            _ParseLiteralObj(_memo, ref _index, new { Name = "implicit", Value = "three" });

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // LITERAL new { Name = "implicit", Value = "four" }
            _ParseLiteralObj(_memo, ref _index, new { Name = "implicit", Value = "four" });

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _AnonObjectParser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


    } // class AnonObjectParser

} // namespace IronMeta.UnitTests.Matcher

