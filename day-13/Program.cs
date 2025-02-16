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

                // iterate, adding each time
                // max 100 iterations
                {
                    // do some fancy math to find the divisors
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
        }
    }
}
