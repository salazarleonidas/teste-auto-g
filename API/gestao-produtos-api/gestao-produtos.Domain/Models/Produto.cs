using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gestao_produtos.Domain.Models
{
    public class Produto
    {
        public long Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Descricao { get; set; }

        public bool Situacao { get; set; } = true;
        public DateTime DataFabricacao { get; set; }
        public DateTime DataValidade { get; set; }

        public virtual List<Fornecedor> Fornecedores { get; set; }

        public Produto() { }

        public Produto(string descricao, bool situacao, DateTime dataFabricacao, DateTime dataValidade, List<Fornecedor> fornecedores)
        {
            Descricao = descricao;
            Situacao = situacao;
            DataFabricacao = dataFabricacao;
            DataValidade = dataValidade;
            Fornecedores = fornecedores;
        }
    }
}
