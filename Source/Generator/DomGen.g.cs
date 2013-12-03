//
// IronMeta DomGen Parser; Generated 2013-08-21 05:00:37Z UTC
//

using System;
using System.Collections.Generic;
using System.Linq;
using IronMeta.Matcher;
using System.CodeDom;

#pragma warning disable 0219
#pragma warning disable 1591

namespace IronMeta.Generator
{

    using _DomGen_Inputs = IEnumerable<AST.Node>;
    using _DomGen_Results = IEnumerable<CodeObject>;
    using _DomGen_Item = IronMeta.Matcher.MatchItem<AST.Node, CodeObject>;
    using _DomGen_Args = IEnumerable<IronMeta.Matcher.MatchItem<AST.Node, CodeObject>>;
    using _DomGen_Memo = Memo<AST.Node, CodeObject>;
    using _DomGen_Rule = System.Action<Memo<AST.Node, CodeObject>, int, IEnumerable<IronMeta.Matcher.MatchItem<AST.Node, CodeObject>>>;
    using _DomGen_Base = IronMeta.Matcher.Matcher<AST.Node, CodeObject>;

    public partial class DomGen : Matcher<AST.Node, CodeObject>
    {
        public DomGen()
            : base()
        { }

        public DomGen(bool handle_left_recursion)
            : base(handle_left_recursion)
        { }
    } // class DomGen

} // namespace IronMeta.Generator

