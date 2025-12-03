using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Solutions;
using Solutions.Year2024;
using Solutions.Year2025;
using Xunit.Abstractions;

namespace AdventOfCode
{
    public class AdventOfCode2024(ITestOutputHelper output)
    {
        private readonly ITestOutputHelper output = output;

        private void RunDay(int day, Func<string, uint> part1, Func<string, uint> part2)
        {
            output.WriteLine($"Day {day}, 2024\n-----------");

            string input = InputLoader.Load(2025, day);

            uint p1 = part1(input);
            Assert.True(p1 > 0);
            output.WriteLine($"Part 1 Result: {p1}\n");

            uint p2 = part2(input);
            Assert.True(p2 > 0);
            output.WriteLine($"Part 2 Result: {p2}\n");
        }

        [Fact]
        public void Day1_Historian_Hysteria()
        {
            RunDay(1, HistorianHysteria.Part1, HistorianHysteria.Part2);
        }

        [Fact]
        public void Day2_Red_Nosed_Reports()
        {
            RunDay(1, RedNosedReports.Part1, RedNosedReports.Part2);
        }

        [Fact]
        public void Day3_Mull_It_Over()
        {
            RunDay(1, MullItOver.Part1, MullItOver.Part2);
        }
    }
}
