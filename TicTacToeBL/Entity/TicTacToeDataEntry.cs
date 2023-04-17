using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace TicTacToe.Entity
{
    [Table("TicTacToeData")]
    public class TicTacToeDataEntry
    {
        public int Id { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        [MaxLength(200)]
        public string InstanceId { get; set; }
        
        [Required]
        [Range(0, int.MaxValue)]
        public int GridSize { get; set; }
        
        [Required]
        [Range(0, int.MaxValue)]
        public int MoveNumber { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int CellIndex { get; set; }

        [Required]
        public int CellContent { get; set; }

    }
}