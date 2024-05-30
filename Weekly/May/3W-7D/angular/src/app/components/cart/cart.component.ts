import { Component, OnInit } from '@angular/core';
import { CartItem, Product } from 'src/app/interfaces/product.interface';
import { ProductsService } from 'src/app/services/products.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {
  cartList!: CartItem[]
  total!: number
  constructor(private prdSrv: ProductsService) { }
  get totalPrice() {
    return this.cartList.reduce((a,b)=> a+b.totalPrice, 0 )
  }
  deleteFromCart(id:number) {
    this.prdSrv.removeFromCart(id)
  }
  ngOnInit(): void {
    this.prdSrv.cartList.subscribe(cart => this.cartList = cart)
  }

}
