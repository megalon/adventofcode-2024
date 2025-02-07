namespace aoc_2024_day_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // read in file

            string filename = "data.txt";
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);

            foreach (string line in File.ReadLines(filePath))
            {
                // parse all numbers in line into a list
                Console.WriteLine(line);

                // iterate through list
                {
                    // keep track of difference for each step
                }

                // if report is safe
                {
                    // increment safe counter
                }
            }

            // log result
        }
    }
}
