using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.ML;
using Newtonsoft.Json.Linq;
using TicTacToe.Extensions;
using T3BL = TicTacToe.BusinessLogic;
using T3Ent = TicTacToe.Entity;
using T3ML = TicTacToe.ML;
using T3Mod = TicTacToe.Models;

namespace UnitTests
{
    public class TicTacToeTests
    {
        private static Random random = new Random();
        private static string _ComputerPlayerModelPath = string.Empty;
        private static string _OutcomePredictionModelPath = string.Empty;

        public class GameSetting
        {
            public int SymbolPlayerOpponent { get; set; }
            public int SymbolPlayerSelf { get; set; }
            public long GameId { get; set; }
        }

        [SetUp]
        public void TestSetup()
        {
            string solutionDir =
                new DirectoryInfo(
                    Path.Combine(
                        TestContext.CurrentContext.TestDirectory,
                        "..", "..", "..", "..")).FullName;

            var TicTacToeDataConnString =
                TestContext
                .Parameters
                .Get("TicTacToeDataConnString", string.Empty)
                .Replace("$(SolutionDir)", solutionDir);

            Assert.IsNotEmpty(TicTacToeDataConnString);
            
            TicTacToe
                .Entity
                .DbContextConfig
                .AddOrReplace("TicTacToeData", TicTacToeDataConnString);

            _ComputerPlayerModelPath =
                TestContext
                .Parameters
                .Get("ComputerPlayerModelPath", string.Empty)
                .Replace("$(SolutionDir)", solutionDir)
                .Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);

            _OutcomePredictionModelPath =
                TestContext
                .Parameters
                .Get("OutcomePredictionModelPath", string.Empty)
                .Replace("$(SolutionDir)", solutionDir)
                .Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
        }

        [Test]
        public void TestValid1PGamesV1()
        {
            var computerPlayer = new T3BL.ComputerPlayerV1();
            var gameoutcomes = TestValid1PGames(computerPlayer);
            VerifyGameOutcomes(gameoutcomes);
        }

        [Test]
        public void TestValid1PGamesV2()
        {
            var computerPlayer = new T3BL.ComputerPlayerV2();
            var gameoutcomes = TestValid1PGames(computerPlayer);
            VerifyGameOutcomes(gameoutcomes);
        }

        [Test]
        public void TestValid1PGamesV3()
        {
            var computerPlayer = new T3BL.ComputerPlayerV3(_ComputerPlayerModelPath);
            var gameoutcomes = TestValid1PGames(computerPlayer);
            VerifyGameOutcomes(gameoutcomes);
        }

        public Dictionary<TicTacToe.Models.TicTacToeGameStatus, List<GameSetting>> TestValid1PGames(T3BL.ComputerPlayerBase computerPlayer)
        {
            string Description = $"UnitTest 1P Valid Test - {computerPlayer.GetType().Name}";
            int gamesCount = 100;
            var gameOutcomes = new Dictionary<TicTacToe.Models.TicTacToeGameStatus, List<GameSetting>>();
            long ticks = DateTime.UtcNow.Ticks;
            for (int g = 0; g < gamesCount; g++)
            {
                var gameStatus = T3Mod.TicTacToeGameStatus.InProgress;
                long InstanceId = ticks + g;
                var request = new T3Mod.TicTacToeUpdateRequest()
                {
                    InstanceId = InstanceId,
                    GridSize = 3,
                    NumberOfPlayers = 1,
                    CellStates = new List<int>()
                    {
                        0, 0, 0,
                        0, 0, 0,
                        0, 0, 0,
                    }
                };

                while (gameStatus == T3Mod.TicTacToeGameStatus.InProgress)
                {
                    List<int> validMoves = new List<int>();
                    for (int c = 0; c < request.CellStates.Count; c++)
                    {
                        if (request.CellStates[c] == 0)
                            validMoves.Add(c);
                    }

                    int index = random.Next(validMoves.Count);
                    request.CellStates[validMoves[index]] = computerPlayer.PlayerSymbolOpponent;

                    var response =
                        T3BL.TicTacToe.ProcessRequest(request, computerPlayer, _OutcomePredictionModelPath, Description);

                    gameStatus = response.Status;

                    if (response.ComputerMove.HasValue)
                        request.CellStates[response.ComputerMove.Value] = computerPlayer.PlayerSymbolSelf;
                }

                var gameSetting = new GameSetting()
                {
                    SymbolPlayerOpponent = computerPlayer.PlayerSymbolOpponent,
                    SymbolPlayerSelf = computerPlayer.PlayerSymbolSelf,
                    GameId = InstanceId
                };

                if (gameOutcomes.ContainsKey(gameStatus))
                    gameOutcomes[gameStatus].Add(gameSetting);
                else
                    gameOutcomes.Add(gameStatus, new List<GameSetting>() { gameSetting });
            }

            return gameOutcomes;
        }

        [Test]
        public void TestInvalid1PGamesV1()
        {
            var computerPlayer = new T3BL.ComputerPlayerV1();
            TestInvalid1PGames(computerPlayer);
        }

        [Test]
        public void TestInvalid1PGamesV2()
        {
            var computerPlayer = new T3BL.ComputerPlayerV2();
            TestInvalid1PGames(computerPlayer);
        }

        [Test]
        public void TestInvalid1PGamesV3()
        {
            var computerPlayer = new T3BL.ComputerPlayerV3(_ComputerPlayerModelPath);
            TestInvalid1PGames(computerPlayer);
        }

        public void TestInvalid1PGames(T3BL.ComputerPlayerBase computerPlayer)
        {
            string Description = $"UnitTest 1P Invalid Test - {computerPlayer.GetType().Name}";
            long InstanceId = DateTime.UtcNow.Ticks;
            var request = new TicTacToe.Models.TicTacToeUpdateRequest()
            {
                InstanceId = InstanceId,
                GridSize = 3,
                NumberOfPlayers = 1,
                CellStates = new List<int>()
                {
                    0, 0, 0,
                    0, 0, 0,
                    0, 0, 0,
                }
            };

            for (int i = 0; i < 4; i++)
            {
                List<int> validMoves = new List<int>();
                for (int c = 0; c < request.CellStates.Count; c++)
                {
                    if (request.CellStates[c] == 0)
                        validMoves.Add(c);
                }

                int index = random.Next(validMoves.Count);
                request.CellStates[validMoves[index]] = computerPlayer.PlayerSymbolOpponent;

                var response = T3BL.TicTacToe.ProcessRequest(request, computerPlayer, _OutcomePredictionModelPath, Description);

                if (response.ComputerMove.HasValue)
                    request.CellStates[response.ComputerMove.Value] = computerPlayer.PlayerSymbolSelf;
            }

            //Each player has moved twice each
            var backupCellStates = request.CellStates.ToList();

            try
            {
                //Test Invalid Cell Value
                request.CellStates = backupCellStates.ToList();

                List<int> validMoves = new List<int>();
                for (int c = 0; c < request.CellStates.Count; c++)
                {
                    if (request.CellStates[c] == 0)
                        validMoves.Add(c);
                }

                int index = random.Next(validMoves.Count);
                request.CellStates[validMoves[index]] = 3; //Invalid value

                var response = T3BL.TicTacToe.ProcessRequest(request, computerPlayer, _OutcomePredictionModelPath, Description);
                Assert.Fail();
            }
            catch (Exception ex)
            {
            }

            try
            {
                //Test Player 1 Move Twice
                request.CellStates = backupCellStates.ToList();

                for (int i = 0; i < 2; i++)
                {
                    List<int> validMoves = new List<int>();
                    for (int c = 0; c < request.CellStates.Count; c++)
                    {
                        if (request.CellStates[c] == 0)
                            validMoves.Add(c);
                    }

                    int index = random.Next(validMoves.Count);
                    request.CellStates[validMoves[index]] = computerPlayer.PlayerSymbolOpponent;
                }

                var response = T3BL.TicTacToe.ProcessRequest(request, computerPlayer, _OutcomePredictionModelPath, Description);
                Assert.Fail();
            }
            catch (Exception ex)
            {
            }

            try
            {
                //Test Player 1 Alter Player 2's Cells
                request.CellStates = backupCellStates.ToList();

                List<int> validMoves = new List<int>();
                for (int c = 0; c < request.CellStates.Count; c++)
                {
                    if (request.CellStates[c] == computerPlayer.PlayerSymbolSelf)
                        validMoves.Add(c);
                }

                int index = random.Next(validMoves.Count);
                request.CellStates[validMoves[index]] = computerPlayer.PlayerSymbolOpponent;

                var response = T3BL.TicTacToe.ProcessRequest(request, computerPlayer, _OutcomePredictionModelPath, Description);
                Assert.Fail();
            }
            catch (Exception ex)
            {
            }

            try
            {
                //Test Player 1 Alter Player 2's Cell and Make a move
                request.CellStates = backupCellStates.ToList();

                //Alter Player 2's Cell
                List<int> validMoves = new List<int>();
                for (int c = 0; c < request.CellStates.Count; c++)
                {
                    if (request.CellStates[c] == computerPlayer.PlayerSymbolSelf)
                        validMoves.Add(c);
                }

                int index = random.Next(validMoves.Count);
                request.CellStates[validMoves[index]] = computerPlayer.PlayerSymbolOpponent;

                // Make a move
                validMoves = new List<int>();
                for (int c = 0; c < request.CellStates.Count; c++)
                {
                    if (request.CellStates[c] == 0)
                        validMoves.Add(c);
                }

                index = random.Next(validMoves.Count);
                request.CellStates[validMoves[index]] = computerPlayer.PlayerSymbolOpponent;

                var response = T3BL.TicTacToe.ProcessRequest(request, computerPlayer, _OutcomePredictionModelPath, Description);
                Assert.Fail();
            }
            catch (Exception ex)
            {
            }

            try
            {
                //Test Invalid GridSize
                request.GridSize = 5;
                var response = T3BL.TicTacToe.ProcessRequest(request, computerPlayer, _OutcomePredictionModelPath, Description);
                Assert.Fail();
            }
            catch (Exception ex)
            {
            }

            try
            {
                //Test Invalid CellStates Array Size
                request.CellStates = new List<int>();
                request.CellStates.AddRange(backupCellStates.ToList());
                request.CellStates.AddRange(new List<int>{ 0,0,0,0,0,0,0 });
                request.GridSize = (int)Math.Sqrt(request.CellStates.Count());

                //Make a move
                List<int> validMoves = new List<int>();
                for (int c = 0; c < request.CellStates.Count; c++)
                {
                    if (request.CellStates[c] == 0)
                        validMoves.Add(c);
                }

                int index = random.Next(validMoves.Count);
                request.CellStates[validMoves[index]] = computerPlayer.PlayerSymbolOpponent;

                var response = T3BL.TicTacToe.ProcessRequest(request, computerPlayer, _OutcomePredictionModelPath, Description);
                Assert.Fail();
            }
            catch (Exception ex)
            {
            }
        }

        [Test]
        public void TestValid2PGamesV1vsV1()
        {
            var player1 = new T3BL.ComputerPlayerV1(2, 1);
            var player2 = new T3BL.ComputerPlayerV1(1, 2);
            var gameoutcomes = TestValidGamesComVsCom(player1, player2);
            VerifyGameOutcomes(gameoutcomes);
        }

        [Test]
        public void TestValid2PGamesV1vsV2()
        {
            var player1 = new T3BL.ComputerPlayerV1(2, 1);
            var player2 = new T3BL.ComputerPlayerV2(1, 2);
            var gameoutcomes = TestValidGamesComVsCom(player1, player2);
            VerifyGameOutcomes(gameoutcomes);
        }

        [Test]
        public void TestValid2PGamesV2vsV1()
        {
            var player1 = new T3BL.ComputerPlayerV2(2, 1);
            var player2 = new T3BL.ComputerPlayerV1(1, 2);
            var gameoutcomes = TestValidGamesComVsCom(player1, player2);
            VerifyGameOutcomes(gameoutcomes);
        }

        [Test]
        public void TestValid2PGamesV2vsV2()
        {
            var player1 = new T3BL.ComputerPlayerV2(2, 1);
            var player2 = new T3BL.ComputerPlayerV2(1, 2);
            var gameoutcomes = TestValidGamesComVsCom(player1, player2);
            VerifyGameOutcomes(gameoutcomes);
        }

        [Test]
        public void TestValid2PGamesV2vsV3()
        {
            var player1 = new T3BL.ComputerPlayerV2(2, 1);
            var player2 = new T3BL.ComputerPlayerV3(1, 2, _ComputerPlayerModelPath);
            var gameoutcomes = TestValidGamesComVsCom(player1, player2);
            VerifyGameOutcomes(gameoutcomes);
        }

        [Test]
        public void TestValid2PGamesV3vsV2()
        {
            var player1 = new T3BL.ComputerPlayerV3(2, 1, _ComputerPlayerModelPath);
            var player2 = new T3BL.ComputerPlayerV2(1, 2);
            var gameoutcomes = TestValidGamesComVsCom(player1, player2);
            VerifyGameOutcomes(gameoutcomes);
        }

        [Test]
        public void TestValid2PGamesV3vsV3()
        {
            var player1 = new T3BL.ComputerPlayerV3(2, 1, _ComputerPlayerModelPath);
            var player2 = new T3BL.ComputerPlayerV3(1, 2, _ComputerPlayerModelPath);
            var gameoutcomes = TestValidGamesComVsCom(player1, player2);
            VerifyGameOutcomes(gameoutcomes);
        }

        public Dictionary<TicTacToe.Models.TicTacToeGameStatus, List<GameSetting>> TestValidGamesComVsCom(T3BL.ComputerPlayerBase computerPlayer1, T3BL.ComputerPlayerBase computerPlayer2)
        {
            string Description = $"UnitTest 2P Valid Test - P1: {computerPlayer1.GetType().Name} P2: {computerPlayer2.GetType().Name}";
            int gamesCount = 100;
            var gameOutcomes = new Dictionary<TicTacToe.Models.TicTacToeGameStatus, List<GameSetting>>();
            long ticks = DateTime.UtcNow.Ticks;
            for (int g = 0; g < gamesCount; g++)
            {
                var gameStatus = T3Mod.TicTacToeGameStatus.InProgress;
                long InstanceId = ticks + g;
                var request = new T3Mod.TicTacToeUpdateRequest()
                {
                    InstanceId = InstanceId,
                    GridSize = 3,
                    NumberOfPlayers = 1,
                    CellStates = new List<int>()
                    {
                        0, 0, 0,
                        0, 0, 0,
                        0, 0, 0,
                    }
                };

                while (gameStatus == T3Mod.TicTacToeGameStatus.InProgress)
                {
                    int p1Move = computerPlayer1.GetMove(request.GridSize, request.CellStates.ToList());
                    if (request.CellStates[p1Move] == 0)
                        request.CellStates[p1Move] = computerPlayer1.PlayerSymbolSelf;

                    var response =
                        T3BL.TicTacToe.ProcessRequest(request, computerPlayer2, _OutcomePredictionModelPath, Description);

                    gameStatus = response.Status;

                    if (response.ComputerMove.HasValue)
                        request.CellStates[response.ComputerMove.Value] = computerPlayer2.PlayerSymbolSelf;
                }

                var gameSetting = new GameSetting()
                {
                    SymbolPlayerOpponent = computerPlayer2.PlayerSymbolOpponent,
                    SymbolPlayerSelf = computerPlayer2.PlayerSymbolSelf,
                    GameId = InstanceId
                };

                if (gameOutcomes.ContainsKey(gameStatus))
                    gameOutcomes[gameStatus].Add(gameSetting);
                else
                {
                    gameOutcomes.Add(
                        gameStatus,
                        new List<GameSetting>() { gameSetting }
                    );
                }
            }

            return gameOutcomes;
        }

        public void VerifyGameOutcomes(Dictionary<TicTacToe.Models.TicTacToeGameStatus, List<GameSetting>> gameoutcomes)
        {
            T3Ent.TicTacToeDataContext dbContext = new T3Ent.TicTacToeDataContext();
            var interestedStatus = new List<T3Mod.TicTacToeGameStatus>()
            {
                T3Mod.TicTacToeGameStatus.Player1Wins,
                T3Mod.TicTacToeGameStatus.Player2Wins,
                T3Mod.TicTacToeGameStatus.Draw
            };

            foreach (var key in gameoutcomes.Keys)
            {
                Assert.IsTrue(interestedStatus.Contains(key));
            }
            
            foreach (var status in interestedStatus)
            {
                if (!gameoutcomes.ContainsKey(status))
                    continue;

                var gameSettings = gameoutcomes[status];
                foreach (var gameSetting in gameSettings)
                {
                    var game =
                        dbContext
                        .TicTacToeGames
                        .FirstOrDefault(x => x.InstanceId == gameSetting.GameId);

                    Assert.IsNotNull(game);

                    var moves =
                        dbContext
                        .TicTacToeData
                        .Where(x => x.InstanceId == game.InstanceId)
                        .OrderBy(x => x.MoveNumber)
                        .ThenBy(x => x.CellIndex)
                        .ToList();

                    var latestMoveNumber =
                        moves.Max(x => x.MoveNumber);

                    var latestMove =
                        moves
                        .Where(x => x.MoveNumber == latestMoveNumber)
                        .OrderBy(x => x.CellIndex)
                        .ToList();

                    var computerPlayer =
                        new T3BL.ComputerPlayerBase(
                                gameSetting.SymbolPlayerOpponent,
                                gameSetting.SymbolPlayerSelf
                            );

                    int BlankCellCount = int.MinValue;
                    List<int> WinningCells = null;

                    var evaluatedStatus =
                        T3BL.TicTacToe.EvaluateResult(
                            computerPlayer,
                            game.GridSize,
                            latestMove.OrderBy(x => x.CellIndex).Select(x => x.CellContent).ToList(),
                            out BlankCellCount,
                            out WinningCells);

                    if (evaluatedStatus == T3Mod.TicTacToeGameStatus.Player1Wins
                        || evaluatedStatus == T3Mod.TicTacToeGameStatus.Player2Wins)
                        Assert.IsTrue(WinningCells.Count == game.GridSize);
                    Assert.IsTrue(evaluatedStatus == status);
                    Assert.IsTrue(evaluatedStatus == game.Status);
                }
            }
        }
    }
}