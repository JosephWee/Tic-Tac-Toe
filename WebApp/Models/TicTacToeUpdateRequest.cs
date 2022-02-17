using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class TicTacToeUpdateRequest
    {
        [Range(3, 5)]
        public int GridSize { get; set; }
        public int TotalCellCount { get { return this.GridSize * this.GridSize; } }
        public List<int> CellStates { get; set; }
    }
}