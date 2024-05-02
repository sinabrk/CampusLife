using BG.CampusLife.Application.Identity.DTOs;
using BG.CampusLife.Application.Interfaces;
using BG.CampusLife.Domain.Entities;
using BG.CampusLife.Infrastructure.Identity;
using BG.CampusLife.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using BG.CampusLife.Application.Interfaces.Services;
using BG.CampusLife.Infrastructure.Notifications;


namespace BG.CampusLife.Infrastructure
{
    public static class InfraStructureDependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<ISignInManager, SignInManager>();
            services.AddDbContext<CampusContext>(options => options.UseSqlServer(configuration.GetConnectionString("CampusContext")));
            services.AddDbContext<IdentityDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("CampusContext")));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<INotificationService, NotificationService>();

            
            services.AddScoped<ICurrentUserService, CurrentUserService>();

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
              });

            return services;
        }
    }
}