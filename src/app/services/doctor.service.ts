import { Injectable } from '@angular/core';
import { of, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DoctorService {
  private apiUrl = 'https://localhost:7240'; // ✅ Sửa thành địa chỉ API của bạn

  constructor(private http: HttpClient) {}

  // ✅ Hàm cần thiết: Lấy bệnh nhân theo doctorId
  getPatientsByDoctorId(doctorId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/doctors/${doctorId}/patients`);
  }
  
  private doctors = [
    {
      id: 1,
      name: 'Dr.Nguyen',
      title: 'IVF Specialist & Reproductive Endocrinologist',
      image: 'assets/img/doctors-3.jpg',
      description: 'Over 15 years of experience helping couples achieve their dream of parenthood through personalized IVF treatments.'
    },
    {
      id: 2,
      name: 'Dr.Le',
      title: 'Male Fertility & Andrology Expert',
      image: 'assets/img/00.jpg',
      description: 'Specializing in male infertility diagnosis and treatments including surgical sperm retrieval and hormone therapy.'
    },
    {
      id: 3,
      name: 'Dr.Tran',
      title: 'Fertility Counselor & Psychologist',
      image: 'assets/img/000.jpg',
      description: 'Supports patients with emotional wellness and decision-making throughout their fertility journey.'
    },
    {
      id: 4,
      name: 'Dr.Pham',
      title: 'Embryologist & Lab Director',
      image: 'assets/img/1111.jpg',
      description: 'Leads the IVF lab with expertise in embryo culture, ICSI, and cryopreservation technology.'
    }
  ];

  getDoctors(): Observable<any[]> {
    return of(this.doctors); // ✅ Trả về dạng Observable
  }

  getDoctorById(id: number): Observable<any | undefined> {
    const doctor = this.doctors.find(d => d.id === id);
    return of(doctor); // Trả về Observable
  }
}
