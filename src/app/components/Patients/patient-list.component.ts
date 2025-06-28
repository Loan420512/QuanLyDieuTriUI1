import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PatientService, Patient } from '../../services/patient.service';
import { ReminderService } from '../../services/reminder.service';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-patient-list',
  standalone: true,
  templateUrl: './patient-list.component.html',
  styleUrls: ['./patient-list.component.css'],
  imports: [CommonModule, FormsModule]
})
export class PatientsComponent implements OnInit {
  patients: Patient[] = [];
  selectedPatient: any;

  reminderForm = {
    medicineTime: '',
    appointmentDate: '',
    method: 'email'
  };

  constructor(
    private patientService: PatientService,
    private reminderService: ReminderService,
    private router: Router
  ) {}

  ngOnInit(): void {
  const stored = localStorage.getItem('currentUser');
  const doctor = stored ? JSON.parse(stored) : null;

  // Kiểm tra xem có đăng nhập và có phải bác sĩ không
  if (!doctor || doctor.role !== 'Doctor') {
    // Không phải bác sĩ thì chuyển về trang không có quyền
    this.router.navigate(['/unauthorized']);
    return;
  }

  // Nếu là bác sĩ thì gọi API lấy danh sách bệnh nhân
  this.patientService.getPatientsByDoctor(doctor.userId).subscribe({
    next: (data: Patient[]) => this.patients = data,
    error: (err: any) => console.error('Lỗi tải bệnh nhân:', err)
  });
  }


  selectPatient(patient: any) {
    this.selectedPatient = patient;
    this.reminderForm = {
      medicineTime: '',
      appointmentDate: '',
      method: 'email'
    };
  }

  sendReminder() {
    if (!this.selectedPatient) return;

    const payload = {
      patientId: this.selectedPatient.id,
      ...this.reminderForm
    };

    this.reminderService.sendReminder(payload).subscribe({
      next: () => {
        alert('Gửi nhắc nhở thành công!');
        this.selectedPatient = null;
      },
      error: () => {
        alert('Gửi nhắc nhở thất bại!');
      }
    });
  }

  loadPatients(): void {
    this.patientService.getPatients().subscribe({
      next: (data) => {
        this.patients = data;
      },
      error: (err) => {
        console.error('Lỗi khi tải danh sách bệnh nhân:', err);
      }
    });
  }

  deletePatient(id: number) {
    this.patientService.deletePatient(id).subscribe(() => {
      this.patients = this.patients.filter(p => p.id !== id);
    });
  }
}
