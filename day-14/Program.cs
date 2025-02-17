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
            int numSeconds = 100;

            int[,] map = new int[mapSize.x, mapSize.y];

            foreach (Match match in Regex.Matches(text, @"p=(-?\d+),(-?\d+) v=(-?\d+),(-?\d+)"))
            {
                IVector2 position = new IVector2(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
                IVector2 velocity = new IVector2(int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value));

                Console.WriteLine($"p:{position.ToString()} v:{velocity.ToString()}");

                // Process movement
                for (int i = 0; i < numSeconds; ++i)
                {
                    // Move along velocity
                    position.x += velocity.x;
                    position.y += velocity.y;

                    // Wrap around edges
                    if (position.x >= mapSize.x)
                        position.x %= mapSize.x;
                    if (position.y >= mapSize.y)
                        position.y %= mapSize.y;

                    // If the value is negative, we need to move it back onto the map
                    if (position.x < 0)
                        position.x = (position.x % mapSize.x) + mapSize.x;
                    if (position.y < 0)
                        position.y = (position.y % mapSize.y) + mapSize.y;
                }
                map[position.x, position.y] += 1;
            }
            PrintMap(map);
        }

        private static void PrintMap(int[,] map)
        {
            for (int y = 0; y < map.GetLength(1); ++y)
            {
                for (int x = 0; x < map.GetLength(0); ++x)
                {
                    Console.Write(map[x, y] == 0 ? "." : map[x, y]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();

        }

        public class IVector2
        {
            public int x { get; set; }
            public int y { get; set; }

            public static IVector2 ZERO = new IVector2(0, 0);
            public IVector2(IVector2 other)
            {
                this.x = other.x;
                this.y = other.y;
            }
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
