using Solutions.Year2023;
using Xunit.Abstractions;

namespace AdventOfCode
{
    public class Examples(ITestOutputHelper output)
    {
        private readonly ITestOutputHelper output = output;

        [Fact]
        public void Day1_2023_Trebuchet()
        {
            output.WriteLine("Day 1, 2023\n-----------");

            uint part1TestResult = Trebuchet.Part1(Properties.Resources2024.Day1Test12023);
            Assert.Equal((uint)142, part1TestResult);
            output.WriteLine($"Part 1 Test Case: {part1TestResult}");

            uint part1FinalResult = Trebuchet.Part1(Properties.Resources2024.Day1Input2023);
            Assert.True(part1FinalResult > 0);
            output.WriteLine($"Part 1 Final Result: {part1FinalResult}");

            output.WriteLine("");

            uint part2TestResult = Trebuchet.Part2(Properties.Resources2024.Day1Test22023);
            Assert.Equal((uint)281, part2TestResult);
            output.WriteLine($"Part 2 Test Case: {part2TestResult}");

            uint part2FinalResult = Trebuchet.Part2(Properties.Resources2024.Day1Input2023);
            Assert.True(part2FinalResult > 0);
            output.WriteLine($"Part 2 Final Result: {part2FinalResult}");

            output.WriteLine("");
        }

        [Fact]
        public void Day2_2023_CubeConundrum()
        {
            output.WriteLine("Day 2, 2023\n-----------");

            uint part1TestResult = CubeConundrum.Part1(Properties.Resources2024.Day2Test12023);
            Assert.Equal((uint)8, part1TestResult);
            output.WriteLine($"Part 1 Test Case: {part1TestResult}");

            uint part1FinalResult = CubeConundrum.Part1(Properties.Resources2024.Day2Input2023);
            Assert.True(part1FinalResult > 0);
            output.WriteLine($"Part 1 Final Result: {part1FinalResult}");

            output.WriteLine("");

            uint part2FinalResult = CubeConundrum.Part2(Properties.Resources2024.Day2Input2023);
            Assert.True(part2FinalResult > 0);
            output.WriteLine($"Part 2 Final Result: {part2FinalResult}");

            output.WriteLine("");
        }
    }
}
