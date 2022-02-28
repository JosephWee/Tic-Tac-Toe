using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using TicTacToe.ML;

namespace WebApp.Controllers
{
    [Authorize]
    public class TicTacToeController : ApiController
    {
        // GET api/values
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
        public IHttpActionResult Post([FromBody] TicTacToe.Models.TicTacToeUpdateRequest value)
        {
            TicTacToe.Models.TicTacToeUpdateResponse tttUpdateResponse = null;

            try
            {
                 //tttUpdateRequest =
                 //   JsonConvert.DeserializeObject(
                 //   value.ToObject<TicTacToe.Models.TicTacToeUpdateRequest>();

                if (value == null)
                    return BadRequest();

                tttUpdateResponse =
                    TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(value);

                if (value.GridSize == 3)
                {
                    var cellStates = value.CellStates.ToList();

                    if (!cellStates.Any(x => !TicTacToe.BusinessLogic.TicTacToe.ValidCellStateValues.Contains(x)))
                    {
                        int moveNumber = cellStates.Count(x => x != 0);

                        if (tttUpdateResponse.ComputerMove.HasValue)
                        {
                            moveNumber++;
                            cellStates[tttUpdateResponse.ComputerMove.Value] = 2;
                        }

                        //var inputModel1 =
                        //    new MLModel1.ModelInput()
                        //    {
                        //        MoveNumber = moveNumber,
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

                        var inputModel2 =
                            new MLModel2.ModelInput()
                            {
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

                        //Get Prediction
                        //var prediction1 = MLModel1.Predict(inputModel1);
                        var prediction2 = MLModel2.Predict(inputModel2);

                        tttUpdateResponse.Prediction = prediction2.Prediction;
                        tttUpdateResponse.PredictionScore = prediction2.Score;
                    }
                }
            }
            catch (ArgumentException argEx)
            {
                return BadRequest();
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }

            return Ok(tttUpdateResponse);
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
