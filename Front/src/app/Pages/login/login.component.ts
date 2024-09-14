import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  loginForm: FormGroup;
  errorMessage: string = '';

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private authService: AuthService
  ) {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.loginForm.valid) {
      const { username, password } = this.loginForm.value;

      this.authService.login(username, password).subscribe(
        (response) => {
          console.log('Resposta do login:', response);

          if (response) {
            console.log('Login bem-sucedido!');
            this.router.navigate(['/home']); 
          } else {
            this.errorMessage = 'Credenciais inválidas. Por favor, tente novamente.';
          }
        },
        (error) => {
          console.error('Erro no login:', error);
          this.errorMessage = 'Erro ao realizar login. Por favor, verifique o login e senha.';
        }
      );
    } else {
      this.errorMessage = 'Por favor, preencha corretamente o usuário e senha.';
    }
  }
}