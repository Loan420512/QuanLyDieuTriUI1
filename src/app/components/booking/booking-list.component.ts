import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { forkJoin } from 'rxjs';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

// --- INTERFACES ---
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
  id: number;
  name: string;
  specialization: string;
  degrees: string;
  email: string;
  phone: string;
  experience: string;
  skills: string[];
  bio: string;
}

interface Member {
  id: number;
  username: string;
}

interface TreatmentService {
  id: number;
  name: string;
}

// --- COMPONENT ---
@Component({
  selector: 'app-booking-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './booking-list.component.html',
  styleUrls: ['./booking-list.component.css']
})
export class BookingListComponent implements OnInit {
  // --- GIAO DIỆN ---
  member: Member = { id: 1, username: 'Khách hàng A' }; // mô phỏng login user
  errorMessage: string = '';
  newBooking = {
  dayBooking: '',
  treatmentServiceId: 0,
  memberId: 0,
  doctorId: 0,
  statusBooking: 'Pending',
  createAt: ''
};

  // --- DỮ LIỆU ---
  bookings: Booking[] = [];
  doctors: Doctor[] = [];
  members: Member[] = [];
  services: TreatmentService[] = [];
  selectedBooking: Booking | null = null;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.loadData();
    this.loadMockData();
  }
  loadMockData() {
  this.doctors = [
    {
      id: 1,
      name: 'Dr.Nguyen',
      specialization: 'IVF Specialist',
      degrees: 'MD',
      email: 'dr.nguyen@example.com',
      phone: '0901-234-567',
      experience: '15 years',
      skills: ['IVF', 'ICSI'],
      bio: '...'
    },
    {
      id: 2,
      name: 'Dr.Le',
      specialization: 'Andrology Expert',
      degrees: 'MD',
      email: 'dr.le@example.com',
      phone: '0902-888-999',
      experience: '10 years',
      skills: ['Semen Analysis'],
      bio: '...'
    }
  ];

  this.members = [
    { id: 1, username: 'Nguyen Van A' },
    { id: 2, username: 'Tran Thi B' }
  ];

  this.services = [
    { id: 1, name: 'IVF Package' },
    { id: 2, name: 'Fertility Checkup' }
  ];
  }
  // --- TẢI DỮ LIỆU ---
  loadData() {
    forkJoin({
      bookings: this.http.get<Booking[]>('/api/bookings'),
      doctors: this.http.get<Doctor[]>('/api/doctors'),
      members: this.http.get<Member[]>('/api/members'),
      services: this.http.get<TreatmentService[]>('/api/treatment-services'),
    }).subscribe({
      next: ({ bookings, doctors, members, services }) => {
        this.bookings = bookings;
        this.doctors = doctors;
        this.members = members;
        this.services = services;
      },
      error: err => {
        this.errorMessage = 'Lỗi khi tải dữ liệu: ' + err.message;
      }
    });
  }

  // --- HỖ TRỢ HIỂN THỊ ---
  getDoctorName(id: number): string {
    return this.doctors.find(d => d.id === id)?.name || 'Không rõ';
  }

  getMemberName(id: number): string {
    return this.members.find(m => m.id === id)?.username || 'Không rõ';
  }

  getServiceName(id: number): string {
    return this.services.find(s => s.id === id)?.name || 'Không rõ';
  }

  // --- HÀNH ĐỘNG ---
  viewDetails(b: Booking) {
    this.selectedBooking = b;
  }

  cancelBooking(id: number) {
    alert('Hủy booking ID: ' + id);
  }
  getSelectedDoctor(): Doctor | undefined {
  return this.doctors.find(d => d.id === this.newBooking.doctorId);
  }


  createBooking() {
  const token = localStorage.getItem('token');
  const headers = {
    headers: {
      Authorization: `Bearer ${token}`,
      'Content-Type': 'application/json'
    }
  };

  const booking = {
    bookingId: 0,
    dayBooking: this.newBooking.dayBooking,
    treatmentServiceId: this.newBooking.treatmentServiceId,
    memberId: this.member.id,
    doctorId: this.newBooking.doctorId,
    statusBooking: this.newBooking.statusBooking,
    createAt: new Date().toISOString()
  };

  this.http.post('https://localhost:7240/api/Booking/create-booking', booking, headers)
    .subscribe({
      next: res => {
        alert('Đặt lịch thành công');
        this.newBooking = {
          dayBooking: '',
          treatmentServiceId: 0,
          memberId: 0,
          doctorId: 0,
          statusBooking: 'Pending',
          createAt: ''
        };
        this.loadData();
      },
      error: err => {
        this.errorMessage = 'Lỗi tạo đặt lịch: ' + err.message;
      }
    });
}
}
