// src/app/services/auth.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';

export interface LoginResponse {
  username: string;
  role: string;
  token?: string;
}

export interface RegisterData {
  UserName : string;
  Email: string;
  Password: string;
  ConfirmPassword: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7240/api'; // Đường dẫn backend của bạn

  constructor(private http: HttpClient) {}

   login(data: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/Auth/login`, data);
  }

    register(data: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/Register/register-with-email-confirmation`, data);
  }

  logout(): void {
    localStorage.removeItem('token');
  }

  storeToken(token: string): void {
    localStorage.setItem('token', token);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }
}
