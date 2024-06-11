using System.Text.Json.Serialization;

namespace gestao_produtos.Application.DTO.FornecedorDtos
{
    public class FornecedorInsertProdutosDto
    {
        [JsonPropertyName("codigo")]
        public long? CodigoFornecedor { get; set; }

        [JsonPropertyName("descricao")]
        public string DescricaoFornecedor { get; set; }

        [JsonPropertyName("cnpj")]
        public string CnpjFornecedor { get; set; }
    }
}