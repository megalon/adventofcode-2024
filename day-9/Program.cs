namespace aoc_2024_day_9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // read in data
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.txt");
            string diskmap = File.ReadAllText(filepath);

            Console.WriteLine(diskmap);

            string data = String.Empty;

            // parse diskmap into a string
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
                    data += "" + id;

                for (int j = 0; j < freeSpace; ++j)
                    data += ".";
            }

            Console.WriteLine(data);

            int endIndex = data.Length - 1;
            string dataDefragged = String.Empty;

            // iterate through string
            for (int i = 0; i < endIndex; ++i)
            {
                // take char from end and place in empty space at front
                if (data[i] != '.')
                {
                    dataDefragged += data[i];
                    continue;
                }

                while (data[endIndex] == '.') {
                    --endIndex;
                }

                dataDefragged += data[endIndex--];
            }

            for (int i = endIndex; i < data.Length; ++i)
            {
                dataDefragged += '.';
            }

            Console.WriteLine(dataDefragged);
        }
    }
}
