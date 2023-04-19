using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Azure;

namespace UnitTests
{
    public class TicTacToeTests
    {
        private static Random random = new Random();

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

            TicTacToe
                .BusinessLogic
                .ComputerPlayerConfig
                .RegisterComputerPlayer(
                    new TicTacToe.BusinessLogic.ComputerPlayerV2()
                );
        }

        [Test]
        public void TestValidGames()
        {
            int gamesCount = 100;

            for (int g = 0; g < gamesCount; g++)
            {
                var gameStatus = TicTacToe.Models.TicTacToeGameStatus.InProgress;
                string InstanceId = $"UnitTest {g} @ {DateTime.UtcNow.ToString("o")}";
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

                    var response = TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);
                    
                    gameStatus = response.Status;

                    if (response.ComputerMove.HasValue)
                        request.CellStates[response.ComputerMove.Value] = 2;
                }
            }
        }

        [Test]
        public void TestInvalidGames()
        {
            string InstanceId = $"UnitTest @ {DateTime.UtcNow.ToString("o")}";
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

                var response = TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);

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

                var response = TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);
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

                var response = TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);
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

                var response = TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);
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

                var response = TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);
                Assert.Fail();
            }
            catch (Exception ex)
            {
            }

            try
            {
                //Test Invalid GridSize
                request.GridSize = 5;
                var response = TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);
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

                var response = TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);
                Assert.Fail();
            }
            catch (Exception ex)
            {
            }
        }
    }
}