import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ApiService, LoginResponse } from '../../services/api.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  username = '';
  password = '';
  errorMessage = '';

  constructor(private api: ApiService, private router: Router) {}

  login(): void {
    const user = this.username.trim();
    const pass = this.password.trim();

    if (!user || !pass) {
      alert('❗ Please fill in both fields.');
      return;
    }

    this.api.login(user, pass).subscribe({
      next: (response: LoginResponse) => {
        alert(`✅ Welcome ${response.username}`);
        if (response.role === 'admin') {
          this.router.navigate(['/admin']);
        } else if (response.role === 'doctor') {
          this.router.navigate(['/doctor']);
        } else {
          this.router.navigate(['/home']);
        }
      },
      error: (err) => {
        console.error(err);
        this.errorMessage = '❌ Invalid username or password!';
        alert(this.errorMessage);
      }
    });
  }
}
