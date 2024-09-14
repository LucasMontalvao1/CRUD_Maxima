import { Department } from "./department.model";

export interface Product {
  id: number;
  codigo: string;
  descricao: string;
  preco: number;
  status: boolean;
  department: Department;
}