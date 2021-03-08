using DataLib.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace ASP.Data
{
    public class BookContext : DbContext
    {
        public BookContext() : base("Gazprom")
        { }
        public DbSet<Factory> Factory { get; set; }
        public DbSet<Unit> Unit { get; set; }

        public DbSet<Tank> Tank { get; set; }
    }
}
