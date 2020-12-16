using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Day14
{
    class Program
    {
        private static string[] input;
        static void Main(string[] args)
        {
            var sr = new StreamReader(@"input.txt");
            input = sr.ReadToEnd().Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            One();
        }

        static void One()
        {

            var mem = new Dictionary<int, long>();
            var mask = "";
            for (var i = 0; i < input.Length; i++)
            {
                var parts = input[i].Split('=', StringSplitOptions.RemoveEmptyEntries);
                var op = parts[0].Trim();
                var val = parts[1].Trim();
                if (op.Equals("mask"))
                {
                    mask = val;
                }
                else if (op.StartsWith("mem"))
                {
                    var address = int.Parse(op.Substring(4).Trim(']'));
                    var bin = new StringBuilder(Convert.ToString(int.Parse(val), 2).PadLeft(36, '0'));

                    long dec = 0;
                    for (var j = 0; j < mask.Length; j++)
                    {
                        if (mask[j] != 'X')
                            bin[j] = mask[j];
                    }

                    dec = Convert.ToInt64(bin.ToString(), 2);

                    if (mem.ContainsKey(address))
                        mem[address] = dec;
                    else
                        mem.Add(address, dec);
                }
            }

            Console.WriteLine($"Sum of values in memory {mem.Values.Sum()}");
        }

        static void Two()
        {

        }
    }
}
