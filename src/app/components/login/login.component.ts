import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../../services/api.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  imports: [CommonModule, FormsModule]
})
export class LoginComponent {
  userName: string = '';
  password: string = '';

  constructor(
    private api: ApiService,
    private router: Router,
    private authService: AuthService
  ) {}

  onLogin(): void {
    this.api.login(this.userName, this.password).subscribe({
      next: (res) => {
        const token = res.token;
        if (!token) {
          alert('Không nhận được token!');
          return;
        }

        // ✅ Gọi một lần duy nhất để lưu toàn bộ user info
        this.authService.storeUserData(res);

        alert('Đăng nhập thành công!');

        const role = res.role?.toLowerCase();
        const userId = res.userId;

        // ✅ Điều hướng theo vai trò
        if (role === 'admin') {
          this.router.navigate(['/admin']);
        } else if (role === 'member') {
          this.router.navigate(['/member-profile', userId]);
        } else {
          this.router.navigate(['/home']);
        }
      },
      error: (err) => {
        console.error('Login error:', err);
        alert('Đăng nhập thất bại!');
      }
    });
  }
}
