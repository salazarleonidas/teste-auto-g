using FluentValidation;
using gestao_produtos.Domain.DTO.ProtudoDtos;

namespace gestao_produtos.Domain.Validators
{
    internal class ProdutoDtoValidator : AbstractValidator<ProdutoUpdateDto>
    {
        public ProdutoDtoValidator() { }
    }
}
