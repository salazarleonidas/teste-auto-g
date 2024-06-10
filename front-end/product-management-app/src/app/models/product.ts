export interface Supplier {
  codigo?: number;
  descricao: string;
  cnpj: string;
}

export interface Product {
  codigo?: number;
  descricao: string;
  situacao: boolean;
  data_fabricacao: Date;
  data_validade: Date;
  fornecedores: Supplier[];
}