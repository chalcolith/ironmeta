//////////////////////////////////////////////////////////////////////
// $Id$
//
// Copyright (c) 2009, The IronMeta Project
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
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace IronMeta
{

    class Program
    {

        public Program()
        {
        }

        public bool Process(string fileName)
        {
            // get base filename
            string baseFname, bareFname, nameSpace;
            GetFileAndNamespace(ref fileName, out baseFname, out bareFname, out nameSpace);

            string outputFName = baseFname + ".cs";

            Console.Write("{0} -> {1}", fileName, outputFName);

            // get file
            string contents = null;
            using (StreamReader sr = new StreamReader(fileName))
            {
                contents = sr.ReadToEnd();
            }

            // parse
            var matcher = new IronMetaMatcher();

            try
            {
                // parse
                DateTime startParse = DateTime.Now;
                var match = matcher.Match(contents, "IronMetaFile");
                DateTime endParse = DateTime.Now;

                if (!match.Success)
                    throw new SyntaxException(match.ErrorIndex, match.Error);

                SyntaxNode ironMetaFile = match.Result as IronMetaFileNode;

                if (ironMetaFile == null)
                    throw new ParseException(0, "Unknown parse error.");

                // analyze
                DateTime startGen = DateTime.Now;

                GenerateInfo info = new GenerateInfo(bareFname, matcher, nameSpace, contents);
                ironMetaFile.AssignLineNumbers(contents, matcher);
                ironMetaFile.Analyze(info);

                // optimize
                SyntaxNode.Optimize(ironMetaFile);

                // generate
                using (StreamWriter sw = new StreamWriter(outputFName))
                {
                    ironMetaFile.Generate(0, sw, info);
                }

                DateTime endGen = DateTime.Now;

                // print times
                Console.WriteLine(": parse: {0}; generate {1}", (endParse - startParse), (endGen - startGen));
            }
            catch (ParseException pe)
            {
                string prefix;

                if (pe is SyntaxException)
                    prefix = "Syntax error: ";
                else if (pe is SemanticException)
                    prefix = "Semantic error: ";
                else
                    prefix = "Eror: ";

                int lineNumber, lineOffset;
                lineNumber = matcher.GetLineNumber(contents, pe.Index, out lineOffset);
                string line = matcher.GetLine(contents, lineNumber).Replace("\t", "    ");

                var sb = new StringBuilder();
                sb.AppendFormat("{0}({1}): {4}{2}\n\n{3}\n", fileName, lineNumber, pe.Message, line, prefix);
                for (int i = 0; i < lineOffset; ++i)
                    sb.Append(" ");
                sb.AppendLine("^");

                Console.WriteLine();
                Console.WriteLine(sb.ToString());
                return false;
            }

            return true;
        }

        static Regex baseFileRegex = new Regex(@"^(.*\\([^\\\.]+))(\.[^\.]+)?$", RegexOptions.Compiled);

        private void GetFileAndNamespace(ref string fname, out string fileBase, out string fileBare, out string nameSpace)
        {
            FileInfo fi = new FileInfo(fname);
            fname = fi.FullName;

            Match match = baseFileRegex.Match(fi.FullName);
            if (match.Success)
            {
                fileBase = match.Groups[1].Value;
                nameSpace = match.Groups[2].Value;
                fileBare = match.Groups[2].Value + match.Groups[3].Value;
            }
            else
            {
                throw new Exception(fname + ": not a valid filename!");
            }
        }


        //////////////////////////////////////////////////////////////

        static void Main(string[] args)
        {
            //Tests t = new Tests();
            //t.Test_QualifiedIdentifier();

            try
            {
                Program program = new Program();

                foreach (string arg in args)
                    program.Process(arg);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

    } // class Program

} // namespace IronMeta

