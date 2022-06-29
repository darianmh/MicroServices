using Domain.Models;
using Elasticsearch.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace WebCore.Configuration
{
    public static class ElasticConfiguration
    {
        private static readonly Uri Uri = new("http://172.16.20.14:9200");
        private static readonly string Index = $"darian-project-index-{DateTime.Now:yyyy-MM}";
        public static void UseElastic(this WebApplicationBuilder builder)
        {
            ConfigureLogging();
            ConfigureSerilog(builder);
            AddElastic(builder.Services);
        }

        private static void ConfigureSerilog(WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((context, configuration) =>
            {
                configuration
                    .Enrich.FromLogContext()
                    .Enrich.WithMachineName()
                    .WriteTo.Debug()
                    .WriteTo.Elasticsearch(
                        new ElasticsearchSinkOptions(Uri)
                        {
                            IndexFormat = Index,
                            AutoRegisterTemplate = true,
                            NumberOfShards = 2,
                            NumberOfReplicas = 1
                        })
                    .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                    .ReadFrom.Configuration(context.Configuration);
            });
        }

        private static void AddElastic(IServiceCollection services)
        {
            var pool = new SingleNodeConnectionPool(Uri);
            var settings = new ConnectionSettings(pool)
                .DefaultMappingFor<ElkLogModel>(x => x.IndexName(Index));
            var client = new ElasticClient(settings);
            services.AddSingleton(client);
        }

        private static void ConfigureLogging()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile(
                    $"appsettings.{environment}.json",
                    optional: true)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(Uri)
                {
                    AutoRegisterTemplate = true,
                    IndexFormat = Index
                })
                .Enrich.WithProperty("Environment", environment)
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }



    }
}
