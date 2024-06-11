using FluentValidation;
using gestao_produtos.Domain.DTO.ProtudoDtos;

namespace gestao_produtos.Domain.Validators
{
    internal class ProdutoUpdateDtoValidator : AbstractValidator<ProdutoUpdateDto>
    {
        public ProdutoUpdateDtoValidator()
        {
            RuleFor(q => q.CodigoProduto).NotNull().NotEmpty();
            RuleFor(q => q.DescricaoProduto).NotNull().NotEmpty();
            RuleFor(q => q.SituacaoProduto).NotNull().NotEmpty();
            RuleFor(q => q.DataFabricacaoProduto).NotNull().NotEmpty();
            RuleFor(q => q.DataValidadeProduto).NotNull().NotEmpty();

            RuleFor(q => q.DataValidadeProduto >= q.DataFabricacaoProduto);
        }
    }
}
