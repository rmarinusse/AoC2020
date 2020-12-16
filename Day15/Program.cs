using System;
using System.Collections.Generic;
using System.Linq;

namespace Day15
{
    class Program
    {
        static void Main(string[] args)
        {
            One();
            Two();
        }

        static void One()
        {
            var sequence = new List<int> {2, 0, 1, 9, 5, 19};
            for (var i = 6; i < 2020; i++)
            {
                var lastNumber = sequence.Last();
                if (!sequence.Take(i-1).Contains(lastNumber))
                    sequence.Add(0);
                else
                {
                    var age = i - sequence.Take(i-1).ToList().FindLastIndex(s => s.Equals(lastNumber))-1;
                    sequence.Add(age);
                }
            }
            Console.WriteLine($"2020th number {sequence.Last()}");
        }

        static void Two()
        {
        }
    }
}
