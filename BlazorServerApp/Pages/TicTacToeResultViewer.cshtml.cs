using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.JSInterop;
using System.Text.Json;
using T3Mod = TicTacToe.Models;

namespace BlazorServerApp.Pages
{
    public class TicTacToeResultViewerModel : PageModel
    {
        static IConfiguration _config = null;

        public TicTacToeResultViewerModel(IConfiguration config)
        {
            _config = config;
        }

        public void OnGet()
        {
        }

        [JSInvokableAttribute("jsGetTicTacToeResultSet")]
        public static async Task<string> OnPostFetchTicTacToeResultSet(int PageSize, int PageNum)
        {
            var configExternalServices =
                _config.GetSection("ExternalServices");

            if (configExternalServices != null)
            {
                var configTicTacToeWebApi = configExternalServices.GetSection("TicTacToeWebApi");
                if (configTicTacToeWebApi != null)
                {
                    var endpoint = configTicTacToeWebApi["endpoint"];

                    var client = new HttpClient();
                    var httpGetTask =
                        client.GetAsync($"{endpoint}?PageSize={PageSize}&PageNum={PageNum}");

                    httpGetTask.Wait();

                    var httpResponseMessage = httpGetTask.Result;

                    var readStringTask =
                        httpResponseMessage
                        .Content
                        .ReadAsStringAsync();

                    readStringTask.Wait();

                    if (readStringTask.Result != null)
                    {
                        var tictactoeResultSet = readStringTask.Result;

                        return tictactoeResultSet;
                    }
                }
            }

            return string.Empty;
        }
    }
}
