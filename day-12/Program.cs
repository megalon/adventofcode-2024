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

                    char plantType = map[x, y];

                    List<IVector2> plot = new List<IVector2>();

                    Direction dir = x < map.GetLength(1) - 1 ? Direction.RIGHT : Direction.DOWN;
                    
                    FindPlot(map, x, y, plantType, plot, dir);

                    uint perimeter = FindPerimeter(plot);

                    Console.WriteLine($"Area for {plantType}: {plot.Count}");
                    Console.WriteLine($"Perimeter for {plantType}: {perimeter}");

                    PrintMap(map);
                    Console.WriteLine();
                }
            }
        }

        private static void FindPlot(char[,] map, int x, int y, char plantType, List<IVector2> plot, Direction movementDir)
        {
            IVector2 delta = IVector2.UP;

            plot.Add(new IVector2(x, y));

            map[x, y] = '.';

            foreach (Direction dir in Enum.GetValues(typeof(Direction)))
            {
                switch (dir)
                {
                    case (Direction.UP):
                        delta = IVector2.UP; break;
                    case (Direction.RIGHT):
                        delta = IVector2.RIGHT; break;
                    case (Direction.DOWN):
                        delta = IVector2.DOWN; break;
                    case (Direction.LEFT):
                        delta = IVector2.LEFT; break;
                }

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
                    switch (dir)
                    {
                        case (Direction.UP):
                            delta = IVector2.UP; break;
                        case (Direction.RIGHT):
                            delta = IVector2.RIGHT; break;
                        case (Direction.DOWN):
                            delta = IVector2.DOWN; break;
                        case (Direction.LEFT):
                            delta = IVector2.LEFT; break;
                    }

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

        private enum Direction
        {
            UP,
            RIGHT,
            DOWN,
            LEFT
        }

        private struct IVector2
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
