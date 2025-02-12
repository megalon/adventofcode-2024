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

                if (DoesCalculate(values, values[0], target, 1, "" + values[0]))
                {
                    total += target;
                }
            }

            Console.WriteLine(total);
        }

        private static bool DoesCalculate(uint[] values, ulong result, ulong target, int index, string equation, Ops op = Ops.NONE, bool isConcat = false)
        {
            if (index == values.Length)
            {
                return result == target;
            }

            equation = "|" + equation;

            switch (op)
            {
                case Ops.ADD:
                    equation += " + " + values[index];
                    break;
                case Ops.MULT:
                    equation += " * " + values[index];
                    break;
                default:
                    break;
            };

            if (index + 1 == values.Length) { 
                Console.WriteLine(equation + " = " + result);
            } else
            {
                Console.WriteLine(equation);
            }

            return DoesCalculate(values, result + values[index], target, index + 1, equation, Ops.ADD, isConcat)
                || DoesCalculate(values, result * values[index], target, index + 1, equation, Ops.MULT, isConcat);
        }

        private enum Ops
        {
            NONE,
            ADD,
            MULT,
            CONCAT
        }
    }
}
