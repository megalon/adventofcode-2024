using System.Numerics;

namespace aoc_2024_day_6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.txt");
            string[] lines = File.ReadAllLines(filepath);
            Vector2 guardVector = new Vector2();

            for (int y = 0; y < lines.Length; ++y)
            {
                for (int x = 0; x < lines[0].Length; ++x)
                { 
                    if (lines[y][x].Equals('^')) guardVector = new Vector2(x, y);

                    Console.Write(lines[y][x]);
                }
                Console.WriteLine();
            }
        }
    }
}
