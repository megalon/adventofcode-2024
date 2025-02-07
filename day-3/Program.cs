namespace aoc_2024_day_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // read in data

            string filename = "data.txt";
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);

            string data = File.ReadAllText(filePath);

            Console.WriteLine(data);

            // get all the matches in a List using regex
            // regex something like "mul("[0-9]*","[0-9]*")"

            // iterate through list
            {
                // get 1st number (a)

                // get 2nd number (b)

                // total += a * b
            }

            // log total
        }
    }
}
