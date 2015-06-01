// IronMeta Copyright © Gordon Tisher 2015

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

namespace IronMeta.UnitTests
{    
    [TestClass]
    public class BugFixesTests
    {
        [TestMethod]
        public void TestBugFix009()
        {
            var matcher = new BugFixes();
            var match = matcher.GetMatch("#\\x000", matcher.Bug_3490042_HexEscapeCharacter);
            Assert.IsTrue(match.Success);
            
            var chars = match.Result as IEnumerable<char>;
            Assert.IsNotNull(chars);

            Assert.AreEqual('0', chars.ElementAt(0));
            Assert.AreEqual('0', chars.ElementAt(1));
            Assert.AreEqual('0', chars.ElementAt(2));

            char[] copy = new char[3];
            int i = 0;
            foreach (var ch in chars)
                copy[i++] = ch;

            Assert.AreEqual('0', copy[0]);
            Assert.AreEqual('0', copy[1]);
            Assert.AreEqual('0', copy[2]);
        }
    }
}
