using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var services = builder.Services;

services.AddHttpContextAccessor();

services.AddRouting(options => options.LowercaseUrls = true);

services.Configure<FormOptions>(x =>
{
	x.ValueLengthLimit = int.MaxValue;
	x.MultipartBodyLengthLimit = long.MaxValue; // In case of multipart
});

services.AddMemoryCache();
services.AddHttpClient();

services.AddDbContext<Web.Data.DataContext>(options =>
{
	options.UseSqlServer(DataConfigurationManager.GetConnectionString());
});

services.AddCors(options => options.AddPolicy("cors", builder =>
{
	builder
	.AllowAnyOrigin()
	.WithMethods("GET", "PUT", "POST", "DELETE")
	.AllowAnyHeader();
}));

services.RegisterDbService(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//	app.UseSwagger();
//	app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("cors");
//app.UseAuthentication();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
