using System.Text.RegularExpressions;

namespace aoc_2024_day_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.txt");

            string text = File.ReadAllText(filepath);

            int width = text.Substring(0, text.IndexOf('\n')).Trim().Length;
            int height = Regex.Count(text, @"\n") + 1;

            Console.WriteLine(width + " " +  height);

            // iterate through every character in data
            foreach (char c in text)
            {
                // check if character is an X

                // right

                // left

                // up

                // down

                // diag up right

                // diag up left

                // diag down right

                // diag down left
            }

            // log total
        }
    }
}
