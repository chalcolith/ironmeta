// IronMeta Copyright © Gordon Tisher 2015

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IronMeta.UnitTests
{
    [TestClass]
    public class TestCalc
    {
        [TestMethod]
        public void TestCalcMultiplicative()
        {
            var parser = new IronMeta.Samples.Calc.Calc();
            var match = parser.GetMatch("2 * 7", parser.Expression);
            Assert.IsTrue(match.Success);
            Assert.AreEqual(14, match.Result);
        }
    }
}
