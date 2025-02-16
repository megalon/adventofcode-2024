namespace aoc_2024_day_13
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // read in data
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.txt");
            string data = File.ReadAllText(filepath);

            Console.WriteLine(data);

            // parse into some kind of collection


            // iterate over claw machines
            {
                // iterate, adding each time
                // max 100 iterations
                {
                    // do some fancy math to find the divisors
                }

                // If not possible to reach target, ignore

                // calculate cost
            }

            // print total cost
        }

        // class ClawMachine
            // IVector2 buttonA
            // IVector2 buttonB
            // IVector2 target

        // class IVector2
            // x, y
    }
}
