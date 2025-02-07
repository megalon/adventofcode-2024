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

                bool isSafe = firstUnsafeLevel == -1;

                // if a level was found to be unsafe
                if (!isSafe)
                {
                    isSafe = IsSafeUsingProblemDampener(levelsList, firstUnsafeLevel);

                    if (!isSafe) continue;
                }

                ++totalSafeReports;
            }

            Console.WriteLine(totalSafeReports);
        }

        private static bool IsSafeUsingProblemDampener(List<int> levelsList, int firstUnsafeLevel)
        {
            List<int> modifiedLevelsList = new List<int>(levelsList);

            // Try removing the unsafe level
            modifiedLevelsList.RemoveAt(firstUnsafeLevel);

            // check safety again
            int unsafeLevel = -1;
            unsafeLevel = FindFirstUnsafeLevel(modifiedLevelsList);

            if (unsafeLevel == -1) return true;

            // If still unsafe, reset and try removing another one
            modifiedLevelsList = new List<int>(levelsList);

            // Try removing the level after this one,
            // and if we can't then try removing the level before this one
            if (firstUnsafeLevel + 1 < levelsList.Count)
            {
                modifiedLevelsList.RemoveAt(firstUnsafeLevel + 1);

                // check safety again
                unsafeLevel = FindFirstUnsafeLevel(modifiedLevelsList);
            }
            else if (firstUnsafeLevel > 0) // Try removing the level before this one
            {
                modifiedLevelsList.RemoveAt(firstUnsafeLevel - 1);

                // check safety again
                unsafeLevel = FindFirstUnsafeLevel(modifiedLevelsList);
            }

            return unsafeLevel == -1;
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

        //private static int TestSafety(int level1, int level2)
        //{

        //}
    }
}
