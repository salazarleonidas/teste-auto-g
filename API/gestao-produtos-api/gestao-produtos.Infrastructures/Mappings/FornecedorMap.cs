using gestao_produtos.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace gestao_produtos.Infrastructures.Mappings
{
    internal class FornecedorMap : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.ToTable("Fornecedores");

            builder.HasKey(entity => entity.Id);

            builder.Property(entity => entity.Id)
                .IsRequired();

            builder.Property(entity => entity.Descricao)
                .IsRequired()
                .HasMaxLength(250)
                .IsUnicode(false);

            builder
                .Property(entity => entity.Cnpj)
                .HasMaxLength(14);

            builder.HasMany(entity => entity.Produtos).WithMany(q => q.Fornecedores);
        }
    }
}
