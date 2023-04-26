using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicTacToe.BusinessLogic
{
    public interface ITicTacToeComputerPlayer
    {
        int PlayerSymbolOpponent { get; }
        int PlayerSymbolSelf { get; }
        void SetPlayerSymbols(int playerSymbolOpponent, int playerSymbolSelf);
        int GetMove(int GridSize, List<int> CellStates);
    }
}