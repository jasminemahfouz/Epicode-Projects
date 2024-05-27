import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './comp/home/home.component';
import { FooterComponent } from './comp/footer/footer.component';
import { NavbarComponent } from './comp/navbar/navbar.component';
import { FiatComponent } from './comp/fiat/fiat.component';
import { AudiComponent } from './comp/audi/audi.component';
import { FordComponent } from './comp/ford/ford.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    FooterComponent,
    NavbarComponent,
    FiatComponent,
    AudiComponent,
    FordComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
