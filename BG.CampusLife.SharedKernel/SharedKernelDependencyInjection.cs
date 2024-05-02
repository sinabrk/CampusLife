using BG.CampusLife.Application.Interfaces.Repositories;
using BG.CampusLife.SharedKernel.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace BG.CampusLife.SharedKernel
{
    public static class SharedKernelDependencyInjection
    {
        public static IServiceCollection AddSharedKernelDependencies(this IServiceCollection services)
        {
            services.AddTransient<IUniversityRepository, UniversityRepository>();
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<ILocationRepository, LocationRepository>();
            services.AddTransient<IFriendRepository, FriendRepository>();
            services.AddTransient<ICalendarRepository, CalendarRepository>();
            services.AddTransient<IEventRepository, EventRepository>();
            services.AddTransient<INotificationRepository, NotificationRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            return services;
        }
    }
}
