using gestao_produtos.Application.DTO.FornecedorDtos;
using gestao_produtos.Application.Validators;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace gestao_produtos.Application.DTO.ProtudoDtos
{
    public class ProdutoInsertDto : BaseDto
    {
        [JsonPropertyName("descricao_produto")]
        public string DescricaoProduto { get; set; }

        [JsonPropertyName("situacao_produto")]
        public bool SituacaoProduto { get; set; }

        [JsonPropertyName("data_fabricacao_produto")]
        public DateTime DataFabricacaoProduto { get; set; }

        [JsonPropertyName("data_validade_produto")]
        public DateTime DataValidadeProduto { get; set; }

        [JsonPropertyName("fornecedores_produto")]
        public IEnumerable<FornecedorInsertProdutosDto> FornecedoresProduto { get; set; }

        public override async Task ValidateAsync()
            => ValidationResult = await BaseValidator.ValidateAsync<ProdutoInsertDtoValidator, ProdutoInsertDto>(this);
    }
}
