using EntertechFP.UI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCookies()
    .AddDependencies(builder.Configuration)
    .AddControllersWithViews();

var app = builder.Build();

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
