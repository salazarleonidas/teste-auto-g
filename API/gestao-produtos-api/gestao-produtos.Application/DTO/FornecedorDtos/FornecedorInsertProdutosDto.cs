using System.Text.Json.Serialization;

namespace gestao_produtos.Application.DTO.FornecedorDtos
{
    public class FornecedorInsertProdutosDto
    {
        [JsonPropertyName("codigo_fornecedor")]
        public long? CodigoFornecedor { get; set; }

        [JsonPropertyName("descricao_fornecedor")]
        public string DescricaoFornecedor { get; set; }

        [JsonPropertyName("cnpj_fornecedor")]
        public string CnpjFornecedor { get; set; }
    }
}