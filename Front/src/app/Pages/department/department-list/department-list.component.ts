import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { DepartmentService } from '../../../services/department.service';
import { Department } from '../../../models/department.model';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-department-list',
  templateUrl: './department-list.component.html',
  styleUrls: ['./department-list.component.css']
})
export class DepartmentListComponent implements OnInit, AfterViewInit {
  departments: Department[] = [];
  displayedColumns: string[] = ['codigo', 'descricao'];
  dataSource = new MatTableDataSource<Department>(this.departments);

  @ViewChild(MatPaginator) paginator: MatPaginator | undefined;
  @ViewChild(MatSort) sort: MatSort | undefined;

  constructor(private departmentService: DepartmentService) { }

  ngOnInit(): void {
    this.loadDepartments();
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

  loadDepartments(): void {
    this.departmentService.getDepartments().subscribe((data: Department[]) => {
      this.departments = data;
      this.dataSource.data = this.departments;
    }, error => {
      console.error('Erro ao carregar departamentos:', error);
    });
  }
}
