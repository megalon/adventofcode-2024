using System.Diagnostics.Metrics;
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

            uint totalCostPart1 = 0;

            foreach (Match match in Regex.Matches(data, @"(Button A: X\+\d+, Y\+\d+)\s+(Button B: X\+\d+, Y\+\d+)\s+(Prize: X=\d+, Y=\d+)"))
            {
                UIVector2 A = VectorFromInputString(match.Groups[1].Value);
                UIVector2 B = VectorFromInputString(match.Groups[2].Value);
                UIVector2 T = VectorFromInputString(match.Groups[3].Value);

                Console.WriteLine(T.ToString());

                totalCostPart1 += GetTokenCost(A, B, T, 100);

                Console.WriteLine();
            }

            Console.WriteLine("Total cost part 1: " + totalCostPart1);
        }

        private static uint GetTokenCost(UIVector2 A, UIVector2 B, UIVector2 T, uint maxPresses)
        {
            UIVector2 result = new UIVector2(0, 0);
            
            // We want as few A presses as possible, so start at 0
            // We want as many B presses as possible, so start at max 
            for (uint countA = 0; countA < maxPresses; ++countA)
            {
                for (uint countB = maxPresses; countB > 0; --countB)
                {
                    result = new UIVector2(
                        A.x * countA + B.x * countB,
                        A.y * countA + B.y * countB
                    );

                    if (T == result)
                    {
                        Console.WriteLine("****** MATCH ******");
                        Console.Write($"{A.ToString()} * {countA} + {B.ToString()} * {countB} = {result.ToString()}");

                        return countA * 3 + countB;
                    }
                }
            }
         
            return 0;
        }

        private static UIVector2 VectorFromInputString(string input)
        {
            Match match = Regex.Match(input, @"\D+(\d+)\D+(\d+)");

            return new UIVector2(uint.Parse(match.Groups[1].Value), uint.Parse(match.Groups[2].Value));
        }

        private struct UIVector2
        {
            public uint x { get; }
            public uint y { get; }

            public UIVector2(uint x, uint y)
            {
                this.x = x;
                this.y = y;
            }
            public static bool operator ==(UIVector2 left, UIVector2 right)
            {
                return left.x == right.x && left.y == right.y;
            }
            public static bool operator !=(UIVector2 left, UIVector2 right)
            {
                return left.x != right.x || left.y != right.y;
            }
            public override string ToString()
            {
                return "(" + x + ", " + y + ")";
            }
        }
    }
}
