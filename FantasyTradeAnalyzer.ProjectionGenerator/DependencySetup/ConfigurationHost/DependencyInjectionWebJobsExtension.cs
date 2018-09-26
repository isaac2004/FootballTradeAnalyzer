using FantasyTradeAnalyzer.ProjectionGenerator.Injection;
using Microsoft.Azure.WebJobs;

namespace FantasyTradeAnalyzer.ProjectionGenerator.ConfigurationHost
{
    public static class DependencyInjectionWebJobsBuilderExtensions
    {
        public static IWebJobsBuilder AddDependencyInjection(this IWebJobsBuilder builder)
        {
            builder.AddExtension(new InjectWebJobsExtension());
            return builder;
        }
    }
}