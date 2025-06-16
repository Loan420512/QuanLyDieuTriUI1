import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-patient-list',
  templateUrl: './patient-list.component.html',
  styleUrls: ['./patient-list.component.css']
})
export class PatientListComponent implements OnInit {
  patients: any[] = [];

  constructor(private apiService: ApiService) {}

  ngOnInit(): void {
    this.apiService.getPatients().subscribe({
      next: (res: any) => {
        this.patients = res;
      },
      error: (err: any)=> {
        console.error('Lỗi tải danh sách bệnh nhân:', err);
      }
    });
  }
}
