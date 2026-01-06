export interface Product {
  id: number;
  name: string;
  sku: string;
  price: number;
  stock: number;
  categoryId: number;
}

export interface ProductRes {
  id: number;
  name: string;
  sku: string;
  price: number;
  stock: number;
  categoryId: number;
  createdAt: Date;
}
