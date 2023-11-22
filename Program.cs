global using System.Text.Json.Serialization;
global using dotnet_rpg.Models;
global using Microsoft.AspNetCore.Mvc;
global using dotnet_rpg.Services.CharacterService;
global using dotnet_rpg.Dtos.Character;
global using AutoMapper;
global using Microsoft.EntityFrameworkCore;
global using dotnet_rpg.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string? strConnection = builder.Configuration.GetConnectionString("ConnectionStrings:DefaultConn");
builder.Services.AddDbContext<Context>(options => options.UseSqlServer(strConnection));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<ICharacterService, CharacterService>();

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
