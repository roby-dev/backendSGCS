using backendSGCS.Controllers;
using backendSGCS.Models;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContextFactory<dbSGCSContext>();
builder.Services.AddTransient<UsuarioController>();
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

/// <Default>
/// Ruta principal del backend
/// </Default>
app.MapGet("/", () => Results.LocalRedirect("~/swagger/index.html"));

/// <Model>
/// Rutas para el CRUD del modelo Usuario
/// </Model>
app.MapGet("/api/usuarios", UsuarioController.getUsers);
app.MapGet("/api/usuarios/{id}", (int id) => UsuarioController.getUserById(id));
app.MapPost("/api/usuarios", (Usuario usuario) => UsuarioController.createUser(usuario));
app.MapDelete("/api/usuarios{id}", (int id) => UsuarioController.deleteUserById(id));

/// <Model>
/// Rutas para el CRUD del modelo MiembroProyecto
/// </Model>
app.MapGet("/api/miembroProyecto/{id}", (int id) => MiembroProyectoController.getMemberById(id));

/// <Model>
/// Rutas para el CRUD del modelo Metodología
/// </Model>

app.Run();