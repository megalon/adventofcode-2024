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

            uint totalPricePart1 = 0;
            uint totalPricePart2 = 0;

            // iterate through matrix
            for (int y = 0; y < map.GetLength(1); ++y)
            {
                for (int x = 0; x < map.GetLength(0); ++x)
                {
                    if (map[x, y] < 'A' || map[x, y] > 'Z')
                        continue;

                    char plantType = map[x, y];

                    List<IVector2> plot = new List<IVector2>();

                    Direction dir = x < map.GetLength(1) - 1 ? Direction.RIGHT : Direction.DOWN;
                    
                    FindPlot(map, x, y, plantType, plot, dir);

                    uint perimeter = FindPerimeter(plot);
                    uint numSides = FindNumSides(map, plot);

                    Console.WriteLine($"Area for {plantType}: {plot.Count}");
                    Console.WriteLine($"Perimeter for {plantType}: {perimeter}");
                    Console.WriteLine($"Num sides for {plantType}: {numSides}");

                    totalPricePart1 += ((uint)plot.Count * perimeter);
                    totalPricePart2 += ((uint)plot.Count * numSides);

                    //PrintMap(map);
                    Console.WriteLine();
                }
            }

            Console.WriteLine($"Part 1 total price: {totalPricePart1}");
            Console.WriteLine($"Part 2 total price: {totalPricePart2}");
        }

        private static void FindPlot(char[,] map, int x, int y, char plantType, List<IVector2> plot, Direction movementDir)
        {
            IVector2 delta = IVector2.UP;

            plot.Add(new IVector2(x, y));

            map[x, y] = '.';

            foreach (Direction dir in Enum.GetValues(typeof(Direction)))
            {
                delta = DirectionToIVector2(dir);

                IVector2 pos = new IVector2(x + delta.x, y + delta.y);

                if (pos.x < 0
                    || pos.y < 0
                    || pos.x >= map.GetLength(0)
                    || pos.y >= map.GetLength(1)
                    || map[pos.x, pos.y] != plantType)
                {
                    continue;
                }

                FindPlot(map, pos.x, pos.y, plantType, plot, dir);
            }

            return;
        }

        private static uint FindPerimeter(List<IVector2> plot)
        {
            IVector2 delta = IVector2.UP;
            uint perimeter = 0;

            foreach (IVector2 tile in plot)
            {
                foreach (Direction dir in Enum.GetValues(typeof(Direction)))
                {
                    delta = DirectionToIVector2(dir);

                    IVector2 pos = new IVector2(tile.x + delta.x, tile.y + delta.y);

                    if (plot.Where(v => v == pos).Any())
                    {
                        continue;
                    }

                    ++perimeter;
                }
            }

            return perimeter;
        }

        private static uint FindNumSides(char[,] map, List<IVector2> plot)
        {
            uint numSides = 0;

            // This stores a bitfield for all 4 possible directions that a wall could be on
            // This is to prevent the same wall from being counted twice
            Dictionary<IVector2, byte> sidesMap = new Dictionary<IVector2, byte>();

            foreach (IVector2 tile in plot)
            {
                // Check all 4 directions
                foreach (Direction dir in Enum.GetValues(typeof(Direction)))
                {
                    WallSearchStatus status = WallCheckRecusive(map, plot, tile, dir, sidesMap);

                    if (status == WallSearchStatus.OK)
                        ++numSides;
                }
            }

            return numSides;
        }

        private static WallSearchStatus WallCheckRecusive(char[,] map, List<IVector2> plot, IVector2 tile, Direction dir, Dictionary<IVector2, byte> sidesMap)
        {
            //Console.Clear();
            //PrintMap(map, tile, GetCursorChar(dir));
            //Console.WriteLine();

            // If this tile is not in the plot, return
            if (!plot.Where(v => v == tile).Any())
                return WallSearchStatus.NONE;

            IVector2 delta = DirectionToIVector2(dir);
            IVector2 wall = new IVector2(tile.x + delta.x, tile.y + delta.y);

            // If this "wall" is not a wall, but actually part of the plot
            if (plot.Where(v => v == wall).Any())
                return WallSearchStatus.NONE;

            // If we have already accounted for this wall
            if (sidesMap.TryGetValue(wall, out byte bitfield))
            {
                if ((bitfield & (byte)dir) != 0)
                    return WallSearchStatus.ALREADY_COUNTED;
            }

            // We found a fresh section of wall!

            // Init entry in sidesmap, if needed
            if (!sidesMap.ContainsKey(wall)) {
                sidesMap.Add(wall, 0);
            }

            // Update sidesmap to indicate we have visited this tile from this direction
            sidesMap[wall] |= (byte)dir;

            switch (dir)
            {
                case Direction.UP: // Wall is UP, move RIGHT to look for more wall
                    delta = DirectionToIVector2(Direction.RIGHT);
                    break;
                case Direction.RIGHT:
                    delta = DirectionToIVector2(Direction.DOWN);
                    break;
                case Direction.DOWN:
                    delta = DirectionToIVector2(Direction.LEFT);
                    break;
                case Direction.LEFT:
                    delta = DirectionToIVector2(Direction.UP);
                    break;
            }

            // Go to the next tile over where the wall would continue along
            // and check if there is wall there

            tile = new IVector2(tile.x + delta.x, tile.y + delta.y);

            WallSearchStatus wallStatus = WallCheckRecusive(map, plot, tile, dir, sidesMap);

            if (wallStatus == WallSearchStatus.ALREADY_COUNTED)
                return WallSearchStatus.ALREADY_COUNTED;

            return WallSearchStatus.OK;
        }

        private enum WallSearchStatus
        {
            NONE,
            OK,
            ALREADY_COUNTED
        }

        private static IVector2 DirectionToIVector2(Direction dir)
        {
            switch (dir)
            {
                case (Direction.RIGHT):
                    return IVector2.RIGHT;
                case (Direction.DOWN):
                    return IVector2.DOWN;
                case (Direction.LEFT):
                    return IVector2.LEFT;
            }

            return IVector2.UP;
        }

        [Flags]
        private enum Direction
        {
            UP = 1,
            RIGHT = 2,
            DOWN = 4,
            LEFT = 8
        }

        private struct IVector2 : IEquatable<IVector2>
        {
            public int x { get; }
            public int y { get; }

            public static readonly IVector2 UP = new IVector2(0, -1);
            public static readonly IVector2 DOWN = new IVector2(0, 1);
            public static readonly IVector2 LEFT = new IVector2(-1, 0);
            public static readonly IVector2 RIGHT = new IVector2(1, 0);

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
                return a.x != b.x || a.y != b.y;
            }

            // Need to implement this for Dictionary.ContainsKey to work
            public bool Equals(IVector2 other)
            {
                return x == other.x && y == other.y;
            }

            // Apparently when checking for equality it first checks hash code,
            // This might not be equivalent, so just set it to zero
            public override int GetHashCode()
            {
                return 0;
            }
        }

        private static char GetCursorChar(Direction dir)
        {
            switch (dir)
            {
                case Direction.RIGHT:
                    return '>';
                case Direction.DOWN:
                    return 'v';
                case Direction.LEFT:
                    return '<';
                default:
                    return '^';
            }
        }

        private static void PrintMap(char[,] map, IVector2? cursor, char c = '*')
        {
            for (int y = 0; y < map.GetLength(1); ++y)
            {
                for (int x = 0; x < map.GetLength(0); ++x)
                {
                    if (cursor.HasValue && cursor.Value.x == x && cursor.Value.y == y)
                        Console.Write(c);
                    else
                        Console.Write(map[x, y]);
                }
                Console.WriteLine();
            }
        }
    }
}
