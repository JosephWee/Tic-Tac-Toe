using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TicTacToe.Models
{
    public class TicTacToeResult
    {
        [Range(0, Int64.MaxValue)]
        public long InstanceId { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Range(3, 5)]
        public int GridSize { get; set; }

        public List<int> CellStates { get; set; }

        public int TotalCellCount { get { return this.GridSize * this.GridSize; } }

        public TicTacToeGameStatus? Status { get; set; }

        public List<int> WinningCells { get; set; }

        public TicTacToeResult()
        {
            GridSize = 3;
            CellStates = new List<int>();
            WinningCells = new List<int>();
        }
    }
}