import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PatientService, Patient } from '../../services/patient.service';

@Component({
  selector: 'app-patient',
  standalone: true,
  templateUrl: './patient-list.component.html',
  styleUrls: ['./patient-list.component.css'],
  imports: [CommonModule]
})
export class PatientsComponent implements OnInit {
  patients: Patient[] = [];

  constructor(private patientService: PatientService) {}

  ngOnInit(): void {
    this.loadPatients();
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
