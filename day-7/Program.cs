using System.Text.RegularExpressions;

namespace aoc_2024_day_7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.txt");

            string[] text = File.ReadAllLines(filepath);

            foreach (string line in text) {
                Console.WriteLine(line);

                int target = int.Parse(line.Substring(0, line.IndexOf(':')));
                int[] values = line.Substring(line.IndexOf(": ") + 2).Split(' ').Select(int.Parse).ToArray();
            }
        }
    }
}
