// src/app/services/patient.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { Observable, of, throwError } from 'rxjs';

// 👉 Interface cho bệnh nhân (có thể sửa thêm nếu cần)
export interface Patient {
  id: number;
  fullName: string;
  email: string;
  doctorId: number;
}

@Injectable({
  providedIn: 'root'
})
export class PatientService {
  private apiUrl = 'https://localhost:5001/api/member'; // hoặc 5001 nếu ASP.NET Core

  constructor(private http: HttpClient) {}

  getPatients(): Observable<Patient[]> {
    return this.http.get<Patient[]>(this.apiUrl).pipe(
      catchError((error) => {
        console.error('Failed to fetch patients:', error);
        return of([]);
      })
    );
  }


  addPatient(patient: Patient): Observable<Patient> {
    return this.http.post<Patient>(this.apiUrl, patient).pipe(
      catchError((error) => {
        console.error('Failed to add patient:', error);
        return throwError(() => new Error('Add patient failed'));
      })
    );
  }

  deletePatient(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`).pipe(
      catchError((error) => {
        console.error('Failed to delete patient:', error);
        return throwError(() => new Error('Delete failed'));
      })
    );
  }
  getPatientsByDoctor(doctorId: number): Observable<Patient[]> {
  const url = `${this.apiUrl}?doctorId=${doctorId}`;
 return this.http.get<Patient[]>(`${this.apiUrl}/patients-by-doctor/${doctorId}`).pipe(
    catchError((error) => {
      console.error('Failed to fetch patients by doctor:', error);
      return of([]);
    })
  );
}

}
