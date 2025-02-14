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

            Part1(data);

            LogData(data);

            Console.WriteLine("Part 1 Total: " + Total(data));
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
