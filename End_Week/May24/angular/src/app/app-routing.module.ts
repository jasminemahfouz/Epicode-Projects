import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './comp/home/home.component';
import { FiatComponent } from './comp/fiat/fiat.component';
import { FordComponent } from './comp/ford/ford.component';
import { AudiComponent } from './comp/audi/audi.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: '/home',
    pathMatch: 'full',
  },
  {
    path: 'home',
    component: HomeComponent,
    title:'Home'
  },
  {
    path: 'fiat',
    component: FiatComponent,
    title:'fiat'
  },
  {
    path: 'ford',
    component: FordComponent,
    title:'ford'
  },
  {
    path: 'audi',
    component: AudiComponent,
    title:'audi'
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }