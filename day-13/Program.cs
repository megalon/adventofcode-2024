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
                BigVector2 A = VectorFromInputString(match.Groups[1].Value);
                BigVector2 B = VectorFromInputString(match.Groups[2].Value);
                BigVector2 T = VectorFromInputString(match.Groups[3].Value);

                Console.WriteLine(T.ToString());

                //totalCostPart1 += GetTokenCostPart1(A, B, T, 100);
                totalCostPart2 += GetTokenCostPart2(A, B, new BigVector2(T.x + 10000000000000, T.y + 10000000000000));

                Console.WriteLine();
            }

            Console.WriteLine("Total cost part 1: " + totalCostPart1);
            Console.WriteLine("Total cost part 2: " + totalCostPart2);
        }

        private static ulong GetTokenCostPart2(BigVector2 A, BigVector2 B, BigVector2 T)
        {
            // "Cramer's Rule"
            // det = (a_x * b_y - a_y * b_x)
            // countA = (p_x * b_y - p_y * b_x) / det
            // countB = (a_x * p_y - a_y * p_x) / det

            double det = (A.x * B.y - A.y * B.x);
            double countA = (T.x * B.y - T.y * B.x) / det;
            double countB = (A.x * T.y - A.y * T.x) / det;

            // If either one is not a whole number,
            // the input does not divide evenly and
            // the claw machine is not winnable
            // so return zero
            if (countA % 1 > 0 || countB % 1 > 0)
                return 0;

            return (ulong)(countA * 3 + countB);
        }

        private static ulong GetTokenCostPart1(BigVector2 A, BigVector2 B, BigVector2 T, ulong maxPresses)
        {
            BigVector2 result = new BigVector2(0, 0);
            
            // We want as few A presses as possible, so start at 0
            // We want as many B presses as possible, so start at max 
            for (ulong countA = 0; countA < maxPresses; ++countA)
            {
                for (ulong countB = maxPresses; countB > 0; --countB)
                {
                    result = new BigVector2(
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

        private static BigVector2 VectorFromInputString(string input)
        {
            Match match = Regex.Match(input, @"\D+(\d+)\D+(\d+)");

            return new BigVector2(double.Parse(match.Groups[1].Value), double.Parse(match.Groups[2].Value));
        }

        private struct BigVector2
        {
            public double x { get; }
            public double y { get; }

            public BigVector2(double x, double y)
            {
                this.x = x;
                this.y = y;
            }
            public static bool operator ==(BigVector2 left, BigVector2 right)
            {
                return left.x == right.x && left.y == right.y;
            }
            public static bool operator !=(BigVector2 left, BigVector2 right)
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
