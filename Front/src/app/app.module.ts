import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';  // Para formulários reativos
import { MatFormFieldModule } from '@angular/material/form-field';  // Para mat-form-field
import { MatInputModule } from '@angular/material/input';  // Para mat-input
import { MatErrorModule } from '@angular/material/core';  // Para mat-error
import { RouterModule } from '@angular/router';  // Para router-outlet
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './Pages/login/login.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,  // Para formulários reativos
    MatFormFieldModule,
    MatInputModule,
    MatErrorModule,
    RouterModule  // Para router-outlet
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
