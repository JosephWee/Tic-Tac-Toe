using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Models;

namespace TicTacToe.Extensions
{
    public static class Extensions
    {
        public static string ToDisplay(this TicTacToeGameStatus status)
        {
            if (status == TicTacToeGameStatus.InProgress)
                return "In Progress";
            else if (status == TicTacToeGameStatus.Player1Wins)
                return "Player 1 Wins";
            else if (status == TicTacToeGameStatus.Player2Wins)
                return "Player 2 Wins";
            else if (status == TicTacToeGameStatus.Draw)
                return "Draw";

            return string.Empty;
        }
    }
}
