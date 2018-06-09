using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FantasyTradeAnalyzer.Database;
using FantasyTradeAnalyzer.Respository;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Azure.WebJobs.Host.Protocols;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FantasyTradeAnalyzer.ProjectionGenerator
{
    public static class ProcessProjections
    {
        [FunctionName("ProcessProjections")]
        public static void Run([TimerTrigger("0 */10 * * * *")]TimerInfo myTimer, TraceWriter log, [Inject]IProjectionRespository projectionRespository)
        {
            projectionRespository.PopulateProjections();


            var projections = projectionRespository.GetProjections().Result.ToList();

            var count = projections.Count;
        }
    }


    [Binding]
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public class InjectAttribute : Attribute
    {
    }

    public class InjectBindingProvider : IBindingProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public static readonly ConcurrentDictionary<Guid, IServiceScope> Scopes =
      new ConcurrentDictionary<Guid, IServiceScope>();

        public InjectBindingProvider(IServiceProvider serviceProvider) =>
            _serviceProvider = serviceProvider;

        public Task<IBinding> TryCreateAsync(BindingProviderContext context)
        {
            IBinding binding = new InjectBinding(_serviceProvider, context.Parameter.ParameterType);
            return Task.FromResult(binding);
        }
    }

    public class InjectBinding : IBinding
    {
        private readonly Type _type;
        private readonly IServiceProvider _serviceProvider;

        public InjectBinding(IServiceProvider serviceProvider, Type type)
        {
            _type = type;
            _serviceProvider = serviceProvider;
        }

        public bool FromAttribute => true;

        public Task<IValueProvider> BindAsync(object value, ValueBindingContext context) =>
            Task.FromResult((IValueProvider)new InjectValueProvider(value));

        public async Task<IValueProvider> BindAsync(BindingContext context)
        {
            await Task.Yield();

            var scope = InjectBindingProvider.Scopes.GetOrAdd(context.FunctionInstanceId, (_) => _serviceProvider.CreateScope());
            var value = scope.ServiceProvider.GetRequiredService(_type);

            return await BindAsync(value, context.ValueContext);
        }

        public ParameterDescriptor ToParameterDescriptor() => new ParameterDescriptor();

        private class InjectValueProvider : IValueProvider
        {
            private readonly object _value;

            public InjectValueProvider(object value) => _value = value;

            public Type Type => _value.GetType();

            public Task<object> GetValueAsync() => Task.FromResult(_value);

            public string ToInvokeString() => _value.ToString();
        }
    }

    public class InjectConfiguration : IExtensionConfigProvider
    {
        public void Initialize(ExtensionConfigContext context)
        {
            var services = new ServiceCollection();
            RegisterServices(services);
            var serviceProvider = services.BuildServiceProvider(true);

            context
                .AddBindingRule<InjectAttribute>()
                .Bind(new InjectBindingProvider(serviceProvider));
        }
        private void RegisterServices(IServiceCollection services)
        {
            services.AddDbContext<FootballContext>(options =>
          options.UseSqlServer(System.Environment.GetEnvironmentVariable("SQLConnectionString", EnvironmentVariableTarget.Process)));

            services.AddTransient<IProjectionRespository, ProjectionRespository>();
        }
    }

    public class ScopeCleanupFilter : IFunctionInvocationFilter, IFunctionExceptionFilter
    {
        public Task OnExceptionAsync(FunctionExceptionContext exceptionContext, CancellationToken cancellationToken)
        {
            RemoveScope(exceptionContext.FunctionInstanceId);
            return Task.CompletedTask;
        }

        public Task OnExecutedAsync(FunctionExecutedContext executedContext, CancellationToken cancellationToken)
        {
            RemoveScope(executedContext.FunctionInstanceId);
            return Task.CompletedTask;
        }

        public Task OnExecutingAsync(FunctionExecutingContext executingContext, CancellationToken cancellationToken) =>
            Task.CompletedTask;

        private void RemoveScope(Guid id)
        {
            if (InjectBindingProvider.Scopes.TryRemove(id, out var scope))
            {
                scope.Dispose();
            }
        }
    }

}
