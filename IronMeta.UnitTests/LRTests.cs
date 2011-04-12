using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xunit;

namespace IronMeta.UnitTests
{

    public class LRTests
    {

        [Fact]
        public void TestParseTree()
        {
            var parser = new LRParser();
            var match = parser.GetMatch("aaaa", parser.A);

            var res = match.Result;
        }

    }

}
