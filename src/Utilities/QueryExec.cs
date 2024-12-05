using Microsoft.Data.SqlClient;

namespace EmberCloudServices.Utilities;

public class QueryExec
{
    private string _connString;

    public QueryExec(string connString)
    {
        _connString = connString;
    }

    public async Task Execute(string query)
    {
        await using (var connection = new SqlConnection(_connString))
        {
            try
            {
                connection.Open();
                await using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding user: {ex.Message}");
            }
        }
    }
}