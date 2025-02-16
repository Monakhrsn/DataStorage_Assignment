using Business.Interfaces;
using Business.Services;
using Data.Contexts;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container
builder.Services.AddControllers();
//builder.Services.AddOpenApi();

builder.Services.AddDbContext<DataContext>(x =>
    x.UseSqlServer(
        @"Server=localhost;Database=MLCDatabase;User Id=sa;Password=VerySecret1234;TrustServerCertificate=True;Encrypt=False")
        .LogTo(Console.WriteLine, LogLevel.Information));
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IStatusTypeRepository, StatusTypeRepository>();
builder.Services.AddScoped<IStatusTypeService, StatusTypeService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
// builder.Services.AddScoped<IRollService, RollService>();
// builder.Services.AddScoped<IRollRepository, RollRepository>();

// Configure Cross-Origin Resource Sharing to allow all origins, headers, and methods
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseCors();

app.MapGet("/", () => Results.Content("<h1>Welcome eee to the WebAPI!</h1><p><a href='/api/customers'>View Customers</a></p>", "text/html"));

app.Run();
