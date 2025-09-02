import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';

export interface Movimento {
  dat_MES: number; dat_ANO: number; num_LANCAMENTO: number;
  cod_PRODUTO: string; cod_COSIF: string; val_VALOR: number;
  des_DESCRICAO: string; dat_MOVIMENTO: string; cod_USUARIO: string;
  des_PRODUTO: string; cod_CLASSIFICACAO: string;
}
export interface NovoMovimento {
  DAT_MES: number; DAT_ANO: number; COD_PRODUTO: string; COD_COSIF: string; VAL_VALOR: number; DES_DESCRICAO: string;
}

@Injectable({ providedIn: 'root' })
export class ApiService {
  private http = inject(HttpClient);
  private base = environment.apiBaseUrl;
  listar(mes: number, ano: number){ return this.http.get<Movimento[]>(`${this.base}/api/movimentos?mes=${mes}&ano=${ano}`); }
  incluir(body: NovoMovimento){ return this.http.post(`${this.base}/api/movimentos`, body); }
  produtos(){ return this.http.get<{COD_PRODUTO:string; DES_PRODUTO:string}[]>(`${this.base}/api/produtos`); }
  cosifs(codProduto: string){ return this.http.get<{COD_PRODUTO:string; COD_COSIF:string; COD_CLASSIFICACAO:string}[]>(`${this.base}/api/produtos/${codProduto}/cosifs`); }
}
