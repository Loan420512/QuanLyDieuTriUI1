import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';


import { AppComponent } from './app.component';
import { routes } from './app.routes';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { PatientsComponent } from './components/Patients/patient-list.component';
import { FeedbackListComponent } from './components/feedback/feedback-list.component';
import { MemberProfileComponent } from './components/member/member-profile.component';
import { CommonModule } from '@angular/common';
import { ReminderComponent } from './components/reminder/reminder.component';
import { DoctorDashboardComponent } from './components/doctor/doctor-dashboard.component';
import { BookingListComponent } from './components/booking/booking-list.component';




@NgModule({
  declarations: [
  ],
  imports: [
    BrowserModule,
    FormsModule,
    RouterModule.forRoot(routes),
    BookingListComponent,
    AppComponent,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    PatientsComponent,
    FeedbackListComponent,
     CommonModule,
    MemberProfileComponent,
    ReminderComponent,
    DoctorDashboardComponent
  ],
  providers: [],
})
export class AppModule {}
