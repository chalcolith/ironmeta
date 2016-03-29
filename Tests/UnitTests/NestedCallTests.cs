using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IronMeta.UnitTests
{
    [TestClass]
    public class NestedCallTests
    {
        NestedCall parser = new NestedCall();

        [TestMethod]
        public void TestNestedCall1()
        {
            var m = parser.GetMatch("onea", parser.A);
            Assert.IsTrue(m.Success);
            Assert.AreEqual(1, m.Result);

            m = parser.GetMatch("twoba", parser.A);
            Assert.IsFalse(m.Success);
        }

        [TestMethod]
        public void TestNestedCall2()
        {
            var m = parser.GetMatch("twoba", parser.B);
            Assert.IsTrue(m.Success);
            Assert.AreEqual(2, m.Result);
        }

        [TestMethod]
        public void TestNestedCall3()
        {
            var m = parser.GetMatch("oneca", parser.D);
            Assert.IsTrue(m.Success);
        }

        [TestMethod]
        public void TestNestedCall4()
        {
            var m = parser.GetMatch("efa", parser.E);
            Assert.IsTrue(m.Success);
        }

        [TestMethod]
        public void TestNestedCall5()
        {
            var m = parser.GetMatch("aca", parser.F);
            Assert.IsTrue(m.Success);
        }

        [TestMethod]
        public void TestNestedCall6()
        {
            var m = parser.GetMatch("baca", parser.G);
        }

        [TestMethod]
        public void TestNestedCall7()
        {
            var m = parser.GetMatch("iaca", parser.I);
        }
    }
}
