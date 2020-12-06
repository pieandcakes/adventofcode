using System;
using System.Diagnostics;
using System.IO;

namespace _6
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");
            var groups = input.Split("\r\n\r\n");


            PartA(groups);
            PartB(groups);
            Debugger.Break();
        }

        static void PartB(string[] groups)
        {
            int total = 0;
            foreach (var group in groups)
            {
                var people = group.Split("\r\n");
                int[] answers = new int[26];
                foreach(var person in people)
                foreach (char x in person)
                {
                    if (x >= 'a' && x <= 'z')
                    {
                        answers[x - 'a'] += 1;
                    }
                }

                for (int i = 0; i < 26; i++)
                {
                    if (answers[i] == people.Length)
                        total++;
                }

            }

            Console.WriteLine($"B: Total is {total}");
        }

        static void PartA(string[] groups)
        {
            int total = 0;
            foreach (var group in groups)
            {
                var line = group.Replace("\r\n", string.Empty);
                bool[] answers = new bool[26];
                foreach (char x in line)
                {
                    if (x >= 'a' && x <= 'z')
                    {
                        answers[x - 'a'] = true;
                    }
                }

                for (int i = 0; i < 26; i++)
                {
                    if (answers[i])
                        total++;
                }
            }

            Console.WriteLine($"A: total is {total}");
        }
    }
}
