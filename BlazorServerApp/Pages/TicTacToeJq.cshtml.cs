using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.JSInterop;
using System.Text.Json;
using TicTacToe.Models;

namespace BlazorServerApp.Pages
{
    public class TicTacToeJqModel : PageModel
    {
        static IConfiguration _config = null;

        public TicTacToeJqModel(IConfiguration config)
        {
            _config = config;
        }

        public void OnGet()
        {
        }

        [JSInvokableAttribute("jsUpdateTicTacToe")]
        public static async Task<string> OnPostUpdateTicTacToeAsync(string requestData)
        {
            var request = JsonSerializer.Deserialize<TicTacToeUpdateRequest>(requestData);

            if (request != null)
            {
                //var configExternalServices =
                //    _config.GetSection("ExternalServices");

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

                        httpPostTask.Wait();

                        var httpResponseMessage = httpPostTask.Result;

                        var readJsonTask =
                            httpResponseMessage
                            .Content
                            .ReadFromJsonAsync<TicTacToeUpdateResponse>();

                        readJsonTask.Wait();

                        if (readJsonTask.Result != null)
                        {
                            var tictactoeUpdateResponse = readJsonTask.Result;
                            if (tictactoeUpdateResponse != null)
                                return JsonSerializer.Serialize(tictactoeUpdateResponse);
                        }
                //    }
                //}
            }

            return string.Empty;
        }
    }
}
