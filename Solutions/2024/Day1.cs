using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Solutions.Year2024
{
    public partial class HistorianHysteria
    {
        [GeneratedRegex(@"(\d+)   (\d+)")]
        public static partial Regex SplitNumberRegex();

        public static uint Part1(string input)
        {
            uint differenceScore = 0;

            List<int> left = [];
            List<int> right = [];

            foreach (Match match in SplitNumberRegex().Matches(input))
            {
                left.Add(int.Parse(match.Groups[1].Value));
                right.Add(int.Parse(match.Groups[2].Value));
            }

            left.Sort();
            right.Sort();

            for (int i = 0; i < left.Count; i++)
            {
                differenceScore += (uint)Math.Abs(left[i] - right[i]);
            }

            return differenceScore;
        }

        public static uint Part2(string input)
        {
            uint similarityScore = 0;

            List<uint> left = [];
            List<uint> right = [];

            foreach (Match match in SplitNumberRegex().Matches(input))
            {
                left.Add(uint.Parse(match.Groups[1].Value));
                right.Add(uint.Parse(match.Groups[2].Value));
            }

            foreach (uint leftNum in left)
            {
                similarityScore += (uint)(
                    leftNum * right.Count((uint rightNum) => (rightNum == leftNum))
                );
            }

            return similarityScore;
        }
    }
}
