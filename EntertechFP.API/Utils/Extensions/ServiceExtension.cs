using AutoMapper;
using EntertechFP.BL.Abstract;
using EntertechFP.BL.Concrete;
using EntertechFP.DAL.Abstract;
using EntertechFP.DAL.Concrete;

namespace EntertechFP.API.Utils.Extensions
{
    public static class ServiceExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IEventDal, EventDal>();
            services.AddSingleton<IEventAttendanceDal, EventAttendanceDal>();
            services.AddSingleton<IUserDal, UserDal>();
            services.AddSingleton<INotificationDal, NotificationDal>();
            services.AddSingleton<ICityDal, CityDal>();
            services.AddSingleton<ICategoryDal, CategoryDal>();
            services.AddSingleton<IEntegratorDal, EntegratorDal>();
            services.AddSingleton<IEntegratorEventDal, EntegratorEventDal>();
                     
            services.AddSingleton<ICategoryService, CategoryService>();
            services.AddSingleton<ICityService, CityService>();
            services.AddSingleton<IEntegratorService, EntegratorService>();
            services.AddSingleton<IEntegratorEventService, EntegratorEventService>();
            services.AddSingleton<IEventAttendanceService, EventAttendanceService>();
            services.AddSingleton<IEventService, EventService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<INotificationService, NotificationService>();
        }
    }
}
