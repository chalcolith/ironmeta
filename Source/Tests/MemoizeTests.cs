using System;
using System.Linq;
using IronMeta.Matcher;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IronMeta.Tests
{
    [TestClass]
    public class MemoizeTests
    {
        [TestMethod]
        public void TestSingleCall()
        {
            const int N = 10;

            int numCalls = 0;

            var seq = Enumerable.Range(0, N).Select(n => { numCalls++; return n; });
            var memo = new MemoizingEnumerable<int>(seq);

            int i = 0;
            foreach (var n in memo)
            {
                Assert.AreEqual(i++, n);
            }
            Assert.AreEqual(N, i);

            for (i = 0; i < N; i++)
            {
                Assert.AreEqual(i, memo[i]);
            }
            Assert.AreEqual(N, numCalls);
        }
    }
}
