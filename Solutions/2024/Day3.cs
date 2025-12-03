using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Solutions.Year2024
{
    public partial class MullItOver
    {
        [GeneratedRegex(@"mul\((\d+),(\d+)\)")]
        public static partial Regex SplitMulsRegex();

        [GeneratedRegex(@"do\(\)")]
        public static partial Regex DoRegex();

        [GeneratedRegex(@"don't\(\)")]
        public static partial Regex DontRegex();

        public static uint Part1(string input)
        {
            int totalMulValue = 0;

            foreach (string line in input.Split('\n'))
            {
                totalMulValue += GetLineTotal(
                    SplitMulsRegex()
                        .Matches(line)
                        .Select(
                            (match) =>
                                new Tuple<int, int>(
                                    int.Parse(match.Groups[1].Value),
                                    int.Parse(match.Groups[2].Value)
                                )
                        )
                );
            }

            return (uint)(totalMulValue);
        }

        public static int GetLineTotal(IEnumerable<Tuple<int, int>> muls)
        {
            int total = 0;
            foreach (var mul in muls)
            {
                total += mul.Item1 * mul.Item2;
            }

            return total;
        }

        public static uint Part2(string input)
        {
            int totalMulValue = 0;

            MatchCollection mulMatches = SplitMulsRegex().Matches(input);
            MatchCollection doMatches = DoRegex().Matches(input);
            MatchCollection dontMatches = DontRegex().Matches(input);

            List<bool> toggles = [];

            foreach (Match dontMatch in dontMatches)
            {
                if (dontMatch.Index > toggles.Count)
                {
                    toggles.AddRange(Enumerable.Repeat(true, dontMatch.Index - toggles.Count));
                    foreach (Match doMatch in doMatches)
                    {
                        if (doMatch.Index > toggles.Count)
                        {
                            toggles.AddRange(
                                Enumerable.Repeat(false, doMatch.Index - toggles.Count)
                            );
                            break;
                        }
                    }
                }
            }

            foreach (Match mulMatch in mulMatches)
            {
                if (mulMatch.Index >= toggles.Count || toggles[mulMatch.Index])
                {
                    totalMulValue +=
                        int.Parse(mulMatch.Groups[1].Value) * int.Parse(mulMatch.Groups[2].Value);
                }
            }

            return (uint)(totalMulValue);
        }
    }
}
