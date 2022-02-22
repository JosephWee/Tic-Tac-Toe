using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class TicTacToeTests
    {
        [TestMethod]
        public void TestPlayer1Wins()
        {
            string InstanceId = $"UnitTest @ {DateTime.UtcNow.ToString("o")}";
            TicTacToe.Models.TicTacToeUpdateRequest request;
            TicTacToe.Models.TicTacToeUpdateResponse response;
            TicTacToe.Models.TicTacToeGameStatus expectedGameStatus = TicTacToe.Models.TicTacToeGameStatus.Player1Wins;

            request = new TicTacToe.Models.TicTacToeUpdateRequest();
            request.InstanceId = InstanceId;
            request.GridSize = 3;
            request.CellStates = new System.Collections.Generic.List<int>()
            {
                1, 1, 1,
                0, 0, 0,
                0, 0, 0
            };
            response = null;

            response =
                TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Status == expectedGameStatus);
            Assert.IsTrue(response.WinningCells.Count == 3);
            Assert.IsTrue(response.WinningCells[0] == 0);
            Assert.IsTrue(response.WinningCells[1] == 1);
            Assert.IsTrue(response.WinningCells[2] == 2);



            request = new TicTacToe.Models.TicTacToeUpdateRequest();
            request.InstanceId = InstanceId;
            request.GridSize = 3;
            request.CellStates = new System.Collections.Generic.List<int>()
            {
                0, 0, 0,
                1, 1, 1,
                0, 0, 0
            };
            response = null;

            response =
                TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Status == expectedGameStatus);
            Assert.IsTrue(response.WinningCells.Count == 3);
            Assert.IsTrue(response.WinningCells[0] == 3);
            Assert.IsTrue(response.WinningCells[1] == 4);
            Assert.IsTrue(response.WinningCells[2] == 5);



            request = new TicTacToe.Models.TicTacToeUpdateRequest();
            request.InstanceId = InstanceId;
            request.GridSize = 3;
            request.CellStates = new System.Collections.Generic.List<int>()
            {
                0, 0, 0,
                0, 0, 0,
                1, 1, 1
            };
            response = null;

            response =
                TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Status == expectedGameStatus);
            Assert.IsTrue(response.WinningCells.Count == 3);
            Assert.IsTrue(response.WinningCells[0] == 6);
            Assert.IsTrue(response.WinningCells[1] == 7);
            Assert.IsTrue(response.WinningCells[2] == 8);



            request = new TicTacToe.Models.TicTacToeUpdateRequest();
            request.InstanceId = InstanceId;
            request.GridSize = 3;
            request.CellStates = new System.Collections.Generic.List<int>()
            {
                1, 0, 0,
                1, 0, 0,
                1, 0, 0
            };
            response = null;

            response =
                TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Status == expectedGameStatus);
            Assert.IsTrue(response.WinningCells.Count == 3);
            Assert.IsTrue(response.WinningCells[0] == 0);
            Assert.IsTrue(response.WinningCells[1] == 3);
            Assert.IsTrue(response.WinningCells[2] == 6);



            request = new TicTacToe.Models.TicTacToeUpdateRequest();
            request.InstanceId = InstanceId;
            request.GridSize = 3;
            request.CellStates = new System.Collections.Generic.List<int>()
            {
                0, 1, 0,
                0, 1, 0,
                0, 1, 0
            };
            response = null;

            response =
                TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Status == expectedGameStatus);
            Assert.IsTrue(response.WinningCells.Count == 3);
            Assert.IsTrue(response.WinningCells[0] == 1);
            Assert.IsTrue(response.WinningCells[1] == 4);
            Assert.IsTrue(response.WinningCells[2] == 7);



            request = new TicTacToe.Models.TicTacToeUpdateRequest();
            request.InstanceId = InstanceId;
            request.GridSize = 3;
            request.CellStates = new System.Collections.Generic.List<int>()
            {
                0, 0, 1,
                0, 0, 1,
                0, 0, 1
            };
            response = null;

            response =
                TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Status == expectedGameStatus);
            Assert.IsTrue(response.WinningCells.Count == 3);
            Assert.IsTrue(response.WinningCells[0] == 2);
            Assert.IsTrue(response.WinningCells[1] == 5);
            Assert.IsTrue(response.WinningCells[2] == 8);


            request = new TicTacToe.Models.TicTacToeUpdateRequest();
            request.InstanceId = InstanceId;
            request.GridSize = 3;
            request.CellStates = new System.Collections.Generic.List<int>()
            {
                1, 0, 0,
                0, 1, 0,
                0, 0, 1
            };
            response = null;

            response =
                TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Status == expectedGameStatus);
            Assert.IsTrue(response.WinningCells.Count == 3);
            Assert.IsTrue(response.WinningCells[0] == 0);
            Assert.IsTrue(response.WinningCells[1] == 4);
            Assert.IsTrue(response.WinningCells[2] == 8);



            request = new TicTacToe.Models.TicTacToeUpdateRequest();
            request.InstanceId = InstanceId;
            request.GridSize = 3;
            request.CellStates = new System.Collections.Generic.List<int>()
            {
                0, 0, 1,
                0, 1, 0,
                1, 0, 0
            };
            response = null;

            response =
                TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Status == expectedGameStatus);
            Assert.IsTrue(response.WinningCells.Count == 3);
            Assert.IsTrue(response.WinningCells[0] == 6);
            Assert.IsTrue(response.WinningCells[1] == 4);
            Assert.IsTrue(response.WinningCells[2] == 2);
        }

        [TestMethod]
        public void TestPlayer2Wins()
        {
            string InstanceId = $"UnitTest @ {DateTime.UtcNow.ToString("o")}";
            TicTacToe.Models.TicTacToeUpdateRequest request;
            TicTacToe.Models.TicTacToeUpdateResponse response;
            TicTacToe.Models.TicTacToeGameStatus expectedGameStatus = TicTacToe.Models.TicTacToeGameStatus.Player2Wins;

            request = new TicTacToe.Models.TicTacToeUpdateRequest();
            request.InstanceId = InstanceId;
            request.GridSize = 3;
            request.CellStates = new System.Collections.Generic.List<int>()
            {
                2, 2, 2,
                0, 0, 0,
                0, 0, 0
            };
            response = null;

            response =
                TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Status == expectedGameStatus);
            Assert.IsTrue(response.WinningCells.Count == 3);
            Assert.IsTrue(response.WinningCells[0] == 0);
            Assert.IsTrue(response.WinningCells[1] == 1);
            Assert.IsTrue(response.WinningCells[2] == 2);



            request = new TicTacToe.Models.TicTacToeUpdateRequest();
            request.InstanceId = InstanceId;
            request.GridSize = 3;
            request.CellStates = new System.Collections.Generic.List<int>()
            {
                0, 0, 0,
                2, 2, 2,
                0, 0, 0
            };
            response = null;

            response =
                TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Status == expectedGameStatus);
            Assert.IsTrue(response.WinningCells.Count == 3);
            Assert.IsTrue(response.WinningCells[0] == 3);
            Assert.IsTrue(response.WinningCells[1] == 4);
            Assert.IsTrue(response.WinningCells[2] == 5);



            request = new TicTacToe.Models.TicTacToeUpdateRequest();
            request.InstanceId = InstanceId;
            request.GridSize = 3;
            request.CellStates = new System.Collections.Generic.List<int>()
            {
                0, 0, 0,
                0, 0, 0,
                2, 2, 2
            };
            response = null;

            response =
                TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Status == expectedGameStatus);
            Assert.IsTrue(response.WinningCells.Count == 3);
            Assert.IsTrue(response.WinningCells[0] == 6);
            Assert.IsTrue(response.WinningCells[1] == 7);
            Assert.IsTrue(response.WinningCells[2] == 8);



            request = new TicTacToe.Models.TicTacToeUpdateRequest();
            request.InstanceId = InstanceId;
            request.GridSize = 3;
            request.CellStates = new System.Collections.Generic.List<int>()
            {
                2, 0, 0,
                2, 0, 0,
                2, 0, 0
            };
            response = null;

            response =
                TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Status == expectedGameStatus);
            Assert.IsTrue(response.WinningCells.Count == 3);
            Assert.IsTrue(response.WinningCells[0] == 0);
            Assert.IsTrue(response.WinningCells[1] == 3);
            Assert.IsTrue(response.WinningCells[2] == 6);



            request = new TicTacToe.Models.TicTacToeUpdateRequest();
            request.InstanceId = InstanceId;
            request.GridSize = 3;
            request.CellStates = new System.Collections.Generic.List<int>()
            {
                0, 2, 0,
                0, 2, 0,
                0, 2, 0
            };
            response = null;

            response =
                TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Status == expectedGameStatus);
            Assert.IsTrue(response.WinningCells.Count == 3);
            Assert.IsTrue(response.WinningCells[0] == 1);
            Assert.IsTrue(response.WinningCells[1] == 4);
            Assert.IsTrue(response.WinningCells[2] == 7);



            request = new TicTacToe.Models.TicTacToeUpdateRequest();
            request.InstanceId = InstanceId;
            request.GridSize = 3;
            request.CellStates = new System.Collections.Generic.List<int>()
            {
                0, 0, 2,
                0, 0, 2,
                0, 0, 2
            };
            response = null;

            response =
                TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Status == expectedGameStatus);
            Assert.IsTrue(response.WinningCells.Count == 3);
            Assert.IsTrue(response.WinningCells[0] == 2);
            Assert.IsTrue(response.WinningCells[1] == 5);
            Assert.IsTrue(response.WinningCells[2] == 8);



            request = new TicTacToe.Models.TicTacToeUpdateRequest();
            request.InstanceId = InstanceId;
            request.GridSize = 3;
            request.CellStates = new System.Collections.Generic.List<int>()
            {
                2, 0, 0,
                0, 2, 0,
                0, 0, 2
            };
            response = null;

            response =
                TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Status == expectedGameStatus);
            Assert.IsTrue(response.WinningCells.Count == 3);
            Assert.IsTrue(response.WinningCells[0] == 0);
            Assert.IsTrue(response.WinningCells[1] == 4);
            Assert.IsTrue(response.WinningCells[2] == 8);



            request = new TicTacToe.Models.TicTacToeUpdateRequest();
            request.InstanceId = InstanceId;
            request.GridSize = 3;
            request.CellStates = new System.Collections.Generic.List<int>()
            {
                0, 0, 2,
                0, 2, 0,
                2, 0, 0
            };
            response = null;

            response =
                TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Status == expectedGameStatus);
            Assert.IsTrue(response.WinningCells.Count == 3);
            Assert.IsTrue(response.WinningCells[0] == 6);
            Assert.IsTrue(response.WinningCells[1] == 4);
            Assert.IsTrue(response.WinningCells[2] == 2);
        }

        [TestMethod]
        public void TestDraw()
        {
            string InstanceId = $"UnitTest @ {DateTime.UtcNow.ToString("o")}";
            TicTacToe.Models.TicTacToeUpdateRequest request;
            TicTacToe.Models.TicTacToeUpdateResponse response;
            TicTacToe.Models.TicTacToeGameStatus expectedGameStatus = TicTacToe.Models.TicTacToeGameStatus.Draw;

            request = new TicTacToe.Models.TicTacToeUpdateRequest();
            request.InstanceId = InstanceId;
            request.GridSize = 3;
            request.CellStates = new System.Collections.Generic.List<int>()
            {
                1, 1, 2,
                2, 2, 1,
                1, 1, 2
            };
            response = null;

            response =
                TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Status == expectedGameStatus);



            request = new TicTacToe.Models.TicTacToeUpdateRequest();
            request.InstanceId = InstanceId;
            request.GridSize = 3;
            request.CellStates = new System.Collections.Generic.List<int>()
            {
                1, 2, 1,
                1, 2, 2,
                2, 1, 1
            };
            response = null;

            response =
                TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Status == expectedGameStatus);



            request = new TicTacToe.Models.TicTacToeUpdateRequest();
            request.InstanceId = InstanceId;
            request.GridSize = 3;
            request.CellStates = new System.Collections.Generic.List<int>()
            {
                2, 1, 1,
                1, 2, 2,
                2, 1, 1
            };
            response = null;

            response =
                TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Status == expectedGameStatus);
        }

        [TestMethod]
        public void TestInProgress()
        {
            string InstanceId = $"UnitTest @ {DateTime.UtcNow.ToString("o")}";
            TicTacToe.Models.TicTacToeUpdateRequest request;
            TicTacToe.Models.TicTacToeUpdateResponse response;
            TicTacToe.Models.TicTacToeGameStatus expectedGameStatus = TicTacToe.Models.TicTacToeGameStatus.InProgress;

            request = new TicTacToe.Models.TicTacToeUpdateRequest();
            request.InstanceId = InstanceId;
            request.GridSize = 3;
            request.CellStates = new System.Collections.Generic.List<int>()
            {
                1, 1, 2,
                2, 2, 1,
                1, 0, 0
            };
            response = null;

            response =
                TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Status == expectedGameStatus);



            request = new TicTacToe.Models.TicTacToeUpdateRequest();
            request.InstanceId = InstanceId;
            request.GridSize = 3;
            request.CellStates = new System.Collections.Generic.List<int>()
            {
                1, 2, 1,
                1, 2, 2,
                0, 0, 1
            };
            response = null;

            response =
                TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Status == expectedGameStatus);



            request = new TicTacToe.Models.TicTacToeUpdateRequest();
            request.InstanceId = InstanceId;
            request.GridSize = 3;
            request.CellStates = new System.Collections.Generic.List<int>()
            {
                2, 0, 1,
                1, 0, 2,
                2, 1, 1
            };
            response = null;

            response =
                TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Status == expectedGameStatus);
        }

        [TestMethod]
        public void TestAnomalousData()
        {
            string InstanceId = $"UnitTest @ {DateTime.UtcNow.ToString("o")}";
            TicTacToe.Models.TicTacToeUpdateRequest request;
            TicTacToe.Models.TicTacToeUpdateResponse response;
            TicTacToe.Models.TicTacToeGameStatus expectedGameStatus = TicTacToe.Models.TicTacToeGameStatus.InProgress;

            request = new TicTacToe.Models.TicTacToeUpdateRequest();
            request.InstanceId = InstanceId;
            request.GridSize = 3;
            request.CellStates = new System.Collections.Generic.List<int>()
            {
                0, 0, 0,
                0, 3, 0,
                0, 0, 0
            };
            response = null;

            bool exceptionFound = false;
            try
            {
                response =
                    TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);
            }
            catch (ArgumentException ex)
            {
                exceptionFound = true;
            }

            Assert.IsTrue(exceptionFound);
        }

        [TestMethod]
        public void TestDataRetrievalAndValidation()
        {
            string InstanceId = $"UnitTest @ {DateTime.UtcNow.ToString("o")}";
            TicTacToe.Models.TicTacToeUpdateRequest request;
            TicTacToe.Models.TicTacToeUpdateResponse response;
            int MoveNumber = 0;

            request = new TicTacToe.Models.TicTacToeUpdateRequest();
            request.InstanceId = InstanceId;
            request.GridSize = 3;
            request.CellStates = new System.Collections.Generic.List<int>()
            {
                1, 0, 0,
                0, 0, 0,
                0, 0, 0
            };
            response = null;

            response =
                TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Status == TicTacToe.Models.TicTacToeGameStatus.InProgress);
            MoveNumber++;



            request = new TicTacToe.Models.TicTacToeUpdateRequest();
            request.InstanceId = InstanceId;
            request.GridSize = 3;
            request.CellStates = new System.Collections.Generic.List<int>()
            {
                1, 0, 0,
                0, 2, 0,
                0, 0, 0
            };
            response = null;

            response =
                TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Status == TicTacToe.Models.TicTacToeGameStatus.InProgress);
            MoveNumber++;



            request = new TicTacToe.Models.TicTacToeUpdateRequest();
            request.InstanceId = InstanceId;
            request.GridSize = 3;
            request.CellStates = new System.Collections.Generic.List<int>()
            {
                1, 0, 0,
                0, 2, 0,
                0, 0, 1
            };
            response = null;

            response =
                TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Status == TicTacToe.Models.TicTacToeGameStatus.InProgress);
            MoveNumber++;



            request = new TicTacToe.Models.TicTacToeUpdateRequest();
            request.InstanceId = InstanceId;
            request.GridSize = 3;
            request.CellStates = new System.Collections.Generic.List<int>()
            {
                1, 0, 0,
                0, 2, 0,
                2, 0, 1
            };
            response = null;

            response =
                TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Status == TicTacToe.Models.TicTacToeGameStatus.InProgress);
            MoveNumber++;



            request = new TicTacToe.Models.TicTacToeUpdateRequest();
            request.InstanceId = InstanceId;
            request.GridSize = 3;
            request.CellStates = new System.Collections.Generic.List<int>()
            {
                1, 0, 1,
                0, 2, 0,
                2, 0, 1
            };
            response = null;

            response =
                TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Status == TicTacToe.Models.TicTacToeGameStatus.InProgress);
            MoveNumber++;



            request = new TicTacToe.Models.TicTacToeUpdateRequest();
            request.InstanceId = InstanceId;
            request.GridSize = 3;
            request.CellStates = new System.Collections.Generic.List<int>()
            {
                1, 2, 1,
                0, 2, 0,
                2, 0, 1
            };
            response = null;

            response =
                TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Status == TicTacToe.Models.TicTacToeGameStatus.InProgress);
            MoveNumber++;



            request = new TicTacToe.Models.TicTacToeUpdateRequest();
            request.InstanceId = InstanceId;
            request.GridSize = 3;
            request.CellStates = new System.Collections.Generic.List<int>()
            {
                1, 2, 1,
                0, 2, 1,
                2, 0, 1
            };
            response = null;

            response =
                TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Status == TicTacToe.Models.TicTacToeGameStatus.Player1Wins);
            MoveNumber++;


            var ds =
                TicTacToe.BusinessLogic.TicTacToe.GetAndValidate(InstanceId, MoveNumber);

            int FetchedMoveNumber = ds.Max(x => x.MoveNumber);
            Assert.AreEqual(MoveNumber, FetchedMoveNumber);
        }
    }
}