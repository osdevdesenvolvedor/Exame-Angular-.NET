import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  standalone: true,
  template: `
  <div class="container">
    <h1>Exame Angular — APP Organizado</h1>
    <div class="actions">
      <a routerLink="/" class="btn">Movimentos</a>
      <a routerLink="/movimentos/novo" class="btn">Novo Movimento</a>
    </div>
    <router-outlet></router-outlet>
  </div>
  `
})
export class AppComponent {}
