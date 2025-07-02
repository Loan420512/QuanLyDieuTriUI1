import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';
import { Observable } from 'rxjs';
import { RouterModule } from '@angular/router'; 

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  username: string | null = null;
  role: string | null = null;
  userId: number = 0; 
  loggedIn$!: Observable<boolean>;

  constructor(private auth: AuthService) {}

  ngOnInit() {
    this.loggedIn$ = this.auth.loggedIn$; // ✅ đúng rồi

    this.auth.username$.subscribe(name => {
      console.log('👤 Username in header:', name);
      this.username = name;
    });

    this.auth.role$.subscribe(role => {
      console.log('👮 Role in header:', role);
      this.role = role;
    });

    // Fallback nếu reload
    const userData = localStorage.getItem('currentUser');
    if (userData) {
      const user = JSON.parse(userData);
      this.username = user.fullName || user.userName || user.username;
      this.role = user.role;
      this.userId = user.userId;
    }
  }

  logout() {
    this.auth.logout();
    window.location.href = '/login'; // hoặc dùng Router.navigate
  }
}
