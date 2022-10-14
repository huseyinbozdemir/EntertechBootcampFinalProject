using EntertechFP.BL.Abstract;
using EntertechFP.BL.Concrete;
using EntertechFP.DAL.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace EntertechFP.BL.Extensions
{
    public static class ServiceBLExtension
    {
        public static IServiceCollection AddBLDependencies(this IServiceCollection services)
        {
            services.AddDALDependencies();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IEventAttendanceService, EventAttendanceService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IEntegratorService, EntegratorService>();
            services.AddScoped<IEntegratorEventService, EntegratorEventService>();
            return services;
        }
    }
}
