using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace _7
{
    class Program
    {
        static Regex bagLineRegex = new Regex(@"^(?<bagColor>.*)\sbags contain(?<contents>.*)$");
        static Regex contentsRegex = new Regex(@"(?<count>[0-9]+)\s(?<bagColor>[^,.]+)\sbag(s{0,1})");

        static void Main(string[] args)
        { 
            var input = File.ReadAllText("input.txt").Trim(' ');
            var bagLines = input.Split("\r\n");
            var baglistA = GetContainingBagsA(bagLines);

            PartA(baglistA);

            var baglistB = GetBagsForPartB(bagLines);
            PartB(baglistB);
            Debugger.Break();
        }


        private static Dictionary<string, Dictionary<string, int>> GetContainingBagsA(string[] bags)
        {
            Dictionary<string, Dictionary<string, int>> bagTypes = new Dictionary<string, Dictionary<string, int>>();
            foreach (var line in bags)
            {
                Match match = bagLineRegex.Match(line);
                string bagColor = match.Groups["bagColor"].Value;

                foreach (Match cMatch in contentsRegex.Matches(match.Groups["contents"].Value))
                {
                    string cColor = cMatch.Groups["bagColor"].Value;
                    int cnt = Int32.Parse(cMatch.Groups["count"].Value);

                    if (!bagTypes.TryGetValue(cColor, out Dictionary<string, int> cValues))
                    {
                        cValues = new Dictionary<string, int>();
                        bagTypes.Add(cColor, cValues);
                    }

                    cValues.Add(bagColor, cnt);
                }
            }

            return bagTypes;
        }

        private static void PartA(Dictionary<string, Dictionary<string, int>> bagList)
        {
            List<string> containingBag = new List<string>();
            Stack<string> bagWalkList = new Stack<string>();

            foreach (var color in bagList["shiny gold"])
            {
                containingBag.Add(color.Key);
                bagWalkList.Push(color.Key);
            }

            while (bagWalkList.Count > 0)
            {
                var color = bagWalkList.Pop();
                if (bagList.TryGetValue(color, out Dictionary<string, int> containingColors))
                {
                    foreach (var cColor in containingColors.Keys)
                    {
                        if (!containingBag.Contains(cColor))
                        {
                            containingBag.Add(cColor);
                            bagWalkList.Push(cColor);
                        }
                    }
                }
            }

            int counter = containingBag.Count;
            Console.WriteLine($"A: Number of bags that can have a shiny gold is {counter}.");
        }
        
        static Dictionary<string, Dictionary<string, int>> GetBagsForPartB(string[] baglines)
        {
            Dictionary<string, Dictionary<string, int>> baglist = new Dictionary<string, Dictionary<string, int>>();
            foreach(var bag in baglines)
            {
                var match = bagLineRegex.Match(bag);
                var key = match.Groups["bagColor"].Value;
                var contents = new Dictionary<string, int>();
                foreach (Match containingBag in contentsRegex.Matches(match.Groups["contents"].Value))
                {
                    contents.Add(containingBag.Groups["bagColor"].Value, Int32.Parse(containingBag.Groups["count"].Value));
                }

                baglist[key] = contents;
            }

            return baglist;
        }

        static void PartB(Dictionary<string, Dictionary<string, int>> bagList)
        {
            int counter = GetBagCount("shiny gold", bagList);
            Console.WriteLine($"B: {counter} bags fit in the shiny gold bag.");
        }

        /// <summary>
        /// Recursive function to add the number of bags itself contains. 
        /// </summary>
        /// <param name="bagColor"></param>
        /// <param name="bagList"></param>
        /// <returns></returns>
        private static int GetBagCount(string bagColor, Dictionary<string, Dictionary<string, int>> bagList)
        {
            int counter = 0;
            if (bagList.ContainsKey(bagColor))
            {
                foreach (var bag in bagList[bagColor])
                {
                    counter += bag.Value;
                    counter += bag.Value * GetBagCount(bag.Key, bagList);
                }
            }
            return counter;
        }
    }
}
