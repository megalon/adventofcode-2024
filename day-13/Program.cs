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

            // parse into some kind of collection
            foreach (Match match in Regex.Matches(data, @"(Button A: X\+\d+, Y\+\d+)\s+(Button B: X\+\d+, Y\+\d+)\s+(Prize: X=\d+, Y=\d+)"))
            {
                Console.WriteLine("A: " + match.Groups[1].Value);
                Console.WriteLine("B: " + match.Groups[2].Value);
                Console.WriteLine("T: " + match.Groups[3].Value);
                Console.WriteLine();
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

        // class ClawMachine
            // IVector2 buttonA
            // IVector2 buttonB
            // IVector2 target

        // class IVector2
            // x, y
    }
}
