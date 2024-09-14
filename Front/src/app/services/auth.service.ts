import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { User } from '../models/auth.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = `${environment.loginEndpoint}`;

  constructor(private http: HttpClient) { }

  login(username: string, password: string): Observable<User> {
    return this.http.post<User>(this.apiUrl, { username, password });
  }

  setSession(user: User): void {
    sessionStorage.setItem('user', JSON.stringify(user));
  }

  getSession(): User | null {
    return JSON.parse(sessionStorage.getItem('user') || 'null');
  }

  clearSession(): void {
    sessionStorage.removeItem('user');
  }
}
