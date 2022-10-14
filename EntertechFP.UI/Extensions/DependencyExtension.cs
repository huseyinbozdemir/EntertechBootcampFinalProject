using EntertechFP.UI.Utils;

namespace EntertechFP.UI.Extensions
{
    public static class DependencyExtension
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddScoped<CookieHelper>();
            services.AddScoped(x => new RequestHelper(configuration.GetSection("ApiKey").Value));
            return services;
        }
    }
}
