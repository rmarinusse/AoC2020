using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day6
{
    class Program
    {
        static void Main(string[] args)
        {
            a();
            Console.ReadKey();
            b();
        }
        static void a()
        {
            var sr = new StreamReader(@"input.txt");
            var input = sr.ReadToEnd().Split("\r\n\r\n", StringSplitOptions.RemoveEmptyEntries).ToList();
            var sum = 0;
            input.ForEach(g =>
            {
                var answers = new string(g.Replace("\r\n", "").Distinct().ToArray()).Length;
                sum += answers;
            });
            Console.WriteLine($"Sum of answer counts (anyone) {sum}");
        }

        static void b()
        {
            var sr = new StreamReader(@"input.txt");
            var input = sr.ReadToEnd().Split("\r\n\r\n", StringSplitOptions.RemoveEmptyEntries).ToList();
            var sum = 0;
            input.ForEach(g =>
            {
                var answerCounts = new Dictionary<char, int>();
                var answers = g.Split("\r\n", StringSplitOptions.RemoveEmptyEntries).ToList();
                answers.ForEach(a =>
                {
                    a.ToCharArray().ToList().ForEach(c =>
                    {
                        if (answerCounts.ContainsKey(c))
                            answerCounts[c]++;
                        else
                            answerCounts.Add(c, 1);
                    });
                    sum += answerCounts.Count(a => a.Value.Equals(answers.Count));
                });
            });
            Console.WriteLine($"Sum of answer counts (everyone) {sum}");
        }
    }
}
