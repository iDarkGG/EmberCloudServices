using Microsoft.Data.SqlClient;

namespace EmberCloudServices.Utilities;

public class DbTreeQuery
{
    private string _connString;
    private DbSchema dbSchema = new DbSchema();
    private SqlDataReader reader;
    private SqlDataReader reader2;
    private TableSchema tb = new TableSchema();
    
    public DbTreeQuery(string connString)
    {
        _connString = connString;
    }

    public bool QueryAllDatabases()
    {
        using (var connection = new SqlConnection(_connString))
        {
            try
            {
                connection.Open();
                string command = "SELECT name FROM sys.databases";
                using (var query = new SqlCommand(command, connection))
                {
                    reader = query.ExecuteReader();
                     while (reader.Read())
                     {
                         string name = reader["name"].ToString();
                         if (dbSchema.DbChecker(name) != -1)
                         {
                             dbSchema.Filler(name, null);
                             QueryDatabaseObjects(_connString, name);
                         }
                     }
                    reader.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding user: {ex.Message}");
                return false;
            }
        }
    }
    public void QueryDatabaseObjects(string connectionString, string databaseName)
    {
        string dbConnectionString = connectionString.Replace("master", databaseName);

        try
        {
            using (SqlConnection conn = new SqlConnection(dbConnectionString))
            {
                conn.Open();

                // Query tables in the database
                string getTablesQuery = @"SELECT TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME, TABLE_TYPE FROM INFORMATION_SCHEMA.TABLES";
                using (var command = new SqlCommand(getTablesQuery, conn))
                {
                    reader2 = command.ExecuteReader();
                    while (reader2.Read())
                    {
                        tb.TableFiller(reader2["TABLE_NAME"].ToString(), reader2["TABLE_TYPE"].ToString());
                    }
                    dbSchema.SearchDb(databaseName);
                    reader2.Close();
                }
               
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error querying database {databaseName}: {ex.Message}");
        }
    }
}