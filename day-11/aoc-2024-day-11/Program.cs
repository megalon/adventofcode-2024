using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace aoc_2024_day_11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // read in data
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.txt");
            string data = File.ReadAllText(filepath).Trim();

            Console.WriteLine(data);

            // build a list of ulong
            List<ulong> stones = new List<ulong>();

            foreach (Match match in Regex.Matches(data, @"\d+")) {
                stones.Add(ulong.Parse(match.Value));
            }

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Console.WriteLine("Part 1 total: " + Part1(25, new List<ulong>(stones)));
            stopWatch.Stop();
            PrintRuntime(stopWatch);

            for (int i = 25; i < 75; ++i)
            {
                stopWatch.Reset();
                stopWatch.Start();
                Console.WriteLine($"Part 2 total for {i} iterations: {Part2(i, new List<ulong>(stones))}");
                stopWatch.Stop();
                PrintRuntime(stopWatch);
            }
        }

        private static void PrintRuntime(Stopwatch stopWatch)
        {
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine("RunTime " + String.Format(
                    "{0:00}:{1:00}.{2:00}",
                    ts.Minutes,
                    ts.Seconds,
                    ts.Milliseconds / 10
                )
            );
        }

        private static int Part1(int n, List<ulong> stones)
        {
            for (int i = 0; i < n; ++i)
            {
                Console.WriteLine("Iteration: " + i);

                // iterate through stones list and check rules
                for (int stoneIdx = 0; stoneIdx < stones.Count; ++stoneIdx)
                {
                    if (stones[stoneIdx] == 0)
                    {
                        stones[stoneIdx] = 1;
                        continue;
                    }

                    // Count digits
                    int numDigits = CountDigits(stones[stoneIdx]);

                    // if even number of digits
                    if (numDigits % 2 == 0)
                    {
                        // split stone in half
                        // left digits go to left stone
                        // right digits go to right stone

                        // ** This sort of thing would not work because of percision issues
                        // Floor returns a double for example
                        // Math.Floor(stones[stoneIdx] / Math.Pow(10, numDigits / 2));

                        // Convert to a string and split so we can parse the string to a ulong
                        string s = stones[stoneIdx].ToString();

                        string left = s.Substring(0, numDigits / 2);
                        string right = s.Substring(numDigits / 2);

                        stones[stoneIdx] = ulong.Parse(left);
                        stones.Insert(stoneIdx + 1, ulong.Parse(right));

                        // Skip over the stone we just added
                        ++stoneIdx;

                        continue;
                    }

                    // other rules didn't apply
                    // so multiply by 2024
                    stones[stoneIdx] *= 2024;
                }

                //Console.WriteLine(String.Join(' ', stones));
            }

            return stones.Count;
        }

        private static ulong Part2(int n, List<ulong> stones)
        {
            ulong total = 0;

            // Recursive call
            for (int i = 0; i < stones.Count; ++i)
            {
                total += Part2Recursive(0, n, stones[i]);
            }

            return total;
        }

        // Create a map for what each stone splits into so we don't have to recalculate it
        private static Dictionary<ulong, StoneInfo> hashmap = new Dictionary<ulong, StoneInfo>();

        private static ulong Part2Recursive(int depth, int targetDepth, ulong stone)
        {
            if (depth == targetDepth)
            {
                return 1;
            }

            ++depth;

            if (stone == 0)
                return Part2Recursive(depth, targetDepth, 1);

            if (hashmap.ContainsKey(stone)) {

                if (hashmap[stone].node.right == null)
                {
                    return Part2Recursive(depth, targetDepth, hashmap[stone].node.left);
                }

                return Part2Recursive(depth, targetDepth, hashmap[stone].node.left)
                    + Part2Recursive(depth, targetDepth, (ulong)hashmap[stone].node.right);
            }

            // Count digits
            int numDigits = CountDigits(stone);

            // if even number of digits
            if (numDigits % 2 == 0)
            {
                // split stone in half
                // left digits go to left stone
                // right digits go to right stone

                // Convert to a string and split so we can parse the string to a ulong
                string s = stone.ToString();

                ulong left = ulong.Parse(s.Substring(0, numDigits / 2));
                ulong right = ulong.Parse(s.Substring(numDigits / 2));

                hashmap.Add(stone, new StoneInfo(new Node(left, right)));

                return Part2Recursive(depth, targetDepth, left)
                    + Part2Recursive(depth, targetDepth, right);
            }

            // other rules didn't apply
            // so multiply by 2024
            return Part2Recursive(depth, targetDepth, stone * 2024);
        }

        public struct StoneInfo
        {
            public Node node;

            /// <summary>
            /// key: depth
            /// value: how many stones will get created from this depth up to the max
            /// 
            /// This prevents us from having to recurse through this number again
            /// since we already know how many stones will be made
            /// </summary>
            public Dictionary<int, ulong> depthMap;

            public StoneInfo(Node node)
            {
                this.node = node;
                depthMap = new Dictionary<int, ulong>();
            }
        }

        public struct Node
        {
            public ulong left { get; }
            public ulong? right { get; }
            public Node(ulong left, ulong? right)
            {
                this.left = left;
                this.right = right;
            }
        } 

        private static int CountDigits(ulong number)
        {
            int digits = 1;
            while ((number /= 10) > 0)
            {
                ++digits;
            }

            return digits;
        }
    }
}
