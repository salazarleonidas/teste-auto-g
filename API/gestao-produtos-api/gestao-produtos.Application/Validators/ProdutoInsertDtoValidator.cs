using FluentValidation;
using gestao_produtos.Application.DTO.ProtudoDtos;

namespace gestao_produtos.Application.Validators
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
