import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { HomeComponent } from './pages/home/home.component';
import { ProductDialogComponent } from './pages/product/product-dialog/product-dialog.component';
import { ConfirmDialogComponent } from './pages/product/confirm-dialog/confirm-dialog.component';
import { DepartmentListComponent } from './pages/department/department-list/department-list.component';

const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'home', component: HomeComponent },
  { path: 'product-dialog', component: ProductDialogComponent },
  { path: 'confirm-dialog', component: ConfirmDialogComponent },
  { path: 'department', component: DepartmentListComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
