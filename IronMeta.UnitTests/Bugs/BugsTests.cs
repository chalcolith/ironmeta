using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xunit;

namespace IronMeta.UnitTests.Bugs
{
    
    public class BugsTests
    {

        [Fact]
        public void Bug_009()
        {
            var matcher = new Bugs();
            var match = matcher.GetMatch("#\\x000", matcher.Bug_009_HexEscapeCharacter);
            Assert.True(match.Success);
            
            var chars = match.Result as IEnumerable<char>;
            Assert.NotNull(chars);

            Assert.Equal('0', chars.ElementAt(0));
            Assert.Equal('0', chars.ElementAt(1));
            Assert.Equal('0', chars.ElementAt(2));

            char[] copy = new char[3];
            int i = 0;
            foreach (var ch in chars)
                copy[i++] = ch;

            Assert.Equal('0', copy[0]);
            Assert.Equal('0', copy[1]);
            Assert.Equal('0', copy[2]);
        }

    }

}
