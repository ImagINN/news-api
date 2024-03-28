using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using News.Server.Data;
using News.Server.Repositories;
using News.Server.Repositories.Contracts;
using News.Server.Services;
using News.Server.Services.Contracts;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7069/") });

/// summary
/// This code block allows the project to be connected to the MSSQL database.
/// summary
builder.Services.AddDbContext<NewsDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"));
});


builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddScoped<INewsRepository, NewsRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<INewsOperationRepository, NewsOperationRepository>();
builder.Services.AddScoped<IUserOperationRepository, UserOperationRepository>();

// Authenticate 
var issuer = builder.Configuration["JwtConfig:Issuer"];
var audience = builder.Configuration["JwtConfig:Audience"];
var signingKey = builder.Configuration["JwtConfig:SigninKey"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey))
    };
});

var app = builder.Build();

app.UseCors(policy =>
    policy.WithOrigins("http://localhost:7060", "https://localhost:7060")
    .AllowAnyMethod()
    .WithHeaders(HeaderNames.ContentType)
);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
