// src/app/services/product.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product } from '../models/product';
import { ResponseModel } from '../models/response.model';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private apiUrl = 'https://localhost:5001/api';
  private httpOptions: any;

  constructor(private http: HttpClient) {
    this.httpOptions = {
      headers:{
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Expose-Headers': '*'
      }
    };
   }

  getProducts(pageNumber: number, pageSize: number): Observable<ResponseModel> {
    return this.http.get<ResponseModel>(`${this.apiUrl}/produto?page=${pageNumber}&size=${pageSize}`, {headers: this.httpOptions.headers});
  }

  getProductById(codigo: number): Observable<ResponseModel> {
    return this.http.get<ResponseModel>(`${this.apiUrl}/produto/${codigo}`, {headers: this.httpOptions.headers});
  }

  createProduct(product: Product): Observable<Product> {
    return this.http.post<Product>(`${this.apiUrl}/produto`, product, {headers: this.httpOptions.headers});
  }

  updateProduct(product: Product): Observable<Product> {
    return this.http.put<Product>(`${this.apiUrl}/produto/${product.codigo}`, product, {headers: this.httpOptions.headers});
  }

  deleteProduct(codigo: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/produto/${codigo}`, {headers: this.httpOptions.headers});
  }
}
