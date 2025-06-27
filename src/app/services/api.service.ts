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
  userName: string;
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
  login(userName: string, password: string): Observable<any> {
  return this.http.post<any>('https://localhost:7240/api/Account/login', {
    userName,
    password
  });
}
getAuthHeaders() {
  const token = localStorage.getItem('access_token');
  return {
    headers: {
      Authorization: `Bearer ${token}`
    }
  };
}
getAdminData(): Observable<any> {
  const token = localStorage.getItem('access_token');
  return this.http.get('https://localhost:7240/api/Account/admin-data', {
    headers: {
      Authorization: `Bearer ${token}`
    }
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
  const token = localStorage.getItem('token'); // hoặc 'access_token' nếu bạn dùng key đó
  const headers = {
    headers: {
      Authorization: `Bearer ${token}`
    }
  };
  return this.http.get<any[]>(`${this.apiUrl}/Bookings`, headers);
  }


  createBooking(bookingData: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/Bookings`, bookingData);
  }
  deleteBooking(id: number): Observable<any> {
  return this.http.delete(`${this.apiUrl}/Bookings/${id}`);
  }
  updateBooking(id: number, bookingData: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/Bookings/${id}`, bookingData);
  }

  getMemberById(id: number): Observable<any> {
  return this.http.get(`https://localhost:7240/api/Members/${id}`);
  }


  // ------------------ ADMIN (DEV MODE) ------------------
  authenticateDevAdmin(userName: string, password: string): boolean {
    const adminUserName = 'admin';
    const adminPassword = 'admin123';
    return userName === adminUserName && password === adminPassword;
  }
}

