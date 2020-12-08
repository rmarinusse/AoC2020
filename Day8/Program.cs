using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day8
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
            var input = sr.ReadToEnd().Split("\r\n", StringSplitOptions.RemoveEmptyEntries).ToList().Select(i =>
                new Instruction
                {
                    Operation = i.Split(" ")[0], Value =
                        int.Parse(i.Split(" ")[1])
                }).ToList();
            var accumulator = 0;
            var i = 0;
            while (true)
            {
                var instruction = input.ElementAt(i);
                if (!instruction.First)
                    break;
                instruction.First = false;
                switch (instruction.Operation)
                {
                    case "acc":
                        accumulator += instruction.Value;
                        i++;
                        break;
                    case "nop":
                        i++;
                        break;
                    case "jmp":
                        i += instruction.Value;
                        break;
                }
            }

            Console.WriteLine($"Accumulator value is {accumulator}");
        }

        static void b()
        {
            var sr = new StreamReader(@"input.txt");
            var input = sr.ReadToEnd().Split("\r\n", StringSplitOptions.RemoveEmptyEntries).ToList().Select(i =>
                new Instruction
                {
                    Operation = i.Split(" ")[0], Value =
                        int.Parse(i.Split(" ")[1])
                }).ToList();
            var permutations = new List<List<Instruction>>();
            for (var i = 0; i < input.Count(); i++)
            {
                var instruction = input.ElementAt(i);
                if (instruction.Operation.Equals("nop"))
                {
                    var copy = input.Select(op=>op.Clone()).ToList();
                    copy.ElementAt(i).Operation = "jmp";
                    permutations.Add(copy);
                }
                else if (instruction.Operation.Equals("jmp"))
                {
                    var copy = input.Select(op => op.Clone()).ToList();
                    copy.ElementAt(i).Operation = "nop";
                    permutations.Add(copy);
                }
            }

            permutations.ForEach(p =>
            {
                var accumulator = 0;
                if (DoesItTerminate(p, out accumulator))
                {
                    Console.WriteLine($"Accumulator value is {accumulator}");
                }
            });
        }

        static bool DoesItTerminate(List<Instruction> instructions, out int accumulator)
        {
            accumulator = 0;
            var i = 0;
            while (true)
            {
                if (i >= instructions.Count)
                {
                    return true;
                }

                var instruction = instructions.ElementAt(i);
                if (!instruction.First)
                    return false;
                instruction.First = false;
                switch (instruction.Operation)
                {
                    case "acc":
                        accumulator += instruction.Value;
                        i++;
                        break;
                    case "nop":
                        i++;
                        break;
                    case "jmp":
                        i += instruction.Value;
                        break;
                }
            }
        }
    }

    internal class Instruction : ICloneable<Instruction>
    {
        public string Operation { get; set; }
        public int Value { get; set; }

        public bool First { get; set; } = true;

        public Instruction Clone()
        {
            return new Instruction
            {
                Operation = this.Operation,
                Value = this.Value,
                First = this.First
            };
        }
    }

    public interface ICloneable<T>
    {
        T Clone();
    }
}
