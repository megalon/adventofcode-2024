using static System.Runtime.InteropServices.JavaScript.JSType;

namespace aoc_2024_day_9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // read in data
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.txt");
            string diskmap = File.ReadAllText(filepath).Trim();

            //Console.WriteLine(diskmap);

            int blockCount = diskmap.Sum(c => c - '0');
            int[] data = new int[blockCount];
            int cursor = 0;

            // parse diskmap
            for (int i = 0, id = 0; i < diskmap.Length; i += 2, ++id)
            {
                // diskmap is a string of pairs of digits
                // 1st digit is file length
                // 2nd digit is free space
                // 12 = file length 1, free space 2 -> "0.."

                int filelength = int.Parse("" + diskmap[i]);
                int freeSpace = 0;

                if (i + 1 < diskmap.Length)
                    freeSpace = int.Parse("" + diskmap[i + 1]);

                for (int j = 0; j < filelength; ++j)
                    data[cursor + j] = id;

                cursor += filelength;

                for (int j = 0; j < freeSpace; ++j)
                    data[cursor + j] = -1;

                cursor += freeSpace;
            }

            LogData(data);

            Console.WriteLine("-- OUTPUT --");

            //Part1(data);

            Part2(data);
        }

        private static void Part1(int[] data)
        {
            int endIndex = data.Length - 1;

            for (int i = 0; i < endIndex; ++i)
            {
                if (data[i] != -1)
                {
                    continue;
                }

                while (data[endIndex] == -1)
                {
                    --endIndex;
                }

                if (endIndex < i) break;

                // take char from end and place in empty space at front
                data[i] = data[endIndex];
                data[endIndex] = -1;
                --endIndex;

                //LogData(data);
            }

            Console.WriteLine("Part 1 Total: " + Total(data));
        }

        private static void Part2(int[] data)
        {
            int emptyLength, fileSize, endIndexId;

            List<int> ids = new List<int>();

            for (int endIndex = data.Length - 1; endIndex >= 0; --endIndex)
            {
                // Find a file (not empty space)
                if (data[endIndex] == -1) 
                    continue;

                endIndexId = data[endIndex];

                if (ids.Contains(endIndexId))
                    continue;

                ids.Add(endIndexId);

                // Get file size
                fileSize = 0;
                while (endIndex - fileSize > 0 && data[endIndex - fileSize] == endIndexId)
                {
                    ++fileSize;
                }

                if (endIndex - fileSize < 0)
                    continue;

                // Search for empty space, starting from the front
                for (int i = 0; i < endIndex; ++i)
                {
                    if (data[i] != -1)
                        continue;

                    // Get the amount of available empty space
                    emptyLength = 0;
                    while (data[i + emptyLength] == -1)
                    {
                        ++emptyLength;
                    }

                    // If file doesn't fit,
                    // keep looking for an empty space that does
                    if (fileSize > emptyLength)
                    {
                        i += emptyLength;
                        continue;
                    }

                    // Move file to the empty space
                    for (int f = 0; f < fileSize; ++f)
                    {
                        data[i + f] = endIndexId;
                        data[endIndex - f] = -1;
                    }

                    //LogData(data);
                    
                    break;
                }

                // Need to add one because the loop subtracts one
                endIndex -= fileSize - 1;
            }

            LogData(data);

            Console.WriteLine("Part 2 Total: " + Total(data));
        }

        private static ulong Total(int[] data)
        {
            ulong total = 0;

            for (int i = 0; i < data.Length; ++i)
            {
                if (data[i] == -1) continue;

                ulong val = (ulong)data[i] * (ulong)i;

                //Console.WriteLine($"{data[i]} * {i} = {val}");

                total += val;
            }

            return total;
        }

        private static void LogData(int[] data)
        {
            foreach (int d in data)
            {
                Console.Write("" + (d == -1 ? "." : d));
            }

            Console.WriteLine();
        }
    }
}
