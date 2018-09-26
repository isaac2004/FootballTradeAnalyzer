using FantasyTradeAnalyzer.Database;
using FantasyTradeAnalyzer.ProjectionGenerator.Injection;
using FantasyTradeAnalyzer.Respository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FantasyTradeAnalyzer.ProjectionGenerator
{
    public class DependencyConfiguration : IDependencyConfiguration
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<FootballContext>(options =>
        options.UseSqlServer(System.Environment.GetEnvironmentVariable("SQLConnectionString", EnvironmentVariableTarget.Process)));

            services.AddSingleton<IProjectionRespository, ProjectionRespository>();
        }
    }
}