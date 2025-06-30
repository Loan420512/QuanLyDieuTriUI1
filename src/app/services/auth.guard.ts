import { Injectable } from '@angular/core';
import { CanActivate, Router,ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

   canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const expectedRole = route.data['role']; // lấy role được cấu hình
    const userStr = localStorage.getItem('currentUser');

    if (userStr) {
      const user = JSON.parse(userStr);
      const userRole = user.role;

      if (!expectedRole || userRole === expectedRole) {
        return true;
      }

      // ❌ Không đúng role
      this.router.navigate(['/unauthorized']); // hoặc về trang home
      return false;
    }

    // ❌ Chưa login
    this.router.navigate(['/login']);
    return false;
  }
  storeUserData(user: any): void {
  localStorage.setItem('currentUser', JSON.stringify({
    userId: user.userId,
    userName: user.userName,
    role: user.role,
    token: user.token
  }));
}
 goToProfile() {
  const userStr = localStorage.getItem('currentUser');
  if (userStr) {
    const user = JSON.parse(userStr);
    if (user.role === 'Doctor') {
      this.router.navigate(['/doctor-profile', user.userId]);
    } else if (user.role === 'Member') {
      this.router.navigate(['/member-profile', user.userId]);
    } else {
      alert('Không xác định được vai trò người dùng.');
    }
  }
}

}
