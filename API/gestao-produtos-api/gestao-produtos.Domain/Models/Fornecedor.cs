using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gestao_produtos.Domain.Models
{
    public class Fornecedor
    {
        [Key]
        public long Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Descricao { get; set; }
        public string Cnpj { get; set; }
        public virtual List<Produto> Produtos { get; set; }

        public Fornecedor() { }

        public Fornecedor(string descricao, string cnpj, List<Produto> produtos)
        {
            Descricao = descricao;
            Cnpj = cnpj;
            Produtos = produtos;
        }
    }
}
