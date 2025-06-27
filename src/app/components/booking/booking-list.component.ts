import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-booking-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './booking-list.component.html',
  styleUrls: ['./booking-list.component.css']
})
export class BookingListComponent implements OnInit {
  bookings: any[] = [];
  member: any = null;
  errorMessage = '';
  selectedBooking: any = null;

  newBooking = {
    fullName: '',
    email: '',
    bookingDate: '',
    serviceName: '',
    memberId: 0
  };

  constructor(private api: ApiService) {}

  ngOnInit(): void {
    const stored = localStorage.getItem('currentUser');
    if (stored) {
      this.member = JSON.parse(stored);
    }

    this.loadBookings();
  }

  createBooking(): void {
    if (!this.member?.userId) {
      alert('Không tìm thấy thông tin người dùng!');
      return;
    }

    const today = new Date();
    const bookingDate = new Date(this.newBooking.bookingDate);
    if (bookingDate < today) {
      alert('Ngày đặt phải là hôm nay hoặc sau hôm nay!');
      return;
    }

    this.newBooking.memberId = this.member.userId;

    this.api.createBooking(this.newBooking).subscribe({
      next: () => {
        alert('Đặt lịch thành công!');
        this.loadBookings();
        this.newBooking = {
          fullName: '',
          email: '',
          bookingDate: '',
          serviceName: '',
          memberId: this.member.userId
        };
      },
      error: (err) => {
        console.error('Lỗi khi đặt lịch:', err);
        alert('Đặt lịch thất bại!');
      }
    });
  }

  loadBookings(): void {
    this.api.getBookings().subscribe({
      next: (data: any[]) => {
        if (this.member) {
          this.bookings = data.filter(b => b.memberId === this.member.userId);
        } else {
          this.bookings = data;
        }
      },
      error: (err: any[]) => {
        console.error('Error loading bookings:', err);
        this.errorMessage = 'Failed to load bookings.';
      }
    });
  }

  viewDetails(booking: any): void {
    this.selectedBooking = booking;
  }

  cancelBooking(id: number): void {
    if (confirm('Bạn có chắc muốn hủy lịch?')) {
      this.api.deleteBooking(id).subscribe({
        next: () => {
          this.loadBookings();
          if (this.selectedBooking?.bookingId === id) {
            this.selectedBooking = null;
          }
          alert('Đã hủy lịch thành công!');
        },
        error: () => {
          alert('Không thể hủy lịch!');
        }
      });
    }
  }
}
