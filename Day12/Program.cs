using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day12
{
    class Program
    {
        private static List<string> input;

        static void Main(string[] args)
        {
            var sr = new StreamReader(@"input.txt");
            input = sr.ReadToEnd().Split("\r\n", StringSplitOptions.RemoveEmptyEntries).ToList();
            One();
            Two();
        }

        static void One()
        {
            var direction = 'E';
            var x = 0;
            var y = 0;
            
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

        static void Two()
        {
            var wayPointX = 10;
            var wayPointY = 1;
            var shipX = 0;
            var shipY = 0;

            input.ForEach(op =>
            {
                var instruction = op[0];
                var amount = Convert.ToInt32(op.Substring(1));
                if (new[] {'N', 'E', 'S', 'W'}.Contains(instruction))
                {
                    var (xOffset, yOffset) = Move(instruction, amount);
                    wayPointX += xOffset;
                    wayPointY += yOffset;
                }
                else if (instruction.Equals('F'))
                {
                    shipX += amount * wayPointX;
                    shipY += amount * wayPointY;
                }
                else if (new[] {'L', 'R'}.Contains(instruction))
                {
                    (wayPointX, wayPointY) = instruction switch
                    {
                        //var degrees = Degrees(shipDirection);
                        'L' => Rotate(360 - amount, wayPointX, wayPointY),
                        'R' => Rotate(amount, wayPointX, wayPointY),
                        _ => (wayPointX, wayPointY)
                    };
                }
            });
            Console.WriteLine($"Manhattan distance {Math.Abs(shipX) + Math.Abs(shipY)}");
        }

        private static (int xOffset, int yOffset) Move(char direction, int amount)
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

        private static (int x, int y) Rotate(int degrees, int waypointX, int waypointY)
        {
            return degrees switch
            {
                0 => (waypointX, waypointY),
                90 => (waypointY, -waypointX),
                180 => (-waypointX, -waypointY),
                270 => (-waypointY, waypointX),
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
