namespace aoc_2024_day_10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.txt");
            string[] data = File.ReadAllLines(filepath);

            int[,] map = new int[data[0].Length, data.Length];

            ulong totalPart1 = 0, totalPart2 = 0;

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
                    totalPart1 += FindTrailsPart1(map, x, y, new List<IVector2>());
                    totalPart2 += FindTrailsPart2(map, x, y);
                }
            }

            Console.WriteLine("Part 1 total = " + totalPart1);
            Console.WriteLine("Part 2 total = " + totalPart2);
        }

        private static ulong FindTrailsPart1(int[,] map, int x, int y, List<IVector2> trailEnds, int? previousVal = null)
        {
            if (!IsNextStepInTrail(map, x, y, previousVal))
                return 0;

            if (map[x, y] == 9)
            {
                IVector2 trailEnd = new IVector2(x, y);

                if (trailEnds.Where(t => t == trailEnd).Any())
                    return 0;

                trailEnds.Add(trailEnd);

                return 1;
            }

            return FindTrailsPart1(map, x, y - 1, trailEnds, map[x, y]) // up
                 + FindTrailsPart1(map, x, y + 1, trailEnds, map[x, y]) // down
                 + FindTrailsPart1(map, x - 1, y, trailEnds, map[x, y]) // left
                 + FindTrailsPart1(map, x + 1, y, trailEnds, map[x, y]); // right
        }

        private static ulong FindTrailsPart2(int[,] map, int x, int y, int? previousVal = null)
        {
            if (!IsNextStepInTrail(map, x, y, previousVal))
                return 0;

            if (map[x, y] == 9)
            {
                return 1;
            }

            return FindTrailsPart2(map, x, y - 1, map[x, y])  // up
                 + FindTrailsPart2(map, x, y + 1, map[x, y])  // down
                 + FindTrailsPart2(map, x - 1, y, map[x, y])  // left
                 + FindTrailsPart2(map, x + 1, y, map[x, y]); // right
        }

        private static bool IsNextStepInTrail(int[,] map, int x, int y, int? previousVal = null)
        {
            if (x < 0 || y < 0 || x >= map.GetLength(0) || y >= map.GetLength(1))
                return false;

            if (map[x, y] < 0 || map[x, y] > 9)
                return false;

            if (previousVal != null && map[x, y] != previousVal + 1)
                return false;

            return true;
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
