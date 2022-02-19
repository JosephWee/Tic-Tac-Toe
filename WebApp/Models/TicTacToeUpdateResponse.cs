using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class TicTacToeUpdateResponse
    {
        public TicTacToeGameStatus Status { get; set; }
        public List<int> WinningCells { get; set; }

        public int? ComputerMove { get; set; }

        public TicTacToeUpdateResponse()
        {
            Status = TicTacToeGameStatus.InProgress;
            WinningCells = new List<int>();
            ComputerMove = null;
        }
    }
}