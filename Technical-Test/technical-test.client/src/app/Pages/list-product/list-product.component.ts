import { Component } from '@angular/core';
import { PriceUpdateReq, Product, SellReq, StatusRes } from './list-product.interface';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-list-product',
  templateUrl: './list-product.component.html',
  styleUrls: ['./list-product.component.css']
})
export class ListProductComponent {
  products: Product[] = [];
  tempCat: number = 0;
  keyword: string = "";
  priceUpdateList = new Map<number,number>();
  priceUpdateReq: PriceUpdateReq[] = [];

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getProducts(0);
  }

  getProducts(category: number) {
      this.http.get<Product[]>('/api/products', { params: { Category: category }} ).subscribe({
        next: (res:Product[]) => {
         this.products = res;
        },
        error: (err) => {
          console.log(err)
        }
      }
    );
  }

  selectedCategory(event: any){
    this.tempCat = parseInt(event)
    this.http.get<Product[]>('/api/products', { params: { Category: parseInt(event) } }).subscribe({
      next: (res: Product[]) => {
        this.products = res;
        this.keyword = "";
      },
      error: (err) => {
        console.error(err);
      }
    });
  }

  sell(data: SellReq) {
    this.http.post<StatusRes>('/api/products/sell', data).subscribe({
      next: (res:StatusRes) => {
        if(res.status == "Success"){
          this.getProducts(this.tempCat); 
          alert("ขายสําเร็จ")
        }else if(res.status == "NotFound"){
          alert("ไม่มีสินค้าในระบบ")
        }else if(res.status == "QuantityBelowZero"){
          alert("กรุณากรอก Quantity > 0")          
        }else if(res.status == "OutOfStock"){
          alert("stock ไม่เพียงพอ")          
        }
      },
      error: (err) => {
        console.log(err)
        alert("ขายไม่สําเร็จ")
      }
    });
  }

  priceUpdating(data: Map<number,number>){
    this.priceUpdateList = data
  }

  priceUpdate(){
    if(this.priceUpdateList.size <= 0) return;
    let temp: number[] = [...this.priceUpdateList.keys()];
    this.priceUpdateReq = temp.map(o => ({ productId: o, newPrice: this.priceUpdateList.get(o) }));
    console.log(this.priceUpdateReq)
    this.http.put<Product[]>('/api/products/bulk-price-update', this.priceUpdateReq).subscribe({
      next: (res: Product[]) => {
        this.priceUpdateReq = []
        this.priceUpdateList.clear();
        alert("อัปเดตราคาสำเร็จ")
        console.log("result => ", res)
      },
      error: (err) => {
        console.error(err);
      }
    });
  }

  keywordChange(event : any){
    this.keyword = event.target.value
  }

  search(){
    this.http.get<Product[]>('/api/products/search', { params: { keyword: this.keyword, category: this.tempCat } }).subscribe({
      next: (res: Product[]) => {
        this.products = res;
      },
      error: (err) => {
        console.error(err);
      }
    });
  }
}
