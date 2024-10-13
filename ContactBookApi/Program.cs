using Microsoft.EntityFrameworkCore;
using ContactBookApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ContactBookApi.Data;
using Swashbuckle.AspNetCore.Filters;
using AutoMapper;
using ContactBookApi.Options;
using Microsoft.Extensions.Options;

var MyAllowSpecificOrigins = "MyAllowedOrigins";

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.ConfigureOptions<DatabaseOptionsSetup>();

//services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                      });
});


builder.Services.AddControllers();
builder.Services.AddDbContext<ContactBookContext>(
    (services, opt) =>
{
    var databaseOptions = services.GetService<IOptions<DatabaseOptions>>()!.Value;

    opt.UseSqlServer(databaseOptions.ConnectionString, sqloptions =>
    {
        sqloptions.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
        sqloptions.CommandTimeout(databaseOptions.CommandTimeout);
    });

    opt.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);
    opt.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging); // be careful, make it is in development environment only, it may lead to leak sensitive information 
});


builder.Services.AddAutoMapper(typeof(ContactBookProfile)); 

builder.Services.AddTransient<IAuthRepo, AuthRepo>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddAuthentication (JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opts =>
{
    opts.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c =>
{
    c.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "standard authorization using bearer scheme",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
    });
    c.OperationFilter<SecurityRequirementsOperationFilter>();
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();


app.UseCors("MyAllowedOrigins");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
