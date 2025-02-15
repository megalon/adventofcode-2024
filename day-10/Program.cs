namespace aoc_2024_day_10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.txt");
            string[] data = File.ReadAllLines(filepath);

            int[,] map = new int[data[0].Length, data.Length];

            int total = 0;

            // Loop through map and find trail heads (0)
            for (int y = 0; y < data.Length; ++y)
            {
                for (int x = 0; x < data[0].Length; ++x)
                {
                    Console.Write(data[y][x]);

                    if (data[y][x] < '0' || data[y][x] > '9')
                        map[x, y] = -1;
                    else
                        map[x, y] = int.Parse("" + data[y][x]);
                }
                Console.WriteLine();
            }

            for (int y = 0; y < map.GetLength(1); ++y)
            {
                for (int x = 0; x < map.GetLength(0); ++x)
                {
                    int val = map[x, y];

                    if (val != 0) continue;

                    // Depth first search to find trail ends
                    total += FindTrails(map, x, y, new List<IVector2>());
                }
            }

            Console.WriteLine("Total = " + total);
        }

        private static int FindTrails(int[,] map, int x, int y, List<IVector2> trailEnds, int? previousVal = null)
        {
            if (x < 0 || y < 0 || x >= map.GetLength(0) || y >= map.GetLength(1))
                return 0;

            if (map[x, y] < 0 || map[x, y] > 9)
                return 0;

            if (previousVal != null && map[x, y] != previousVal + 1)
                return 0;

            if (map[x, y] == 9)
            {
                IVector2 trailEnd = new IVector2(x, y);

                if (trailEnds.Where(t => t == trailEnd).Any())
                    return 0;

                trailEnds.Add(trailEnd);

                return 1;
            }

            return FindTrails(map, x, y - 1, trailEnds, map[x, y]) // up
                 + FindTrails(map, x, y + 1, trailEnds, map[x, y]) // down
                 + FindTrails(map, x - 1, y, trailEnds, map[x, y]) // left
                 + FindTrails(map, x + 1, y, trailEnds, map[x, y]); // right
        }

        private class IVector2
        {
            public int x { get; }
            public int y { get; }

            public IVector2(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public static bool operator ==(IVector2 a, IVector2 b)
            {
                return a.x == b.x && a.y == b.y;
            }

            public static bool operator !=(IVector2 a, IVector2 b)
            {
                return a.x != b.x
                    || a.y != b.y;
            }
        }
    }
}
