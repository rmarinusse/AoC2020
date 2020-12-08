using System;
using System.IO;
using System.Linq;

namespace Day5
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
            var input = sr.ReadToEnd();
            var bps = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            var highestSeatId = bps.Select(bp =>
                Convert.ToInt32(bp.Replace('F', '0').Replace('B', '1').Replace('L', '0').Replace('R', '1'), 2)).Max();
            Console.WriteLine($"Highest seat id is {highestSeatId}");
        }

        static void b()
        {
            var sr = new StreamReader(@"input.txt");
            var input = sr.ReadToEnd();
            var bps = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            var seatIds = bps.Select(bp =>
                    Convert.ToInt32(bp.Replace('F', '0').Replace('B', '1').Replace('L', '0').Replace('R', '1'), 2))
                .OrderBy(s => s)
                .Skip(8)
                .SkipLast(8).ToList();
            for (var i = 0; i < seatIds.Count()-1; i++)
            {
                if (seatIds.ElementAt(i).Equals(seatIds.ElementAt(i + 1) - 1)) continue;
                var assignedSeatId = seatIds.ElementAt(i) + 1;
                Console.WriteLine($"Assigned seat id {assignedSeatId}");
                break;
            }
        }
    }
}
