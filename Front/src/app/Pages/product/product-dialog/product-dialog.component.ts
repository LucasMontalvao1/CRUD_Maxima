import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ProductService } from '../../../services/product.service';
import { DepartmentService } from '../../../services/department.service';
import { Department } from '../../../models/department.model';
import { Product } from '../../../models/product.model';
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
    this.isEditMode = !!this.data?.product;

    this.productForm = this.fb.group({
      codigo: [this.data?.product?.codigo || '', Validators.required],
      descricao: [this.data?.product?.descricao || '', Validators.required],
      preco: [this.data?.product?.preco || '', [Validators.required, this.precoValidator]],
      status: [this.data?.product?.status || true],
      codigodepartamento: [this.data?.product?.codigodepartamento || '', Validators.required],
    });

    console.log('Dados do produto no diálogo:', this.data?.product); // Log para verificar os dados
    this.loadDepartments();
  }

  precoValidator(control: FormControl): { [key: string]: boolean } | null {
    if (control.value === 0) {
      return { 'precoInvalido': true };
    }
    return null;
  }

  loadDepartments(): void {
    this.departmentService.getDepartments().subscribe((departments) => {
      console.log('Departamentos carregados:', departments); // Log para verificar os departamentos
      this.departments = departments;
      this.setInitialDepartment(); // Define o departamento após o carregamento
    }, error => {
      this.snackBar.open('Erro ao carregar departamentos.', 'Fechar', { duration: 3000 });
      console.error(error);
    });
  }

  setInitialDepartment(): void {
    const departmentCode = this.data?.product?.codigodepartamento;
    console.log('Código do departamento a ser definido:', departmentCode);

    if (departmentCode) {
      // Se a lista de departamentos não contiver o departamento, faça a chamada para obter um departamento específico
      const department = this.departments.find(d => d.codigo === departmentCode);

      if (department) {
        // Departamento já está na lista, defina o valor do campo
        this.productForm.patchValue({
          codigodepartamento: department.codigo
        });
      } else {
        // Se o departamento não estiver na lista, faça uma chamada separada para obter o departamento
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

    const productData = this.productForm.value;
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
