import { Component, OnInit } from '@angular/core';
import { iCars } from '../../modules/cars';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  featuredCars: iCars[] = [];

  ngOnInit() {
    this.getFeaturedCars();
  }

  async getFeaturedCars() {
    const res = await fetch('../../../assets/db.json');
    const cars: iCars[] = await res.json();
    
    // Ordina le auto per prezzo in modo decrescente
    const sortedCars = cars.sort((a, b) => b.price - a.price);

    // Seleziona le prime due auto come macchine in evidenza
    this.featuredCars = sortedCars.slice(0, 2);
  }

}
