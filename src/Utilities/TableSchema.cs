using Microsoft.SqlServer.Management.Smo;

namespace EmberCloudServices.Utilities;

public class TableSchema
{
    private static List<TableSchema> TableList = new List<TableSchema>();

    // public TableSchema()
    // {
    //     
    // }
    
    // public TableSchema(string tableName, string tableType) : this()
    // {
    //     TableName = tableName;
    //     TableType = tableType;
    // }

    public string TableName{  get; set; }
  

    public string TableType { get; set; }

    public void TableFiller(string tableName, string tableType)
    {
        TableList.Add(new TableSchema{TableName = tableName, TableType = tableType});
    }

    public List<TableSchema> ReturnList()
    {
        return TableList;
    }

    public void ListCleaner()
    {
        TableList.Clear();
    }
}