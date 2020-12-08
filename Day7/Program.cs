using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace Day7
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
            var bags = ReadInput();
            var answer = new List<string>();
            var stack = new Stack<string>();
            stack.Push("shiny gold");
            while (stack.Count > 0)
            {
                var current = stack.Pop();
                bags.ForEach(b =>
                {
                    if (answer.Contains(b.Color)) return;
                    if (!b.Children.ContainsKey(current)) return;
                    stack.Push(b.Color);
                    answer.Add(b.Color);
                });
            }
            Console.WriteLine($"{answer.Count} bags can contain a shiny gold bag");
        }

        static void b()
        {
            var bags = ReadInput();
            var bag = bags.First(b => b.Color.Equals("shiny gold"));
            var count = CountBags(bag, bags);
            Console.WriteLine($"A shiny gold bag holds {count - 1} individual bags");
        }

        static List<Bag> ReadInput()
        {
            var sr = new StreamReader(@"input.txt");
            var input = sr.ReadToEnd().Split("\r\n", StringSplitOptions.RemoveEmptyEntries).ToList();
            var bags = new List<Bag>();
            input.ForEach(b =>
            {
                var rule = b.Split("bags contain", StringSplitOptions.RemoveEmptyEntries);

                var bag = new Bag
                {
                    Color = rule[0].Trim()
                };

                if (!rule[1].Trim().Equals("no other bags."))
                {
                    rule[1].Split(", ", StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(c =>
                    {
                        var ruleparts = c.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                        var color = $"{ruleparts[1]} {ruleparts[2]}";
                        var amount = Convert.ToInt32(ruleparts[0]);
                        bag.Children.Add(color, amount);
                    });
                }

                bags.Add(bag);
            });
            return bags;
        }

        static int CountBags(Bag bag, List<Bag> bags)
        {
            var count = 1;
            var children = bags.Where(b => bag.Children.ContainsKey(b.Color)).ToList();
            children.ForEach(c =>
            {
                var multiplier = bag.Children.First(b => b.Key.Equals(c.Color)).Value;
                count += multiplier * CountBags(c, bags);
            });
            return count;
        }
    }

    class Bag
    {
        public Bag()
        {
            Children = new Dictionary<string, int>();
        }
        public string Color { get; set; }
        public Dictionary<string, int> Children { get; set; }
    }

}
