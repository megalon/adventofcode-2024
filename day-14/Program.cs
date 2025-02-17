namespace aoc_2024_day_14
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.txt");
            string[] lines = File.ReadAllLines(filepath);

            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
        }
    }
}
