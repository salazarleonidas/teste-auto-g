using FluentValidation;
using gestao_produtos.Application.DTO.ProtudoDtos;

namespace gestao_produtos.Application.Validators
{
    internal class ProdutoDtoValidator : AbstractValidator<ProdutoUpdateDto>
    {
        public ProdutoDtoValidator() { }
    }
}
