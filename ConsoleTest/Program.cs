using System;
using EmberCloudServices;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;

class Program
{
    static void Main()
    {
        SqlInstance db = new SqlInstance();
       // SqlUserManager user = new SqlUserManager("Server=localhost;Database=DB_EmberCloudServices;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True;");

        //db.SqlInstanceCreate("testinstancia", "testpassword");
        
        //Recuerda que "testinstancia tiene que ser reemplazado por la instancia que a la cual el cliente quiera agregar el user
        //De momento solo se agrega login y user
        SqlUserManager user = new SqlUserManager("Server=localhost\\testinstancia;Database=master;User Id=sa;Password=testpassword;");
        user.AddUserToSqlInstance("testuser", "testpassword", null);
        //db.SqlInstanceDrop("testinstancia");
        //db.AddUserToSqlInstance("TestInstancia", "TestUser", "12345678","123456789");
    }
}
