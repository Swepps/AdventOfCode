using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private void RunDay<T>(int day, Func<string, T> part1, Func<string, T> part2)
            where T : System.Numerics.INumber<T>
        {
            output.WriteLine($"Day {day}, 2025\n-----------");

            string input = InputLoader.Load(2025, day);
            string exampleInput = InputLoader.LoadExample(2025, day);

            if (!string.IsNullOrEmpty(exampleInput))
            {
                T p1Example = part1(exampleInput);
                output.WriteLine($"Part 1 Example Result: {p1Example}");
            }

            Stopwatch sw = new();

            sw.Start();
            T p1 = part1(input);
            sw.Stop();

            Assert.True(p1 > T.Zero, $"Part 1 result must be > 0 but was {p1}");
            output.WriteLine(
                $"Part 1 Result: {p1}\nTime Elapsed: {sw.Elapsed.TotalMilliseconds}\n"
            );
            sw.Reset();

            if (!string.IsNullOrEmpty(exampleInput))
            {
                T p2Example = part2(exampleInput);
                output.WriteLine($"Part 2 Example Result: {p2Example}");
            }

            sw.Start();
            T p2 = part2(input);
            sw.Stop();

            Assert.True(p2 > T.Zero, $"Part 2 result must be > 0 but was {p2}");
            output.WriteLine(
                $"Part 2 Result: {p2}\nTime Elapsed: {sw.Elapsed.TotalMilliseconds}\n"
            );
            sw.Reset();
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

        [Fact]
        public void Day3_Lobby()
        {
            RunDay(3, Lobby.Part1, Lobby.Part2);
        }

        [Fact]
        public void Day4_Printing_Department()
        {
            RunDay(4, PrintingDepartment.Part1, PrintingDepartment.Part2);
        }

        [Fact]
        public void Day4_Printing_Department_Optimised()
        {
            RunDay(4, PrintingDepartment.Part1, PrintingDepartment.Part2Optimised);
        }

        [Fact]
        public void Day5_Cafeteria()
        {
            RunDay(5, Cafeteria.Part1, Cafeteria.Part2);
        }

        [Fact]
        public void Day6_Trash_Compactor()
        {
            RunDay(6, TrashCompactor.Part1, TrashCompactor.Part2);
        }

        [Fact]
        public void Day7_Laboratories()
        {
            RunDay(7, Laboratories.Part1, Laboratories.Part2);
        }

        [Fact]
        public void Day8_Playground()
        {
            RunDay(8, Playground.Part1, Playground.Part2);
        }

        [Fact]
        public void Day9_Movie_Theater()
        {
            RunDay(9, MovieTheater.Part1, MovieTheater.Part2);
        }
    }
}
