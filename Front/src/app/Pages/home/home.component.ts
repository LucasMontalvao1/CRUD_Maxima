import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  username: string = '';

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    const user = this.authService.getSession();
    console.log('User from session:', user);
    if (user) {
      this.username = user.username;
    }
  }
}
