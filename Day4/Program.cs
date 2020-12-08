using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;

namespace Day4
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

            /*

    byr (Birth Year)
    iyr (Issue Year)
    eyr (Expiration Year)
    hgt (Height)
    hcl (Hair Color)
    ecl (Eye Color)
    pid (Passport ID)
    cid (Country ID)

             */
            var file = new StreamReader(@"input.txt");
            string line;
            var passport = new StringBuilder();
            var valid = 0;
            while ((line = file.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(line))
                {
                    var pp = passport.ToString();
                    if (pp.Contains("byr:") && pp.Contains("iyr:") && pp.Contains("eyr:") && pp.Contains("hgt:") &&
                        pp.Contains("hcl:") && pp.Contains("ecl:") && pp.Contains("pid:"))
                    {
                        valid++;
                    }

                    passport = new StringBuilder();
                }

                passport.Append(" ");
                passport.Append(line);
            }

            Console.WriteLine($"{valid} valid passports");
        }

        static void b()
        {
            /*
             * 
    byr (Birth Year) - four digits; at least 1920 and at most 2002.
    iyr (Issue Year) - four digits; at least 2010 and at most 2020.
    eyr (Expiration Year) - four digits; at least 2020 and at most 2030.
    hgt (Height) - a number followed by either cm or in:
        If cm, the number must be at least 150 and at most 193.
        If in, the number must be at least 59 and at most 76.
    hcl (Hair Color) - a # followed by exactly six characters 0-9 or a-f.
    ecl (Eye Color) - exactly one of: amb blu brn gry grn hzl oth.
    pid (Passport ID) - a nine-digit number, including leading zeroes.
    cid (Country ID) - ignored, missing or not.

             */
            var file = new StreamReader(@"input.txt");
            var passports = file.ReadToEnd().Replace("\r\n\r\n", "~").Replace("\r\n", " ").Split("~");
            var counter = passports.Where(passport => !string.IsNullOrEmpty(passport.Trim()) &&
                                                      passport.Contains("byr:") && passport.Contains("iyr:") &&
                                                      passport.Contains("eyr:") && passport.Contains("hgt:") &&
                                                      passport.Contains("hcl:") && passport.Contains("ecl:") &&
                                                      passport.Contains("pid:")).Count(IsValid);
            Console.WriteLine($"{counter} valid passports");
        }

        static bool IsValid(string passport)
        {
            var parts = passport.Split(" ");
            foreach (var part in parts)
            {
                var field = part.Split(":")[0];
                var value = part.Split(":")[1];
                switch (field)
                {
                    case "byr":
                        if (!new Regex("^[0-9]{4}$").IsMatch(value)) return false;
                        if (!int.TryParse(value, out var byr)) return false;
                        if (byr < 1920 || byr > 2002) return false;
                        break;
                    case "iyr":
                        if (!new Regex("^[0-9]{4}$").IsMatch(value)) return false;
                        if (!int.TryParse(value, out var iyr)) return false;
                        if (iyr < 2010 || iyr > 2020) return false;
                        break;
                    case "eyr":
                        if (!new Regex("^[0-9]{4}$").IsMatch(value)) return false;
                        if (!int.TryParse(value, out var eyr)) return false;
                        if (eyr < 2020 || eyr > 2030) return false;
                        break;
                    case "hgt":
                        if (!new Regex("^[0-9]{3}cm$|^[0-9]{2}in$").IsMatch(value)) return false;
                        var unit = value.Substring(value.Length - 2);
                        if (unit.Equals("cm"))
                        {
                            if (!int.TryParse(value.Substring(0, 3), out var hgt)) return false;
                            if (hgt < 150 || hgt > 193) return false;
                        }

                        if (unit.Equals("in"))
                        {
                            if (!int.TryParse(value.Substring(0, 2), out var hgt)) return false;
                            if (hgt < 59 || hgt > 76) return false;
                        }

                        break;
                    case "hcl":
                        if (!new Regex("^#[a-z0-9]{6}$").IsMatch(value)) return false;
                        break;
                    case "ecl":
                        var eyes = new List<string> {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};
                        if (!eyes.Contains(value)) return false;
                        break;
                    case "pid":
                        if (!new Regex("^[0-9]{9}$").IsMatch(value)) return false;
                        break;
                    case "cid":
                        break;
                }
            }

            return true;
        }
    }
}