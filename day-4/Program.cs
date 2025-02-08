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

            int total1 = 0;
            int total2 = 0;

            // iterate through every character in data
            for (int y = 0; y < matrix.GetLength(0); ++y)
            {
                for (int x = 0; x < matrix.GetLength(1); ++x)
                {
                    char c = matrix[x, y];

                    //Console.Write(c);
                    
                    total1 += Part1(matrix, x, y);

                    total2 += Part2(matrix, x, y);
                }

                //Console.WriteLine();
            }

            Console.WriteLine($"Part 1: {total1}");
            Console.WriteLine($"Part 2: {total2}");
        }

        private static int Part1(char[,] matrix, int x, int y)
        {
            string textToFind = "XMAS";
            int matchCount = 0;

            if (matrix[x, y] != 'X') return 0;

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

        private static int Part2(char[,] matrix, int x, int y)
        {
            string textToFind = "MAS";
            int matchCount = 0;

            if (matrix[x, y] != 'A') return 0;

            matchCount += FindDirectional(matrix, x - 1, y - 1, textToFind, 1, 1); // diag down right
            matchCount += FindDirectional(matrix, x + 1, y - 1, textToFind, -1, 1); // diag down left
            matchCount += FindDirectional(matrix, x - 1, y + 1, textToFind, 1, -1); // diag up right
            matchCount += FindDirectional(matrix, x + 1, y + 1, textToFind, -1, -1); // diag up left
            
            // Need two matches to make an X
            return matchCount == 2 ? 1 : 0;
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
