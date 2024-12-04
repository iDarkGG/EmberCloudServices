using Microsoft.SqlServer.Management.Smo;

namespace EmberCloudServices.Utilities;

public class DbSchema
{
    private static List<DbSchema> List = new List<DbSchema>();
    private TableSchema Tb = new TableSchema();

    // public DbSchema()
    // {
    //     
    // }
    // public DbSchema(string dbName, List<TableSchema> tables) : this()
    // {
    //     DbName = dbName;
    //     Tables = tables;
    // }

    public string DbName { get; set; }
    public List<TableSchema> Tables { get; set; }

    public void Filler(string dbName, List<TableSchema> tables)   
    {
        List.Add(new DbSchema{DbName = dbName, Tables = tables});
    }
    
    public static async Task<IEnumerable<DbSchema>> GetAllDbsAsync()
    {
        return List;
    }



    public void SearchDb(string dbName)
    {
        var dbSchema = List.FirstOrDefault(d => d.DbName == dbName);
        if (dbSchema != null & dbSchema.Tables is null )
        {
            dbSchema.Tables = Tb.ReturnList().ToList();
            Tb.ListCleaner();
        }
    }

    public int DbChecker(string dbName)
    {
        var dbSchema = List.FirstOrDefault(d => d.DbName == dbName);
        if (dbSchema != null)
        {
            return -1;
        }

        return 0;
    }
}