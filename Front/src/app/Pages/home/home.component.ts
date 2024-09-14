import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {
  userName: string = '';

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    const user = this.authService.getSession();
    this.userName = user ? user.name : 'Nome nao carregou';
  }

  logout(): void {
    this.authService.clearSession();
  }
}
