// IronMeta Generated MasterPlexQTReport: 18/07/2009 2:45:05 AM UTC

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace MasterPlexQTReport
{

    public partial class MasterPlexQTReportMatcher : Common.CommonMatcher
    {

        /// <summary>Default Constructor.</summary>
        public MasterPlexQTReportMatcher()
            : base(a => default(XmlNode), true)
        {
        }

        /// <summary>Constructor.</summary>
        public MasterPlexQTReportMatcher(Func<char,XmlNode> conv, bool strictPEG)
            : base(conv, strictPEG)
        {
        }

        /// <summary>Utility class for referencing variables in conditions and actions.</summary>
        private class MasterPlexQTReportMatcherItem : MatchItem
        {
            public MasterPlexQTReportMatcherItem() : base() { }
            public MasterPlexQTReportMatcherItem(string name) : base(name) { }
            public MasterPlexQTReportMatcherItem(MatchItem mi) : base(mi) { }

            public static implicit operator XmlNode(MasterPlexQTReportMatcherItem item) { return item.Results.LastOrDefault(); }
            public static implicit operator List<XmlNode>(MasterPlexQTReportMatcherItem item) { return item.Results.ToList(); }
            public static implicit operator char(MasterPlexQTReportMatcherItem item) { return item.Inputs.LastOrDefault(); }
            public static implicit operator List<char>(MasterPlexQTReportMatcherItem item) { return item.Inputs.ToList(); }
        }

        protected virtual IEnumerable<MatchItem> Report(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _Report_Body_ = null;

            Combinator _disj_0_ = null;
            {
                var xmlDoc = new MasterPlexQTReportMatcherItem("xmlDoc");
                var NewXmlDocument = new MasterPlexQTReportMatcherItem("NewXmlDocument");
                var WS = new MasterPlexQTReportMatcherItem("WS");
                var readout = new MasterPlexQTReportMatcherItem("readout");
                var EOF = new MasterPlexQTReportMatcherItem("EOF");
                _disj_0_ = _ACTION(_AND(_VAR(_REF(NewXmlDocument, this), xmlDoc), _REF(WS, this), _LITERAL("MasterPlex QT Report By Analyte"), _REF(WS, this), _VAR(_CALL(Headers, new List<MatchItem> { xmlDoc }), readout), _CALL(Test, new List<MatchItem> { readout }), _REF(EOF, this)), (_IM_Result_MI_) => {{ 
#line 15 "MasterPlexQTReport.ironmeta"
     { return xmlDoc; }
#line default
}});
            }

            _Report_Body_ = _disj_0_;

            foreach (var _res_ in _Report_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> Headers(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _Headers_Body_ = null;

            Combinator _disj_0_ = null;
            {
                var xmlDoc = new MasterPlexQTReportMatcherItem("xmlDoc");
                var readout = new MasterPlexQTReportMatcherItem("readout");
                var Date = new MasterPlexQTReportMatcherItem("Date");
                var Time = new MasterPlexQTReportMatcherItem("Time");
                var Line = new MasterPlexQTReportMatcherItem("Line");
                _disj_0_ = _ARGS(_VAR(_ANY(), xmlDoc), _args, _ACTION(_AND(_VAR(_CALL(NewXmlDocElement, new List<MatchItem> { xmlDoc, new MatchItem("readout", CONV) }), readout), _CALL(Header, new List<MatchItem> { new MatchItem("Report Date", CONV), new MatchItem("reportDate", CONV), Date, readout }), _CALL(Header, new List<MatchItem> { new MatchItem("Run Date", CONV), new MatchItem("runDate", CONV), Date, readout }), _CALL(Header, new List<MatchItem> { new MatchItem("Report Time", CONV), new MatchItem("reportTime", CONV), Time, readout }), _CALL(Header, new List<MatchItem> { new MatchItem("Run Time", CONV), new MatchItem("runTime", CONV), Time, readout }), _CALL(Header, new List<MatchItem> { new MatchItem("Data File", CONV), new MatchItem("dataFile", CONV), Line, readout }), _CALL(Header, new List<MatchItem> { new MatchItem("Hardware Serial No.", CONV), new MatchItem("serialNo", CONV), Line, readout }), _CALL(Header, new List<MatchItem> { new MatchItem("Plate Name", CONV), new MatchItem("plateName", CONV), Line, readout }), _CALL(Header, new List<MatchItem> { new MatchItem("Operator", CONV), new MatchItem("operator", CONV), new MatchItem(Operator), readout }), _CALL(Header, new List<MatchItem> { new MatchItem("Version", CONV), new MatchItem("version", CONV), Line, readout }), _CALL(Header, new List<MatchItem> { new MatchItem("Analyst", CONV), new MatchItem("analyst", CONV), Line, readout }), _CALL(Header, new List<MatchItem> { new MatchItem("Background", CONV), new MatchItem("background", CONV), Line, readout })), (_IM_Result_MI_) => {{ 
#line 30 "MasterPlexQTReport.ironmeta"
     { return readout; }
#line default
}}));
            }

            _Headers_Body_ = _disj_0_;

            foreach (var _res_ in _Headers_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> Test(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _Test_Body_ = null;

            Combinator _disj_0_ = null;
            {
                var readout = new MasterPlexQTReportMatcherItem("readout");
                var test = new MasterPlexQTReportMatcherItem("test");
                var Line = new MasterPlexQTReportMatcherItem("Line");
                _disj_0_ = _ARGS(_VAR(_ANY(), readout), _args, _AND(_VAR(_CALL(NewXmlElement, new List<MatchItem> { readout, new MatchItem("test", CONV) }), test), _CALL(Header, new List<MatchItem> { new MatchItem("Analyte Name", CONV), new MatchItem("name", CONV), Line, test }), _CALL(DataHeading), _PLUS(_CALL(Measurement, new List<MatchItem> { test }))));
            }

            _Test_Body_ = _disj_0_;

            foreach (var _res_ in _Test_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> DataHeading(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _DataHeading_Body_ = null;

            Combinator _disj_0_ = null;
            {
                var WS = new MasterPlexQTReportMatcherItem("WS");
                _disj_0_ = _AND(_LITERAL("Well"), _REF(WS, this), _LITERAL("Sample Name"), _REF(WS, this), _LITERAL("MFI"), _REF(WS, this), _LITERAL("Concentration"), _REF(WS, this), _LITERAL("Unit"), _REF(WS, this), _LITERAL("Count"), _REF(WS, this));
            }

            _DataHeading_Body_ = _disj_0_;

            foreach (var _res_ in _DataHeading_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> Measurement(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _Measurement_Body_ = null;

            Combinator _disj_0_ = null;
            {
                var test = new MasterPlexQTReportMatcherItem("test");
                var well = new MasterPlexQTReportMatcherItem("well");
                var Number = new MasterPlexQTReportMatcherItem("Number");
                var WS = new MasterPlexQTReportMatcherItem("WS");
                var sampleName = new MasterPlexQTReportMatcherItem("sampleName");
                var mfi = new MasterPlexQTReportMatcherItem("mfi");
                var concentration = new MasterPlexQTReportMatcherItem("concentration");
                var unit = new MasterPlexQTReportMatcherItem("unit");
                var Token = new MasterPlexQTReportMatcherItem("Token");
                var count = new MasterPlexQTReportMatcherItem("count");
                _disj_0_ = _ARGS(_VAR(_ANY(), test), _args, _ACTION(_AND(_VAR(_REF(Number, this), well), _REF(WS, this), _VAR(_CALL(SampleName), sampleName), _REF(WS, this), _VAR(_REF(Number, this), mfi), _REF(WS, this), _VAR(_REF(Number, this), concentration), _REF(WS, this), _VAR(_REF(Token, this), unit), _REF(WS, this), _VAR(_REF(Number, this), count), _REF(WS, this)), (_IM_Result_MI_) => {{ 
#line 41 "MasterPlexQTReport.ironmeta"
     {
				XmlElement elem = (XmlElement)test;
				XmlElement row = elem.OwnerDocument.CreateElement("measurement");
				elem.AppendChild(row);
				
				row.SetAttribute("well", _IM_GetText(well));
				row.SetAttribute("sample-name", _IM_GetText(sampleName));
				row.SetAttribute("mfi", _IM_GetText(mfi));
				row.SetAttribute("concentration", _IM_GetText(concentration));
				row.SetAttribute("unit", _IM_GetText(unit));
				row.SetAttribute("count", _IM_GetText(count));
			}
#line default
} return default(XmlNode);}));
            }

            _Measurement_Body_ = _disj_0_;

            foreach (var _res_ in _Measurement_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        private int _Operator_Body__Index_ = -1;

        protected virtual IEnumerable<MatchItem> Operator(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _Operator_Body_ = null;

            if (_Operator_Body__Index_ == -1 || CachedCombinators[_Operator_Body__Index_] == null)
            {
                if (_Operator_Body__Index_ == -1)
                {
                    _Operator_Body__Index_ = CachedCombinators.Count;
                    CachedCombinators.Add(null);
                }

                Combinator _disj_0_ = null;
                {
                    _disj_0_ = _PLUS(_AND(_NOT(_LITERAL("Version")), _ANY()));
                }

                CachedCombinators[_Operator_Body__Index_] = _disj_0_;
            }

            _Operator_Body_ = CachedCombinators[_Operator_Body__Index_];


            foreach (var _res_ in _Operator_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> SampleName(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _SampleName_Body_ = null;

            Combinator _disj_0_ = null;
            {
                var Token = new MasterPlexQTReportMatcherItem("Token");
                var WS = new MasterPlexQTReportMatcherItem("WS");
                var Number = new MasterPlexQTReportMatcherItem("Number");
                _disj_0_ = _AND(_REF(Token, this), _QUES(_AND(_REF(WS, this), _LITERAL("("), _REF(Number, this), _LITERAL(")"))));
            }

            _SampleName_Body_ = _disj_0_;

            foreach (var _res_ in _SampleName_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }


    } // class MasterPlexQTReportMatcher
} // namespace MasterPlexQTReport
