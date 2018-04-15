using System;
using System.Collections.Generic;
using System.Linq;
using ReversiGame.Entities;

namespace ReversiGame
{
    public class Game
    {
        private Board Board { get; }

        private readonly Direction[] _possibleDirections =
        {
            new Direction(-1, -1),
            new Direction(-1, 0),
            new Direction(-1, 1),
            new Direction(0, -1),
            new Direction(0, 1),
            new Direction(1, -1),
            new Direction(1, 0),
            new Direction(1, 1),
        };

        public Game(Board board)
        {
            Board = board;
        }

        public class Result
        {
            public int Count { get; set; }
            public Board.Cell[] Points { get; set; }
        }

        private class PreResult
        {
            public int Count { get; set; }
            public Board.Cell Cell { get; set; }
        }

        public Result FindGreedyPositions()
        {
            var possiblePositions = GetPosiblePositions();

            var firstOrDefault = possiblePositions.OrderByDescending(x => x.Key).FirstOrDefault();

            return new Result
            {
                Count = firstOrDefault?.Key ?? 0,
                Points = firstOrDefault?.ToArray(),
            };
        }

        private ILookup<int, Board.Cell> GetPosiblePositions()
        {
            var possiblePositions = new Dictionary<Board.Cell, int>();

            for (int column = 0; column < Board.Width; column++)
            for (int row = 0; row < Board.Height; row++)
            {
                if (Board[column, row].Disc != DiscTypes.O)
                {
                    continue;
                }

                var surroundedPositions = GetPossibleSurroundedPositions(column, row).ToArray();

                if (surroundedPositions.Length < 0)
                {
                    continue;
                }

                foreach (var surroundedPosition in surroundedPositions)
                {
                    if (possiblePositions.ContainsKey(surroundedPosition.Cell))
                    {
                        possiblePositions[surroundedPosition.Cell] += surroundedPosition.Count;
                    }
                    else
                    {
                        possiblePositions[surroundedPosition.Cell] = surroundedPosition.Count;
                    }
                }
            }

            return possiblePositions.ToLookup(x => x.Value, x => x.Key);
        }

        private IEnumerable<PreResult> GetPossibleSurroundedPositions(int column, int row)
        {
            foreach (var direction in _possibleDirections)
            {
                var possibleColumn = column + direction.X;
                var possibleRow = row + direction.Y;

                if (possibleColumn < 0 || possibleColumn >= Board.Width
                    || possibleRow < 0 || possibleRow >= Board.Height
                    && Board[possibleColumn, possibleRow].Disc != DiscTypes.Empty)
                {
                    continue;
                }

                var columnDelta = direction.X * -1;
                var rowDelta = direction.Y * -1;

                var count = CanEatCount(column, row, columnDelta, rowDelta);

                if (count > 0)
                {
                    yield return new PreResult
                    {
                        Count = count,
                        Cell = Board[possibleColumn, possibleRow]
                    };
                }
            }
        }

        private int CanEatCount(int column, int row, int columnDelta, int rowDelta)
        {
            var count = 0;
            while (column < Board.Width && column >= 0
                   && row < Board.Height && row >= 0)
            {
                switch (Board[column, row].Disc)
                {
                    case DiscTypes.X:
                        return count;

                    case DiscTypes.Empty:
                        return 0;

                    case DiscTypes.O:
                        count++;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                column += columnDelta;
                row += rowDelta;
            }

            return 0;
        }
    }
}