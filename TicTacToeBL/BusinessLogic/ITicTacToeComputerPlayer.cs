using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicTacToe.BusinessLogic
{
    public interface ITicTacToeComputerPlayer
    {
        int GetMove(string InstanceId, int LastMoveNumber);
    }
}