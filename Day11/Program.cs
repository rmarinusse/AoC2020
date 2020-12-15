using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.VisualBasic.CompilerServices;

namespace Day11
{
    class Program
    {
        private static List<string> input;



        static void Main(string[] args)
        {
            var sr = new StreamReader(@"input.txt");
            input = sr.ReadToEnd().Split("\r\n", StringSplitOptions.RemoveEmptyEntries).Select(s => '.' + s + '.')
                .ToList();
            input.Add(new string('.', input.First().Length));
            input.Insert(0, new string('.', input.First().Length));

            a();
            b();
        }

        static void a()
        {
            var iteration = 0;
            var mutating = true;
            var thisIteration = input.ConvertAll(s => new string(s));
            while (mutating)
            {
                mutating = false;
                var nextIteration = thisIteration.ConvertAll(s => new string(s));
                for (var i = 1; i < thisIteration.Count - 1; i++)
                {
                    nextIteration[i] = ".";
                    for (var j = 1; j < thisIteration[i].Length - 1; j++)
                    {
                        if (thisIteration[i][j].Equals('.'))
                        {
                            nextIteration[i] += ".";
                            continue;
                        }

                        var occupiedNeighbours = CountOccupiedNeighbours(thisIteration, i, j);
                        switch (thisIteration[i][j])
                        {
                            case 'L' when occupiedNeighbours == 0:
                                mutating = true;
                                nextIteration[i] += "#";
                                break;
                            case '#' when occupiedNeighbours > 3:
                                mutating = true;
                                nextIteration[i] += "L";
                                break;
                            default:
                                nextIteration[i] += thisIteration[i][j];
                                break;
                        }
                    }

                    nextIteration[i] += ".";
                }

                thisIteration = nextIteration.ConvertAll(s => new string(s));
                iteration++;
                /*
                Console.Clear();
                Console.WriteLine($"Iteration: {iteration}");
                foreach (var s in thisIteration)
                {
                    Console.WriteLine(s);
                }
                */
            }

            var occupiedSeats = thisIteration.Sum(s => s.Count(c => c.Equals('#')));
            Console.WriteLine($"{occupiedSeats} Occupied seats after {iteration} iterations");
        }

        static int CountOccupiedNeighbours(List<string> grid, int row, int column)
        {
            // L: empty
            // #:occupied
            // .:floor

            var neighbors = string.Empty;
            neighbors += grid[row - 1].Substring(column - 1, 3);
            neighbors += grid[row][column - 1];
            neighbors += grid[row][column + 1];
            neighbors += grid[row + 1].Substring(column - 1, 3);
            return neighbors.Count(n => n.Equals('#'));
        }

        static void b()
        {
            var iteration = 0;
            var mutating = true;
            var thisIteration = input.ConvertAll(s => new string(s));
            var occupiedVisibleNeighbours = 0;
            while (mutating)
            {
                mutating = false;
                var nextIteration = thisIteration.ConvertAll(s => new string(s));
                for (var row = 1; row <= thisIteration.Count - 2; row++)
                {
                    nextIteration[row] = ".";
                    for (var column = 1; column <= thisIteration[row].Length-2 ; column++)
                    {
                        if (!thisIteration[row][column].Equals('.'))
                            occupiedVisibleNeighbours = CountOccupiedVisibleNeighbours(thisIteration, row, column);
                        switch (thisIteration[row][column])
                        {
                            case 'L' when occupiedVisibleNeighbours == 0:
                                mutating = true;
                                nextIteration[row] += "#";
                                break;
                            case '#' when occupiedVisibleNeighbours > 4:
                                mutating = true;
                                nextIteration[row] += "L";
                                break;
                            default:
                                nextIteration[row] += thisIteration[row][column];
                                break;
                        }
                    }

                    nextIteration[row] += ".";
                }

                thisIteration = nextIteration.ConvertAll(s => new string(s));
                iteration++;
                Console.Clear();
                Console.WriteLine($"Iteration: {iteration}");
                foreach (var s in thisIteration)
                {
                    Console.WriteLine(s);
                }
            }

            var occupiedSeats = thisIteration.Sum(s => s.Count(c => c.Equals('#')));
            Console.WriteLine($"{occupiedSeats} Occupied seats after {iteration} iterations");
        }

        static int CountOccupiedVisibleNeighbours(List<string> grid, int row, int column)
        {
            // L: empty
            // #:occupied
            // .:floor

            var neighbors = 0;
            int x;
            int y;

            // Up
            y = row-1;
            while (y >= 0)
            {
                if (grid[y].ElementAt(column).Equals('#'))
                {
                    neighbors++;
                    break;
                }

                y--;
            }

            // Down
            y = row + 1;
            while (y < grid.Count)
            {
                if (grid[y].ElementAt(column).Equals('#'))
                {
                    neighbors++;
                    break;
                }

                y++;
            }

            // Left
            x = column - 1;
            while (x >= 0)
            {
                if (grid[row].ElementAt(x).Equals('#'))
                {
                    neighbors++;
                    break;
                }

                x--;
            }

            // Right
            x = column + 1;
            while (x < grid[row].Length)
            {
                if (grid[row].ElementAt(x).Equals('#'))
                {
                    neighbors++;
                    break;
                }

                x++;
            }



            // NW
            x = column - 1;
            y = row - 1;
            while (x >= 0 && y >= 0)
            {
                if (grid[x].ElementAt(y).Equals('#'))
                {
                    neighbors++;
                    break;
                }

                x--;
                y--;
            }

            // NE
            x = column + 1;
            y = row - 1;
            while (x < grid[row].Length - 1 && y >= 0)
            {
                if (grid[x].ElementAt(y).Equals('#'))
                {
                    neighbors++;
                    break;
                }

                x++;
                y--;
            }

            // SW
            x = column - 1;
            y = row + 1;
            while (x >= 0 && y < grid.Count - 1)
            {
                if (grid[x].ElementAt(y).Equals('#'))
                {
                    neighbors++;
                    break;
                }

                x--;
                y++;
            }

            // SE
            x = column + 1;
            y = row + 1;
            while (x < grid[row].Length - 1 && y < grid.Count - 1)
            {
                if (grid[x].ElementAt(y).Equals('#'))
                {
                    neighbors++;
                    break;
                }

                x++;
                y++;
            }


            return neighbors;
        }

    }
}
