// src/app/services/booking.service.ts
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';


const BASE_URL = 'https://localhost:7240/api/Booking'; // Đổi theo API backend

@Injectable({
  providedIn: 'root'
})
export class BookingService {
  constructor(private http: HttpClient) {}

  private getHeaders(): HttpHeaders {
    const token = localStorage.getItem('token');
    return new HttpHeaders({ Authorization: `Bearer ${token}` });
  }

  createBooking(data: any): Observable<any> {
    return this.http.post(`https://localhost:7240/api/Booking/create-booking`, data, { headers: this.getHeaders() });
  }

  getMyBookings(page: number): Observable<any> {
    return this.http.get(`https://localhost:7240/api/Booking/my-bookings`, { headers: this.getHeaders() });
  }

  getBookingsByDoctor(page: number): Observable<any> {
    return this.http.get(`https://localhost:7240/api/Booking/search-by-doctor`, { headers: this.getHeaders() });
  }

  searchBookings(keyword: string, page = 1): Observable<any> {
    return this.http.post(`https://localhost:7240/api/Booking/search-booking`, {}, { headers: this.getHeaders() });
  }

  updateBooking(id: number, data: any): Observable<any> {
    return this.http.put(`https://localhost:7240/api/Booking/${id}`, data, { headers: this.getHeaders() });
  }

}
