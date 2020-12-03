using System;
using System.Collections.Generic;
using System.IO;

namespace _1a
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            List<int> Items = new List<int>();
            using (FileStream file = new FileStream("input.txt", FileMode.Open))
            using (StreamReader reader = new StreamReader(file))
            {
                while (!reader.EndOfStream)
                {
                    var firstItem = reader.ReadLine();
                    int x = Int32.Parse(firstItem);
                    Items.Add(x);
                }
            }
            TwoItems(Items);
            ThreeItems(Items);
        }

        private static void TwoItems(List<int> Items)
        {
            int count = Items.Count;
            Console.WriteLine($"We have {count} items.");

            var itemArray = Items.ToArray();
            for (int i = 0; i < count; i++)
            {
                int a = itemArray[i];
                for (int j = i + 1; j < count; j++)
                {
                    if (a + itemArray[j] == 2020)
                    {
                        Console.WriteLine($"[TWO] The answers are: {a} , {itemArray[j]} , {a * itemArray[j]}");
                    }
                }
            }
        }
        private static void ThreeItems(List<int> Items)
        {
            int count = Items.Count;
            Console.WriteLine($"We have {count} items.");

            var itemArray = Items.ToArray();
            for (int i = 0; i < count; i++)
            {
                int a = itemArray[i];
                for (int j = i + 1; j < count; j++)
                {
                    for (int k = j; k < count; k++)
                    {
                        if (a + itemArray[j] + itemArray[k] == 2020)
                        {
                            Console.WriteLine($"[THREE]The answers are: {a} , {itemArray[j]}, {itemArray[k]}, {a * itemArray[j] * itemArray[k]}");
                        }
                    }
                }
            }
        }

    }
}
