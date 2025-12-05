using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Solutions.Year2025
{
    public partial class Cafeteria
    {
        [GeneratedRegex(@"(\d+)-(\d+)")]
        public static partial Regex SplitIngredientIdRangeRegex();

        [GeneratedRegex(@"(\d+)")]
        public static partial Regex IngredientIdsRegex();

        private static void MergeRanges(ref List<(long, long)> ranges)
        {
            // ranges must be sorted on their begin values for this to work
            List<(long, long)> sortedRanges = [.. ranges.OrderBy(range => range.Item1)];
            List<(long, long)> newRanges = [];

            long newRangeBegin = -1;
            long newRangeEnd = -1;
            foreach ((long begin, long end) in sortedRanges)
            {
                if (begin > newRangeEnd + 1)
                {
                    if (newRangeBegin > -1 && newRangeEnd > -1)
                    {
                        newRanges.Add((newRangeBegin, newRangeEnd));
                    }

                    newRangeBegin = begin;
                    newRangeEnd = end;
                }
                else
                {
                    newRangeEnd = Math.Max(newRangeEnd, end);
                }
            }
            if (newRangeBegin > -1 && newRangeEnd > -1)
            {
                newRanges.Add((newRangeBegin, newRangeEnd));
            }

            ranges = newRanges;
        }

        public static long Part1(string input)
        {
            // double new line indicates next section of input
            string[] inputs = input.Split("\r\n\r\n");

            List<(long Begin, long End)> ranges = [];
            foreach (Match match in SplitIngredientIdRangeRegex().Matches(inputs[0]))
            {
                long newBegin = long.Parse(match.Groups[1].Value);
                long newEnd = long.Parse(match.Groups[2].Value);

                ranges.Add((newBegin, newEnd));

                // merge after each new range added
                MergeRanges(ref ranges);
            }

            long countFreshIds = 0;
            foreach (Match match in IngredientIdsRegex().Matches(inputs[1]))
            {
                long ingredientId = long.Parse(match.Value);

                foreach (var range in ranges)
                {
                    if (range.Begin <= ingredientId && ingredientId <= range.End)
                    {
                        countFreshIds++;
                        break;
                    }
                }
            }
            return countFreshIds;
        }

        public static long Part2(string input)
        {
            // double new line indicates next section of input
            string[] inputs = input.Split("\r\n\r\n");

            List<(long Begin, long End)> ranges = [];
            foreach (Match match in SplitIngredientIdRangeRegex().Matches(inputs[0]))
            {
                long newBegin = long.Parse(match.Groups[1].Value);
                long newEnd = long.Parse(match.Groups[2].Value);

                ranges.Add((newBegin, newEnd));

                // merge after each new range added
                MergeRanges(ref ranges);
            }

            long countPossibleFreshIds = 0;

            foreach (var range in ranges)
            {
                countPossibleFreshIds += 1 + (range.End - range.Begin);
            }
            return countPossibleFreshIds;
        }
    }
}
