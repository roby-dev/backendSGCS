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
//app.MapPut("/api/usuarios/{id}",( [FromRoute] int id,
//    [FromBody]
                                            
//    ))
app.MapDelete("/api/usuarios/{id}", (int id) => UsuarioController.deleteUser(id));

/// <Model>
/// Rutas para el CRUD del modelo MiembroProyecto
/// </Model>
app.MapGet("/api/miembros", MiembroProyectoController.getMembers);
app.MapGet("/api/miembros/{id}", (int id) => MiembroProyectoController.getMemberById(id));
app.MapPost("/api/miembros", (MiembroProyecto _miembro) => MiembroProyectoController.createMember(_miembro));
app.MapDelete("/api/miembros/{id}", (int id) => MiembroProyectoController.deleteMember(id));

/// <Model>
/// Rutas para el CRUD del modelo Cargo
/// </Model>
app.MapGet("/api/cargos", CargoController.getCargos);
app.MapGet("/api/cargos/{id}", (int id) => CargoController.getCargoById(id));
app.MapPost("/api/cargos", (Cargo usuario) => CargoController.createCargo(usuario));
app.MapDelete("/api/cargos/{id}", (int id) => CargoController.deleteCargo(id));

/// <Model>
/// Rutas para el CRUD del modelo Metodologia
/// </Model>
app.MapGet("/api/metodologias", MetodologiaController.getMetodologias);
app.MapGet("/api/metodologias/{id}", (int id) => MetodologiaController.getMetodologiaById(id));
app.MapPost("/api/metodologias", (Metodologium metodologia) => MetodologiaController.createMetodologia(metodologia));
app.MapDelete("/api/metodologias/{id}", (int id) => MetodologiaController.deleteMetodologia(id));

app.Run();