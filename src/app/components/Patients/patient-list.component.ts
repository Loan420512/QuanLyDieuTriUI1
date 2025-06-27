import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PatientService, Patient } from '../../services/patient.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-patient-list',
  standalone: true,
  templateUrl: './patient-list.component.html',
  styleUrls: ['./patient-list.component.css'],
  imports: [CommonModule]
})
export class PatientsComponent implements OnInit {
  patients: Patient[] = [];

  constructor(private patientService: PatientService) {}

  ngOnInit(): void {
  const stored = localStorage.getItem('currentUser');
  const doctor = stored ? JSON.parse(stored) : null;

  if (doctor?.userId) {
    this.patientService.getPatientsByDoctor(doctor.userId).subscribe({
      next: (data: Patient[]) => this.patients = data,
      error: (err: any) => console.error('Lỗi tải bệnh nhân:', err)
    });
  }
  }
  // Nếu không có doctorId, tải tất cả bệnh nhân
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
