using System.Text.RegularExpressions;

namespace aoc_2024_day_14
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.txt");
            string text = File.ReadAllText(filepath);

            //IVector2 mapSize = new IVector2(11, 7); // Test map
            IVector2 mapSize = new IVector2(101, 103);

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

            Part1(map, robots, mapSize.x * mapSize.y);
        }

        private static uint Part1(int[,] map, List<Robot> robots, int numSeconds)
        {
            IVector2 mapSize = new IVector2(map.GetLength(0), map.GetLength(1));

            int numClusters = 0;

            for (int i = 1; i <= numSeconds; ++i)
            {
                foreach (Robot robot in robots)
                {
                    if (i > 1)
                        map[robot.position.x, robot.position.y] -= 1;

                    robot.Move(mapSize);
                    map[robot.position.x, robot.position.y] += 1;
                }


                bool found = false;

                // look for clusters of robots
                foreach (Robot robot in robots)
                {
                    int boxSize = 4;
                    int count = 0;

                    // If we are at the bounds, ignore
                    if (robot.position.x - boxSize < 0
                     || robot.position.y - boxSize < 0
                     || robot.position.x + boxSize > map.GetLength(0)
                     || robot.position.y + boxSize > map.GetLength(1))
                    {
                        continue;
                    }

                    for (int x = 0; x < boxSize; ++x)
                    {
                        for (int y = 0; y < boxSize; ++y)
                        {
                            if (map[robot.position.x + x, robot.position.y + y] > 0)
                            {
                                ++count;
                            }
                        }
                    }

                    // If the whole area is full of robots
                    if (count == boxSize * boxSize)
                    {
                        found = true;
                    }
                }

                if (found)
                {
                    Console.WriteLine($"Found tree after robots ran for: {numSeconds + 1} sec");
                    PrintMap(map);
                }

                // Looking for similar quadrants didn't work
                //uint q0 = ComputeQuadrant(map, 0);
                //uint q1 = ComputeQuadrant(map, 1);
                //uint q2 = ComputeQuadrant(map, 2);
                //uint q3 = ComputeQuadrant(map, 3);

                //if (Math.Abs(q0 - q1) <= 2 && Math.Abs(q2 - q3) <= 2)
                //{
                //    PrintMap(map);
                //}

            }

            return GetSafetyFactor(map);
        }
        // Count the number of robots in each quadrant of the map
        // ignore the exact middle row / column if the map is not evenly divisible
        // multiply the count in each quadrant together to get the final safety factor
        private static uint GetSafetyFactor(int[,] map)
        {
            uint safetyFactor = 1;

            for (int quad = 0; quad < 4; ++quad)
            {
                safetyFactor *= ComputeQuadrant(map, quad);
            }

            return safetyFactor;
        }

        private static uint ComputeQuadrant(int[,] map, int quadrant)
        {
            IVector2 offset = IVector2.ZERO;

            int halfWidth = (int)Math.Floor((double)map.GetLength(0) / 2);
            int halfHeight = (int)Math.Floor((double)map.GetLength(1) / 2);

            switch (quadrant)
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

            return (uint)robotCount;
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
