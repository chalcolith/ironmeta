using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace AssemblyVersion
{
    class Program
    {

        static void Main(string[] args)
        {
            foreach (string fname in args)
            {
                Assembly asm = Assembly.LoadFile(fname);
                AssemblyName aname = new AssemblyName(asm.FullName);
                Console.WriteLine(aname.Version);
            }
        }

    }
}
