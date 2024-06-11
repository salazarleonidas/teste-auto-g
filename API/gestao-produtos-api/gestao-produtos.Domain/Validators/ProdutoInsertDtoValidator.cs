using FluentValidation;
using gestao_produtos.Domain.DTO.ProtudoDtos;

namespace gestao_produtos.Domain.Validators
{
    internal class ProdutoInsertDtoValidator : AbstractValidator<ProdutoInsertDto>
    {
        public ProdutoInsertDtoValidator()
        {
            RuleFor(q => q.DescricaoProduto).NotNull().NotEmpty();
            RuleFor(q => q.SituacaoProduto).NotNull().NotEmpty();
            RuleFor(q => q.DataFabricacaoProduto).NotNull().NotEmpty();
            RuleFor(q => q.DataValidadeProduto).NotNull().NotEmpty();

            RuleFor(q => q.DataValidadeProduto >= q.DataFabricacaoProduto);
        }
    }
}
