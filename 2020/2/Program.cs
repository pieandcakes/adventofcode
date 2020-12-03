using System;
using System.Collections.Generic;
using System.IO;

namespace _2
{
    class Program
    {
        public struct Policy
        {
            public int min;
            public int max;
            public char c;
            public string pwd;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            List<Policy> policies = new List<Policy>();
            using (FileStream file = new FileStream("input.txt", FileMode.Open))
            using (StreamReader reader = new StreamReader(file))
            {
                while (!reader.EndOfStream)
                {
                    var firstItem = reader.ReadLine();
                    Policy line;
                    var items = firstItem.Split(':');
                    var lhs = items[0].Split(' ');
                    line.c = lhs[1][0];
                    var rules = lhs[0].Split('-');
                    line.min = Int32.Parse(rules[0]);
                    line.max = Int32.Parse(rules[1]);
                    line.pwd = items[1].Trim();

                    policies.Add(line);
                }
            }

            var x = ProcessPolicies1(policies);
            Console.WriteLine($"A: {x} are correct.");

            var y = ProcessPolicies2(policies);
            Console.WriteLine($"B: {y} are correct.");
        }

        static int ProcessPolicies1(List<Policy> policies)
        {
            int count = 0;
            foreach (var item in policies)
            {
                var occurances = item.pwd.Split(item.c);
                if (occurances.Length > item.min && occurances.Length <= item.max + 1)
                {
                    count++;
                }
            }

            return count;
        }

        static int ProcessPolicies2(List<Policy> policies)
        {
            int count = 0;
            foreach (var item in policies)
            {
                bool first = item.pwd[item.min - 1] == item.c;
                if (item.pwd.Length < item.max)
                {
                    if (first)
                        count++;
                    continue;
                }
                bool second = item.pwd[item.max - 1] == item.c;

                if ((first || second) && !(first == second))
                {
                    count++;
                }
            }

            return count;
        }
    }
}
