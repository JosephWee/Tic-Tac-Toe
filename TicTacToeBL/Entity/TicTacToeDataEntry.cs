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
        [Key]
        public long Id { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [ForeignKey("TicTacToeGame")]
        public long InstanceId { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int MoveNumber { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int CellIndex { get; set; }

        [Required]
        public int CellContent { get; set; }

        [ForeignKey("InstanceId")]
        public TicTacToeGame Game { get; set; }

        public TicTacToeDataEntry Copy()
        {
            var copy = new Entity.TicTacToeDataEntry()
            {
                Id = this.Id,
                CreatedDate = this.CreatedDate,
                InstanceId = this.InstanceId,
                MoveNumber = this.MoveNumber,
                CellIndex = this.CellIndex,
                CellContent = this.CellContent
            };

            return copy;
        }
    }
}