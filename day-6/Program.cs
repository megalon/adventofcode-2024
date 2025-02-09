namespace aoc_2024_day_6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.txt");
            string[] lines = File.ReadAllLines(filepath);

            char[,] matrix = new char[lines[0].Length, lines.Length];

            for (int y = 0; y < matrix.GetLength(1); ++y)
            {
                for (int x = 0; x < matrix.GetLength(0); ++x)
                {
                    matrix[x, y] = lines[y][x];

                    Console.Write(matrix[x, y]);
                }
                Console.WriteLine();
            }
        }
    }
}
