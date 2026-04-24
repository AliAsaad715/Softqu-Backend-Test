using Microsoft.EntityFrameworkCore;
using Softqu.API.Middleware;
using Softqu.Infrastructure.Data;
using Softqu.Infrastucture.Seeders;

namespace Softqu.API.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task UseDatabaseConfiguration(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();
                await context.Database.MigrateAsync();
                await SeedData.SeedAsync(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while seeding the database.");
            }
        }

        public static void UseApiDocumentation(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseScalarDashboard();
            }
        }

        public static void UseApplicationMiddleware(this WebApplication app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
        }
    }
}
