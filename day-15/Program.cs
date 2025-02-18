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
            IVector2 robot = new IVector2(0, 0);

            // use char array so we can modify it when things move
            char[,] map = new char[matches[0].Length, matches.Count];

            for (int y = 0; y < matches.Count; ++y)
            {
                for (int x = 0; x < matches[0].Length; ++x)
                {
                    map[x, y] = matches[y].Value[x];

                    if (map[x, y] == '@')
                    {
                        robot.x = x;
                        robot.y = y;
                    }
                }
            }

            // Join all commands into single string
            string commandsInput = string.Join("", Regex.Matches(text, @"[\^v<>]+").Select(x => x.Value).ToArray());

            // iterate through commands 
            foreach (var command in commandsInput)
            {
                // from robot position,
                // check tile in direction of movemet
                // if it's a wall
                //   continue
                // if it's a box
                //   check area in direction of movment + 1
                // if it's empty space
                //   move to that space, set the previous space to empty
            }

            // iterate through grid
            {
                // if this is not a box, ignore

                // get the GPS coord
                // x + (y * 100)

                // add this to the total of all gps box positions
            }

            // print total
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
    }
}
