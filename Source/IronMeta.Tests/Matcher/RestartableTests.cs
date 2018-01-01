using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IronMeta.UnitTests.Matcher
{
    [TestClass]
    public class RestartableTests
    {
        [TestMethod]
        public void TestRestart()
        {
            var parser = new Restartable();

            var match1 = parser.GetMatch("abzdef", parser.Rule1);
            Assert.IsFalse(match1.Success);

            var match2 = parser.GetMatch(match1.MatchState, parser.Rule2, 3);
            Assert.IsTrue(match2.Success);
        }
    }
}
