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
  userId: number;
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
  newBooking = {
    dayBooking: '',
    treatmentServiceId: 0,
    doctorId: 0,
    statusBooking: 'Pending',
    createAt: ''
  };

  bookings: Booking[] = [];
  doctors: Doctor[] = [];
  services: TreatmentService[] = [];
  patients: any[] = [];

  role: string = '';
  member = {
    id: 0,
    username: ''
  };

  today: string = new Date().toISOString().split('T')[0];
  errorMessage = '';
  successMessage = '';

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    const user = this.getUser();
    if (!user) {
      this.errorMessage = 'Bạn chưa đăng nhập.';
      return;
    }

    this.role = user.role;
    this.member = {
      id: user.userId,
      username: user.userName || 'Khách'
    };

    this.loadDoctorsAndServices();

    if (this.role === 'Doctor') {
      this.loadBookingsByDoctor();
    } else if (this.role === 'User') {
      this.loadBookingsByMember();
    }
  }

  getUser() {
    const userJson = localStorage.getItem('currentUser');
    return userJson ? JSON.parse(userJson) : null;
  }

  getHeaders(): HttpHeaders {
    const token = localStorage.getItem('token');
    return new HttpHeaders({ Authorization: `Bearer ${token}` });
  }

  loadDoctorsAndServices(): void {
    const headers = { headers: this.getHeaders() };

    this.http.get<any>('https://localhost:7240/api/InfoDoctor', headers).subscribe({
      next: data => {
        this.doctors = Array.isArray(data) ? data : data.data || [];
      },
      error: err => {
        this.errorMessage = 'Lỗi tải bác sĩ: ' + (err.message || 'Không xác định');
      }
    });

    this.http.get<any>('https://localhost:7240/api/TreatmentService', headers).subscribe({
      next: data => {
        this.services = Array.isArray(data) ? data : data.data || [];
      },
      error: err => {
        this.errorMessage = 'Lỗi tải dịch vụ: ' + (err.message || 'Không xác định');
      }
    });
  }

 loadBookingsByDoctor(): void {
  const headers = { headers: this.getHeaders() };

  this.http.get<any>('https://localhost:7240/api/Booking/search-by-doctor?page=1', headers).subscribe({
    next: res => {
      this.bookings = res.data || res.Data || [];
      console.log('📥 Bookings from API:', this.bookings);
    },
    error: err => {
      console.error('❌ Không thể tải danh sách booking:', err);
    }
  });
  }

  loadBookingsByMember(): void {
    const headers = { headers: this.getHeaders() };

    this.http.get<any>('https://localhost:7240/api/Booking/my-bookings?page=1', headers).subscribe({
      next: res => {
        this.bookings = res.data || res.Data || [];
        console.log('📥 Bookings:', this.bookings);
      },
      error: err => {
        this.errorMessage = 'Không thể tải lịch hẹn của bạn: ' + (err.message || 'Không xác định');
      }
    });
  }
  loadPatients(): void {
  const headers = { headers: this.getHeaders() };
  this.http.get<any>('https://localhost:7240/api/Booking/search-by-doctor?page=1', headers).subscribe({
    next: res => {
      const bookings = res.data || res.Data || [];

      // Gộp dữ liệu theo bệnh nhân (có thể thêm API lấy tên thật)
      this.patients = bookings.map((b: any) => ({
        name: `Bệnh nhân #${b.memberId}`,  // Tạm thời hiển thị theo ID
        treatment: `Dịch vụ #${b.treatmentServiceId}`,
        status: b.statusBooking
      }));
    },
    error: err => {
      console.error('Lỗi khi tải danh sách bệnh nhân:', err);
      this.patients = [];
    }
  });
}


 createBooking(): void {
  this.errorMessage = '';
  this.successMessage = '';

  const user = this.getUser();
  if (!user || this.role !== 'User') {
    this.errorMessage = 'Chỉ người dùng mới có thể đặt lịch.';
    return;
  }

  // Kiểm tra dữ liệu nhập
  if (
    !this.newBooking.dayBooking ||
    !this.newBooking.treatmentServiceId ||
    !this.newBooking.doctorId
  ) {
    this.errorMessage = 'Vui lòng điền đầy đủ thông tin.';
    return;
  }

  // ✅ Chuyển định dạng yyyy-MM-dd (DateOnly)
  const formatDate = (dateStr: string): string => {
    const d = new Date(dateStr);
    const year = d.getFullYear();
    const month = String(d.getMonth() + 1).padStart(2, '0');
    const day = String(d.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
  };

  // ✅ Bọc payload đúng yêu cầu backend
  const bookingPayload = {
    bookingReq: {
      bookingId: 0,
      dayBooking: formatDate(this.newBooking.dayBooking),
      treatmentServiceId: Number(this.newBooking.treatmentServiceId),
      doctorId: Number(this.newBooking.doctorId),
      statusBooking: 'Pending',
      createAt: formatDate(new Date().toISOString())
    }
  };

  const headers = {
    headers: this.getHeaders().set('Content-Type', 'application/json')
  };

  console.log('📤 Payload gửi đi:', bookingPayload);

  this.http
    .post<any>('https://localhost:7240/api/Booking/create-booking', bookingPayload, headers)
    .subscribe({
      next: (res) => {
        console.log('✅ Response:', res);
        if (res.success) {
          this.successMessage = 'Đặt lịch thành công!';
          this.resetForm();
          this.loadBookingsByMember();
        } else {
          this.errorMessage = res.message || 'Đặt lịch thất bại.';
        }
      },
      error: (err) => {
        console.error('❌ Lỗi backend:', err);
        alert('❌ Lỗi backend: ' + JSON.stringify(err.error));
        this.errorMessage = err.error?.message || 'Lỗi hệ thống khi đặt lịch.';
      }
    });
}


  resetForm(): void {
    this.newBooking = {
      dayBooking: '',
      treatmentServiceId: 0,
      doctorId: 0,
      statusBooking: 'Pending',
      createAt: ''
    };
  }
}
