//////////////////////////////////////////////////////////////////////
// $Id$
//
// Copyright (C) 2009-2011, The IronMeta Project
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
using System.Reflection;

/// Unit tests for IronMeta.
namespace IronMeta.UnitTests
{

    /// <summary>
    /// The main program runs all the tests in order, without parallelizing.
    /// </summary>
    class Program
    {

        static void RunTests(object tests)
        {
            var methods = tests.GetType().GetMethods();
            foreach (MethodInfo method in methods)
            {
                try
                {
                    foreach (object att in method.GetCustomAttributes(false))
                    {
                        if (att is Xunit.FactAttribute)
                        {
                            Console.WriteLine(method.Name);
                            method.Invoke(tests, null);
                            break;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + (e.InnerException != null ? ": " + e.InnerException.Message : ""));
                }
            }
        }

        static void Main(string[] args)
        {
            MatcherTests matcherTests = new MatcherTests();
            RunTests(matcherTests);

            CalcTests calcTests = new CalcTests();
            RunTests(calcTests);

            LRTests lrTests = new LRTests();
            RunTests(lrTests);

            StringTests stringTests = new StringTests();
            RunTests(stringTests);
        }

    } // class Program

} // namespace IronMeta.UnitTests
