using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IronMeta.Tests.Matcher.AnonObject
{
    
    [TestClass]
    public class AnonObjectTests
    {

        static IEnumerable<InputObject> first = new List<InputObject>
            {
                new InputObject { Name = "actual", Value = "one" },
                new InputObject { Name = "actual", Value = "two" }
            };

        static IEnumerable<InputObject> second = new List<InputObject>
            {
                new InputObject { Name = "implicit", Value = "three" },
                new InputObject { Name = "implicit", Value = "four" }
            };


        [TestMethod]
        public void TestAnonObjectActual()
        {
            var matcher = new AnonObjectParser();
            var match = matcher.GetMatch(first, matcher.ActualObject);
            Assert.IsTrue(match.Success);

            match = matcher.GetMatch(second, matcher.ActualObject);
            Assert.IsFalse(match.Success);
        }

        [TestMethod]
        public void TestAnonObjectImplicit()
        {
            var matcher = new AnonObjectParser();
            var match = matcher.GetMatch(second, matcher.ImplicitObject);
            Assert.IsTrue(match.Success);

            match = matcher.GetMatch(first, matcher.ImplicitObject);
            Assert.IsFalse(match.Success);
        }

    }

}
