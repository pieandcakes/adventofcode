using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _9
{
    class Program
    {
        static void Main(string[] args)
        {
            UInt64[] input = File.ReadAllText("input.txt").Trim(' ').Split("\r\n").Select(item => UInt64.Parse(item)).ToArray<UInt64>();

            PartA(input);
            PartB(input);
            Debugger.Break();
        }

        static void PartA(UInt64[] input)
        {
            int pos = 25;

            while (ValidateA(pos, 25, input) && pos < input.Length)
            {
                pos++;
            }

            if (pos >= input.Length)
            {
                Debugger.Break();
            }

            Console.WriteLine($"A: {pos} is {input[pos]}");
        }

        static void PartB(UInt64[] input)
        {
            int loc = 653;
            UInt64 amount = input[loc];
            UInt64 low; 
            UInt64 high;

            int starting = 0;
            int range = 0;
            UInt64 total = input[starting];

            while (total != amount)
            {
                while (total < amount)
                {
                    range++;
                    total += input[starting + range];
                }

                while(total > amount) 
                {
                    total -= input[starting++];
                    range--;
                }
            }

            if (total == amount)
            {
                Console.WriteLine($"B: start: {starting} and range: {range} ");
                FindHighLow(starting, range, input);
            }
        }

        static void FindHighLow(int pos, int range, UInt64[] input)
        {
            UInt64 low = input[pos];
            UInt64 high = input[pos];
            int loc = 1;
            while(loc <= range)
            {
                UInt64 posValue = input[pos + loc++];
                if ( posValue < low)
                {
                    low = posValue;
                }
                if(posValue > high)
                {
                    high = posValue;
                }
            }

            Console.WriteLine($"Low: {low} and High: {high}, Total: {low + high}");
        }

        static bool ValidateA(int pos, int distance, UInt64[] input)
        {
            int starting = pos - distance;
            int ending = pos - 1;
            for (int i = starting; i <= ending; i++)
            {
                for (int j = ending; j > i; j--)
                    if (input[pos] == input[i] + input[j])
                        return true;
            }

            return false;
        }
    }
}
