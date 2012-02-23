using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xunit;

namespace IronMeta.UnitTests.Combine
{
    
    public class CombineTests
    {

        [Fact]
        public void TestCombine()
        {
            var matcher = new Combine2();

            var match = matcher.GetMatch("ghi", matcher.Rule1);
            Assert.True(match.Success);
            Assert.Equal(3, match.Result);

            match = matcher.GetMatch("jkl", matcher.Rule2);
            Assert.True(match.Success);
            Assert.Equal(4, match.Result);

            match = matcher.GetMatch("abc", matcher.Rule5);
            Assert.True(match.Success);
            Assert.Equal(1, match.Result);

            match = matcher.GetMatch("def", matcher.Rule6);
            Assert.True(match.Success);
            Assert.Equal(2, match.Result);
        }

        [Fact]
        public void TestBase()
        {
            var matcher = new Combine1();

            var match = matcher.GetMatch("virtual", matcher.VirtualRule);
            Assert.True(match.Success);
            Assert.Equal(42, match.Result);
        }

        [Fact]
        public void TestOverride()
        {
            var matcher = new Combine2();

            var match = matcher.GetMatch("override", matcher.VirtualRule);
            Assert.True(match.Success);
            Assert.Equal(314, match.Result);
        }

    }

}
