using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Solutions.Year2025
{
    public partial class PrintingDepartment
    {
        private class PaperGrid
        {
            private sealed class Cell
            {
                private readonly Cell?[] _neighbours = new Cell[8];

                public IEnumerable<Cell?> Neighbours => _neighbours.AsEnumerable();

                public bool HasRoll;
                public Cell? North
                {
                    get => _neighbours[0];
                    set => _neighbours[0] = value;
                }
                public Cell? NorthEast
                {
                    get => _neighbours[1];
                    set => _neighbours[1] = value;
                }
                public Cell? East
                {
                    get => _neighbours[2];
                    set => _neighbours[2] = value;
                }
                public Cell? SouthEast
                {
                    get => _neighbours[3];
                    set => _neighbours[3] = value;
                }
                public Cell? South
                {
                    get => _neighbours[4];
                    set => _neighbours[4] = value;
                }
                public Cell? SouthWest
                {
                    get => _neighbours[5];
                    set => _neighbours[5] = value;
                }
                public Cell? West
                {
                    get => _neighbours[6];
                    set => _neighbours[6] = value;
                }
                public Cell? NorthWest
                {
                    get => _neighbours[7];
                    set => _neighbours[7] = value;
                }

                public int CountAdjacentRolls()
                {
                    int count = 0;
                    foreach (Cell? cell in _neighbours)
                    {
                        if (cell is not null && cell.HasRoll)
                            count++;
                    }
                    return count;
                }
            }

            private List<List<Cell>> _grid = [];

            public void AddRow(IEnumerable<bool> paperRow)
            {
                List<Cell>? northRow = _grid.LastOrDefault();
                _grid.Add([]);
                List<Cell> newRow = _grid.Last();

                int col = 0;
                foreach (bool paper in paperRow)
                {
                    Cell? westCell = newRow.LastOrDefault();
                    Cell? northCell = northRow?.Count > col ? northRow?[col] : null;
                    Cell? northEastCell = northRow?.Count > col + 1 ? northRow?[col + 1] : null;
                    Cell? northWestCell =
                        (northRow?.Count > col - 1) && (col - 1 >= 0) ? northRow?[col - 1] : null;

                    Cell newCell = new()
                    {
                        HasRoll = paper,
                        North = northCell,
                        NorthEast = northEastCell,
                        East = null,
                        SouthEast = null,
                        South = null,
                        SouthWest = null,
                        West = westCell,
                        NorthWest = northWestCell,
                    };

                    if (westCell is not null)
                        westCell.East = newCell;

                    if (northCell is not null)
                        northCell.South = newCell;

                    if (northEastCell is not null)
                        northEastCell.SouthWest = newCell;

                    if (northWestCell is not null)
                        northWestCell.SouthEast = newCell;

                    newRow.Add(newCell);

                    col++;
                }
            }

            public void AddRow(string row)
            {
                AddRow(row.Select(c => c == '@'));
            }

            public long CountRollsWithFewerAdjacentRolls(int n)
            {
                long count = 0;
                foreach (var row in _grid)
                {
                    foreach (Cell cell in row)
                    {
                        if (cell.HasRoll && cell.CountAdjacentRolls() < n)
                        {
                            count++;
                        }
                    }
                }
                return count;
            }

            public long CountAndRemoveRollsWithFewerAdjacentRolls(int n)
            {
                long count = 0;
                foreach (var row in _grid)
                {
                    foreach (Cell cell in row)
                    {
                        if (cell.HasRoll && cell.CountAdjacentRolls() < n)
                        {
                            cell.HasRoll = false;
                            count++;
                        }
                    }
                }
                return count;
            }

            public long RemoveAllRollsWithFewerAdj_Optimised(int n)
            {
                long count = 0;

                Stack<Cell> cellsToCheck = [];

                // first pass to find all rolls we can immediately delete
                foreach (var row in _grid)
                {
                    foreach (Cell cell in row)
                    {
                        if (cell.HasRoll && cell.CountAdjacentRolls() < n)
                        {
                            cellsToCheck.Push(cell);
                            count++;
                            cell.HasRoll = false;
                        }
                    }
                }

                while (cellsToCheck.Count > 0)
                {
                    Cell cell = cellsToCheck.Pop();
                    if (cell.HasRoll)
                    {
                        count++;
                        cell.HasRoll = false;
                    }

                    foreach (Cell? neighbour in cell.Neighbours)
                    {
                        if (
                            neighbour is not null
                            && neighbour.HasRoll
                            && neighbour.CountAdjacentRolls() < n
                        )
                        {
                            cellsToCheck.Push(neighbour);
                        }
                    }
                }

                return count;
            }
        }

        public static long Part1(string input)
        {
            PaperGrid paperGrid = new();

            foreach (string line in input.Split('\n'))
            {
                paperGrid.AddRow(line);
            }

            return paperGrid.CountRollsWithFewerAdjacentRolls(4);
        }

        public static long Part2(string input)
        {
            PaperGrid paperGrid = new();

            foreach (string line in input.Split('\n'))
            {
                paperGrid.AddRow(line);
            }

            const int numAdjacentRollsTooMany = 4;

            // this could be optimised used a BFS
            long rollsRemovedLastPass = paperGrid.CountAndRemoveRollsWithFewerAdjacentRolls(
                numAdjacentRollsTooMany
            );
            long numberOfRollsRemoved = rollsRemovedLastPass;
            while (rollsRemovedLastPass > 0)
            {
                rollsRemovedLastPass = paperGrid.CountAndRemoveRollsWithFewerAdjacentRolls(
                    numAdjacentRollsTooMany
                );
                numberOfRollsRemoved += rollsRemovedLastPass;
            }
            return numberOfRollsRemoved;
        }

        public static long Part2Optimised(string input)
        {
            PaperGrid paperGrid = new();

            foreach (string line in input.Split('\n'))
            {
                paperGrid.AddRow(line);
            }

            const int numAdjacentRollsTooMany = 4;
            return paperGrid.RemoveAllRollsWithFewerAdj_Optimised(numAdjacentRollsTooMany);
        }
    }
}
