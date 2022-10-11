using AutoMapper;
using EntertechFP.API.Mappers;
using EntertechFP.BL.Abstract;
using EntertechFP.BL.Concrete;
using EntertechFP.DAL.Abstract;
using EntertechFP.DAL.Concrete;
using EntertechFP.DAL.Concrete.Contexts;

namespace EntertechFP.API.Utils.Extensions
{
    public static class ServiceExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IEventDal, EventDal>();
            services.AddScoped<IEventAttendanceDal, EventAttendanceDal>();
            services.AddScoped<IUserDal, UserDal>();
            services.AddScoped<INotificationDal, NotificationDal>();
            services.AddScoped<ICityDal, CityDal>();
            services.AddScoped<ICategoryDal, CategoryDal>();
            services.AddScoped<IEntegratorDal, EntegratorDal>();
            services.AddScoped<IEntegratorEventDal, EntegratorEventDal>();

            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IEventAttendanceService, EventAttendanceService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IEntegratorService, EntegratorService>();
            services.AddScoped<IEntegratorEventService, EntegratorEventService>();
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddControllers().AddNewtonsoftJson(opt =>
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

        }
    }
}
