﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebApp.Entity
{
    public class TicTacToeDataContext : DbContext
    {
        public TicTacToeDataContext() : base("TicTacToeData")
        {
        }

        public DbSet<TicTacToeDataEntry> TicTacToeData { get; set; }
    }
}