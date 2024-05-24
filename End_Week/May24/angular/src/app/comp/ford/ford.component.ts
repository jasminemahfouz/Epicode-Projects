import { Component, OnInit } from '@angular/core';
import { iCars } from '../../modules/cars';

@Component({
  selector: 'app-ford',
  templateUrl: './ford.component.html',
  styleUrls: ['./ford.component.scss']
})
export class FordComponent implements OnInit {

  fordArray: iCars[] = [];

  ngOnInit() {
    this.getFord();
  }

  async getFord() {
    const res = await fetch('../../../assets/db.json');
    const cars: iCars[] = await res.json();
    console.log(cars); 
    this.fordArray = cars.filter(car => car.brand === 'Ford');
    console.log(this.fordArray); 
  }
}
