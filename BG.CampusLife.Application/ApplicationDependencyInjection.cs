using BG.CampusLife.Application.Common.Behaviours;
using BG.CampusLife.Application.Identity.Commands.ConfirmEmailToken;
using BG.CampusLife.Application.Identity.Commands.Login;
using BG.CampusLife.Application.Identity.Commands.RefreshToken;
using BG.CampusLife.Application.Identity.Commands.Register;
using BG.CampusLife.Application.Identity.Commands.ResetPassword;
using FluentValidation.AspNetCore;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using BG.CampusLife.Application.Locations.Commands.CreateLocation;

namespace BG.CampusLife.Application
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateLocationValidation>());


            return services;
        }
    }
}