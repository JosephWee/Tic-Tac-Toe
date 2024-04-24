using TicTacToe.Models;

namespace AspCoreWebAppRazorPages
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
