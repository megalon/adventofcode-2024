using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace aoc_2024_day_11
{
    internal class Program
    {
        private static Dictionary<ulong, StoneInfo> hashmap = new Dictionary<ulong, StoneInfo>();

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

            stopWatch.Reset();
            stopWatch.Start();
            Console.WriteLine($"Part 2 total: {Part2(75, new List<ulong>(stones))}");
            stopWatch.Stop();
            PrintRuntime(stopWatch);
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

        // Main optimizations for part 2
        // 1: Cache the left and right splits for each unique stone
        // 2: Cache the final number of stones that a stone would generate from a depth to the max depth
        private static ulong Part2(int n, List<ulong> stones)
        {
            ulong total = 0;

            hashmap.Clear();

            // Rule 1:
            // If stone is 0, change it to 1
            hashmap.Add(0, new StoneInfo(new Node(1, null)));

            // Iterate through the input data to start the recusive calls
            for (int i = 0; i < stones.Count; ++i)
            {
                total += Part2Recursive(0, n, stones[i]);
            }

            return total;
        }

        private static ulong Part2Recursive(int depth, int targetDepth, ulong stone)
        {
            if (depth == targetDepth)
                return 1;

            ++depth;

            ulong result = 0;

            if (hashmap.ContainsKey(stone))
            {
                if (hashmap[stone].depthMap.ContainsKey(depth))
                {
                    return hashmap[stone].depthMap[depth];
                }

                // There might not be a right value if the stone had an odd number of digits
                if (hashmap[stone].node.right == null)
                {
                    result = Part2Recursive(depth, targetDepth, hashmap[stone].node.left);
                } else
                {
                    result = Part2Recursive(depth, targetDepth, hashmap[stone].node.left)
                        + Part2Recursive(depth, targetDepth, (ulong)hashmap[stone].node.right);
                }

                hashmap[stone].depthMap.TryAdd(depth, result);
                return result;
            }

            // Rule 2:
            // If stone has an even number of digits, split into two stones.
            // Left half of digits goes to left stone. 
            // Right half of digits goes to right stone
            // ex: 2024 -> 20, 24
            int numDigits = CountDigits(stone);
            if (numDigits % 2 == 0)
            {
                // Convert to a string and split so we can parse the string to a ulong
                string s = stone.ToString();

                ulong left = ulong.Parse(s.Substring(0, numDigits / 2));
                ulong right = ulong.Parse(s.Substring(numDigits / 2));

                hashmap.Add(stone, new StoneInfo(new Node(left, right)));

                result = Part2Recursive(depth, targetDepth, left)
                    + Part2Recursive(depth, targetDepth, right);

                hashmap[stone].depthMap.TryAdd(depth, result);
                return result;
            }

            // Rule 3:
            // If other rules didn't apply,
            // multiply by 2024

            // Add to hashmap before starting recursion
            if (!hashmap.ContainsKey(stone))
            {
                hashmap.Add(stone, new StoneInfo(new Node(stone * 2024, null)));
            }

            result = Part2Recursive(depth, targetDepth, stone * 2024);

            hashmap[stone].depthMap.TryAdd(depth, result);
            return result;
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
    }
}
