using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace WebApp.Controllers
{
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
        public IHttpActionResult Post([FromBody] string value)
        {
            Models.TicTacToeUpdateResponse tttResponse =
                new Models.TicTacToeUpdateResponse();

            Models.TicTacToeUpdateRequest tttUpdateRequest =
                JsonConvert.DeserializeObject<Models.TicTacToeUpdateRequest>(value);

            if (tttUpdateRequest == null
                || tttUpdateRequest.CellStates == null
                || tttUpdateRequest.CellStates.Count != tttUpdateRequest.TotalCellCount)
            {
                return BadRequest();
            }

            

            return Ok();
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
