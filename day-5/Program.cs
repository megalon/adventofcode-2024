namespace aoc_2024_day_5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Read input

            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.txt");

            string text = File.ReadAllText(filepath);

            Console.WriteLine(text);

            // Collect all page number rules "X|Y"
            // Build dictionary using these rules
            {

            }

            // Collect all lists of pages

            // Convert strings to list of ints
            {

            }

            // Iterate through lists
            {
                // valid = true

                // Iterate through each element in list
                {
                    // Check element against all other elements
                    // Using rules from rule dict

                    // If failure, valid = false, break
                }

                // if !valid: continue

                // find middle value

                // add middle value to total
            }

            // log total
        }
    }
}
