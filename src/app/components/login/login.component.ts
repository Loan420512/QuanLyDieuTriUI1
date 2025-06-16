import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, CommonModule],
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
    alert(`✅ Logged in as ${user}`);
    this.router.navigate(['/admin']);
  } else if (user === 'doctor' && pass === 'doc123') {
    alert(`✅ Logged in as ${user}`);
    this.router.navigate(['/doctor']);
  } else {
    alert('❌ Invalid credentials!');
  }
}
}

