import { Component, OnInit } from '@angular/core';
import { interval } from 'rxjs';

@Component({
  selector: 'app-carousel',
  templateUrl: './carousel.component.html',
  styleUrls: ['./carousel.component.css'],
})
export class CarouselComponent implements OnInit {
  public activeImageIndex: number = 0;
  public imageSrcs: string[] = [
    '../../assets/IMG_9299.webp',
    '../../assets/p268_orig.png',
    '../../assets/Reshape-2_20190215131519_20200117112648_20220725181723_20220924183900.jpeg',
  ];

  ngOnInit() {
    setInterval(() => {
      if (this.activeImageIndex < 2) {
        this.activeImageIndex++;
      } else this.activeImageIndex = 0;
    }, 5000);
  }

  onPrevious() {
    if (this.activeImageIndex === 0) {
      this.activeImageIndex = 2;
    } else {
      this.activeImageIndex--;
    }
  }

  onNext() {
    if (this.activeImageIndex === 2) {
      this.activeImageIndex = 0;
    } else {
      this.activeImageIndex++;
    }
  }
}
