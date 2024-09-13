using API.Infra.Data;
using API.Models;
using API.Repositorys.Interfaces;
using MySql.Data.MySqlClient;
using System.Data;

namespace API.Repositorys
{
    public class AuthRepository : IAuthRepository
    {
        private readonly MySqlConnectionDB _mySqlConnectionDB;

        public AuthRepository(MySqlConnectionDB mySqlConnectionDB)
        {
            _mySqlConnectionDB = mySqlConnectionDB;
        }

        public async Task<User> ValidarUsuarioAsync(string username, string password)
        {
            User uservalid = null;

            try
            {
                using (var connection = _mySqlConnectionDB.CreateConnection())
                {
                    string query = "SELECT id_usuario, Username, Name, Email FROM usuarios WHERE Username = @Username AND Password = @Password";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);

                        await connection.OpenAsync();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                uservalid = new User
                                {
                                    id_usuario = reader.GetInt32("id_usuario"),
                                    Username = reader.GetString("Username"),
                                    Name = reader.GetString("Name"),
                                    Email = reader.GetString("Email")
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu erro: {ex.Message}");
            }

            return uservalid;
        }
    }
}
