using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xunit;

namespace IronMeta.UnitTests.AnonObject
{
    
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


        [Fact]
        public void TestActual()
        {
            var matcher = new AnonObjectParser();
            var match = matcher.GetMatch(first, matcher.ActualObject);
            Assert.True(match.Success);

            match = matcher.GetMatch(second, matcher.ActualObject);
            Assert.False(match.Success);
        }

        [Fact]
        public void TestImplicit()
        {
            var matcher = new AnonObjectParser();
            var match = matcher.GetMatch(second, matcher.ImplicitObject);
            Assert.True(match.Success);

            match = matcher.GetMatch(first, matcher.ImplicitObject);
            Assert.False(match.Success);
        }

    }

}
