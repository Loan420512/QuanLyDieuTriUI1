import { Component } from '@angular/core';

@Component({
  selector: 'app-patients',
  standalone: true,
  templateUrl: './patients.component.html',
  styleUrls: ['./patients.component.css']
})
export class PatientsComponent {
  patients = [
    { id: 1, name: 'Nguyen Van A', age: 32, gender: 'Male' },
    { id: 2, name: 'Tran Thi B', age: 29, gender: 'Female' },
    { id: 3, name: 'Le Van C', age: 40, gender: 'Male' }
  ];
}
