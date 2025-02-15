namespace aoc_2024_day_10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.txt");
            string[] data = File.ReadAllLines(filepath);

            int[,] map = new int[data[0].Length, data.Length];

            int total = 0;

            // Loop through map and find trail heads (0)
            for (int y = 0; y < data.Length; ++y)
            {
                for (int x = 0; x < data[0].Length; ++x)
                {
                    Console.Write(data[y][x]);

                    if (data[y][x] < '0' || data[y][x] > '9') continue;

                    int val = int.Parse("" + data[y][x]);
                    
                    if (val != 0) continue;

                    // Depth first search until we find trail end (9)
                    // Probably recursive call here
                    // findTrails(map, x, y)
                    total += FindTrails(data, x, y);

                    // Add number of trails to total
                }
                Console.WriteLine();
            }

            Console.WriteLine("Total = " + total);
        }

        private static int FindTrails(string[] data, int x, int y, char? previousVal = null)
        {
            if(x < 0 || y < 0 || x >= data[0].Length || y >= data.Length)
            {
                return 0;
            }

            if (data[y][x] == '9') return 1;

            if (previousVal != null && data[y][x] != previousVal + 1) return 0;

            return FindTrails(data, x, y - 1, data[y][x]) // up
                 + FindTrails(data, x, y + 1, data[y][x]) // down
                 + FindTrails(data, x - 1, y, data[y][x]) // left
                 + FindTrails(data, x + 1, y, data[y][x]); // right
        }
    }
}
