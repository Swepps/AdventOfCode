using System.Reflection;

namespace Solutions.Year2023
{
    public class Trebuchet
    {
        public static uint Part1(string input)
        {
            uint total = 0;
            string[] lines = input.Split("\n");

            foreach (string line in lines)
            {
                total += FindCalibrationValuePart1(line);
            }

            return total;
        }

        private static uint FindCalibrationValuePart1(string word)
        {
            char firstDigit = '\0';
            char lastDigit = '\0';
            foreach (char c in word)
            {
                if (char.IsDigit(c))
                {
                    if (firstDigit == '\0')
                    {
                        firstDigit = c;
                    }
                    lastDigit = c;
                }
            }

            return uint.Parse(firstDigit.ToString() + lastDigit.ToString());
        }

        public static uint Part2(string input)
        {
            uint total = 0;
            string[] lines = input.Split("\n");

            foreach (string line in lines)
            {
                total += FindCalibrationValuePart2(line);
            }

            return total;
        }

        static readonly Dictionary<string, char> Numbers = new()
        {
            { "one", '1' },
            { "two", '2' },
            { "three", '3' },
            { "four", '4' },
            { "five", '5' },
            { "six", '6' },
            { "seven", '7' },
            { "eight", '8' },
            { "nine", '9' },
        };

        private static uint FindCalibrationValuePart2(string word)
        {
            char firstDigit = '\0';
            char lastDigit = '\0';
            foreach (var c in word.Select((value, i) => new { i, value }))
            {
                // test for digit char
                if (char.IsDigit(c.value))
                {
                    if (firstDigit == '\0')
                    {
                        firstDigit = c.value;
                    }
                    lastDigit = c.value;
                }
                else
                {
                    // test for word
                    foreach (var key in Numbers.Keys)
                    {
                        if (word.IndexOf(key, c.i) == c.i)
                        {
                            if (firstDigit == '\0')
                            {
                                firstDigit = Numbers[key];
                            }
                            lastDigit = Numbers[key];
                        }
                    }
                }
            }

            return uint.Parse(firstDigit.ToString() + lastDigit.ToString());
        }
    }
}
