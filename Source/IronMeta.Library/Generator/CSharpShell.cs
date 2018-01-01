// IronMeta Copyright © Gordon Tisher 2018

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IronMeta.Matcher;

namespace IronMeta.Generator
{
    /// <summary>
    /// Wrapper for the C# generator that handles file IO.
    /// </summary>
    public static class CSharpShell
    {
        /// <summary>
        /// Generate a parser from an IronMeta grammar.
        /// </summary>
        /// <param name="src_fname">The name of the source file (to use for error messages).</param>
        /// <param name="input">Input stream.</param>
        /// <param name="output">Output writer.</param>
        /// <param name="name_space">Namespace to use in the generated parser.</param>
        public static MatchResult<char, AST.AstNode> Process(string src_fname, IEnumerable<char> input, TextWriter output, string name_space)
        {
            var parser = new Parser();
            var match = parser.GetMatch(input, parser.IronMetaFile);

            if (match.Success)
            {
                var csgen = new CSharpGen(match.Result, name_space);
                csgen.Generate(src_fname, output);
            }

            return match;
        }

        /// <summary>
        /// Generate a parser from an IronMeta grammar.
        /// </summary>
        /// <param name="input_fname">Input filename.</param>
        /// <param name="output_fname">Output filename.</param>
        /// <param name="name_space">Namespace for the generated parser.</param>
        /// <param name="force">Force generation even if the existing parser is newer than the source.</param>
        /// <returns>The results of the attempt to generate the parser.</returns>
        public static MatchResult<char, AST.AstNode> Process(string input_fname, string output_fname, string name_space, bool force)
        {
            if (string.IsNullOrEmpty(name_space))
            {
                FileInfo info = new FileInfo(input_fname);
                name_space = info.Directory.Name;
            }

            FileInfo srcInfo = new FileInfo(input_fname);
            if (!srcInfo.Exists)
                throw new Exception("File not found: " + input_fname);

            FileInfo destInfo = new FileInfo(output_fname);
            if (destInfo.Exists && !force)
            {
                if (srcInfo.LastWriteTimeUtc <= destInfo.LastWriteTimeUtc)
                {
                    Console.WriteLine("{0} unchanged; not generating.", input_fname);
                    return null;
                }
            }

            string contents;
            using (var sr = new StreamReader(input_fname))
            {
                contents = sr.ReadToEnd();
            }

            MatchResult<char, AST.AstNode> match = null;
            using (var sw = new StringWriter())
            {
                match = Process(input_fname, contents, sw, name_space);

                if (match.Success)
                {
                    using (StreamWriter fw = new StreamWriter(output_fname))
                    {
                        fw.Write(sw.ToString());
                    }
                }
            }

            return match;
        }
    }
}
