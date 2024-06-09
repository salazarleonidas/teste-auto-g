using gestao_produtos.Infrastructures.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace gestao_produtos.Infrastructures.IoC
{
    public static class ConfigureMigration
    {
        public static async Task UseMigrationsAsync(this IApplicationBuilder app, ILogger logger)
        {
            await using var serviceScope = app.ApplicationServices.CreateAsyncScope();
            await using var context = serviceScope.ServiceProvider.GetRequiredService<GestaoProdutosContext>();

            var connectionString = context.Database.GetConnectionString();
            logger.LogInformation("----- SQL Server: {Connection}", connectionString);
            logger.LogInformation("----- SQL Server: Verificando se existem migrações pendentes...");

            if ((await context.Database.GetPendingMigrationsAsync()).Any())
            {
                logger.LogInformation("----- SQL Server: Criando e migrando a base de dados...");

                await context.Database.MigrateAsync();

                logger.LogInformation("----- SQL Server: Base de dados criada e migrada com sucesso!");
            }
            else
            {
                logger.LogInformation("----- SQL Server: Migrações estão em dia");
            }
        }
    }
}
