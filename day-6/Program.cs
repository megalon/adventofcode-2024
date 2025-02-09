using System.Numerics;

namespace aoc_2024_day_6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.txt");
            string[] lines = File.ReadAllLines(filepath);
            
            char[,] matrix = new char[lines[0].Length, lines.Length];

            Vector2Int guardPosition = new Vector2Int(0, 0);
            MovementChoice currentMovement = MovementChoice.UP;

            // Build the char matrix
            for (int y = 0; y < matrix.GetLength(1); ++y)
            {
                for (int x = 0; x < matrix.GetLength(0); ++x)
                {
                    matrix[x, y] = lines[y][x];

                    if (matrix[x, y].Equals('^')) guardPosition = new Vector2Int(x, y);

                    Console.Write(lines[y][x]);
                }
                Console.WriteLine();
            }

            while (currentMovement != MovementChoice.FINISH)
            {
                currentMovement = Move(matrix, ref guardPosition, currentMovement);
            }

            int total = 0;
            foreach (char c  in matrix)
            {
                if (c == 'X') ++total;
            }

            Console.WriteLine(total);
        }

        private static MovementChoice Move(char[,] matrix, ref Vector2Int guardPosition, MovementChoice direction)
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

            matrix[guardPosition.x, guardPosition.y] = 'X';
            
            Vector2Int nextPosition = new Vector2Int(0, 0);

            while (true)
            {
                nextPosition = guardPosition + delta;

                if (nextPosition.x < 0
                 || nextPosition.y < 0
                 || nextPosition.x > matrix.GetLength(0) - 1
                 || nextPosition.y > matrix.GetLength(1) - 1)
                {
                    return MovementChoice.FINISH;
                }

                // If we hit an obstacle
                if (matrix[nextPosition.x, nextPosition.y] == '#')
                {
                    break;
                }

                guardPosition += delta;
                matrix[guardPosition.x, guardPosition.y] = 'X';

                // Print movement to console
                for (int y = 0; y < matrix.GetLength(1); ++y)
                {
                    for (int x = 0; x < matrix.GetLength(0); ++x)
                    {
                        Console.Write(matrix[x, y]);
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
