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
        [GeneratedRegex(@"(\d+)-(\d+),")]
        public static partial Regex SplitProductIdsRegex();

        public static int Part1(string input)
        {
            int total = 0;
            foreach (Match match in SplitProductIdsRegex().Matches(input))
            {
                string id1 = match.Groups[1].Value;
                string id2 = match.Groups[2].Value;
            }
            return total;
        }

        public static int Part2(string input)
        {
            int total = 0;
            foreach (Match match in SplitProductIdsRegex().Matches(input))
            {
                string id1 = match.Groups[1].Value;
                string id2 = match.Groups[2].Value;
            }
            return total;
        }
    }
}
