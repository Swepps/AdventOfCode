using System;
using System.IO;

namespace AdventOfCode
{
    public static class InputLoader
    {
        public static string Load(int year, int day)
        {
            // Current working directory + Inputs/<year>/Day1.txt, etc.
            var baseDir = Directory.GetCurrentDirectory();
            var path = Path.Combine(baseDir, "Inputs", year.ToString(), $"Day{day}.txt");

            if (!File.Exists(path))
                throw new FileNotFoundException($"Input file not found: {path}");

            return File.ReadAllText(path);
        }
    }
}
