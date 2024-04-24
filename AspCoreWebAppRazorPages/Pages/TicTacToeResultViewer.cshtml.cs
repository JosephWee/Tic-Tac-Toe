using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.JSInterop;
using System.Text.Json;
using T3Mod = TicTacToe.Models;

namespace AspCoreWebAppRazorPages.Pages
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

        public async Task<IActionResult> OnPostFetchTicTacToeResultSetAsync(int PageSize, int PageNum)
        {
            var endpoint = _config.GetValue<string>("TicTacToeWebApiEndPoint");

            var client = new HttpClient();
            var httpResponseMessage =
                await client.GetAsync($"{endpoint}?PageSize={PageSize}&PageNum={PageNum}");

            var tictactoeResultSet =
                await httpResponseMessage
                .Content
                .ReadAsStringAsync();

            return new OkObjectResult(tictactoeResultSet);
        }
    }
}
