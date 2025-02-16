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

            ulong totalCostPart1 = 0;
            ulong totalCostPart2 = 0;

            foreach (Match match in Regex.Matches(data, @"(Button A: X\+\d+, Y\+\d+)\s+(Button B: X\+\d+, Y\+\d+)\s+(Prize: X=\d+, Y=\d+)"))
            {
                ULVector2 A = VectorFromInputString(match.Groups[1].Value);
                ULVector2 B = VectorFromInputString(match.Groups[2].Value);
                ULVector2 T = VectorFromInputString(match.Groups[3].Value);

                Console.WriteLine(T.ToString());

                //totalCostPart1 += GetTokenCostPart1(A, B, T, 100);
                totalCostPart1 += GetTokenCostPart2(A, B, T);
                //totalCostPart2 += GetTokenCostPart2(A, B, new ULVector2(T.x + 10000000000000, T.y + 10000000000000));

                Console.WriteLine();
            }

            Console.WriteLine("Total cost part 1: " + totalCostPart1);
        }

        private static ulong GetTokenCostPart2(ULVector2 A, ULVector2 B, ULVector2 T)
        {
            // 80 * 94 + 40 * 22 = 8400
            //      94 +      22 = 116

            // Max value 

            // The max number of button presses to check is
            // how many presses it would take for the button
            // that adds less to total up to a value greater than the target
            decimal xMax = Math.Ceiling(Math.Max(T.x / A.x, T.x / B.x));
            decimal yMax = Math.Ceiling(Math.Max(T.y / A.y, T.y / B.y));

            Part2Recursive(A, B, 0, 10000, T);

            return 0;
        }

        private static ulong Part2Recursive(ULVector2 A, ULVector2 B, decimal countA, decimal countB, ULVector2 T)
        {
            return 0;
        }

        private static ulong GetTokenCostPart1(ULVector2 A, ULVector2 B, ULVector2 T, ulong maxPresses)
        {
            ULVector2 result = new ULVector2(0, 0);
            
            // We want as few A presses as possible, so start at 0
            // We want as many B presses as possible, so start at max 
            for (ulong countA = 0; countA < maxPresses; ++countA)
            {
                for (ulong countB = maxPresses; countB > 0; --countB)
                {
                    result = new ULVector2(
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

        private static ULVector2 VectorFromInputString(string input)
        {
            Match match = Regex.Match(input, @"\D+(\d+)\D+(\d+)");

            return new ULVector2(decimal.Parse(match.Groups[1].Value), decimal.Parse(match.Groups[2].Value));
        }

        private struct ULVector2
        {
            public decimal x { get; }
            public decimal y { get; }

            public ULVector2(decimal x, decimal y)
            {
                this.x = x;
                this.y = y;
            }
            public static bool operator ==(ULVector2 left, ULVector2 right)
            {
                return left.x == right.x && left.y == right.y;
            }
            public static bool operator !=(ULVector2 left, ULVector2 right)
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
