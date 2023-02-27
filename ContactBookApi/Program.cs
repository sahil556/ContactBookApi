using Microsoft.EntityFrameworkCore;
using ContactBookApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using ContactBookApi.Data;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

// Add services to the container.


//services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

builder.Services.AddControllers();
builder.Services.AddDbContext<ContactBookContext>(opt =>
    opt.UseSqlServer("ContactBookDB"));

builder.Services.AddTransient<IAuthRepo, AuthRepo>();
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
//app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
