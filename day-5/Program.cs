using System.Text.RegularExpressions;

namespace aoc_2024_day_5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Read input

            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.txt");

            string text = File.ReadAllText(filepath);

            Dictionary<int, List<int>> rulesDict = new Dictionary<int, List<int>>();

            // Collect all page number rules "X|Y"
            // Build dictionary using these rules
            foreach (Match match in Regex.Matches(text, @"([0-9]+)\|([0-9]+)"))
            {
                Console.WriteLine(match.Groups[1].Value + " " + match.Groups[2].Value);

                int page1 = int.Parse(match.Groups[1].Value);
                int page2 = int.Parse(match.Groups[2].Value);

                if (rulesDict.ContainsKey(page1))
                {
                    rulesDict[page1].Add(page2);
                } else
                {
                    rulesDict.Add(page1, new List<int> { page2 });
                }
            }

            // Collect all lists of pages
            // Convert strings to list of ints
            foreach (Match match in Regex.Matches(text, @"([0-9]+,)+[0-9]+([\r\n]+|$)"))
            {
                Console.WriteLine(match.Value.Trim());
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
