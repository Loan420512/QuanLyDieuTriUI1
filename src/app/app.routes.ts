import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { PatientsComponent } from './components/Patients/patient-list.component';
import { TreatmentServiceComponent } from './components/treatment/treatment-service.component';

export const routes: Routes = [
  // Redirect root to /home
  { path: '', redirectTo: '/home', pathMatch: 'full' },

  // ✅ Load standalone HomeComponent
  {
    path: 'home',
    loadComponent: () => import('./components/home/home.component').then(m => m.HomeComponent)

  },

  // ✅ Regular component-based routes
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'patients', component: PatientsComponent },
   { path: 'treatment-service', component: TreatmentServiceComponent },

];
