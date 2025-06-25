import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../../services/api.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  standalone: true,
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  imports: [CommonModule, FormsModule]
})
export class LoginComponent {
  username: string = '';
  password: string = '';

  constructor(
    private api: ApiService,
    private router: Router
  ) {}

  onLogin(): void {
    const user = this.username.trim();
    const pass = this.password.trim();

    if (!user || !pass) {
      alert('Vui lòng nhập đầy đủ tài khoản và mật khẩu.');
      return;
    }

    this.api.login(user, pass).subscribe({
      next: (res) => {
        const userData = res?.data;

        if (!res?.success || !userData?.username || !userData?.role) {
          alert(' Sai thông tin đăng nhập!');
          return;
        }

        //  Lưu thông tin vào localStorage
        localStorage.setItem('currentUser', JSON.stringify(userData));
        alert('Đăng nhập thành công!');

        //  Điều hướng theo role
        const role = userData.role.toLowerCase();
        if (role === 'admin') {
          this.router.navigate(['/admin']);
        } else {
          this.router.navigate(['/home']);
        }
      },
      error: (err) => {
        const msg = err.error?.message || 'Không xác định';
        alert('Lỗi từ server: ' + msg);
        console.error(err);
      }
    });
  }
}
