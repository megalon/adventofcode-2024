using System.Text.RegularExpressions;

namespace aoc_2024_day_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filename = "data.txt";
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);

            string data = File.ReadAllText(filePath);

            string processedData = ProcessData(data);

            // get all the matches in a List using regex
            // regex something like "mul("[0-9]*","[0-9]*")"
            string regex = @"mul\([0-9]*,[0-9]*\)";

            int a, b;

            long total = 0;

            // iterate through list
            foreach(Match match in Regex.Matches(processedData, regex))
            {
                int indexOfComma = match.Value.IndexOf(',');

                // get 1st number (a)
                a = int.Parse(match.Value.Substring(4, indexOfComma - 4));

                // get 2nd number (b)
                b = int.Parse(match.Value.Substring(indexOfComma + 1, match.Value.IndexOf(")") - indexOfComma - 1));

                //Console.WriteLine($"{a} * {b} = {a * b}");

                total += a * b;
            }

            Console.WriteLine(total);
        }

        /// <summary>
        /// This uses a buffer to read through all of the input data
        /// Ultimately this works but this is a bad solution compared to just using regex.
        /// I didn't know about regex groups and didn't see how regex could be used to track the indexes of the pairs
        /// I knew that I had to scan through the input data, and a buffer made the most sense
        /// </summary>
        private static string ProcessData(string data)
        {
            string processedData = "";

            int bufferSize = "don't()".Length;
            char[] buffer = new char[bufferSize];
            string bufferString = "";
            char c = ' ';
            bool enabled = true;

            for (int i = 0; i < data.Length; ++i)
            {
                c = data[i];

                // 2 bytes per char
                Buffer.BlockCopy(buffer, 2, buffer, 0, (bufferSize - 1) * 2);

                buffer[bufferSize - 1] = c;

                bufferString = new string(buffer);

                if (bufferString.Contains("don't()"))
                {
                    enabled = false;
                }
                
                if (bufferString.Contains("do()"))
                {
                    enabled = true;

                    // clear the buffer so we don't trigger the condition again on the next iteration
                    buffer = new char[bufferSize];
                }

                if (enabled) processedData += c;
            }

            return processedData;
        }
    }
}
