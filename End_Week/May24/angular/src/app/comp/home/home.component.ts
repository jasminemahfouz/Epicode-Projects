import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { iCars } from '../../modules/cars';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  allCars: iCars[] = [];
  randomCars: iCars[] = [];

  constructor(private router: Router) {}

  ngOnInit() {
    this.getAllCars();
  }

  async getAllCars() {
    const res = await fetch('../../../assets/db.json');
    this.allCars = await res.json();
    this.selectRandomCars();
  }

  navigateToBrandPage(brand: string) {
    const brandPath = brand.toLowerCase();
    this.router.navigate([`/brands/${brandPath}`]);
  }

  selectRandomCars() {
    const randomIndices: number[] = [];
    while (randomIndices.length < 2) {
      const randomIndex = Math.floor(Math.random() * this.allCars.length);
      if (!randomIndices.includes(randomIndex)) {
        randomIndices.push(randomIndex);
      }
    }
    this.randomCars = randomIndices.map(index => this.allCars[index]);
  }
}
