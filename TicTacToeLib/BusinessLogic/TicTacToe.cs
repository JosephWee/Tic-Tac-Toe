using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicTacToe.BusinessLogic
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

            int moveNumber = request.TotalCellCount - BlankCellCount;

            //Store to DB
            SaveToDatabase(request.InstanceId, request.GridSize, moveNumber, request.CellStates);
            
            if (request.NumberOfPlayers == 1 && response.Status == Models.TicTacToeGameStatus.InProgress)
            {
                ITicTacToeComputerPlayer computerPlayer =
                    ComputerPlayerConfig.CreateComputerPlayer();

                int? ComputerMove =
                    computerPlayer.GetMove(request.InstanceId, moveNumber);

                if (ComputerMove.HasValue && request.CellStates[ComputerMove.Value] == 0)
                {
                    var CellStates = request.CellStates.ToList();
                    CellStates[ComputerMove.Value] = 2;

                    //Store to Computer Move to DB
                    moveNumber++;
                    SaveToDatabase(request.InstanceId, request.GridSize, moveNumber, CellStates);

                    int BlankCellCount2 = int.MinValue;
                    List<int> WinningCells2 = new List<int>();
                    response.Status = EvaluateResult(request.GridSize, CellStates, out BlankCellCount2, out WinningCells2);

                    response.WinningCells = WinningCells2;
                    response.ComputerMove = ComputerMove;
                }
            }

            return response;
        }

        public static Models.TicTacToeGameStatus EvaluateResult(int GridSize, List<int> CellStates, out int BlankCellCount, out List<int> WinningCells)
        {
            if (CellStates == null)
                throw new ArgumentNullException("CellStates is null");

            int TotalCellCount = GridSize * GridSize;
            int winner = int.MinValue;
            bool winnerFound = false;
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

            //Check every row
            if (!winnerFound)
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
                        winnerFound = true;

                        for (int i = 0; i < GridSize; i++)
                            winningCells.Add(i + r * GridSize);
                    }
                }
            }

            //Check every column
            if (!winnerFound)
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
                        winnerFound = true;

                        for (int r = 0; r < GridSize; r++)
                            winningCells.Add(c + (r * GridSize));
                    }
                }
            }

            //Check diagonal top-bottom
            if (!winnerFound)
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
                    winnerFound = true;

                    for (int i = 0; i < GridSize; i++)
                        winningCells.Add(i * (GridSize + 1));
                }
            }

            //Check diagonal bottom-top
            if (!winnerFound)
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
                    winnerFound = true;

                    for (int i = GridSize; i >= 1; i--)
                        winningCells.Add((GridSize - 1) * i);
                }
            }

            Models.TicTacToeGameStatus Status = Models.TicTacToeGameStatus.InProgress;
            BlankCellCount = blankCellCount;

            if (winnerFound)
            {
                if (winner == 1)
                    Status = Models.TicTacToeGameStatus.Player1Wins;
                else if (winner == 2)
                    Status = Models.TicTacToeGameStatus.Player2Wins;
                
                WinningCells = winningCells;
            }
            else
            {
                if (blankCellCount <= 0)
                    Status = Models.TicTacToeGameStatus.Draw;
                
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

        public static void SaveToDatabase(string InstanceId, int GridSize, int MoveNumber, List<int> CellStates)
        {
            List<Entity.TicTacToeDataEntry> TicTacToeData = new List<Entity.TicTacToeDataEntry>();
            for (int i = 0; i < CellStates.Count; i++)
            {
                Entity.TicTacToeDataEntry newEntry = new Entity.TicTacToeDataEntry()
                {
                    CreatedDate = DateTime.UtcNow,
                    InstanceId = InstanceId,
                    GridSize = GridSize,
                    MoveNumber = MoveNumber,
                    CellIndex = i,
                    CellContent = CellStates[i]
                };
                TicTacToeData.Add(newEntry);
            }

            Entity.TicTacToeDataContext dbContext = new Entity.TicTacToeDataContext();
            dbContext.TicTacToeData.AddRange(TicTacToeData);
            dbContext.SaveChanges();
        }

        public static void PrepData(string SourceNameOrConnectionString, string DestinationNameOrConnectionString)
        {
            Entity.TicTacToeDataContext sourceContext = new Entity.TicTacToeDataContext(SourceNameOrConnectionString);
            Entity.TicTacToeDataContext destinationContext = new Entity.TicTacToeDataContext(DestinationNameOrConnectionString);

            List<string> SourceInstanceIds =
                sourceContext
                .TicTacToeData
                .Select(x => x.InstanceId)
                .ToList();

            List<string> DestinationInstanceIds =
                destinationContext
                .TicTacToeClassificationModel01
                .Select(x => x.InstanceId)
                .ToList();

            List<string> instancesToMigrate =
                SourceInstanceIds.Except(DestinationInstanceIds).ToList();

            List<Entity.TicTacToeDataEntry> entriesToPrep =
                (from entry in sourceContext.TicTacToeData
                where instancesToMigrate.Contains(entry.InstanceId)
                select entry)
                .OrderBy(x => x.InstanceId)
                .ThenBy(x => x.MoveNumber)
                .ThenBy(x => x.CellIndex)
                .ToList();

            var entriesGroupByInstanceId =
                entriesToPrep
                .GroupBy(x => x.InstanceId);

            var modelEntries =
                new List<Entity.TicTacToeClassificationModel01>();

            int InstancesImportedCount = 0;
            int MovesImportedCount = 0;
            foreach (var gbInstanceId in entriesGroupByInstanceId)
            {
                var gbInstanceMoves =
                    gbInstanceId
                    .GroupBy(x => x.MoveNumber);

                int maxMoveNumber =
                    gbInstanceMoves
                    .Max(g => g.Key);

                var entriesToEvaluate =
                    gbInstanceMoves
                    .First(g => g.Key == maxMoveNumber)
                    .OrderBy(x => x.CellIndex)
                    .ToList();

                int GridSize = gbInstanceId.Max(g => g.GridSize);
                int CellsPerEntry = GridSize * GridSize;
                List<int> CellStates = entriesToEvaluate.Select(x => x.CellContent).ToList();
                int BlankCellCount = int.MinValue;
                List<int> WinningCells = null;

                var Status =
                    TicTacToe.EvaluateResult(GridSize, CellStates, out BlankCellCount, out WinningCells);

                if (Status != Models.TicTacToeGameStatus.InProgress)
                {
                    //Check if data is complete
                    int numberOfMoves = gbInstanceMoves.Select(g => g.Key).Count();
                    if (gbInstanceMoves.Any(g => g.Count() != CellsPerEntry))
                        continue;

                    InstancesImportedCount++;

                    foreach (var move in gbInstanceMoves.OrderBy(g => g.Key).ToList())
                    {
                        var cells =
                            move
                            .OrderBy(x => x.CellIndex)
                            .Select(x => x.CellContent)
                            .ToList();

                        var modelEntry =
                            new Entity.TicTacToeClassificationModel01()
                            {
                                CreatedDate = DateTime.UtcNow,
                                InstanceId = gbInstanceId.Key,
                                MoveNumber = move.Key,
                                Cell0 = cells[0],
                                Cell1 = cells[1],
                                Cell2 = cells[2],
                                Cell3 = cells[3],
                                Cell4 = cells[4],
                                Cell5 = cells[5],
                                Cell6 = cells[6],
                                Cell7 = cells[7],
                                GameResultCode = (int)Status,
                                Draw = Status == Models.TicTacToeGameStatus.Draw,
                                Player1Wins = Status == Models.TicTacToeGameStatus.Player1Wins,
                                Player2Wins = Status == Models.TicTacToeGameStatus.Player2Wins
                            };

                        modelEntries.Add(modelEntry);
                        MovesImportedCount++;
                    }
                }
            }

            if (modelEntries.Count > 0)
            {
                destinationContext
                    .TicTacToeClassificationModel01
                    .AddRange(modelEntries);

                destinationContext.SaveChanges();
            }
        }

        public static void PrepData2(string SourceNameOrConnectionString, string DestinationNameOrConnectionString)
        {
            int GridSize = 3;
            Entity.TicTacToeDataContext sourceContext = new Entity.TicTacToeDataContext(SourceNameOrConnectionString);
            Entity.TicTacToeDataContext destinationContext = new Entity.TicTacToeDataContext(DestinationNameOrConnectionString);

            List<string> SourceInstanceIds =
                sourceContext
                .TicTacToeData
                .Where(x => x.GridSize == GridSize)
                .Select(x => x.InstanceId)
                .ToList();

            List<string> DestinationInstanceIds =
                destinationContext
                .TicTacToeClassificationModel02
                .Select(x => x.InstanceId)
                .ToList();

            List<string> instancesToMigrate =
                SourceInstanceIds.Except(DestinationInstanceIds).ToList();

            List<Entity.TicTacToeDataEntry> entriesToPrep =
                (from entry in sourceContext.TicTacToeData
                 where instancesToMigrate.Contains(entry.InstanceId)
                 select entry)
                .OrderBy(x => x.InstanceId)
                .ThenBy(x => x.MoveNumber)
                .ThenBy(x => x.CellIndex)
                .ToList();

            var entriesGroupByInstanceId =
                entriesToPrep
                .GroupBy(x => x.InstanceId);

            var modelEntries =
                new List<Entity.TicTacToeClassificationModel02>();

            int InstancesImportedCount = 0;
            int MovesImportedCount = 0;
            foreach (var gbInstanceId in entriesGroupByInstanceId)
            {
                var gbInstanceMoves =
                    gbInstanceId
                    .GroupBy(x => x.MoveNumber);

                int maxMoveNumber =
                    gbInstanceMoves
                    .Max(g => g.Key);

                var entriesToEvaluate =
                    gbInstanceMoves
                    .First(g => g.Key == maxMoveNumber)
                    .OrderBy(x => x.CellIndex)
                    .ToList();

                int CellsPerEntry = GridSize * GridSize;
                List<int> CellStates = entriesToEvaluate.Select(x => x.CellContent).ToList();
                int BlankCellCount = int.MinValue;
                List<int> WinningCells = null;

                var Status =
                    TicTacToe.EvaluateResult(GridSize, CellStates, out BlankCellCount, out WinningCells);

                if (Status != Models.TicTacToeGameStatus.InProgress)
                {
                    //Check if data is complete
                    int numberOfMoves = gbInstanceMoves.Select(g => g.Key).Count();
                    if (gbInstanceMoves.Any(g => g.Count() != CellsPerEntry))
                        continue;

                    InstancesImportedCount++;

                    foreach (var moves in gbInstanceMoves.OrderBy(g => g.Key).ToList())
                    {
                        foreach (var move in moves.OrderBy(x => x.CellIndex))
                        {
                            var modelEntry =
                                new Entity.TicTacToeClassificationModel02()
                                {
                                    InstanceId = gbInstanceId.Key,
                                    MoveNumber = move.MoveNumber,
                                    CellIndex = move.CellIndex,
                                    CellContent = move.CellContent,
                                    GameResultCode = (int)Status
                                };

                            if (!modelEntries.Any(x => x.InstanceId == modelEntry.InstanceId && x.CellIndex == modelEntry.CellIndex && x.CellContent == modelEntry.CellContent && x.GameResultCode == modelEntry.GameResultCode))
                            {
                                modelEntries.Add(modelEntry);
                                MovesImportedCount++;
                            }
                        }
                    }
                }
            }

            if (modelEntries.Count > 0)
            {
                destinationContext
                    .TicTacToeClassificationModel02
                    .AddRange(modelEntries);

                destinationContext.SaveChanges();
            }
        }
    }
}