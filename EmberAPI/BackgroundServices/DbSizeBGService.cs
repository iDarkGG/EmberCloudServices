using System;
using System.Configuration;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

namespace EmberAPI.BackgroundServices;

public class DbSizeBGService : BackgroundService
{
    private static IConfiguration _configuration;
    private readonly HttpClient _httpClient;
    private readonly ILogger<DbSizeBGService> _logger;
    private readonly string? _connString;
    private readonly string? _apiEndpoint;

    public DbSizeBGService(ILogger<DbSizeBGService> logger, HttpClient httpClient, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
        _connString = _configuration.GetConnectionString("Conn");
        _httpClient = httpClient;
        _apiEndpoint = _configuration.GetConnectionString("ServicesEndpoint");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("DataBaseSizeBGService is starting.");

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("DataBaseSizeBGService is running. at {Time}", DateTimeOffset.Now);
            
            //Code
            var dbSize = Convert.ToDouble(await GetDbSizeAsync())/1000000;
            _logger.LogInformation("Test: MEGABYTES {dbSize}", dbSize);

            await SendPayload(dbSize);
            
            await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
    
        }
        _logger.LogInformation("DataBaseSizeBGService is stopping.");
    }
    private async Task<int> GetDbSizeAsync()
    {
        int dbSize = 0;
        try
        {
            using (SqlConnection connection = new SqlConnection(_connString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("SELECT SUM(size) * 8 * 1024 FROM sys.master_files WHERE type = 0;", connection))
                {
                    dbSize = (int) await command.ExecuteScalarAsync();
                }
            }
            _logger.LogInformation("Database size retrieved successfully: {0} bytes", dbSize);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving database size.");
        }
        return dbSize;
    }

    private async Task SendPayload(double dbSize)
    {
        _logger.LogInformation("Sending payload to database: {dbSize}", dbSize);
    
        var payload = new
        {
            DataBaseSize = dbSize,
            Timestamp = DateTime.UtcNow
        };
        
        var jsonContent = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        try
        {
            var response = await _httpClient.PostAsync(_apiEndpoint, jsonContent);
            response.EnsureSuccessStatusCode();
            _logger.LogInformation("Payload sent to database: {dbSize}", dbSize);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending payload to database: {dbSize}", dbSize);
        }
        
    }
}