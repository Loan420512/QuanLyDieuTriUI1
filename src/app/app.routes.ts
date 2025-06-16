import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/Home/Home.component';
import { RegisterComponent } from './components/register/register.component';
import { PatientListComponent } from './components/patient-list/patient-list.component';

export const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'home', component: HomeComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'patients', component: PatientListComponent }
];
