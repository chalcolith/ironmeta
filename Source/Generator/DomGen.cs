using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronMeta.Generator
{
    partial class DomGen : Matcher.Matcher<AST.Node, CodeObject>, IGenerator
    {
        #region IGenerator Members

        public void Generate(TextWriter sb)
        {
            throw new NotImplementedException();
        }

        #endregion

        IEnumerable<AST.Node> Flatten(AST.Node node)
        {
            yield return node;
            foreach (var child in node.Children)
                foreach (var sub in Flatten(child))
                    yield return sub;
        }
    }
}
