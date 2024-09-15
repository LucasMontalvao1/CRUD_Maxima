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

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit, AfterViewInit {
  products: Product[] = [];
  displayedColumns: string[] = ['codigo', 'descricao', 'preco', 'status', 'department', 'actions'];
  dataSource = new MatTableDataSource<Product>(this.products);

  @ViewChild(MatPaginator) paginator: MatPaginator | undefined;
  @ViewChild(MatSort) sort: MatSort | undefined;

  constructor(
    private productService: ProductService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) { }

  ngOnInit(): void {
    this.loadProducts();
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
  }

  loadProducts(): void {
    this.productService.getProducts().subscribe((data: Product[]) => {
      this.products = data;
      this.dataSource.data = this.products;
    }, error => {
      console.error('Erro ao carregar produtos:', error);
      this.snackBar.open('Erro ao carregar produtos.', 'Fechar', { duration: 3000 });
    });
  }

  openProductDialog(product?: Product): void {
    const dialogRef = this.dialog.open(ProductDialogComponent, {
      width: '700px',
      data: { product, reloadProducts: () => this.loadProducts() }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result?.success) {
        this.loadProducts(); // Recarrega a lista de produtos após criação/edição
      }
    });
  }

  openDeleteDialog(productId: number): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '400px',
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
        this.snackBar.open('Erro ao deletar o produto.', 'Fechar', { duration: 3000 });
        console.error('Erro ao deletar produto:', error);
      }
    });
  }
}
