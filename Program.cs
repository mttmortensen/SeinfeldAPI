
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using SeinfeldAPI.Data;
using SeinfeldAPI.Interfaces;
using SeinfeldAPI.Repo;
using SeinfeldAPI.Services;
using System.Threading.RateLimiting;

namespace SeinfeldAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<IEpisodeRepository, EpisodeRepoistory>();
            builder.Services.AddScoped<IEpisodeQuotesRepository, EpisodeQuotesRepoistory>();
            builder.Services.AddScoped<IEpisodeService, EpisodeService>();
            builder.Services.AddScoped<IEpisodeQuotesService, EpisodeQuotesService>();

            // Set the custom port for Kestrel
            // This is going to be used for hosting this as a service on MRTN-APPS
            builder.WebHost.ConfigureKestrel((context, options) =>
            {
                // Only configure if explicitly in config
                options.Configure(context.Configuration.GetSection("Kestrel"));
            });
            
            /*
             * When you return a list of Episodes with their quotes 
             * which each have an episode, which has quotes, 
             * it becomes a never-ending cycle, which System.Text.Json can’t handle by default.
             */
            builder.Services.AddControllers()
                .AddJsonOptions(x =>
                    x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve);

            // === Swagger / OpenAPI ===
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Seinfeld API",
                    Version = "v1"
                });

                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            // DB Connection Service
            builder.Services.AddDbContext<SeinfeldDbContext>(options => 
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            // Rate Limiting
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

            var app = builder.Build();

            // Enable Swagger for all environments (or restrict to dev if preferred)
            app.UsePathBase("/seinfeld");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Seinfeld API v1");
                c.RoutePrefix = "swagger"; // URL will be /swagger


            });

            app.UseHttpsRedirection();

            app.UseRateLimiter();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
