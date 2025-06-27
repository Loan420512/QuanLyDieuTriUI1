import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  username: string | null = null;
  role: string | null = null;
   loggedIn$!: Observable<boolean>;


  // ✅ Truyền AuthService vào constructor
  constructor(private auth: AuthService) {}

  ngOnInit() {
    // Lấy realtime từ BehaviorSubject
     this.auth.username$.subscribe(name => {
    console.log('👤 Username in header:', name);
    this.username = name;
    });
    this.auth.role$.subscribe(role => {
      console.log('👮 Role in header:', role);
      this.role = role;
    });
    this.loggedIn$ = this.auth.loggedIn$; // Lấy trạng thái đăng nhập

    // Đồng thời fallback nếu có trong localStorage (trường hợp reload)
    const userData = localStorage.getItem('currentUser');
    if (userData) {
      const user = JSON.parse(userData);
      this.username = user.userName || user.username;
      this.role = user.role;
    }
  }

  logout() {
    this.auth.logout(); // gọi từ service luôn
    window.location.href = '/login';
  }
}
