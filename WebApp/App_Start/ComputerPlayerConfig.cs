using System.Web;
using System.Web.Optimization;
using System.Reflection;

namespace WebApp
{
    public class ComputerPlayerConfig
    {
        private static System.Type ComputerPlayerType = null;

        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterComputerPlayer(BusinessLogic.ITicTacToeComputerPlayer computerPlayer)
        {
            ComputerPlayerType = computerPlayer.GetType();
        }

        public static BusinessLogic.ITicTacToeComputerPlayer CreateComputerPlayer()
        {
            if (ComputerPlayerType == null)
                return null;

            Assembly assembly = ComputerPlayerType.Assembly;
            BusinessLogic.ITicTacToeComputerPlayer p =
                assembly.CreateInstance(ComputerPlayerType.FullName) as BusinessLogic.ITicTacToeComputerPlayer;

            return p;
        }
    }
}
