using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProd)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
        }

        private static void SeedData(AppDbContext context, bool isProd)
        {
            if (isProd)
            {
                Console.WriteLine("Attempting to apply migrations.");
                try
                {
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Could not run migrations. Error: {ex.Message}");
                }
            }

            if (!context.Platforms.Any())
            {
                Console.WriteLine("Seeding data...");

                context.Platforms.AddRange(
                    new Platform() { Name = "Dot Net", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "Docker", Publisher = "Docker Inc.", Cost = "Free" },
                    new Platform() { Name = "Amazon Web Services", Publisher = "Amazon", Cost = "Demand" }
                    );

                context.SaveChanges();
            }
            else
                Console.WriteLine("Already have data.");
        }
    }
}
