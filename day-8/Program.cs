namespace aoc_2024_day_8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Read in data
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.txt");
            string[] data = File.ReadAllLines(filepath);

            // Iterate through data and create a dictionary containing all the antenna locations
            foreach (string line in data) {
                Console.WriteLine(line);
            }

            // Iterate through antenna locations dictionary
            {
                // Calculate anode locations based on pairs

                // Count pairs and add to total
            }

            // Display total
        }
    }
}
