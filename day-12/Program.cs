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
                    Console.Write(map[x, y]);
                }
                Console.WriteLine();
            }

            // iterate through matrix
            {
                // when a valid character is found,
                // begin searching for area
                // probably use recursion
                {

                }
            }
        }

        // return count and area
        private static void Recursion(int x, int y, int area, int perimeter)
        {
            // if tile is not valid
            {
                // return count and 
            }

            // increment count

            // switch(direction)
            {
                // case up
                {
                    // recurse right
                    // recurse down
                    // recurse left
                }
                // ...
            }

            // return the total count and the area
        }
    }
}
