// IronMeta Copyright © Gordon Tisher 2019

using System;
using System.Linq;

#if __MonoCS__
using NUnit.Framework;
using TestClassAttribute = NUnit.Framework.TestFixtureAttribute;
using TestInitializeAttribute = NUnit.Framework.TestFixtureSetUpAttribute;
using TestCleanupAttribute = NUnit.Framework.TestFixtureTearDownAttribute;
using TestMethodAttribute = NUnit.Framework.TestAttribute;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

using IronMeta.Generator;

namespace IronMeta.UnitTests.Matcher
{
    [TestClass]
    public class TestLineNumbers
    {

        const string SOURCE1 = 
@"
//////////////////
// line 3; comment
// line 4; comment

ironmeta Test<char, int> : IronMeta.Matcher.Matcher<char, int>
{
    Rule = 'a'; 
    8238gb jd uuuuuu34u4u
}

";

        IronMeta.Generator.Parser parser;

        [TestInitialize]
        public void TestInitialize()
        {
            parser = new IronMeta.Generator.Parser();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            parser = null;
        }

        [TestMethod]
        public void TestLineNumbers_BeginPositions()
        {
            var source = SOURCE1.Replace("\r\n", "\n").Replace("\n", "\r\n");
            var match = parser.GetMatch(source, parser.IronMetaFile);
            var lineBegins = match.MatchState.Positions.OrderBy(n => n).ToArray();

            Assert.AreEqual(2, lineBegins[0], "line 2: 2");
            Assert.AreEqual(22, lineBegins[1], "line 3: 22");
            Assert.AreEqual(42, lineBegins[2], "line 4: 42");
            Assert.AreEqual(62, lineBegins[3], "line 5: 62");
            Assert.AreEqual(64, lineBegins[4], "line 6: 64");
            Assert.AreEqual(128, lineBegins[5], "lin 7: 128");
            Assert.AreEqual(131, lineBegins[6], "line 8: 131");
            Assert.AreEqual(149, lineBegins[7], "line 9: 149");
        }

        [TestMethod]
        public void TestLineNumbers_ErrorLineAndOffset()
        {
            var match = parser.GetMatch(SOURCE1, parser.IronMetaFile);
            Assert.IsFalse(match.Success, "match should fail");

            int num, offset;
            var line = match.MatchState.GetLine(match.ErrorIndex, out num, out offset);
            Assert.AreEqual(9, num, "wrong line number");
            Assert.AreEqual(4, offset, "wrong offset");

            Assert.AreEqual("    8238gb jd uuuuuu34u4u", line);
        }

        const string SOURCE2 = @"// IronMeta Copyright © Gordon Tisher 2019

using IronMeta;

    blargh

ironmeta Parser<char, AST.Node> : IronMeta.Matcher.CharMatcher<AST.Node>
{

    Rule = 'a';

}";

        [TestMethod]
        public void TestLineNumbers_LongCommentBlock()
        {
            var match = parser.GetMatch(SOURCE2, parser.IronMetaFile);
            Assert.IsFalse(match.Success, "match should fail");

            int num, offset;
            match.MatchState.GetLine(match.ErrorIndex, out num, out offset);

            Assert.AreEqual(5, num, "line number should be 5");
            Assert.AreEqual(4, offset, "offset should be 4");
        }
    }
}
