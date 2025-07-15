import { Component, Input } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-reminder',
  standalone: true, // ✅ quan trọng
  imports: [CommonModule, FormsModule],
  templateUrl: './reminder.component.html'
})
export class ReminderComponent {
  @Input() patient: any;

  reminderForm = {
    medicineTime: '',
    appointmentDate: '',
    method: 'email'
  };

  constructor(private http: HttpClient, private authService: AuthService) {}

  sendReminder() {
    const payload = {
      patientId: this.patient.patientId,
      ...this.reminderForm
      
    };

    this.http.post('https://localhost:7240/api/Notification/create-notification', payload, {
      headers: new HttpHeaders({
        Authorization: `Bearer ${this.authService.getToken()}`
      })
    }).subscribe({
      next: () => alert('Đã gửi nhắc nhở thành công!'),
      error: () => alert('Lỗi khi gửi nhắc nhở.')
    });
  }
}
