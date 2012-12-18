using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Xunit;

namespace IronMeta.UnitTests
{

    public class Timing
    {

        [Fact(Skip = "Only for benchmarking purposes.")]
        public void TimeParser()
        {
            // copy ironmeta file
            File.Copy(@"..\..\..\IronMeta.Generator\Parser.ironmeta", @".", true);



        }

    }

}
