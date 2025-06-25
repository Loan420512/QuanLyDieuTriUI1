import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-treatment-service',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './treatment-service.component.html',
})
export class TreatmentServiceComponent implements OnInit {
  services: any[] = [];
  formData: any = {};
  isEditing: boolean = false;

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.getAll();
  }

  getAll() {
    this.http.get<any[]>('https://localhost:5001/api/TreatmentService')
      .subscribe(data => this.services = data);
  }

  onSubmit() {
    if (this.isEditing) {
      this.http.put(`https://localhost:5001/api/TreatmentService/${this.formData.serviceID}`, this.formData)
        .subscribe(() => {
          this.getAll();
          this.resetForm();
        });
    } else {
      this.http.post('https://localhost:5001/api/TreatmentService', this.formData)
        .subscribe(() => {
          this.getAll();
          this.resetForm();
        });
    }
  }

  edit(service: any) {
    this.formData = { ...service };
    this.isEditing = true;
  }

  delete(id: number) {
    if (confirm('Bạn có chắc chắn muốn xóa dịch vụ này?')) {
      this.http.delete(`https://localhost:5001/api/TreatmentService/${id}`)
        .subscribe(() => this.getAll());
    }
  }

  resetForm() {
    this.formData = {};
    this.isEditing = false;
  }
}
