using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Entity
{
    public class TicTacToeClassificationModel02
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string InstanceId { get; set; }
        public int MoveNumber { get; set; }
        public int CellIndex { get; set; }
        public int CellContent { get; set; }
        public int GameResultCode { get; set; }
    }
}
