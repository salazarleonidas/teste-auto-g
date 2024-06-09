using Ardalis.Result;
using gestao_produtos.Application.DTO.ProtudoDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace gestao_produtos.Application.Interfaces
{
    public interface IProdutoService
    {
        Task<Result<bool>> Atualizar(ProdutoUpdateDto produto);
        Task<Result<bool>> Delete(long id);
        Task<Result<long>> Inserir(ProdutoInsertDto produto);
        Task<Result<ProdutoDto>> ObterProdutoById(long id);
        Task<Result<IEnumerable<ProdutoDto>>> ObterProdutos();
    }
}
