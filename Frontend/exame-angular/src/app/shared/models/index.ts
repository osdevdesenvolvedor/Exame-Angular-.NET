export interface Movimento {
  dat_MES: number; dat_ANO: number; num_LANCAMENTO: number;
  cod_PRODUTO: string; cod_COSIF: string; val_VALOR: number;
  des_DESCRICAO: string; dat_MOVIMENTO: string; cod_USUARIO: string;
  des_PRODUTO: string; cod_CLASSIFICACAO: string;
}
export interface NovoMovimento {
  DAT_MES: number; DAT_ANO: number; COD_PRODUTO: string; COD_COSIF: string; VAL_VALOR: number; DES_DESCRICAO: string;
}
export interface Produto { COD_PRODUTO: string; DES_PRODUTO: string; }
export interface ProdutoCosif { COD_PRODUTO: string; COD_COSIF: string; COD_CLASSIFICACAO: string; }
