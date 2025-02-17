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
            //IVector2 mapSize = new IVector2(101, 103);

            int[,] map = new int[mapSize.x, mapSize.y];

            List<Robot> robots = new List<Robot>();

            foreach (Match match in Regex.Matches(text, @"p=(-?\d+),(-?\d+) v=(-?\d+),(-?\d+)"))
            {
                IVector2 position = new IVector2(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
                IVector2 velocity = new IVector2(int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value));

                Console.WriteLine($"p:{position.ToString()} v:{velocity.ToString()}");

                robots.Add(new Robot(position, velocity));
            }

            Console.WriteLine($"Part 1: {Part1(map, robots, 100)}");
        }

        private static uint Part1(int[,] map, List<Robot> robots, int numSeconds)
        {
            IVector2 mapSize = new IVector2(map.GetLength(0), map.GetLength(1));

            for (int i = 0; i < numSeconds; ++i)
            {
                foreach (Robot robot in robots)
                {
                    robot.Move(mapSize);
                }
            }

            foreach (Robot robot in robots)
            {
                map[robot.position.x, robot.position.y] += 1;
            }
            
            PrintMap(map);

            return GetSafetyFactor(map);
        }

        // Count the number of robots in each quadrant of the map
        // ignore the exact middle row / column if the map is not evenly divisible
        // multiply the count in each quadrant together to get the final safety factor
        private static uint GetSafetyFactor(int[,] map)
        {
            uint safetyFactor = 1;

            int halfWidth = (int)Math.Floor((double)map.GetLength(0) / 2);
            int halfHeight = (int)Math.Floor((double)map.GetLength(1) / 2);

            IVector2 offset = IVector2.ZERO;

            for (int quad = 0; quad < 4; ++quad)
            {
                switch (quad)
                {
                    case 1:
                        offset = new IVector2(map.GetLength(0) - halfWidth, 0);
                        break;
                    case 2:
                        offset = new IVector2(0, map.GetLength(1) - halfHeight);
                        break;
                    case 3:
                        offset = new IVector2(map.GetLength(0) - halfWidth, map.GetLength(1) - halfHeight);
                        break;
                    default:
                        offset = IVector2.ZERO;
                        break;
                }

                int robotCount = 0;

                for (int y = 0; y < halfHeight; ++y)
                {
                    for (int x = 0; x < halfWidth; ++x)
                    {
                        robotCount += map[x + offset.x, y + offset.y];
                    }
                }

                safetyFactor *= (uint)robotCount;
            }

            return safetyFactor;
        }

        private class Robot
        {
            public IVector2 position { get; set;}
            public IVector2 velocity { get; set;}
            public IVector2 startPos { get; set;}
            public Robot(IVector2 position, IVector2 velocity)
            {
                this.position = position;
                this.velocity = velocity;
                startPos = position;
            }

            public void Move(IVector2 bounds)
            {
                // Move along velocity
                position.x += velocity.x;
                position.y += velocity.y;

                // Wrap around edges
                if (position.x >= bounds.x)
                    position.x %= bounds.x;
                if (position.y >= bounds.y)
                    position.y %= bounds.y;

                // If the value is negative, we need to move it back onto the map
                if (position.x < 0)
                    position.x = (position.x % bounds.x) + bounds.x;
                if (position.y < 0)
                    position.y = (position.y % bounds.y) + bounds.y;
            }
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
