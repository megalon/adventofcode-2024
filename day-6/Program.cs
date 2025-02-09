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

            // Build the char matrix
            for (int y = 0; y < matrix.GetLength(1); ++y)
            {
                for (int x = 0; x < matrix.GetLength(0); ++x)
                {
                    matrix[x, y] = lines[y][x];

                    if (matrix[x, y].Equals('^')) guardPosition = new Vector2Int(x, y);

                    //Console.Write(lines[y][x]);
                }
                //Console.WriteLine();
            }

            Stack<CollisionPoint> collisionPoints = new Stack<CollisionPoint>();
            collisionPoints.Push(MoveStraight(matrix, ref guardPosition, MovementChoice.UP));

            while (collisionPoints.Peek().collisionType != CollisionType.OUT_OF_BOUNDS)
            {
                Console.WriteLine($"Collided at ({collisionPoints.Peek().pos.x}, {collisionPoints.Peek().pos.y}) moving {collisionPoints.Peek().movementWhenHit.ToString()}");
                
                collisionPoints.Push(MoveStraight(matrix, ref guardPosition, collisionPoints.Peek().nextMove));
            }

            Console.WriteLine($"Finished at ({collisionPoints.Peek().pos.x}, {collisionPoints.Peek().pos.y}) moving {collisionPoints.Peek().movementWhenHit.ToString()}");

            int total = 0;
            foreach (char c  in matrix)
            {
                if (c == 'X') ++total;
            }

            Console.WriteLine(total);
        }

        private static CollisionPoint MoveStraight(char[,] matrix, ref Vector2Int guardPosition, MovementChoice direction, bool leaveMark = true)
        {
            Vector2Int delta = new Vector2Int(0, 0);

            if (leaveMark)
                matrix[guardPosition.x, guardPosition.y] = 'X';

            switch (direction)
            {
                case MovementChoice.UP: delta = new Vector2Int(0, -1); break;
                case MovementChoice.DOWN: delta = new Vector2Int(0, 1); break;
                case MovementChoice.LEFT: delta = new Vector2Int(-1, 0); break;
                case MovementChoice.RIGHT: delta = new Vector2Int(1, 0); break;
            }
            
            Vector2Int nextPosition = new Vector2Int(0, 0);

            while (true)
            {
                nextPosition = guardPosition + delta;

                switch (CheckCollision(matrix, nextPosition))
                {
                    case CollisionType.OUT_OF_BOUNDS:
                        return new CollisionPoint(guardPosition, direction, CollisionType.OUT_OF_BOUNDS);
                    case CollisionType.OBJECT:
                        return new CollisionPoint(guardPosition, direction, CollisionType.OBJECT);
                    default:
                        guardPosition += delta;
                        if (leaveMark)
                            matrix[guardPosition.x, guardPosition.y] = 'X';
                        break;
                }
            }
        }

        private static CollisionType CheckCollision(char[,] matrix, Vector2Int nextPosition)
        {
            if (nextPosition.x < 0
             || nextPosition.y < 0
             || nextPosition.x > matrix.GetLength(0) - 1
             || nextPosition.y > matrix.GetLength(1) - 1)
            {
                return CollisionType.OUT_OF_BOUNDS;
            }

            if (matrix[nextPosition.x, nextPosition.y] == '#')
            {
                return CollisionType.OBJECT;
            }

            return CollisionType.NONE;
        }

        private void PrintMovement(char[,] matrix)
        {
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

        private struct CollisionPoint
        {
            public Vector2Int pos { get; }
            public MovementChoice movementWhenHit { get; }
            public MovementChoice nextMove { get { return CalculateNextMove(); } }
            public CollisionType collisionType { get; }

            public CollisionPoint(Vector2Int pos, MovementChoice movementWhenHit, CollisionType collisionType)
            {
                this.pos = pos;
                this.movementWhenHit = movementWhenHit;
                this.collisionType = collisionType;
            }

            private MovementChoice CalculateNextMove()
            {
                switch (movementWhenHit)
                {
                    case MovementChoice.DOWN: return MovementChoice.LEFT;
                    case MovementChoice.LEFT: return MovementChoice.UP;
                    case MovementChoice.RIGHT: return MovementChoice.DOWN;
                    default: return MovementChoice.RIGHT;
                }
            }
        }

        public enum MovementChoice
        {
            UP,
            DOWN,
            LEFT,
            RIGHT
        }

        public enum CollisionType
        {
            NONE,
            OBJECT,
            OUT_OF_BOUNDS
        }
    }
}
