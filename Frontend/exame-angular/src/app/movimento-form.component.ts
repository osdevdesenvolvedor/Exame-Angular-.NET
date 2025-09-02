import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService, NovoMovimento } from './api.service';
import { Router } from '@angular/router';

@Component({
  standalone: true,
  selector: 'app-movimento-form',
  imports: [CommonModule, FormsModule],
  template: `
    <div class="card">
      <h2>Novo Movimento</h2>
      <form (ngSubmit)="salvar()">
        <div class="actions">
          <div><label>Mês</label><input type="number" [(ngModel)]="form.DAT_MES" name="mes" min="1" max="12" required /></div>
          <div><label>Ano</label><input type="number" [(ngModel)]="form.DAT_ANO" name="ano" min="1900" required /></div>
          <div>
            <label>Produto</label>
            <select [(ngModel)]="form.COD_PRODUTO" name="produto" (change)="onProduto()" required>
              <option value="" disabled selected>Selecione...</option>
              <option *ngFor="let p of produtos" [value]="p.COD_PRODUTO">{{p.DES_PRODUTO}} ({{p.COD_PRODUTO}})</option>
            </select>
          </div>
          <div>
            <label>Cosif</label>
            <select [(ngModel)]="form.COD_COSIF" name="cosif" required>
              <option value="" disabled selected>Selecione...</option>
              <option *ngFor="let c of cosifs" [value]="c.COD_COSIF">{{c.COD_COSIF}} ({{c.COD_CLASSIFICACAO}})</option>
            </select>
          </div>
          <div><label>Valor</label><input type="number" step="0.01" [(ngModel)]="form.VAL_VALOR" name="valor" required /></div>
          <div style="flex-basis:100%"><label>Descrição</label><input type="text" [(ngModel)]="form.DES_DESCRICAO" name="desc" maxlength="50" required style="width:100%"/></div>
        </div>
        <div class="actions" style="margin-top:12px">
          <button type="submit">Salvar</button>
          <button type="button" (click)="voltar()">Cancelar</button>
        </div>
        <p *ngIf="msg" style="color:#34d399">{{msg}}</p>
        <p *ngIf="erro" style="color:#f87171">{{erro}}</p>
      </form>
    </div>
  `
})
export class MovimentoFormComponent {
  private api = inject(ApiService);
  private router = inject(Router);
  form: NovoMovimento = { DAT_MES:new Date().getMonth()+1, DAT_ANO:new Date().getFullYear(), COD_PRODUTO:'', COD_COSIF:'', VAL_VALOR:0, DES_DESCRICAO:'' };
  produtos:any[]=[]; cosifs:any[]=[]; msg=''; erro='';

  constructor(){ this.api.produtos().subscribe(p => this.produtos = p as any[]); }
  onProduto(){ this.form.COD_COSIF=''; this.form.COD_PRODUTO ? this.api.cosifs(this.form.COD_PRODUTO).subscribe(c => this.cosifs = c as any[]) : this.cosifs=[]; }
  salvar(){ this.msg=''; this.erro=''; this.api.incluir(this.form).subscribe({ next: _ => { this.msg='Incluído!'; setTimeout(()=> this.router.navigate(['/']), 600); }, error: e => this.erro = e?.error || 'Falha ao incluir' }); }
  voltar(){ this.router.navigate(['/']); }
}
