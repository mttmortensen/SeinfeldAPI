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
using System.Security.Claims;
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
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

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

            // Only these hosts (the NGINX reverse proxy) may supply the client IP via CF-Connecting-IP
            _trustedProxies = builder.Configuration.GetSection("TrustedProxies").Get<string[]>() ?? [];

            builder.Services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                // Each client gets its own bucket: logged-in users by username, everyone else by IP
                options.AddPolicy("fixed", context =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        context.User.Identity?.IsAuthenticated == true
                            ? $"user:{context.User.FindFirstValue(ClaimTypes.NameIdentifier)}"
                            : $"ip:{GetClientIp(context)}",
                        _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 5, // Max 5 requests
                            Window = TimeSpan.FromSeconds(10), // Every 10 seconds
                            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                            QueueLimit = 2
                        }));

                // Stricter per-IP limit on login/register to slow password guessing and mass sign-ups
                options.AddPolicy("auth", context =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        $"ip:{GetClientIp(context)}",
                        _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 5, // Max 5 attempts
                            Window = TimeSpan.FromMinutes(1), // Every minute
                            QueueLimit = 0
                        }));
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

            // Authentication runs first so the rate limiter can partition by user
            app.UseAuthentication();

            // Rate Limiting
            app.UseRateLimiter();

            // Authorization (JWT roles)
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        private static string[] _trustedProxies = [];

        // Behind Cloudflare Tunnel + NGINX every request arrives from the proxy,
        // so the real client IP comes from Cloudflare's CF-Connecting-IP header.
        // The header is ignored from anyone else so LAN clients can't spoof it.
        private static string GetClientIp(HttpContext context)
        {
            string remoteIp = context.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? "unknown";
            string? cfIp = context.Request.Headers["CF-Connecting-IP"].FirstOrDefault();

            return _trustedProxies.Contains(remoteIp) && !string.IsNullOrWhiteSpace(cfIp)
                ? cfIp
                : remoteIp;
        }
    }
}
