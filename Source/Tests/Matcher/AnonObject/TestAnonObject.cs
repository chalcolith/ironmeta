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

#if __MonoCS__
using NUnit.Framework;
using TestClassAttribute = NUnit.Framework.TestFixtureAttribute;
using TestMethodAttribute = NUnit.Framework.TestAttribute;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

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
