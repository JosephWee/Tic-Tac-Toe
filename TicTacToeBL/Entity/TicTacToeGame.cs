using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using T3Mod = TicTacToe.Models;

namespace TicTacToe.Entity
{
    [Table("TicTacToeGames")]
    public class TicTacToeGame
    {
        [Key]
        public long InstanceId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int GridSize { get; set; }

        public T3Mod.TicTacToeGameStatus? Status { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime? CompletedDate { get; set; }

        [InverseProperty("Game")]
        public List<TicTacToeDataEntry> TicTacToeDataEntries { get; set; }

        public TicTacToeGame Copy()
        {
            var copy = new Entity.TicTacToeGame()
            {
                InstanceId = this.InstanceId,
                Description = this.Description,
                GridSize = this.GridSize,
                Status = this.Status,
                CreatedDate = this.CreatedDate,
                CompletedDate = this.CompletedDate
            };

            return copy;
        }
    }
}