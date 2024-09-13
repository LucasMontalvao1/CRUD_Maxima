using API.Infra.Data;
using API.Models;
using API.Models.DTOs;
using API.Repositorys.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace API.Repositorys
{
    public class ProductRepository : IProductRepository, IDisposable
    {
        private readonly MySqlConnectionDB _mySqlConnectionDB;
        private MySqlConnection _connection;

        public ProductRepository(MySqlConnectionDB mySqlConnectionDB)
        {
            _mySqlConnectionDB = mySqlConnectionDB ?? throw new ArgumentNullException(nameof(mySqlConnectionDB));
            _connection = _mySqlConnectionDB.CreateConnection();
            _connection.Open();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var products = new List<Product>();
            var query = @"SELECT p.Id,
                                 p.Codigo,
                                 p.Descricao,
                                 p.Preco,
                                 p.Status,
                                 p.Deletado,
                                 d.Codigo AS CodigoDepartamento,
                                 d.Descricao AS DescricaoDepartamento
                          FROM Product p
                          INNER JOIN Department d ON p.CodigoDepartamento = d.Codigo
                          WHERE p.Deletado = FALSE";

            try
            {
                using (var command = new MySqlCommand(query, _connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var product = new Product
                        {
                            Id = reader.GetInt32("Id"),
                            Codigo = reader.GetString("Codigo"),
                            Descricao = reader.GetString("Descricao"),
                            Preco = reader.GetDecimal("Preco"),
                            Status = reader.GetBoolean("Status"),
                            Deletado = reader.GetBoolean("Deletado"),

                            Department = new Department
                            {
                                Codigo = reader.GetString("CodigoDepartamento"),
                                Descricao = reader.GetString("DescricaoDepartamento")
                            }
                        };

                        products.Add(product);
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Erro ao obter produtos.", ex);
            }

            return products;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            Product product = null;
            var query = @"SELECT p.Id,
                                 p.Codigo,
                                 p.Descricao,
                                 p.Preco,
                                 p.Status,
                                 p.Deletado,
                                 d.Codigo AS CodigoDepartamento,
                                 d.Descricao AS DescricaoDepartamento
                          FROM Product p
                          INNER JOIN Department d ON p.CodigoDepartamento = d.Codigo
                          WHERE p.Id = @Id AND p.Deletado = FALSE";

            try
            {
                using (var command = new MySqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            product = new Product
                            {
                                Id = reader.GetInt32("Id"),
                                Codigo = reader.GetString("Codigo"),
                                Descricao = reader.GetString("Descricao"),
                                Preco = reader.GetDecimal("Preco"),
                                Status = reader.GetBoolean("Status"),
                                Deletado = reader.GetBoolean("Deletado"),

                                Department = new Department
                                {
                                    Codigo = reader.GetString("CodigoDepartamento"),
                                    Descricao = reader.GetString("DescricaoDepartamento")
                                }
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Erro ao obter produto por ID.", ex);
            }

            return product;
        }

        public async Task<Product> GetByCodigoAsync(string codigo)
        {
            Product product = null;
            var query = @"SELECT p.Id,
                                 p.Codigo,
                                 p.Descricao,
                                 p.Preco,
                                 p.Status,
                                 p.Deletado,
                                 d.Codigo AS CodigoDepartamento,
                                 d.Descricao AS DescricaoDepartamento
                          FROM Product p
                          INNER JOIN Department d ON p.CodigoDepartamento = d.Codigo
                          WHERE p.Codigo = @Codigo AND p.Deletado = FALSE";

            try
            {
                using (var command = new MySqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("@Codigo", codigo);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            product = new Product
                            {
                                Id = reader.GetInt32("Id"),
                                Codigo = reader.GetString("Codigo"),
                                Descricao = reader.GetString("Descricao"),
                                Preco = reader.GetDecimal("Preco"),
                                Status = reader.GetBoolean("Status"),
                                Deletado = reader.GetBoolean("Deletado"),

                                Department = new Department
                                {
                                    Codigo = reader.GetString("CodigoDepartamento"),
                                    Descricao = reader.GetString("DescricaoDepartamento")
                                }
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Erro ao obter produto por código.", ex);
            }

            return product;
        }

        public async Task<int> AddProductAsync(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Produto não pode ser nulo.");
            }

            var query = @"INSERT INTO Product (Codigo, Descricao, Preco, Status, CodigoDepartamento, Deletado)
                      VALUES (@Codigo, @Descricao, @Preco, @Status, @CodigoDepartamento, FALSE)";

            try
            {
                using (var command = new MySqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("@Codigo", product.Codigo);
                    command.Parameters.AddWithValue("@Descricao", product.Descricao);
                    command.Parameters.AddWithValue("@Preco", product.Preco);
                    command.Parameters.AddWithValue("@Status", product.Status);
                    command.Parameters.AddWithValue("@CodigoDepartamento", product.CodigoDepartamento);

                    return await command.ExecuteNonQueryAsync();
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Erro ao adicionar produto.", ex);
            }
        }

        public async Task UpdateProductAsync(int id, Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Produto não pode ser nulo.");
            }

            var query = @"UPDATE Product 
                      SET Codigo = @Codigo, 
                          Descricao = @Descricao, 
                          Preco = @Preco, 
                          Status = @Status, 
                          CodigoDepartamento = @CodigoDepartamento
                      WHERE Id = @Id AND Deletado = FALSE";

            try
            {
                using (var command = new MySqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Codigo", product.Codigo);
                    command.Parameters.AddWithValue("@Descricao", product.Descricao);
                    command.Parameters.AddWithValue("@Preco", product.Preco);
                    command.Parameters.AddWithValue("@Status", product.Status);
                    command.Parameters.AddWithValue("@CodigoDepartamento", product.CodigoDepartamento);

                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Erro ao atualizar produto.", ex);
            }
        }

        public async Task DeleteProductAsync(int id)
        {
            var query = @"UPDATE Product SET Deletado = TRUE WHERE Id = @Id";

            try
            {
                using (var command = new MySqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Erro ao deletar produto.", ex);
            }
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Close();
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}
