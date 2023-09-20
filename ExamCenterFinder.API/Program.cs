using System.Reflection;

using ExamCenterFinder.API.Data.Context;
using ExamCenterFinder.API.Data.InitialSeed;

using Microsoft.EntityFrameworkCore;

namespace ExamCenterFinder.API
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

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            // Ask the service provider for the configuration abstraction.
            var serviceProvider = builder.Services.BuildServiceProvider();
            IConfiguration config = serviceProvider.GetService<IConfiguration>();

            // Configure the DbContext with the connection string
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                var cs = config.GetConnectionString("DefaultConnection");
                options.UseSqlServer(cs);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            RunMigration(builder);

            app.Run();
        }

        public static void RunMigration(WebApplicationBuilder builder)
        {
            var serviceProvider = builder.Services.BuildServiceProvider();

            try
            {
                var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();
                DbSeed.SeedData(context);
            }
            catch (Exception ex)
            {
                var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An exception Occured during migration");
            }
        }
    }
}