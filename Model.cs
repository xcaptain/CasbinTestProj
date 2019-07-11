using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CasbinTestProj
{
    public class TestDbContext : DbContext
    {
        public DbSet<CasbinRule> CasbinRule { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=casbin_test.sqlite3");
        }
    }

    public class CasbinRule
    {
        public int Id { get; set; }
        public string PType { get; set; }
        public string V0 { get; set; }
        public string V1 { get; set; }
        public string V2 { get; set; }
        public string V3 { get; set; }
        public string V4 { get; set; }
        public string V5 { get; set; }

    }
}
