using System;
using System.Linq;
using System.Numerics;

namespace Day13
{
    class Program
    {
        private static int timeStamp = 1006697;

        private static string[] input =
            "13,x,x,41,x,x,x,x,x,x,x,x,x,641,x,x,x,x,x,x,x,x,x,x,x,19,x,x,x,x,17,x,x,x,x,x,x,x,x,x,x,x,29,x,661,x,x,x,x,x,37,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,23"
                .Split(',', StringSplitOptions.RemoveEmptyEntries);

        static void Main(string[] args)
        {
            One();
            Two();
        }

        static void One()
        {
            var waitTime = int.MaxValue;
            var busId = 0;
            foreach (var s in input)
            {
                if (!int.TryParse(s, out var id)) continue;
                var remainder = timeStamp % id;
                if (id - remainder >= waitTime) continue;
                waitTime = id - remainder;
                busId = id;
            }

            Console.WriteLine($"busId: {busId}, waitTime: {waitTime}. Answer {busId * waitTime}");
        }

        static void Two()
        {
            ulong step = 1;
            ulong ts = 0;
            //var schedule = input.Select(x => x == "x" ? 1 : int.Parse(x)).ToList();
            var schedule = input.Select((x, i) => (x, i)).Where(x => x.x != "x")
                .Select(b => new {id = ulong.Parse(b.x), offset = (ulong)b.i}).ToList();

            while (schedule.Count > 0)
            {
                ts += step;
                if ((ts + schedule[0].offset) % schedule[0].id == 0)
                {
                    step *= schedule[0].id;
                    schedule.RemoveAt(0);
                }

                Console.Write($"\rTimestamp {ts}");
            }

            Console.WriteLine($"Timestamp {ts}");
        }
    }
}
