using Microsoft.Data.SqlClient;

namespace EmberCloudServices;
using System;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;

public class SqlInstance
{
    public void SqlInstanceCreate(string nombreInstancia, string password)
    {
        string instanceName = nombreInstancia;

        ServerConnection serverConnection = new ServerConnection("localhost");
        Server server = new Server(serverConnection);

        try
        {
            var processInfo = new System.Diagnostics.ProcessStartInfo
            {
                FileName = @"C:\SQL2022\Evaluation_ENU\SETUP.EXE",
                Arguments = $"/Q /ACTION=Install /INSTANCENAME={instanceName} /FEATURES=SQL /TCPENABLED=1 /SQLSVCACCOUNT=\"NT AUTHORITY\\NETWORK SERVICE\" /AGTSVCACCOUNT=\"NT AUTHORITY\\NETWORK SERVICE\" /SECURITYMODE=SQL /SAPWD={password} /IAcceptSQLServerLicenseTerms /SQLSYSADMINACCOUNTS=\"BUILTIN\\Administrators\"",
                UseShellExecute = true,
                CreateNoWindow = false
            };

            using (var process = System.Diagnostics.Process.Start(processInfo))
            {
                process!.WaitForExit(); 
            }

            Console.WriteLine($"Instancia de SQL Server '{instanceName}' creada con éxito.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al crear la instancia: {ex.Message}");
        }
        finally
        {
            // Cierra la conexión
            if (serverConnection != null && serverConnection.IsOpen)
            {
                serverConnection.Disconnect();
            }
        }
    }

    public void SqlInstanceDrop(string nombreInstancia)
    {
        string instanceName = nombreInstancia;
        
        ServerConnection serverConnection = new ServerConnection("localhost");
        Server server = new Server(serverConnection);

        try
        {
            var processInfo = new System.Diagnostics.ProcessStartInfo
            {
                FileName = @"C:\SQL2022\Evaluation_ENU\SETUP.EXE",
                Arguments = $"/Q /ACTION=Uninstall /INSTANCENAME={instanceName} /FEATURES=SQL",
                UseShellExecute = true,
                CreateNoWindow = false
            };

            using (var process = System.Diagnostics.Process.Start(processInfo))
            {
                process!.WaitForExit();
            }

            Console.WriteLine($"Instancia de SQL Server '{instanceName}' Eliminada con éxito.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al eliminar la instancia: {ex.Message}");
        }
        finally
        {
            if (serverConnection.IsOpen)
            {
                serverConnection.Disconnect();
            }
        }
    }

    public void AddUserToSqlInstance(string instanceName, string loginName, string password, string saPassword, string dbName = null)
{
    ServerConnection serverConnection = new ServerConnection("localhost", "sa", saPassword);
    Server server = new Server(serverConnection);

    try
    {
        // Verifica si el inicio de sesión ya existe
        if (!server.Logins.Contains(loginName))
        {
            // Crea un nuevo inicio de sesión
            Login newLogin = new Login(server, loginName)
            {
                LoginType = LoginType.SqlLogin,
                PasswordPolicyEnforced = false
            };
            newLogin.Create(password);
            Console.WriteLine($"Inicio de sesión '{loginName}' creado con éxito.");
        }
        else
        {
            Console.WriteLine($"El inicio de sesión '{loginName}' ya existe.");
        }

        // Si se especifica un nombre de base de datos, crea un usuario en la base de datos
        if (!string.IsNullOrEmpty(dbName))
        {
            Database database = server.Databases[dbName];

            if (database != null)
            {
                // Verifica si el usuario ya existe en la base de datos
                if (!database.Users.Contains(loginName))
                {
                    // Crea un nuevo usuario en la base de datos basado en el inicio de sesión
                    User newUser = new User(database, loginName)
                    {
                        Login = loginName
                    };
                    newUser.Create();
                    Console.WriteLine($"Usuario '{loginName}' creado en la base de datos '{dbName}'.");
                }
                else
                {
                    Console.WriteLine($"El usuario '{loginName}' ya existe en la base de datos '{dbName}'.");
                }
            }
            else
            {
                Console.WriteLine($"La base de datos '{dbName}' no existe.");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error al agregar el usuario: {ex.Message}");
    }
    finally
    {
        // Cierra la conexión
        if (serverConnection.IsOpen)
        {
            serverConnection.Disconnect();
        }
    }
}



}