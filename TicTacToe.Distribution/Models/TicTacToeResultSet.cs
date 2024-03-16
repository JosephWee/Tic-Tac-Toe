using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TicTacToe.Models
{
    public class TicTacToeResultSet
    {
        public string AppInstanceId { get; set; }

        public string AppStartTimeUTC { get; set; }

        public List<TicTacToeResult> Results { get; set; }
        
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int PageCount { get; set; }

        public TicTacToeResultSet()
        {
            AppInstanceId = string.Empty;
            AppStartTimeUTC = string.Empty;
            Results = new List<TicTacToeResult>();
            PageNumber = 0;
            PageSize = 0;
            PageCount = 0;
        }
    }
}