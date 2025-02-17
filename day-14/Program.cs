using System.Text.RegularExpressions;

namespace aoc_2024_day_14
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.txt");
            string text = File.ReadAllText(filepath);

            IVector2 mapSize = new IVector2(11, 7); // Test map

            int[,] map = new int[mapSize.x, mapSize.y];

            foreach (Match match in Regex.Matches(text, @"p=(-?\d+),(-?\d+) v=(-?\d+),(-?\d+)"))
            {
                IVector2 position = new IVector2(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
                IVector2 velocity = new IVector2(int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value));

                Console.WriteLine($"p:{position.ToString()} v:{velocity.ToString()}");

                map[position.x, position.y] += 1;
            }

            for (int y = 0; y < mapSize.y; ++y)
            {
                for (int x = 0; x < mapSize.x; ++x)
                {
                    Console.Write(map[x, y] == 0 ? "." : map[x, y]);
                }
                Console.WriteLine();
            }
        }

        public class IVector2
        {
            public int x { get; }
            public int y { get; }
            public IVector2(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
            public override string ToString()
            {
                return $"({x},{y})";
            }
        }
    }
}
