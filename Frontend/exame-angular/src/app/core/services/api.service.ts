import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Movimento, NovoMovimento, Produto, ProdutoCosif } from '../../shared/models';

@Injectable({ providedIn: 'root' })
export class ApiService {
  private http = inject(HttpClient);
  private base = environment.apiBaseUrl;

  listar(mes: number, ano: number){ return this.http.get<Movimento[]>(`${this.base}/movimentos?mes=${mes}&ano=${ano}`); }
  incluir(body: NovoMovimento){ return this.http.post(`${this.base}/movimentos`, body); }
  produtos(){ return this.http.get<Produto[]>(`${this.base}/produtos`); }
  cosifs(codProduto: string){ return this.http.get<ProdutoCosif[]>(`${this.base}/produtos/${codProduto}/cosifs`); }
}
