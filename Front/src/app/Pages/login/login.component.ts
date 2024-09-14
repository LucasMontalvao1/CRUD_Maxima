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
    // Inicializa o formulário com validações
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  // Método para tratar o envio do formulário
  onSubmit() {
    if (this.loginForm.valid) {
      const { username, password } = this.loginForm.value;

      // Chama o serviço de autenticação
      this.authService.login(username, password).subscribe(
        (response) => {
          console.log('Resposta do login:', response);

          // Verifica se a resposta é válida
          if (response) {
            console.log('Login bem-sucedido!');
            
            // Armazena as informações do usuário no sessionStorage
            sessionStorage.setItem('user', JSON.stringify(response));

            // Redireciona para a página 'home' após login bem-sucedido
            this.router.navigate(['/home']); 
          } else {
            // Caso o login falhe, exibe uma mensagem de erro
            this.errorMessage = 'Credenciais inválidas. Por favor, tente novamente.';
          }
        },
        (error) => {
          // Trata erros de comunicação ou outros problemas com a API
          console.error('Erro no login:', error);
          this.errorMessage = 'Erro ao realizar login. Por favor, verifique o login e senha.';
        }
      );
    } else {
      // Validação para garantir que os campos de login e senha estão preenchidos
      this.errorMessage = 'Por favor, preencha corretamente o usuário e senha.';
    }
  }
}
