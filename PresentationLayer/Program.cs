using AutoMapper;
using BuisnessLayer.Interfaces;
using BuisnessLayer.Services;
using DataLayer.EfContext;
using DataLayer.Entities;
using DataLayer.Interfaces;
using DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using PresentationLayer.Middlewares;
using PresentationLayer.Services;

namespace PresentationLayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IPolygonService, PolygonService>();
            builder.Services.AddScoped<IStorageService, StorageService>();

            var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));
            IMapper mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);

            var connection = builder.Configuration.GetConnectionString("B2Group");
            builder.Services.AddDbContext<PolygonContext>
            (options =>
            {
                options.UseNpgsql(connection, b => b.MigrationsAssembly("DataLayer"));

            });

            builder.Services.AddTransient<IRepository<Polygon>,PolygonRepository>();
            builder.Services.AddTransient<IUnitOfWork,EfUnitOfWork>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            var app = builder.Build();

            app.UseMiddleware<ErrorHandlerMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAllOrigins");
            app.MapControllers();
            app.Run();
        }
    }
}
