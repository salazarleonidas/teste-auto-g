using gestao_produtos.Application.Interfaces;
using gestao_produtos.Application.Mapper;
using gestao_produtos.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace gestao_produtos.Application.IoC
{
    [ExcludeFromCodeCoverage]
    public static class ConfigureService
    {
        public static IServiceCollection AddApplication(this IServiceCollection services) =>
            services
                .AddAutoMapper(typeof(DomainToDtoProduto))
                .AddScoped<IProdutoService, ProdutoService>();
    }
}
