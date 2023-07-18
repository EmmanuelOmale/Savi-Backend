using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Savi.Api.Extensions;
using Savi.Api.Service;
using Savi.Core.Interfaces;
using Savi.Core.Services;
using Savi.Data.Context;
using Savi.Data.Domains;
using Serilog;
using Serilog.Extensions.Logging;
using Savi.Data.EmailService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Savi.Data.IRepositories;
using Savi.Data.UnitOfWork;
using Savi.Data.Seeding;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;

        // Add services to the container.
        // New Comments
        Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("log/SaviLogs.txt", rollingInterval: RollingInterval.Day).CreateLogger();
        builder.Host.UseSerilog();

        builder.Services.AddControllers();
        

        builder.Services.AddScoped<IGoogleSignupService, GoogleSignupService>();
        builder.Services.AddHttpClient();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Savi.Api", Version = "v1" });
        });

        var loggerFactory = new SerilogLoggerFactory(Log.Logger);
        builder.Services.AddSingleton<ILoggerFactory>(loggerFactory);

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
        }).AddCookie()
        .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
        {
            options.ClientId = "443815310479-9rcoi66q352erfj1udd88au2tqgdmug0.apps.googleusercontent.com";
            options.ClientSecret = "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";
        });
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        builder.Services.AddScoped<IDocumentUploadService, DocumentUploadService>();
        builder.Services.AddCloudinaryExtension(builder.Configuration);
        builder.Services.AddDbContext<SaviDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SaviContext")));
        builder.Services.AddTransient<IEmailService, SmtpEmailService>();
       // builder.Services.AddTransient<IPasswordService, PasswordService>();
        builder.Services.AddAppSettingsConfig(builder.Configuration, builder.Environment);
        builder.Services.AddHttpContextAccessor();

        //Entityframework
        builder.Services.AddDbContext<SaviDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("SAVIBackEnd")));
        builder.Services.AddScoped<IAuthService, AuthService>();

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
       .AddEntityFrameworkStores<SaviDbContext>()
        .AddDefaultTokenProviders();

        // Adding Authentication
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = configuration["JWT:ValidAudience"],
                ValidIssuer = configuration["JWT:ValidIssuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
            };
        });

        builder.Services.AddSwaggerGen(option =>
        {

            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
        });

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.WithOrigins("http://localhost:3000")
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });
        });


        var app = builder.Build();
        var logger = app.Services.GetRequiredService<ILogger<Program>>();
        // app.ConfigureExceptionHandler(logger);
        // Create a scope and resolve the SaviDbContext
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<SaviDbContext>();

            // Seed the data
            Seeder.SeedData(dbContext);
        }
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
        app.UseCors();
        

        app.UseRouting();


        app.UseAuthentication();

        app.UseAuthorization();


        app.UseEndpoints(endpoints =>
        {
            

            endpoints.MapControllers();
        });

        app.Run();
    }
}
