using System.Reflection.Metadata.Ecma335;

namespace aoc_2024_day_8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Read in data
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.txt");
            string[] data = File.ReadAllLines(filepath);

            Dictionary<char, AntennaCollection> antennaMap = new Dictionary<char, AntennaCollection>();

            char[,] matrix = new char[data[0].Length, data.Length];

            // Iterate through data and create a dictionary containing all the antenna locations
            for (int y = 0; y < data.Length; ++y) {
                for (int x = 0; x < data[y].Length; ++x) {
                    char c = data[y][x];

                    matrix[x, y] = '.';

                    if (c == '.') continue;

                    if (antennaMap.ContainsKey(c))
                    {
                        antennaMap[c].Add(new IVector2(x, y));
                    } else
                    {
                        antennaMap[c] = new AntennaCollection(new IVector2(x, y));
                    }
                }
            }

            List<IVector2> allAntinodes = new List<IVector2>();

            foreach (char c in antennaMap.Keys)
            {
                Console.WriteLine($"Antenna map for {c} : {antennaMap[c].ToString()}");

                foreach (IVector2 v in antennaMap[c].antennas)
                {
                    matrix[v.x, v.y] = c;
                }

                // Calculate antinode locations based on pairs
                antennaMap[c].CalculateAntinodes();

                // Count pairs and add to total
                foreach (IVector2 v in antennaMap[c].antinodes)
                {
                    // Check if antinode is inside the bounds of the map
                    if (v.x < 0
                        || v.x >= data[0].Length
                        || v.y < 0
                        || v.y >= data.Length)
                    {
                        continue;
                    }

                    Console.WriteLine($"Antinode for {c}: {v.ToString()}");

                    if (matrix[v.x, v.y] == '.')
                        matrix[v.x, v.y] = '#';

                    if (!allAntinodes.Contains(v))
                    {
                        allAntinodes.Add(v);
                    }
                }
            }

            for (int y = 0; y < matrix.GetLength(1); ++y)
            {
               for (int x = 0; x < matrix.GetLength(0); ++x)
                {
                    Console.Write(matrix[x, y]);
                }
                Console.WriteLine();
            }

            // Display total
            Console.WriteLine("Total: " + allAntinodes.Count);
        }
    }

    internal class AntennaCollection
    {
        public List<IVector2> antennas { get; }
        public List<IVector2> antinodes { get; }

        public AntennaCollection(IVector2 antenna) {
            antennas = new List<IVector2>();
            antinodes = new List<IVector2>();

            Add(antenna);
        }

        public void Add(IVector2 antenna)
        {
            antennas.Add(antenna);
        }

        public void CalculateAntinodes()
        {
            IVector2 v1, v2, delta;
            for (int i = 0; i < antennas.Count; ++i)
            {
                v1 = antennas[i];

                for (int j = i + 1; j < antennas.Count; ++j)
                {
                    v2 = antennas[j];
                    delta = new IVector2(v2.x - v1.x, v2.y - v1.y);

                    antinodes.Add(new IVector2(v1.x - delta.x, v1.y - delta.y));
                    antinodes.Add(new IVector2(v2.x + delta.x, v2.y + delta.y));
                }
            }

            antennas.Sort();
            antinodes.Sort();

            Console.WriteLine("antinodes count: " + antinodes.Count);
        }

        public override string ToString()
        {
            string output = string.Empty;

            foreach (IVector2 antenna in antennas)
            {
                output += antenna.ToString() + " ";
            }
            return output;
        }
    }

    internal class IVector2 : IEquatable<IVector2>, IComparable<IVector2>
    {
        public int x;
        public int y;

        public IVector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int CompareTo(IVector2? other)
        {
            if (other == null) return 1;

            return y == other.y
                 ? x - other.x
                 : y - other.y;
        }

        public bool Equals(IVector2? other)
        {
            if (other == null) return false;

            return CompareTo(other) == 0;
        }

        public override string ToString()
        {
            return "(" + x + ", " + y + ")";
        }
    }
}
