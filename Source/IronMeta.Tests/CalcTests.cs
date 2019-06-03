// IronMeta Copyright © Gordon Tisher 2019

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

        [TestMethod]
        public void TestCalcIncomplete_Issue_20()
        {
            var parser = new Samples.Calc.Calc();
            var match = parser.GetMatch("5+", parser.Expression);
            Assert.IsTrue(match.Success);
            Assert.AreEqual(1, match.NextIndex);
        }
    }
}
