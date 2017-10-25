using FantasyTradeAnalyzer.Database.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FantasyTradeAnalyzer.Database
{
   public  class FootballContext : DbContext
    {
        public FootballContext(DbContextOptions<FootballContext> options) : base(options) { }
        public DbSet<ActualStatisticEntity> ActualStatistics { get; set; }
        public DbSet<ProjectionEntity> Projections { get; set; }
        public DbSet<RankingEntity> Rankings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
