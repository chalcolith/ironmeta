using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IronMeta.Tests.Calc
{
    [TestClass]
    public class TestCalc
    {
        [TestMethod]
        public void TestCalcMultiplicative()
        {
            var parser = new IronMeta.Calc.Calc();
            var match = parser.GetMatch("2 * 7", parser.Expression);
            Assert.IsTrue(match.Success);
            Assert.AreEqual(14, match.Result);
        }
    }
}
