using gestao_produtos.Domain.Repositories;
using gestao_produtos.Infrastructures.Data.Context;
using gestao_produtos.Infrastructures.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace gestao_produtos.Infrastructures.IoC
{
    [ExcludeFromCodeCoverage]
    public static class ConfigureServices
    {
        private const int DbMaxRetryCount = 3;
        private const int DbCommandTimeout = 35;
        private const string DbMigrationAssemblyName = "gestao-produtos.Infrastructures";

        public static IServiceCollection AddAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetSection(ConnectionStrings.ConfigSectionPath).Get<ConnectionStrings>();

            return services.AddSingleton(connectionString);
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
            => services
                    .AddTransient<IProdutoRepository, ProdutoRepository>();

        public static IServiceCollection AddContext(this IServiceCollection services)
        {
            return services.AddDbContext<GestaoProdutosContext>((serviceProvider, optionsBuilder)
                =>
            {
                var connections = serviceProvider.GetRequiredService<ConnectionStrings>();

                optionsBuilder.UseSqlServer(connections.Database, sqlServerOptions =>
                {
                    sqlServerOptions.MigrationsAssembly(DbMigrationAssemblyName);
                    sqlServerOptions.EnableRetryOnFailure(DbMaxRetryCount);
                    sqlServerOptions.CommandTimeout(DbCommandTimeout);
                });

                var logger = serviceProvider.GetRequiredService<ILogger<GestaoProdutosContext>>();

                // Log tentativas de repetição
                optionsBuilder.LogTo(
                    (eventId, _) => eventId.Id == CoreEventId.ExecutionStrategyRetrying,
                    eventData =>
                    {
                        if (eventData is not ExecutionStrategyEventData retryEventData)
                            return;

                        var exceptions = retryEventData.ExceptionsEncountered;

                        logger.LogWarning(
                            "----- Retry #{Count} with delay {Delay} due to error: {Message}",
                            exceptions.Count,
                            retryEventData.Delay,
                            exceptions[^1].Message);
                    });

                var environment = serviceProvider.GetRequiredService<IHostEnvironment>();
                if (!environment.IsProduction())
                {
                    optionsBuilder.EnableDetailedErrors();
                    optionsBuilder.EnableSensitiveDataLogging();
                }
            });
        }
    }
}
