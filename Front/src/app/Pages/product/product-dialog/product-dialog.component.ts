import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ProductService } from '../../../services/product.service';
import { DepartmentService } from '../../../services/department.service';
import { Department } from '../../../models/department.model';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-product-dialog',
  templateUrl: './product-dialog.component.html',
  styleUrls: ['./product-dialog.component.css']
})
export class ProductDialogComponent implements OnInit {
  productForm!: FormGroup;
  departments: Department[] = [];
  isEditMode: boolean = false;

  constructor(
    private fb: FormBuilder,
    private productService: ProductService,
    private departmentService: DepartmentService,
    private dialogRef: MatDialogRef<ProductDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private snackBar: MatSnackBar
  ) { }

  ngOnInit(): void {
    this.productForm = this.fb.group({
      codigo: ['', Validators.required],
      descricao: ['', Validators.required],
      preco: ['', [Validators.required, this.precoValidator]],
      status: [true],
      codigodepartamento: ['', Validators.required]
    });

    this.loadDepartments();

    if (this.data?.product?.id) {
      this.isEditMode = true;
      this.productService.getProductById(this.data.product.id).subscribe(product => {
        this.productForm.patchValue({
          codigo: product.codigo,
          descricao: product.descricao,
          preco: product.preco,
          status: product.status,
          codigodepartamento: product.department?.codigo || product.department.codigo
        });
        console.log('Produto carregado para edição:', product);
      }, error => {
        console.error('Erro ao carregar produto:', error);
        this.snackBar.open('Erro ao carregar produto.', 'Fechar', { duration: 3000 });
      });
    }
  }

  precoValidator(control: FormControl): { [key: string]: boolean } | null {
    if (control.value <= 0) {
      return { 'precoInvalido': true };
    }
    return null;
  }

  loadDepartments(): void {
    this.departmentService.getDepartments().subscribe(departments => {
      this.departments = departments;
      console.log('Departamentos carregados:', departments);

      if (this.isEditMode) {
        this.setInitialDepartment();
      }
    }, error => {
      this.snackBar.open('Erro ao carregar departamentos.', 'Fechar', { duration: 3000 });
      console.error(error);
    });
  }

  setInitialDepartment(): void {
    const departmentCode = this.data?.product?.codigodepartamento;
    if (departmentCode) {
      const department = this.departments.find(d => d.codigo === departmentCode);
      if (department) {
        this.productForm.patchValue({
          codigodepartamento: department.codigo
        });
      } else {
        this.departmentService.getDepartmentByCode(departmentCode).subscribe(department => {
          this.productForm.patchValue({
            codigodepartamento: department.codigo
          });
        }, error => {
          this.snackBar.open('Erro ao carregar o departamento.', 'Fechar', { duration: 3000 });
          console.error(error);
        });
      }
    }
  }

  onSave(): void {
    if (this.productForm.invalid) {
      return;
    }

    const productData = {
      ...this.productForm.value,
      id: this.data?.product?.id
    };

    if (this.isEditMode) {
      this.productService.updateProduct(this.data.product.id, productData).subscribe(() => {
        this.snackBar.open('Produto atualizado com sucesso!', 'Fechar', { duration: 3000 });
        this.dialogRef.close({ success: true });
      }, error => {
        this.snackBar.open('Erro ao atualizar o produto.', 'Fechar', { duration: 3000 });
        console.error(error);
      });
    } else {
      this.productService.createProduct(productData).subscribe(() => {
        this.snackBar.open('Produto criado com sucesso!', 'Fechar', { duration: 3000 });
        this.dialogRef.close({ success: true });
      }, error => {
        this.snackBar.open('Erro ao criar o produto.', 'Fechar', { duration: 3000 });
        console.error(error);
      });
    }
  }

  onCancel(): void {
    this.dialogRef.close({ success: false });
  }

  setStatus(isActive: boolean): void {
    this.productForm.patchValue({ status: isActive });
  }
}
