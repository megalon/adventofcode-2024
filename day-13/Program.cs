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

            // Button A: X + 94, Y + 34
            // Button B: X + 22, Y + 67
            // Prize: X = 8400, Y = 5400

            foreach (Match match in Regex.Matches(data, @"(Button A: X\+\d+, Y\+\d+)\s+(Button B: X\+\d+, Y\+\d+)\s+(Prize: X=\d+, Y=\d+)"))
            {
                ClawMachine machine = new ClawMachine(
                    VectorFromInputString(match.Groups[1].Value),
                    VectorFromInputString(match.Groups[2].Value),
                    VectorFromInputString(match.Groups[3].Value)
                );
            }

            // iterate over claw machines
            {
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

        private class ClawMachine
        {
            public IVector2 buttonA { get; }
            public IVector2 buttonB { get; }
            public IVector2 target { get; }
            public ClawMachine(IVector2 buttonA, IVector2 buttonB, IVector2 target)
            {
                this.buttonA = buttonA;
                this.buttonB = buttonB;
                this.target = target;
            }
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
