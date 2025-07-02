import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';

export interface LoginResponse {
  userName: string;
  role: string;
  token: string;
  userId?: number; // thêm nếu có
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

  // Subjects để theo dõi trạng thái đăng nhập và user
  private usernameSubject = new BehaviorSubject<string | null>(null);
  private roleSubject = new BehaviorSubject<string | null>(null);
  private loggedInSubject = new BehaviorSubject<boolean>(this.hasToken());

  username$ = this.usernameSubject.asObservable();
  role$ = this.roleSubject.asObservable();
  loggedIn$ = this.loggedInSubject.asObservable();

  constructor(private http: HttpClient) {
    this.initializeUserFromStorage();
  }

  private initializeUserFromStorage(): void {
    const userStr = localStorage.getItem('currentUser');
    if (userStr) {
      const user: LoginResponse = JSON.parse(userStr);
      this.usernameSubject.next(user.userName);
      this.roleSubject.next(user.role);
      this.loggedInSubject.next(true);
    }
  }

  login(credentials: { userName: string; password: string }): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.apiUrl}/Account/login`, credentials);
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
    this.loggedInSubject.next(true);
  }

  getCurrentUser(): LoginResponse | null {
    const userStr = localStorage.getItem('currentUser');
    return userStr ? JSON.parse(userStr) : null;
  }

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('currentUser');
    this.usernameSubject.next(null);
    this.roleSubject.next(null);
    this.loggedInSubject.next(false);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  private hasToken(): boolean {
    return !!localStorage.getItem('token');
  }

  isLoggedIn(): boolean {
    return this.loggedInSubject.value;
  }
}
