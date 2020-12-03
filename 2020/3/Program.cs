using System;
using System.Collections.Generic;
using System.IO;

namespace _3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            List<string> rows = new List<string>();
            using (FileStream file = new FileStream("input.txt", FileMode.Open))
            using (StreamReader reader = new StreamReader(file))
            {
                while (!reader.EndOfStream)
                {
                    rows.Add(reader.ReadLine());
                }
            }
            //int x = A(rows, 1, 3);
            //Console.WriteLine($"A: {x} trees.");
            var results = A(rows, 1, 1) * A(rows, 3, 1) * A(rows, 5, 1) * A(rows, 7, 1) * A(rows, 1, 2);
            Console.WriteLine($"B: {results} is the total");
        }

        static int A(List<string> rows, int across, int down)
        {
            int count = 0;
            int numOfRows = rows.Count;
            int rowlen = rows[0].Length;
            int counter = 0;
            for (int i = down; i < numOfRows; i += down)
            {
                counter = counter + across;
                if (counter >= rowlen)
                {
                    counter = counter % rowlen;
                }

                if (rows[i][counter] == '#')
                {
                    count++;
                }
            }

            return count;
        }
    }
}
