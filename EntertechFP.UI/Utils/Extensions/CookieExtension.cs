namespace EntertechFP.UI.Extensions
{
    public static class CookieExtension
    {
        public static IServiceCollection AddCookies(this IServiceCollection services)
        {
            services.AddAuthentication(options => options.DefaultScheme = "admin_scheme")
                .AddCookie("admin_scheme", options =>
                {
                    options.LoginPath = "/login";
                    options.Cookie.Name = "admin_session";
                    options.AccessDeniedPath = "/login";
                    options.LogoutPath = "/login";

                });
            services.AddAuthentication(options => options.DefaultScheme = "user_scheme")
                .AddCookie("user_scheme", options =>
                {
                    options.LoginPath = "/login";
                    options.Cookie.Name = "user_session";
                    options.AccessDeniedPath = "/login";
                    options.LogoutPath = "/login";

                });
            services.AddAuthentication(options => options.DefaultScheme = "entegrator_scheme")
                .AddCookie("entegrator_scheme", options =>
                {
                    options.LoginPath = "/login";
                    options.Cookie.Name = "entegrator_session";
                    options.AccessDeniedPath = "/login";
                    options.LogoutPath = "/login";

                });
            return services;
        }
    }
}
