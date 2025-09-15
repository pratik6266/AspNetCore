using _02_apis.Db;
using _02_apis.Repository.Implementation;
using _02_apis.Repository.Interface;
using _02_apis.Service.Implementation;
using _02_apis.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()              // Minimum log level
    .WriteTo.Console()                 // Log to console
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day) // Log to files daily
    .CreateLogger();    
builder.Host.UseSerilog();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<DbContextProvider>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGetAllUsers, GetAllUsers>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.MapControllers();

Log.Information("Starting up the application");
app.Run();