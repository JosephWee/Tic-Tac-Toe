using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TicTacToe.Models
{
    public class TicTacToeUpdateRequest
    {
        [Range(0, Int64.MaxValue)]
        public long InstanceId { get; set; }
        
        [Range(3, 5)]
        public int GridSize { get; set; }

        [Range(1, 2)]
        public int NumberOfPlayers { get; set; }

        public List<int> CellStates { get; set; }

        public int TotalCellCount { get { return this.GridSize * this.GridSize; } }
        
        public TicTacToeUpdateRequest()
        {
            GridSize = 3;
            CellStates = new List<int>();
        }
    }
}