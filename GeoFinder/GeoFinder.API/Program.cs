using GeoFinder.Data;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.OpenApi.Models;
using GeoFinder.Utility.Services.Interface;
using GeoFinder.Utility.Services.Implementation;
using GeoFinder.Utility.Repository;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
    logging.AddFile($@"{Directory.GetCurrentDirectory()}/Logs/log.txt");
});

//builder.Host.ConfigureLogging(logging =>
//{
//    logging.ClearProviders();
//    logging.AddConsole();
//    logging.AddFile($@"{Directory.GetCurrentDirectory()}/Logs/log.txt");

//});


//var builder = WebApplication.CreateBuilder(args);


//ConfigurationManager configuration = builder.Configuration;



//using var loggerFactory = LoggerFactory.Create(loggingBuilder => loggingBuilder
//  .SetMinimumLevel(LogLevel.Trace)
//    .AddConsole());
//loggerFactory.AddFile("Logs/mylog-{Date}.txt");



// //For Logger
//builder.Logging.ClearProviders();
//builder.Services.AddLogging($@"{Directory.GetCurrentDirectory()}C:\Users\pc\source\repos\Geo-Finder-API\GeoFinder");
//Host.CreateDefaultBuilder(args);
//configuration.ConfigureLogging((hostingContext, builder) =>
// {
//     builder.Services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
// });



// Add services to the ApplicationDbContext.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// For Entity Framework
// builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ConnStr")));

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddControllers()
    // for enum value as string
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IGeoFinderService, GeoFinderService>();
builder.Services.AddScoped<IGeoFinderRepository, GeoFinderRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();
//builder.Services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

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
