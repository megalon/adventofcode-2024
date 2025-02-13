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

            // Iterate through data and create a dictionary containing all the antenna locations
            for (int y = 0; y < data.Length; ++y) {
                for (int x = 0; x < data[y].Length; ++x) {
                    char c = data[y][x];

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

            foreach (char c in  antennaMap.Keys)
            {
                Console.WriteLine($"{c} : {antennaMap[c].ToString()}");
            }

            // Iterate through antenna locations dictionary
            {
                // Calculate anode locations based on pairs

                // Count pairs and add to total
            }

            // Display total
        }
    }

    internal class AntennaCollection
    {
        List<IVector2> antennas = new List<IVector2>();
        List<IVector2> antinodes = new List<IVector2>();

        public AntennaCollection(IVector2 antenna) {
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
