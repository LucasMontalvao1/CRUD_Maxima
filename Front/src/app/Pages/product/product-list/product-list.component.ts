import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { ProductService } from '../../../services/product.service';
import { Product } from '../../../models/product.model';
import { MatTableDataSource } from '@angular/material/table';

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

  constructor(private productService: ProductService) { }

  ngOnInit(): void {
    this.loadProducts();
  }

  ngAfterViewInit(): void {
    if (this.paginator) {
      this.dataSource.paginator = this.paginator;
      this.paginator.pageSize = 5; // Defina o tamanho da página
      this.paginator.pageIndex = 1;  // Defina o índice inicial da página
    }

    if (this.sort) {
      this.dataSource.sort = this.sort;
    }
  }

  loadProducts(): void {
    this.productService.getProducts().subscribe((data: Product[]) => {
      console.log('Loaded products:', data);
      this.products = data;
      this.dataSource.data = this.products; // Atualize a fonte de dados
      console.log('DataSource data:', this.dataSource.data); // Verifique os dados no dataSource
    });
  }

  deleteProduct(productId: number): void {
    this.productService.deleteProduct(productId).subscribe(() => {
      this.loadProducts(); // Recarrega os produtos após exclusão
    });
  }
}
