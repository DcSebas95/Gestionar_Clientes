using System.ComponentModel;
using Clientes.DATA;
using Microsoft.Extensions.Options;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ClientesData>();

//corse son para que no halla conflicto entre mi el back y el front 
builder.Services.AddCors(Options =>
{
    Options.AddPolicy("NuevaPolitica", app =>
        {
            app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("NuevaPolitica");

app.UseAuthorization();

app.MapControllers();

// 3. (Opcional) Imprimir en consola la ruta de Swagger
Console.WriteLine("Swagger UI disponible en: http://localhost:5010/swagger/index.html");

app.Run();
