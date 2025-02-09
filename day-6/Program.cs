using System.Numerics;

namespace aoc_2024_day_6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.txt");
            string[] lines = File.ReadAllLines(filepath);
            Vector2Int guardPosition = new Vector2Int(0, 0);
            MovementChoice currentDirection = MovementChoice.UP;

            for (int y = 0; y < lines.Length; ++y)
            {
                for (int x = 0; x < lines[0].Length; ++x)
                { 
                    if (lines[y][x].Equals('^')) guardPosition = new Vector2Int(x, y);

                    Console.Write(lines[y][x]);
                }
                Console.WriteLine();
            }

            while (currentDirection != MovementChoice.FINISH)
            {
                currentDirection = Move(lines, ref guardPosition, currentDirection);
            }
        }

        private static MovementChoice Move(string[] lines, ref Vector2Int guardPosition, MovementChoice direction)
        {
            Vector2Int delta = new Vector2Int(0, 0);

            MovementChoice nextMove;

            switch (direction)
            {
                case MovementChoice.UP: delta = new Vector2Int(0, -1); nextMove = MovementChoice.RIGHT; break;
                case MovementChoice.DOWN: delta = new Vector2Int(0, 1); nextMove = MovementChoice.LEFT; break;
                case MovementChoice.LEFT: delta = new Vector2Int(-1, 0); nextMove = MovementChoice.UP; break;
                case MovementChoice.RIGHT: delta = new Vector2Int(1, 0); nextMove = MovementChoice.DOWN; break;
                default: return MovementChoice.FINISH;
            }

            Vector2Int nextPosition = new Vector2Int(0, 0);

            while (true)
            {
                nextPosition = guardPosition + delta;

                if (nextPosition.x < 0
                 || nextPosition.y < 0
                 || nextPosition.x > lines[0].Length - 1
                 || nextPosition.y > lines.Length - 1)
                {
                    return MovementChoice.FINISH;
                }

                // If we hit an obstacle
                if (lines[nextPosition.y][nextPosition.x] == '#')
                {
                    break;
                }

                // Oh this doesn't work because strings are immutable...
                //lines[guardPosition.y][guardPosition.x] = 'X';

                guardPosition += delta;

                // This is just for printing the movement to console
                for (int y = 0; y < lines.Length; ++y)
                {
                    for (int x = 0; x < lines[0].Length; ++x)
                    {
                        if (lines[y][x].Equals('^'))
                        {
                            Console.Write('.');
                        } else if (y == guardPosition.y && x == guardPosition.x)
                        {
                            Console.Write('^');
                        }
                        else
                        {
                            Console.Write(lines[y][x]);
                        }
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();

            }

            return nextMove;
        }

        private struct Vector2Int
        {
            public int x { get; set; }
            public int y { get; set; }

            public Vector2Int(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            // I think I'm overengineering this puzzle haha
            public static Vector2Int operator +(Vector2Int a, Vector2Int b) => new Vector2Int(a.x + b.x, a.y + b.y);
        }

        private enum MovementChoice
        {
            UP,
            DOWN,
            LEFT,
            RIGHT,
            FINISH
        }
    }
}
