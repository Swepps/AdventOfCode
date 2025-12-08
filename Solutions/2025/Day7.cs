using System;
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
    public partial class Laboratories
    {
        private class TachyonManifold
        {
            public enum CellType
            {
                Beam,
                Splitter,
                Empty,
            };

            private sealed class Cell
            {
                private readonly Cell?[] _neighbours = new Cell[8];

                public IEnumerable<Cell?> Neighbours => _neighbours.AsEnumerable();

                public long HitCount = 0;
                public CellType Type;
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
            }

            private List<List<Cell>> _grid = [];

            public void AddRow(IEnumerable<CellType> row)
            {
                List<Cell>? northRow = _grid.LastOrDefault();
                _grid.Add([]);
                List<Cell> newRow = _grid.Last();

                int col = 0;
                foreach (CellType type in row)
                {
                    Cell? westCell = newRow.LastOrDefault();
                    Cell? northCell = northRow?.Count > col ? northRow?[col] : null;
                    Cell? northEastCell = northRow?.Count > col + 1 ? northRow?[col + 1] : null;
                    Cell? northWestCell =
                        (northRow?.Count > col - 1) && (col - 1 >= 0) ? northRow?[col - 1] : null;

                    Cell newCell = new()
                    {
                        Type = type,
                        North = northCell,
                        NorthEast = northEastCell,
                        East = null,
                        SouthEast = null,
                        South = null,
                        SouthWest = null,
                        West = westCell,
                        NorthWest = northWestCell,
                    };

                    if (type == CellType.Beam)
                    {
                        newCell.HitCount = 1;
                    }

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
                AddRow(
                    row.Select(c =>
                    {
                        if (c == '.')
                            return CellType.Empty;
                        else if (c == 'S')
                            return CellType.Beam;
                        else
                            return CellType.Splitter;
                    })
                );
            }

            public int SplitBeamAndCountSplits()
            {
                int splitCount = 0;

                foreach (var row in _grid)
                {
                    foreach (Cell cell in row)
                    {
                        if (cell.Type == CellType.Beam && cell.South is not null)
                        {
                            if (cell.South.Type == CellType.Splitter)
                            {
                                if (cell.SouthEast is not null)
                                    cell.SouthEast.Type = CellType.Beam;

                                if (cell.SouthWest is not null)
                                    cell.SouthWest.Type = CellType.Beam;

                                splitCount++;
                            }
                            else if (cell.South.Type == CellType.Empty)
                            {
                                cell.South.Type = CellType.Beam;
                            }
                        }
                    }
                }

                return splitCount;
            }

            public long SplitBeamAndCountTimelines()
            {
                foreach (var row in _grid)
                {
                    foreach (Cell cell in row)
                    {
                        if (cell.Type == CellType.Beam && cell.South is not null)
                        {
                            if (cell.South.Type == CellType.Splitter)
                            {
                                if (cell.SouthEast is not null)
                                {
                                    cell.SouthEast.Type = CellType.Beam;
                                    cell.SouthEast.HitCount += cell.HitCount;
                                }

                                if (cell.SouthWest is not null)
                                {
                                    cell.SouthWest.Type = CellType.Beam;
                                    cell.SouthWest.HitCount += cell.HitCount;
                                }
                            }
                            else if (cell.South.Type == CellType.Empty)
                            {
                                cell.South.Type = CellType.Beam;
                                cell.South.HitCount = cell.HitCount;
                            }
                            else if (cell.South.Type == CellType.Beam)
                            {
                                cell.South.HitCount += cell.HitCount;
                            }
                        }
                    }
                }

                return _grid.Last().Sum((Func<Cell, long>)(cell => cell.HitCount));
            }
        }

        public static long Part1(string input)
        {
            TachyonManifold manifold = new();

            foreach (string line in input.Split('\n'))
            {
                manifold.AddRow(line);
            }

            return manifold.SplitBeamAndCountSplits();
        }

        public static long Part2(string input)
        {
            TachyonManifold manifold = new();

            foreach (string line in input.Split('\n'))
            {
                manifold.AddRow(line);
            }

            return manifold.SplitBeamAndCountTimelines();
        }
    }
}
