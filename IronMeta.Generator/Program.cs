//////////////////////////////////////////////////////////////////////
// $Id$
//
// Copyright (C) 2009-2010, The IronMeta Project
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
using System.IO;

using IronMeta.Matcher;

/// Tool that parses IronMeta parser files (using IronMeta), and generates a C# parser class.
namespace IronMeta.Generator
{

    using Result = MatchResult<char, AST.ASTNode<_Parser_Item>, _Parser_Item>;

    /// <summary>
    /// Main program of the IronMeta generator.
    /// </summary>
    public class Program
    {

        /// <summary>
        /// Generate a parser from an IronMeta grammar.
        /// </summary>
        /// <param name="input">Input stream.</param>
        /// <param name="output">Output stream.</param>
        /// <returns></returns>
        public Result Process(IEnumerable<char> input, TextWriter output, string nameSpace)
        {
            Parser parser = new Parser();
            Result match = parser.GetMatch(input, parser.IronMetaFile);

            if (match.Success)
            {
                CSharpGen csgen = new CSharpGen(match.Result, nameSpace);
                csgen.Generate(output);
            }

            return match;
        }

        /// <summary>
        /// Generate a parser from an IronMeta grammar.
        /// </summary>
        /// <param name="fname">Input filename.</param>
        /// <returns>Whether or not generation succeeded.</returns>
        public Result Process(string inputFname, string outputFname, string nameSpace, bool force)
        {
            if (string.IsNullOrEmpty(nameSpace))
            {
                FileInfo info = new FileInfo(inputFname);
                nameSpace = info.Directory.Name;
            }

            FileInfo srcInfo = new FileInfo(inputFname);
            if (!srcInfo.Exists)
                throw new Exception("File not found: " + inputFname);

            FileInfo destInfo = new FileInfo(outputFname);
            if (destInfo.Exists && !force)
            {
                if (srcInfo.LastWriteTimeUtc <= destInfo.LastWriteTimeUtc)
                {
                    Console.WriteLine("{0} unchanged; not generating.", inputFname);
                    return null;
                }
            }

            string contents;
            using (StreamReader sr = new StreamReader(inputFname))
            {
                contents = sr.ReadToEnd();
            }

            Result match = null;
            using (StringWriter sw = new StringWriter())
            {
                match = Process(contents, sw, nameSpace);

                if (match.Success)
                {
                    using (StreamWriter fw = new StreamWriter(outputFname))
                    {
                        fw.Write(sw.ToString());
                    }
                }
            }

            return match;
        }

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
                    Console.WriteLine("         -o, --output:    Specify output file name (defaults to adding \".g.cs\"");
                    Console.WriteLine();
                    return 3;
                }

                if ((args[i] == "-n" || args[i] == "--namespace") && i < args.Length - 1)
                {
                    nameSpace = args[++i];
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
            Program generator = new Program();

            for (int i = 0; i < inputFiles.Count; ++i)
            {
                string inputFile = inputFiles[i];
                string outputFile = outputFiles.Count > 0 ? outputFiles[i] : inputFile + ".g.cs";

                if (inputFile.ToUpper().EndsWith(".IRONMETA"))
                {
                    try
                    {
                        DateTime start = DateTime.Now;
                        Result match = generator.Process(inputFile, outputFile, nameSpace, force);
                        DateTime end = DateTime.Now;

                        FileInfo file = new FileInfo(inputFile);
                        if (match != null)
                        {
                            if (match.Success)
                            {
                                Console.WriteLine("{0}: {1}", file.Name, (end - start));
                            }
                            else
                            {
                                ErrorRec rec = match.Matcher.LastError;

                                var charMatcher = match.Matcher as CharMatcher<AST.ASTNode<_Parser_Item>, _Parser_Item>;
                                if (charMatcher != null)
                                {
                                    int lineNum = charMatcher.GetLineNumber(rec.Pos);
                                    Console.WriteLine("{0} ({1}): {2}", file.Name, lineNum, rec.Message);

                                    int offset;
                                    string line = charMatcher.GetLine(rec.Pos, out offset);

                                    Console.WriteLine();
                                    Console.WriteLine(line.Trim());
                                    Console.WriteLine(new string(' ', offset - 2) + '^');
                                }
                                else
                                {
                                    Console.WriteLine("{0}: {1}", file.Name, rec.Message);
                                }

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
