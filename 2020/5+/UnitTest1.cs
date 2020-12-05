using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace _5_
{
    class Program
    {
        static void Main(string[] args)
        {
            PartA();
            PartB();
            Debugger.Break();
        }

        static void PartA()
        {
            var seats = File.ReadAllLines("input5.txt");
            int highest = 0;
            foreach (var seat in seats)
            {
                int seatVal = GetSeatVal(seat);
                if (seatVal > highest)
                    highest = seatVal;
            }

            Console.WriteLine($"A: highest is {highest}");
        }

        static void PartB()
        {
            var seats = File.ReadAllLines("input5.txt");
            List<int> SeatIds = new List<int>();
            foreach (var seat in seats)
            {
                SeatIds.Add(GetSeatVal(seat));
            }

            SeatIds.Sort();
            int seatId = SeatIds[0];
            for (int i = 1; i < SeatIds.Count; i++)
            {
                if (SeatIds[i] != ++seatId)
                {
                    Console.WriteLine($"B: Your seatId is {seatId}");
                    return;
                }
            }
        }

        static int GetSeatVal(string seat)
        {
            int row = 0;
            int col = 0;

            for (int i = 0; i < 7; i++)
            {
                row <<= 1;
                row += seat[i] == 'F' ? 0 : 1;
            }

            for (int i = 7; i < 10; i++)
            {
                col <<= 1;
                col += seat[i] == 'R' ? 1 : 0;
            }

            return (int)row * 8 + (int)col;
        }
    }
}
