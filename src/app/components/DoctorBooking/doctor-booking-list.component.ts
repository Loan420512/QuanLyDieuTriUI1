import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-doctor-booking-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './doctor-booking-list.component.html',
  styleUrls: ['./doctor-booking-list.component.css']
})
export class DoctorBookingListComponent implements OnInit {
  bookings: any[] = [];
  errorMessage = '';
  
  constructor(private http: HttpClient, private router: Router) {}

  ngOnInit(): void {
    const token = localStorage.getItem('token');
    const userJson = localStorage.getItem('currentUser');
    const user = userJson ? JSON.parse(userJson) : null;

    // ✅ Kiểm tra quyền
    if (!token || !user || user.role !== 'Doctor') {
      this.router.navigate(['/unauthorized']);
      return;
    }

    // ✅ Gửi token lên server
    const headers = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${token}`
      })
    };

    // ✅ Gọi API lấy danh sách booking của bác sĩ hiện tại
    this.http.get<any>('https://localhost:7240/api/Booking/search-by-doctor?page=1', headers).subscribe({
      next: (res) => {
        // res.Data hoặc res.data tùy backend
         console.log('Response:', res);
        this.bookings = res.data || res.Data || [];
      },
      error: (err) => {
        this.errorMessage = 'Không thể tải lịch khám: ' + (err.error?.message || err.message);
      }
    });
  }
}
