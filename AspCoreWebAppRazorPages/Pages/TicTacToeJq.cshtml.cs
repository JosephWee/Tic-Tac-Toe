using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.JSInterop;
using System.Text.Json;
using TicTacToe.Models;

namespace AspCoreWebAppRazorPages.Pages
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

        public async Task<IActionResult> OnPostUpdateTicTacToeAsync(string requestData)
        {
            var request = JsonSerializer.Deserialize<TicTacToeUpdateRequest>(requestData);

            if (request != null)
            {
                var endpoint = _config.GetValue<string>("TicTacToeWebApiEndPoint");

                var client = new HttpClient();
                var httpResponseMessage =
                    await client.PostAsJsonAsync<TicTacToeUpdateRequest>(
                        endpoint,
                        request
                    );

                var tictactoeUpdateResponse =
                    await httpResponseMessage
                    .Content
                    .ReadFromJsonAsync<TicTacToeUpdateResponse>();

                if (tictactoeUpdateResponse != null)
                {
                    string jsonResponse = JsonSerializer.Serialize(tictactoeUpdateResponse);
                    return new OkObjectResult(jsonResponse);
                }
            }

            return NotFound();
        }
    }
}
