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

            foreach (char c in antennaMap.Keys)
            {
                Console.WriteLine($"Antenna map for {c} : {antennaMap[c].ToString()}");

                foreach (IVector2 v in antennaMap[c].antennas)
                {
                    matrix[v.x, v.y] = c;
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

    internal class IVector2
    {
        public int x;
        public int y;

        public IVector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return "(" + x + ", " + y + ")";
        }
    }
}
