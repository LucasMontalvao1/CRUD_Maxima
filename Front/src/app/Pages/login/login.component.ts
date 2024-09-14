import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm: FormGroup;
  isLoading: boolean = false;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private authService: AuthService,
    private snackBar: MatSnackBar
  ) {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.loginForm.valid) {
      const { username, password } = this.loginForm.value;
      this.isLoading = true;

      this.authService.login(username, password).subscribe(
        response => {
          this.isLoading = false;
          this.authService.setSession(response);
          this.router.navigate(['/home']);
          this.openSnackBar('Login realizado com sucesso!');
        },
        error => {
          this.isLoading = false;
          this.openSnackBar('Login ou senha incorretos.');
        }
      );
    } else {
      this.openSnackBar('Por favor, preencha corretamente o usuário e senha.');
    }
  }

  openSnackBar(message: string,) {
    const snackBarConfig = {
      duration: 3000
    };
    this.snackBar.open(message, 'Fechar', snackBarConfig);
  }
}
