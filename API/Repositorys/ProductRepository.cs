using API.Infra.Data;
using API.Models;
using API.Models.DTOs;
using API.Repositorys.Interfaces;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace API.Repositorys
{
    public class ProductRepository : IProductRepository, IDisposable
    {
        private readonly MySqlConnectionDB _mySqlConnectionDB;
        private MySqlConnection _connection;

        public ProductRepository(MySqlConnectionDB mySqlConnectionDB)
        {
            _mySqlConnectionDB = mySqlConnectionDB;
            _connection = _mySqlConnectionDB.CreateConnection();
            _connection.Open();
        }

        public IEnumerable<Product> GetAll()
        {
            var products = new List<Product>();
            var query = "SELECT * FROM Product";

            using (var command = new MySqlCommand(query, _connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var product = new Product
                    {
                        Id = reader.GetInt32("Id"),
                        Codigo = reader.GetString("Codigo"),
                        Descricao = reader.GetString("Descricao"),
                        Preco = reader.GetDecimal("Preco"),
                        Status = reader.GetBoolean("Status"),
                        CodigoDepartamento = reader.GetString("CodigoDepartamento"),
                        Deletado = reader.GetBoolean("Deletado")
                    };

                    products.Add(product);
                }
            }

            return products;
        }

        public Product GetById(int id)
        {
            Product product = null;

            var query = "SELECT * FROM Product WHERE Id = @Id";

            using (var command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        product = new Product
                        {
                            Id = reader.GetInt32("Id"),
                            Codigo = reader.GetString("Codigo"),
                            Descricao = reader.GetString("Descricao"),
                            Preco = reader.GetDecimal("Preco"),
                            Status = reader.GetBoolean("Status"),
                            CodigoDepartamento = reader.GetString("CodigoDepartamento"),
                            Deletado = reader.GetBoolean("Deletado")
                        };
                    }
                }
            }

            return product;
        }

        public Product GetByCodigo(string codigo)
        {
            Product product = null;

            var query = "SELECT * FROM Product WHERE Codigo = @Codigo";

            using (var command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@Codigo", codigo);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        product = new Product
                        {
                            Id = reader.GetInt32("Id"),
                            Codigo = reader.GetString("Codigo"),
                            Descricao = reader.GetString("Descricao"),
                            Preco = reader.GetDecimal("Preco"),
                            Status = reader.GetBoolean("Status"),
                            CodigoDepartamento = reader.GetString("CodigoDepartamento"),
                            Deletado = reader.GetBoolean("Deletado")
                        };
                    }
                }
            }

            return product;
        }

        public void AddProduct(ProductAddDTO productDto)
        {
            if (productDto == null)
            {
                throw new ArgumentNullException(nameof(productDto), "Produto não pode ser nulo.");
            }

            // Conversão de ProductAddDTO para Product
            var product = new Product
            {
                Codigo = productDto.Codigo,
                Descricao = productDto.Descricao,
                Preco = productDto.Preco,
                Status = productDto.Status,
                CodigoDepartamento = productDto.CodigoDepartamento
            };

            string query = @"
        INSERT INTO Product (Codigo, Descricao, DepartamentoCodigo, Preco, Status)
        VALUES (@Codigo, @Descricao, @DepartamentoCodigo, @Preco, @Status)";

            using (var command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@Codigo", product.Codigo);
                command.Parameters.AddWithValue("@Descricao", product.Descricao);
                command.Parameters.AddWithValue("@DepartamentoCodigo", product.CodigoDepartamento);
                command.Parameters.AddWithValue("@Preco", product.Preco);
                command.Parameters.AddWithValue("@Status", product.Status);

                command.ExecuteNonQuery();
            }
        }

        public void UpdateProduct(int id, ProductDTO productDto)
        {
            if (productDto == null)
            {
                throw new ArgumentNullException(nameof(productDto), "Produto não pode ser nulo.");
            }

            // Conversão de ProductDTO para Product
            var product = new Product
            {
                Id = id,
                Codigo = productDto.Codigo,
                Descricao = productDto.Descricao,
                Preco = productDto.Preco,
                Status = productDto.Status,
                CodigoDepartamento = productDto.CodigoDepartamento
            };

            string query = @"
        UPDATE Product
        SET Codigo = @Codigo, Descricao = @Descricao, DepartamentoCodigo = @DepartamentoCodigo, Preco = @Preco, Status = @Status
        WHERE Id = @Id";

            using (var command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@Id", product.Id);
                command.Parameters.AddWithValue("@Codigo", product.Codigo);
                command.Parameters.AddWithValue("@Descricao", product.Descricao);
                command.Parameters.AddWithValue("@DepartamentoCodigo", product.CodigoDepartamento);
                command.Parameters.AddWithValue("@Preco", product.Preco);
                command.Parameters.AddWithValue("@Status", product.Status);

                command.ExecuteNonQuery();
            }
        }

        public void IsDeleted(int id)
        {
            string query = @"
            UPDATE Product
            SET Deletado = true
            WHERE Id = @Id";

            using (var command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Close();
                _connection.Dispose();
            }
        }
    }
}
