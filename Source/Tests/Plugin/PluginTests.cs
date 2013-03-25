using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IronMeta.Tests.Plugin
{
    [TestClass]
    public class PluginTests
    {
        [TestMethod]
        public void Test_001()
        {
            var testGrammar = new PluginTestGrammar();
            var match = testGrammar.GetMatch("3", testGrammar.Digit);

            Assert.AreEqual(3, match.Result);
        }
    }
}
