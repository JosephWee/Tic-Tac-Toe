using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.BusinessLogic
{
    public class TicTacToe
    {
        public static IReadOnlyList<int> ValidCellStateValues
        {
            get
            {
                return
                    (new List<int>() { 0, 1, 2 })
                    .AsReadOnly();
            }
        }

        public static Models.TicTacToeUpdateResponse EvaluateResult(Models.TicTacToeUpdateRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("Request is null");
            if (request.CellStates == null)
                throw new ArgumentNullException("Request.CellStates is null");
            if (request.CellStates.Count != request.TotalCellCount)
                throw new ArgumentNullException("Unexpected Request.CellStates count");

            List<int> WinningCells = null;
            int BlankCellCount = int.MinValue;

            Models.TicTacToeUpdateResponse response = new Models.TicTacToeUpdateResponse()
            {
                Status = EvaluateResult(request.GridSize, request.CellStates, out BlankCellCount, out WinningCells),
                WinningCells = WinningCells
            };

            //Store to DB
            List<Entity.TicTacToeDataEntry> TicTacToeData = new List<Entity.TicTacToeDataEntry>();
            for (int i = 0; i < request.CellStates.Count; i++)
            {
                Entity.TicTacToeDataEntry newEntry = new Entity.TicTacToeDataEntry()
                {
                    CreatedDate = DateTime.UtcNow,
                    InstanceId = request.InstanceId,
                    GridSize = request.GridSize,
                    MoveNumber = request.TotalCellCount - BlankCellCount,
                    CellIndex = i,
                    CellContent = request.CellStates[i]
                };
                TicTacToeData.Add(newEntry);
            }

            Entity.TicTacToeDataContext dbContext = new Entity.TicTacToeDataContext();
            dbContext.TicTacToeData.AddRange(TicTacToeData);
            dbContext.SaveChanges();

            if (request.NumberOfPlayers == 1 && response.Status == Models.TicTacToeGameStatus.InProgress)
            {
                ITicTacToeComputerPlayer computerPlayer =
                    ComputerPlayerConfig.CreateComputerPlayer();

                response.ComputerMove =
                    computerPlayer.GetMove(request.InstanceId, request.TotalCellCount - BlankCellCount);
            }

            return response;
        }

        public static Models.TicTacToeGameStatus EvaluateResult(int GridSize, List<int> CellStates, out int BlankCellCount, out List<int> WinningCells)
        {
            if (CellStates == null)
                throw new ArgumentNullException("CellStates is null");

            int TotalCellCount = GridSize * GridSize;
            int winner = int.MinValue;
            bool gameOver = true;
            int blankCellCount = 0;
            List<int> winningCells = new List<int>();

            int[,] cell = new int[GridSize, GridSize];

            for (int i = 0; i < TotalCellCount; i++)
            {
                int col = i % GridSize;
                int row = i / GridSize;

                int cellState = CellStates[i];

                if (!ValidCellStateValues.Contains(cellState))
                    throw new ArgumentException(string.Format("Cell[{0},{1}] is invalid", row, col));

                cell[row, col] = cellState;

                if (cellState == 0)
                    blankCellCount++;
            }

            if (blankCellCount > 0)
                gameOver = false;

            //Check every row
            if (!gameOver)
            {
                for (int r = 0; r < GridSize; r++)
                {
                    bool allTheSame = true;
                    int prevCellState = cell[r, 0];
                    for (int c = 1; c < GridSize; c++)
                    {
                        int cellState = cell[r, c];
                        if (cellState != prevCellState)
                        {
                            allTheSame = false;
                            break;
                        }
                    }

                    if (cell[r, 0] != 0 && allTheSame)
                    {
                        winner = cell[r, 0];
                        gameOver = true;

                        for (int i = 0; i < GridSize; i++)
                            winningCells.Add(i + r * GridSize);
                    }
                }
            }

            //Check every column
            if (!gameOver)
            {
                for (int c = 0; c < GridSize; c++)
                {
                    bool allTheSame = true;
                    int prevCellState = cell[0, c];
                    for (int r = 1; r < GridSize; r++)
                    {
                        int cellState = cell[r, c];
                        if (cellState != prevCellState)
                        {
                            allTheSame = false;
                            break;
                        }
                    }

                    if (cell[0, c] != 0 && allTheSame)
                    {
                        winner = cell[0, c];
                        gameOver = true;

                        for (int r = 0; r < GridSize; r++)
                            winningCells.Add(c + (r * GridSize));
                    }
                }
            }

            //Check diagonal top-bottom
            if (!gameOver)
            {
                bool allTheSame = true;
                int prevCellState = cell[0, 0];

                for (int i = 1; i < GridSize; i++)
                {
                    int cellState = cell[i, i];
                    if (cellState != prevCellState)
                    {
                        allTheSame = false;
                        break;
                    }
                }

                if (cell[0, 0] != 0 && allTheSame)
                {
                    winner = cell[0, 0];
                    gameOver = true;

                    for (int i = 0; i < GridSize; i++)
                        winningCells.Add(i * (GridSize + 1));
                }
            }

            //Check diagonal bottom-top
            if (!gameOver)
            {
                bool allTheSame = true;
                int r = GridSize - 1;
                int prevCellState = cell[r, 0];

                for (int i = 1; i < GridSize; i++)
                {
                    r = GridSize - 1 - i;
                    int cellState = cell[r, i];
                    if (cellState != prevCellState)
                    {
                        allTheSame = false;
                        break;
                    }
                }

                if (cell[GridSize - 1, 0] != 0 && allTheSame)
                {
                    winner = cell[GridSize - 1, 0];
                    gameOver = true;

                    for (int i = GridSize; i >= 1; i--)
                        winningCells.Add((GridSize - 1) * i);
                }
            }

            Models.TicTacToeGameStatus Status = Models.TicTacToeGameStatus.InProgress;
            BlankCellCount = blankCellCount;

            if (gameOver)
            {
                if (winner == 1)
                    Status = Models.TicTacToeGameStatus.Player1Wins;
                else if (winner == 2)
                    Status = Models.TicTacToeGameStatus.Player2Wins;
                else
                    Status = Models.TicTacToeGameStatus.Draw;

                WinningCells = winningCells;
            }
            else
            {
                WinningCells = null;
            }
            
            return Status;
        }

        public static List<Entity.TicTacToeDataEntry>  GetAndValidate(string InstanceId, int LastMoveNumber)
        {
            Entity.TicTacToeDataContext context = new Entity.TicTacToeDataContext();

            List<Entity.TicTacToeDataEntry> ds =
                (from dr in context.TicTacToeData
                 where
                     dr.InstanceId == InstanceId
                     && dr.MoveNumber == LastMoveNumber
                 orderby
                     dr.CellIndex
                 select dr).ToList();



            if (ds == null || !ds.Any())
                return ds;



            var invalidCellContent =
                ds.Where(x => !TicTacToe.ValidCellStateValues.Contains(x.CellContent))
                .ToList();

            if (invalidCellContent.Any())
                throw new ArgumentOutOfRangeException(
                    string.Format("Cells {0} has invalid cell contents", string.Join(", ", invalidCellContent))
                );



            int minGridSize = ds.Min(x => x.GridSize);
            int maxGridSize = ds.Max(x => x.GridSize);

            if (minGridSize != maxGridSize)
                throw new ArgumentOutOfRangeException("Data has inconsistent Grid Size");



            bool allCellIndexOkay = true;
            int TotalCellCount = minGridSize * minGridSize;
            for (int i = 0; i < TotalCellCount; i++)
            {
                if (ds[i].CellIndex != i)
                {
                    allCellIndexOkay = false;
                    break;
                }
            }

            if (!allCellIndexOkay)
                throw new IndexOutOfRangeException("Cell Indices are not 0 based or contiguous");

            return ds;
        }
    }
}