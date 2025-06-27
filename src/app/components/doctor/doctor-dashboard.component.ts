import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DoctorService } from '../../services/doctor.service';
import { PatientsComponent } from '../Patients/patient-list.component';

@Component({
  selector: 'app-doctor-dashboard',
  standalone: true,
  imports: [CommonModule, PatientsComponent],
  templateUrl: './doctor-dashboard.component.html',
  styleUrls: ['./doctor-dashboard.component.css']
})
export class DoctorDashboardComponent implements OnInit {
  doctor: any = null; // ✅ phải có
  todaySchedule: any[] = []; // ✅ phải có
  activeCases: any[] = []; // ✅ phải có

  constructor(private doctorService: DoctorService) {}

  ngOnInit(): void {
    // Dữ liệu mẫu (có thể gọi từ API sau)
    this.doctor = {
      name: 'Dr.Nguyen',
      specialization: 'IVF Specialist',
      degrees: 'MD, PhD',
      email: 'dr.nguyen@example.com',
      phone: '0901-234-567',
      experience: '15 years',
      skills: ['IVF', 'ICSI', 'Fertility Counseling']
    };

    this.todaySchedule = [
      { time: '09:00', patient: 'Lê Thị Nga', type: 'Consultation', status: 'Confirmed' },
      { time: '11:00', patient: 'Nguyễn Văn A', type: 'Follow-up', status: 'Pending' }
    ];

    this.activeCases = [
      { name: 'Lê Thị Nga', treatment: 'IVF', progress: '40%', next: '27/06/2025' },
      { name: 'Nguyễn Văn A', treatment: 'Hormone Therapy', progress: '70%', next: '30/06/2025' }
    ];
  }
}
