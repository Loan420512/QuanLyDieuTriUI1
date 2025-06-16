import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  constructor(private http: HttpClient) {}

  getPatients() {
    return this.http.get('/api/patients');
  }

  registerUser(data: any) {
    return this.http.post('/api/register', data);
  }
}
