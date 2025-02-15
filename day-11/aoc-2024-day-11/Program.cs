namespace aoc_2024_day_11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // read in data
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.txt");
            string data = File.ReadAllText(filepath);

            Console.WriteLine(data);

            // build a list of ulong

            // loop 25 times
            {
                // check rules

                // if stones[i] == 0
                {
                    // stones[i] = 1
                    // continue;
                }

                // count digits

                // if even number of digits
                {
                    // split in half
                    // insert left half before this index
                    // insert right half after this index

                    // continue
                }

                // other rules didn't apply
                // so multiply by 2024
            }

            // print stones count
        }
    }
}
