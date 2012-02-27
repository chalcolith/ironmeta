using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xunit;

namespace IronMeta.UnitTests.String
{

    public class StringParserTests
    {

        static readonly IEnumerable<string> StrList1 = new List<string> { "one" };
        static readonly IEnumerable<string> StrList2 = new List<string> { "two" };
        static readonly IEnumerable<string> StrListPi = new List<string> { "three", "point", "one", "four", "one", "five", "nine" };

        [Fact]
        public void TestStringOne()
        {
            var matcher = new StringParser();

            var match = matcher.GetMatch(StrList1, matcher.One);
            Assert.True(match.Success);
            Assert.Equal(1, match.Result);
        }

        [Fact]
        public void TestStringPi()
        {
            var matcher = new StringParser();

            var match = matcher.GetMatch(StrListPi, matcher.Pi);
            Assert.True(match.Success);
            Assert.Equal(314, match.Result);
        }

    }

}
