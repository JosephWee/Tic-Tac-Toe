using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicTacToe.Models
{
    public class TicTacToeUpdateResponse
    {
        public string AppInstanceId { get; set; }

        public string AppStartTimeUTC { get; set; }

        public TicTacToeGameStatus Status { get; set; }
        public List<int> WinningCells { get; set; }

        public int? ComputerMove { get; set; }

        public float? Prediction { get; set; }

        public float[] PredictionScore { get; set; }

        public TicTacToeUpdateResponse()
        {
            AppInstanceId = string.Empty;
            AppStartTimeUTC = string.Empty;
            Status = TicTacToeGameStatus.InProgress;
            WinningCells = new List<int>();
            ComputerMove = null;
            Prediction = null;
            PredictionScore = null;
        }
    }
}