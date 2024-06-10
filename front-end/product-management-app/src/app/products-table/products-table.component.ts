import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ProductService } from '../services/product.service';
import { NewProductFormComponent } from '../new-product-form/new-product-form.component';
import { Product } from '../models/product';

@Component({
  selector: 'app-products-table',
  templateUrl: './products-table.component.html',
  styleUrls: ['./products-table.component.scss']
})
export class ProductsTableComponent implements OnInit {
  displayedColumns: string[] = ['codigo', 'descricao', 'situacao', 'dataFabricacao', 'dataValidade', 'acoes'];
  products: Product[] = [];

  constructor(private productService: ProductService, private dialog: MatDialog) { }

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.productService.getProducts().subscribe((response) => {
      if(response.statusCode == 200){
        this.products = response.result;
      }
    });
  }

  openNewProductForm(): void {
    const dialogRef = this.dialog.open(NewProductFormComponent, {
      width: '600px',
      data: { isEdit: false, product: null }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadProducts();
      }
    });
  }

  editProduct(product: Product): void {
    const dialogRef = this.dialog.open(NewProductFormComponent, {
      width: '600px',
      data: { isEdit: true, product }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadProducts();
      }
    });
  }

  deleteProduct(codigo: number): void {
    this.productService.deleteProduct(codigo).subscribe(() => {
      this.loadProducts();
    });
  }
}
