using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Day9
{
    class Program
    {
        static List<long> input;

        static void Main(string[] args)
        {
            var sr = new StreamReader(@"input.txt");
            input = sr.ReadToEnd().Split("\r\n", StringSplitOptions.RemoveEmptyEntries).Select(i => Convert.ToInt64(i))
                .ToList();
            a();
            b();
        }

        static void a()
        {
            int i;
            long invalidNumber;
            (invalidNumber, i) = GetInvalidNumber();
            Console.WriteLine($"{invalidNumber} with index {i} has no combination in preamble");
        }

        static void b()
        {
            int index;
            long invalidNumber;
            (invalidNumber, index) = GetInvalidNumber();
            var j = index;
            for (var i = 0; i < index - 2; i++)
            {
                for (var k = 2; k < index - i; k++)
                {
                    var test = input.Skip(i).Take(k).ToList();
                    if (test.Sum() != invalidNumber) continue;
                    var answer = test.Min() + test.Max();
                    Console.WriteLine($"{string.Join('+', test)} sums to {invalidNumber}. Answer is {test.Min()}+{test.Max()}={answer}");
                    break;
                }
            }

        }

        static (long, int) GetInvalidNumber()
        {
            int i;
            for (i = 25; i < input.Count; i++)
            {

                var preamble = input.Skip(i - 25).Take(25).ToList();
                var digit = input.ElementAt(i);
                if (!CombinationExists(preamble, digit)) break;
            }

            return (input[i], i);
        }

        static bool CombinationExists(List<long> preamble, long digit)
        {
            var match = false;
            foreach (var l in preamble.Where(l => preamble.Contains(digit - l)))
            {
                match = true;
            }

            return match;
        }
    }
}
