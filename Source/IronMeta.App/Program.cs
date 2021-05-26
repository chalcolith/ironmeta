// IronMeta Copyright © Gordon Tisher

using System;
using System.Diagnostics;
using System.IO;
using IronMeta.Generator;

namespace IronMeta
{
    class Program
    {
        static int Main(string[] args)
        {
            const string message = "IronMeta.App -n {0} -o {1} {2}: {3}";

            try
            {
                if (args.Length == 1 && args[0].Equals("--version", StringComparison.InvariantCultureIgnoreCase))
                {
                    Console.WriteLine("IronMeta.App {0}", typeof(CSharpGen).Assembly.GetName().Version.ToString());
                    return 0;
                }

                var options = Options.Parse(args);

                var inputInfo = new FileInfo(options.InputFile);
                var outputInfo = new FileInfo(options.OutputFile);

                if (outputInfo.Exists && outputInfo.LastWriteTimeUtc > inputInfo.LastWriteTimeUtc && !options.Force)
                {
                    Console.WriteLine(string.Format(message, options.Namespace, outputInfo.FullName, inputInfo.FullName, "input is older than output; not generating"));
                    return 0;
                }
                else
                {
                    var stopwatch = new Stopwatch();
                    stopwatch.Start();
                    var match = CSharpShell.Process(inputInfo.FullName, outputInfo.FullName, options.Namespace, true);
                    stopwatch.Stop();

                    if (match.Success)
                    {
                        Console.WriteLine(string.Format(message, options.Namespace, outputInfo.FullName, inputInfo.FullName, stopwatch.Elapsed));
                        return 0;
                    }
                    else
                    {
                        int num, offset;
                        var line = match.MatchState.GetLine(match.MatchState.LastErrorIndex, out num, out offset);
                        Console.Error.WriteLine("{0}({1},{2}): error: {3}", inputInfo.FullName, num, offset, match.MatchState.LastError);
                        Console.Error.WriteLine("{0}", line);
                        Console.Error.WriteLine("{0}^", new string(' ', offset));
                        return 1;
                    }
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                if (e.InnerException != null)
                    Console.Error.WriteLine(e.InnerException.Message);

                return 2;
            }
        }
    }
}
