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
            if (serverConnection != null && serverConnection.IsOpen)
            {
                serverConnection.Disconnect();
            }
        }
    }
    

}