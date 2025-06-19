import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/Home.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { PatientsComponent } from './components/Patients/patient-list.component';

export const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' }, // Redirect to home if no path
  { path: 'home', component: HomeComponent }, // Home route
  { path: 'login', component: LoginComponent }, // Login route
  { path: 'register', component: RegisterComponent }, // Register route
  { path: 'patients', component: PatientsComponent }, // Patients route
];
