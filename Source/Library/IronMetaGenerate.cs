using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using IronMeta.Generator;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace IronMeta
{
    /// <summary>
    /// An MsBuild task for generating parsers.
    /// </summary>
    public class IronMetaGenerate : Task
    {
        [Required]
        public string Input { get; set; }

        public string Namespace { get; set; }

        public string Output { get; set; }

        public bool Force { get; set; }

        static readonly Regex IronMetaFileName = new Regex(@"^(.*).ironmeta\s*$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public override bool Execute()
        {
            if (string.IsNullOrWhiteSpace(Output))
            {
                var match = IronMetaFileName.Match(Input);
                if (match.Success)
                    Output = match.Groups[1].Value + ".g.cs";
                else
                    Output = Input + ".g.cs";
            }

            var result = CSharpShell.Process(Input, Output, Namespace, Force);
            if (result.Success)
                return true;

            int num, offset;
            result.MatchState.GetLine(result.ErrorIndex, out num, out offset);

            Log.LogError("", "", "", Input, num, offset, num, offset, result.Error);
            return false;
        }
    }
}
