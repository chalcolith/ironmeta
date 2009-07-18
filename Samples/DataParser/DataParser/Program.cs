using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace DataParser
{
    class Program
    {

        static void Main(string[] args)
        {
            foreach (string arg in args)
            {
                MasterPlexQTReport.MasterPlexQTReportMatcher matcher = new MasterPlexQTReport.MasterPlexQTReportMatcher();

                try
                {
                    string contents = "";
                    using (StreamReader sr = new StreamReader(arg))
                    {
                        contents = sr.ReadToEnd();
                    }

                    var match = matcher.Match(contents, "Report");

                    if (match.Success)
                    {
                        XmlDocument doc = (XmlDocument)match.Result;
                        Console.WriteLine(doc.InnerXml);
                    }
                    else
                    {
                        int lineNumber, lineOffset;
                        lineNumber = matcher.GetLineNumber(match.ErrorIndex, out lineOffset);
                        string line = matcher.GetLine(contents, lineNumber).Replace("\t", "    ");

                        var sb = new StringBuilder();
                        sb.AppendFormat("{0}({1}): {4}{2}\n\n{3}\n", arg, lineNumber, match.Error, line, "");
                        for (int i = 0; i < lineOffset; ++i)
                            sb.Append(" ");
                        sb.AppendLine("^");

                        Console.WriteLine();
                        Console.WriteLine(sb.ToString());
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(arg + ": ERROR: " + e.Message);
                }
            }
        }

    }
}
