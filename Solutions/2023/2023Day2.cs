using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutions.Year2023
{
    public class CubeConundrum
    {
        public static uint Part1(string input)
        {
            uint total = 0;
            string[] lines = input.Split("\n");

            foreach (string line in lines)
            {
                total += GetGameValue(line);
            }

            return total;
        }

        private static uint GetGameValue(string line)
        {
            string[] splitLine = line.Split(": ", 2);
            string gameName = splitLine[0];
            uint gameId = uint.Parse(gameName.Split(" ")[1]);
            string[] sets = splitLine[1].Split("; ");

            return sets.Any((string set) => !IsSetPossible(set)) ? 0 : gameId;
        }

        static readonly Dictionary<string, int> MaxColourCubes = new()
        {
            { "red", 12 },
            { "green", 13 },
            { "blue", 14 },
        };

        private static bool IsSetPossible(string set)
        {
            string[] splitSet = set.Split(", ");
            foreach (string cubeCount in splitSet)
            {
                foreach (string key in MaxColourCubes.Keys)
                {
                    if (cubeCount.Contains(key))
                    {
                        int count = int.Parse(cubeCount.Split(" ")[0]);
                        if (count > MaxColourCubes[key])
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public static uint Part2(string input)
        {
            uint total = 0;
            string[] lines = input.Split("\n");

            foreach (string line in lines)
            {
                total += GetGamePower(line);
            }

            return total;
        }

        private static uint GetGamePower(string line)
        {
            string[] splitLine = line.Split(": ", 2);
            string[] sets = splitLine[1].Split("; ");

            uint minRed = 0;
            uint minGreen = 0;
            uint minBlue = 0;

            foreach (string set in sets)
            {
                minRed = Math.Max(GetCubeCount("red", set), minRed);
                minGreen = Math.Max(GetCubeCount("green", set), minGreen);
                minBlue = Math.Max(GetCubeCount("blue", set), minBlue);
            }

            return minRed * minGreen * minBlue;
        }

        private static uint GetCubeCount(string colour, string set)
        {
            string[] splitSet = set.Split(", ");
            foreach (string cubeCount in splitSet)
            {
                if (cubeCount.Contains(colour))
                {
                    return uint.Parse(cubeCount.Split(" ")[0]);
                }
            }
            return 0;
        }
    }
}
