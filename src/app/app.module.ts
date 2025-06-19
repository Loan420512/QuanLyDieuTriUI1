import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';  // Import FormsModule for ngModel and ngForm
import { RouterModule } from '@angular/router';  // Import RouterModule for routing

import { AppComponent } from './app.component';
import { routes } from './app.routes';  // Make sure your routing configuration is correct
import { HomeComponent } from './components/home/Home.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { PatientsComponent } from './components/Patients/patient-list.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    RegisterComponent,
    LoginComponent,
    PatientsComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,  // Ensure FormsModule is included here
    RouterModule.forRoot(routes),  // Configure RouterModule to use routes
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
