using Microsoft.EntityFrameworkCore;
using ProjetoRaizes.Data;
using ProjetoRaizes.Interfaces;
using ProjetoRaizes.Repositories;
using ProjetoRaizes.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 1. Configurar a conexão com o Banco de Dados (usando uma string de conexão local)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Registrar seu par de Repository
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

// 3. Registrar seu par de Service
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddScoped<IItemCardapioRepository, ItemCardapioRepository>();
builder.Services.AddScoped<IItemCardapioService, ItemCardapioService>();

builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IPedidoService, PedidoService>();




var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // É isso aqui que cria a página HTML que você acessa
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
