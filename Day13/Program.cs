using System;

namespace Day13
{
    class Program
    {
        private static int timeStamp = 1006697;

        private static string[] schedule =
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
            foreach (var s in schedule)
            {
                if (!int.TryParse(s, out var id)) continue;
                var remainder = timeStamp % id;
                if (id-remainder >= waitTime) continue;
                waitTime = id-remainder;
                busId = id;
            }
            Console.WriteLine($"busId: {busId}, waitTime: {waitTime}. Answer {busId*waitTime}");
        }

        static void Two()
        {
        }
    }
}
