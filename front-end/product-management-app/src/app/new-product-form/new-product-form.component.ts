import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProductService } from '../services/product.service';
import { Product, Supplier } from '../models/product';

@Component({
  selector: 'app-new-product-form',
  templateUrl: './new-product-form.component.html',
  styleUrls: ['./new-product-form.component.scss']
})
export class NewProductFormComponent implements OnInit {
  productForm!: FormGroup;
  supplierForm!: FormGroup;
  isEdit: boolean = false;
  showAddSupplierForm: boolean = false;
  suppliers: Supplier[] = [];
  supplierColumns: string[] = ['codigo', 'descricao', 'cnpj', 'acoes'];

  constructor(
    private fb: FormBuilder,
    private productService: ProductService,
    public dialogRef: MatDialogRef<NewProductFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { isEdit: boolean, product: Product }
  ) { }

  ngOnInit(): void {
    this.isEdit = this.data.isEdit;

    this.productForm = this.fb.group({
      codigo: [{value: this.data.product?.codigo || null, disabled: true}],
      descricao: [this.data.product?.descricao || '', Validators.required],
      situacao: [this.data.product?.situacao || true],
      data_fabricacao: [this.data.product?.data_fabricacao || '', Validators.required],
      data_validade: [this.data.product?.data_validade || '', Validators.required],
      fornecedores: [this.data.product?.fornecedores || []]
    });

    this.suppliers = this.data.product?.fornecedores || [];
    
    this.supplierForm = this.fb.group({
      descricao: ['', Validators.required],
      cnpj: ['', Validators.required],
    });
  }

  toggleAddSupplierForm() {
    this.showAddSupplierForm = !this.showAddSupplierForm;
  }

  addSupplier(): void {    
    if (this.supplierForm.valid) {
      const newSupplier: Supplier = this.supplierForm.value;
      this.suppliers.push(newSupplier);
      this.supplierForm.reset(); // Reset form fields
      this.toggleAddSupplierForm(); // Hide add supplier form
    }
  }
  cancelAddSupplier() {
    this.supplierForm.reset(); // Reset form fields
    this.toggleAddSupplierForm(); // Hide add supplier form
  }

  deleteSupplier(cnpj: string): void {
    this.suppliers = this.suppliers.filter(supplier => supplier.cnpj !== cnpj);
  }

  onSave(): void {
    if (this.productForm.valid) {
      const product: Product = this.productForm.value;
      if(this.data.isEdit) product.codigo= this.data.product.codigo;
      product.fornecedores = this.suppliers;
      if (this.data.product) {
        this.productService.updateProduct(product).subscribe(() => {
          this.dialogRef.close(true);
        });
      } else {
        this.productService.createProduct(product).subscribe(() => {
          this.dialogRef.close(true);
        });
      }
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
