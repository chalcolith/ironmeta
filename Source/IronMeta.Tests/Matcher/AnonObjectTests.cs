// IronMeta Copyright © The IronMeta Developers

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if __MonoCS__
using NUnit.Framework;
using TestClassAttribute = NUnit.Framework.TestFixtureAttribute;
using TestMethodAttribute = NUnit.Framework.TestAttribute;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace IronMeta.UnitTests.Matcher
{
    [TestClass]
    public class AnonObjectTests
    {

        static IEnumerable<AnonInputObject> first = new List<AnonInputObject>
            {
                new AnonInputObject { Name = "actual", Value = "one" },
                new AnonInputObject { Name = "actual", Value = "two" }
            };

        static IEnumerable<AnonInputObject> second = new List<AnonInputObject>
            {
                new AnonInputObject { Name = "implicit", Value = "three" },
                new AnonInputObject { Name = "implicit", Value = "four" }
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
