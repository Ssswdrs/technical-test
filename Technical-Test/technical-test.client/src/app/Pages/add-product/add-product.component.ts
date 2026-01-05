import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';
import { Product } from './add-product.interface';

interface Category {
  id: number;
  name: string;
}

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css']
})
export class AddProductComponent implements OnInit {
  productForm: FormGroup;
  products: Product[] = [];
  categories: Category[] = [
    { id: 1, name: 'เครื่องดื่ม' },
    { id: 2, name: 'อาหาร' },
    { id: 3, name: 'อุปกรณ์' }
  ];

  constructor(private fb: FormBuilder) {
    this.productForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(3)]],
      sku: ['', [Validators.required, this.skuUniqueValidator.bind(this)]],
      price: [0, [Validators.required, Validators.min(0.01)]],
      stock: [0, [Validators.required, Validators.min(0)]],
      categoryId: [null, Validators.required]
    });
  }

  ngOnInit(): void {}

  // Custom validator for SKU uniqueness
  skuUniqueValidator(control: AbstractControl) {
    const exists = this.products.some(p => p.sku === control.value);
    return exists ? { skuTaken: true } : null;
  }

  addProduct() {
    if (this.productForm.invalid) return;

    const newProduct: Product = {
      id: this.products.length + 1,
      ...this.productForm.value
    };

    this.products.push(newProduct);
    this.productForm.reset();
    // reset default values if needed
    this.productForm.patchValue({ price: 0, stock: 0 });
  }

  // Helpers for template
  get name() { return this.productForm.get('name'); }
  get sku() { return this.productForm.get('sku'); }
  get price() { return this.productForm.get('price'); }
  get stock() { return this.productForm.get('stock'); }
  get categoryId() { return this.productForm.get('categoryId'); }

  selectedCategory(id: number){
    this.productForm.get("categoryId")?.setValue(id)
  }
}
