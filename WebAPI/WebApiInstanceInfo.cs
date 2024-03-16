using TicTacToe.Models;

namespace WebApi
{
    public class WebApiInstanceInfo
    {
        public string AppInstanceId { get; set; }

        public string AppStartTimeUTC { get; set; }

        public WebApiInstanceInfo()
        {
            AppInstanceId = string.Empty;
            AppStartTimeUTC = string.Empty;
        }
    }
}
