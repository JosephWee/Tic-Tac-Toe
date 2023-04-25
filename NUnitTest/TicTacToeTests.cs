using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Azure;
using Azure.Core;
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
        public void TestValid1PGamesV1()
        {
            var computerPlayer = new T3BL.ComputerPlayerV1();
            var gameoutcomes = TestValid1PGames("V1", computerPlayer);
        }

        [Test]
        public void TestValid1PGamesV2()
        {
            var computerPlayer = new T3BL.ComputerPlayerV2();
            var gameoutcomes = TestValid1PGames("V2", computerPlayer);
        }

        [Test]
        public void TestValid1PGamesV3()
        {
            var computerPlayer = new T3BL.ComputerPlayerV3(_MLModel1Path);
            var gameoutcomes = TestValid1PGames("V3", computerPlayer);
        }

        public Dictionary<TicTacToe.Models.TicTacToeGameStatus, int> TestValid1PGames(string Version, T3BL.ComputerPlayerBase computerPlayer)
        {
            int gamesCount = 100;
            var gameOutcomes = new Dictionary<TicTacToe.Models.TicTacToeGameStatus, int>();
            for (int g = 0; g < gamesCount; g++)
            {
                var gameStatus = T3Mod.TicTacToeGameStatus.InProgress;
                string InstanceId = $"UnitTest {Version} {DateTime.UtcNow.Ticks} @ {DateTime.UtcNow.ToString("o")}";
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
                        T3BL.TicTacToe.ProcessRequest(request, computerPlayer, _MLModel1Path);

                    gameStatus = response.Status;

                    if (response.ComputerMove.HasValue)
                        request.CellStates[response.ComputerMove.Value] = computerPlayer.PlayerSymbolSelf;
                }

                if (gameOutcomes.ContainsKey(gameStatus))
                    gameOutcomes[gameStatus] += 1;
                else
                    gameOutcomes.Add(gameStatus,1);
            }

            return gameOutcomes;
        }

        [Test]
        public void TestInvalid1PGamesV1()
        {
            var computerPlayer = new T3BL.ComputerPlayerV1();
            TestInvalid1PGames("V1", computerPlayer);
        }

        [Test]
        public void TestInvalid1PGamesV2()
        {
            var computerPlayer = new T3BL.ComputerPlayerV2();
            TestInvalid1PGames("V2", computerPlayer);
        }

        [Test]
        public void TestInvalid1PGamesV3()
        {
            var computerPlayer = new T3BL.ComputerPlayerV3(_MLModel1Path);
            TestInvalid1PGames("V3", computerPlayer);
        }

        public void TestInvalid1PGames(string Version, T3BL.ComputerPlayerBase computerPlayer)
        {
            string InstanceId = $"UnitTest {Version} {DateTime.UtcNow.Ticks} @ {DateTime.UtcNow.ToString("o")}";
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

                var response = T3BL.TicTacToe.ProcessRequest(request, computerPlayer, _MLModel1Path);

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

                var response = T3BL.TicTacToe.ProcessRequest(request, computerPlayer, _MLModel1Path);
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

                var response = T3BL.TicTacToe.ProcessRequest(request, computerPlayer, _MLModel1Path);
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

                var response = T3BL.TicTacToe.ProcessRequest(request, computerPlayer, _MLModel1Path);
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

                var response = T3BL.TicTacToe.ProcessRequest(request, computerPlayer, _MLModel1Path);
                Assert.Fail();
            }
            catch (Exception ex)
            {
            }

            try
            {
                //Test Invalid GridSize
                request.GridSize = 5;
                var response = T3BL.TicTacToe.ProcessRequest(request, computerPlayer, _MLModel1Path);
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

                var response = T3BL.TicTacToe.ProcessRequest(request, computerPlayer, _MLModel1Path);
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
            var gameoutcomes = TestValidGamesComVsCom("V1-P1", "V1-P2", player1, player2);
        }

        [Test]
        public void TestValid2PGamesV1vsV2()
        {
            var player1 = new T3BL.ComputerPlayerV1(2, 1);
            var player2 = new T3BL.ComputerPlayerV2(1, 2);
            var gameoutcomes = TestValidGamesComVsCom("V1-P1", "V2-P2", player1, player2);
        }

        [Test]
        public void TestValid2PGamesV2vsV1()
        {
            var player1 = new T3BL.ComputerPlayerV2(2, 1);
            var player2 = new T3BL.ComputerPlayerV1(1, 2);
            var gameoutcomes = TestValidGamesComVsCom("V2-P1", "V1-P2", player1, player2);
        }

        [Test]
        public void TestValid2PGamesV2vsV2()
        {
            var player1 = new T3BL.ComputerPlayerV2(2, 1);
            var player2 = new T3BL.ComputerPlayerV2(1, 2);
            var gameoutcomes = TestValidGamesComVsCom("V2-P1", "V2-P2", player1, player2);
        }

        [Test]
        public void TestValid2PGamesV2vsV3()
        {
            var player1 = new T3BL.ComputerPlayerV2(2, 1);
            var player2 = new T3BL.ComputerPlayerV3(1, 2, _MLModel1Path);
            var gameoutcomes = TestValidGamesComVsCom("V2-P1", "V3-P2", player1, player2);
        }

        [Test]
        public void TestValid2PGamesV3vsV2()
        {
            var player1 = new T3BL.ComputerPlayerV3(2, 1, _MLModel1Path);
            var player2 = new T3BL.ComputerPlayerV2(1, 2);
            var gameoutcomes = TestValidGamesComVsCom("V3-P1", "V2-P2", player1, player2);
        }

        [Test]
        public void TestValid2PGamesV3vsV3()
        {
            var player1 = new T3BL.ComputerPlayerV3(2, 1, _MLModel1Path);
            var player2 = new T3BL.ComputerPlayerV3(1, 2, _MLModel1Path);
            var gameoutcomes = TestValidGamesComVsCom("V3-P1", "V3-P2", player1, player2);
        }

        public Dictionary<TicTacToe.Models.TicTacToeGameStatus, int> TestValidGamesComVsCom(string P1Ver, string P2Ver, T3BL.ComputerPlayerBase computerPlayer1, T3BL.ComputerPlayerBase computerPlayer2)
        {
            int gamesCount = 100;
            var gameOutcomes = new Dictionary<TicTacToe.Models.TicTacToeGameStatus, int>();
            for (int g = 0; g < gamesCount; g++)
            {
                var gameStatus = T3Mod.TicTacToeGameStatus.InProgress;
                string InstanceId = $"UnitTest {P1Ver} vs {P2Ver} {DateTime.UtcNow.Ticks} @ {DateTime.UtcNow.ToString("o")}";
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

                // Player 1 makes first move
                int p1FirstMove = computerPlayer1.GetMove(InstanceId);
                if (p1FirstMove < 0)
                    p1FirstMove = computerPlayer1.GetRandomMove(request.CellStates.ToList());
                if (request.CellStates[p1FirstMove] == 0)
                    request.CellStates[p1FirstMove] = computerPlayer1.PlayerSymbolSelf;

                T3Mod.TicTacToeUpdateResponse response;
                while (gameStatus == T3Mod.TicTacToeGameStatus.InProgress)
                {
                    var response2 =
                        T3BL.TicTacToe.ProcessRequest(request, computerPlayer2, _MLModel1Path);

                    gameStatus = response2.Status;
                    response = response2;

                    if (response2.ComputerMove.HasValue)
                        request.CellStates[response2.ComputerMove.Value] = computerPlayer2.PlayerSymbolSelf;

                    if (gameStatus != T3Mod.TicTacToeGameStatus.InProgress)
                        break;

                    int p1Move = computerPlayer1.GetMove(InstanceId);
                    if (request.CellStates[p1Move] == 0)
                        request.CellStates[p1Move] = computerPlayer1.PlayerSymbolSelf;

                    var response1 = T3BL.TicTacToe.EvaluateResult(request, computerPlayer2);

                    gameStatus = response1.Status;
                    response = response1;
                }

                if (gameOutcomes.ContainsKey(gameStatus))
                    gameOutcomes[gameStatus] += 1;
                else
                    gameOutcomes.Add(gameStatus, 1);
            }

            return gameOutcomes;
        }
    }
}