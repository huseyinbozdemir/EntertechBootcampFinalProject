using EntertechFP.DAL.Abstract;
using EntertechFP.DAL.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace EntertechFP.DAL.Extensions
{
    public static class ServiceDALExtension
    {
        public static IServiceCollection AddDALDependencies(this IServiceCollection services)
        {
            services.AddScoped<IEventDal, EventDal>();
            services.AddScoped<IEventAttendanceDal, EventAttendanceDal>();
            services.AddScoped<IUserDal, UserDal>();
            services.AddScoped<INotificationDal, NotificationDal>();
            services.AddScoped<ICityDal, CityDal>();
            services.AddScoped<ICategoryDal, CategoryDal>();
            services.AddScoped<IEntegratorDal, EntegratorDal>();
            services.AddScoped<IEntegratorEventDal, EntegratorEventDal>();
            return services;
        }
    }
}
