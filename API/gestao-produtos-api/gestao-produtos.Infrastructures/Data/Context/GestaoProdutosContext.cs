using gestao_produtos.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace gestao_produtos.Infrastructures.Data.Context
{
    internal sealed class GestaoProdutosContext : DbContext
    {
        private readonly string _collation;

        public override ChangeTracker ChangeTracker
        {
            get
            {
                base.ChangeTracker.LazyLoadingEnabled = false;
                base.ChangeTracker.CascadeDeleteTiming = CascadeTiming.OnSaveChanges;
                base.ChangeTracker.CascadeDeleteTiming = CascadeTiming.OnSaveChanges;
                return base.ChangeTracker;
            }
        }

        public DbSet<Produto> Produtos => Set<Produto>();
        public DbSet<Fornecedor> Fornecedores => Set<Fornecedor>();

        [ActivatorUtilitiesConstructor]
        public GestaoProdutosContext(DbContextOptions<GestaoProdutosContext> dbOptions) : base(dbOptions) { }

        public GestaoProdutosContext(IOptions<ConnectionStrings> options, DbContextOptions<GestaoProdutosContext> dbOptions)
            : this(dbOptions) => _collation = options.Value.Collation;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (!string.IsNullOrWhiteSpace(_collation))
                modelBuilder.UseCollation(_collation);

            modelBuilder
                .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
