import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { User } from '../models/auth.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = `${environment.endpoints.login}`;

  constructor(private http: HttpClient) { }

  login(username: string, password: string): Observable<User> {
    return this.http.post<User>(this.apiUrl, { username, password })
      .pipe(
        tap(user => this.setSession(user)),
        catchError(this.handleError)
      );
  }

  public setSession(user: User): void {
    // sessionStorage.setItem('token', user.token);
    sessionStorage.setItem('user', JSON.stringify(user));
  }

  getSession(): User | null {
    const user = sessionStorage.getItem('user');
    return user ? JSON.parse(user) : null;
  }

  clearSession(): void {
    sessionStorage.removeItem('user');
    // sessionStorage.removeItem('token');
  }

  isAuthenticated(): boolean {
    return !!this.getSession();
  }

  private handleError(error: HttpErrorResponse): Observable<never> {
    let errorMessage = 'Ocorreu um erro desconhecido!';

    if (error.error instanceof ErrorEvent) {
      // Erro do lado do cliente
      errorMessage = `Erro: ${error.error.message}`;
    } else {
      // Erro do lado do servidor
      switch (error.status) {
        case 401:
          errorMessage = 'Login ou senha incorretos';
          break;

        default:
          errorMessage = `Código do Erro: ${error.status}\nMensagem: ${error.message}`;
          break;
      }
    }

    return throwError(errorMessage);
  }

}
