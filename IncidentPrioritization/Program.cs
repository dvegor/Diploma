using IncidentPrioritization.Configs;
using IncidentPrioritization.Interfaces;
using IncidentPrioritization.Models;
using IncidentPrioritization.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using System;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddDbContext<ITSMContext>(options =>
              options.UseSqlServer(configuration.GetConnectionString("MyAppConnection")));


builder.Services.Configure<ITSMConfiguration>(configuration.GetSection(nameof(ITSMConfiguration)));
builder.Services.AddSingleton<IWebApi, WebApi>();
builder.Services.AddScoped<IITSMService, ITSMService>();
builder.Services.AddTransient<IDataService, DataService>();

builder.Services.AddControllers()
    .AddNewtonsoftJson(setupAction => setupAction.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());

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

app.UseAuthorization();

app.MapControllers();

app.Run();
