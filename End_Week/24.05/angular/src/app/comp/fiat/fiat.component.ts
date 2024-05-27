import { Component, OnInit } from '@angular/core';
import { iCars } from '../../modules/cars';

@Component({
  selector: 'app-fiat',
  templateUrl: './fiat.component.html',
  styleUrls: ['./fiat.component.scss']
})
export class FiatComponent implements OnInit {

  fiatArray: iCars[] = [];

  ngOnInit() {
    this.getFiat();
  }
  async getFiat() {
    const res = await fetch('../../../assets/db.json');
    const cars: iCars[] = await res.json();
    console.log(cars); 
    this.fiatArray = cars.filter(car => car.brand === 'Fiat');
    console.log(this.fiatArray);
  }
  }