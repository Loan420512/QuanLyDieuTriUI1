import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',  // Ensure path is correct
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  fullName: string = '';
  email: string = '';
  password: string = '';
  confirmPassword: string = '';

  // Define the onSubmit method to handle form submission
  onSubmit() {
    if (this.password !== this.confirmPassword) {
      alert("Passwords do not match!");
      return;
    }

    const formData = {
      fullName: this.fullName,
      email: this.email,
      password: this.password
    };

    console.log("Form submitted:", formData);
  }
}
