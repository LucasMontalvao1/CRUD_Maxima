using API.Infra.Data;
using API.Models;
using API.Repositorys.Interfaces;
using MySql.Data.MySqlClient;

namespace API.Repositorys
{
    public class AuthRepository : IAuthRepository
    {
        private readonly MySqlConnectionDB _mySqlConnectionDB;

        public AuthRepository(MySqlConnectionDB mySqlConnectionDB)
        {
            _mySqlConnectionDB = mySqlConnectionDB;
        }

        public User ValidarUsuario(string username, string password)
        {
            User uservalid = null;

            try
            {
                using (MySqlConnection connection = _mySqlConnectionDB.CreateConnection())
                {
                    string query = "SELECT id_usuario, Username, Name, Email FROM usuarios WHERE Username = @Username AND Password = @Password";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);

                        connection.Open();

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
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
