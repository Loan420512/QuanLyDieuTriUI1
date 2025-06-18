import { Component, AfterViewInit } from '@angular/core';

declare var AOS: any;
declare var Swiper: any;
declare var PureCounter: any;

@Component({
  selector: 'app-home',
  standalone: true,
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements AfterViewInit {
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
  }
}
