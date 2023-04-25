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

namespace TicTacToe.BusinessLogic
{
    public class ComputerPlayerV3: ComputerPlayerBase
    {
        public class Cell
        {
            public int Row { get; set; }
            public int Col { get; set; }
            public Entity.TicTacToeDataEntry CellState { get; set; }
        }

        public class CellCollction
        {
            public ICollection<Cell> Cells { get; set; }
            public int Player1Count { get; set; }
            public int Player2Count { get; set; }
        }

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

        public override int GetMove(string InstanceId)
        {
            var ds = TicTacToe.GetAndValidatePreviousMove(InstanceId);

            int GridSize = ds.First().GridSize;
            List<int> CellStates = ds.Select(x => x.CellContent).ToList();
            int BlankCellCount = int.MinValue;
            List<int> WinningCells = null;

            var GameStatus = TicTacToe.EvaluateResult(this, GridSize, CellStates, out BlankCellCount, out WinningCells);

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
                Dictionary<float, List<int>> winningMoves = new Dictionary<float, List<int>>();
                Dictionary<float, List<int>> blockingMoves = new Dictionary<float, List<int>>();
                blankCells = blankCells.Distinct().ToList();

                var mlContext = new MLContext();
                ITransformer mlModel = mlContext.Model.Load(_MLNetModelPath, out var _);
                var predEngine = mlContext.Model.CreatePredictionEngine<MLModel1.ModelInput,MLModel1.ModelOutput>(mlModel);

                for (int i = 0; i < blankCells.Count; i++)
                {
                    var blankCell = blankCells[i];
                    int ci = blankCell.CellIndex;
                    var inputModel1 =
                        new MLModel1.ModelInput()
                        {
                            MoveNumber = cells.Count - blankCells.Count + 1,
                            Cell0 = ci == 0 ? 2 : cells[0].CellState.CellContent,
                            Cell1 = ci == 1 ? 2 : cells[1].CellState.CellContent,
                            Cell2 = ci == 2 ? 2 : cells[2].CellState.CellContent,
                            Cell3 = ci == 3 ? 2 : cells[3].CellState.CellContent,
                            Cell4 = ci == 4 ? 2 : cells[4].CellState.CellContent,
                            Cell5 = ci == 5 ? 2 : cells[5].CellState.CellContent,
                            Cell6 = ci == 6 ? 2 : cells[6].CellState.CellContent,
                            Cell7 = ci == 7 ? 2 : cells[7].CellState.CellContent,
                            Cell8 = ci == 8 ? 2 : cells[8].CellState.CellContent,
                            GameResultCode = 0
                        };

                    var prediction1 = predEngine.Predict(inputModel1);

                    var PredictionLabel = prediction1.PredictedLabel;
                    var PredictionScore = prediction1.Score;

                    if (PredictionLabel == PlayerSymbolOpponent)
                    {
                        if (blockingMoves.ContainsKey(PredictionScore[0]))
                            blockingMoves[PredictionScore[0]].Add(ci);
                        else
                        {
                            blockingMoves.Add(
                                PredictionScore[0],
                                new List<int>() { ci }
                            );
                        }
                    }
                    else if (PredictionLabel == PlayerSymbolSelf)
                    {
                        if (winningMoves.ContainsKey(PredictionScore[0]))
                            winningMoves[PredictionScore[0]].Add(ci);
                        else
                        {
                            winningMoves.Add(
                                PredictionScore[0],
                                new List<int>() { ci }
                            );
                        }
                    }
                }

                int cellIndex = int.MinValue;

                if (winningMoves.Keys.Count > 0)
                {
                    var key = winningMoves.Keys.Max();
                    cellIndex = winningMoves[key].FirstOrDefault();
                }
                else if (blockingMoves.Keys.Count > 0)
                {
                    var key = blockingMoves.Keys.Max();
                    cellIndex = blockingMoves[key].FirstOrDefault();
                }
                else
                {
                    int m = random.Next(0, blankCells.Count - 1);
                    cellIndex = blankCells[m].CellIndex;
                }

                return cellIndex;
            }

            return -1;
        }
    }
}