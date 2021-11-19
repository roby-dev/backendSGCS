using backendSGCS.Controllers;
using backendSGCS.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContextFactory<dbSGCSContext>();
builder.Services.AddTransient<UsuarioController>();
builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

/// <Default>
/// Ruta principal del backend
/// </Default>
app.MapGet("/", () => Results.LocalRedirect("~/swagger/index.html"));

/// < Model >
/// Rutas para el CRUD del modelo Usuario
/// </Model>
app.MapGet("/api/usuarios", UsuarioController.getUsers);
app.MapGet("/api/usuarios/{id}", (int id) => UsuarioController.getUserById(id));
app.MapPost("/api/usuarios", (Usuario usuario) => UsuarioController.createUser(usuario));
app.MapDelete("/api/usuarios/{id}", (int id) => UsuarioController.deleteUser(id));
app.MapPut("/api/usuarios/{id}", async ([FromRoute] int id,
                                        [FromBody] Usuario _usuario) => await UsuarioController.updateUser(id, _usuario));

app.MapPut("/api/usuarios/cambiarClave/{id}", async ([FromRoute] int id,
                                        [FromBody] Usuario _usuario) => await UsuarioController.changePassword(id, _usuario));

/// <Model>
/// Rutas para el CRUD del modelo MiembroProyecto
/// </Model>
app.MapGet("/api/miembros", MiembroProyectoController.getMembers);
app.MapGet("/api/miembros/{id}", (int id) => MiembroProyectoController.getMemberById(id));
app.MapPost("/api/miembros", (MiembroProyecto _miembro) => MiembroProyectoController.createMember(_miembro));
app.MapDelete("/api/miembros/{id}", (int id) => MiembroProyectoController.deleteMember(id));
app.MapPut("/api/miembros/{id}", async ([FromRoute] int id,
                                        [FromBody] MiembroProyecto _miembro) => await MiembroProyectoController.updateMember(id, _miembro));

/// <Model>
/// Rutas para el CRUD del modelo Cargo
/// </Model>
app.MapGet("/api/cargos", CargoController.getCargos);
app.MapGet("/api/cargos/{id}", (int id) => CargoController.getCargoById(id));
app.MapPost("/api/cargos", (Cargo usuario) => CargoController.createCargo(usuario));
app.MapDelete("/api/cargos/{id}", (int id) => CargoController.deleteCargo(id));
app.MapPut("/api/cargos/{id}", async ([FromRoute] int id,
                                        [FromBody] Cargo _cargo) => await CargoController.updateCargo(id, _cargo));

/// <Model>
/// Rutas para el CRUD del modelo Metodologia
/// </Model>
app.MapGet("/api/metodologias", MetodologiaController.getMetodologias);
app.MapGet("/api/metodologias/{id}", (int id) => MetodologiaController.getMetodologiaById(id));
app.MapPost("/api/metodologias", (Metodologia metodologia) => MetodologiaController.createMetodologia(metodologia));
app.MapDelete("/api/metodologias/{id}", (int id) => MetodologiaController.deleteMetodologia(id));
app.MapPut("/api/metodologias/{id}", async ([FromRoute] int id,
                                        [FromBody] Metodologia _metodologia) => await MetodologiaController.updateMetodologia(id, _metodologia));

/// <Model>
/// Rutas para el CRUD del modelo Proyecto
/// </Model>
app.MapGet("/api/proyectos", ProyectoController.getProjects);
app.MapGet("/api/proyectos/{id}", (int id) => ProyectoController.getProjectById(id));
app.MapPost("/api/proyectos", (Proyecto _proyecto) => ProyectoController.createProject(_proyecto));
app.MapDelete("/api/proyectos/{id}", (int id) => ProyectoController.deleteProject(id));
app.MapPut("/api/proyectos/{id}", async ([FromRoute] int id,
                                        [FromBody] Proyecto _proyecto) => await ProyectoController.updateProject(id, _proyecto));

app.Run();