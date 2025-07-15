import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class PatientService {
  private bookingUrl = 'https://localhost:7240/api/Booking';
  private doctorUrl = 'https://localhost:7240/api/InfoDoctor';
  private reminderUrl = 'https://localhost:7240/api/Reminder';

  constructor(private http: HttpClient) {}

  // 🔹 Gọi API booking theo bác sĩ (dựa vào token)
  getBookingsByDoctor(): Observable<any> {
  const token = localStorage.getItem('token');
  const headers = token ? { headers: { Authorization: `Bearer ${token}` } } : {};
  return this.http.get<any>(`${this.bookingUrl}/search-by-doctor?page=1`, headers);
  }


  // 🔹 Lấy thông tin doctor từ token
  getMyDoctorInfo(): Observable<any> {
    const token = localStorage.getItem('token');
    const headers = token ? { headers: { Authorization: `Bearer ${token}` } } : {};
    return this.http.get<any>(`${this.doctorUrl}/my-info`, headers);
  }

  // 🔹 Gửi nhắc nhở đến bệnh nhân
  sendReminder(payload: any): Observable<any> {
    const token = localStorage.getItem('token');
    const headers = token ? { headers: { Authorization: `Bearer ${token}` } } : {};
    return this.http.post(`https://localhost:7240/api/Notification/create-notification`, payload, headers);
  }

  // 🔹 Gửi booking (User tạo lịch khám)
  createBooking(booking: any): Observable<any> {
    const token = localStorage.getItem('token');
    const headers = token ? { headers: { Authorization: `Bearer ${token}`, 'Content-Type': 'application/json' } } : {};
    return this.http.post(`https://localhost:7240/api/Booking/create-booking`, booking, headers);
  }
  getMemberById(id: number): Observable<any> {
  const token = localStorage.getItem('token');
  const headers = token ? { headers: { Authorization: `Bearer ${token}` } } : {};
  return this.http.get(`https://localhost:7240/api/Member/${id}`, headers);
}

  getTreatmentServiceById(id: number): Observable<any> {
  const token = localStorage.getItem('token');
  const headers = token ? { headers: { Authorization: `Bearer ${token}` } } : {};
  return this.http.get(`https://localhost:7240/api/TreatmentService/${id}`, headers);
  }
  getMyNotifications(userId: number): Observable<any> {
  const token = localStorage.getItem('token');
  const headers = token ? {
    headers: {
      Authorization: `Bearer ${token}`
    }
  } : {};

  return this.http.post(`https://localhost:7240/api/Notification/search-notification?userId=${userId}`, {}, headers);
}
createNotification(payload: any): Observable<any> {
  const token = localStorage.getItem('token');
  const headers = token ? {
    headers: {
      Authorization: `Bearer ${token}`,
      'Content-Type': 'application/json'
    }
  } : {};
  return this.http.post(`https://localhost:7240/api/Notification/create-notification`, payload, headers);
}

} 