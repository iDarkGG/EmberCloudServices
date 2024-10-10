using AutoMapper;
using EmberAPI.BackgroundServices;
using EmberAPI.Context;
using Microsoft.EntityFrameworkCore;

namespace EmberAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddHttpClient();
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();
        builder.Services.AddSwaggerGen();
        builder.Services.AddAutoMapper(typeof(Mapper));
        builder.Services.AddDbContext<MainContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Conn"))
        );
        builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
        Console.WriteLine(builder.Configuration.GetConnectionString("Conn"));
        builder.Services.AddSingleton<DataBaseSizeBgService>();
        builder.Services.AddHostedService<BackgroundService>(provider => provider.GetRequiredService<DataBaseSizeBgService>());

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}