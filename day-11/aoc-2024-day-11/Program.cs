using System.Text.RegularExpressions;

namespace aoc_2024_day_11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // read in data
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.txt");
            string data = File.ReadAllText(filepath);

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

                    // if even number of digits
                    if (CountDigits(stones[stoneIdx]) % 2 == 0)
                    {
                        // split in half
                        // insert left half before this index
                        // insert right half after this index

                        continue;
                    }

                    // other rules didn't apply
                    // so multiply by 2024
                    stones[stoneIdx] *= 2024;
                }

                Console.WriteLine();
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
