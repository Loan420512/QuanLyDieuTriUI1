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
  role: string = '';

  constructor(private doctorService: DoctorService) {}

  ngOnInit(): void {
    const stored = localStorage.getItem('currentUser');
    const user = stored ? JSON.parse(stored) : null;

    if (!user || user.role !== 'Doctor') {
      alert('Không có quyền truy cập.');
      return;
    }

    this.role = user.role;

    // ✅ Gọi API lấy danh sách bác sĩ
    this.doctorService.loadAndGetDoctors().subscribe({
  next: (doctors) => {
    this.doctors = doctors;
    this.doctor = doctors.find(d => d.userId === user.userId);

    if (!this.doctor) {
      alert('⚠️ Không tìm thấy bác sĩ cho tài khoản đăng nhập.');
    }
  },
  error: err => {
    console.error('❌ Lỗi khi tải danh sách bác sĩ:', err);
    alert('Không thể tải danh sách bác sĩ.');
  }
  });

  }

  selectDoctor(doctor: any): void {
    this.doctor = doctor;
  }
}
