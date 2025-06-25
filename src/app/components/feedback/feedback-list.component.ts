import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';                    // ✅ Cho *ngIf, *ngFor
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms'; // ✅ Cho [formGroup]
import {  HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-feedback-list',
  standalone: true,                                               // ✅ Bắt buộc với Standalone
  imports: [CommonModule, ReactiveFormsModule], // ✅ Import đầy đủ
  templateUrl: './feedback-list.component.html',
  styleUrls: ['./feedback-list.component.css']
})
export class FeedbackListComponent implements OnInit {
  feedbackForm!: FormGroup;
  doctors: any[] = [];
  submitted = false;
  successMessage = '';
  errorMessage = '';

  constructor(private fb: FormBuilder, private http: HttpClient) {}

  ngOnInit(): void {
    this.feedbackForm = this.fb.group({
      doctorId: ['', Validators.required],
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      rating: [5, [Validators.required, Validators.min(1), Validators.max(5)]],
      message: ['', [Validators.required, Validators.minLength(10)]],
    });

    this.getDoctors();
  }

  getDoctors(): void {
    this.http.get<any[]>('https://your-api-url.com/api/doctors')
      .subscribe(data => this.doctors = data);
  }

  onSubmit(): void {
    this.submitted = true;
    this.successMessage = '';
    this.errorMessage = '';

    if (this.feedbackForm.invalid) return;

    this.http.post('https://your-api-url.com/api/doctor-feedback', this.feedbackForm.value)
      .subscribe({
        next: () => {
          this.successMessage = 'Đã gửi phản hồi thành công!';
          this.feedbackForm.reset({ rating: 5 });
          this.submitted = false;
        },
        error: () => {
          this.errorMessage = 'Gửi phản hồi thất bại.';
        }
      });
  }
}
