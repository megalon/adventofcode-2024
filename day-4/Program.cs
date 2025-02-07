using System.Text.RegularExpressions;

namespace aoc_2024_day_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.txt");

            string text = File.ReadAllText(filepath);

            string[] lines = text.Split('\n');

            char[,] matrix = new char[lines[0].Length, lines.Length];

            for (int x = 0; x < lines.Length - 1; ++x)
            {
                for (int y = 0; y < lines[0].Length - 1; ++y)
                {
                    matrix[x, y] = lines[x][y];
                }
            }

            int total = 0;
            string textToFind = "XMAS";

            // iterate through every character in data
            for (int x = 0; x < matrix.GetLength(0); ++x)
            {
                for (int y = 0; y < matrix.GetLength(1); ++y)
                {
                    char c = matrix[x, y];

                    Console.Write(c);

                    // check if character is an X
                    if (c != 'X') continue;

                    total += FindRight(matrix, x, y, textToFind);

                    // left

                    // up

                    // down

                    // diag up right

                    // diag up left

                    // diag down right

                    // diag down left

                }
                
                Console.WriteLine();
            }

            // log total
            Console.WriteLine(total);
        }

        private static int FindRight(char[,] matrix,  int x, int y, string textToFind)
        {
            string foundText = "X";

            for (int i = 1; i < matrix.GetLength(0) && i < textToFind.Length; ++i)
            {
                foundText += matrix[x + i, y];
            }

            return foundText.Equals(textToFind) ? 1 : 0;
        }
    }
}
