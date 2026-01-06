export interface Category {
  id: number;
  categoryName: string;
}

export interface Product {
  id: number;
  name: string;
  sku: string;
  price: number;
  stock: number;
  category?: string;
  categoryId?: number
}

export interface SellReq {
  id: number;
  quantity: number;
}
export interface PriceUpdateReq {
  productId: number;
  newPrice?: number;
}

export interface StatusRes {
  status: string;
}

