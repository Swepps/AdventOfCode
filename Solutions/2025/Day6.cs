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
    public partial class TrashCompactor
    {
        [GeneratedRegex(@"\d+")]
        public static partial Regex FindNumbersRegex();

        [GeneratedRegex(@"[\+\*]")]
        public static partial Regex FindOperatorsRegex();

        public static long Part1(string input)
        {
            List<char> operators = [];
            foreach (Match match in FindOperatorsRegex().Matches(input))
            {
                operators.Add(match.Value.FirstOrDefault());
            }

            List<int> numbers = [];
            foreach (Match match in FindNumbersRegex().Matches(input))
            {
                numbers.Add(int.Parse(match.Value));
            }

            int numCalculations = operators.Count;
            int numRows = numbers.Count / numCalculations;

            long total = 0;
            int col = 0;
            foreach (char op in operators)
            {
                List<int> workingNums = [];
                for (int i = 0; i < numRows; i++)
                {
                    workingNums.Add(numbers[col + (i * numCalculations)]);
                }

                long thisAnswer = 0;

                if (op == '+')
                {
                    foreach (int num in workingNums)
                    {
                        thisAnswer += num;
                    }
                }
                else
                {
                    thisAnswer = 1;
                    foreach (int num in workingNums)
                    {
                        thisAnswer *= num;
                    }
                }

                total += thisAnswer;
                col++;
            }

            return total;
        }

        public static long Part2(string input)
        {
            string[] inputByLine = input.Split('\n');

            string operatorsInput = inputByLine.Last();

            List<(char op, int len)> calculationsInfo = [];

            {
                char lastOp = ' ';
                int lastLen = 1;
                foreach (char c in operatorsInput)
                {
                    if (c == '*' || c == '+')
                    {
                        if (lastLen > 1)
                        {
                            calculationsInfo.Add((lastOp, lastLen));
                            lastLen = 1;
                        }

                        lastOp = c;
                    }
                    else
                    {
                        lastLen++;
                    }
                }

                // the last one has one space missing so don't minus 1 from len
                if (lastLen > 1)
                {
                    calculationsInfo.Add((lastOp, lastLen + 1));
                }
            }

            long total = 0;

            {
                int countNumRows = inputByLine.Length - 1;
                int offset = 0;

                foreach (var (op, len) in calculationsInfo)
                {
                    List<string> workingNumStrings = [];

                    for (int col = (offset + len) - 1; col >= offset; col--)
                    {
                        int internalCol = col - offset;
                        for (int row = 0; row < countNumRows; row++)
                        {
                            if (internalCol == col - offset)
                                workingNumStrings.Add("");

                            char inputChar = inputByLine[row][col];
                            if (Char.IsNumber(inputChar))
                            {
                                workingNumStrings[internalCol] += inputChar;
                            }
                        }
                    }

                    List<int> workingNums =
                    [
                        .. workingNumStrings
                            .Where(x => !string.IsNullOrEmpty(x))
                            .Select(x => int.Parse(x)),
                    ];

                    long thisAnswer = 0;
                    if (op == '+')
                    {
                        foreach (int num in workingNums)
                        {
                            thisAnswer += num;
                        }
                    }
                    else
                    {
                        thisAnswer = 1;
                        foreach (int num in workingNums)
                        {
                            thisAnswer *= num;
                        }
                    }

                    offset += len;
                    total += thisAnswer;
                }
            }

            return total;
        }
    }
}
