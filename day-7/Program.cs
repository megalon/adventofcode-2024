using System.Text.RegularExpressions;

namespace aoc_2024_day_7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.txt");

            string[] text = File.ReadAllLines(filepath);

            int numValid = 0;

            foreach (string line in text) {
                Console.WriteLine(line);

                int target = int.Parse(line.Substring(0, line.IndexOf(':')));
                int[] values = line.Substring(line.IndexOf(": ") + 2).Split(' ').Select(int.Parse).ToArray();

                if (DoesCalculate(values, 0, target, 0))
                {
                    ++numValid;
                }
            }

            Console.WriteLine(numValid);
        }

        private static bool DoesCalculate(int[] values, int total, int target, int index)
        {
            if (index == values.Length)
            {
                return total == target;
            }

            return DoesCalculate(values, total + values[index], target, index + 1)
                || DoesCalculate(values, total * values[index], target, index + 1);
        }
    }
}
