import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { tap, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class DoctorService {
  private apiUrl = 'https://localhost:7240/api';
  private doctors: any[] = [];

  constructor(private http: HttpClient) {}

  getAllDoctors(): Observable<any[]> {
    return this.http.get<any>(`https://localhost:7240/api/InfoDoctor`).pipe(
      map(res => Array.isArray(res) ? res : res?.data || []),
      tap(data => this.doctors = data)
    );
  }
  getDoctors(): Observable<any[]> {
  return of(this.doctors || []);
  }

  // ✅ Thêm tiện ích đảm bảo luôn trả ra dữ liệu
  loadAndGetDoctors(): Observable<any[]> {
    if (this.doctors.length > 0) return of(this.doctors);
    return this.getAllDoctors();
  }
  

  getDoctorById(id: number): Observable<any | undefined> {
    const doctor = this.doctors.find(d => d.infoId === id);
    return of(doctor);
  } 

  getPatientsByDoctorId(doctorId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/doctors/${doctorId}/patients`);
  }
}
