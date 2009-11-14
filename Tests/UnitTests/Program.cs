using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace UnitTests
{

    class Program
    {

        static void RunTests(object tests)
        {
            var methods = tests.GetType().GetMethods();
            foreach (MethodInfo method in methods)
            {
                foreach (object att in method.GetCustomAttributes(false))
                {
                    if (att is Xunit.FactAttribute)
                    {
                        method.Invoke(tests, null);
                        break;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            IronMeta.Matcher.MatcherTests matcherTests = new IronMeta.Matcher.MatcherTests();
            RunTests(matcherTests);

            IronMeta.Tests ironMetaTests = new IronMeta.Tests();
            RunTests(ironMetaTests);

            Calc.Tests calcTests = new Calc.Tests();
            RunTests(calcTests);
        }

    }

}
