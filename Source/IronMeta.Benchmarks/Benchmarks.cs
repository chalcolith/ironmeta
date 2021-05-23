using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System.Collections.Generic;
using System.Linq;

namespace IronMeta.Benchmarks {

    [SimpleJob(RuntimeMoniker.Net472, baseline: true)]
    [SimpleJob(RuntimeMoniker.NetCoreApp21)]
    [SimpleJob(RuntimeMoniker.NetCoreApp30)]
    [SimpleJob(RuntimeMoniker.Net50)]
    [RPlotExporter]
    public class Benchmarks {

        static string input;
        static string GenerateInput(int count) => string.Join("+", Enumerable.Repeat("1", count));

        [GlobalSetup]
        public void GlobalSetup() => input = GenerateInput(5_000);

        [Benchmark]
        public void CalcParserBenchmark() {
            var parser = new IronMeta.Samples.Calc.Calc();
            var match = parser.GetMatch(input, parser.Expression);
        }
    }
}