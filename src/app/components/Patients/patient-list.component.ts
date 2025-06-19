import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-patient',
  templateUrl: './patient-list.component.html',
  styleUrls: ['./patient-list.component.css'],
  imports: [CommonModule]
})
export class PatientsComponent {
  patients = [
    { id: 1, name: 'Nguyen Van A', age: 32, gender: 'Male' },
    { id: 2, name: 'Tran Thi B', age: 29, gender: 'Female' },
    { id: 3, name: 'Le Van C', age: 40, gender: 'Male' }
  ];
}
