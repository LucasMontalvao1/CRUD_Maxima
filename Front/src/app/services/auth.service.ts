import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environments';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = environment.loginEndpoint; // URL da API para login

  constructor(private http: HttpClient, private router: Router) { }

  // Método para realizar o login
  login(username: string, password: string): Observable<any> {
    const body = { username, password };
    return this.http.post<any>(`${this.apiUrl}`, body, { responseType: 'text' as 'json' });
  }

  // Método para realizar o logout
  logout(): void {
    sessionStorage.removeItem('user'); // Remove os dados do usuário
    this.router.navigate(['/login']);  // Redireciona para a página de login
  }
}
