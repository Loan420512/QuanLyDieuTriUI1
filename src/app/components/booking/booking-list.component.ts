import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

interface Booking {
  bookingId: number;
  dayBooking: string;
  treatmentServiceId: number;
  memberId: number;
  doctorId: number;
  statusBooking: string;
  createAt: string;
}

interface Doctor {
  infoId: number;
  fullName: string;
  speciality: string;
  degree: string;
  phoneNumber: string;
  experianYear: number;
  certificate: string;
  userId: number;
}

interface Member {
  id: number;
  username: string;
}

interface TreatmentService {
  id: number;
  name: string;
}

@Component({
  selector: 'app-booking-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './booking-list.component.html',
  styleUrls: ['./booking-list.component.css']
})
export class BookingListComponent implements OnInit {
  member: Member = { id: 1, username: 'Khách hàng A' };

  newBooking = {
    dayBooking: '',
    treatmentServiceId: 0,
    memberId: 0,
    doctorId: 0,
    statusBooking: 'Pending',
    createAt: ''
  };

  doctors: Doctor[] = [];
  services: TreatmentService[] = [];
  errorMessage = '';
  successMessage = '';
  role: string = '';

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    const userJson = localStorage.getItem('currentUser');
    const user = userJson ? JSON.parse(userJson) : null;
    this.role = user?.role || '';
    this.loadData();
  }

  loadData(): void {
    const token = localStorage.getItem('token');
    const headers = token ? { headers: new HttpHeaders({ Authorization: `Bearer ${token}` }) } : {};

    this.http.get<Doctor[]>('https://localhost:7240/api/InfoDoctor', headers).subscribe({
      next: data => this.doctors = Array.isArray(data) ? data : data['data'] || [],
      error: err => this.errorMessage = 'Lỗi tải bác sĩ: ' + err.message
    });

    this.http.get<TreatmentService[]>('https://localhost:7240/api/TreatmentService', headers).subscribe({
      next: data => this.services = Array.isArray(data) ? data : data['data'] || [],
      error: err => this.errorMessage = 'Lỗi tải dịch vụ: ' + err.message
    });
  }

  createBooking(): void {
    this.errorMessage = '';
    this.successMessage = '';

    const userJson = localStorage.getItem('currentUser');
    const user = userJson ? JSON.parse(userJson) : null;

    if (!user || !user.userId) {
      this.errorMessage = 'Không tìm thấy người dùng đăng nhập.';
      return;
    }

    // Nếu role là Doctor → tự động chọn doctorId
    if (this.role === 'Doctor') {
      const doctor = this.doctors.find(d => d.userId === user.userId);
      if (!doctor) {
        this.errorMessage = 'Không tìm thấy bác sĩ tương ứng với tài khoản.';
        return;
      }
      this.newBooking.doctorId = doctor.infoId;
    }

    // Kiểm tra dữ liệu
    if (!this.newBooking.dayBooking || this.newBooking.treatmentServiceId === 0 || this.newBooking.doctorId === 0) {
      this.errorMessage = 'Vui lòng điền đầy đủ thông tin.';
      return;
    }

    const token = localStorage.getItem('token');
    const headers = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${token}`,
        'Content-Type': 'application/json'
      })
    };

    const bookingPayload = {
      bookingId: 0,
      dayBooking: new Date(this.newBooking.dayBooking).toISOString(),
      treatmentServiceId: this.newBooking.treatmentServiceId,
      memberId: this.member.id,
      doctorId: this.newBooking.doctorId,
      statusBooking: 'Pending',
      createAt: new Date().toISOString()
    };

    this.http.post('https://localhost:7240/api/Booking/create-booking', bookingPayload, headers).subscribe({
      next: res => {
        this.successMessage = 'Đặt lịch thành công!';
        this.newBooking = {
          dayBooking: '',
          treatmentServiceId: 0,
          memberId: 0,
          doctorId: 0,
          statusBooking: 'Pending',
          createAt: ''
        };
      },
      error: err => {
        this.errorMessage = err.error?.message || 'Đặt lịch thất bại';
      }
    });
  }
}
