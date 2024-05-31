import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TodoComponent } from '../app/components/todo/todo.component';
import { CompletedComponent } from '../app/components/completed/completed.component';
import { UserComponent } from '../app/components/user/user.component';
import { UncompletedComponent } from './components/uncompleted/uncompleted.component';

const routes: Routes = [
  { path: '', component: TodoComponent, title:'home' },
  { path: 'completed', component: CompletedComponent,title:'completed' },
  { path: 'users', component: UserComponent,title:'users' },
  { path: 'uncompleted', component: UncompletedComponent, title:'uncompleted' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
