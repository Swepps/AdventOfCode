using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Solutions.Year2025
{
    public partial class Playground
    {
        internal class Junction
        {
            public List<(Junction, double)> Distances = [];

            public (int x, int y, int z) Position;

            public double DistanceTo(Junction otherJunc)
            {
                if (otherJunc == this)
                    return 0;

                double deltaX = Position.x - otherJunc.Position.x;
                double deltaY = Position.y - otherJunc.Position.y;
                double deltaZ = Position.z - otherJunc.Position.z;

                return deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ;
            }

            public override bool Equals(object? obj)
            {
                return obj is Junction j && j.Position == Position;
            }

            public override int GetHashCode() => Position.GetHashCode();
        }

        [GeneratedRegex(@"(\d+),(\d+),(\d+)")]
        public static partial Regex SplitJunctionPositions();

        private static List<Junction> ParseJunctions(string input)
        {
            var list = new List<Junction>();

            foreach (Match m in SplitJunctionPositions().Matches(input))
            {
                var pos = (
                    x: int.Parse(m.Groups[1].Value),
                    y: int.Parse(m.Groups[2].Value),
                    z: int.Parse(m.Groups[3].Value)
                );

                var j = new Junction { Position = pos };

                foreach (var existing in list)
                    existing.Distances.Add((j, existing.DistanceTo(j)));

                list.Add(j);
            }

            return list;
        }

        private static List<(Junction a, Junction b, double dist)> BuildSortedPairs(
            IEnumerable<Junction> junctions
        )
        {
            var pairs = new List<(Junction a, Junction b, double dist)>();

            foreach (var j in junctions)
            {
                foreach (var (other, d) in j.Distances)
                    pairs.Add((j, other, d));
            }

            return [.. pairs.OrderBy(p => p.dist)];
        }

        private static void Union(Junction a, Junction b, List<HashSet<Junction>> sets)
        {
            HashSet<Junction>? setA = sets.FirstOrDefault(s => s.Contains(a));
            HashSet<Junction>? setB = sets.FirstOrDefault(s => s.Contains(b));

            if (setA is null && setB is null)
            {
                sets.Add([a, b]);
                return;
            }

            if (setA == setB)
                return;

            if (setA is not null && setB is not null)
            {
                setA.UnionWith(setB);
                sets.Remove(setB);
                return;
            }

            setA?.Add(b);
            setB?.Add(a);
        }

        public static long Part1(string input)
        {
            var junctions = ParseJunctions(input).ToList();
            var sortedPairs = BuildSortedPairs(junctions);
            var circuits = new List<HashSet<Junction>>();

            int joinLimit = junctions.Count <= 20 ? 10 : 1000;
            int joined = 0;

            foreach (var (a, b, _) in sortedPairs)
            {
                if (joined++ >= joinLimit)
                    break;

                Union(a, b, circuits);
            }

            const int take = 3;

            var biggest = circuits.OrderByDescending(c => c.Count).Take(take).Select(c => c.Count);

            long product = 1;
            foreach (var size in biggest)
                product *= size;

            return product;
        }

        public static long Part2(string input)
        {
            var junctions = ParseJunctions(input).ToHashSet();
            var sortedPairs = BuildSortedPairs(junctions);
            List<HashSet<Junction>> circuits = [];

            Junction lastA = null!;
            Junction lastB = null!;

            foreach (var (a, b, _) in sortedPairs)
            {
                Union(a, b, circuits);

                // if the first circuit contains all junctions, we must have finished
                if (circuits.First().SetEquals(junctions))
                {
                    lastA = a;
                    lastB = b;
                    break;
                }
            }

            return lastA.Position.x * lastB.Position.x;
        }
    }
}
