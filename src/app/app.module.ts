import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { routes } from './app.routes';

import { HomeComponent } from './components/Home/Home.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { PatientsComponent } from './components/Patients/patient-list.component';

@NgModule({
  declarations: [],
  imports: [
    BrowserModule,
    RouterModule.forRoot(routes),
    AppComponent,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    PatientsComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
