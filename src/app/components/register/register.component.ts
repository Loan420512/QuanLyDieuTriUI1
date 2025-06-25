import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService, RegisterData } from '../../services/api.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-register',
   standalone: true,
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
   imports: [
    FormsModule
  ]
})
export class RegisterComponent {
  user: RegisterData = {
    Username: '',
    Email: '',
    Password: '',
    ConfirmPassword: ''
  };

    constructor(
    private api: ApiService,
    private router: Router
  ) {}

  onSubmit(): void {
    if (this.user.Password !== this.user.ConfirmPassword) {
      alert('❌ Mật khẩu không khớp!');
      return;
    }

    this.api.registerUser(this.user).subscribe({
  next: (res) => {
    // Kiểm tra nếu response có thuộc tính 'success' và là true
    if (res?.success === true) {
      const userId = res.data?.userId;
      alert('✅ ' + res.message);
      console.log('User ID:', userId);

      // 👉 Chuyển hướng sau khi đăng ký
      this.router.navigate(['/login']);
    } else {
      // Trường hợp backend trả lỗi logic nhưng không throw
      alert('❌ Đăng ký thất bại: ' + (res?.message || 'Không xác định'));
    }
  },
  error: (err) => {
    // Trường hợp backend trả về lỗi HTTP (400, 500, etc.)
    const errorMessage = err?.error?.message || 'Lỗi không xác định từ server.';
    alert('❌ Lỗi từ server: ' + errorMessage);
    console.error('Chi tiết lỗi:', err);
  }
});
  }
}
