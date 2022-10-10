﻿using AutoMapper;
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
        }
    }
}
