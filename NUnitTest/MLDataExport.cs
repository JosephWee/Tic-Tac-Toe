using NUnit.Framework;
using System;
using static UnitTests.TicTacToeTests;
using System.Linq;
using T3BL = TicTacToe.BusinessLogic;
using T3Ent = TicTacToe.Entity;
using T3ML = TicTacToe.ML;
using T3Mod = TicTacToe.Models;
using Microsoft.EntityFrameworkCore;
using static TicTacToe.BusinessLogic.ComputerPlayerBase;

namespace UnitTests
{
    public class MLDataExport
    {
        //private static Random random = new Random();
        private static FileInfo _MLModel1FileInfo = null;
        private static DirectoryInfo _CsvOutputDirInfo = null;

        [SetUp]
        public void TestSetup()
        {
            bool RunTest =
                TestContext.Parameters.Get("PrepAndExportData", false);

            Assert.IsTrue(RunTest);

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

            var MLModel1Path =
                TestContext
                .Parameters
                .Get("MLModel1Path", string.Empty)
                .Replace("$(SolutionDir)", solutionDir)
                .Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);

            _MLModel1FileInfo = new FileInfo( MLModel1Path );
            Assert.IsTrue(_MLModel1FileInfo.Exists);

            var CsvOutputPath =
                TestContext
                .Parameters
                .Get("CsvOutputPath", string.Empty)
                .Replace("$(SolutionDir)", solutionDir)
                .Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);

            _CsvOutputDirInfo = new DirectoryInfo(CsvOutputPath);
            Assert.IsTrue(_CsvOutputDirInfo.Exists);
        }

        [Test]
        public void PrepAndExportData()
        {
            T3Ent.TicTacToeDataContext dbContext = new T3Ent.TicTacToeDataContext();

            var games =
                dbContext
                .TicTacToeGames
                .Where(x => x.Status == T3Mod.TicTacToeGameStatus.Player1Wins)
                .ToList();

            try
            {
                FileInfo outputFile =
                    new FileInfo(
                        Path.Combine(
                            _CsvOutputDirInfo.FullName,
                            $"ClassificationModel01Data-{DateTime.UtcNow.ToString("yyyy-MM-dd-HHmm")}.csv"
                        )
                    );
                using (TextWriter textWriter = File.CreateText(outputFile.FullName))
                {
                    textWriter.WriteLine("\"MoveNumber\",\"Cell0\",\"Cell1\",\"Cell2\",\"Cell3\",\"Cell4\",\"Cell5\",\"Cell6\",\"Cell7\",\"Cell8\",\"GameResultCode\"");
                    for (int g = 0; g < games.Count; g++)
                    {
                        bool gameHasErrors = false;

                        var game = games[g];

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
                            new T3BL.ComputerPlayerBase(2, 1);

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
                        {
                            if (WinningCells.Count != game.GridSize)
                                continue;
                        }

                        if (game.Status != evaluatedStatus)
                            continue;

                        var grpMoves = moves.GroupBy(x => x.MoveNumber);

                        foreach (var grpMove in grpMoves)
                        {
                            List<string> cells = new List<string>();

                            cells.Add($"{grpMove.Key}");

                            int m = 0;
                            foreach (var cell in grpMove)
                            {
                                if (cell.CellIndex != m)
                                {
                                    gameHasErrors = true;
                                    break;
                                }

                                cells.Add($"{cell.CellContent}");
                                m++;
                            }

                            if (gameHasErrors)
                                break;

                            cells.Add($"{(int)game.Status}");

                            textWriter.WriteLine(string.Join(",", cells));
                        }

                        if (gameHasErrors)
                            continue;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
