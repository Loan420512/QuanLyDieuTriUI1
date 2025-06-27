import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { RouterModule } from '@angular/router';
import { HeaderComponent } from './components/header/header.component';
import { AuthService } from './services/auth.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterModule,
    HeaderComponent,
  ],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'infertility';
  username$!: Observable<string | null>;
  loggedIn$!: Observable<boolean>; // ✅ Thêm dòng này

  constructor(public authService: AuthService, private router: Router) {}

  ngOnInit() {
    this.username$ = this.authService.username$;
    this.loggedIn$ = this.authService.loggedIn$; // ✅ Gán trong lifecycle để tránh lỗi
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
