
using BG.CampusLife.Application.Interfaces.Repositories;
using BG.CampusLife.Application.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace BG.CampusLife.Infrastructure;

public static class InfraStructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<IEmailSender, EmailSender>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddDbContext<IdentityDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("CampusContext")));


        services.AddDefaultIdentity<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<IdentityDbContext>();

        services.AddIdentityServer()
            .AddApiAuthorization<ApplicationUser, IdentityDbContext>();

        services.Configure<IdentityOptions>(options =>
        {
            // Default Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 3;
            options.Lockout.AllowedForNewUsers = true;

            // Default Password settings.
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 8;
            options.Password.RequiredUniqueChars = 1;
        });

        services.Configure<TokenManagement>(configuration.GetSection("tokenManagement"));
        var token = configuration.GetSection("tokenManagement").Get<TokenManagement>();

        services.AddAuthentication(cfg =>
            {
                cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddIdentityServerJwt()
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(token.Secret)),
                    ValidIssuer = token.Issuer,
                    ValidAudience = token.Audience,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                x.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        // If the request is for our hub...
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) &&
                            (path.StartsWithSegments("/notification")))
                        {
                            // Read the token out of the query string
                            Debug.Write(accessToken);
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                  
                };
            });

        //Repositories
        services.AddTransient<IRepositories, DbRepositories>();
        services.AddTransient<ICategoryRepository, DbRepositories>();
        services.AddTransient<IDocumentRepository, DbRepositories>();
        services.AddTransient<IEventRepository, DbRepositories>();
        services.AddTransient<ILocationRepository, DbRepositories>();
        services.AddTransient<IMarketRepository, DbRepositories>();
        services.AddTransient<INotificationRepository, DbRepositories>();
        services.AddTransient<IPostRepository, DbRepositories>();
        services.AddTransient<IPropertyRepository, DbRepositories>();
        services.AddTransient<ITagRepository, DbRepositories>();
        services.AddTransient<IUniversityRepository, DbRepositories>();
        services.AddTransient<IUserCalendarRepository, DbRepositories>();
        services.AddTransient<IUserRepository, DbRepositories>();
        
        //Services
        services.AddScoped<IUserManager, DbRepositories>();
        services.AddScoped<ISignInManager, DbRepositories>();
        services.AddTransient<INotificationService, DbRepositories>();

        return services;
    }
}