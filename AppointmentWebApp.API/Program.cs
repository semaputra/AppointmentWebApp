using AppointmentWebApp.API.Helper;
using AppointmentWebApp.DataAccess.Models;
using AppointmentWebApp.Service.Service;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// In NET Six we can use built in DI Container, 
// Add services to the container.
builder.Services.AddDbContext<AppointmentWebAppDatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppointmentWebAppDatabase") ?? throw new InvalidOperationException("Connection string 'AppointmentWebAppDatabase' not found.")));

builder.Services.AddServicesFromAssembly(Assembly.Load("AppointmentWebApp.Service"));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
