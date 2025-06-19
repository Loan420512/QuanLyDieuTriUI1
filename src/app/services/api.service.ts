import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';

// Define types for the response and request data
export interface RegisterResponse {
  status: string;
  message: string;
  data?: any;
}

export interface RegisterData {
  fullName: string;
  email: string;
  phone: string;
  password: string;
}

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private adminUsername = 'admin';
  private adminPassword = 'admin123';

  constructor(private http: HttpClient) {}


  authenticate(username: string, password: string): boolean {
    return username === this.adminUsername && password === this.adminPassword;
  }


  getPatients(): Observable<any[]> {
    return this.http.get<any[]>('/api/patients').pipe(
      catchError((error) => {
        console.error('Failed to fetch patients', error);
        return of([]);
      })
    );
  }


  registerUser(data: RegisterData): Observable<RegisterResponse> {
    return this.http.post<RegisterResponse>('/api/register', data).pipe(
      catchError((error) => {
        console.error('Registration failed', error);
        throw new Error('Registration failed. Please try again later.');
      })
    );
  }
}
