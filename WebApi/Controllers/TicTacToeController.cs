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
using TicTacToe.BusinessLogic;

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

                //var computerPlayer = new T3BL.ComputerPlayerV2();
                var computerPlayer = new ComputerPlayerV3(_MLNetModelPath);
                var retVal = T3BL.TicTacToe.ProcessRequest(value, computerPlayer, _MLNetModelPath);

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
