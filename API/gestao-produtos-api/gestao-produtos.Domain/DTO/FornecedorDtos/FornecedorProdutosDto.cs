using System.Text.Json.Serialization;

namespace gestao_produtos.Domain.DTO.FornecedorDtos
{
    public class FornecedorProdutosDto
    {
        [JsonPropertyName("codigo")]
        public long CodigoFornecedor { get; set; }

        [JsonPropertyName("descricao")]
        public string DescricaoFornecedor { get; set; }

        [JsonPropertyName("cnpj")]
        public string CnpjFornecedor { get; set; }
    }
}