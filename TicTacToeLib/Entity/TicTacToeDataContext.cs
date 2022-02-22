using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace TicTacToe.Entity
{
    public class TicTacToeDataContext : DbContext
    {
        public TicTacToeDataContext() : base("name=TicTacToeDataConnString")
        {
        }

        public TicTacToeDataContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        public DbSet<TicTacToeDataEntry> TicTacToeData { get; set; }

        public DbSet<TicTacToeClassificationModel01> TicTacToeClassificationModel01 { get; set; }
    }
}