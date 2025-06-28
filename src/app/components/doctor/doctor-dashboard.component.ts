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
  doctors: any[] = []; // ✅ Danh sách nhiều bác sĩ
  doctor: any = null;  // ✅ Bác sĩ đang được chọn
  todaySchedule: any[] = [];
  activeCases: any[] = [];

  constructor(private doctorService: DoctorService) {}

  ngOnInit(): void {
    // Danh sách giả định, có thể load từ API
    this.doctors = [
      {
        id: 1,
        name: 'Dr.Nguyen',
        specialization: 'IVF Specialist',
        degrees: 'MD, PhD',
        email: 'dr.nguyen@example.com',
        phone: '0901-234-567',
        experience: '15 years',
        skills: ['IVF', 'ICSI', 'Fertility Counseling']
      },
      {
        id: 2,
        name: 'Dr.Le',
        specialization: 'Andrology Expert',
        degrees: 'MD',
        email: 'dr.le@example.com',
        phone: '0902-888-999',
        experience: '10 years',
        skills: ['Semen Analysis', 'Male Fertility']
      }
    ];

    this.selectDoctor(this.doctors[0]); // Mặc định chọn bác sĩ đầu tiên
  }

  selectDoctor(doctor: any): void {
    this.doctor = doctor;

    // Lấy lịch và ca đang hoạt động tương ứng (giả lập)
    if (doctor.name === 'Dr.Nguyen') {
      this.todaySchedule = [
        { time: '09:00', patient: 'Lê Thị Nga', type: 'Consultation', status: 'Confirmed' },
        { time: '11:00', patient: 'Nguyễn Văn A', type: 'Follow-up', status: 'Pending' }
      ];

      this.activeCases = [
        { name: 'Lê Thị Nga', treatment: 'IVF', progress: '40%', next: '27/06/2025' },
        { name: 'Nguyễn Văn A', treatment: 'Hormone Therapy', progress: '70%', next: '30/06/2025' }
      ];
    } else {
      this.todaySchedule = [
        { time: '10:00', patient: 'Trần Văn B', type: 'Andrology Check', status: 'Confirmed' }
      ];

      this.activeCases = [
        { name: 'Trần Văn B', treatment: 'Semen Analysis', progress: '30%', next: '01/07/2025' }
      ];
    }
  }
}
