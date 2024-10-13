using Microsoft.EntityFrameworkCore;
using SIRCADE.ApiCore.Models;
using SIRCADE.Notifications.Worker.Common.Extensions;
using SIRCADE.Notifications.Worker.Processes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add persistence services to the container.
builder.Services.AddPersistence();

// Add services to the container.
builder.Services.AddServices();

// Add strategies to the container.
builder.Services.AddStrategies();

// Add factories to the container.
builder.Services.AddFactories();

// Add hosted services to the container.
builder.Services.AddHostedService<UserNotificationsTask>();

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
