using BenchmarkDotNet.Running;
using System;

namespace IronMeta.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            //ManualRun();
            var summary = BenchmarkRunner.Run<Benchmarks>();
        }

        static void ManualRun()
        {
            var benchmark = new Benchmarks();
            benchmark.GlobalSetup();
            benchmark.CalcParserBenchmark();
        }
    }
}
