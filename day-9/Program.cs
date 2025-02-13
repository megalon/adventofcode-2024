namespace aoc_2024_day_9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // read in data
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.txt");
            string data = File.ReadAllText(filepath);

            Console.WriteLine(data);

            // parse diskmap into a string
            {
                // diskmap is a string of pairs of digits
                // 1st digit is file length
                // 2nd digit is free space
                // 12 = file length 1, free space 2 -> "0.."
            }

            // iterate through string
            {
                // take char from end and place in empty space at front
            }
        }
    }
}
