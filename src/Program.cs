using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using sda_3_online_Backend_Teamwork.src.DataBase;
using sda_3_online_Backend_Teamwork.src.Entity;
using sda_3_online_Backend_Teamwork.src.Middlewares;
using sda_3_online_Backend_Teamwork.src.Repository;
using sda_3_online_Backend_Teamwork.src.Services.Category;
using sda_3_online_Backend_Teamwork.src.Services.Order;
using sda_3_online_Backend_Teamwork.src.Services.Product;
using sda_3_online_Backend_Teamwork.src.Services.User;
using sda_3_online_Backend_Teamwork.src.Utils;

var builder = WebApplication.CreateBuilder(args);
var dataSourceBulider = new NpgsqlDataSourceBuilder(
    builder.Configuration.GetConnectionString("Local")
);
dataSourceBulider.MapEnum<Role>();

builder.Services.AddDbContext<DataBaseContext>(options =>
{
    options.UseNpgsql(dataSourceBulider.Build());
});
builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);

builder.Services.AddScoped<IUserService, UserService>().AddScoped<UserRepository, UserRepository>();

builder
    .Services.AddScoped<IProductService, ProductService>()
    .AddScoped<ProductRepository, ProductRepository>();

builder
    .Services.AddScoped<ICategoryService, CategoryService>()
    .AddScoped<CategoryRepository, CategoryRepository>();

builder.Services
.AddScoped<IOrderService, OrderService>()
.AddScoped<OrderRepository, OrderRepository>();


var MyAllowSpecificOrigins = "MyAllowSpecificOrigins";
builder.Services.AddCors(options => {
    options.AddPolicy(name: MyAllowSpecificOrigins,
    policy => {
        policy.WithOrigins("http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod()  
        .SetIsOriginAllowed((host) => true)
        .AllowCredentials();
    });
});

builder
    .Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
            ),
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});

// AddCloudinary
builder.Services.AddCloudinary(builder.Configuration);
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

// Middleware order is updated: Authentication and Authorization first
app.UseAuthentication();
app.UseAuthorization();

// Logging and error handling middlewares
app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<ErrorHandlerMiddleware>();

// Map controllers after authentication/authorization
app.MapControllers();

app.MapGet("", () => "#Sharpers's Backend project");

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DataBaseContext>();

    try
    {
        if (dbContext.Database.CanConnect())
        {
            Console.WriteLine($"Database is connected");
        }
        else
        {
            Console.WriteLine($"Unable to connect to Database");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database failed: {ex.Message}");
    }
}

app.Run();
