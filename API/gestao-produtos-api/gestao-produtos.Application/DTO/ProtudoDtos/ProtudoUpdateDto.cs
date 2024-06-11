using gestao_produtos.Application.DTO.FornecedorDtos;
using gestao_produtos.Application.Validators;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace gestao_produtos.Application.DTO.ProtudoDtos
{
    public class ProdutoUpdateDto : BaseDto
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
        public IEnumerable<FornecedorUpdateProdutosDto> FornecedoresProduto { get; set; }

        public override async Task ValidateAsync()
            => ValidationResult = await BaseValidator.ValidateAsync<ProdutoUpdateDtoValidator, ProdutoUpdateDto>(this);
    }
}
