import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { PatientsComponent } from './components/Patients/patient-list.component';
import { TreatmentServiceComponent } from './components/treatment/treatment-service.component';
import { MemberProfileComponent } from './components/member/member-profile.component';
import { DoctorDashboardComponent } from './components/doctor/doctor-dashboard.component';
import { AuthGuard } from './services/auth.guard';


export const routes: Routes = [
  // Redirect root to /home
  { path: '', redirectTo: '/home', pathMatch: 'full' },

  // ✅ Load standalone HomeComponent

  {
    path: 'home',
    loadComponent: () => import('./components/home/home.component').then(m => m.HomeComponent)

  },
   {
    path: 'login',
    loadComponent: () =>
      import('./components/login/login.component').then(m => m.LoginComponent)
  },
  {
    path: 'member-profile/:id',
    loadComponent: () =>
      import('./components/member/member-profile.component').then(m => m.MemberProfileComponent)
  },

  {
  path: 'bookings',
  loadComponent: () =>
    import('./components/booking/booking-list.component').then(m => m.BookingListComponent),
  canActivate: [AuthGuard]
  },
  {
  path: 'reminder',
  loadComponent: () => import('./components/reminder/reminder.component').then(m => m.ReminderComponent)
  },
  {path: 'feedback', loadComponent: () => import('./components/feedback/feedback-list.component').then(m => m.FeedbackListComponent) },
  {path: 'treatment-service', loadComponent: () => import('./components/treatment/treatment-service.component').then(m => m.TreatmentServiceComponent) },
  {path: 'doctor-dashboard', loadComponent: () => import('./components/doctor/doctor-dashboard.component').then(m => m.DoctorDashboardComponent) },

  // ✅ Regular component-based routes
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'patients', component: PatientsComponent },
   { path: 'treatment-service', component: TreatmentServiceComponent },
   { path: 'member-profile/:id', component: MemberProfileComponent },

  {
  path: 'doctor-profile/:id',
  component: DoctorDashboardComponent,
  canActivate: [AuthGuard]
  }




];
