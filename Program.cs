
using SeinfeldAPI.Data;
using Microsoft.EntityFrameworkCore;
using SeinfeldAPI.Interfaces;
using SeinfeldAPI.Repo;
using SeinfeldAPI.Services;

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


            /*
             * When you return a list of Episodes with their quotes 
             * which each have an episode, which has quotes, 
             * it becomes a never-ending cycle, which System.Text.Json can’t handle by default.
             */
            builder.Services.AddControllers()
                .AddJsonOptions(x =>
                    x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve);

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            // DB Connection Service
            builder.Services.AddDbContext<SeinfeldDbContext>(options => 
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
