import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {
  name: string = '';

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    const sessionUser = this.authService.getSession();
    console.log('User from session:', sessionUser);

    if (sessionUser) {
      this.name = sessionUser.user.name;
      console.log('Name in component:', this.name);
    }
  }
}