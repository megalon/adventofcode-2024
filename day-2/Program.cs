namespace aoc_2024_day_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // read in file

            string filename = "data.txt";
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);

            long totalSafeReports = 0;

            foreach (string line in File.ReadLines(filePath))
            {
                //Console.WriteLine(line);

                // parse all numbers in line into a list
                List<int> levelsList = new List<int>();
                
                foreach (string num in line.Split())
                {
                    levelsList.Add(int.Parse(num));
                }

                bool isSafe = true;
                int prevDistance = 0;

                // iterate through list
                for (int i = 1; i < levelsList.Count; ++i)
                {
                    int distance = levelsList[i] - levelsList[i - 1];

                    if (Math.Abs(distance) < 1 || Math.Abs(distance) > 3)
                    {
                        isSafe = false;
                        break;
                    }
                }

                // if report is safe
                if (isSafe)
                {
                    // increment safe counter
                    ++totalSafeReports;
                }
            }

            // log result
            Console.WriteLine(totalSafeReports);
        }
    }
}
