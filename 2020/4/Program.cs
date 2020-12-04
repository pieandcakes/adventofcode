using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace _4
{
    class Program
    {
        struct Passport
        {
            public int? byr;
            public int? iyr;
            public int? eyr;
            public string hgt;
            public string hcl;
            public string ecl;
            public string pid;
            public string cid;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            List<Passport> rows = new List<Passport>();
            int counter = 0;
            int counter2 = 0;
            int total = 0;
            using (FileStream file = new FileStream("input.txt", FileMode.Open))
            using (StreamReader reader = new StreamReader(file))
            {
                while (!reader.EndOfStream)
                {
                    StringBuilder passportDataLine = new StringBuilder();
                    var line = reader.ReadLine();
                    while (!string.IsNullOrEmpty(line))
                    {
                        passportDataLine.Append(line);
                        passportDataLine.Append(' ');
                        line = reader.ReadLine();
                    }
                    var kvps = passportDataLine.ToString().Split(' ');

                    Passport passport = new Passport();
                    ProcessPassport(kvps, ref passport);

                    counter += IsValidPassportPartA(passport);
                    counter2 += IsValidPassportPartB(passport);

                    total++;
                }
            }

            Console.WriteLine($"A: {counter} valid values out of {total}");
            Console.WriteLine($"B: {counter2} valid values out of {total}");
        }

        /// <summary>
        /// Part A: Only validation was no missing parts except 'cid'
        /// </summary>
        static int IsValidPassportPartA(Passport passport)
        {
            if (!passport.byr.HasValue)
                return 0;

            if (!passport.iyr.HasValue)
                return 0;

            if (!passport.eyr.HasValue)
                return 0;

            if (String.IsNullOrWhiteSpace(passport.hgt))
                return 0;

            if (String.IsNullOrWhiteSpace(passport.hcl))
                return 0;


            if (String.IsNullOrWhiteSpace(passport.ecl))
                return 0;

            if (String.IsNullOrWhiteSpace(passport.pid))
                return 0;
            //if (String.IsNullOrWhiteSpace(passport.cid))
            //    return 0;


            return 1;
        }

        /// <summary>
        /// Part B: Contains checks for boundaries on each of the values
        /// </summary>
        static int IsValidPassportPartB(Passport passport)
        {
            if (!(passport.byr.GetValueOrDefault(0) >= 1920 && passport.byr.GetValueOrDefault(0) <= 2002))
                return 0;

            if (!(passport.iyr.GetValueOrDefault(0) >= 2010 && passport.iyr.GetValueOrDefault(0) <= 2020))
                return 0;

            if (!(passport.eyr.GetValueOrDefault(0) >= 2020 && passport.eyr.GetValueOrDefault(0) <= 2030))
                return 0;

            if (String.IsNullOrWhiteSpace(passport.hgt))
                return 0;
            else
            {
                if (passport.hgt.EndsWith("cm"))
                {
                    int value;
                    if (!Int32.TryParse(passport.hgt.Substring(0, passport.hgt.Length - 2), out value))
                        return 0;
                    if (value < 150 || value > 193)
                        return 0;
                }
                else if (passport.hgt.EndsWith("in"))
                {
                    int value;
                    if (!Int32.TryParse(passport.hgt.Substring(0, passport.hgt.Length - 2), out value))
                        return 0;
                    if (value < 59 || value > 76)
                        return 0;
                }
                else
                    return 0;
            }

            Regex hclRegex = new Regex("^#[0-9a-f]{6}$");
            if (String.IsNullOrWhiteSpace(passport.hcl) || !hclRegex.Match(passport.hcl).Success)
                return 0;

            Regex eclRegex = new Regex("^(amb)|(blu)|(brn)|(gry)|(grn)|(hzl)|(oth){1}$");

            if (String.IsNullOrWhiteSpace(passport.ecl) || !eclRegex.Match(passport.ecl).Success)
                return 0;

            Regex pidRegex = new Regex("^[0-9]{9}$"); // this doesn't check length for some reason.
            if (String.IsNullOrWhiteSpace(passport.pid) || !pidRegex.Match(passport.pid).Success)
                return 0;

            // This wasn't required
            // if (String.IsNullOrWhiteSpace(passport.cid))
            //    return 0;

            return 1;
        }

        static void ProcessPassport(string[] kvp, ref Passport passport)
        {
            foreach (var item in kvp)
            {
                if (string.IsNullOrEmpty(item))
                    continue;
                var entry = item.Split(':');
                switch (entry[0])
                {
                    case "byr":
                        if (passport.byr.HasValue)
                            throw new ArgumentException();
                        passport.byr = Int32.Parse(entry[1]);
                        break;
                    case "iyr":
                        if (passport.iyr.HasValue)
                            throw new ArgumentException();
                        passport.iyr = Int32.Parse(entry[1]);
                        break;
                    case "eyr":
                        if (passport.eyr.HasValue)
                            throw new ArgumentException();
                        passport.eyr = Int32.Parse(entry[1]);
                        break;
                    case "hgt":
                        if (!String.IsNullOrEmpty(passport.hgt))
                            throw new ArgumentException();
                        passport.hgt = entry[1];
                        break;
                    case "hcl":
                        if (!String.IsNullOrEmpty(passport.hcl))
                            throw new ArgumentException();
                        passport.hcl = entry[1];
                        break;
                    case "ecl":
                        if (!String.IsNullOrEmpty(passport.ecl))
                            throw new ArgumentException();
                        passport.ecl = entry[1];
                        break;
                    case "pid":
                        if (!String.IsNullOrEmpty(passport.pid))
                            throw new ArgumentException();
                        passport.pid = entry[1];
                        break;
                    case "cid":
                        passport.cid = entry[1];
                        break;
                    default:
                        Debug.Fail("WHAT?!?!");
                        throw new ArgumentException(entry[0]);
                }
            }
        }
    }
}
