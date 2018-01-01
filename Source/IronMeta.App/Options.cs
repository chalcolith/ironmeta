using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronMeta
{
    class Options
    {
        public bool Force { get; set; }
        public string Namespace { get; set; }
        public string OutputFile { get; set; }
        public string InputFile { get; set; }

        const string Usage = @"Usage: IronMeta [-f] [-n Custom.Namespace] [-o OutputFile.cs] IronMetaFile.ironmeta";

        static void FailWithUsage()
        {
            throw new Exception(Usage);
        }

        public static Options Parse(string[] args)
        {
            var options = new Options();

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-f")
                {
                    options.Force = true;
                }
                else if (args[i] == "-n")
                {
                    if (i + 1 >= args.Length || args[i + 1].StartsWith("-"))
                        FailWithUsage();
                    options.Namespace = args[i + 1];
                    i++;
                }
                else if (args[i] == "-o")
                {
                    if (i + 1 >= args.Length || args[i + 1].StartsWith("-"))
                        FailWithUsage();
                    options.OutputFile = args[i + 1];
                    i++;
                }
                else if (args[i] == "-h" || args[i] == "-?" || args[i] == "/?" || args[i] == "--help")
                {
                    FailWithUsage();
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(options.InputFile))
                        FailWithUsage();

                    options.InputFile = args[i];
                }
            }

            // check for input file
            options.InputFile = Path.GetFullPath(options.InputFile);
            if (!File.Exists(options.InputFile))
                throw new Exception("File not found: " + options.InputFile);

            // namespace
            if (string.IsNullOrWhiteSpace(options.Namespace))
            {
                var dir = Path.GetDirectoryName(options.InputFile);
                var idx = dir.LastIndexOf('\\');
                if (idx == -1) idx = dir.LastIndexOf('/');
                if (idx != -1)
                    dir = dir.Substring(idx + 1);
                options.Namespace = dir;
            }

            // output
            if (string.IsNullOrWhiteSpace(options.OutputFile))
            {
                const string sfx = ".ironmeta";

                var fname = options.InputFile;
                if (fname.EndsWith(sfx, StringComparison.InvariantCultureIgnoreCase))
                    fname = fname.Substring(0, fname.Length - sfx.Length);

                options.OutputFile = Path.GetFullPath(fname + ".g.cs");

                if (string.IsNullOrWhiteSpace(fname))
                    throw new Exception("Invalid output file name " + options.OutputFile);
            }

            return options;
        }
    }
}
