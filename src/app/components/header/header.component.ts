import { Component } from '@angular/core';
import { CommonModule } from '@angular/common'; // ← Thêm dòng này

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {
  username: string | null = null;
  role: string | null = null;

  ngOnInit() {
    const userData = localStorage.getItem('user');
    if (userData) {
      const user = JSON.parse(userData);
      this.username = user.username;
      this.role = user.role;
    }
  }

  logout() {
    localStorage.removeItem('user');
    window.location.href = '/login';
  }
}
