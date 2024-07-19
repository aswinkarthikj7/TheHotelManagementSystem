
using Core.Interfaces;
using Core.Models;
using Core.Services;
using MongoDB.Driver;
using TheHotelManagementSystem.Middlewares;

namespace TheHotelManagementSystem
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

            builder.Services.Configure<HotelDatabaseSettings>(builder.Configuration.GetSection("TheHotelManagementSettings"));

            builder.Services.AddSingleton<IMongoClient>(_ => {
                var connectionString =
                    builder
                        .Configuration
                        .GetSection("TheHotelManagementSettings:ConnectionString")?
                        .Value;

                return new MongoClient(connectionString);
            });

            builder.Services.AddSingleton<ICategoryService, CategoryService>();
            builder.Services.AddSingleton<IRoomService, RoomService>();
            builder.Services.AddSingleton<IBookingService, BookingService>();
            builder.Services.AddSingleton<IAvailabilityService, AvailabilityService>();
            builder.Services.AddSingleton<ICheckInService, CheckInService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseMiddleware<GlobalExceptionMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
