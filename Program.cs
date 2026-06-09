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

//Configuracao do Banco de dados(Conexao local)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Registra o repository e o service do usuario
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

//Registra o repository e o service dos itens do cardapio
builder.Services.AddScoped<IItemCardapioRepository, ItemCardapioRepository>();
builder.Services.AddScoped<IItemCardapioService, ItemCardapioService>();

//Registra o repository e o service do pedido
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IPedidoService, PedidoService>();




var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
