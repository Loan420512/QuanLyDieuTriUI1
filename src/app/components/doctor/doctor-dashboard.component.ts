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
  role: string = '';
  ngOnInit(): void {
  const stored = localStorage.getItem('currentUser');
  const user = stored ? JSON.parse(stored) : null;
    
  if (!user || user.role !== 'Doctor') {
    alert('Không có quyền truy cập.');
    return;
  }

  this.role = user.role;

  // Dữ liệu bác sĩ giả lập
  this.doctors = [
    {
      id: 1,
      name: 'Dr.Nguyen',
      specialization: 'IVF Specialist & Reproductive Endocrinologist',
      degrees: 'MD, PhD',
      email: 'dr.nguyen@example.com',
      phone: '0901-234-567',
      experience: '15 years',
      skills: ['IVF', 'ICSI', 'Fertility Counseling'],
      bio: 'Over 15 years of experience helping couples achieve their dream of parenthood through personalized IVF treatments.'
    },
    {
      id: 2,
      name: 'Dr.Le',
      specialization: 'Male Fertility & Andrology Expert',
      degrees: 'MD',
      email: 'dr.le@example.com',
      phone: '0902-888-999',
      experience: '10 years',
      skills: ['Semen Analysis', 'Male Fertility'],
      bio: 'Specializing in male infertility diagnosis and treatments including surgical sperm retrieval and hormone therapy.'
    },
    {
      id: 3,
      name: 'Dr.Tran',
      specialization: 'Fertility Counselor & Psychologist',
      degrees: 'PhD',
      email: 'dr.tran@example.com',
      phone: '0903-777-111',
      experience: '12 years',
      skills: ['Emotional Counseling', 'Decision Support'],
      bio: 'Supports patients with emotional wellness and decision-making throughout their fertility journey.'
    },
    {
      id: 4,
      name: 'Dr.Pham',
      specialization: 'Embryologist & Lab Director',
      degrees: 'MSc, PhD',
      email: 'dr.pham@example.com',
      phone: '0904-123-456',
      experience: '18 years',
      skills: ['Embryo Culture', 'ICSI', 'Cryopreservation'],
      bio: 'Leads the IVF lab with expertise in embryo culture, ICSI, and cryopreservation technology.'
    }
  ];

  // Tìm bác sĩ tương ứng với ID người đăng nhập
  this.doctor = this.doctors.find(d => d.id === user.userId);

  if (this.doctor) {
    this.selectDoctor(this.doctor);
  } else {
    alert('Không tìm thấy thông tin bác sĩ.');
  }
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
