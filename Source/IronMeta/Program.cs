//////////////////////////////////////////////////////////////////////
// Copyright © 2013 The IronMeta Project
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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

using IronMeta.Matcher;
using IronMeta.Generator;

namespace IronMeta
{

    /// <summary>
    /// Main program of the IronMeta generator.
    /// </summary>
    public class Program
    {

        /// <summary>
        /// Main function.
        /// </summary>
        /// <param name="args">Args.</param>
        static int Main(string[] args)
        {
            // get options
            string nameSpace = string.Empty;
            bool force = false;

            List<string> inputFiles = new List<string>();
            List<string> outputFiles = new List<string>();

            for (int i = 0; i < args.Length; ++i)
            {
                if (args[i].ToUpper().StartsWith("-H") || args[i].ToUpper().StartsWith("--H"))
                {
                    Console.WriteLine("usage: IronMeta.Generator [-f|--force] [-n|--namespace Namespace] InputFile [-o|--output OutputFile] ...");
                    Console.WriteLine("         -f, --force:     Force generation even if the input file is older than the output.");
                    Console.WriteLine("         -n, --namespace: Set namespace (defaults to the current directory).");
                    Console.WriteLine("         -o, --output:    Specify output file name (defaults to adding \".g.cs\").");
                    Console.WriteLine();
                    return 3;
                }

                if ((args[i] == "-n" || args[i] == "--namespace") && i < args.Length - 1)
                {
                    nameSpace = args[++i];

                    // strip initial and final punctuation
                    while (nameSpace.Length > 0 && !char.IsLetter(nameSpace[0]))
                        nameSpace = nameSpace.Substring(1);
                    while (nameSpace.Length > 0 && !char.IsLetter(nameSpace[nameSpace.Length - 1]))
                        nameSpace = nameSpace.Substring(0, nameSpace.Length - 1);
                    nameSpace = nameSpace.Replace('\\', '.').Replace('/', '.');

                    continue;
                }

                if ((args[i] == "-f" || args[i] == "--force"))
                {
                    force = true;
                    continue;
                }

                if ((args[i] == "-o" || args[i] == "--output") && i < args.Length - 1)
                {
                    outputFiles.Add(args[++i]);
                    continue;
                }

                inputFiles.Add(args[i]);
            }

            if (outputFiles.Count > 0 && outputFiles.Count != inputFiles.Count)
            {
                Console.WriteLine("If you specify an output file, you must specify one for all inputs.");
                return 4;
            }

            // process files
            for (int i = 0; i < inputFiles.Count; ++i)
            {
                string inputFile = inputFiles[i];
                string inputName = inputFile.ToUpperInvariant().EndsWith(".IRONMETA") ? inputFile.Substring(0, inputFile.Length - 9) : inputFile;

                string outputFile = outputFiles.Count > 0 ? outputFiles[i] : inputName + ".g.cs";

                if (inputFile.ToUpperInvariant().EndsWith(".IRONMETA"))
                {
                    try
                    {
                        Stopwatch stopwatch = new Stopwatch();

                        stopwatch.Start();
                        var match = CSharpShell.Process(inputFile, outputFile, nameSpace, force);
                        stopwatch.Stop();

                        FileInfo inputInfo = new FileInfo(inputFile);
                        FileInfo outputInfo = new FileInfo(outputFile);
                        if (match != null)
                        {
                            if (match.Success)
                            {
                                Console.WriteLine("{0} -> {1}: {2}", inputInfo.Name, outputInfo, stopwatch.Elapsed);
                            }
                            else
                            {
                                int num, offset;
                                var line = CharMatcher<AST.Node>.GetLine(match.Memo, match.Memo.LastErrorIndex, out num, out offset);
                                Console.WriteLine("{0}({1},{2}): {3}", inputInfo.Name, num, offset, match.Memo.LastError);
                                Console.WriteLine(line.TrimEnd());
                                Console.WriteLine(new string(' ', offset) + '^');

                                return 1;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("{0}: Error: {1}", inputFile, e.Message);
                        return 2;
                    }
                }
            }

            return 0;
        } // Main()

    } // class Program

} // namespace IronMeta.Generator
