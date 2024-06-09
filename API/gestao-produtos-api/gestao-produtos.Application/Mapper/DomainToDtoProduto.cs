using AutoMapper;
using gestao_produtos.Application.DTO.FornecedorDtos;
using gestao_produtos.Application.DTO.ProtudoDtos;
using gestao_produtos.Domain.Models;
using System.Text.RegularExpressions;

namespace gestao_produtos.Application.Mapper
{
    public class DomainToDtoProduto : Profile
    {
        public DomainToDtoProduto()
        {
            CreateMap<Produto, ProdutoDto>(MemberList.Source)
                .ForMember(q => q.CodigoProduto, opt => opt.MapFrom(src => src.Id))
                .ForMember(q => q.DescricaoProduto, opt => opt.MapFrom(src => src.Descricao))
                .ForMember(q => q.SituacaoProduto, opt => opt.MapFrom(src => src.Situacao))
                .ForMember(q => q.DataFabricacaoProduto, opt => opt.MapFrom(src => src.DataFabricacao))
                .ForMember(q => q.DataValidadeProduto, opt => opt.MapFrom(src => src.DataValidade))
                .ForMember(q => q.FornecedoresProduto, opt => opt.MapFrom(src => src.Fornecedores))
                .ReverseMap();

            CreateMap<Produto, ProdutoInsertDto>(MemberList.Source)
                .ForMember(q => q.DescricaoProduto, opt => opt.MapFrom(src => src.Descricao))
                .ForMember(q => q.SituacaoProduto, opt => opt.MapFrom(src => src.Situacao))
                .ForMember(q => q.DataFabricacaoProduto, opt => opt.MapFrom(src => src.DataFabricacao))
                .ForMember(q => q.DataValidadeProduto, opt => opt.MapFrom(src => src.DataValidade))
                .ForMember(q => q.FornecedoresProduto, opt => opt.MapFrom(src => src.Fornecedores))
                .ReverseMap();

            CreateMap<Produto, ProdutoUpdateDto>(MemberList.Source)
                .ForMember(q => q.CodigoProduto, opt => opt.MapFrom(src => src.Id))
                .ForMember(q => q.DescricaoProduto, opt => opt.MapFrom(src => src.Descricao))
                .ForMember(q => q.SituacaoProduto, opt => opt.MapFrom(src => src.Situacao))
                .ForMember(q => q.DataFabricacaoProduto, opt => opt.MapFrom(src => src.DataFabricacao))
                .ForMember(q => q.DataValidadeProduto, opt => opt.MapFrom(src => src.DataValidade))
                .ForMember(q => q.FornecedoresProduto, opt => opt.MapFrom(src => src.Fornecedores))
                .ReverseMap();

            CreateMap<Fornecedor, FornecedorProdutosDto>(MemberList.Source)
                .ForMember(q => q.CodigoFornecedor, opt => opt.MapFrom(src => src.Id))
                .ForMember(q => q.DescricaoFornecedor, opt => opt.MapFrom(src => src.Descricao))
                .ForMember(q => q.CnpjFornecedor, opt => opt.MapFrom(src => src.Cnpj));

            CreateMap<Fornecedor, FornecedorInsertProdutosDto>(MemberList.Source)
                .ForMember(q => q.CodigoFornecedor, opt => opt.MapFrom(src => src.Id))
                .ForMember(q => q.DescricaoFornecedor, opt => opt.MapFrom(src => src.Descricao))
                .ForMember(q => q.CnpjFornecedor, opt => opt.MapFrom(src => src.Cnpj));

            CreateMap<Fornecedor, FornecedorUpdateProdutosDto>(MemberList.Source)
                .ForMember(q => q.CodigoFornecedor, opt => opt.MapFrom(src => src.Id))
                .ForMember(q => q.DescricaoFornecedor, opt => opt.MapFrom(src => src.Descricao))
                .ForMember(q => q.CnpjFornecedor, opt => opt.MapFrom(src => src.Cnpj));


            CreateMap<FornecedorProdutosDto, Fornecedor>(MemberList.Source)
                .ForMember(q => q.Id, opt => opt.MapFrom(src => src.CodigoFornecedor))
                .ForMember(q => q.Descricao, opt => opt.MapFrom(src => src.DescricaoFornecedor))
                .AfterMap((s, d) => d.Cnpj = Regex.Replace(s.CnpjFornecedor, "[^0-9]", string.Empty));

            CreateMap<FornecedorInsertProdutosDto, Fornecedor>(MemberList.Source)
                .ForMember(q => q.Id, opt => opt.MapFrom(src => src.CodigoFornecedor))
                .ForMember(q => q.Descricao, opt => opt.MapFrom(src => src.DescricaoFornecedor))
                .AfterMap((s, d) => d.Cnpj = Regex.Replace(s.CnpjFornecedor, "[^0-9]", string.Empty));

            CreateMap<FornecedorUpdateProdutosDto, Fornecedor>(MemberList.Source)
                .ForMember(q => q.Id, opt => opt.MapFrom(src => src.CodigoFornecedor))
                .ForMember(q => q.Descricao, opt => opt.MapFrom(src => src.DescricaoFornecedor))
                .AfterMap((s, d) => d.Cnpj = Regex.Replace(s.CnpjFornecedor, "[^0-9]", string.Empty));
        }
    }
}
