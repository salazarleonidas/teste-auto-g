import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ProductService } from '../services/product.service';
import { NewProductFormComponent } from '../new-product-form/new-product-form.component';
import { Product } from '../models/product';
import { PageResult } from '../models/page-result';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-products-table',
  templateUrl: './products-table.component.html',
  styleUrls: ['./products-table.component.scss']  
})
export class ProductsTableComponent implements OnInit {
  displayedColumns: string[] = ['codigo', 'descricao', 'situacao', 'dataFabricacao', 'dataValidade', 'acoes'];
  dataSource = new MatTableDataSource<Product>([]);

  pageSize: number = 3;
  totalProducts = 0;
  pageEvent!: PageEvent;

  constructor(private productService: ProductService, private dialog: MatDialog)
  {

  }

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    const pageIndex = this.pageEvent ? this.pageEvent.pageIndex : 1;
    const pageSize = this.pageEvent ? this.pageEvent.pageSize : this.pageSize;

    this.productService.getProducts(pageIndex, pageSize).subscribe((response) => {
      if(response.statusCode == 200){
        this.dataSource.data = response.result.items;
        this.totalProducts = response.result.count;
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
