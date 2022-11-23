using System.Text.Json.Serialization;
using Constech.API.Authorization.Handlers.Interfaces;
using Constech.API.Authorization.Settings;
using Constech.API.Domain.Models;
using Constech.API.Domain.Repositories;
using Constech.API.Domain.Services;
using Constech.API.Persistence.Repositories;
using Constech.API.Security.Authorization.Handlers.Implementations;
using Constech.API.Security.Authorization.Middleware;
using Constech.API.Services;
using Constech.API.Shared.Domain.Repositories;
using Constech.API.Shared.Persistence;
using Constech.API.Shared.Persistence.Contexts;
using Constech.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(); //! Add CORS

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ACME Learning Center API",
        Description = "ACME Learning Center RESTful API",
        TermsOfService = new Uri("https://acme-learning.com/tos"),
        Contact = new OpenApiContact
        {
            Name = "ACME.studio",
            Url = new Uri("https://acme.studio")
        },
        License = new OpenApiLicense
        {
            Name = "ACME Learning Center Resources License",
            Url = new Uri("https://acme-learning.com/license")
        }
    });
    options.EnableAnnotations();
    options.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "JWT Authorization header using the Bearer scheme."
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth" }
            },
            Array.Empty<string>()
        }
    });
});

// Configure the HTTP request pipeline
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(
    opts => opts.UseMySQL(connectionString)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors()
); //! Database Connection

//? Add lowercase routes
builder.Services.AddRouting(opts => opts.LowercaseUrls = true);

//? Dependency Injection Configuration

// Shared Injection Configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// API Injection Configuration

builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectService, ProjectService>();

builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICompanyService, CompanyService>();

// Security Injection Configuration

builder.Services.AddScoped<IJwtHandler, JwtHandler>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// AutoMapper Configuration
builder.Services.AddAutoMapper(
    typeof(Constech.API.Mapping.ModelToResourceProfile),
    typeof(Constech.API.Security.Mapping.ModelToResourceProfile),
    typeof(Constech.API.Mapping.ResourceToModelProfile),
    typeof(Constech.API.Security.Mapping.ResourceToModelProfile)
);


var app = builder.Build();

// Validation for ensuring Database Objects are created
using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<AppDbContext>())
{
    // if (app.Environment.IsDevelopment())
    //     context.Database.EnsureDeleted();
    
    context.Database.EnsureCreated();

    // if (app.Environment.IsDevelopment())
    //     new Seeder(context).Seed();
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opts =>
    {
        opts.SwaggerEndpoint("v1/swagger.json", "v1");
        opts.RoutePrefix = "swagger";
    });
}

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseMiddleware<JwtMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();