-- Criação da tabela de Usuários
CREATE TABLE usuarios (
    id_usuario INT AUTO_INCREMENT PRIMARY KEY,
    Username VARCHAR(50) NOT NULL UNIQUE,
    Password VARCHAR(100) NOT NULL,
    Name VARCHAR(100),
    Email VARCHAR(100)
);

-- Criação da tabela de Departamentos
CREATE TABLE Department (
    Codigo VARCHAR(50) PRIMARY KEY NOT NULL,
    Descricao TEXT NOT NULL
);

-- Criação da tabela de Produtos
CREATE TABLE Product (
    Id INT AUTO_INCREMENT PRIMARY KEY NOT NULL,
    Codigo VARCHAR(50) NOT NULL,
    Descricao TEXT NOT NULL,
    Preco DECIMAL(10, 2) NOT NULL,
    Status BOOLEAN NOT NULL, 
    CodigoDepartamento VARCHAR(50) NOT NULL,
    Deletado BOOLEAN NOT NULL DEFAULT FALSE, 
    FOREIGN KEY (CodigoDepartamento) REFERENCES Department(Codigo)
);

-- Criação da tabela de Log de Produtos
CREATE TABLE LogProduct (
    Id INT AUTO_INCREMENT PRIMARY KEY NOT NULL,
    ProductId INT NOT NULL,
    OldCodigo VARCHAR(50),
    NewCodigo VARCHAR(50),
    OldDescricao TEXT,
    NewDescricao TEXT,
    OldPreco DECIMAL(10, 2),
    NewPreco DECIMAL(10, 2),
    OldStatus BOOLEAN,
    NewStatus BOOLEAN,
    OldCodigoDepartamento VARCHAR(50),
    NewCodigoDepartamento VARCHAR(50),
    ChangedAt DATETIME NOT NULL,
    ChangeType VARCHAR(10) NOT NULL,
    FOREIGN KEY (ProductId) REFERENCES Product(Id)
);
