import { Component, OnInit } from '@angular/core';
import { iCars } from '../../modules/cars';

@Component({
  selector: 'app-audi',
  templateUrl: './audi.component.html',
  styleUrls: ['./audi.component.scss']
})
export class AudiComponent implements OnInit {

  audiArray: iCars[] = [];

  ngOnInit() {
    this.getAudi();
  }

  async getAudi() {
    const res = await fetch('../../../assets/db.json');
    const cars: iCars[] = await res.json();
    console.log(cars); 
    this.audiArray = cars.filter(car => car.brand === 'Audi');
    console.log(this.audiArray); // Controlla le auto Audi assegnate
  }
}
