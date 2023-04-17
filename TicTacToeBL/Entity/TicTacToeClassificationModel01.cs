using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Entity
{
    public class TicTacToeClassificationModel01
    {
        public int Id { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        [MaxLength(200)]
        public string InstanceId { get; set; }
        public int MoveNumber { get; set; }
        public int Cell0 { get; set; }
        public int Cell1 { get; set; }
        public int Cell2 { get; set; }
        public int Cell3 { get; set; }
        public int Cell4 { get; set; }
        public int Cell5 { get; set; }
        public int Cell6 { get; set; }
        public int Cell7 { get; set; }
        public int Cell8 { get; set; }
        public int GameResultCode { get; set; }
        public bool Draw { get; set; }
        public bool Player1Wins { get; set; }
        public bool Player2Wins { get; set; }
    }
}
