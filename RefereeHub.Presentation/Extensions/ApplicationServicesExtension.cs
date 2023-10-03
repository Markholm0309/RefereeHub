using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using RefereeHub.Application.Hubs;
using RefereeHub.Application.Services;
using RefereeHub.Application.Services.Event;
using RefereeHub.Application.Services.Referee;
using RefereeHub.Application.Services.Report;
using RefereeHub.Domain.Events.Interfaces;
using RefereeHub.Domain.Interfaces.Repositories;
using RefereeHub.Domain.Interfaces.Services;
using RefereeHub.Domain.Referee.Interfaces;
using RefereeHub.Domain.Report.Interfaces;
using RefereeHub.Infrastructure.Data;
using RefereeHub.Infrastructure.Repositories;
using RefereeHub.Infrastructure.Repositories.Events;
using RefereeHub.Infrastructure.Repositories.Referees;
using RefereeHub.Infrastructure.Repositories.Reports;

namespace RefereeHub.Presentation.Extensions;

public static class ApplicationServicesExtension
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                               throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<ApplicationDbContext>(options => options
            .EnableSensitiveDataLogging()
            .UseSqlite(connectionString, b => b.MigrationsAssembly("RefereeHub.Infrastructure")));

        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddSwaggerGen();
        services.AddEndpointsApiExplorer();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        var path = context.HttpContext.Request.Path;

                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs"))
                            context.Token = accessToken;

                        return Task.CompletedTask;
                    }
                };
            });
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authentication using bearer token from login endpoint",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new List<string>()
                }
            });
        });

        // .NET stuff
        services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
        services.AddSignalR();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        // Repositories
        services.AddScoped<IReportRepository, ReportRepository>();
        services.AddScoped<IRefereeRepository, RefereeRepository>();
        services.AddScoped<IEventRepository, EventRepository>();

        // Services
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IReportService, ReportService>();
        services.AddScoped<IReportBllService, ReportBllService>();
        services.AddScoped<IRefereeService, RefereeService>();
        services.AddScoped<IRefereeBllService, RefereeBllService>();
        services.AddScoped<IEventService, EventService>();
        services.AddScoped<IEventBllService, EventBllService>();
        
        // Hubs
        services.AddScoped<ReportHub>();
        
        // Cors
        services.AddCors(p => p.AddPolicy("MyPolicy",
            build =>
            {
                build.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                build.WithOrigins("https://delightful-cliff-049385a03.3.azurestaticapps.net")
                    .AllowAnyMethod().AllowAnyHeader().AllowCredentials();
            }));
    }
}