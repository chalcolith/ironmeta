using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System.Collections.Generic;
using System.Linq;

namespace IronMeta.Benchmarks
{

    [SimpleJob(RuntimeMoniker.Net48, baseline: true)]
    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [SimpleJob(RuntimeMoniker.Net50)]
    [RPlotExporter]
    public class Benchmarks
    {
        private static List<char> inputList;
        private static char[] inputArray;
        private static string inputString;

        static IEnumerable<int> RandomInts(int count, int min, int max)
        {
            // We need this to generate the same numbers every time, so hard coding arbitrary seed
            var rand = new Random(289759387);
            for (int i = 0; i < count; i++)
                yield return rand.Next(min, max);
        }

        static string GenerateInput(int count)
        {
            var rand = new Random(289759387);
            var sb = new System.Text.StringBuilder(count * 5);
            var ops = new[] { '+', '-', '*' };
            foreach (var n in RandomInts(count, 0, 10_000))
            {
                sb.Append(n);
                sb.Append(ops[rand.Next(0, ops.Length)]);
            }
            sb.Append('0');
            return sb.ToString();
        }

        [GlobalSetup]
        public void GlobalSetup()
        {
            inputString = GenerateInput(1_250);
            inputList = inputString.ToList();
            inputArray = inputString.ToArray();
        }

        [Benchmark]
        public void CalcParserBenchmark()
        {
            var parser = new IronMeta.Samples.Calc.Calc();
            var match = parser.GetMatch(inputString, parser.Expression);
            if (match.NextIndex != inputString.Length)
                Console.Error.WriteLine("INPUT NOT FULLY PARSED");
            try
            {
                var r = match.Result;
            } catch (Exception)
            {
                Console.Error.WriteLine($"Error: {match.Error}");
            }
        }
    }
}