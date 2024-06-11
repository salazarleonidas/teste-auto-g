using gestao_produtos.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace gestao_produtos.Domain.Interfaces.Repositories
{
    public interface IProdutoRepository
    {
        Task<bool> AtualizarAsync(Produto obj);
        Task<bool> DeleteAsync(long id);
        Task<long> InserirAsync(Produto obj);
        Task<PagedResult<Produto>> ObterProdutos(int? page, int pagesize);
        Task<Produto> ObterProdutoById(long id);
    }
}
