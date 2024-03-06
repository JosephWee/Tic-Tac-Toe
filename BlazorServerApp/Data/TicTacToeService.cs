using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http;
using Microsoft.Net.Http.Headers;
using TicTacToe.Models;

namespace BlazorServerApp.Data
{
    public class TicTacToeService
    {
        private IConfiguration _config = null;

        public TicTacToeService(IConfiguration config)
        {
            _config = config;
        }

        public string GetTimestamp()
        {
            return DateTime.UtcNow.ToString("O");
        }

        public async Task<TicTacToeUpdateResponse> UpdateTicTacToe(TicTacToeUpdateRequest request)
        {
            //var configExternalServices =
            //    this._config.GetSection("ExternalServices");

            //if (configExternalServices != null)
            //{
            //    var configTicTacToeWebApi = configExternalServices.GetSection("TicTacToeWebApi");
            //    if (configTicTacToeWebApi != null)
            //    {
                    //var endpoint = configTicTacToeWebApi["endpoint"];
                    var endpoint = _config.GetValue<string>("TicTacToeWebApiEndPoint");

                    var client = new HttpClient();
                    var httpPostTask =
                        client.PostAsJsonAsync<TicTacToeUpdateRequest>(
                            endpoint,
                            request
                        );

                    await httpPostTask;

                    var httpResponseMessage = httpPostTask.Result;
                    
                    var readJsonTask =
                        httpResponseMessage
                        .Content
                        .ReadFromJsonAsync<TicTacToeUpdateResponse>();

                    await readJsonTask;

                    if (readJsonTask.Result != null)
                        return readJsonTask.Result;
            //    }
            //}

            return null;
        }
    }
}
