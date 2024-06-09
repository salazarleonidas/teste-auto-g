using gestao_produtos.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace gestao_produtos.Infrastructures.Mappings
{
    internal class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produtos");

            builder.HasKey(entity => entity.Id);

            builder.Property(entity => entity.Id)
                .IsRequired();

            builder.Property(entity => entity.Descricao)
                .IsRequired()
                .HasMaxLength(250)
                .IsUnicode(false);

            builder.Property(entity => entity.DataValidade).IsRequired();

            builder.Property(entity => entity.DataFabricacao).IsRequired();

            builder.HasMany(entity => entity.Fornecedores).WithMany(q => q.Produtos);
        }
    }
}
