using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Solutions.Year2024
{
    public partial class RedNosedReports
    {
        [GeneratedRegex(@"(\d+)\s*")]
        public static partial Regex SplitNumbersRegex();

        public static uint Part1(string input)
        {
            uint numberSafeReports = 0;

            foreach (string line in input.Split('\n'))
            {
                if (
                    IsSafeReport(
                        SplitNumbersRegex()
                            .Matches(line)
                            .Select((match) => int.Parse(match.Groups[1].Value))
                    )
                )
                {
                    numberSafeReports++;
                }
            }

            return numberSafeReports;
        }

        public static bool IsSafeReport(IEnumerable<int> reportNums)
        {
            List<int> numbers = [];
            foreach (int reportNum in reportNums)
            {
                numbers.Add(reportNum);
                if (numbers.Count == 1)
                    continue;

                if (
                    Math.Abs(numbers[^1] - numbers[^2]) == 0
                    || Math.Abs(numbers[^1] - numbers[^2]) > 3
                )
                    return false;

                if (numbers.Count == 2)
                    continue;

                if ((numbers[^1] - numbers[^2]) * (numbers[^2] - numbers[^3]) < 0)
                    return false;
            }

            return true;
        }

        public static uint Part2(string input)
        {
            uint numberSafeReports = 0;

            foreach (string line in input.Split('\n'))
            {
                if (
                    IsDampenedSafeReport(
                        SplitNumbersRegex()
                            .Matches(line)
                            .Select((match) => int.Parse(match.Groups[1].Value))
                            .ToList()
                    )
                )
                {
                    numberSafeReports++;
                }
            }

            return numberSafeReports;
        }

        public static bool IsDampenedSafeReport(IList<int> reportNums)
        {
            if (IsSafeReport(reportNums))
                return true;

            for (int i = 0; i < reportNums.Count; i++)
            {
                if (IsSafeReport(reportNums.Take(i).Concat(reportNums.Skip(i + 1))))
                    return true;
            }

            return false;
        }
    }
}
