﻿using System.Text.RegularExpressions;

namespace aoc_2024_day_11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // read in data
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.txt");
            string data = File.ReadAllText(filepath).Trim();

            Console.WriteLine(data);

            // build a list of ulong
            List<ulong> stones = new List<ulong>();

            foreach (Match match in Regex.Matches(data, @"\d+")) {
                stones.Add(ulong.Parse(match.Value));
            }

            // loop 25 times
            for (int i = 0; i < 25; ++i)
            {
                // iterate through stones list and check rules
                for (int stoneIdx = 0; stoneIdx < stones.Count; ++stoneIdx)
                {
                    if (stones[stoneIdx] == 0)
                    {
                        stones[stoneIdx] = 1;
                        continue;
                    }

                    // Count digits
                    int numDigits = CountDigits(stones[stoneIdx]);

                    // if even number of digits
                    if (numDigits % 2 == 0)
                    {
                        // split stone in half
                        // left digits go to left stone
                        // right digits go to right stone

                        // ** This sort of thing would not work because of percision issues
                        // Floor returns a double for example
                        // Math.Floor(stones[stoneIdx] / Math.Pow(10, numDigits / 2));

                        // Convert to a string and split so we can parse the string to a ulong
                        string s = stones[stoneIdx].ToString();

                        string left = s.Substring(0, numDigits / 2);
                        string right = s.Substring(numDigits / 2);

                        stones[stoneIdx] = ulong.Parse(left);
                        stones.Insert(stoneIdx + 1, ulong.Parse(right));

                        // Skip over the stone we just added
                        ++stoneIdx;

                        continue;
                    }

                    // other rules didn't apply
                    // so multiply by 2024
                    stones[stoneIdx] *= 2024;
                }

                Console.WriteLine(String.Join(' ', stones));
            }

            Console.WriteLine("Total: " + stones.Count);
        }

        private static int CountDigits(ulong number)
        {
            int digits = 1;
            while ((number /= 10) > 0)
            {
                ++digits;
            }

            return digits;
        }
    }
}
