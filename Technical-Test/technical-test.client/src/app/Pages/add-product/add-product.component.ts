import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  AbstractControl,
} from '@angular/forms';
import { Product, ProductRes } from './add-product.interface';
import { HttpClient } from '@angular/common/http';

interface Category {
  id: number;
  name: string;
}

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css'],
})
export class AddProductComponent implements OnInit {
  productForm: FormGroup;
  products: Product[] = [];

  constructor(private fb: FormBuilder, private http: HttpClient) {
    this.productForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(3)]],
      sku: ['', [Validators.required, Validators.minLength(3), this.skuUniqueValidator.bind(this)]],
      price: [0, [Validators.required, Validators.min(0.01)]],
      stock: [0, [Validators.required, Validators.min(0)]],
      categoryId: [null, Validators.required],
    });
  }

  ngOnInit(): void {
    this.getProducts();
  }

  skuUniqueValidator(control: AbstractControl) {
    const exists = this.products.some((p) => p.sku === control.value);
    return exists ? { skuTaken: true } : null;
  }

  addProduct() {
    if (this.productForm.invalid) return;

    const newProduct: Product = this.productForm.getRawValue();

    this.http.post<ProductRes>('/api/products', newProduct).subscribe({
      next: (res: ProductRes) => {
        console.log("response: ", res)
        this.productForm.reset();
        // reset default values if needed
        this.productForm.patchValue({ price: 0, stock: 0 });
      },
      error: (err) => {
        console.error(err);
      },
    });
  }

  get name() {
    return this.productForm.get('name');
  }
  get sku() {
    return this.productForm.get('sku');
  }
  get price() {
    return this.productForm.get('price');
  }
  get stock() {
    return this.productForm.get('stock');
  }
  get categoryId() {
    return this.productForm.get('categoryId');
  }

  selectedCategory(id: number) {
    this.productForm.get('categoryId')?.setValue(id);
  }

  getProducts() {
    this.http.get<Product[]>('/api/products').subscribe({
      next: (res: Product[]) => {
        this.products = res;
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
}
