using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Solutions.Year2025
{
    public partial class SecretEntrance
    {
        private static int Mod(int a, int n) => ((a % n) + n) % n;

        [GeneratedRegex(@"([LR])(\d+)")]
        public static partial Regex SplitDirectionAndNumberRegex();

        public static int Part1(string input)
        {
            int password = 0;
            int dial = 50;

            foreach (Match match in SplitDirectionAndNumberRegex().Matches(input))
            {
                int moveAmount = int.Parse(match.Groups[2].Value);
                if (match.Groups[1].Value == "L")
                {
                    dial -= moveAmount;
                }
                else
                {
                    dial += moveAmount;
                }

                dial = Mod(dial, 100);

                if (dial == 0)
                {
                    password++;
                }
            }

            return password;
        }

        public static int Part2(string input)
        {
            int password = 0;
            int dial = 50;
            bool prevWas0 = false;

            foreach (Match match in SplitDirectionAndNumberRegex().Matches(input))
            {
                int moveAmount = int.Parse(match.Groups[2].Value);

                if (match.Groups[1].Value == "L")
                {
                    dial -= moveAmount;
                }
                else
                {
                    dial += moveAmount;
                }

                if (dial <= 0)
                {
                    password += (1 + Math.Abs(dial) / 100);
                    if (prevWas0)
                    {
                        // double counted a 0
                        password--;
                    }
                }
                else if (dial >= 100)
                {
                    password += Math.Abs(dial) / 100;
                }

                dial = Mod(dial, 100);

                prevWas0 = dial == 0;
            }

            return password;
        }
    }
}
