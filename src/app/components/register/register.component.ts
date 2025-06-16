// register component sample

import { Component } from '@angular/core';
import { ApiService } from '../../../services/api.service';



@Component({
  selector: 'app-register',
  template: `
    <h2>Register</h2>
    <input [(ngModel)]="fullName" placeholder="Full Name" />
    <input [(ngModel)]="email" placeholder="Email" />
    <input [(ngModel)]="password" placeholder="Password" type="password" />
    <button (click)="register()">Register</button>
  `
})
export class RegisterComponent {
  fullName = '';
  email = '';
  password = '';

  constructor(private api: ApiService) {}

  register() {
    this.api.register({
      fullName: this.fullName,
      email: this.email,
      password: this.password
    }).subscribe({
      next: res => alert('Registered'),
      error: err => alert('Failed')
    });
  }
}
