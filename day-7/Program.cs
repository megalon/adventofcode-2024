/*
 * I misunderstood the question for part 2, so it took me a long time to arrive at the answer
 * 
 * Originally, I was concatenating before evaluting with the result
 * Like this:
 * 1 + 1 + 1 || 1 + 1
 *     2 + 1 || 1 + 1
 *     2 +     11 + 1
 *             13 + 1
 *                 14
 * 
 * But since the operations are evaluated left to right,
 * and the concat operator is just an operator like "+" or "*"
 * you are supposed to concat onto the result of the prior operators
 * like this
 * 1 + 1 + 1 || 1 + 1
 *     2 + 1 || 1 + 1
 *         3 || 1 + 1
 *             31 + 1
 *                 32
 * 
 * This looks pretty obvious when you work through the problem like this.
 * Once I realized I was trying to solve the wrong problem it was fairly easy to fix.
 * I was then able to greatly simplify the code.
 * 
 * */

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
                ulong[] values = line.Split(": ")[1].Split(' ').Select(ulong.Parse).ToArray();

                Console.WriteLine(target + ": " + String.Join(' ', values));

                if (DoesCalculate(values, values[0], target, 0, "" + values[0]))
                {
                    Console.WriteLine($"{target} is valid! ^^^");
                    total += target;
                } else
                {
                    Console.WriteLine($"{target} is invalid.");
                }
            }

            Console.WriteLine("Total: " + total);
        }

        private static bool DoesCalculate(ulong[] values, ulong result, ulong target, int index, string equation, Ops op = Ops.START)
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
                case Ops.CONCAT:
                    equation = equation + " || " + values[index];
                    result = ulong.Parse($"{result}{values[index]}");
                    break;
                default:
                    break;
            };

            //if (index + 1 >= values.Length)
            //    Console.WriteLine($"{equation} = {result}");

            return DoesCalculate(values, result, target, index + 1, equation, Ops.ADD)
                || DoesCalculate(values, result, target, index + 1, equation, Ops.MULT)
                || DoesCalculate(values, result, target, index + 1, equation, Ops.CONCAT);
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
