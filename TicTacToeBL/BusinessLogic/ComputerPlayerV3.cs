using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TicTacToe.Entity;

namespace TicTacToe.BusinessLogic
{
    public class ComputerPlayerV3 : ITicTacToeComputerPlayer
    {
        private static Random random = new Random();
        public delegate int GetMoveDelegate(GetMoveEventArgs e);
        public GetMoveDelegate GetMoveHandler = null;

        public class Cell
        {
            public int Row { get; set; }
            public int Col { get; set; }
            public Entity.TicTacToeDataEntry CellState { get; set; }

            public ReadonlyCell AsReadonly()
            {
                return new ReadonlyCell(
                    Row,
                    Col,
                    CellState
                );
            }
        }

        public class ReadonlyCell
        {
            protected int _Row = int.MinValue;
            public int Row
            {
                get
                {
                    return _Row;
                }
            }

            protected int _Col = int.MinValue;
            public int Col
            {
                get
                {
                    return _Col;
                }
            }

            protected Entity.TicTacToeDataEntry _CellState = null;
            public Entity.TicTacToeDataEntry CellState
            {
                get
                {
                    if (_CellState == null)
                        return _CellState;

                    return _CellState.Copy();
                }
            }

            public ReadonlyCell(int row, int col, Entity.TicTacToeDataEntry cellState)
            {
                _Row = row;
                _Col = col;
                _CellState = cellState;
            }
        }

        public class CellCollction
        {
            public ICollection<Cell> Cells { get; set; }
            public int Player1Count { get; set; }
            public int Player2Count { get; set; }
        }

        public class GetMoveEventArgs
        {
            protected IReadOnlyCollection<ReadonlyCell> _Cells = null;
            public IReadOnlyCollection<ReadonlyCell> Cells
            {
                get
                {
                    return _Cells;
                }
            }

            protected IReadOnlyCollection<TicTacToeDataEntry> _BlankCells = null;
            public IReadOnlyCollection<TicTacToeDataEntry> BlankCells
            {
                get
                {
                    return _BlankCells;
                }
            }

            public GetMoveEventArgs(ICollection<Cell> cells, ICollection<TicTacToeDataEntry> blankCells)
            {
                _Cells = cells.Select(x => x.AsReadonly()).ToList().AsReadOnly();
                _BlankCells = blankCells.Select(x => x.Copy()).ToList().AsReadOnly();
            }
        }

        public int GetMove(string InstanceId)
        {
            var ds = TicTacToe.GetAndValidatePreviousMove(InstanceId);

            int GridSize = ds.First().GridSize;
            List<int> CellStates = ds.Select(x => x.CellContent).ToList();
            int BlankCellCount = int.MinValue;
            List<int> WinningCells = null;

            var GameStatus = TicTacToe.EvaluateResult(GridSize, CellStates, out BlankCellCount, out WinningCells);

            if (GameStatus == Models.TicTacToeGameStatus.InProgress && BlankCellCount > 0)
            {
                List<Cell> cells = new List<Cell>();

                for (int i = 0; i < ds.Count; i++)
                {
                    int row = i / GridSize;
                    int col = i % GridSize;

                    var cellState = ds[i];
#if DEBUG
                    if (i != cellState.CellIndex)
                        throw new ArgumentOutOfRangeException("i != cellState.CellIndex");
                    int vRow = cellState.CellIndex / GridSize;
                    int vCol = cellState.CellIndex % GridSize;
                    if (row != vRow || col != vCol)
                        throw new ArgumentOutOfRangeException("row != vRow || col != vCol");
#endif
                    var cell = new Cell()
                    {
                        Row = row,
                        Col = col,
                        CellState = cellState
                    };

                    cells.Add(cell);
                }


                List<CellCollction> parsed = new List<CellCollction>();

                //Check every Row
                var gbRow = cells.GroupBy(x => x.Row);
                foreach (var row in gbRow)
                {
                    if (row.Any(x => x.CellState.CellContent == 0))
                    {
                        CellCollction collection = new CellCollction()
                        {
                            Cells = row.ToList(),
                            Player1Count = row.Count(x => x.CellState.CellContent == 1),
                            Player2Count = row.Count(x => x.CellState.CellContent == 2)
                        };
                        parsed.Add(collection);
                    }
                }



                //Check every Col
                var gbCol = cells.GroupBy(x => x.Col);
                foreach (var col in gbCol)
                {
                    if (col.Any(x => x.CellState.CellContent == 0))
                    {
                        CellCollction collection = new CellCollction()
                        {
                            Cells = col.ToList(),
                            Player1Count = col.Count(x => x.CellState.CellContent == 1),
                            Player2Count = col.Count(x => x.CellState.CellContent == 2)
                        };
                        parsed.Add(collection);
                    }
                }



                //Check top to bottom diagonal
                List<Cell> TopToBottomDiagonal = new List<Cell>();
                for (int i = 0; i < GridSize; i++)
                {
                    Cell mCell = cells.FirstOrDefault(x => x.Row == i && x.Col == i);
                    if (mCell != null)
                        TopToBottomDiagonal.Add(mCell);
                }

                if (TopToBottomDiagonal.Any(x => x.CellState.CellContent == 0))
                {
                    CellCollction diagonal1 = new CellCollction()
                    {
                        Cells = TopToBottomDiagonal.ToList(),
                        Player1Count = TopToBottomDiagonal.Count(x => x.CellState.CellContent == 1),
                        Player2Count = TopToBottomDiagonal.Count(x => x.CellState.CellContent == 2)
                    };
                    parsed.Add(diagonal1);
                }



                //Check bottom to top diagonal
                List<Cell> BottomToTopDiagonal = new List<Cell>();
                for (int i = 0; i < GridSize; i++)
                {
                    Cell mCell = cells.FirstOrDefault(x => x.Row == GridSize - 1 - i && x.Col == i);
                    if (mCell != null)
                        BottomToTopDiagonal.Add(mCell);
                }

                if (BottomToTopDiagonal.Any(x => x.CellState.CellContent == 0))
                {
                    CellCollction diagonal2 = new CellCollction()
                    {
                        Cells = BottomToTopDiagonal.ToList(),
                        Player1Count = BottomToTopDiagonal.Count(x => x.CellState.CellContent == 1),
                        Player2Count = BottomToTopDiagonal.Count(x => x.CellState.CellContent == 2)
                    };
                    parsed.Add(diagonal2);
                }


                var blankCells = new List<Entity.TicTacToeDataEntry>();
                if (parsed.Any())
                {
                    int maxPlayer1Count =
                        parsed
                        .Max(x => x.Player1Count);

                    int maxPlayer2Count =
                        parsed
                        .Max(x => x.Player2Count);

                    if (maxPlayer2Count + 1 >= GridSize)
                    {
                        //Try to win
                        blankCells =
                            parsed
                            .OrderByDescending(x => x.Player1Count)
                            .Where(x => x.Player2Count >= maxPlayer2Count)
                            .Take(1)
                            .SelectMany(x =>
                                x.Cells
                                .Where(y => y.CellState.CellContent == 0)
                                .Select(y => y.CellState))
                            .ToList();
                    }
                    else if (maxPlayer1Count + 1 >= GridSize)
                    {
                        //Try to Block Player 1
                        blankCells =
                            parsed
                            .OrderByDescending(x => x.Player2Count)
                            .Where(x => x.Player1Count >= maxPlayer1Count)
                            .Take(1)
                            .SelectMany(x =>
                                x.Cells
                                .Where(y => y.CellState.CellContent == 0)
                                .Select(y => y.CellState))
                            .ToList();
                    }
                    else
                    {
                        blankCells =
                            parsed
                            .Where(x => x.Player2Count >= maxPlayer2Count)
                            .SelectMany(x =>
                                x.Cells
                                .Where(y => y.CellState.CellContent == 0)
                                .Select(y => y.CellState))
                            .ToList();
                    }
                }
                else
                {
                    blankCells = ds.Where(x => x.CellContent == 0).ToList();
                }

                // Choose the most strategic cell instead of choosing randomly
                if (GetMoveHandler == null)
                    throw new InvalidOperationException("GetMoveHandler must be assigned for this computer type to ");
                
                var getMoveEventArgs = new GetMoveEventArgs(cells, blankCells);
                if (GetMoveHandler != null)
                {
                    return GetMoveHandler(getMoveEventArgs);
                }

                int m = random.Next(0, blankCells.Count - 1);
                return blankCells[m].CellIndex;
            }

            return -1;
        }
    }
}