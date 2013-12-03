using System;

namespace IronMeta.Tests.Mono
{
	public class Tester
	{
		public static void Main(string[] args)
		{
			var anon = new Matcher.AnonObject.AnonObjectTests();
			anon.TestAnonObjectImplicit();
		}
	}
}

