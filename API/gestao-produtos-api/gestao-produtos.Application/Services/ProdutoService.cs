using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using gestao_produtos.Application.DTO.ProtudoDtos;
using gestao_produtos.Application.Interfaces;
using gestao_produtos.Domain.Models;
using gestao_produtos.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestao_produtos.Application.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper
            ;
        public ProdutoService(IMapper mapper, IProdutoRepository produtoRepository)
            => (_mapper, _produtoRepository) = (mapper, produtoRepository);

        public async Task<Result<bool>> Atualizar(ProdutoUpdateDto produto)
        {
            await produto.ValidateAsync();
            if (!produto.IsValid)
                return Result.Invalid(produto.ValidationResult.AsErrors());

            var obj = _mapper.Map<Produto>(produto);
            var success = await _produtoRepository.AtualizarAsync(obj);

            return success ? Result.Success(success, $"Produto {produto.DescricaoProduto} atualizado com sucesso") :
                             Result.Error($"Erro ao atualizar {produto.DescricaoProduto}");
        }

        public async Task<Result<bool>> Delete(long id)
        {
            var produto = await _produtoRepository.ObterProdutoById(id);
            produto.Situacao = false;
            var result = await _produtoRepository.AtualizarAsync(produto);

            return result ? Result.Success(true, $"Produto removido com sucesso") :
                            Result.Error($"Erro ao remover produto");
        }

        public async Task<Result<long>> Inserir(ProdutoInsertDto produto)
        {
            await produto.ValidateAsync();
            if (!produto.IsValid)
                return Result.Invalid(produto.ValidationResult.AsErrors());

            var obj = _mapper.Map<Produto>(produto);
            var id = await _produtoRepository.InserirAsync(obj);

            return id != 0 ? Result.Success(id, $"Produto {produto.DescricaoProduto} inserido com sucesso") :
                             Result.Error($"Erro ao inserir produto {produto.DescricaoProduto}");
        }

        public async Task<Result<ProdutoDto>> ObterProdutoById(long id)
        {
            var produto = await _produtoRepository.ObterProdutoById(id);
            return produto != null ? Result.Success(_mapper.Map<ProdutoDto>(produto)) :
                                     Result.Error($"Erro ao nuscar produto {produto.Descricao}");
        }

        public async Task<Result<IEnumerable<ProdutoDto>>> ObterProdutos()
        {
            var produtos = await _produtoRepository.ObterProdutos();
            return produtos.Any() ?
                    Result.Success(_mapper.Map<IEnumerable<ProdutoDto>>(produtos)) :
                    Result.NoContent();
        }
    }
}
