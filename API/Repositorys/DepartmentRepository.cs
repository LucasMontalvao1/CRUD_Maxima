using API.Infra.Data;
using API.Models;
using API.Repositorys.Interfaces;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace API.Repositorys
{
    public class DepartmentRepository : IDepartmentRepository, IDisposable
    {
        private readonly MySqlConnectionDB _mySqlConnectionDB;
        private MySqlConnection _connection;

        public DepartmentRepository(MySqlConnectionDB mySqlConnectionDB)
        {
            _mySqlConnectionDB = mySqlConnectionDB;
            _connection = _mySqlConnectionDB.CreateConnection();
            _connection.Open();
        }

        public IEnumerable<Department> GetAll()
        {
            var departments = new List<Department>();
            var query = "SELECT * FROM department";

            using (var command = new MySqlCommand(query, _connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var department = new Department
                    {
                        Codigo = reader.GetString("Codigo"),
                        Descricao = reader.GetString("Descricao")
                    };

                    departments.Add(department);
                }
            }

            return departments;
        }

        public Department GetByCodigo(string codigo)
        {
            Department department = null;

            var query = @"
                SELECT *
                FROM Department
                WHERE Codigo = @Codigo";

            using (var command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@Codigo", codigo);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        department = new Department
                        {
                            Codigo = reader.GetString("Codigo"),
                            Descricao = reader.GetString("Descricao")
                        };
                    }
                }
            }

            return department;
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
