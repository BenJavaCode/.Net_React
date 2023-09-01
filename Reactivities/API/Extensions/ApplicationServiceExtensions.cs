
using Application.Activities;
using Application.Core;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // Database connection using DataContext class
            services.AddDbContext<DataContext>(opt=>{
                opt.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });

            // For allowing all requests from "http://localhost:3000" REACT APP
            services.AddCors( opt => {
                opt.AddPolicy("CorsPolicy", policy => 
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
                });
            });

            // Register handlers (stupid name its the list mediator) mediator. Registers all handlers found in dir of List
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(List.Handler).Assembly));

            // Find all mappingprofiles in assembly and register them
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);

            return services;
        }
        
    }
}