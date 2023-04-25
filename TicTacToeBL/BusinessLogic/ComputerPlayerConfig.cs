using System.Reflection;

namespace TicTacToe.BusinessLogic
{
    public class ComputerPlayerConfig
    {
        private static System.Type ComputerPlayerType = null;
        private static Func<ComputerPlayerBase> funcCreateComputerPlayer = null;

        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterComputerPlayer<TComputerPlayerType>(Func<ComputerPlayerBase> FuncCreateComputerPlayer)
            where TComputerPlayerType: ComputerPlayerBase
        {
            ComputerPlayerType = typeof(TComputerPlayerType);
            funcCreateComputerPlayer = FuncCreateComputerPlayer;
        }

        public static ComputerPlayerBase CreateComputerPlayer()
        {
            return funcCreateComputerPlayer.Invoke();
        }
    }
}
