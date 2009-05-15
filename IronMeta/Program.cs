//////////////////////////////////////////////////////////////////////
// $Id$
//
// Copyright (c) The IronMeta Project 2009
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
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
            string code = null;
            using (StreamReader sr = new StreamReader(fileName))
            {
                contents = sr.ReadToEnd();
            }

            // parse and generate
            DateTime startTime = DateTime.Now;

            var matcher = new IronMetaMatcher();

            try
            {
                // parse
                var match = matcher.Match(contents, "IronMetaFile");

                if (!match.Success)
                    throw new SyntaxException(match.ErrorIndex, match.Error);

                var ironMetaFile = match.Result as IronMetaFileNode;

                if (ironMetaFile == null)
                    throw new ParseException(0, "Unknown parse error.");

                // analyze
                GenerateInfo info = new GenerateInfo(bareFname, matcher, nameSpace);
                ironMetaFile.AssignLineNumbers(matcher);
                ironMetaFile.Analyze(info);

                // generate
                code = ironMetaFile.Generate(0, info);
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
                lineNumber = matcher.GetLineNumber(pe.Index, out lineOffset);
                string line = matcher.GetLine(contents, lineNumber).Replace("\t", "    ");

                var sb = new StringBuilder();
                sb.AppendFormat("{0}({1}): {4}, {2}\n\n{3}\n", fileName, lineNumber, pe.Message, line, prefix);
                for (int i = 0; i < lineOffset + 1; ++i)
                    sb.Append(" ");
                sb.AppendLine("^");

                Console.WriteLine();
                Console.WriteLine(sb.ToString());
                return false;
            }

            // write output
            if (!string.IsNullOrEmpty(code))
            {
                using (StreamWriter sw = new StreamWriter(outputFName))
                {
                    sw.WriteLine(code);
                }

                Console.WriteLine(": {0}", DateTime.Now - startTime);
            }
            else
            {
                throw new Exception("Internal error: no code generated.");
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
            Program program = new Program();

            foreach (string arg in args)
                program.Process(arg);
        }

    } // class Program

} // namespace IronMeta

