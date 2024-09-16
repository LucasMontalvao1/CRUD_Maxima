# CRUD de Produtos com Angular e .NET

# Resumo
Este projeto é um CRUD (Create, Read, Update, Delete) de produtos, utilizando Angular para o frontend e .NET para o backend. O banco de dados MySQL armazena os dados dos produtos e logs.

## Tecnologias Utilizadas

- **Backend:** .NET 8.0.401
- **Frontend:** Angular 18
- **Banco de Dados:** MySQL 8.0
- **Node.js:** Versão 18.19.1 
- **NPM:** Versão 10.8.2

## Pré-requisitos

Antes de começar, certifique-se de ter instalado:

1. **.NET SDK** 8.0 ou superior - [Instalar aqui](https://dotnet.microsoft.com/download)
2. **Node.js** 18 ou superior - [Instalar aqui](https://nodejs.org/)
3. **Angular CLI** 18 - [Instalar aqui](https://angular.io/cli)
4. **MySQL** 8.0 ou superior - [Instalar aqui](https://dev.mysql.com/downloads/mysql/)

## Instalação

### Backend (.NET Core)

Clone o repositorio:

 - Navegue até a pasta desejada abra o terminal de seguinte comando: 

```bash
git clone https://github.com/LucasMontalvao1/CRUD_Maxima.git
```
    
Quando o repositorio for clonado, as estrutura da pasta ficara assim:  

```plaintext
.
├── api/                 # Backend da API
├── api.Tests/           # Testes da API
├── front/               # Frontend Angular
├── database/            # Scripts de banco de dados
│   ├── create_tables.sql  # Script para criação das tabelas
│   ├── insert_data.sql    # Script para inserção de dados
│   └── triggers.sql       # Script para triggers
└── README.md             # Instruções detalhadas
```

## Configurando o Banco de Dados

## Scripts SQL

Os scripts SQL utilizados para criar as tabelas e triggers estão localizados na pasta `database/`. Para facilitar, siga os passos da seção "Configurando o Banco de Dados".

### Certifique-se de que o MySQL está rodando em sua máquina.

- Crie um banco de dados chamado **`crud_maxima`**:

```
CREATE DATABASE crud_maxima;
```

### Execute os arquivos SQL na seguinte ordem para criar as tabelas, inserir dados e adicionar triggers:

### 1. Criação das Tabelas:
- Execute o script de criação das tabelas com o comando:

```bash
mysql -u root -p crud_maxima < ./database/create_tables.sql
```

### 2. Inserção de Dados de Exemplo:
- Insira os dados de exemplo com o comando:

```bash
mysql -u root -p crud_maxima < ./database/insert_data.sql
```

### 3. Adição das Triggers:
- Se necessario, adicione as triggers de log com o comando:

```bash
mysql -u root -p crud_maxima < ./database/triggers.sql
```

### 4. Verifique se as tabelas e triggers foram criadas corretamente executando as consultas apropriadas.

    ```
    -- Verificar tabelas
    SHOW TABLES;

    -- Verificar triggers
    SHOW TRIGGERS;
    ```

### Notas:

- Certifique-se de que o banco de dados crud_maxima foi criado antes de executar esses scripts.
- Será solicitado a senha do usuário root ao executar os comandos.

## Configurando o Backend (.NET)

### Vá para a pasta aonde clonou o repositorio:

```bash
cd API/
```

### Restaure as dependências e compile o projeto:

```bash
dotnet restore
dotnet build
```

### Configure a string de conexão do banco de dados no arquivo **`appsettings.json`**:

```json
{
  "ConnectionStrings": {
    "ConfiguracaoPadrao": "server=localhost;userid=root;password=asd123;database=crud_maxima"
  },
}
```

### Execute a aplicação backend:

```bash
dotnet run
```

## Configurando o Frontend (Angular)

### Vá para a pasta **`front`**:

```bash
cd front/
```

### Instale as dependências do Angular:

```bash
npm install
```

### Verifique se está instalado o Angular CLI, se nao, instale com o comando abaixo:

```bash
npm install -g @angular/cli@18
```

### Configure o arquivo **`environment.ts`** com a URL do backend:

```typescript
const apiUrl = 'https://localhost:7263';

export const environment = {
  production: false,
  apiUrl: apiUrl,
  endpoints: {
    login: `${apiUrl}/Api/login`,
    products: `${apiUrl}/Api/products`,
    departments: `${apiUrl}/Api/departments`,
  },
};
```

### Execute a aplicação frontend:

```bash
ng serve
```

##  Acessando a Aplicação

**FRONTEND** - http://localhost:4200/
**BACKEND** - https://localhost:7263/swagger/index.html

## Login e Senha
- Se os insert foram inseridos com sucesso, utilize as seguintes credenciais para acessar o aplicativo:

- **username:** `lucas`
- **Senha:** `123`


## Logs e Monitoramento

### Logs de Eventos 

- Foi adicionado o framework Serilog para adicionar os logs:

1. Navegue até o diretório de logs da API:

```bash
cd api/logs
```

2. Abra os arquivos de log com um editor de texto ou visualize-os diretamente no terminal:

```bash
cat nome-do-arquivo.log
```


### Trigger de Registro de Modificações de Produto

- A trigger de banco de dados foi criada para registrar modificações nos produtos. Para visualizar e testar a trigger:

1. Acesse o banco de dados MySQL:

```bash
mysql -u root -p crud_maxima
```

2. Verifique a existência da trigger:

```bash
SHOW TRIGGERS;
```

3. Para visualizar os registros de modificações, consulte a tabela  **`logproduct`**: onde as alterações são armazenadas:
   
```bash
SELECT * FROM logproduct;
```


## Configurando e Rodando Testes da API

### Para garantir que a API está funcionando corretamente e rodar testes, siga os passos abaixo:

1. Navegue até o diretório da API:

```bash
cd API.Tests
```

2. Restaure as dependências do projeto se ainda não o fez:

```bash
dotnet restore
```

3. Compile o projeto para garantir que não há erros de compilação:

```bash
dotnet build
```

4. Execute os testes da API com o comando:

```bash
dotnet test
```

5. Isso irá rodar todos os testes unitários e de integração configurados no projeto.

6. Para visualizar o resultado dos testes, examine o output no terminal.

## Demonstração do Aplicativo

### Abaixo, você pode conferir um vídeo demonstrativo da aplicação em funcionamento:

<a href="https://youtu.be/7k5aLhsTehQ" target="_blank">
  <img src="https://img.youtube.com/vi/7k5aLhsTehQ/0.jpg" alt="Vídeo de Demonstração" />
</a>

[Assista ao vídeo de demonstração](https://youtu.be/7k5aLhsTehQ)



Se você tiver dúvidas ou precisar de mais assistência, sinta-se à vontade para entrar em contato diretamente.

Atenciosamente,

<<<<<<< HEAD
**Lucas Montalvão**  
=======
**Lucas Montalvão**  
>>>>>>> abf9e3548118447d704955f4509b22d0fa2cc88d
