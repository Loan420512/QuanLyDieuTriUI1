import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PatientService } from '../../services/patient.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-patient-list',
  templateUrl: './patient-list.component.html',
  styleUrls: ['./patient-list.component.css'],
  standalone: true,
  imports: [CommonModule, FormsModule]
})
export class PatientsComponent implements OnInit {
  bookings: any[] = [];
  selectedBooking: any = null;
  selectedMemberInfo: any = null;
  notifications: any[] = [];

  reminderForm = {
    medicineTime: '',
    appointmentDate: '',
    method: 'email'
  };

  newBooking = {
    dayBooking: '',
    treatmentServiceId: 0,
    doctorId: 0,
    statusBooking: 'Pending',
    createAt: ''
  };

  constructor(
    private patientService: PatientService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.patientService.getMyDoctorInfo().subscribe({
      next: () => this.loadDoctorBookings(),
      error: () => this.router.navigate(['/unauthorized'])
    });
  }
  

  loadDoctorBookings(): void {
    this.patientService.getBookingsByDoctor().subscribe({
      next: (res) => {
        this.bookings = res.data || res || [];

        this.bookings.forEach(b => {
          // 🟦 Bổ sung thông tin Member nếu thiếu
          if (!b.member?.fullName && b.memberId) {
            this.patientService.getMemberById(b.memberId).subscribe({
              next: (res) => {
                b.member = res?.data || { fullName: `BN #${b.memberId}` };
              },
              error: (err) => {
                console.warn('❌ Lỗi lấy Member:', err);
                b.member = { fullName: `BN #${b.memberId}` };
              }
            });
          }

          // 🟦 Bổ sung thông tin Dịch vụ nếu thiếu
          if (!b.treatmentService?.name && b.treatmentServiceId) {
            this.patientService.getTreatmentServiceById(b.treatmentServiceId).subscribe({
              next: (res) => {
                b.treatmentService = res?.data || { name: `Dịch vụ #${b.treatmentServiceId}` };
              },
              error: (err) => {
                console.warn('❌ Lỗi lấy Dịch vụ:', err);
                b.treatmentService = { name: `Dịch vụ #${b.treatmentServiceId}` };
              }
            });
          }
        });
      },
      error: (err) => {
        console.error('❌ Lỗi tải lịch khám:', err);
      }
    });
  }

  selectBooking(booking: any): void {
    this.selectedBooking = booking;
    this.reminderForm = {
      medicineTime: '',
      appointmentDate: '',
      method: 'email'
    };
  }

  cancelReminder(): void {
    this.selectedBooking = null;
  }

  sendReminder(): void {
    if (!this.selectedBooking) return;

    const payload = {
      memberId: this.selectedBooking.member?.memberId,
      email: this.selectedBooking.member?.email,
      medicineTime: this.reminderForm.medicineTime,
      appointmentDate: this.reminderForm.appointmentDate,
      method: this.reminderForm.method
    };

    console.log('📤 Nhắc nhở gửi:', payload);

    this.patientService.sendReminder(payload).subscribe({
      next: () => {
        alert('✅ Đã gửi nhắc nhở!');
        this.cancelReminder();
      },
      error: (err) => {
        console.error('❌ Lỗi gửi nhắc nhở:', err);
        alert('❌ Gửi nhắc nhở thất bại!');
      }
    });
  }

  showMemberDetails(memberId: number): void {
  this.patientService.getMemberById(memberId).subscribe({
    next: (res) => {
      this.selectedMemberInfo = res?.data || null;
    },
    error: () => {
      this.selectedMemberInfo = null;
    }
  });
}
loadNotifications(): void {
  const user = JSON.parse(localStorage.getItem('currentUser') || '{}');
  const userId = user.userId;

  this.patientService.getMyNotifications(userId).subscribe({
    next: (res) => {
      this.notifications = res.data || res;
      console.log('🔔 Notifications:', this.notifications);
    },
    error: (err) => {
      console.error('❌ Lỗi tải thông báo:', err);
    }
  });
}
}
