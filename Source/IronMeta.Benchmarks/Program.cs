using BenchmarkDotNet.Running;
using System;

namespace IronMeta.Benchmarks {
    class Program {
        static void Main(string[] args) {
            var summary = BenchmarkRunner.Run<Benchmarks>();
        }
    }
}
