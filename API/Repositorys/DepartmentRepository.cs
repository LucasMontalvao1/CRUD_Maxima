using API.Infra.Data;
using API.Models;
using API.Repositorys.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            var departments = new List<Department>();
            var query = "SELECT * FROM Department";

            try
            {
                using (var command = new MySqlCommand(query, (MySqlConnection)_connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var department = new Department
                        {
                            Codigo = reader.GetString("Codigo"),
                            Descricao = reader.GetString("Descricao")
                        };

                        departments.Add(department);
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Erro ao obter todos os departamentos.", ex);
            }

            return departments;
        }

        public async Task<Department> GetByCodigoAsync(string codigo)
        {
            Department department = null;
            var query = "SELECT * FROM Department WHERE Codigo = @Codigo";

            try
            {
                using (var command = new MySqlCommand(query, (MySqlConnection)_connection))
                {
                    command.Parameters.AddWithValue("@Codigo", codigo);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            department = new Department
                            {
                                Codigo = reader.GetString("Codigo"),
                                Descricao = reader.GetString("Descricao")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Erro ao obter departamento por código.", ex);
            }

            return department;
        }

        public async Task<bool> ExistsAsync(string codigo)
        {
            var query = "SELECT COUNT(1) FROM Department WHERE Codigo = @Codigo";

            try
            {
                using (var command = new MySqlCommand(query, (MySqlConnection)_connection))
                {
                    command.Parameters.AddWithValue("@Codigo", codigo);
                    var count = Convert.ToInt32(await command.ExecuteScalarAsync());
                    return count > 0;
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Erro ao verificar existência do departamento.", ex);
            }
        }

        public void Dispose()
        {
            _connection?.Close();
            _connection?.Dispose();
        }
    }
}
