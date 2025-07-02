import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DoctorService } from '../../services/doctor.service';

@Component({
  selector: 'app-doctor-profile',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './doctor-profile.component.html',
  styleUrls: ['./doctor-profile.component.css']
})
export class DoctorProfileComponent implements OnInit {
  doctor: any = null;
  patients: any[] = [];

  constructor(private doctorService: DoctorService) {}

  ngOnInit(): void {
    const stored = localStorage.getItem('currentUser');
    const user = stored ? JSON.parse(stored) : null;

    if (!user || user.role !== 'Doctor') {
      alert('Bạn không có quyền truy cập.');
      return;
    }

    // Bác sĩ hiện tại
    this.doctor = {
      id: user.userId,
      name: user.fullname,
      email: user.email
    };

    // Gọi API lấy danh sách bệnh nhân của bác sĩ
    this.doctorService.getPatientsByDoctorId(user.userId).subscribe(data => {
      this.patients = data;
    });
  }
}
