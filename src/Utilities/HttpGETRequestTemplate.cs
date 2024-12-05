using System.Text.Json;

namespace EmberCloudServices.Utilities;

public class HttpGetRequestTemplate
{
    public static async Task zi()
        {
            try
            {
                /* Este es un ejemplo de un HttpGET para la API, en este caso para Clientes, devuelve la lista completa de clientes
                 * en la BD, sin Utilizar DTOs, usando JsonDocument y extrayendo manualmente las propiedades.
                 * NOTA: No es el mejor acercamiento, pero me parece razonable sin necesidad de utilizar DTOS o propiedades
                 */
                
                //Este es el Endpoint a Consultar
                string endpoint = "http://localhost:5290/EmberAPI/Client";
                //Creando el HttpClient
                using HttpClient httpClient = new HttpClient();
                //Obtiene respuesta
                HttpResponseMessage response = await httpClient.GetAsync(endpoint);
                //Se asegura que sea un HTTP.200OK Code
                //Si no, regresa una excepcion
                response.EnsureSuccessStatusCode();
                //Lee el contenido de la respuesta como un string
                string jsonResponse = await response.Content.ReadAsStringAsync();
                //Luego lo convierte en un JsonDocument
                using JsonDocument jsonDocument = JsonDocument.Parse(jsonResponse);
                //Obtiene el elemento Raiz del documento
                JsonElement root = jsonDocument.RootElement;
                //verifica que el Elemento sea un Array
                if (root.ValueKind == JsonValueKind.Array)
                {
                    //Luego recorre el arreglo de Elementos
                    foreach (JsonElement client in root.EnumerateArray())
                    {
                        //Declaramos las variables necesarias con la siguiente sintaxis Array.GetProperty(string name).GetDataType(); 
                        int clientId = client.GetProperty("clientID").GetInt32();
                        string clientName = client.GetProperty("clientName").GetString()!;
                        string clientContactNumber = client.GetProperty("clientContactNumber").GetString()!;
                        string clientEmail = client.GetProperty("clientEmail").GetString()!;
                        bool status = client.GetProperty("status").GetBoolean();
                        string creationDate = client.GetProperty("creationDate").GetString()!;
                        
                        //Test en consola
                        
                        //Ejemplo
                        
                        // ID: 1
                        // Name: Updated Client Name
                        // Contact Number: 123-456-7890
                        // Email: contact@techcorp.com
                        // Status: Active
                        // Creation Date: 2023-05-01
                        // ------------------------------
                        // ID: 2
                        // Name: HealthPlus
                        // Contact Number: 234-567-8901
                        // Email: support@healthplus.com
                        // Status: Active
                        // Creation Date: 2022-11-10
                        // ------------------------------
                         
                        Console.WriteLine($"ID: {clientId}");
                        Console.WriteLine($"Name: {clientName}");
                        Console.WriteLine($"Contact Number: {clientContactNumber}");
                        Console.WriteLine($"Email: {clientEmail}");
                        Console.WriteLine($"Status: {(status ? "Active" : "Inactive")}");
                        Console.WriteLine($"Creation Date: {creationDate}");
                        Console.WriteLine(new string('-', 30));
                    }
                }
                else
                {
                    Console.WriteLine("The JSON response is not an array.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
}