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
}
