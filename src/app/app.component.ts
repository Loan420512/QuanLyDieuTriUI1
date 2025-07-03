import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { RouterModule } from '@angular/router';
import { AuthService } from './services/auth.service';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterModule,
    CommonModule
  ],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'infertility';

  // Observables
  username$!: Observable<string | null>;
  loggedIn$!: Observable<boolean>;

  // Values for display
  username: string = '';
  role: string = '';
  userId: number = 0;
  loggedIn: boolean = false;
  profileRoute: string = '';

  constructor(
    public authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loggedIn$ = this.authService.loggedIn$;

    // Đọc user từ localStorage nếu đã đăng nhập
    const stored = localStorage.getItem('currentUser');
    const user = stored ? JSON.parse(stored) : null;
    if (user) {
      this.username = user.userName || '';
      this.userId = user.userId || 0;
      this.role = user.role || '';
      this.profileRoute = this.role === 'Doctor' ? '/doctor-profile' : '/member-profile';
    }

    // Theo dõi trạng thái đăng nhập và tên người dùng
    this.authService.username$.subscribe(name => {
      this.username = name ?? '';
    });

    this.authService.role$.subscribe(r => {
      this.role = r ?? '';
    });

    this.authService.loggedIn$.subscribe(status => {
      this.loggedIn = status;
    });
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
