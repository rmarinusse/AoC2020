using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day12
{
    class Program
    {
        static void Main(string[] args)
        {
            One();
        }

        static void One()
        {
            var direction = 'E';
            var x = 0;
            var y = 0;
            var sr = new StreamReader(@"input.txt");
            var input = sr.ReadToEnd().Split("\r\n", StringSplitOptions.RemoveEmptyEntries).ToList();
            input.ForEach(op =>
            {
                var instruction = op[0];
                var amount = Convert.ToInt32(op.Substring(1));
                if (new[]{'N', 'E', 'S','W'}.Contains(instruction)){
                    var (xOffset, yOffset) = Move(instruction, amount);
                    x += xOffset;
                    y += yOffset;
                } else if (instruction.Equals('F'))
                {
                    var (xOffset, yOffset) = Move(direction, amount);
                    x += xOffset;
                    y += yOffset;
                } else if (new[] {'L', 'R'}.Contains(instruction))
                {
                    var degrees = Degrees(direction);
                    if (instruction.Equals('L'))
                    {
                        direction = Direction((360+degrees - amount) % 360);
                    }

                    if (instruction.Equals('R'))
                    {
                        direction = Direction((degrees + amount) % 360);
                    }
                }
            });
            Console.WriteLine($"Manhattan distance {Math.Abs(x) + Math.Abs(y)}");
        }

        private static ( int xOffset, int yOffset) Move(char direction, int amount)
        {
            return direction switch
            {
                'N' => (0, amount),
                'E' => (amount, 0),
                'S' => (0, -amount),
                'W' => (-amount, 0),
                _ => (0, 0)
            };
        }

        private static int Degrees(char direction)
        {
            return direction switch
            {
                'N' => 0,
                'E' => 90,
                'S' => 180,
                'W' => 270,
                _ => throw new InvalidOperationException($"Wrong direction {direction}")
            };
        }

        private static char Direction(int degrees)
        {
            return degrees switch
            {
                0 => 'N',
                90 => 'E',
                180 => 'S',
                270 => 'W',
                _ => throw new InvalidOperationException($"Wrong angle {degrees}")
            };
        }

    }
}
