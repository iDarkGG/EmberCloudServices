using AutoMapper;
using EmberAPI.APIMapper;
using EmberAPI.BackgroundServices;
using EmberAPI.Context;
using EmberAPI.Repositories;
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
        builder.Services.AddAutoMapper(typeof(ApiMapper));
        builder.Services.AddScoped<IClientRepository, ClientRepository>();
        builder.Services.AddDbContext<MainContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Conn"))
        );
        builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
        builder.Services.AddSingleton<DbSizeBGService>();
        builder.Services.AddHostedService<BackgroundService>(provider => provider.GetRequiredService<DbSizeBGService>());
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        //app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}