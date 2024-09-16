-- Inserção dos Departamentos
INSERT INTO Department (Codigo, Descricao) VALUES
('010', 'BEBIDAS'),
('020', 'CONGELADOS'),
('030', 'LATICÍNIOS'),
('040', 'VEGETAIS');

-- Inserção dos Usuários
INSERT INTO usuarios (Username, Password, Name, Email) VALUES
('a', '1', 'Lucas Montalvao', 'lucas@example.com'),
('lucas', '123', 'Lucas', 'lucas.admin@example.com'),
('admin', 'admin123', 'administrador', 'admin@example.com');

-- Inserção de Produtos
INSERT INTO Product (Codigo, Descricao, Preco, Status, CodigoDepartamento, Deletado) VALUES
('P001', 'Refrigerante XYZ', 3.99, TRUE, '010', FALSE),
('P002', 'Pizza Congelada ABC', 12.99, TRUE, '020', FALSE),
('P003', 'Queijo Minas', 8.49, TRUE, '030', FALSE),
('P004', 'Cenoura Fresca', 2.49, TRUE, '040', FALSE),
('P005', 'Suco de Laranja 1L', 4.59, TRUE, '010', FALSE),
('P006', 'Frango Congelado 1kg', 15.99, TRUE, '020', FALSE),
('P007', 'Manteiga 200g', 5.89, TRUE, '030', FALSE),
('P008', 'Batata Doce', 3.29, TRUE, '040', FALSE),
('P009', 'Cerveja Lager 350ml', 2.79, TRUE, '010', FALSE),
('P010', 'Hambúrguer Congelado 500g', 10.49, TRUE, '020', FALSE),
('P011', 'Iogurte Natural 1L', 6.99, TRUE, '030', FALSE),
('P012', 'Alface Orgânica', 2.89, TRUE, '040', FALSE);
