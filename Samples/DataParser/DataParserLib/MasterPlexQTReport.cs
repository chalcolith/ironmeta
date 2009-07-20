// IronMeta Generated MasterPlexQTReport: 7/20/2009 9:22:10 PM UTC

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
                var WSL = new MasterPlexQTReportMatcherItem("WSL");
                var readout = new MasterPlexQTReportMatcherItem("readout");
                var EOF = new MasterPlexQTReportMatcherItem("EOF");
                _disj_0_ = _ACTION(_AND(_VAR(_REF(NewXmlDocument, this), xmlDoc), _REF(WSL, this), _LITERAL("MasterPlex QT Report By Analyte"), _REF(WSL, this), _VAR(_CALL(Headers, new List<MatchItem> { xmlDoc }), readout), _PLUS(_CALL(Test, new List<MatchItem> { readout })), _REF(EOF, this)), (_IM_Result_MI_) => {{ 
#line 16 "MasterPlexQTReport.ironmeta"
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
                var Token = new MasterPlexQTReportMatcherItem("Token");
                _disj_0_ = _ARGS(_VAR(_ANY(), xmlDoc), _args, _ACTION(_AND(_VAR(_CALL(NewXmlDocElement, new List<MatchItem> { xmlDoc, new MatchItem("readout", CONV) }), readout), _CALL(Header, new List<MatchItem> { new MatchItem("Report Date", CONV), new MatchItem("reportDate", CONV), Date, readout }), _CALL(Header, new List<MatchItem> { new MatchItem("Run Date", CONV), new MatchItem("runDate", CONV), Date, readout }), _CALL(Header, new List<MatchItem> { new MatchItem("Report Time", CONV), new MatchItem("reportTime", CONV), Time, readout }), _CALL(Header, new List<MatchItem> { new MatchItem("Run Time", CONV), new MatchItem("runTime", CONV), Time, readout }), _CALL(Header, new List<MatchItem> { new MatchItem("Data File", CONV), new MatchItem("dataFile", CONV), Token, readout }), _CALL(Header, new List<MatchItem> { new MatchItem("Hardware Serial No.", CONV), new MatchItem("serialNo", CONV), Token, readout }), _CALL(Header, new List<MatchItem> { new MatchItem("Plate Name", CONV), new MatchItem("plateName", CONV), Token, readout }), _CALL(Header, new List<MatchItem> { new MatchItem("Operator", CONV), new MatchItem("operator", CONV), Token, readout }), _CALL(Header, new List<MatchItem> { new MatchItem("MasterPlex QT Version", CONV), new MatchItem("version", CONV), new MatchItem(Version), readout }), _CALL(Header, new List<MatchItem> { new MatchItem("Analyst", CONV), new MatchItem("analyst", CONV), Token, readout })), (_IM_Result_MI_) => {{ 
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

        protected virtual IEnumerable<MatchItem> Version(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _Version_Body_ = null;

            Combinator _disj_0_ = null;
            {
                var Digit = new MasterPlexQTReportMatcherItem("Digit");
                var WS = new MasterPlexQTReportMatcherItem("WS");
                var Token = new MasterPlexQTReportMatcherItem("Token");
                _disj_0_ = _AND(_PLUS(_REF(Digit, this)), _STAR(_AND(_LITERAL("."), _PLUS(_REF(Digit, this)))), _REF(WS, this), _REF(Token, this));
            }

            _Version_Body_ = _disj_0_;

            foreach (var _res_ in _Version_Body_.Match(_indent+1, _inputs, _index, null, _memo))
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
                _disj_0_ = _ARGS(_VAR(_ANY(), readout), _args, _AND(_VAR(_CALL(NewXmlElement, new List<MatchItem> { readout, new MatchItem("test", CONV) }), test), _OR(_AND(_CALL(AnalyteHeader, new List<MatchItem> { test }), _CALL(BackgroundHeader, new List<MatchItem> { test })), _AND(_CALL(BackgroundHeader, new List<MatchItem> { test }), _CALL(AnalyteHeader, new List<MatchItem> { test }))), _CALL(DataHeading), _PLUS(_AND(_CALL(Measurement, new List<MatchItem> { test }), _QUES(_AND(_CALL(PageBreak), _QUES(_CALL(PageHeader))))))));
            }

            _Test_Body_ = _disj_0_;

            foreach (var _res_ in _Test_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> AnalyteHeader(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _AnalyteHeader_Body_ = null;

            Combinator _disj_0_ = null;
            {
                var test = new MasterPlexQTReportMatcherItem("test");
                var Token = new MasterPlexQTReportMatcherItem("Token");
                _disj_0_ = _ARGS(_VAR(_ANY(), test), _args, _CALL(Header, new List<MatchItem> { new MatchItem("Analyte Name", CONV), new MatchItem("name", CONV), Token, test }));
            }

            _AnalyteHeader_Body_ = _disj_0_;

            foreach (var _res_ in _AnalyteHeader_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> BackgroundHeader(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _BackgroundHeader_Body_ = null;

            Combinator _disj_0_ = null;
            {
                var test = new MasterPlexQTReportMatcherItem("test");
                var Token = new MasterPlexQTReportMatcherItem("Token");
                _disj_0_ = _ARGS(_VAR(_ANY(), test), _args, _CALL(Header, new List<MatchItem> { new MatchItem("Background", CONV), new MatchItem("background", CONV), Token, test }));
            }

            _BackgroundHeader_Body_ = _disj_0_;

            foreach (var _res_ in _BackgroundHeader_Body_.Match(_indent+1, _inputs, _index, null, _memo))
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
                var WSL = new MasterPlexQTReportMatcherItem("WSL");
                _disj_0_ = _AND(_LITERAL("Well"), _REF(WS, this), _LITERAL("Sample Name"), _REF(WS, this), _LITERAL("MFI"), _REF(WS, this), _LITERAL("Concentration"), _REF(WS, this), _LITERAL("Unit"), _REF(WS, this), _LITERAL("Count"), _REF(WSL, this));
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
                var Token = new MasterPlexQTReportMatcherItem("Token");
                var WS = new MasterPlexQTReportMatcherItem("WS");
                var sampleName = new MasterPlexQTReportMatcherItem("sampleName");
                var mfi = new MasterPlexQTReportMatcherItem("mfi");
                var concentration = new MasterPlexQTReportMatcherItem("concentration");
                var unit = new MasterPlexQTReportMatcherItem("unit");
                var count = new MasterPlexQTReportMatcherItem("count");
                var WSL = new MasterPlexQTReportMatcherItem("WSL");
                _disj_0_ = _ARGS(_VAR(_ANY(), test), _args, _ACTION(_AND(_VAR(_REF(Token, this), well), _REF(WS, this), _VAR(_CALL(SampleName), sampleName), _REF(WS, this), _VAR(_REF(Token, this), mfi), _REF(WS, this), _VAR(_REF(Token, this), concentration), _REF(WS, this), _VAR(_REF(Token, this), unit), _REF(WS, this), _VAR(_REF(Token, this), count), _REF(WSL, this)), (_IM_Result_MI_) => {{ 
#line 47 "MasterPlexQTReport.ironmeta"
     {
				//Console.WriteLine("Measurement {0}", _IM_GetText(well));
		
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

        protected virtual IEnumerable<MatchItem> PageBreak(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _PageBreak_Body_ = null;

            Combinator _disj_0_ = null;
            {
                var Token = new MasterPlexQTReportMatcherItem("Token");
                var WS = new MasterPlexQTReportMatcherItem("WS");
                var Number = new MasterPlexQTReportMatcherItem("Number");
                var RestOfLine = new MasterPlexQTReportMatcherItem("RestOfLine");
                var WSL = new MasterPlexQTReportMatcherItem("WSL");
                _disj_0_ = _AND(_REF(Token, this), _REF(WS, this), _LITERAL("Page"), _REF(WS, this), _LITERAL("-"), _REF(WS, this), _REF(Number, this), _REF(WS, this), _LITERAL("of"), _REF(WS, this), _REF(Number, this), _REF(WS, this), _REF(RestOfLine, this), _REF(WSL, this));
            }

            _PageBreak_Body_ = _disj_0_;

            foreach (var _res_ in _PageBreak_Body_.Match(_indent+1, _inputs, _index, null, _memo))
            {
                yield return _res_;

                if (StrictPEG) yield break;
            }
        }

        protected virtual IEnumerable<MatchItem> PageHeader(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _PageHeader_Body_ = null;

            Combinator _disj_0_ = null;
            {
                var Date = new MasterPlexQTReportMatcherItem("Date");
                var WS = new MasterPlexQTReportMatcherItem("WS");
                var Time = new MasterPlexQTReportMatcherItem("Time");
                var WSL = new MasterPlexQTReportMatcherItem("WSL");
                _disj_0_ = _AND(_REF(Date, this), _REF(WS, this), _LITERAL("Report By Analytes"), _REF(WS, this), _REF(Time, this), _REF(WSL, this), _CALL(DataHeading));
            }

            _PageHeader_Body_ = _disj_0_;

            foreach (var _res_ in _PageHeader_Body_.Match(_indent+1, _inputs, _index, null, _memo))
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
