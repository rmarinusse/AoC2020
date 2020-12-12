using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Xml;

namespace Day10
{
    class Program
    {
        //private static List<int> input;
        private static ImmutableList<int> orderedInput;
        static void Main(string[] args)
        {
            var sr = new StreamReader(@"input.txt");
            var input = sr.ReadToEnd().Split("\r\n", StringSplitOptions.RemoveEmptyEntries).Select(i => Convert.ToInt32(i))
                .ToList();
            //input.Add(0);
            input.Add(input.Max()+3);
            orderedInput = input.OrderBy(i => i).ToImmutableList();
            a();
            b();
        }

        static void a()
        {
            var adapterSteps = new List<int>();
            for (var i = 1; i < orderedInput.Count(); i++)
            {
                adapterSteps.Add(orderedInput.ElementAt(i) - orderedInput.ElementAt(i - 1));
            }

            adapterSteps.Add(3);
            var stepsOfOne = adapterSteps.Count(step => step == 1);
            var stepsOfThree = adapterSteps.Count(step => step == 3);
            Console.WriteLine(
                $"{stepsOfOne} steps of 1, {stepsOfThree} steps of 3. Answer is {stepsOfOne * stepsOfThree}");
        }


        static void b()
        {
            var partB = new PartB();
            var answer = partB.GetAdapterCombinations(0, orderedInput);
            Console.WriteLine($"Found {answer} paths");
        }

        class PartB
        {
            readonly Dictionary<int, long> paths = new Dictionary<int, long>();

            public long GetAdapterCombinations(int currentAdapter, IEnumerable<int> adapters)
            {
                if (!adapters.Any())
                    return 1;
                if (paths.ContainsKey(currentAdapter))
                    return paths[currentAdapter];
                paths[currentAdapter] = adapters.TakeWhile(a => a > currentAdapter && a <= currentAdapter + 3)
                    .Select((a, idx) => GetAdapterCombinations(a, adapters.Skip(idx + 1))).Sum();
                return paths[currentAdapter];
            }

        }
    }
}
