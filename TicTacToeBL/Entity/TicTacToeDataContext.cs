using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicTacToe.Entity;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace TicTacToe.Entity
{
    public class TicTacToeDataContext : DbContext
    {
        protected string dbNameOrConnectionString = string.Empty;

        public TicTacToeDataContext()
        {
            dbNameOrConnectionString = DbContextConfig.Get("TicTacToeData");
        }

        public TicTacToeDataContext(string nameOrConnectionString)
        {
            dbNameOrConnectionString = nameOrConnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(dbNameOrConnectionString);
        }

        public DbSet<TicTacToeGame> TicTacToeGames { get; set; }

        public DbSet<TicTacToeDataEntry> TicTacToeData { get; set; }

        public DbSet<TicTacToeClassificationModel01> TicTacToeClassificationModel01 { get; set; }

        public DbSet<TicTacToeClassificationModel02> TicTacToeClassificationModel02 { get; set; }
    }
}