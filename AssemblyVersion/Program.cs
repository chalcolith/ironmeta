using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace AssemblyVersion
{
    class Program
    {

        static void Main(string[] args)
        {
            foreach (string fname in args)
            {
                FileInfo fi = new FileInfo(fname);
                Assembly asm = Assembly.LoadFile(fi.FullName);
                AssemblyName aname = new AssemblyName(asm.FullName);
                Console.WriteLine(aname.Version);
            }
        }

    }
}
