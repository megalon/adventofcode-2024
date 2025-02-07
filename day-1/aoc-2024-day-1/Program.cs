namespace aoc_2024_day_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // open file
            string dataFileName = "data.txt";

            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dataFileName);

            // read each line
            foreach (string line in File.ReadLines(filePath))
            {
                Console.WriteLine(line);
                // parse 1st number and place into list a
                // parse 2nd number and place into list b
            }

            // sort each list

            // iterate through list a
            {
                // total += abs(a[i] - b[i])
            }

            // print total
        }
    }
}
