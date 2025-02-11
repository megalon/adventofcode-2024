using System.Text.RegularExpressions;

namespace aoc_2024_day_7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.txt");

            string[] text = File.ReadAllLines(filepath);

            ulong total = 0;

            foreach (string line in text) {
                ulong target = ulong.Parse(line.Substring(0, line.IndexOf(':')));
                uint[] values = line.Split(": ")[1].Split(' ').Select(uint.Parse).ToArray();

                //Console.WriteLine(target + ": " + String.Join(' ', values));

                if (DoesCalculate(values, 0, target, 0))
                {
                    total += target;
                }
            }

            Console.WriteLine(total);
        }

        private static bool DoesCalculate(uint[] values, ulong result, ulong target, int index)
        {
            if (index == values.Length)
            {
                return result == target;
            }

            return DoesCalculate(values, result + values[index], target, index + 1)
                || DoesCalculate(values, result * values[index], target, index + 1);
        }
    }
}
