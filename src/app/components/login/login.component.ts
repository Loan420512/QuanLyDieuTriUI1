import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  username = '';
  password = '';

  constructor(private router: Router) {}

  login(): void {
    const user = this.username.trim();
    const pass = this.password.trim();

    if (!user || !pass) {
      alert('❗ Please fill in both fields.');
      return;
    }
    if (user === 'admin' && pass === 'admin123') {
  this.router.navigate(['/admin']);
} else if (user === 'doctor' && pass === 'doc123') {
  this.router.navigate(['/doctor']);
} else {
  alert('❌ Invalid credentials!');
}

    // Giả lập đăng nhập thành công
    alert(`✅ Logged in as ${user}`);
    this.router.navigate(['/dashboard']); // Điều hướng sau login
  }
}

