import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-booking-list',
   standalone: true,
  imports: [CommonModule],
  templateUrl: './booking-list.component.html',
  styleUrls: ['./booking-list.component.css']
})
export class BookingListComponent implements OnInit {
  bookings: any[] = [];
  errorMessage = '';

  constructor(private api: ApiService) {}

  ngOnInit(): void {
    this.loadBookings();
  }

  loadBookings(): void {
    this.api.getBookings().subscribe({
      next: (data: any[]) => {
        this.bookings = data;
      },
      error: (err:any[]) => {
        console.error('Error loading bookings:', err);
        this.errorMessage = 'Failed to load bookings.';
      }
    });
  }
}
