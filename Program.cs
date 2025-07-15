using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SeinfeldAPI.Data;
using SeinfeldAPI.Interfaces;
using SeinfeldAPI.Repo;
using SeinfeldAPI.Services;
using SeinfeldAPI.Services.Core;
using SeinfeldAPI.Services.Security;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.RateLimiting;

namespace SeinfeldAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            /* =======================================================
             * SERVICE REGISTRATION
             * ======================================================= */

            // Repository layer
            builder.Services.AddScoped<IEpisodeRepository, EpisodeRepoistory>();
            builder.Services.AddScoped<IEpisodeQuotesRepository, EpisodeQuotesRepoistory>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            // Service layer
            builder.Services.AddScoped<IEpisodeService, EpisodeService>();
            builder.Services.AddScoped<IEpisodeQuotesService, EpisodeQuotesService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<JwtHelper>(); // For JWT token generation

            // Database Context
            builder.Services.AddDbContext<SeinfeldDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Controllers and JSON options
            builder.Services.AddControllers()
                .AddJsonOptions(x =>
                    x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve);

            /* =======================================================
             * JWT AUTHENTICATION & AUTHORIZATION
             * ======================================================= */
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });

            builder.Services.AddAuthorization();

            /* =======================================================
             * SWAGGER / OPENAPI
             * ======================================================= */
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Seinfeld API",
                    Version = "v1"
                });

                // Base URL for reverse proxy / path base
                options.AddServer(new OpenApiServer { Url = "/seinfeld" });

                // Enable XML comments for better documentation
                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                // Add JWT Auth button in Swagger UI
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        new string[] {}
                    }
                });
            });

            /* =======================================================
             * RATE LIMITING
             * ======================================================= */
            builder.Services.AddRateLimiter(options =>
            {
                options.AddFixedWindowLimiter(policyName: "fixed", config =>
                {
                    config.PermitLimit = 5; // Max 5 requests
                    config.Window = TimeSpan.FromSeconds(10); // Every 10 seconds
                    config.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    config.QueueLimit = 2;
                });
            });

            /* =======================================================
             * KESTREL CONFIGURATION
             * ======================================================= */
            builder.WebHost.ConfigureKestrel((context, options) =>
            {
                options.Configure(context.Configuration.GetSection("Kestrel"));
            });

            /* =======================================================
             * BUILD THE APP
             * ======================================================= */
            var app = builder.Build();

            // Base Path for API (reverse proxy scenario)
            app.UsePathBase("/seinfeld");
            app.UseStaticFiles();

            // Swagger UI
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/seinfeld/swagger/v1/swagger.json", "Seinfeld API v1");
                c.RoutePrefix = "swagger"; // Accessible at /seinfeld/swagger
            });

            app.UseRouting();
            app.UseHttpsRedirection();

            // Rate Limiting
            app.UseRateLimiter();

            // Authentication & Authorization (JWT)
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
