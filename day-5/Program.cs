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
                //Console.WriteLine(match.Groups[1].Value + " " + match.Groups[2].Value);

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
            foreach (Match match in Regex.Matches(text, @"([0-9]+,)+[0-9]+([\r\n]+|$)"))
            {
                // Convert strings to array of ints
                string[] pagesStringArray = match.Value.Trim().Split(',');
                int[] pagesArray = new int[pagesStringArray.Length];
                for (int i = 0; i < pagesStringArray.Length; ++i)
                {
                    pagesArray[i] = int.Parse(pagesStringArray[i]);
                }

                bool valid = true;

                // Iterate 
                for (int i = pagesArray.Length - 1; i >= 0; --i)
                {
                    int page = pagesArray[i];

                    // Check element against all preceding elements
                    for (int j = i - 1; j >= 0; --j)
                    {
                        // If the rules dictionary for this page
                        // contains a rule one of the elements before this in the list
                        // then this page is in the wrong order
                        if (rulesDict.TryGetValue(page, out List<int> list))
                        {
                            if (list.Contains(pagesArray[j]))
                            {
                                valid = false;
                                break;
                            }
                        }
                    }

                    if (!valid) break;
                }
                
                Console.WriteLine($"{match.Value.Trim()} -> {(valid ? "VALID" : "INVALID")}");

                if (!valid) continue;

                // find middle value

                // add middle value to total
            }

            // log total
        }
    }
}
