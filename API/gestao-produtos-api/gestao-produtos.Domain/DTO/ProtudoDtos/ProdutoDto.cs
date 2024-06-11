using gestao_produtos.Domain.DTO.FornecedorDtos;
using gestao_produtos.Domain.Validators;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace gestao_produtos.Domain.DTO.ProtudoDtos
{
    public class ProdutoDto : BaseDto
    {
        [JsonPropertyName("codigo")]
        public long CodigoProduto { get; set; }

        [JsonPropertyName("descricao")]
        public string DescricaoProduto { get; set; }

        [JsonPropertyName("situacao")]
        public bool SituacaoProduto { get; set; }

        [JsonPropertyName("data_fabricacao")]
        public DateTime DataFabricacaoProduto { get; set; }

        [JsonPropertyName("data_validade")]
        public DateTime DataValidadeProduto { get; set; }

        [JsonPropertyName("fornecedores")]
        public IEnumerable<FornecedorProdutosDto> FornecedoresProduto { get; set; }

        public override async Task ValidateAsync() =>
            ValidationResult = await BaseValidator.ValidateAsync<ProdutoDtoValidator, ProdutoDto>(this);
    }
}
