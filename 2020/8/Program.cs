using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace _8
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt").Trim(' ').Split("\r\n");

            PartA(input);
            PartB(input);
            Debugger.Break();
        }

        private static Regex lineRegex = new Regex(@"^(?<action>.*)\s(?<sign>[+-]{1})(?<count>[0-9]+)$");
        private static void PartA(string[] input)
        {
            int value = 0;

            bool[] traversal = new bool[input.Length];
            int pos = 0;
            while (!traversal[pos])
            {
                traversal[pos] = true;
                Match match = lineRegex.Match(input[pos]);

                switch (match.Groups["action"].Value)
                {
                    case "nop":
                        pos++;
                        break;
                    case "jmp":
                        pos += GetValue(match.Groups["sign"].Value, match.Groups["count"].Value);
                        break;
                    case "acc":
                        value += GetValue(match.Groups["sign"].Value, match.Groups["count"].Value);
                        pos++;
                        break;
                    default:
                        Debugger.Break();
                        break;
                }
            }

            Console.WriteLine($"A: Value is {value}");
        }

        static void PartB(string[] input)
        {
            int pos = 0;
            int value;
            while (!Traverse(pos, input, out value))
            {
                pos++;
            }

            Console.WriteLine($"B: Value is {value}");
        }

        private static bool Traverse(int curPos, string[] input, out int val)
        {
            val = 0;
            int value = 0;
            int length = input.Length;
            bool[] traversal = new bool[length];
            int  pos = 0;
            while (pos < length && pos >= 0 && !traversal[pos])
            {
                traversal[pos] = true;
                Match match = lineRegex.Match(input[pos]);
                switch (match.Groups["action"].Value)
                {
                    case "nop":
                        if (curPos == pos)
                        {
                            // treat it as a jump
                            pos += GetValue(match.Groups["sign"].Value, match.Groups["count"].Value);
                            continue;
                        }
                        pos++;
                        break;
                    case "jmp":
                        if (curPos == pos)
                        {
                            pos++;
                            break;
                        }
                        pos += GetValue(match.Groups["sign"].Value, match.Groups["count"].Value);
                        break;
                    case "acc":
                        value += GetValue(match.Groups["sign"].Value, match.Groups["count"].Value);
                        pos++;
                        break;
                    default:
                        Debugger.Break();
                        break;
                }
            }

            if (pos < length)
                return false;

            val = value;
            return true;
        }

        private static int GetValue(string sign, string count)
        {
            int val = Int32.Parse(count);
            switch (sign)
            {
                case "+":
                    return val;
                case "-":
                    return 0 - val;
                default:
                    Debugger.Break();
                    break;
            }
            return 0;
        }
    }
}
