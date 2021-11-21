using backendSGCS.Controllers;
using backendSGCS.Helpers;
using backendSGCS.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
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

/// < Login>
/// Rutas para Logearse a la aplicación
/// </Login>
app.MapPost("/api/login", ([FromBody] AuthHelper body) => AuthController.login(body));

/// < Model >
/// Rutas para el CRUD del modelo Usuario
/// </Model>
app.MapGet("/api/usuarios", UsuarioController.getUsers);
app.MapGet("/api/usuarios/{id}", (int id) => UsuarioController.getUserById(id));
app.MapPost("/api/usuarios", (Usuario _usuario) => UsuarioController.createUser(_usuario));
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
app.MapGet("/api/miembros/proyecto/{id}", (int id) => MiembroProyectoController.getMembersByProject(id));
app.MapPost("/api/miembros", (MiembroProyecto _miembroProyecto) => MiembroProyectoController.createMember(_miembroProyecto));
app.MapDelete("/api/miembros/{id}", (int id) => MiembroProyectoController.deleteMember(id));
app.MapPut("/api/miembros/{id}", async ([FromRoute] int id,
                                        [FromBody] MiembroProyecto _miembroProyecto) => await MiembroProyectoController.updateMember(id, _miembroProyecto));

/// <Model>
/// Rutas para el CRUD del modelo Cargo
/// </Model>
app.MapGet("/api/cargos", CargoController.getCargos);
app.MapGet("/api/cargos/{id}", (int id) => CargoController.getCargoById(id));
app.MapPost("/api/cargos", (Cargo _cargo) => CargoController.createCargo(_cargo));
app.MapDelete("/api/cargos/{id}", (int id) => CargoController.deleteCargo(id));
app.MapPut("/api/cargos/{id}", async ([FromRoute] int id,
                                        [FromBody] Cargo _cargo) => await CargoController.updateCargo(id, _cargo));


/// <Model>
/// Rutas para el CRUD del modelo ElementoConfiguracion
/// </Model>
app.MapGet("/api/elementosConfiguracion", ElementoConfiguracionController.getElementoConfiguracions);
app.MapGet("/api/elementosConfiguracion/{id}", (int id) => ElementoConfiguracionController.getElementoConfiguracionById(id));
app.MapPost("/api/elementosConfiguracion", (ElementoConfiguracion _elementoConfiguracion) => ElementoConfiguracionController.createElementoConfiguracion(_elementoConfiguracion));
app.MapDelete("/api/elementosConfiguracion/{id}", (int id) => ElementoConfiguracionController.deleteElementoConfiguracion(id));
app.MapPut("/api/elementosConfiguracion/{id}", async ([FromRoute] int id,
                                        [FromBody] ElementoConfiguracion _elementoConfiguracion) => await ElementoConfiguracionController.updateElementoConfiguracion(id, _elementoConfiguracion));



/// <Model>
/// Rutas para el CRUD del modelo Entregable
/// </Model>
app.MapGet("/api/entregables", EntregableController.getEntregables);
app.MapGet("/api/entregables/{id}", (int id) => EntregableController.getEntregableById(id));
app.MapPost("/api/entregables", (Entregable _entregable) => EntregableController.createEntregable(_entregable));
app.MapDelete("/api/entregables/{id}", (int id) => EntregableController.deleteEntregable(id));
app.MapPut("/api/entregables/{id}", async ([FromRoute] int id,
                                        [FromBody] Entregable _entregable) => await EntregableController.updateEntregable(id, _entregable));



/// <Model>
/// Rutas para el CRUD del modelo FaseMetodologia
/// </Model>
app.MapGet("/api/fasesMetodologia", FaseMetodologiaController.getFaseMetodologias);
app.MapGet("/api/fasesMetodologia/{id}", (int id) => FaseMetodologiaController.getFaseMetodologiaById(id));
app.MapPost("/api/fasesMetodologia", (FaseMetodologia _faseMetodologia) => FaseMetodologiaController.createFaseMetodologia(_faseMetodologia));
app.MapDelete("/api/fasesMetodologia/{id}", (int id) => FaseMetodologiaController.deleteFaseMetodologia(id));
app.MapPut("/api/fasesMetodologia/{id}", async ([FromRoute] int id,
                                        [FromBody] FaseMetodologia _faseMetodologia) => await FaseMetodologiaController.updateFaseMetodologia(id, _faseMetodologia));





/// <Model>
/// Rutas para el CRUD del modelo LineaBase
/// </Model>
app.MapGet("/api/lineasBase", LineaBaseController.getLineaBases);
app.MapGet("/api/lineasBase/{id}", (int id) => LineaBaseController.getLineaBaseById(id));
app.MapPost("/api/lineasBase", (LineaBase _lineaBase) => LineaBaseController.createLineaBase(_lineaBase));
app.MapDelete("/api/lineasBase/{id}", (int id) => LineaBaseController.deleteLineaBase(id));
app.MapPut("/api/lineasBase/{id}", async ([FromRoute] int id,
                                        [FromBody] LineaBase _lineaBase) => await LineaBaseController.updateLineaBase(id, _lineaBase));


/// <Model>
/// Rutas para el CRUD del modelo Metodologia
/// </Model>
app.MapGet("/api/metodologias", MetodologiaController.getMetodologias);
app.MapGet("/api/metodologias/{id}", (int id) => MetodologiaController.getMetodologiaById(id));
app.MapPost("/api/metodologias", (Metodologia _metodologia) => MetodologiaController.createMetodologia(_metodologia));
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



/// <Model>
/// Rutas para el CRUD del modelo Solicitud
/// </Model>
app.MapGet("/api/solicitudes", SolicitudController.getSolicituds);
app.MapGet("/api/solicitudes/{id}", (int id) => SolicitudController.getSolicitudById(id));
app.MapPost("/api/solicitudes", (Solicitud _solicitud) => SolicitudController.createSolicitud(_solicitud));
app.MapDelete("/api/solicitudes/{id}", (int id) => SolicitudController.deleteSolicitud(id));
app.MapPut("/api/solicitudes/{id}", async ([FromRoute] int id,
                                        [FromBody] Solicitud _solicitud) => await SolicitudController.updateSolicitud(id, _solicitud));



/// <Model>
/// Rutas para los reportes
/// </Model>
app.MapGet("/api/reportes/totales", ReportController.getAllTotal);

app.Run();