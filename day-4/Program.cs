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

            char[,] matrix = new char[lines[0].Trim().Length, lines.Length];

            for (int y = 0; y < matrix.GetLength(0); ++y)
            {
                for (int x = 0; x < matrix.GetLength(1); ++x)
                {
                    matrix[x, y] = lines[y][x];
                }
            }

            int total = 0;

            // iterate through every character in data
            for (int y = 0; y < matrix.GetLength(0); ++y)
            {
                for (int x = 0; x < matrix.GetLength(1); ++x)
                {
                    char c = matrix[x, y];

                    //Console.Write(c);

                    total += Part1(matrix, x, y);
                }

                //Console.WriteLine();
            }

            Console.WriteLine(total);
        }


        private static int Part1(char[,] matrix, int x, int y)
        {
            string textToFind = "XMAS";
            int matchCount = 0;

            matchCount += FindDirectional(matrix, x, y, textToFind, 1, 0); // right
            matchCount += FindDirectional(matrix, x, y, textToFind, -1, 0); // left
            matchCount += FindDirectional(matrix, x, y, textToFind, 0, 1); // up
            matchCount += FindDirectional(matrix, x, y, textToFind, 0, -1); // down

            matchCount += FindDirectional(matrix, x, y, textToFind, 1, 1); // diag down right
            matchCount += FindDirectional(matrix, x, y, textToFind, -1, 1); // diag down left
            matchCount += FindDirectional(matrix, x, y, textToFind, 1, -1); // diag up right
            matchCount += FindDirectional(matrix, x, y, textToFind, -1, -1); // diag up left

            return matchCount;
        }

        private static int FindDirectional(char[,] matrix, int x, int y, string textToFind, int xDelta, int yDelta)
        {
            string foundText = "";

            for (int i = 0;
                i < textToFind.Length
                && x + (i * xDelta) >= 0
                && y + (i * yDelta) >= 0
                && x + (i * xDelta) < matrix.GetLength(0)
                && y + (i * yDelta) < matrix.GetLength(1);
                ++i)
            {
                foundText += matrix[x + (i * xDelta), y + (i * yDelta)];
            }

            return foundText.Equals(textToFind) ? 1 : 0;
        }
    }
}
