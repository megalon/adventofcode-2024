using System.Numerics;
using System.Text.RegularExpressions;

namespace aoc_2024_day_13
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // read in data
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.txt");
            string data = File.ReadAllText(filepath);

            foreach (Match match in Regex.Matches(data, @"(Button A: X\+\d+, Y\+\d+)\s+(Button B: X\+\d+, Y\+\d+)\s+(Prize: X=\d+, Y=\d+)"))
            {
                IVector2 A = VectorFromInputString(match.Groups[1].Value);
                IVector2 B = VectorFromInputString(match.Groups[2].Value);
                IVector2 T = VectorFromInputString(match.Groups[3].Value);

                // Loop maximums
                // This is the max number of times we can add this
                // number to get a number smaller than the target t
                // It also has to be less than 100 by the puzzle rules
                int maxA = (int)Math.Min(Math.Floor(T.x / (decimal)A.x), Math.Floor(T.y / (decimal)A.y));
                int maxB = (int)Math.Min(Math.Floor(T.x / (decimal)B.x), Math.Floor(T.y / (decimal)B.y));

                int maxCount = Math.Min(100, Math.Min(maxA, maxB));

                IVector2 divisorA = new IVector2(0, 0);
                IVector2 divisorB = new IVector2(0, 0);
                int countInv = 0;

                // iterate, adding each time
                // max 100 iterations
                for (int count = maxCount; count > 0; --count)
                {
                    divisorB = new IVector2(B.x * count, B.y * count);

                    // if not divisible ignore
                    if (T.x % divisorB.x != 0)
                        continue;

                    countInv = maxCount - count;

                    divisorA = new IVector2(A.x * countInv, A.y * countInv);

                    if (T.x % divisorA.x != 0)
                        continue;

                    // I guess that's it we found a match for x?
                    Console.WriteLine("Match? Target: " + T.ToString());
                    Console.WriteLine($"{T.x} * {count} + {T.y} * {countInv} = {(T.x * count) + (T.y * countInv)}");
                }

                // If not possible to reach target, ignore

                // calculate cost
            }

            // print total cost
        }

        private static IVector2 VectorFromInputString(string input)
        {
            Match match = Regex.Match(input, @"\D+(\d+)\D+(\d+)");

            return new IVector2(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
        }

        private struct IVector2
        {
            public int x { get; }
            public int y { get; }

            public IVector2(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
            public override string ToString()
            {
                return "" + x + ", " + y;
            }
        }
    }
}
