using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicTacToe.BusinessLogic
{
    public class ComputerPlayerV1 : ComputerPlayerBase
    {
        public ComputerPlayerV1()
            : base(1, 2)
        {
        }

        public ComputerPlayerV1(int playerSymbolOpponent, int playerSymbolSelf)
            : base(playerSymbolOpponent, playerSymbolSelf)
        {
        }
    }
}