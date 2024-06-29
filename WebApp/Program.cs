using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Web.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

builder.Services.AddControllersWithViews();

services.AddRouting(options => options.LowercaseUrls = true);

services.AddDbContext<Web.Data.DataContext>(options =>
{
	options.UseSqlServer(Web.Data.Extensions.DataConfigurationManager.GetConnectionString());
});

services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();

services.AddHttpContextAccessor();
services.AddMemoryCache();

services.AddHttpClient();
services.AddScoped<Web.Data.Extensions.WebApi.HttpClientApi>();
services.RegisterDbService(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
}

//app.UseSession();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
