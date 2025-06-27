import { Component, AfterViewInit, OnInit } from '@angular/core';
import { DoctorService } from '../../services/doctor.service';

declare var AOS: any;
declare var Swiper: any;
declare var PureCounter: any;
declare var bootstrap: any;

@Component({
  selector: 'app-home',
  standalone: true,
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, AfterViewInit {
  doctors: any[] = [];

  constructor(private doctorService: DoctorService) {}

  ngOnInit(): void {
    this.doctorService.getDoctors().subscribe({
      next: (data) => {
        this.doctors = data;
      },
      error: (err) => {
        console.error('Failed to load doctors:', err);
      }
    });
  }

  ngAfterViewInit(): void {
    if (AOS) AOS.init();

    if (Swiper) {
      new Swiper('.init-swiper', {
        loop: true,
        speed: 600,
        autoplay: { delay: 5000 },
        slidesPerView: 'auto',
        pagination: {
          el: '.swiper-pagination',
          type: 'bullets',
          clickable: true
        }
      });
    }

    if (PureCounter) new PureCounter();

    document.querySelectorAll('[data-bs-toggle="tab"]').forEach(tab => {
      new bootstrap.Tab(tab);
    });
  }
}
