import { Component, OnInit } from '@angular/core';
import { Product, ProductResponse } from 'src/app/interfaces/product.interface';
import { ProductsService } from 'src/app/services/products.service';

@Component({
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.scss']
})
export class HomepageComponent implements OnInit {
  products!: ProductResponse["products"]
  constructor(private prdSrv: ProductsService) { }

  ngOnInit(): void {
    this.prdSrv.getProducts().subscribe(({ products }: ProductResponse) => this.products = products)
  }

}
