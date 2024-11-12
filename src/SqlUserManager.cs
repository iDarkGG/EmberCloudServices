using System.Text.RegularExpressions;

namespace EmberCloudServices;

using System;
using System.Data.SqlClient;

public class SqlUserManager
{
    private string _connectionString;

    public SqlUserManager(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    private string SanitizeUsername(string username)
    {

        var regex = new Regex(@"^[a-zA-Z0-9_]+$");
        if (!regex.IsMatch(username))
        {
            throw new ArgumentException("Nombre de Usuario invalido, caracteres no validos utilizados");
        }
        
        return username;
    }

    public bool AddUserToSqlInstance(string username, string password, string role)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            try
            {
                connection.Open();
                
                string createLoginQuery = $"CREATE LOGIN [{SanitizeUsername(username)}] WITH PASSWORD = '{password}';";
                using (var createLoginCommand = new SqlCommand(createLoginQuery, connection))
                {
                    createLoginCommand.ExecuteNonQuery();
                }
                
                string createUserQuery = $"CREATE USER [{username}] FOR LOGIN [{username}];";
                using (var createUserCommand = new SqlCommand(createUserQuery, connection))
                {
                    createUserCommand.Parameters.AddWithValue("@username", username);
                    createUserCommand.ExecuteNonQuery();
                }
                // string grantRoleQuery = "ALTER ROLE @role ADD MEMBER @username;";
                // using (var grantRoleCommand = new SqlCommand(grantRoleQuery, connection))
                // {
                //     grantRoleCommand.Parameters.AddWithValue("@role", role);
                //     grantRoleCommand.Parameters.AddWithValue("@username", username);
                //     grantRoleCommand.ExecuteNonQuery();
                // }

                return true; // Success
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding user: {ex.Message}");
                return false; // Failure
            }
        }
    }
}

