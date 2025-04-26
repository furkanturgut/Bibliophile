using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// DbContext ve diğer tüm servisleri app'i oluşturmadan ÖNCE kaydedin
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDataContext>(options=>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddIdentity<AppUser, IdentityRole>(Options=> 
{
    // Identity konfigürasyonu...
}).AddEntityFrameworkStores<ApplicationDataContext>().AddDefaultTokenProviders();

builder.Services.AddAuthentication(Options=>
{
    // Authentication konfigürasyonu...
}).AddJwtBearer(Options=>
{
    // JWT konfigürasyonu...
});

// Tüm servisler kaydedildikten SONRA app'i oluşturun
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.Run();

