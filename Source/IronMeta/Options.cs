using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Fclp;

namespace IronMeta
{
    class Options
    {
        public bool Force { get; set; }
        public string Namespace { get; set; }
        public string OutputFile { get; set; }
        public string InputFile { get; set; }

        public static Options Parse(string[] args)
        {
            var result = parser.Parse(args);
            if (result.HasErrors)
            {
                throw new OptionsException(result);
            }
            else
            {
                var options = parser.Object;

                // check for input file
                options.InputFile = Path.GetFullPath(options.InputFile);
                if (!File.Exists(options.InputFile))
                    throw new OptionsException("File not found: " + options.InputFile);

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
                        throw new OptionsException("Invalid output file name " + options.OutputFile);
                }

                return options;
            }
        }

        static readonly FluentCommandLineParser<Options> parser = new FluentCommandLineParser<Options>();
        static string helpText;

        static Options()
        {
            parser.Setup(o => o.Force)
                .As('f', "force")
                .SetDefault(false)
                .WithDescription("Force IronMeta to generate a new parser even if the input file has not changed.");

            parser.Setup(o => o.Namespace)
                .As('n', "namespace")
                .SetDefault(null)
                .WithDescription("The namespace for the generated parser.  Defaults to the name of the current directory.");

            parser.Setup(o => o.OutputFile)
                .As('o', "output")
                .SetDefault(null)
                .WithDescription("The filename of the generated parser.  Defaults to adding .g.cs to the name of the input file.");

            parser.Setup(o => o.InputFile)
                .As('i', "input")
                .Required()
                .WithDescription("The filename of the input grammar.");

            parser.SetupHelp("?", "h", "help")
                .Callback(s => helpText = s);
        }

        class OptionsException : Exception
        {
            public OptionsException(string message)
                : base(message)
            {
            }

            public OptionsException(ICommandLineParserResult result)
                : base(GetErrorText(result))
            {
            }

            static string GetHelpText()
            {
                parser.HelpOption.ShowHelp(parser.Options);
                return helpText;
            }

            static string GetErrorText(ICommandLineParserResult result)
            {
                var sb = new StringBuilder();
                sb.AppendLine("Invalid command-line options:");
                sb.AppendLine(result.ErrorText);
                sb.AppendLine();

                parser.HelpOption.ShowHelp(parser.Options);
                sb.AppendLine("Usage:");
                sb.AppendLine(helpText);
                sb.AppendLine();

                return sb.ToString();
            }
        }
    }
}
