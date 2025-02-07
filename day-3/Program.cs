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
        /// This uses a buffer to read through all of the input data and track the current "don't()" and "do()" pairs
        /// When a pair is found, the characters before it are added to the processedData string, and the characters inside the pair is ignored
        /// </summary>
        private static string ProcessData(string data)
        {
            string processedData = "";

            int bufferSize = "don't()".Length;
            char[] buffer = new char[bufferSize];
            string bufferString = "";
            char c = ' ';
            int processIndex = 0, dontIndex = 0;

            for (int i = 0; i < data.Length; ++i)
            {
                c = data[i];

                // 2 bytes per char
                Buffer.BlockCopy(buffer, 2, buffer, 0, (bufferSize - 1) * 2);

                buffer[bufferSize - 1] = c;

                bufferString = new string(buffer);

                if (bufferString.Contains("don't()"))
                {
                    dontIndex = i;
                }
                else if (bufferString.Contains("do()"))
                {
                    // substring from previous position to current "don't()" position
                    processedData += data.Substring(processIndex, dontIndex - processIndex);

                    // move process index to current read index to skip the chars between don't and do
                    processIndex = i;
                    dontIndex = i;

                    // clear the buffer so we don't trigger the condition again on the next iteration
                    buffer = new char[bufferSize];
                }
            }

            // Special case for end of file
            processedData += data.Substring(processIndex);

            return processedData;
        }
    }
}
