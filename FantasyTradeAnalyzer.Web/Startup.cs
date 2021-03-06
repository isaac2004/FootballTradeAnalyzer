﻿using FantasyTradeAnalyzer.Database;
using FantasyTradeAnalyzer.Respository;
using FantasyTradeAnalyzer.Service;
using FantasyTradeAnalyzer.Service.Yahoo;
using Microsoft.ApplicationInsights.AspNetCore;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.SnapshotCollector;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using YahooFantasyWrapper.Client;
using YahooFantasyWrapper.Configuration;
using YahooFantasyWrapper.Infrastructure;

namespace FantasyTradeAnalyzer.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1); ;
            services.AddMemoryCache();

            services.AddDbContext<FootballContext>(options =>
                   options.UseSqlServer(Configuration.GetConnectionString("FootballDatabase")));

            services.AddSingleton<ITelemetryProcessorFactory>(new SnapshotCollectorTelemetryProcessorFactory());
            services.Configure<YahooConfiguration>((IConfiguration)this.Configuration.GetSection("YahooConfiguration"));
            services.AddTransient<IProjectionRespository, ProjectionRespository>();
            services.AddTransient<IRequestFactory, RequestFactory>();
            services.AddTransient<IYahooAuthClient, YahooAuthClient>();
            services.AddTransient<IYahooFantasyClient, YahooFantasyClient>();
            services.AddTransient<IYahooService, YahooService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (HostingEnvironmentExtensions.IsDevelopment(env))
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                ExceptionHandlerExtensions.UseExceptionHandler(app, "/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            MapperConfig.Config();
            app.UseMvc(routes =>
            MapRouteRouteBuilderExtensions.MapRoute(routes, "default", "{controller=Home}/{action=Index}/{id?}"));
        }

        private class SnapshotCollectorTelemetryProcessorFactory : ITelemetryProcessorFactory
        {
            public ITelemetryProcessor Create(ITelemetryProcessor next) =>
                new SnapshotCollectorTelemetryProcessor(next);
        }
    }
}