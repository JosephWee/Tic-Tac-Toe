using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        public IHttpActionResult Post([FromBody] Models.TicTacToeUpdateRequest value)
        {
            Models.TicTacToeUpdateResponse tttUpdateResponse = null;

            try
            {
                 //tttUpdateRequest =
                 //   JsonConvert.DeserializeObject(
                 //   value.ToObject<Models.TicTacToeUpdateRequest>();

                if (value == null)
                    return BadRequest();

                tttUpdateResponse =
                    BusinessLogic.TicTacToe.EvaluateResult(value);
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
