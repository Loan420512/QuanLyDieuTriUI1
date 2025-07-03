import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

interface TreatmentService {
  id: number;
  name: string;
}

@Component({
  selector: 'app-treatment-service',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './treatment-service.component.html',
  styleUrls: ['./treatment-service.component.css']
})
export class TreatmentServiceComponent implements OnInit {
  services: TreatmentService[] = [];
  newServiceName: string = '';
  errorMessage = '';

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.loadServices();
  }

  loadServices() {
    const token = localStorage.getItem('token');
    const headers = {
      headers: {
        Authorization: `Bearer ${token}`
      }
    };

    this.http.get<TreatmentService[]>('https://localhost:7240/api/TreatmentService', headers)
      .subscribe({
        next: (data) => {
          this.services = data;
        },
        error: (err) => {
          console.error('Lỗi khi tải dịch vụ:', err);
          this.errorMessage = 'Không thể tải danh sách dịch vụ.';
        }
      });
  }

  addService() {
    if (!this.newServiceName.trim()) {
      this.errorMessage = 'Tên dịch vụ không được để trống.';
      return;
    }

    const token = localStorage.getItem('token');
    const headers = {
      headers: {
        Authorization: `Bearer ${token}`,
        'Content-Type': 'application/json'
      }
    };

    const newService = {
      id: 0,
      name: this.newServiceName.trim()
    };

    this.http.post('https://localhost:7240/api/TreatmentService', newService, headers)
      .subscribe({
        next: () => {
          this.newServiceName = '';
          this.loadServices();
        },
        error: (err) => {
          console.error('Lỗi khi thêm dịch vụ:', err);
          this.errorMessage = 'Không thể thêm dịch vụ.';
        }
      });
  }
}
