using gestao_produtos.Domain.Interfaces.Repositories;
using gestao_produtos.Domain.Models;
using gestao_produtos.Infrastructures.Data.Common;
using gestao_produtos.Infrastructures.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestao_produtos.Infrastructures.Data.Repositories
{
    internal class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(GestaoProdutosContext context) : base(context) { }

        public async Task<bool> AtualizarAsync(Produto obj)
        {
            var entity = await DbSet.Include(c => c.Fornecedores).FirstOrDefaultAsync(q => q.Id == obj.Id);

            if (entity == null) return false;

            _context.Entry(entity).CurrentValues.SetValues(obj);

            foreach (var fornecedor in entity.Fornecedores.ToList())
                if (!obj.Fornecedores.Any(c => c.Id == fornecedor.Id))
                    _context.Fornecedores.Remove(fornecedor);

            foreach (var fornecedor in obj.Fornecedores)
            {
                var existingFornecedor = entity.Fornecedores
                    .Where(c => c.Id == fornecedor.Id && c.Id != default(int))
                    .SingleOrDefault();

                if (existingFornecedor != null)
                    _context.Entry(existingFornecedor).CurrentValues.SetValues(fornecedor);
                else
                    entity.Fornecedores.Add(fornecedor);
            }

            _context.SaveChanges();

            return true;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await DbSet.FirstOrDefaultAsync(q => q.Id == id);

            if (entity == null) return false;

            var deleted = new Produto { Id = id, Situacao = false };
            _context.Produtos.Attach(deleted);
            _context.Entry(entity).Property(q => q.Situacao).IsModified = true;
            _context.SaveChanges();

            return true;
        }

        public async Task<long> InserirAsync(Produto obj)
        {
            await DbSet.AddAsync(obj);
            _context.SaveChanges();

            return obj.Id;
        }

        public async Task<Produto> ObterProdutoById(long id)
            => await DbSet
                .AsNoTrackingWithIdentityResolution()
                .Include(c => c.Fornecedores)
                .FirstOrDefaultAsync(c => c.Id == id);

        public async Task<IEnumerable<Produto>> ObterProdutos()
            => await DbSet
                .AsNoTrackingWithIdentityResolution()
                .Include(e => e.Fornecedores)
                .Where(q => q.Situacao)
                .OrderBy(e => e.Id)
                .ToListAsync();
    }
}
