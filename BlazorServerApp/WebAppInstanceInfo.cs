using TicTacToe.Models;

namespace BlazorServerApp
{
    public class WebAppInstanceInfo
    {
        public string AppInstanceId { get; set; }

        public string AppStartTimeUTC { get; set; }

        public WebAppInstanceInfo()
        {
            AppInstanceId = string.Empty;
            AppStartTimeUTC = string.Empty;
        }
    }
}
