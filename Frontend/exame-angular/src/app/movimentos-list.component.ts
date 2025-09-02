import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService } from './api.service';

@Component({
  standalone: true,
  selector: 'app-movimentos-list',
  imports: [CommonModule, FormsModule],
  template: `
    <div class="card">
      <h2>Movimentos ({{mes}}/{{ano}})</h2>
      <div class="actions">
        <div><label>Mês</label><input type="number" min="1" max="12" [(ngModel)]="mes" (change)="carregar()"/></div>
        <div><label>Ano</label><input type="number" min="1900" [(ngModel)]="ano" (change)="carregar()"/></div>
      </div>
      <table class="grid" *ngIf="dados().length">
        <thead><tr><th>#</th><th>Produto</th><th>Cosif (Class.)</th><th>Valor</th><th>Descrição</th><th>Data</th><th>Usuário</th></tr></thead>
        <tbody>
          <tr *ngFor="let m of dados(); trackBy: track">
            <td>{{m.num_LANCAMENTO}}</td>
            <td>{{m.des_PRODUTO}} ({{m.cod_PRODUTO}})</td>
            <td>{{m.cod_COSIF}} ({{m.cod_CLASSIFICACAO}})</td>
            <td>{{m.val_VALOR | number:'1.2-2'}}</td>
            <td>{{m.des_DESCRICAO}}</td>
            <td>{{m.dat_MOVIMENTO | date:'dd/MM/yyyy HH:mm'}}</td>
            <td>{{m.cod_USUARIO}}</td>
          </tr>
        </tbody>
      </table>
      <p *ngIf="!dados().length">Nenhum movimento encontrado.</p>
    </div>
  `
})
export class MovimentosListComponent {
  private api = inject(ApiService);
  dados = signal<any[]>([]);
  mes = new Date().getMonth()+1;
  ano = new Date().getFullYear();

  constructor(){ this.carregar(); }
  track = (_:number, m:any) => `${m.dat_ANO}-${m.dat_MES}-${m.num_LANCAMENTO}`;
  carregar(){ this.api.listar(this.mes, this.ano).subscribe(d => this.dados.set(d as any[])); }
}
