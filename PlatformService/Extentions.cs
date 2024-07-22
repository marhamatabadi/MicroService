using PlatformService.Data;
using PlatformService.Models;

namespace PlatformService
{
    public static class Extentions
    {
        public static void InitDb(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            SeedData(scope.ServiceProvider.GetRequiredService<AppDbContext>());
        }

        private static void SeedData(AppDbContext dbContext)
        {
            if (!dbContext.Platforms.Any())
            {
                dbContext.Platforms.AddRange(
                    new Platform { Name="Dotnet",Publisher="Microsoft",Cost="Free"},
                    new Platform { Name="SQL Server",Publisher="Microsoft",Cost="Free"},
                    new Platform { Name="Kubernetes",Publisher="Microsoft",Cost="Free"},
                    new Platform { Name="Azure",Publisher="Microsoft",Cost="Free"}
                    );
                dbContext.SaveChanges();
            }
        }
    }
}
