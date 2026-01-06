import { Component, EventEmitter, Input, OnChanges, Output } from '@angular/core';
import { Product } from './product-list.interface';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnChanges {
  @Input() data: Product[] = []; 
  @Output() sellClick = new EventEmitter();
  @Output() priceChange = new EventEmitter();
  sellQuantity = new Map<number,number>();
  priceUpdate = new Map<number,number>();

  ngOnChanges(): void {
    this.data.forEach(o => {
      this.sellQuantity.set(o.id, o.stock > 0 ? 1 : 0)
    })
  }
  
  get totalPrice(): number {
    if (!this.data) return 0;
    return this.data.reduce((sum, item) => sum + item.price, 0);
  }

  get totalStock(): number {
    if (!this.data) return 0;
    return this.data.reduce((sum, item) => sum + item.stock, 0);
  }

  get totalValue(): number {
    if (!this.data) return 0;
    return this.data.reduce((sum, item) => sum + (item.price * item.stock), 0);
  }

  sell(id: number){
    const quantityToSell = this.sellQuantity.get(id) || 0;
    if(quantityToSell <= 0){
      window.alert("กรุณาเลือกจำนวนที่จะขายมากกว่า 0");
      return;
    }
    console.log(this.sellQuantity)
    this.sellClick.emit({ id, quantity: quantityToSell });
  }


  changeSellQuantity(event :any, id: number){
    let quantity = parseInt(event.target.value);
    if(this.sellQuantity.has(id)){
      this.sellQuantity.set(id, quantity)
    }

  }
  changePrice(event :any, id: number){
    let price = parseFloat(event.target.value);
    this.priceUpdate.set(id, price)
    this.priceChange.emit(this.priceUpdate)
  }
}
