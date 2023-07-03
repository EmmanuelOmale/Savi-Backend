using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Savi.Api.Extensions;
using Savi.Core.Interfaces;
using Savi.Core.Services;
using Savi.Data.Context;
using Savi.Data.Domains;
using Serilog;
using Serilog.Extensions.Logging;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // New Comments
        Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("log/SaviLogs.txt", rollingInterval: RollingInterval.Day).CreateLogger();
        builder.Host.UseSerilog();

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Savi.Api", Version = "v1" });
        });

        var loggerFactory = new SerilogLoggerFactory(Log.Logger);

        //Entityframework
        builder.Services.AddDbContext<SaviDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("SAVIBackEnd")));
        builder.Services.AddScoped<IAuthService, AuthService>();

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
       .AddEntityFrameworkStores<SaviDbContext>()
        .AddDefaultTokenProviders();

        var app = builder.Build();
        var logger = app.Services.GetRequiredService<ILogger<Program>>();
        app.ConfigureExceptionHandler(logger);

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Savi.Api v1");
            });
        }

        app.UseHttpsRedirection();

        app.UseRouting();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.Run();
    }
}
