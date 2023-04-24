using System.Reflection;

namespace TicTacToe.BusinessLogic
{
    public class ComputerPlayerConfig
    {
        private static System.Type ComputerPlayerType = null;
        private static Func<ITicTacToeComputerPlayer> funcCreateComputerPlayer = null;

        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterComputerPlayer<TComputerPlayerType>(Func<ITicTacToeComputerPlayer> FuncCreateComputerPlayer = null) where TComputerPlayerType: ITicTacToeComputerPlayer
        {
            ComputerPlayerType = typeof(TComputerPlayerType);
            funcCreateComputerPlayer = FuncCreateComputerPlayer;
        }

        public static ITicTacToeComputerPlayer CreateComputerPlayer()
        {
            if (funcCreateComputerPlayer == null)
                return CreateComputerPlayerDefault();
            else
                return funcCreateComputerPlayer.Invoke();
        }

        protected static ITicTacToeComputerPlayer CreateComputerPlayerDefault()
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
