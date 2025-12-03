using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Solutions;
using Solutions.Year2025;
using Xunit.Abstractions;

namespace AdventOfCode
{
    public class AdventOfCode2025(ITestOutputHelper output)
    {
        private readonly ITestOutputHelper output = output;

        private void RunDay(int day, Func<string, int> part1, Func<string, int> part2)
        {
            output.WriteLine($"Day {day}, 2025\n-----------");

            string input = InputLoader.Load(2025, day);

            int p1 = part1(input);
            Assert.True(p1 > 0);
            output.WriteLine($"Part 1 Result: {p1}\n");

            int p2 = part2(input);
            Assert.True(p2 > 0);
            output.WriteLine($"Part 2 Result: {p2}\n");
        }

        [Fact]
        public void Day1_Secret_Entrance()
        {
            RunDay(1, SecretEntrance.Part1, SecretEntrance.Part2);
        }

        [Fact]
        public void Day2_Gift_Shop()
        {
            RunDay(2, GiftShop.Part1, GiftShop.Part2);
        }
    }
}
