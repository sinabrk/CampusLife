namespace BG.CampusLife.Persistence;

public static class InfraStructureDependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<CampusContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("CampusContext")));

        services.AddScoped<ICampusContext>(provider => provider.GetService<CampusContext>());

        return services;
    }
}