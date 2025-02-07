namespace aoc_2024_day_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string a, b, trimmedLine;

            List<int> listA = new List<int>();
            List<int> listB = new List<int>();
            
            string dataFileName = "data.txt";
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dataFileName);

            // open file
            // read each line
            foreach (string line in File.ReadLines(filePath))
            {
                trimmedLine = line.Trim();

                // parse 1st number and place into list a
                a = trimmedLine.Substring(0, trimmedLine.IndexOf(" "));

                listA.Add(int.Parse(a));

                // parse 2nd number and place into list b
                b = trimmedLine.Substring(trimmedLine.Trim().LastIndexOf(" ") + 1);

                listB.Add(int.Parse(b));


                //Console.WriteLine($"\"{a}\", \"{b}\"");
            }

            // sort each list
            listA.Sort();
            listB.Sort();

            long total = 0;

            // iterate through list a
            for (int i = 0; i < listA.Count; ++i)
            {
                // total += abs(a[i] - b[i])
                total += Math.Abs(listA[i] - listB[i]);
            }

            // print total
            Console.WriteLine(total);
        }
    }
}
