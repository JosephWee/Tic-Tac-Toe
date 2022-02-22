using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace TicTacToe.Entity
{
    public class TicTacToeDataContext : DbContext
    {
        private readonly string _connectionString = null;

        public TicTacToeDataContext() : base("name=TicTacToeDataConnString")
        {
        }

        public DbSet<TicTacToeDataEntry> TicTacToeData { get; set; }
    }
}