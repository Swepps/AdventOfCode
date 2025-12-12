using System.Data;
using System.Text.RegularExpressions;

namespace Solutions.Year2025
{
    public partial class MovieTheater
    {
        [GeneratedRegex(@"(\d+),(\d+)")]
        public static partial Regex SplitRectangleCornerPositions();

        public static IEnumerable<(T, T)> GetAllPairs<T>(IList<T> source)
        {
            return source.SelectMany((_, i) => source.Where((_, j) => i < j), (x, y) => (x, y));
        }

        internal struct Corner
        {
            public long X;
            public long Y;

            public long AreaWith(Corner other)
            {
                long deltaX = X - other.X;
                long deltaY = Y - other.Y;
                return (Math.Abs(deltaX) + 1) * (Math.Abs(deltaY) + 1);
            }
        }

        public static long Part1(string input)
        {
            List<Corner> corners = [];

            foreach (Match match in SplitRectangleCornerPositions().Matches(input))
            {
                corners.Add(
                    new()
                    {
                        X = long.Parse(match.Groups[1].Value),
                        Y = long.Parse(match.Groups[2].Value),
                    }
                );
            }

            long bestPairArea = 0;

            for (int i = 0; i < corners.Count; i++)
            {
                for (int j = i + 1; j < corners.Count; j++)
                {
                    long cornerPairArea = corners[i].AreaWith(corners[j]);

                    if (cornerPairArea > bestPairArea)
                    {
                        bestPairArea = cornerPairArea;
                    }
                }
            }

            return bestPairArea;
        }

        private static bool CornerTouchesSegment(Corner corner, (Corner Start, Corner End) segment)
        {
            if (corner.X < Math.Min(segment.Start.X, segment.End.X))
                return false;
            if (corner.X > Math.Max(segment.Start.X, segment.End.X))
                return false;
            if (corner.Y < Math.Min(segment.Start.Y, segment.End.Y))
                return false;
            if (corner.Y > Math.Max(segment.Start.Y, segment.End.Y))
                return false;

            return true;
        }

        private static bool ShapeContainsCorner(
            Corner corner,
            List<(Corner Start, Corner End)> shape
        )
        {
            int countSegmentIntersections = 0;

            // cull the list into only segments that could possible be crossed
            List<(Corner Start, Corner End)> interestedSegments = [];
            foreach ((Corner segStart, Corner segEnd) in shape)
            {
                if (
                    corner.X < Math.Min(segStart.X, segEnd.X)
                    || corner.X > Math.Max(segStart.X, segEnd.X)
                    || corner.Y < Math.Min(segStart.Y, segEnd.Y)
                )
                {
                    continue;
                }

                interestedSegments.Add((segStart, segEnd));
            }

            foreach ((Corner Start, Corner End) segment in interestedSegments)
            {
                // if the segment contains the point then it's definitely in the shape
                // so return early
                if (
                    corner.X >= Math.Min(segment.Start.X, segment.End.X)
                    && corner.X <= Math.Max(segment.Start.X, segment.End.X)
                    && corner.Y >= Math.Min(segment.Start.Y, segment.End.Y)
                    && corner.Y <= Math.Max(segment.Start.Y, segment.End.Y)
                )
                {
                    return true;
                }

                // we're casting a ray south so need to do some jank with that in mind

                bool horizSeg = segment.Start.Y - segment.End.Y == 0;

                if (horizSeg)
                {
                    if (
                        corner.X > Math.Min(segment.Start.X, segment.End.X)
                        && corner.X < Math.Max(segment.Start.X, segment.End.X)
                        && corner.Y >= segment.Start.Y
                    )
                    {
                        countSegmentIntersections++;
                    }
                }
                else if (
                    corner.Y >= Math.Min(segment.Start.Y, segment.End.Y)
                    && corner.X == segment.Start.X
                )
                {
                    countSegmentIntersections++;
                }
            }

            return countSegmentIntersections % 2 == 1;
        }

        public static long Part2(string input)
        {
            List<Corner> corners = [];

            foreach (Match match in SplitRectangleCornerPositions().Matches(input))
            {
                corners.Add(
                    new()
                    {
                        X = long.Parse(match.Groups[1].Value),
                        Y = long.Parse(match.Groups[2].Value),
                    }
                );
            }

            List<(Corner Start, Corner End)> shape = [];

            for (int i = 1; i < corners.Count; i++)
            {
                Corner segStart = corners[i - 1];
                Corner segEnd = corners[i];
                shape.Add((segStart, segEnd));
            }

            // do the final wrapped segment
            if (corners.Count > 1)
            {
                Corner segStart = corners.First();
                Corner segEnd = corners.Last();
                shape.Add((segStart, segEnd));
            }

            long bestPairArea = 0;

            for (int i = 0; i < corners.Count; i++)
            {
                for (int j = i + 1; j < corners.Count; j++)
                {
                    Corner a = corners[i];
                    Corner b = corners[j];

                    long cornerPairArea = a.AreaWith(b);

                    if (cornerPairArea > bestPairArea)
                    {
                        Queue<(Corner, Corner)> cornersToCheck = [];
                        cornersToCheck.Enqueue((a, b));

                        bool validRect = true;

                        while (validRect && cornersToCheck.Count > 0)
                        {
                            (Corner miniA, Corner miniB) = cornersToCheck.Dequeue();

                            long rectWidth = Math.Abs(miniA.X - miniB.X);
                            long rectHeight = Math.Abs(miniA.Y - miniB.Y);

                            Corner centre = new()
                            {
                                X = (miniA.X + miniB.X) / 2,
                                Y = miniA.Y + miniB.Y / 2,
                            };

                            validRect = ShapeContainsCorner(centre, shape);

                            if (validRect)
                            {
                                // if the centre is either of the two corners then we're done with this bit
                                if (
                                    (centre.X == miniA.X && centre.Y == miniA.Y)
                                    || (centre.X == miniB.X && centre.Y == miniB.Y)
                                )
                                {
                                    continue;
                                }

                                // split rect in half and add their centres
                                if (rectWidth > rectHeight)
                                {
                                    cornersToCheck.Enqueue(
                                        (
                                            new Corner()
                                            {
                                                X = Math.Min(miniA.X, miniB.X),
                                                Y = Math.Max(miniA.Y, miniB.Y),
                                            },
                                            new Corner()
                                            {
                                                X = centre.X - 1,
                                                Y = Math.Min(miniA.Y, miniB.Y),
                                            }
                                        )
                                    );

                                    cornersToCheck.Enqueue(
                                        (
                                            new Corner()
                                            {
                                                X = centre.X + 1,
                                                Y = Math.Max(miniA.Y, miniB.Y),
                                            },
                                            new Corner()
                                            {
                                                X = Math.Max(miniA.X, miniB.X),
                                                Y = Math.Min(miniA.Y, miniB.Y),
                                            }
                                        )
                                    );
                                }
                                else
                                {
                                    cornersToCheck.Enqueue(
                                        (
                                            new Corner()
                                            {
                                                X = Math.Min(miniA.X, miniB.X),
                                                Y = Math.Max(miniA.Y, miniB.Y),
                                            },
                                            new Corner()
                                            {
                                                X = Math.Max(miniA.X, miniB.X),
                                                Y = centre.Y + 1,
                                            }
                                        )
                                    );

                                    cornersToCheck.Enqueue(
                                        (
                                            new Corner()
                                            {
                                                X = Math.Min(miniA.X, miniB.X),
                                                Y = centre.Y - 1,
                                            },
                                            new Corner()
                                            {
                                                X = Math.Max(miniA.X, miniB.X),
                                                Y = Math.Min(miniA.Y, miniB.Y),
                                            }
                                        )
                                    );
                                }
                            }
                        }

                        if (validRect)
                        {
                            bestPairArea = cornerPairArea;
                        }
                    }
                }
            }

            return bestPairArea;
        }
    }
}
