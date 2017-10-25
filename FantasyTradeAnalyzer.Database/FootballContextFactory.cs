using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace FantasyTradeAnalyzer.Database
{
    public class FootballContextFactory : IDesignTimeDbContextFactory<FootballContext>
    {
        public FootballContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<FootballContext>();
            builder.UseSqlServer(
                "Server=localhost;Database=FantasyTradeAnalyzer;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new FootballContext(builder.Options);
        }
    }
}
