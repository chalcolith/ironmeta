// IronMeta Copyright © Gordon Tisher 2018

using System;
using System.Linq;

using IronMeta.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IronMeta.UnitTests
{
    [TestClass]
    public class MemoizerTests
    {
        [TestMethod]
        public void TestSingleCall()
        {
            const int N = 10;

            int numCalls = 0;

            var seq = Enumerable.Range(0, N).Select(n => { numCalls++; return n; });
            var memo = new Memoizer<int>(seq);

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
