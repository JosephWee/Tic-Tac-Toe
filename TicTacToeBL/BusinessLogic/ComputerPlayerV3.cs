using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using TicTacToe.Entity;
using TicTacToe.ML;
using static TicTacToe.ML.MLModel1;
using TicTacToe.Models;

namespace TicTacToe.BusinessLogic
{
    public class ComputerPlayerV3: ComputerPlayerBase
    {
        private string _MLNetModelPath = string.Empty;

        public ComputerPlayerV3(string MLNetModelPath)
            :this(1, 2, MLNetModelPath)
        {
        }

        public ComputerPlayerV3(int symbolPlayerOpponent, int symbolPlayerSelf, string MLNetModelPath)
            :base(symbolPlayerOpponent, symbolPlayerSelf)
        {
            _MLNetModelPath = MLNetModelPath;
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


                CellCollction validMoves = null;
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
                            .First();
                    }
                    else if (maxOpponentCount + 1 >= GridSize)
                    {
                        //Try to Block Player 1
                        validMoves =
                            parsed
                            .OrderByDescending(x => x.SelfCount)
                            .Where(x => x.OpponentCount >= maxOpponentCount)
                            .First();
                    }
                    else
                    {
                        validMoves =
                            parsed
                            .Where(x => x.SelfCount >= maxSelfCount)
                            .First();
                    }
                }

                // Choose the most strategic cell instead of choosing randomly
                Dictionary<float, List<int>> winningMoves = new Dictionary<float, List<int>>();
                Dictionary<float, List<int>> drawMoves = new Dictionary<float, List<int>>();

                var mlContext = new MLContext();
                ITransformer mlModel = mlContext.Model.Load(_MLNetModelPath, out var _);
                var predEngine = mlContext.Model.CreatePredictionEngine<MLModel1.ModelInput, MLModel1.ModelOutput>(mlModel);

                if (validMoves != null)
                {
                    var validCells = validMoves.Cells.Where(x => x.CellState == 0).ToList();
                    foreach (var validCell in validCells)
                    {
                        int validCellIndex = ((validCell.Row * GridSize) + validCell.Col);

                        var inputModel1 =
                            new MLModel1.ModelInput()
                            {
                                MoveNumber = ExpectedCellCount - BlankCellCount + 1,
                                Cell0 = validCellIndex == 0 ? PlayerSymbolSelf : CellStates[0],
                                Cell1 = validCellIndex == 1 ? PlayerSymbolSelf : CellStates[1],
                                Cell2 = validCellIndex == 2 ? PlayerSymbolSelf : CellStates[2],
                                Cell3 = validCellIndex == 3 ? PlayerSymbolSelf : CellStates[3],
                                Cell4 = validCellIndex == 4 ? PlayerSymbolSelf : CellStates[4],
                                Cell5 = validCellIndex == 5 ? PlayerSymbolSelf : CellStates[5],
                                Cell6 = validCellIndex == 6 ? PlayerSymbolSelf : CellStates[6],
                                Cell7 = validCellIndex == 7 ? PlayerSymbolSelf : CellStates[7],
                                Cell8 = validCellIndex == 8 ? PlayerSymbolSelf : CellStates[8],
                                GameResultCode = 0
                            };

                        var prediction1 = predEngine.Predict(inputModel1);

                        var PredictionLabel = prediction1.PredictedLabel;
                        var PredictionScore = prediction1.Score;

                        if (PredictionLabel == PlayerSymbolSelf)
                        {
                            if (winningMoves.ContainsKey(PredictionScore[0]))
                                winningMoves[PredictionScore[0]].Add(validCellIndex);
                            else
                            {
                                winningMoves.Add(
                                    PredictionScore[0],
                                    new List<int>() { validCellIndex }
                                );
                            }
                        }
                        else if (PredictionLabel == (int)TicTacToeGameStatus.Draw)
                        {
                            if (drawMoves.ContainsKey(PredictionScore[0]))
                                drawMoves[PredictionScore[0]].Add(validCellIndex);
                            else
                            {
                                drawMoves.Add(
                                    PredictionScore[0],
                                    new List<int>() { validCellIndex }
                                );
                            }
                        }
                    }
                }

                if (winningMoves.Keys.Count > 0)
                {
                    var key = winningMoves.Keys.Max();
                    var strategicCells = winningMoves[key];

                    int strategicCellIndex = random.Next(0, strategicCells.Count - 1);
                    
                    return strategicCells[strategicCellIndex];
                }
                else if (drawMoves.Keys.Count > 0)
                {
                    var key = drawMoves.Keys.Max();
                    var strategicCells = drawMoves[key];

                    int strategicCellIndex = random.Next(0, strategicCells.Count - 1);

                    return strategicCells[strategicCellIndex];
                }

                // Fall back to ComputerPlayerV2's algorithm
                var blankCells = validMoves.Cells.Where(x => x.CellState == 0).ToList();
                int c = random.Next(0, blankCells.Count - 1);
                var selectedCell = blankCells[c];
                return ((selectedCell.Row * GridSize) + selectedCell.Col);
            }

            return -1;
        }
    }
}