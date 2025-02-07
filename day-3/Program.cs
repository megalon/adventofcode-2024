using System.Text.RegularExpressions;

namespace aoc_2024_day_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // read in data

            string filename = "data.txt";
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);

            string data = File.ReadAllText(filePath);

            // get all the matches in a List using regex
            // regex something like "mul("[0-9]*","[0-9]*")"
            string regex = @"mul\([0-9]*,[0-9]*\)";

            int a, b;

            long total = 0;

            // iterate through list
            foreach(Match match in Regex.Matches(data, regex))
            {
                int indexOfComma = match.Value.IndexOf(',');

                // get 1st number (a)
                a = int.Parse(match.Value.Substring(4, indexOfComma - 4));

                // get 2nd number (b)
                b = int.Parse(match.Value.Substring(indexOfComma + 1, match.Value.IndexOf(")") - indexOfComma - 1));

                Console.WriteLine($"{a} * {b} = {a * b}");

                total += a * b;
            }

            Console.WriteLine(total);
        }
    }
}
