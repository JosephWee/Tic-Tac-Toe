using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicTacToe.BusinessLogic
{
    public class ComputerPlayerV2 : ComputerPlayerBase
    {
        
        public ComputerPlayerV2()
            :base(1, 2)
        {
        }

        public ComputerPlayerV2(int playerSymbolOpponent, int playerSymbolSelf)
            :base(playerSymbolOpponent, playerSymbolSelf)
        {
        }

        public override int GetMove(int GridSize, List<int> CellStates)
        {
            int ExpectedCellCount = GridSize * GridSize;
            if (CellStates.Count != ExpectedCellCount)
                throw new ArgumentException("GridSize and CellStates length do not match.");

            int BlankCellCount = int.MinValue;
            List<int> WinningCells = null;

            var GameStatus = TicTacToe.EvaluateResult(this, GridSize, CellStates, out BlankCellCount, out WinningCells);

            if (GameStatus == Models.TicTacToeGameStatus.InProgress && BlankCellCount > 0)
            {
                List<Cell> cells = new List<Cell>();

                for (int i = 0; i < CellStates.Count; i++)
                {
                    int row = i / GridSize;
                    int col = i % GridSize;

                    var cellState = CellStates[i];
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
                    if (row.Any(x => x.CellState == 0))
                    {
                        CellCollction collection = new CellCollction()
                        {
                            Cells = row.ToList(),
                            OpponentCount = row.Count(x => x.CellState == PlayerSymbolOpponent),
                            SelfCount = row.Count(x => x.CellState == PlayerSymbolSelf)
                        };
                        parsed.Add(collection);
                    }
                }



                //Check every Col
                var gbCol = cells.GroupBy(x => x.Col);
                foreach (var col in gbCol)
                {
                    if (col.Any(x => x.CellState == 0))
                    {
                        CellCollction collection = new CellCollction()
                        {
                            Cells = col.ToList(),
                            OpponentCount = col.Count(x => x.CellState == PlayerSymbolOpponent),
                            SelfCount = col.Count(x => x.CellState == PlayerSymbolSelf)
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

                if (TopToBottomDiagonal.Any(x => x.CellState == 0))
                {
                    CellCollction diagonal1 = new CellCollction()
                    {
                        Cells = TopToBottomDiagonal.ToList(),
                        OpponentCount = TopToBottomDiagonal.Count(x => x.CellState == PlayerSymbolOpponent),
                        SelfCount = TopToBottomDiagonal.Count(x => x.CellState == PlayerSymbolSelf)
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

                if (BottomToTopDiagonal.Any(x => x.CellState == 0))
                {
                    CellCollction diagonal2 = new CellCollction()
                    {
                        Cells = BottomToTopDiagonal.ToList(),
                        OpponentCount = BottomToTopDiagonal.Count(x => x.CellState == PlayerSymbolOpponent),
                        SelfCount = BottomToTopDiagonal.Count(x => x.CellState == PlayerSymbolSelf)
                    };
                    parsed.Add(diagonal2);
                }


                var validMoves = new List<CellCollction>();
                if (parsed.Any())
                {
                    int maxOpponentCount =
                        parsed
                        .Max(x => x.OpponentCount);

                    int maxSelfCount =
                        parsed
                        .Max(x => x.SelfCount);

                    if (maxSelfCount + 1 >= GridSize)
                    {
                        //Try to win
                        validMoves =
                            parsed
                            .OrderByDescending(x => x.OpponentCount)
                            .Where(x => x.SelfCount >= maxSelfCount)
                            .ToList();
                    }
                    else if (maxOpponentCount + 1 >= GridSize)
                    {
                        //Try to Block Player 1
                        validMoves =
                            parsed
                            .OrderByDescending(x => x.SelfCount)
                            .Where(x => x.OpponentCount >= maxOpponentCount)
                            .ToList();
                    }
                    else
                    {
                        validMoves =
                            parsed
                            .Where(x => x.SelfCount >= maxSelfCount)
                            .ToList();
                    }
                }
                
                int moveIndex = random.Next(0, validMoves.Count - 1);
                var blankCells = validMoves[moveIndex].Cells.Where(x => x.CellState == 0).ToList();
                int c = random.Next(0, blankCells.Count - 1);
                var selectedCell = blankCells[c];
                return ((selectedCell.Row * GridSize) + selectedCell.Col);
            }

            return -1;
        }
    }
}