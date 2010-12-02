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
            Parser parser = new Parser(input);
            Result match = parser.GetMatch(parser.IronMetaFile);

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
        public Result Process(string fname, string nameSpace, bool force)
        {
            string out_fname;
            if (fname.ToUpper().EndsWith(".IRONMETA"))
                out_fname = fname.Substring(0, fname.Length - 9) + ".cs";
            else
                out_fname = fname + ".cs";

            if (string.IsNullOrEmpty(nameSpace))
            {
                FileInfo info = new FileInfo(fname);
                nameSpace = info.Directory.Name;
            }

            FileInfo srcInfo = new FileInfo(fname);
            if (!srcInfo.Exists)
                throw new Exception("File not found: " + fname);

            FileInfo destInfo = new FileInfo(out_fname);
            if (destInfo.Exists && !force)
            {
                if (srcInfo.LastWriteTimeUtc <= destInfo.LastWriteTimeUtc)
                {
                    Console.WriteLine("Source file unchanged; not generating.");
                    return null;
                }
            }

            string contents;
            using (StreamReader sr = new StreamReader(fname))
            {
                contents = sr.ReadToEnd();
            }

            Result match = null;
            using (StringWriter sw = new StringWriter())
            {
                match = Process(contents, sw, nameSpace);

                if (match.Success)
                {
                    using (StreamWriter fw = new StreamWriter(out_fname))
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
            Program generator = new Program();

            // get options
            string nameSpace = string.Empty;
            bool force = false;

            List<string> fileArgs = new List<string>();
            for (int i = 0; i < args.Length; ++i)
            {
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

                fileArgs.Add(args[i]);
            }

            // process files
            foreach (string arg in fileArgs)
            {
                if (arg.ToUpper().EndsWith(".IRONMETA"))
                {
                    try
                    {
                        DateTime start = DateTime.Now;
                        Result match = generator.Process(arg, nameSpace, force);
                        DateTime end = DateTime.Now;

                        FileInfo file = new FileInfo(arg);
                        if (match != null)
                        {
                            if (match.Success)
                            {
                                Console.WriteLine("{0}: {1}", file.Name, (end - start));
                            }
                            else
                            {
                                ErrorRec rec = match.Matcher.GetLastError();

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
                        Console.WriteLine("{0}: Error: {1}", arg, e.Message);
                        break;
                    }
                }
            }

            return 0;
        } // Main()

    } // class Program

} // namespace IronMeta.Generator
