import { Component, Input, OnInit } from '@angular/core';
import { Product } from 'src/app/interfaces/product.interface';
import { ProductsService } from 'src/app/services/products.service';

@Component({
  selector: 'app-single-product',
  templateUrl: './single-product.component.html',
  styleUrls: ['./single-product.component.scss']
})
export class SingleProductComponent{
  @Input() product!: Product
  constructor(private prdSrv: ProductsService) { }


  addToCart(prd:Product) {
    this.prdSrv.addToCart(prd)
  }

  addToFavs(prd:Product) {
    this.prdSrv.addToFavs(prd)
  }
  isFav(id:number) {
   return this.prdSrv.isFav(id)
  }

}
