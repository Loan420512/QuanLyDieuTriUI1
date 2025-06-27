import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';

export interface LoginResponse {
  userName: string;
  role: string;
  token: string;
  [key: string]: any;
}

export interface RegisterData {
  UserName: string;
  Email: string;
  Password: string;
  ConfirmPassword: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7240/api';

  private usernameSubject = new BehaviorSubject<string | null>(null);
  private roleSubject = new BehaviorSubject<string | null>(null);
  private loggedInSubject = new BehaviorSubject<boolean>(!!this.getToken()); // ✅ THÊM DÒNG NÀY

  username$ = this.usernameSubject.asObservable();
  role$ = this.roleSubject.asObservable();
  loggedIn$ = this.loggedInSubject.asObservable(); // ✅ THÊM DÒNG NÀY

  constructor(private http: HttpClient) {
    const userStr = localStorage.getItem('currentUser');
    if (userStr) {
      const user = JSON.parse(userStr);
      this.usernameSubject.next(user.userName);
      this.roleSubject.next(user.role);
      this.loggedInSubject.next(true); // ✅ THÊM nếu user tồn tại
    }
  }

  login(data: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/Account/login`, data);
  }

  register(data: RegisterData): Observable<any> {
    return this.http.post(`${this.apiUrl}/Register/register-with-email-confirmation`, data);
  }

  getMemberById(id: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/Members/${id}`);
  }

  storeUserData(user: LoginResponse): void {
    localStorage.setItem('token', user.token);
    localStorage.setItem('currentUser', JSON.stringify(user));
    this.usernameSubject.next(user.userName);
    this.roleSubject.next(user.role);
    this.loggedInSubject.next(true); // ✅ thông báo đăng nhập
  }

   getCurrentUser() {
    return JSON.parse(localStorage.getItem('currentMember') || 'null');
  }
  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('currentUser');
    this.usernameSubject.next(null);
    this.roleSubject.next(null);
    this.loggedInSubject.next(false); // ✅ thông báo đăng xuất
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }
}
