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
                // parse all numbers in line into a list
                List<int> levelsList = new List<int>();
           
                foreach (string num in line.Split())
                {
                    levelsList.Add(int.Parse(num));
                }

                int firstUnsafeLevel = -1;
                firstUnsafeLevel = FindFirstUnsafeLevel(levelsList);

                // if a level was found to be unsafe
                if (firstUnsafeLevel != -1)
                {
                    if (!IsSafeUsingProblemDampener(levelsList)) continue;
                }

                ++totalSafeReports;
            }

            Console.WriteLine(totalSafeReports);
        }

        private static bool IsSafeUsingProblemDampener(List<int> levelsList)
        {
            for (int i = 0; i < levelsList.Count; ++i)
            {
                List<int> modifiedLevelsList = new List<int>(levelsList);

                modifiedLevelsList.RemoveAt(i);

                if (FindFirstUnsafeLevel(modifiedLevelsList) == -1) return true;
            }

            return false;
        }

        private static int FindFirstUnsafeLevel(List<int> levelsList)
        {
            int prevDistance = 0;

            for (int i = 1; i < levelsList.Count; ++i)
            {
                int distance = levelsList[i] - levelsList[i - 1];

                if (Math.Abs(distance) < 1 || Math.Abs(distance) > 3)
                {
                    return i - 1;
                }

                // Only compare if we have more than one distance
                if (i > 1)
                {
                    if (prevDistance > 0 && distance < 0
                        || prevDistance < 0 && distance > 0)
                    {
                        return i - 1;
                    }
                }

                prevDistance = distance;
            }

            return -1;
        }
    }
}
