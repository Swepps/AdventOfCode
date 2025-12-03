using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Solutions.Year2025
{
    public partial class Lobby
    {
        [GeneratedRegex(@"(\d+)")]
        public static partial Regex SplitBatteryBanks();

        private static string MaxStringOfNChars(List<char> chars, int n)
        {
            string finalString = "";

            for (int i = n - 1; i >= 0; i--)
            {
                char nextMax = chars.SkipLast(i).Max();
                int indexOfNextMax = chars.IndexOf(nextMax);
                chars = [.. chars.Skip(indexOfNextMax + 1)];
                finalString += nextMax;
            }

            return finalString;
        }

        public static long Part1(string input)
        {
            long voltageSum = 0;
            const int numOfDigits = 2;

            foreach (Match match in SplitBatteryBanks().Matches(input))
            {
                List<char> batteryBank = [.. match.Value.Select(c => c)];
                voltageSum += long.Parse(MaxStringOfNChars(batteryBank, numOfDigits));
            }

            return voltageSum;
        }

        public static long Part2(string input)
        {
            long voltageSum = 0;
            const int numOfDigits = 12;

            foreach (Match match in SplitBatteryBanks().Matches(input))
            {
                List<char> batteryBank = [.. match.Value.Select(c => c)];
                voltageSum += long.Parse(MaxStringOfNChars(batteryBank, numOfDigits));
            }

            return voltageSum;
        }
    }
}
