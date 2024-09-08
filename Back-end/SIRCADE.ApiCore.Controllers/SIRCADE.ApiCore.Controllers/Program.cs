using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SIRCADE.ApiCore.Controllers.Common.Extensions;
using SIRCADE.ApiCore.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder
    .Services
    .AddControllers()
    .AddJsonOptions(jsonOptions => 
        jsonOptions.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header
    });

    c.AddSecurityRequirement(new()
    {
        {
            new()
            {
                Reference = new()
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Add Db context to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add common services to the container.
builder.Services.AddCommonServices();

// Add persistence services to the container.
builder.Services.AddPersistence();

// Add services to the container.
builder.Services.AddServices();

// Add authentication to the container.
builder.Services.AddAuthentication(builder);

// Add strategies to the container
builder.Services.AddStrategies();

// Add factories to the container
builder.Services.AddFactories();

builder.Services.AddHttpContextAccessor();


// Enable CORS to connect with the SIRCADE angular projects
builder.Services.AddCors(x =>
{
    x.AddDefaultPolicy(options =>
    {
        options
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

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

app.UseCors();

app.Run();
