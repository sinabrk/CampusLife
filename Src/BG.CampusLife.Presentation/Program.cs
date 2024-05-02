namespace BG.CampusLife.Presentation;

public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var campusContext = services.GetRequiredService<CampusContext>();
                campusContext.Database.Migrate();
                
                var identityContext = services.GetRequiredService<IdentityDbContext>();
                identityContext.Database.Migrate();


                // Test Development
                // In Future can be real data
                IdentitySeed.SeedAsync(services.GetService<UserManager<ApplicationUser>>(),
                    services.GetService<RoleManager<IdentityRole>>());
                identityContext.Database.CloseConnection();
                campusContext.Database.CloseConnection();
                CampusSeed.SeedAsync(campusContext, identityContext);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
}