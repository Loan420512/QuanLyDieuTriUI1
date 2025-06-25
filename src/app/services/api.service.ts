import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

// ---------- Interfaces ----------
export interface RegisterData {
  Username: string;
  Email: string;
  Password: string;
  ConfirmPassword: string;
}

export interface RegisterResponse {
  data: {
    userId: number;
    userName: string;
    email: string;
  };
  success: boolean;
  code: string | null;
  message: string;
  variant: string;
  title: string;
}


export interface LoginResponse {
  username: string;
  role: string;
  token?: string;
  message?: string;
}

// ---------- API Service ----------
@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = 'https://localhost:7240/api';

  constructor(private http: HttpClient) {}

  // ------------------ AUTH ------------------
  login(username: string, password: string): Observable<any> {
  return this.http.post('https://localhost:7240/api/Login/Login', null, {
    params: new HttpParams()
      .set('userName', username)
      .set('Password', password),
  });
  }


  registerUser(data: RegisterData): Observable<RegisterResponse> {
    return this.http.post<RegisterResponse>('https://localhost:7240/api/Register/register-with-email-confirmation', data);
  }

  confirmEmail(userId: number, token: string): Observable<any> {
    const url = `${this.apiUrl}/Register/confirm-email?userId=${userId}&token=${encodeURIComponent(token)}`;
    return this.http.get(url);
  }

  // ------------------ PATIENT ------------------
  getPatients(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/patients`).pipe(
      catchError((error) => {
        console.error('Failed to fetch patients', error);
        return of([]); // Trả mảng rỗng nếu có lỗi
      })
    );
  }

  // ------------------ BOOKING ------------------
  getBookings(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/Bookings`);
  }

  // ------------------ ADMIN (DEV MODE) ------------------
  authenticateDevAdmin(username: string, password: string): boolean {
    const adminUsername = 'admin';
    const adminPassword = 'admin123';
    return username === adminUsername && password === adminPassword;
  }
}

