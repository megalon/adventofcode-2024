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

            int totalCost = 0;

            foreach (Match match in Regex.Matches(data, @"(Button A: X\+\d+, Y\+\d+)\s+(Button B: X\+\d+, Y\+\d+)\s+(Prize: X=\d+, Y=\d+)"))
            {
                IVector2 A = VectorFromInputString(match.Groups[1].Value);
                IVector2 B = VectorFromInputString(match.Groups[2].Value);
                IVector2 T = VectorFromInputString(match.Groups[3].Value);

                Console.WriteLine(T.ToString());

                totalCost += GetTokenCost(A, B, T);

                Console.WriteLine();
            }

            // print total cost
            Console.WriteLine("Total cost: " +  totalCost);
        }

        private static int GetTokenCost(IVector2 A, IVector2 B, IVector2 T)
        {
            IVector2 result = new IVector2(0, 0);

            // max 100 button presses
            int maxCount = 100;
            
            // We want as few A presses as possible, so start at 0
            // We want as many B presses as possible, so start at max count 
            for (int countA = 0; countA < maxCount; ++countA)
            {
                for (int countB = maxCount; countB > 0; --countB)
                {
                    result = new IVector2(
                        A.x * countA + B.x * countB,
                        A.y * countA + B.y * countB
                    );

                    if (T == result)
                    {
                        Console.WriteLine("****** MATCH ******");
                        //Console.WriteLine($"{result.ToString()} == {T.ToString()} -> {result == T}");
                        Console.Write($"{A.ToString()} * {countA} + {B.ToString()} * {countB} = {result.ToString()}");

                        return countA * 3 + countB;
                    }
                }
            }
         
            return 0;
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
            public static bool operator ==(IVector2 left, IVector2 right)
            {
                return left.x == right.x && left.y == right.y;
            }
            public static bool operator !=(IVector2 left, IVector2 right)
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
