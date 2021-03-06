﻿using System;
using System.Linq;
using FantasyTradeAnalyzer.ProjectionGenerator.Injection.Internal;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Extensions.DependencyInjection;

namespace FantasyTradeAnalyzer.ProjectionGenerator.Injection
{
    internal class InjectWebJobsExtension : IExtensionConfigProvider
    {
        public void Initialize(ExtensionConfigContext context)
        {
            var rule = context.AddBindingRule<InjectAttribute>();

            rule.BindToInput<Anonymous>(attribute => null);

            var dependencyConfig = InitializeContainer(context);

            if (dependencyConfig == null) return;

            var serviceCollection = new ServiceCollection();
            dependencyConfig.ConfigureServices(serviceCollection);

            var container = serviceCollection.BuildServiceProvider();
            rule.AddOpenConverter<Anonymous, OpenType>(typeof(InjectConverter<>), container);
        }

        private static IDependencyConfiguration InitializeContainer(ExtensionConfigContext context)
        {
            var configType = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .FirstOrDefault(x =>
                    typeof(IDependencyConfiguration).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);

            IDependencyConfiguration configuration = null;

            if (configType == null) return configuration;

            var configInstance = Activator.CreateInstance(configType);
            configuration = (IDependencyConfiguration) configInstance;

            return configuration;
        }
    }
}