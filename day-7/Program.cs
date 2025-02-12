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
                ulong target = ulong.Parse(line.Split(": ")[0]);
                uint[] values = line.Split(": ")[1].Split(' ').Select(uint.Parse).ToArray();

                Console.WriteLine(target + ": " + String.Join(' ', values));

                if (DoesCalculate(values, values[0], target, 0, "" + values[0]))
                {
                    Console.WriteLine("^^^ VALID ^^^");
                    total += target;
                } else
                {
                    Console.WriteLine("^^ INVALID ^^^");
                }
            }

            Console.WriteLine("Total: " + total);
        }

        private static bool DoesCalculate(uint[] values, ulong result, ulong target, int index, string equation, Ops op = Ops.START)
        {
            if (index >= values.Length)
            {
                return result == target;
            }

            switch (op)
            {
                case Ops.ADD:
                    equation += " + " + values[index];
                    result += values[index];
                    break;
                case Ops.MULT:
                    equation += " * " + values[index];
                    result *= values[index];
                    break;
                default:
                    break;
            };

            if (index < values.Length - 1)
            {
                uint[] concatValues = new uint[values.Length - index - 1];

                concatValues[0] = uint.Parse($"{result}{values[index + 1]}");

                // Skip first two elements because we just concatanated them above
                for (int i = index + 2; i < values.Length; ++i)
                {
                    // offset concat index because array is smaller
                    concatValues[i - index - 1] = values[i];
                }

                // Call using the new concat values
                // DON'T increase index because the array is 1 smaller so everything was shifted up anyway
                if (DoesCalculate(
                        concatValues,
                        concatValues[0],
                        target,
                        index,
                        equation + " || " + values[index + 1],
                        op
                    ))
                {
                    return true;
                }
            }

            Console.WriteLine($"{equation}{(index < values.Length - 1 ? "" : " = " + result)}");

            equation = "  " + equation;

            return DoesCalculate(values, result, target, index + 1, equation, Ops.ADD)
                || DoesCalculate(values, result, target, index + 1, equation, Ops.MULT);
        }

        private enum Ops
        {
            START,
            ADD,
            MULT,
            CONCAT
        }
    }
}
