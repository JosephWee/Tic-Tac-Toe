using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class TicTacToeUpdateRequest
    {
        public int GridSize { get; set; }
        public int TotalCellCount { get; set; }
        public List<int> CellStates { get; set; }
    }
}