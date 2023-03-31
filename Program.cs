using AuctionApi.Data;
using AuctionApi.Repositories.Interfaces;
using AuctionApi.Repositories.Users;
using AuctionApi.Services.Interfaces;
using AuctionApi.Services.Users;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Desactivar la verificación del certificado SSL
SqlConnection.ClearAllPools();
System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };


// Configurar la conexión a la base de datos
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer("Data Source=DESKTOP-C6TOJU0\\SQLEXPRESS;Initial Catalog=AuctionDB;Integrated Security=True;Encrypt=False"));

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
    options.RespectBrowserAcceptHeader = true;
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

}).ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
    options.SuppressMapClientErrors = true;
    options.SuppressInferBindingSourcesForParameters = true;
});// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Configurar la inyección de dependencias
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IUserService, UserService>();
// Configurar el HttpClient personalizado
builder.Services.AddSingleton<HttpClientHandler>(new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
});
builder.Services.AddHttpClient("custom", client =>
{
    client.BaseAddress = new Uri("https://localhost:7085");
}).ConfigurePrimaryHttpMessageHandler<HttpClientHandler>();

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
