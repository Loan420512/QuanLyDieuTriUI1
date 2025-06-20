import { Component, AfterViewInit } from '@angular/core';

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

export class HomeComponent implements AfterViewInit {

  ngAfterViewInit(): void {
    if (AOS) {
      AOS.init();
    }

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

    if (PureCounter) {
      new PureCounter();
    }
    document.querySelectorAll('[data-bs-toggle="tab"]').forEach(tab => {
      new bootstrap.Tab(tab); // 👈 khởi tạo các tab
    });
    document.querySelectorAll('[data-bs-toggle="tab"]').forEach(tab => {
    new bootstrap.Tab(tab);
  });
  }
}
