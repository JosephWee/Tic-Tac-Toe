using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.ML;
using System.Configuration;
using T3BL = TicTacToe.BusinessLogic;
using T3Ent = TicTacToe.Entity;
using T3ML = TicTacToe.ML;
using T3Mod = TicTacToe.Models;

namespace WebApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TicTacToeController : ControllerBase
    {
        private readonly IConfiguration _config;
        private string _OutcomePredictionModelPath = string.Empty;
        private string _ComputerPlayerV3ModelPath = string.Empty;

        public TicTacToeController(IConfiguration config)
        {
            _config = config;

            string solutionDir = new DirectoryInfo(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..")).FullName;
            string msbuildDir = Path.Combine(solutionDir, "WebApi");

            string connectionString = config.GetConnectionString("TicTacToeDataConnString") ?? string.Empty;
            string TicTacToeDataConnString =
                connectionString
                .Replace("$(SolutionDir)", solutionDir)
                .Replace("$(MSBuildProjectDirectory)", msbuildDir);

            T3Ent.DbContextConfig
                .AddOrReplace(
                    "TicTacToeData",
                    TicTacToeDataConnString);

            var ComputerPlayerV3ModelPath = config.GetValue<string>("ComputerPlayerV3ModelPath") ?? string.Empty;
            _ComputerPlayerV3ModelPath =
                ComputerPlayerV3ModelPath
                .Replace("$(SolutionDir)", solutionDir)
                .Replace("$(MSBuildProjectDirectory)", msbuildDir);

            if (!System.IO.File.Exists(_ComputerPlayerV3ModelPath))
                throw new FileNotFoundException($"ComputerPlayerV3ModelPath file not found.\r\nFile not found:\r\n{_ComputerPlayerV3ModelPath}");

            var OutcomePredictionModelPath = config.GetValue<string>("OutcomePredictionModelPath") ?? string.Empty;
            _OutcomePredictionModelPath =
                OutcomePredictionModelPath
                .Replace("$(SolutionDir)", solutionDir)
                .Replace("$(MSBuildProjectDirectory)", msbuildDir);

            if (!System.IO.File.Exists(_OutcomePredictionModelPath))
                throw new FileNotFoundException($"OutcomePredictionModelPath file not found.\r\nFile not found:\r\n{_OutcomePredictionModelPath}");
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult> Get(int pageSize, int pageNum)
        {
            try
            {
                T3Mod.TicTacToeResultSet resultSet = new T3Mod.TicTacToeResultSet();

                int index = pageNum - 1;
                if (index < 0)
                    return BadRequest("Page number should be greater than 0.");

                resultSet.PageNumber = pageNum;

                if (pageSize < 1 || pageSize > 100)
                    return BadRequest("Page size should be between 1 to 100.");

                resultSet.PageSize = pageSize;

                T3Ent.TicTacToeDataContext dbContext = new T3Ent.TicTacToeDataContext();

                int gamesCount =
                    dbContext
                    .TicTacToeGames
                    .Count(
                        x =>
                        (
                            x.Status == T3Mod.TicTacToeGameStatus.InProgress
                            || x.Status == T3Mod.TicTacToeGameStatus.Player1Wins
                            || x.Status == T3Mod.TicTacToeGameStatus.Player2Wins
                            || x.Status == T3Mod.TicTacToeGameStatus.Draw
                        )
                    );

                resultSet.PageCount = gamesCount / pageSize;

                if (resultSet.PageNumber > resultSet.PageCount)
                    return BadRequest($"Page number should be less than {resultSet.PageCount}.");

                var games =
                    dbContext
                    .TicTacToeGames
                    .Where(
                        x =>
                        (
                            x.Status == T3Mod.TicTacToeGameStatus.InProgress
                            || x.Status == T3Mod.TicTacToeGameStatus.Player1Wins
                            || x.Status == T3Mod.TicTacToeGameStatus.Player2Wins
                            || x.Status == T3Mod.TicTacToeGameStatus.Draw
                        )
                    ).Skip(pageSize * index)
                    .Take(pageSize)
                    .ToList();

                foreach (var game in games)
                {
                    var moves =
                        dbContext
                        .TicTacToeData
                        .Where(x => x.InstanceId == game.InstanceId)
                        .OrderBy(x => x.MoveNumber)
                        .ThenBy(x => x.CellIndex)
                        .ToList();

                    var firstMoveNumber =
                        moves.Min(x => x.MoveNumber);

                    var latestMoveNumber =
                        moves.Max(x => x.MoveNumber);

                    var firstMove =
                        moves
                        .Where(x => x.MoveNumber == firstMoveNumber)
                        .OrderBy(x => x.CellIndex)
                        .ToList();

                    var latestMove =
                        moves
                        .Where(x => x.MoveNumber == latestMoveNumber)
                        .OrderBy(x => x.CellIndex)
                        .ToList();

                    int ExpectedTotalCells = game.GridSize * game.GridSize;
                    if (firstMove == null
                        || firstMove.Count != ExpectedTotalCells
                        || (ExpectedTotalCells - firstMove.Count(x => x.CellContent == 0)) != 1)
                        continue;

                    int symbolPlayerOpponent = firstMove.First(x => x.CellContent != 0).CellContent;
                    if (symbolPlayerOpponent != 1 && symbolPlayerOpponent != 2)
                        continue;
                    int symbolPlayerSelf = symbolPlayerOpponent == 1 ? 2 : 1;

                    var computerPlayer =
                        new T3BL.ComputerPlayerBase(
                            symbolPlayerOpponent,
                            symbolPlayerSelf
                        );

                    int BlankCellCount = int.MinValue;
                    List<int> WinningCells = null;
                    var Status =
                        T3BL
                        .TicTacToe
                        .EvaluateResult(
                            computerPlayer,
                            game.GridSize,
                            latestMove
                            .OrderBy(x => x.CellIndex)
                            .Select(x => x.CellContent)
                            .ToList(),
                            out BlankCellCount,
                            out WinningCells);

                    var result =
                        new T3Mod.TicTacToeResult()
                        {
                            InstanceId = game.InstanceId,
                            Description = game.Description,
                            GridSize = game.GridSize,
                            CellStates =
                            latestMove
                            .OrderBy(x => x.CellIndex)
                            .Select(x => x.CellContent)
                            .ToList(),
                            Status = Status,
                            WinningCells = WinningCells
                        };

                    resultSet.Results.Add(result);
                }

                return Ok(resultSet);
            }
            catch (Exception ex)
            {
                return Problem(detail: "Internal Error", statusCode: 500);
            }
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TicTacToe.Models.TicTacToeUpdateRequest value)
        {
            try
            {
                if (value == null)
                    return BadRequest();

                //var computerPlayer = new T3BL.ComputerPlayerV2();
                var computerPlayer = new T3BL.ComputerPlayerV3(_ComputerPlayerV3ModelPath);

                string Description = $"Web Api - {computerPlayer.GetType().Name}";
                var retVal = T3BL.TicTacToe.ProcessRequest(value, computerPlayer, _OutcomePredictionModelPath, Description);

                return Ok(retVal);
            }
            catch (ArgumentException argEx)
            {
                return BadRequest();
            }
            catch (Exception ex)
            {
                return Problem(detail: "Internal Error", statusCode: 500);
            }
        }

        //// PUT api/values/5
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //public void Delete(int id)
        //{
        //}
    }
}
