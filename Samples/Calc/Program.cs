// IronMeta Copyright © Gordon Tisher 2019

using System;

namespace IronMeta.Samples.Calc
{
    class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Enter an arithmatic expression (using only integers).");
            Console.Write("Enter to quit");

            string input;

            do
            {
                Console.Write(": ");
                input = Console.ReadLine();

                if (!string.IsNullOrEmpty(input))
                {
                    var matcher = new Calc();
                    var result = matcher.GetMatch(input, matcher.Expression);

                    if (result.Success)
                        Console.WriteLine("Result: " + result.Result);
                    else
                        Console.WriteLine("Error: " + result.Error);
                }
            }
            while (!string.IsNullOrEmpty(input));
        }
    }
}
