// IronMeta Generated Common: 11/10/2009 8:48:16 PM UTC

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Common
{

    public partial class CommonMatcher : IronMeta.CharacterMatcher<XmlNode>
    {

        /// <summary>Default Constructor.</summary>
        public CommonMatcher()
            : base(a => default(XmlNode), true)
        {
        }

        /// <summary>Constructor.</summary>
        public CommonMatcher(Func<char,XmlNode> conv, bool strictPEG)
            : base(conv, strictPEG)
        {
        }

        /// <summary>Utility class for referencing variables in conditions and actions.</summary>
        private class CommonMatcherItem : MatchItem
        {
            public CommonMatcherItem() : base() { }
            public CommonMatcherItem(string name) : base(name) { }
            public CommonMatcherItem(MatchItem mi) : base(mi) { }

            public static implicit operator XmlNode(CommonMatcherItem item) { return item.Results.LastOrDefault(); }
            public static implicit operator List<XmlNode>(CommonMatcherItem item) { return item.Results.ToList(); }
            public static implicit operator char(CommonMatcherItem item) { return item.Inputs.LastOrDefault(); }
            public static implicit operator List<char>(CommonMatcherItem item) { return item.Inputs.ToList(); }
        }

        private int _NewXmlDocument_Body__Index_ = -1;

        protected virtual IEnumerable<MatchItem> NewXmlDocument(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _NewXmlDocument_Body_ = null;

            if (_NewXmlDocument_Body__Index_ == -1 || CachedCombinators[_NewXmlDocument_Body__Index_] == null)
            {
                if (_NewXmlDocument_Body__Index_ == -1)
                {
                    _NewXmlDocument_Body__Index_ = CachedCombinators.Count;
                    CachedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _ACTION(_LOOK(_ANY()), (_IM_Result_MI_) => {{ 
#line 11 "Common.ironMeta"
     { return new XmlDocument(); }
#line default
}});
                }

                CachedCombinators[_NewXmlDocument_Body__Index_] = _disj_0_;
            }

            _NewXmlDocument_Body_ = CachedCombinators[_NewXmlDocument_Body__Index_];


            foreach (var _res_ in _NewXmlDocument_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> NewXmlDocElement(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _NewXmlDocElement_Body_ = null;

            Combinator _disj_0_ = null;
            {
                var parent = new CommonMatcherItem("parent");
                var name = new CommonMatcherItem("name");
                _disj_0_ = _ARGS(_AND(_VAR(_ANY(), parent), _VAR(_STAR(_ANY()), name)), _args, _ACTION(_LOOK(_ANY()), (_IM_Result_MI_) => {{ 
#line 14 "Common.ironMeta"
     {
				XmlDocument doc = (XmlDocument)parent;
				XmlElement child = doc.CreateElement(_IM_GetText(name));
				doc.AppendChild(child);
				return child;
			}
#line default
}}));
            }

            _NewXmlDocElement_Body_ = _disj_0_;

            foreach (var _res_ in _NewXmlDocElement_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> NewXmlElement(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _NewXmlElement_Body_ = null;

            Combinator _disj_0_ = null;
            {
                var parent = new CommonMatcherItem("parent");
                var name = new CommonMatcherItem("name");
                _disj_0_ = _ARGS(_AND(_VAR(_ANY(), parent), _VAR(_STAR(_ANY()), name)), _args, _ACTION(_LOOK(_ANY()), (_IM_Result_MI_) => {{ 
#line 22 "Common.ironMeta"
     {
				XmlElement elem = (XmlElement)parent;
				XmlElement child = elem.OwnerDocument.CreateElement(_IM_GetText(name));
				elem.AppendChild(child);
				return child;
			}
#line default
}}));
            }

            _NewXmlElement_Body_ = _disj_0_;

            foreach (var _res_ in _NewXmlElement_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> Header(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _Header_Body_ = null;

            Combinator _disj_0_ = null;
            {
                var name = new CommonMatcherItem("name");
                var att = new CommonMatcherItem("att");
                var rule = new CommonMatcherItem("rule");
                var element = new CommonMatcherItem("element");
                var d = new CommonMatcherItem("d");
                var WSL = new CommonMatcherItem("WSL");
                var c = new CommonMatcherItem("c");
                _disj_0_ = _ARGS(_AND(_VAR(_ANY(), name), _VAR(_ANY(), att), _VAR(_ANY(), rule), _VAR(_ANY(), element)), _args, _ACTION(_AND(_REF(name, this), _CALL(WS), _LITERAL(":"), _CALL(WS), _OR(_AND(_VAR(_REF(rule, this), d), _REF(WSL, this)), _AND(_STAR(_CONDITION(_VAR(_ANY(), c), (_IM_Result_MI_) => { return (
#line 29 "Common.ironMeta"
                                                                       (c == '\n' && c == '\r' && c == '\f')
#line default
);})), _REF(WSL, this)))), (_IM_Result_MI_) => {{ 
#line 30 "Common.ironMeta"
     { 
				//Console.WriteLine("header: {0}: {2}", _IM_GetText(name), _IM_GetText(att), _IM_GetText(d));
				((XmlElement)element).SetAttribute(_IM_GetText(att), _IM_GetText(d)); 
			}
#line default
} return default(XmlNode);}));
            }

            _Header_Body_ = _disj_0_;

            foreach (var _res_ in _Header_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> Token(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _Token_Body_ = null;

            Combinator _disj_0_ = null;
            {
                var c = new CommonMatcherItem("c");
                _disj_0_ = _OR(_PLUS(_CONDITION(_VAR(_ANY(), c), (_IM_Result_MI_) => { return (
#line 35 "Common.ironMeta"
               (!char.IsWhiteSpace(c))
#line default
);})), _FAIL("expected Token"));
            }
            Combinator _disj_1_ = null;
            {
                var Whitespace = new CommonMatcherItem("Whitespace");
                _disj_1_ = _PLUS(_AND(_NOT(_REF(Whitespace, this)), _ANY()));
            }

            _Token_Body_ = _OR(_disj_0_, _disj_1_);

            foreach (var _res_ in _Token_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> RestOfLine(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _RestOfLine_Body_ = null;

            Combinator _disj_0_ = null;
            {
                var c = new CommonMatcherItem("c");
                _disj_0_ = _STAR(_CONDITION(_VAR(_ANY(), c), (_IM_Result_MI_) => { return (
#line 36 "Common.ironMeta"
                    (c != '\n' && c != '\r' && c != '\f')
#line default
);}));
            }

            _RestOfLine_Body_ = _disj_0_;

            foreach (var _res_ in _RestOfLine_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        private int _Date_Body__Index_ = -1;

        protected virtual IEnumerable<MatchItem> Date(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _Date_Body_ = null;

            if (_Date_Body__Index_ == -1 || CachedCombinators[_Date_Body__Index_] == null)
            {
                if (_Date_Body__Index_ == -1)
                {
                    _Date_Body__Index_ = CachedCombinators.Count;
                    CachedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _OR(_AND(_CALL(Digit), _QUES(_CALL(Digit)), _LITERAL('/'), _CALL(Digit), _QUES(_CALL(Digit)), _LITERAL('/'), _CALL(Digit), _CALL(Digit), _QUES(_AND(_CALL(Digit), _CALL(Digit)))), _FAIL("expected a date value"));
                }
                Combinator _disj_1_ = null;
                {
                    _disj_1_ = _AND(_CALL(Digit), _QUES(_CALL(Digit)), _LITERAL('/'), _CALL(Digit), _QUES(_CALL(Digit)), _LITERAL('/'), _CALL(Digit), _CALL(Digit), _QUES(_AND(_CALL(Digit), _CALL(Digit))));
                }

                CachedCombinators[_Date_Body__Index_] = _OR(_disj_0_, _disj_1_);
            }

            _Date_Body_ = CachedCombinators[_Date_Body__Index_];


            foreach (var _res_ in _Date_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        private int _Time_Body__Index_ = -1;

        protected virtual IEnumerable<MatchItem> Time(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _Time_Body_ = null;

            if (_Time_Body__Index_ == -1 || CachedCombinators[_Time_Body__Index_] == null)
            {
                if (_Time_Body__Index_ == -1)
                {
                    _Time_Body__Index_ = CachedCombinators.Count;
                    CachedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _OR(_AND(_CALL(Digit), _QUES(_CALL(Digit)), _LITERAL(":"), _CALL(Digit), _QUES(_CALL(Digit)), _LITERAL(":"), _CALL(Digit), _QUES(_CALL(Digit)), _CALL(WS), _QUES(_OR(_LITERAL("AM"), _LITERAL("PM")))), _FAIL("expected a time value"));
                }
                Combinator _disj_1_ = null;
                {
                    _disj_1_ = _AND(_CALL(Digit), _QUES(_CALL(Digit)), _LITERAL(":"), _CALL(Digit), _QUES(_CALL(Digit)), _LITERAL(":"), _CALL(Digit), _QUES(_CALL(Digit)), _CALL(WS), _QUES(_OR(_LITERAL("AM"), _LITERAL("PM"))));
                }

                CachedCombinators[_Time_Body__Index_] = _OR(_disj_0_, _disj_1_);
            }

            _Time_Body_ = CachedCombinators[_Time_Body__Index_];


            foreach (var _res_ in _Time_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> WSList(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _WSList_Body_ = null;

            Combinator _disj_0_ = null;
            {
                var first = new CommonMatcherItem("first");
                var rest = new CommonMatcherItem("rest");
                _disj_0_ = _ARGS(_AND(_VAR(_ANY(), first), _VAR(_PLUS(_ANY()), rest)), _args, _AND(_ACTION(_REF(first, this), (_IM_Result_MI_) => {{ var _IM_Result = new CommonMatcherItem(_IM_Result_MI_); int _IM_StartIndex = _IM_Result.StartIndex; int _IM_NextIndex = _IM_Result.NextIndex; 
#line 41 "Common.ironMeta"
                                   {Console.WriteLine(_IM_GetText(_IM_Result));}
#line default
} return default(XmlNode);}), _CALL(WS), _CALL(WSList, rest.AsList)));
            }
            Combinator _disj_1_ = null;
            {
                var last = new CommonMatcherItem("last");
                _disj_1_ = _ARGS(_VAR(_ANY(), last), _args, _REF(last, this));
            }

            _WSList_Body_ = _OR(_disj_0_, _disj_1_);

            foreach (var _res_ in _WSList_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> Line(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _Line_Body_ = null;

            Combinator _disj_0_ = null;
            {
                var EOL = new CommonMatcherItem("EOL");
                _disj_0_ = _PLUS(_AND(_NOT(_REF(EOL, this)), _ANY()));
            }

            _Line_Body_ = _disj_0_;

            foreach (var _res_ in _Line_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        private int _Number_Body__Index_ = -1;

        protected virtual IEnumerable<MatchItem> Number(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _Number_Body_ = null;

            if (_Number_Body__Index_ == -1 || CachedCombinators[_Number_Body__Index_] == null)
            {
                if (_Number_Body__Index_ == -1)
                {
                    _Number_Body__Index_ = CachedCombinators.Count;
                    CachedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _AND(_PLUS(_CALL(Digit)), _QUES(_AND(_LITERAL("."), _PLUS(_CALL(Digit)))));
                }
                Combinator _disj_1_ = null;
                {
                    _disj_1_ = _FAIL("expected Number");
                }

                CachedCombinators[_Number_Body__Index_] = _OR(_disj_0_, _disj_1_);
            }

            _Number_Body_ = CachedCombinators[_Number_Body__Index_];


            foreach (var _res_ in _Number_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        private int _Digit_Body__Index_ = -1;

        protected virtual IEnumerable<MatchItem> Digit(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _Digit_Body_ = null;

            if (_Digit_Body__Index_ == -1 || CachedCombinators[_Digit_Body__Index_] == null)
            {
                if (_Digit_Body__Index_ == -1)
                {
                    _Digit_Body__Index_ = CachedCombinators.Count;
                    CachedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _OR(_LITERAL('0'), _LITERAL('1'), _LITERAL('2'), _LITERAL('3'), _LITERAL('4'), _LITERAL('5'), _LITERAL('6'), _LITERAL('7'), _LITERAL('8'), _LITERAL('9'));
                }

                CachedCombinators[_Digit_Body__Index_] = _disj_0_;
            }

            _Digit_Body_ = CachedCombinators[_Digit_Body__Index_];


            foreach (var _res_ in _Digit_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> WS(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _WS_Body_ = null;

            Combinator _disj_0_ = null;
            {
                var Whitespace = new CommonMatcherItem("Whitespace");
                _disj_0_ = _STAR(_REF(Whitespace, this));
            }

            _WS_Body_ = _disj_0_;

            foreach (var _res_ in _WS_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

    } // class CommonMatcher

} // namespace Common
