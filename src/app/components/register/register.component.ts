import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ApiService, RegisterData, RegisterResponse } from '../../services/api.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  fullName: string = '';
  email: string = '';
  phone: string = '';
  password: string = '';
  confirmPassword: string = '';

  constructor(private api: ApiService) {}

  onSubmit(): void {
    if (!this.fullName || !this.email || !this.phone || !this.password || !this.confirmPassword) {
      alert(' Please fill in all fields.');
      return;
    }

    if (this.password !== this.confirmPassword) {
      alert(" Passwords do not match!");
      return;
    }

    const formData: RegisterData = {
      fullName: this.fullName,
      email: this.email,
      phone: this.phone,
      password: this.password
    };

    this.api.registerUser(formData).subscribe({
      next: (response: RegisterResponse) => {
        alert(` Registration successful: ${response.message}`);
        // Optional: Reset form fields
        this.fullName = '';
        this.email = '';
        this.phone = '';
        this.password = '';
        this.confirmPassword = '';
      },
      error: (err) => {
        console.error(err);
        alert('Registration failed. Please try again.');
      }
    });
  }
}
