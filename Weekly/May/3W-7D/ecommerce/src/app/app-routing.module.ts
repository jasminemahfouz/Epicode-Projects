import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomepageComponent } from './pages/homepage/homepage.component';
import { FavoritesComponent } from './pages/favorites/favorites.component';

const routes: Routes = [
  {
    path: "",
    component: HomepageComponent
  },
  {
    path: "fav",
    component: FavoritesComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
