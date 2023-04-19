namespace BlazorServerApp.Data
{
    public class TicTacToeService
    {
        public Task<string> GetTimestampAsync()
        {
            return Task.FromResult(DateTime.UtcNow.ToString("O"));
        }
    }
}
