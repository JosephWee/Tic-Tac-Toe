using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Entity
{
    [Table("TicTacToeData")]
    public class TicTacToeDataEntry
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        [MaxLength(200)]
        public string InstanceId { get; set; }
        public int GridSize { get; set; }
        public int MoveNumber { get; set; }
        public int CellIndex { get; set; }
        public int CellContent { get; set; }

    }
}