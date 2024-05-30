import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http"
import { CartItem, Product, ProductResponse } from '../interfaces/product.interface';
import { Observable, Observer } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {
  private url: string = "https://dummyjson.com/products"
  cart: CartItem[] = []
  favs: Product[] = []
  constructor(private http: HttpClient) { }
  //GET * PRODS
  getProducts() {
    return this.http.get<ProductResponse>(this.url)
  }
  //CARRELLO
  addToCart(prod: Product) {
    const found = this.cart.find(prd => prd.id === prod.id)
    if (found) {
      found!.amount += 1
      found.totalPrice = found.price * found.amount

    } else this.cart.push({ ...prod, amount: 1, totalPrice: prod.price })
  }
  removeFromCart(id: number) {
    const index = this.cart.findIndex(prd => prd.id === id)
    if(this.cart[index].amount === 1) {
      this.cart.splice(index, 1)
    } else {
      this.cart[index].amount--
      this.cart[index].totalPrice = this.cart[index].price * this.cart[index].amount
    }
  }
  get cartList() {
    return new Observable((obs: Observer<CartItem[]>) => {
      obs.next(this.cart)
    })
  }
  //FAVS
  addToFavs(prod: Product) {
    const found = this.favs.find(prd => prd.id === prod.id)
    if (!found) {
      this.favs.push(prod)
    }
  }

  removeFromFav(id: number) {
    const index = this.favs.findIndex(el => el.id === id)
    this.favs.splice(index, 1)
  }

  get favList(){
    return new Observable((obs: Observer<Product[]>) => {
      obs.next(this.favs)
    })

  }

  isFav(id: number) {
    return this.favs.find(prd => prd.id === id)
  }

}
