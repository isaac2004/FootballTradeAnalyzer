using Microsoft.Extensions.DependencyInjection;

namespace FantasyTradeAnalyzer.ProjectionGenerator.Injection
{
    public interface IDependencyConfiguration
    {
        void ConfigureServices(IServiceCollection services);
    }
}