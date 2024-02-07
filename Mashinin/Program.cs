using FluentValidation.AspNetCore;
using Mashinin;
using Mashinin.Extensions;
using Mashinin.Logger;
using Mashinin.Middlewares;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NLog;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json")
    .Build();

string connectionString = configuration.GetConnectionString("Default");

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "VINCODE_API",
        policy =>
        {
            //policy.AllowAnyHeader()
            //      .AllowAnyMethod()
            //      .WithOrigins("");
            policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
        });
});

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddFluentValidationAutoValidation();

//builder.Services.IdentityBuilder();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

//builder.Services.AddAuth(configuration);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.ServicesBuilder();

builder.Services.AddMemoryCache();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ExceptionHandling(logger);

app.UseMiddleware<LanguageMiddleware>();

//app.UseStaticFiles();

app.UseRouting();

//app.UseAuthentication();

//app.UseAuthorization();

app.UseCors("VINCODE_API");

app.MapControllers();

app.Run();
