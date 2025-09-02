import { Routes } from '@angular/router';
import { MovimentosListComponent } from './features/movimentos/list/movimentos-list.component';
import { MovimentoFormComponent } from './features/movimentos/form/movimento-form.component';

export const routes: Routes = [
  { path: '', component: MovimentosListComponent },
  { path: 'movimentos/novo', component: MovimentoFormComponent },
  { path: '**', redirectTo: '' }
];
