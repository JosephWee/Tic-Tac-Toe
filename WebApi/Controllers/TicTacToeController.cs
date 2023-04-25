using Azure.Core;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.ML;
using System.Configuration;
using T3BL = TicTacToe.BusinessLogic;
using T3Ent = TicTacToe.Entity;
using T3ML = TicTacToe.ML;
using T3Mod = TicTacToe.Models;
using System.Reflection.Metadata.Ecma335;

namespace WebApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TicTacToeController : ControllerBase
    {
        private readonly IConfiguration _config;
        private string _MLNetModelPath = string.Empty;

        public TicTacToeController(IConfiguration config)
        {
            _config = config;

            string msbuildDir = new DirectoryInfo(Path.Combine(AppContext.BaseDirectory, @"..\..\..")).FullName;

            string connectionString = config.GetConnectionString("TicTacToeDataConnString") ?? string.Empty;
            string TicTacToeDataConnString = connectionString.Replace("$(MSBuildProjectDirectory)", msbuildDir);

            T3Ent.DbContextConfig
                .AddOrReplace(
                    "TicTacToeData",
                    TicTacToeDataConnString);

            _MLNetModelPath = Path.Combine(msbuildDir, "MLModels", "MLModel1.zip");
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "You have reached the TicTacToe Controller", DateTime.UtcNow.ToString("o") };
        }

        //// GET api/values/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] TicTacToe.Models.TicTacToeUpdateRequest value)
        {
            try
            {
                if (value == null)
                    return BadRequest();

                T3Mod.TicTacToeUpdateResponse retVal = null;

                //var computerPlayer = new T3BL.ComputerPlayerV2();
                var computerPlayer = new T3BL.ComputerPlayerV3(_MLNetModelPath);

                var latestMove = new List<T3Ent.TicTacToeDataEntry>();
                T3BL.TicTacToe.ValidateTicTacToeUpdateRequest(value, computerPlayer, out latestMove);

                int moveNumber = latestMove.Any() ? latestMove.Max(x => x.MoveNumber) : 0;
                int cellsChanged = 0;
                latestMove.ForEach(
                    x =>
                    {
                        if (x.CellContent != value.CellStates[x.CellIndex])
                            cellsChanged++;
                    });

                if (!latestMove.Any() || cellsChanged == 1)
                {
                    // Save: New Game or New Move
                    moveNumber++;
                    T3BL.TicTacToe.SaveToDatabase(value.InstanceId, value.GridSize, moveNumber, value.CellStates);
                }

                // Evaluate Current Game Outcome
                var response1 =
                    T3BL.TicTacToe.EvaluateResult(value, computerPlayer);

                retVal = response1;

                if (value.NumberOfPlayers == 1
                    && (!latestMove.Any() || cellsChanged == 1)
                    && response1.Status == T3Mod.TicTacToeGameStatus.InProgress)
                {
                    // Computer Player Moves
                    int? ComputerMove =
                        computerPlayer.GetMove(value.InstanceId);

                    if (ComputerMove.HasValue && value.CellStates[ComputerMove.Value] == 0)
                    {
                        var CellStates = value.CellStates.ToList();
                        CellStates[ComputerMove.Value] = computerPlayer.PlayerSymbolSelf;

                        //Save Computer Player's move
                        moveNumber++;
                        T3BL.TicTacToe.SaveToDatabase(value.InstanceId, value.GridSize, moveNumber, CellStates);

                        int BlankCellCount2 = int.MinValue;
                        List<int> WinningCells2 = new List<int>();

                        var response2 =
                            new T3Mod.TicTacToeUpdateResponse()
                            {
                                Status = T3BL.TicTacToe.EvaluateResult(computerPlayer, value.GridSize, CellStates, out BlankCellCount2, out WinningCells2),
                                ComputerMove = ComputerMove,
                                WinningCells = WinningCells2
                            };

                        retVal = response2;
                    }
                }

                var cellStates = value.CellStates.ToList();

                if (!cellStates.Any(x => !T3BL.TicTacToe.ValidCellStateValues.Contains(x)))
                {
                    if (retVal.ComputerMove.HasValue)
                    {
                        cellStates[retVal.ComputerMove.Value] = computerPlayer.PlayerSymbolSelf;
                    }

                    var mlContext = new MLContext();
                    ITransformer mlModel = mlContext.Model.Load(_MLNetModelPath, out var _);
                    var predEngine = mlContext.Model.CreatePredictionEngine<T3ML.MLModel1.ModelInput, T3ML.MLModel1.ModelOutput>(mlModel);

                    var inputModel1 =
                        new T3ML.MLModel1.ModelInput()
                        {
                            MoveNumber = moveNumber,
                            Cell0 = value.CellStates[0],
                            Cell1 = value.CellStates[1],
                            Cell2 = value.CellStates[2],
                            Cell3 = value.CellStates[3],
                            Cell4 = value.CellStates[4],
                            Cell5 = value.CellStates[5],
                            Cell6 = value.CellStates[6],
                            Cell7 = value.CellStates[7],
                            Cell8 = value.CellStates[8],
                            GameResultCode = 0
                        };

                    //var inputModel2 =
                    //    new MLModel2.ModelInput()
                    //    {
                    //        Cell0 = value.CellStates[0],
                    //        Cell1 = value.CellStates[1],
                    //        Cell2 = value.CellStates[2],
                    //        Cell3 = value.CellStates[3],
                    //        Cell4 = value.CellStates[4],
                    //        Cell5 = value.CellStates[5],
                    //        Cell6 = value.CellStates[6],
                    //        Cell7 = value.CellStates[7],
                    //        Cell8 = value.CellStates[8],
                    //        GameResultCode = 0
                    //    };

                    // Get Prediction
                    var prediction1 = predEngine.Predict(inputModel1);

                    retVal.Prediction = prediction1.PredictedLabel;
                    retVal.PredictionScore = prediction1.Score;
                }

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
