using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace aoc_2024_day_15
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // read data
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.txt");
            string text = File.ReadAllText(filepath);

            MatchCollection matches = Regex.Matches(text, @"#+\S+#\s");
            IVector2 robotPosition = new IVector2(0, 0);

            // use char array so we can modify it when things move
            char[,] map = new char[matches[0].Length, matches.Count];

            for (int y = 0; y < matches.Count; ++y)
            {
                for (int x = 0; x < matches[0].Length; ++x)
                {
                    map[x, y] = matches[y].Value[x];

                    if (map[x, y] == '@')
                    {
                        robotPosition.x = x;
                        robotPosition.y = y;
                    }
                }
            }

            // Join all commands into single string
            string commandsInput = string.Join("", Regex.Matches(text, @"[\^v<>]+").Select(x => x.Value).ToArray());

            // iterate through commands 
            foreach (var command in commandsInput)
            {
                Console.WriteLine(command);
                MoveInDirection(map, ref robotPosition, command);
                Console.WriteLine();
            }

            uint gpsTotal = 0;

            // Get GPS total
            for (int y = 0; y < matches.Count; ++y)
            {
                for (int x = 0; x < matches[0].Length; ++x)
                {
                    // if this is not a box, ignore
                    if (map[x, y] != 'O')
                        continue;

                    // get the GPS coord
                    // x + (y * 100)
                    // add this to the total of all gps box positions
                    gpsTotal += (uint)(x + (y * 100));
                }
            }

            Console.WriteLine(gpsTotal);
        }

        private static void MoveInDirection(char[,] map, ref IVector2 robotPosition, char direction)
        {
            IVector2 delta = new IVector2(0, 0);
            switch (direction)
            {
                case 'v': 
                    delta = new IVector2(0, 1);
                    break;
                case '<':
                    delta = new IVector2(-1, 0);
                    break;
                case '>':
                    delta = new IVector2(1, 0);
                    break;
                default:
                    delta = new IVector2(0, -1);
                    break;
            }

            if (MoveRobotAndBoxesRecursive(map, robotPosition, delta))
            {
                robotPosition.x += delta.x;
                robotPosition.y += delta.y;
            }

            //PrintMap(map);
        }

        private static bool MoveRobotAndBoxesRecursive(char[,] map, IVector2 position, IVector2 delta)
        {
            bool canMove = false;

            // if next is a box
            if (map[position.x + delta.x, position.y + delta.y] == 'O')
                canMove = MoveRobotAndBoxesRecursive(map, position + delta, delta);

            // if next is empty space
            if (map[position.x + delta.x, position.y + delta.y] == '.')
                canMove = true;

            if (canMove)
            {
                map[position.x + delta.x, position.y + delta.y] = map[position.x, position.y];
                map[position.x, position.y] = '.';
            }

            return canMove;
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

    internal struct IVector2
    {
        public int x { get; set; }
        public int y { get; set; }
        public IVector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public static IVector2 operator +(IVector2 a, IVector2 b)
        {
            return new IVector2(a.x + b.x, a.y + b.y);
        }
    }
}
