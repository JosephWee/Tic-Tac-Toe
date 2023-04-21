using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http;
using Microsoft.Net.Http.Headers;

namespace BlazorServerApp.Data
{
    public class TicTacToeService
    {
        private IConfiguration _config = null;

        public TicTacToeService(IConfiguration config)
        {
            _config = config;
        }

        public Task<string> GetTimestampAsync()
        {
            return Task.FromResult(DateTime.UtcNow.ToString("O"));
        }

        public Task<string> UpdateTicTacToeAsync()
        {
            var client = new HttpClient();
            var configExternalServices = this._config.GetSection("ExternalServices");
            if (configExternalServices != null)
            {
                var configTicTacToeWebApi = configExternalServices.GetSection("TicTacToeWebApi");
                if (configTicTacToeWebApi != null)
                {
                    var configEndpoint = configTicTacToeWebApi["endpoint"];
                }
            }
            return Task.FromResult(DateTime.UtcNow.ToString("O"));
        }
    }
}
