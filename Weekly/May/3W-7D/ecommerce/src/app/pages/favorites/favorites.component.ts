import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/interfaces/product.interface';
import { ProductsService } from 'src/app/services/products.service';

@Component({
  templateUrl: './favorites.component.html',
  styleUrls: ['./favorites.component.scss']
})
export class FavoritesComponent implements OnInit {
  favs!: Product[]
  constructor(public prdSrv: ProductsService) {}

  ngOnInit(): void {
    this.prdSrv.favList.subscribe((favs:Product[]) => {
      this.favs = favs
    })
  }
  removeFromFav(id:number) {
    this.prdSrv.removeFromFav(id)
  }
}
