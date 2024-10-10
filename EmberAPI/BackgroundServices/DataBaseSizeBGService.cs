using System;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

namespace EmberAPI.BackgroundServices;

public class DataBaseSizeBgService : BackgroundService
{
    private static readonly IConfiguration _configuration;
    private readonly ILogger<DataBaseSizeBgService> _logger;
    private readonly string? connString;

    public DataBaseSizeBgService(ILogger<DataBaseSizeBgService> logger)
    {
        _logger = logger;
        connString  = _configuration.GetConnectionString("conn");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("DataBaseSizeBGService is starting.");

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("DataBaseSizeBGService is running. at {Time}", DateTimeOffset.Now);
            
            //Code
            var dbSize = await GetDbSizeAsync();
            
            await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
    
        }
        _logger.LogInformation("DataBaseSizeBGService is stopping.");
    }
    private async Task<Decimal> GetDbSizeAsync()
    {
        decimal dbSize = 0;
        try
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("SELECT SUM(size) * 8 * 1024 FROM sys.master_files WHERE type = 0;", connection))
                {
                    dbSize = (decimal)await command.ExecuteScalarAsync();
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
}