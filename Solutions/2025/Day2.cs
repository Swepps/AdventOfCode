using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Solutions.Year2025
{
    public partial class GiftShop
    {
        [GeneratedRegex(@"(\d+)-(\d+),*")]
        public static partial Regex SplitProductIdsRegex();

        [GeneratedRegex(@"^(\d+)(\1)$")]
        public static partial Regex CheckDuplicateNumberRegex();

        private static long GetValueForIdPart1(string id)
        {
            // return 0 if valid id
            Match match = CheckDuplicateNumberRegex().Match(id);
            if (!match.Success)
                return 0;

            return long.Parse(match.Value);
        }

        public static long Part1(string input)
        {
            long total = 0;
            foreach (Match match in SplitProductIdsRegex().Matches(input))
            {
                long begin = long.Parse(match.Groups[1].Value);
                long end = long.Parse(match.Groups[2].Value);

                for (long i = begin; i <= end; i++)
                {
                    total += GetValueForIdPart1(i.ToString());
                }
            }
            return total;
        }

        [GeneratedRegex(@"^(\d+)(\1)+$")]
        public static partial Regex CheckMultipleNumberRegex();

        private static long GetValueForIdPart2(string id)
        {
            // return 0 if valid id
            Match match = CheckMultipleNumberRegex().Match(id);
            if (!match.Success)
                return 0;

            return long.Parse(match.Value);
        }

        public static long Part2(string input)
        {
            long total = 0;
            foreach (Match match in SplitProductIdsRegex().Matches(input))
            {
                long begin = long.Parse(match.Groups[1].Value);
                long end = long.Parse(match.Groups[2].Value);

                for (long i = begin; i <= end; i++)
                {
                    total += GetValueForIdPart2(i.ToString());
                }
            }
            return total;
        }
    }
}
