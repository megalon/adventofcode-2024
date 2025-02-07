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

            // iterate through list
            foreach(Match match in Regex.Matches(data, regex))
            {
                Console.WriteLine(match.Value);
                // get 1st number (a)

                // get 2nd number (b)

                // total += a * b
            }

            // log total
        }
    }
}
