using System.ComponentModel.Design;

namespace aoc_2024_day_12
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // read in data
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.txt");
            string[] data = File.ReadAllLines(filepath);

            char[,] map = new char[data[0].Length, data.Length];

            // convert string to char matrix array
            for (int y = 0; y < map.GetLength(1); ++y)
            {
                for (int x = 0; x < map.GetLength(0); ++x)
                {
                    map[x, y] = data[y][x];
                }
            }

            // iterate through matrix
            for (int y = 0; y < map.GetLength(1); ++y)
            {
                for (int x = 0; x < map.GetLength(0); ++x)
                {
                    if (map[x, y] < 'A' || map[x, y] > 'Z')
                        continue;

                    // If we are at the right edge of the map, start by searching down
                    Direction dir = x < map.GetLength(0) - 1 ? Direction.RIGHT : Direction.DOWN;

                    char plantType = map[x, y];

                    uint area = FindAreaAndPerimeterRecursive(map, x, y, plantType, dir);

                    Console.WriteLine($"Area for {plantType}: {area}");

                    PrintMap(map);
                    Console.WriteLine();
                }
            }
        }

        // return count and area
        private static uint FindAreaAndPerimeterRecursive(char[,] map, int x, int y, char plantType, Direction dir)
        {
            // if tile is not valid
            if (x < 0 || y < 0 || x >= map.GetLength(0) || y >= map.GetLength(1))
                return 0;

            if (map[x, y] != plantType)
                return 0;

            map[x, y] = '.';

            uint area = 0;

            area += FindAreaAndPerimeterRecursive(map, x, y - 1, plantType, Direction.UP);
            area += FindAreaAndPerimeterRecursive(map, x, y + 1, plantType, Direction.DOWN);
            area += FindAreaAndPerimeterRecursive(map, x - 1, y, plantType, Direction.LEFT);
            area += FindAreaAndPerimeterRecursive(map, x + 1, y, plantType, Direction.RIGHT);

            return area + 1;
        }

        private enum Direction
        {
            UP,
            DOWN,
            LEFT,
            RIGHT,
        }

        private static void PrintMap(char[,] map)
        {
            for (int y = 0; y < map.GetLength(1); ++y)
            {
                for (int x = 0; x < map.GetLength(0); ++x)
                {
                    Console.Write(map[x, y]);
                }
                Console.WriteLine();
            }
        }
    }
}
