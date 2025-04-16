using api_acesso_ia;
using api_acesso_ia.Repositories.Interfaces;
using api_acesso_ia.Repositories;
using api_acesso_ia.Services.Interfaces;
using api_acesso_ia.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurando CORS
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:8080")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

// Configurando DB MySQL
var connectionString = builder
    .Configuration
    .GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString,
    ServerVersion.Parse("5.7.0"))); // Altere a versão se necessário

// Injeção de dependência
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();

builder.Services.AddScoped<IAcessoService, AcessoService>();
builder.Services.AddScoped<IAcessoRepository, AcessoRepository>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuração do pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// HTTPS só se não for desenvolvimento
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
