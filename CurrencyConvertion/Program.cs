using CurrencyConvertion.Context;
using CurrencyConvertion.Interfaces;
using CurrencyConvertion.Services;
using CurrencyConvertion.ViewModels;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CurrencyConvertion.Services.BackgroundJobs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ApiKeyAuthAndRetryAttribute>();

builder.Services.AddDbContext<CurrencyDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0); // v1.0
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;

    // Use URL segment versioning
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<CurrencyRateUrl>(builder.Configuration.GetSection("CurrencyRateUrl"));
builder.Services.AddScoped<ICurrencyRateService, CurrencyRateService>();
builder.Services.AddHttpClient();
builder.Services.AddHostedService<ExchangeRateFetchService>();
builder.Services.AddHostedService<HistoricalRateFetchService>();

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
