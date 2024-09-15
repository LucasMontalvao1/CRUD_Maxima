import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatDialog } from '@angular/material/dialog';
import { ProductService } from '../../../services/product.service';
import { Product } from '../../../models/product.model';
import { MatTableDataSource } from '@angular/material/table';
import { ProductDialogComponent } from '../product-dialog/product-dialog.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';
import { FormControl } from '@angular/forms';
import { map, Observable, startWith } from 'rxjs';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit, AfterViewInit {
  products: Product[] = [];
  displayedColumns: string[] = ['codigo', 'descricao', 'preco', 'status', 'department', 'actions'];
  dataSource = new MatTableDataSource<Product>(this.products);

  searchControl = new FormControl();
  filteredOptions: Observable<Product[]> | undefined;

  @ViewChild(MatPaginator) paginator: MatPaginator | undefined;
  @ViewChild(MatSort) sort: MatSort | undefined;

  constructor(
    private productService: ProductService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) { }

  ngOnInit(): void {
    this.loadProducts();

    this.filteredOptions = this.searchControl.valueChanges.pipe(
      startWith(''),
      map(value => this._filter(value))
    );
  }

  ngAfterViewInit(): void {
    if (this.paginator) {
      this.dataSource.paginator = this.paginator;
      this.paginator.pageSize = 5;
      this.paginator.pageIndex = 0;
    }

    if (this.sort) {
      this.dataSource.sort = this.sort;
    }

    this.dataSource.filterPredicate = (data: Product, filter: string) => {
      const dataStr = `${data.codigo} ${data.descricao} ${data.department.descricao}`.toLowerCase();
      return dataStr.includes(filter);
    };
  }


  loadProducts(): void {
    this.productService.getProducts().subscribe((data: Product[]) => {
      this.products = data;
      this.dataSource.data = this.products;
      this.filteredOptions = this.searchControl.valueChanges.pipe(
        startWith(''),
        map(value => this._filter(value))
      );
    }, error => {
      console.error('Erro ao carregar produtos:', error);
      this.snackBar.open('Erro ao carregar produtos.', 'Fechar', { duration: 3000 });
    });
  }

  openProductDialog(product?: Product): void {
    const dialogRef = this.dialog.open(ProductDialogComponent, {
      width: '750px',
      data: { product, reloadProducts: () => this.loadProducts() }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result?.success) {
        this.loadProducts();
      }
    });
  }

  openDeleteDialog(productId: number): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '450px',
      panelClass: 'confirm-dialog'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.deleteProduct(productId);
      }
    });
  }

  deleteProduct(productId: number): void {
    this.productService.deleteProduct(productId).subscribe({
      next: (message) => {
        this.loadProducts();
        this.snackBar.open(message, 'Fechar', { duration: 3000 });
      },
      error: (error) => {
        this.snackBar.open('Não foi possivel deletar, tente novamente..', 'Fechar', { duration: 3000 });
        console.error('Erro ao deletar produto:', error);
      }
    });
  }

  applyFilter(event: Event): void {
    const filterValue = (event.target as HTMLInputElement).value.trim().toLowerCase();
    this.dataSource.filter = filterValue;
  }

  private _filter(value: string): Product[] {
    const filterValue = value.toLowerCase();
    return this.products.filter(option =>
      option.codigo.toLowerCase().includes(filterValue) ||
      option.descricao.toLowerCase().includes(filterValue) ||
      option.department.descricao.toLowerCase().includes(filterValue)
    );
  }

  onAutocompleteSelection(event: any): void {
    const selectedValue = event.option.value;
    this.searchControl.setValue(selectedValue);
    this.applyFilter({ target: { value: selectedValue } } as unknown as Event);
  }
}