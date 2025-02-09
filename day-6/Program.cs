namespace aoc_2024_day_6
{
    internal class Program
    {
        private static int loopCounter = 0;
        private static Vector2Int startingPos;

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

            startingPos = guardPosition;

            Stack<CollisionPoint> collisionPoints = new Stack<CollisionPoint>();
            collisionPoints.Push(MoveStraight(matrix, ref guardPosition, Direction.UP));

            while (collisionPoints.Peek().collisionType != CollisionType.OUT_OF_BOUNDS)
            {
                Console.WriteLine($"Collided at ({collisionPoints.Peek().pos.x}, {collisionPoints.Peek().pos.y}) moving {collisionPoints.Peek().directionWhenHit.ToString()}");
                
                collisionPoints.Push(MoveStraight(matrix, ref guardPosition, collisionPoints.Peek().nextDirection));
            }

            Console.WriteLine($"Finished at ({collisionPoints.Peek().pos.x}, {collisionPoints.Peek().pos.y}) moving {collisionPoints.Peek().directionWhenHit.ToString()}");

            int total = 0;
            foreach (char c  in matrix)
            {
                if (c == 'X') ++total;
            }

            Console.WriteLine("Part 1: " + total);
            Console.WriteLine("Part 2: " + loopCounter);
        }

        private static CollisionPoint MoveStraight(char[,] matrix, ref Vector2Int guardPosition, Direction direction, bool loopCheck = false)
        {
            Vector2Int delta = new Vector2Int(0, 0);

            if (!loopCheck)
                matrix[guardPosition.x, guardPosition.y] = 'X';

            switch (direction)
            {
                case Direction.UP: delta = new Vector2Int(0, -1); break;
                case Direction.DOWN: delta = new Vector2Int(0, 1); break;
                case Direction.LEFT: delta = new Vector2Int(-1, 0); break;
                case Direction.RIGHT: delta = new Vector2Int(1, 0); break;
            }
            
            Vector2Int nextPosition = new Vector2Int(0, 0);

            while (true)
            {
                nextPosition = guardPosition + delta;

                //PrintMovement(matrix, guardPosition);

                switch (CheckCollision(matrix, nextPosition))
                {
                    case CollisionType.OUT_OF_BOUNDS:
                        return new CollisionPoint(guardPosition, direction, CollisionType.OUT_OF_BOUNDS);
                    case CollisionType.OBJECT:
                        return new CollisionPoint(guardPosition, direction, CollisionType.OBJECT);
                    default:
                        if (!loopCheck && nextPosition != startingPos)
                        {
                            // Store old character at the new position so we can set it to an obstacle temporarily
                            char oldChar = matrix[nextPosition.x, nextPosition.y];
                            matrix[nextPosition.x, nextPosition.y] = '#';

                            Stack<CollisionPoint> collisionPoints = new Stack<CollisionPoint>();

                            Vector2Int tempPositionTracker = new Vector2Int(guardPosition.x, guardPosition.y);  
                            
                            Console.WriteLine($"Checking for loops at {guardPosition.x}, {guardPosition.y}");

                            collisionPoints.Push(MoveStraight(matrix, ref tempPositionTracker, CollisionPoint.CalculateNextDirection(direction), true));

                            // Keep going until we go out of bounds or connect back with a previous point (a loop!)
                            while (collisionPoints.Peek().collisionType != CollisionType.OUT_OF_BOUNDS)
                            {
                                CollisionPoint point = MoveStraight(matrix, ref tempPositionTracker, collisionPoints.Peek().nextDirection, true);

                                if (collisionPoints.Where(p => p == point).Any())
                                {
                                    ++loopCounter;
                                    break;
                                }

                                collisionPoints.Push(point);
                            }

                            matrix[nextPosition.x, nextPosition.y] = oldChar;
                        }

                        guardPosition += delta;

                        if (!loopCheck)
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

        private static void PrintMovement(char[,] matrix, Vector2Int cursorPos)
        {
            // Print movement to console
            for (int y = 0; y < matrix.GetLength(1); ++y)
            {
                for (int x = 0; x < matrix.GetLength(0); ++x)
                {
                    if (cursorPos.x == x && cursorPos.y == y)
                    {
                        Console.Write("*");
                    } else
                    {
                        Console.Write(matrix[x, y]);
                    }
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
            public static bool operator ==(Vector2Int a, Vector2Int b)
            {
                return a.x == b.x
                    && a.y == b.y;
            }

            public static bool operator !=(Vector2Int a, Vector2Int b)
            {
                return a.x != b.x
                    && a.y != b.y;
            }
        }

        private struct CollisionPoint
        {
            public Vector2Int pos { get; }
            public Direction directionWhenHit { get; }
            public Direction nextDirection { get { return CalculateNextDirection(directionWhenHit); } }
            public CollisionType collisionType { get; }

            public CollisionPoint(Vector2Int pos, Direction directionWhenHit, CollisionType collisionType)
            {
                this.pos = pos;
                this.directionWhenHit = directionWhenHit;
                this.collisionType = collisionType;
            }

            public static bool operator ==(CollisionPoint a, CollisionPoint b)
            {
                return a.pos == b.pos
                    && a.directionWhenHit == b.directionWhenHit;
            }

            public static bool operator !=(CollisionPoint a, CollisionPoint b)
            {
                return a.pos.x != b.pos.x
                    || a.directionWhenHit != b.directionWhenHit;
            }

            public static Direction CalculateNextDirection(Direction choice)
            {
                switch (choice)
                {
                    case Direction.DOWN: return Direction.LEFT;
                    case Direction.LEFT: return Direction.UP;
                    case Direction.RIGHT: return Direction.DOWN;
                    default: return Direction.RIGHT;
                }
            }
        }

        public enum Direction
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
            OUT_OF_BOUNDS,
            LOOP_BARRIER
        }
    }
}
