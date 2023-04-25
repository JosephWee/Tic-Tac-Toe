using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Azure;
using TicTacToe.BusinessLogic;
using TicTacToe.Extensions;
using Microsoft.ML;
using Newtonsoft.Json.Linq;
using TicTacToe.ML;
using TicTacToe.Models;
using Azure.Core;

namespace UnitTests
{
    public class TicTacToeTests
    {
        private static Random random = new Random();
        private static string _MLModel1Path = string.Empty;

        [SetUp]
        public void TestSetup()
        {
            string msbuildDir = new DirectoryInfo(Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\..")).FullName;
            var TicTacToeDataConnString = TestContext.Parameters.Get("TicTacToeDataConnString", string.Empty).Replace("$(MSBuildProjectDirectory)", msbuildDir);
            Assert.IsNotEmpty(TicTacToeDataConnString);
            
            TicTacToe
                .Entity
                .DbContextConfig
                .AddOrReplace("TicTacToeData", TicTacToeDataConnString);

            _MLModel1Path = Path.Combine(msbuildDir, "MLModels", "MLModel1.zip");
        }

        [Test]
        public void TestValidGamesV2()
        {
            var computerPlayer = new ComputerPlayerV2();
            var gameoutcomes = TestValidGames("V2", computerPlayer);
        }

        [Test]
        public void TestValidGamesV3()
        {
            var computerPlayer = new ComputerPlayerV3(_MLModel1Path);
            var gameoutcomes = TestValidGames("V3", computerPlayer);
        }

        public Dictionary<TicTacToe.Models.TicTacToeGameStatus, int> TestValidGames(string Version, ComputerPlayerBase computerPlayer)
        {
            int gamesCount = 100;
            var gameOutcomes = new Dictionary<TicTacToe.Models.TicTacToeGameStatus, int>();
            for (int g = 0; g < gamesCount; g++)
            {
                var gameStatus = TicTacToe.Models.TicTacToeGameStatus.InProgress;
                string InstanceId = $"UnitTest {Version} {g} @ {DateTime.UtcNow.ToString("o")}";
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

                while (gameStatus == TicTacToe.Models.TicTacToeGameStatus.InProgress)
                {
                    List<int> validMoves = new List<int>();
                    for (int c = 0; c < request.CellStates.Count; c++)
                    {
                        if (request.CellStates[c] == 0)
                            validMoves.Add(c);
                    }

                    int index = random.Next(validMoves.Count);
                    request.CellStates[validMoves[index]] = 1;

                    var response = TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request, computerPlayer);
                    
                    gameStatus = response.Status;

                    if (response.ComputerMove.HasValue)
                        request.CellStates[response.ComputerMove.Value] = 2;
                }

                if (gameOutcomes.ContainsKey(gameStatus))
                    gameOutcomes[gameStatus] += 1;
                else
                    gameOutcomes.Add(gameStatus,1);
            }

            return gameOutcomes;
        }

        [Test]
        public void TestInvalidGamesV2()
        {
            var computerPlayer = new ComputerPlayerV2();
            TestInvalidGames("V2", computerPlayer);
        }

        [Test]
        public void TestInvalidGamesV3()
        {
            var computerPlayer = new ComputerPlayerV3(_MLModel1Path);
            TestInvalidGames("V3", computerPlayer);
        }

        public void TestInvalidGames(string Version, ComputerPlayerBase computerPlayerBase)
        {
            string InstanceId = $"UnitTest {Version} @ {DateTime.UtcNow.ToString("o")}";
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
                request.CellStates[validMoves[index]] = 1;

                var response = TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request, computerPlayerBase);

                if (response.ComputerMove.HasValue)
                    request.CellStates[response.ComputerMove.Value] = 2;
            }

            //Each player has 2 move each
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

                var response = TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request, computerPlayerBase);
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
                    request.CellStates[validMoves[index]] = 1;
                }

                var response = TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request, computerPlayerBase);
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
                    if (request.CellStates[c] == 2)
                        validMoves.Add(c);
                }

                int index = random.Next(validMoves.Count);
                request.CellStates[validMoves[index]] = 1;

                var response = TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request, computerPlayerBase);
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
                    if (request.CellStates[c] == 2)
                        validMoves.Add(c);
                }

                int index = random.Next(validMoves.Count);
                request.CellStates[validMoves[index]] = 1;

                // Make a move
                validMoves = new List<int>();
                for (int c = 0; c < request.CellStates.Count; c++)
                {
                    if (request.CellStates[c] == 0)
                        validMoves.Add(c);
                }

                index = random.Next(validMoves.Count);
                request.CellStates[validMoves[index]] = 1;

                var response = TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request, computerPlayerBase);
                Assert.Fail();
            }
            catch (Exception ex)
            {
            }

            try
            {
                //Test Invalid GridSize
                request.GridSize = 5;
                var response = TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request, computerPlayerBase);
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
                request.CellStates[validMoves[index]] = 1;

                var response = TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request, computerPlayerBase);
                Assert.Fail();
            }
            catch (Exception ex)
            {
            }
        }

        [Test]
        public void TestValidGamesV2vsV2()
        {
            var player1 = new ComputerPlayerV2(2, 1);
            var player2 = new ComputerPlayerV2(1, 2);
            var gameoutcomes = TestValidGamesP1vsP2("V2-P1", "V2-P2", player1, player2);
        }

        [Test]
        public void TestValidGamesV2vsV3()
        {
            var player1 = new ComputerPlayerV2(2, 1);
            var player2 = new ComputerPlayerV3(1, 2, _MLModel1Path);
            var gameoutcomes = TestValidGamesP1vsP2("V2", "V3", player1, player2);
        }

        protected Dictionary<TicTacToe.Models.TicTacToeGameStatus, int> TestValidGamesP1vsP2(string P1Ver, string P2Ver, ComputerPlayerBase player1, ComputerPlayerBase player2)
        {
            int gamesCount = 100;
            var gameOutcomes = new Dictionary<TicTacToe.Models.TicTacToeGameStatus, int>();
            for (int g = 0; g < gamesCount; g++)
            {
                var gameStatus = TicTacToe.Models.TicTacToeGameStatus.InProgress;
                string InstanceId = $"UnitTest {P1Ver} vs {P2Ver} {g} @ {DateTime.UtcNow.ToString("o")}";
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

                int firstMove = player1.GetRandomMove(request.CellStates);
                request.CellStates[firstMove] = player1.PlayerSymbolSelf;

                while (gameStatus == TicTacToeGameStatus.InProgress)
                {
                    var response2 = TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request, player2);
                    gameStatus = UpdateGame(player2, request, response2);

                    if (gameStatus != TicTacToeGameStatus.InProgress)
                        break;

                    var response1 = TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request, player1);
                    gameStatus = UpdateGame(player1, request, response1);
                }

                if (gameOutcomes.ContainsKey(gameStatus))
                    gameOutcomes[gameStatus] += 1;
                else
                    gameOutcomes.Add(gameStatus, 1);
            }

            return gameOutcomes;
        }

        protected TicTacToeGameStatus UpdateGame(ComputerPlayerBase player, TicTacToeUpdateRequest request, TicTacToeUpdateResponse response)
        {
            if (request.GridSize == 3)
            {
                var cellStates = request.CellStates.ToList();

                if (!cellStates.Any(x => !TicTacToe.BusinessLogic.TicTacToe.ValidCellStateValues.Contains(x)))
                {
                    int moveNumber = cellStates.Count(x => x != 0);

                    if (response.ComputerMove.HasValue)
                    {
                        moveNumber++;
                        cellStates[response.ComputerMove.Value] = player.PlayerSymbolSelf;
                    }
                }
            }

            return response.Status;
        }
    }
}