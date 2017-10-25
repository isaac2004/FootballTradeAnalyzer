using FantasyTradeAnalyzer.Database;
using FantasyTradeAnalyzer.Respository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Net;

namespace FantasyTradeAnalyzer.ProjectGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IProjectionRespository, ProjectionRespository>()
                .AddDbContext<FootballContext>(options =>
                   options.UseSqlServer("ConnectionString"))
                .BuildServiceProvider();            


            var projectionService = serviceProvider.GetService<IProjectionRespository>();
            projectionService.PopulateProjections();

        }
    }
}
