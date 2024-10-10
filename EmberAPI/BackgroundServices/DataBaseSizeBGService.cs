using System;
using System.Configuration;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

namespace EmberAPI.BackgroundServices;

public class DataBaseSizeBgService : BackgroundService
{
    private static IConfiguration _configuration;
    private readonly HttpClient _httpClient;
    private readonly ILogger<DataBaseSizeBgService> _logger;
    private readonly string? connString;

    public DataBaseSizeBgService(ILogger<DataBaseSizeBgService> logger, HttpClient httpClient, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
        connString = _configuration.GetConnectionString("Conn");
        _httpClient = httpClient;
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
            
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
    
        }
        _logger.LogInformation("DataBaseSizeBGService is stopping.");
    }
    private async Task<int> GetDbSizeAsync()
    {
        int dbSize = 0;
        try
        {
            using (SqlConnection connection = new SqlConnection(connString))
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

    // private async Task SendPayload(double dbSize)
    // {
    //     _logger.LogInformation("Sending payload to database: {dbSize}", dbSize);
    //
    //     var payload = new
    //     {
    //         DbSize = dbSize,
    //         Timestamp = DateTime.UtcNow
    //     };
    //     
    //     var jsonContent = await JsonSerializer.SerializeAsync(payload);
    //     
    // }
}