using Microsoft.ML;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using T3Ent = TicTacToe.Entity;
using T3ML = TicTacToe.ML;
using T3Mod = TicTacToe.Models;

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

        public static T3Mod.TicTacToeUpdateResponse ProcessRequest(T3Mod.TicTacToeUpdateRequest request, ComputerPlayerBase computerPlayer, string MLNetModelPath, string Description)
        {
            T3Mod.TicTacToeUpdateResponse response = null;

            T3Ent.TicTacToeGame game = null;
            var latestMove = new List<T3Ent.TicTacToeDataEntry>();
            TicTacToe.ValidateTicTacToeUpdateRequest(request, computerPlayer, out game, out latestMove);

            int moveNumber = latestMove.Any() ? latestMove.Max(x => x.MoveNumber) : 0;
            int cellsChanged = 0;
            latestMove.ForEach(
            x =>
            {
                if (x.CellContent != request.CellStates[x.CellIndex])
                    cellsChanged++;
            });

            // Evaluate Game Outcome after Player 1 made a move
            var response1 =
                TicTacToe.EvaluateResult(request, computerPlayer);

            if (!latestMove.Any() || cellsChanged == 1)
            {
                // Save: New Game or New Move
                moveNumber++;
                TicTacToe.SaveToDatabase(request.InstanceId, request.GridSize, moveNumber, response1.Status, request.CellStates, Description);
            }

            response = response1;

            if (request.NumberOfPlayers == 1
                && (!latestMove.Any() || cellsChanged == 1)
                && response1.Status == T3Mod.TicTacToeGameStatus.InProgress)
            {
                // Computer Player Moves
                int? ComputerMove =
                    computerPlayer.GetMove(request.GridSize, request.CellStates.ToList());

                if (ComputerMove.HasValue && request.CellStates[ComputerMove.Value] == 0)
                {
                    var CellStates = request.CellStates.ToList();
                    CellStates[ComputerMove.Value] = computerPlayer.PlayerSymbolSelf;

                    // Evaluate Game Outcome after Player 2 made a move
                    int BlankCellCount2 = int.MinValue;
                    List<int> WinningCells2 = new List<int>();

                    var response2 =
                        new T3Mod.TicTacToeUpdateResponse()
                        {
                            Status = TicTacToe.EvaluateResult(computerPlayer, request.GridSize, CellStates, out BlankCellCount2, out WinningCells2),
                            ComputerMove = ComputerMove,
                            WinningCells = WinningCells2
                        };

                    //Save Computer Player's move
                    moveNumber++;
                    TicTacToe.SaveToDatabase(request.InstanceId, request.GridSize, moveNumber, response2.Status, CellStates, Description);

                    response = response2;
                }
            }

            var cellStates = request.CellStates.ToList();

            if (!cellStates.Any(x => !TicTacToe.ValidCellStateValues.Contains(x)))
            {
                if (response.ComputerMove.HasValue)
                {
                    cellStates[response.ComputerMove.Value] = computerPlayer.PlayerSymbolSelf;
                }

                var mlContext = new MLContext();
                ITransformer mlModel = mlContext.Model.Load(MLNetModelPath, out var _);
                var predEngine = mlContext.Model.CreatePredictionEngine<T3ML.MLModel1.ModelInput, T3ML.MLModel1.ModelOutput>(mlModel);

                var inputModel1 =
                    new T3ML.MLModel1.ModelInput()
                    {
                        MoveNumber = moveNumber,
                        Cell0 = request.CellStates[0],
                        Cell1 = request.CellStates[1],
                        Cell2 = request.CellStates[2],
                        Cell3 = request.CellStates[3],
                        Cell4 = request.CellStates[4],
                        Cell5 = request.CellStates[5],
                        Cell6 = request.CellStates[6],
                        Cell7 = request.CellStates[7],
                        Cell8 = request.CellStates[8],
                        GameResultCode = 0
                    };

                // Get Prediction
                var prediction1 = predEngine.Predict(inputModel1);

                response.Prediction = prediction1.PredictedLabel;
                response.PredictionScore = prediction1.Score;
            }

            return response;
        }

        public static Models.TicTacToeUpdateResponse EvaluateResult(Models.TicTacToeUpdateRequest request, ComputerPlayerBase computerPlayer)
        {
            //Validate TicTacToeUpdateRequest
            T3Ent.TicTacToeGame game = null;
            var latestMove = new List<T3Ent.TicTacToeDataEntry>();
            TicTacToe.ValidateTicTacToeUpdateRequest(request, computerPlayer, out game, out latestMove);

            List<int> WinningCells = null;
            int BlankCellCount = int.MinValue;

            Models.TicTacToeUpdateResponse response = new Models.TicTacToeUpdateResponse()
            {
                Status = EvaluateResult(computerPlayer, request.GridSize, request.CellStates, out BlankCellCount, out WinningCells),
                WinningCells = WinningCells,
                ComputerMove = null,
                Prediction = null,
                PredictionScore = null
            };

            return response;
        }

        public static Models.TicTacToeGameStatus EvaluateResult(ComputerPlayerBase computerPlayer, int GridSize, List<int> CellStates, out int BlankCellCount, out List<int> WinningCells)
        {
            if (computerPlayer == null)
                throw new CustomValidationException(new ArgumentNullException("computerPlayer should not be null"));

            if (CellStates == null)
                throw new CustomValidationException(new ArgumentNullException("CellStates should not be null"));

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
                    throw new CustomValidationException(new ArgumentException(string.Format("Cell[{0},{1}] is invalid", row, col)));

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
                if (winner == computerPlayer.PlayerSymbolOpponent || winner == computerPlayer.PlayerSymbolSelf)
                    Status = (Models.TicTacToeGameStatus)winner;
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

        public static List<Entity.TicTacToeDataEntry> GetAndValidatePreviousMove(long InstanceId, out Entity.TicTacToeGame? game)
        {
            Entity.TicTacToeDataContext context = new Entity.TicTacToeDataContext();

            Entity.TicTacToeGame? matchingGame =
                (from g in context.TicTacToeGames
                 where
                     g.InstanceId == InstanceId
                 select g).FirstOrDefault();

            List<Entity.TicTacToeDataEntry> ds =
                (from dr in context.TicTacToeData
                 where
                     dr.InstanceId == InstanceId
                 orderby
                     dr.MoveNumber,
                     dr.CellIndex
                 select dr).ToList();

            game = matchingGame;

            if (game == null && ds.Any())
                throw new CustomValidationException(new DataException("TicTacToeData has orphaned entries."));

            if (game == null && !ds.Any())
                return ds;

            int LastMoveNumber = ds.Max(dr => dr.MoveNumber);

            ds = ds.Where(dr => dr.MoveNumber == LastMoveNumber).OrderBy(dr => dr.CellIndex).ToList();

            var invalidCellContent =
                ds.Where(x => !TicTacToe.ValidCellStateValues.Contains(x.CellContent))
                .ToList();

            if (invalidCellContent.Any())
                throw new CustomValidationException(
                    new ArgumentOutOfRangeException(
                        string.Format("Cells {0} has invalid cell contents", string.Join(", ", invalidCellContent))
                    )
                );

            bool allCellIndexOkay = true;
            int TotalCellCount = game.GridSize * game.GridSize;
            for (int i = 0; i < TotalCellCount; i++)
            {
                if (ds[i].CellIndex != i)
                {
                    allCellIndexOkay = false;
                    break;
                }
            }

            if (!allCellIndexOkay)
                throw new CustomValidationException(new IndexOutOfRangeException("Cell Indices are not 0 based or contiguous"));

            return ds;
        }

        public static void ValidateTicTacToeUpdateRequest(Models.TicTacToeUpdateRequest request, ComputerPlayerBase computerPlayer, out Entity.TicTacToeGame game, out List<Entity.TicTacToeDataEntry> latestMove)
        {
            if (request == null)
                throw new CustomValidationException(new ArgumentNullException("Request is null"));
            if (request.CellStates == null)
                throw new CustomValidationException(new ArgumentNullException("Request.CellStates is null"));
            if (request.CellStates.Count != request.TotalCellCount)
                throw new CustomValidationException(new ArgumentNullException("Unexpected Request.CellStates count"));

            //Check that the request is valid
            var innerEx = new ArgumentException("TicTacToeUpdateRequest is invalid.");
            var exInvalidRquest = new CustomValidationException(innerEx);

            if (request.CellStates.Count() != request.TotalCellCount)
                throw exInvalidRquest;

            if (request.CellStates.Count(x => x == 0) == request.TotalCellCount)
                throw exInvalidRquest;

            if (request.CellStates.Any(x => !TicTacToe.ValidCellStateValues.Contains(x)))
                throw exInvalidRquest;

            //Validate the last entry first
            latestMove = TicTacToe.GetAndValidatePreviousMove(request.InstanceId, out game);

            if (request.CellStates.Count(x => x == computerPlayer.PlayerSymbolOpponent) == 1
                && request.CellStates.Count(x => x == 0) == request.TotalCellCount - 1
                && (game == null || !latestMove.Any()))
                return;

            if (game == null || game.GridSize != request.GridSize)
                throw exInvalidRquest;

            if (latestMove.Count() != request.TotalCellCount)
                throw exInvalidRquest;

            int changedCellCount = 0;
            for (int i = 0; i < latestMove.Count(); i++)
            {
                if (request.CellStates[i] != latestMove[i].CellContent)
                {
                    changedCellCount++;
                    if (latestMove[i].CellContent != 0 || changedCellCount > 1)
                        throw exInvalidRquest;
                }
            }
        }

        public static void SaveToDatabase(long InstanceId, int GridSize, int MoveNumber, T3Mod.TicTacToeGameStatus? Status, List<int> CellStates, string Description)
        {
            Entity.TicTacToeDataContext dbContext = new Entity.TicTacToeDataContext();
            var game = dbContext.TicTacToeGames.Where(g => g.InstanceId == InstanceId).FirstOrDefault();

            if (game == null)
            {
                var newGame = new T3Ent.TicTacToeGame()
                {
                    InstanceId = InstanceId,
                    Description = Description,
                    GridSize = GridSize,
                    Status = Status,
                    CreatedDate = DateTime.UtcNow,
                    CompletedDate = null
                };

                dbContext.TicTacToeGames.Add(newGame);
            }
            else if (Status.HasValue)
            {
                game.Status = Status;

                if (Status.Value == T3Mod.TicTacToeGameStatus.Player1Wins
                    || Status.Value == T3Mod.TicTacToeGameStatus.Player2Wins
                    || Status.Value == T3Mod.TicTacToeGameStatus.Draw)
                    game.CompletedDate = DateTime.UtcNow;
            }

            List<Entity.TicTacToeDataEntry> TicTacToeData = new List<Entity.TicTacToeDataEntry>();
            for (int i = 0; i < CellStates.Count; i++)
            {
                Entity.TicTacToeDataEntry newEntry = new Entity.TicTacToeDataEntry()
                {
                    CreatedDate = DateTime.UtcNow,
                    InstanceId = InstanceId,
                    MoveNumber = MoveNumber,
                    CellIndex = i,
                    CellContent = CellStates[i]
                };
                TicTacToeData.Add(newEntry);
            }

            dbContext.TicTacToeData.AddRange(TicTacToeData);
            dbContext.SaveChanges();
        }

        //public static void PrepData(string SourceNameOrConnectionString, string DestinationNameOrConnectionString, ComputerPlayerBase computerPlayer)
        //{
        //    Entity.TicTacToeDataContext sourceContext = new Entity.TicTacToeDataContext(SourceNameOrConnectionString);
        //    Entity.TicTacToeDataContext destinationContext = new Entity.TicTacToeDataContext(DestinationNameOrConnectionString);

        //    List<string> SourceInstanceIds =
        //        sourceContext
        //        .TicTacToeData
        //        .Select(x => x.InstanceId)
        //        .ToList();

        //    List<string> DestinationInstanceIds =
        //        destinationContext
        //        .TicTacToeClassificationModel01
        //        .Select(x => x.InstanceId)
        //        .ToList();

        //    List<string> instancesToMigrate =
        //        SourceInstanceIds.Except(DestinationInstanceIds).ToList();

        //    List<Entity.TicTacToeDataEntry> entriesToPrep =
        //        (from entry in sourceContext.TicTacToeData
        //        where instancesToMigrate.Contains(entry.InstanceId)
        //        select entry)
        //        .OrderBy(x => x.InstanceId)
        //        .ThenBy(x => x.MoveNumber)
        //        .ThenBy(x => x.CellIndex)
        //        .ToList();

        //    var entriesGroupByInstanceId =
        //        entriesToPrep
        //        .GroupBy(x => x.InstanceId);

        //    var modelEntries =
        //        new List<Entity.TicTacToeClassificationModel01>();

        //    int InstancesImportedCount = 0;
        //    int MovesImportedCount = 0;
        //    foreach (var gbInstanceId in entriesGroupByInstanceId)
        //    {
        //        var gbInstanceMoves =
        //            gbInstanceId
        //            .GroupBy(x => x.MoveNumber);

        //        int maxMoveNumber =
        //            gbInstanceMoves
        //            .Max(g => g.Key);

        //        var entriesToEvaluate =
        //            gbInstanceMoves
        //            .First(g => g.Key == maxMoveNumber)
        //            .OrderBy(x => x.CellIndex)
        //            .ToList();

        //        int GridSize = gbInstanceId.Max(g => g.GridSize);
        //        int CellsPerEntry = GridSize * GridSize;
        //        List<int> CellStates = entriesToEvaluate.Select(x => x.CellContent).ToList();
        //        int BlankCellCount = int.MinValue;
        //        List<int> WinningCells = null;

        //        var Status =
        //            TicTacToe.EvaluateResult(computerPlayer, GridSize, CellStates, out BlankCellCount, out WinningCells);

        //        if (Status != Models.TicTacToeGameStatus.InProgress)
        //        {
        //            //Check if data is complete
        //            int numberOfMoves = gbInstanceMoves.Select(g => g.Key).Count();
        //            if (gbInstanceMoves.Any(g => g.Count() != CellsPerEntry))
        //                continue;

        //            InstancesImportedCount++;

        //            foreach (var move in gbInstanceMoves.OrderBy(g => g.Key).ToList())
        //            {
        //                var cells =
        //                    move
        //                    .OrderBy(x => x.CellIndex)
        //                    .Select(x => x.CellContent)
        //                    .ToList();

        //                var modelEntry =
        //                    new Entity.TicTacToeClassificationModel01()
        //                    {
        //                        CreatedDate = DateTime.UtcNow,
        //                        InstanceId = gbInstanceId.Key,
        //                        MoveNumber = move.Key,
        //                        Cell0 = cells[0],
        //                        Cell1 = cells[1],
        //                        Cell2 = cells[2],
        //                        Cell3 = cells[3],
        //                        Cell4 = cells[4],
        //                        Cell5 = cells[5],
        //                        Cell6 = cells[6],
        //                        Cell7 = cells[7],
        //                        GameResultCode = (int)Status,
        //                        Draw = Status == Models.TicTacToeGameStatus.Draw,
        //                        Player1Wins = Status == Models.TicTacToeGameStatus.Player1Wins,
        //                        Player2Wins = Status == Models.TicTacToeGameStatus.Player2Wins
        //                    };

        //                modelEntries.Add(modelEntry);
        //                MovesImportedCount++;
        //            }
        //        }
        //    }

        //    if (modelEntries.Count > 0)
        //    {
        //        destinationContext
        //            .TicTacToeClassificationModel01
        //            .AddRange(modelEntries);

        //        destinationContext.SaveChanges();
        //    }
        //}

        //public static void PrepData2(string SourceNameOrConnectionString, string DestinationNameOrConnectionString, ComputerPlayerBase computerPlayer)
        //{
        //    int GridSize = 3;
        //    Entity.TicTacToeDataContext sourceContext = new Entity.TicTacToeDataContext(SourceNameOrConnectionString);
        //    Entity.TicTacToeDataContext destinationContext = new Entity.TicTacToeDataContext(DestinationNameOrConnectionString);

        //    List<string> SourceInstanceIds =
        //        sourceContext
        //        .TicTacToeData
        //        .Where(x => x.GridSize == GridSize)
        //        .Select(x => x.InstanceId)
        //        .ToList();

        //    List<string> DestinationInstanceIds =
        //        destinationContext
        //        .TicTacToeClassificationModel02
        //        .Select(x => x.InstanceId)
        //        .ToList();

        //    List<string> instancesToMigrate =
        //        SourceInstanceIds.Except(DestinationInstanceIds).ToList();

        //    List<Entity.TicTacToeDataEntry> entriesToPrep =
        //        (from entry in sourceContext.TicTacToeData
        //         where instancesToMigrate.Contains(entry.InstanceId)
        //         select entry)
        //        .OrderBy(x => x.InstanceId)
        //        .ThenBy(x => x.MoveNumber)
        //        .ThenBy(x => x.CellIndex)
        //        .ToList();

        //    var entriesGroupByInstanceId =
        //        entriesToPrep
        //        .GroupBy(x => x.InstanceId);

        //    var modelEntries =
        //        new List<Entity.TicTacToeClassificationModel02>();

        //    int InstancesImportedCount = 0;
        //    int MovesImportedCount = 0;
        //    foreach (var gbInstanceId in entriesGroupByInstanceId)
        //    {
        //        var gbInstanceMoves =
        //            gbInstanceId
        //            .GroupBy(x => x.MoveNumber);

        //        int maxMoveNumber =
        //            gbInstanceMoves
        //            .Max(g => g.Key);

        //        var entriesToEvaluate =
        //            gbInstanceMoves
        //            .First(g => g.Key == maxMoveNumber)
        //            .OrderBy(x => x.CellIndex)
        //            .ToList();

        //        int CellsPerEntry = GridSize * GridSize;
        //        List<int> CellStates = entriesToEvaluate.Select(x => x.CellContent).ToList();
        //        int BlankCellCount = int.MinValue;
        //        List<int> WinningCells = null;

        //        var Status =
        //            TicTacToe.EvaluateResult(computerPlayer, GridSize, CellStates, out BlankCellCount, out WinningCells);

        //        if (Status != Models.TicTacToeGameStatus.InProgress)
        //        {
        //            //Check if data is complete
        //            int numberOfMoves = gbInstanceMoves.Select(g => g.Key).Count();
        //            if (gbInstanceMoves.Any(g => g.Count() != CellsPerEntry))
        //                continue;

        //            InstancesImportedCount++;

        //            foreach (var moves in gbInstanceMoves.OrderBy(g => g.Key).ToList())
        //            {
        //                foreach (var move in moves.Where(x => x.CellContent != 0).OrderBy(x => x.CellIndex))
        //                {
        //                    var modelEntry =
        //                        new Entity.TicTacToeClassificationModel02()
        //                        {
        //                            InstanceId = gbInstanceId.Key,
        //                            MoveNumber = move.MoveNumber,
        //                            CellIndex = move.CellIndex,
        //                            CellContent = move.CellContent,
        //                            GameResultCode = (int)Status
        //                        };

        //                    if (!modelEntries.Any(x => x.InstanceId == modelEntry.InstanceId && x.CellIndex == modelEntry.CellIndex && x.CellContent == modelEntry.CellContent && x.GameResultCode == modelEntry.GameResultCode))
        //                    {
        //                        modelEntries.Add(modelEntry);
        //                        MovesImportedCount++;
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    if (modelEntries.Count > 0)
        //    {
        //        destinationContext
        //            .TicTacToeClassificationModel02
        //            .AddRange(modelEntries);

        //        destinationContext.SaveChanges();
        //    }
        //}
    }
}