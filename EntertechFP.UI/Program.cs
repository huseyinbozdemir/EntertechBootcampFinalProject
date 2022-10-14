using EntertechFP.UI.Utils;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(options => options.DefaultScheme = "admin_scheme")
    .AddCookie("admin_scheme", options =>
    {
        options.LoginPath = "/login";
        options.Cookie.Name = "admin_session";
        options.AccessDeniedPath = "/login";
        options.LogoutPath = "/login";

    });
builder.Services.AddAuthentication(options => options.DefaultScheme = "user_scheme")
    .AddCookie("user_scheme", options =>
    {
        options.LoginPath = "/login";
        options.Cookie.Name = "user_session";
        options.AccessDeniedPath = "/login";
        options.LogoutPath = "/login";

    });
builder.Services.AddScoped<CookieHelper>();
builder.Services.AddScoped(x => new RequestHelper(builder.Configuration.GetSection("ApiKey").Value));
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
