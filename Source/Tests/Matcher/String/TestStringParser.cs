//////////////////////////////////////////////////////////////////////
//
// Copyright © 2013 Verophyle Informatics
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without 
// modification, are permitted provided that the following conditions 
// are met:
// 
//     * Redistributions of source code must retain the above 
//       copyright notice, this list of conditions and the following 
//       disclaimer.
//     * Redistributions in binary form must reproduce the above 
//       copyright notice, this list of conditions and the following 
//       disclaimer in the documentation and/or other materials 
//       provided with the distribution.
//     * Neither the name of the IronMeta Project nor the names of its 
//       contributors may be used to endorse or promote products 
//       derived from this software without specific prior written 
//       permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS 
// "AS IS" AND  ANY EXPRESS OR  IMPLIED WARRANTIES, INCLUDING, BUT NOT 
// LIMITED TO, THE  IMPLIED WARRANTIES OF  MERCHANTABILITY AND FITNESS 
// FOR  A  PARTICULAR  PURPOSE  ARE DISCLAIMED. IN  NO EVENT SHALL THE 
// COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, 
// INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, 
// BUT NOT  LIMITED TO, PROCUREMENT  OF SUBSTITUTE  GOODS  OR SERVICES; 
// LOSS OF USE, DATA, OR  PROFITS; OR  BUSINESS  INTERRUPTION) HOWEVER 
// CAUSED AND ON ANY THEORY OF  LIABILITY, WHETHER IN CONTRACT, STRICT 
// LIABILITY, OR  TORT (INCLUDING NEGLIGENCE  OR OTHERWISE) ARISING IN 
// ANY WAY OUT  OF THE  USE OF THIS SOFTWARE, EVEN  IF ADVISED  OF THE 
// POSSIBILITY OF SUCH DAMAGE.
//
//////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IronMeta.Tests.Matcher.String
{
    [TestClass]
    public class StringParserTests
    {
        static readonly IEnumerable<string> StrList1 = new List<string> { "one" };
        static readonly IEnumerable<string> StrList2 = new List<string> { "two" };
        static readonly IEnumerable<string> StrListPi = new List<string> { "three", "point", "one", "four", "one", "five", "nine" };

        [TestMethod]
        public void TestStringOne()
        {
            var matcher = new StringParser();

            var match = matcher.GetMatch(StrList1, matcher.One);
            Assert.IsTrue(match.Success);
            Assert.AreEqual(1, match.Result);
        }

        [TestMethod]
        public void TestStringPi()
        {
            var matcher = new StringParser();

            var match = matcher.GetMatch(StrListPi, matcher.Pi);
            Assert.IsTrue(match.Success);
            Assert.AreEqual(314, match.Result);
        }

    }

}
