using backendSGCS.Controllers;
using backendSGCS.Helpers;
using backendSGCS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddCors(options => {
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder => {
                          builder.AllowAnyOrigin();
                          builder.AllowAnyMethod();
                          builder.AllowAnyHeader();
                      });
});

builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<dbSGCSContext>(
    options => {
        options.UseSqlServer(builder.Configuration.GetConnectionString("dbSGCS"));

});

var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.MapControllers();

/// <Default>
/// Ruta principal del backend
/// </Default>
app.MapGet("/", () => Results.LocalRedirect("~/swagger/index.html"));

/// < Login>
/// Rutas para Logearse a la aplicación
/// </Login>
app.MapPost("/api/login", ([FromBody] AuthHelper body) => AuthController.login(body));
app.MapPut("/api/usuarios/cambiarClave/{id:int}", ([FromRoute] int id,
                                        [FromBody] string clave) => AuthController.changePassword(id, clave));


/// <Model>
/// Rutas para el CRUD del modelo MiembroProyecto
/// </Model>
app.MapGet("/api/miembros", MiembroProyectoController.getMembers);
app.MapGet("/api/miembros/{id:int}", (int id) => MiembroProyectoController.getMemberById(id));
app.MapGet("/api/miembros/proyecto/{id:int}", (int id) => MiembroProyectoController.getMembersByProject(id));
app.MapGet("/api/miembros/proyectos/usuario/{id:int}", (int id) => MiembroProyectoController.getProyectsByUser(id));
app.MapPost("/api/miembros", (MiembroProyecto _miembroProyecto) => MiembroProyectoController.createMember(_miembroProyecto));
app.MapDelete("/api/miembros/{id:int}", (int id) => MiembroProyectoController.deleteMember(id));
app.MapPut("/api/miembros/{id:int}", async ([FromRoute] int id,
                                        [FromBody] MiembroProyecto _miembroProyecto) => await MiembroProyectoController.updateMember(id, _miembroProyecto));

/// <Model>
/// Rutas para el CRUD del modelo ElementoConfiguracion
/// </Model>
app.MapGet("/api/elementosConfiguracion", ElementoConfiguracionController.getElementoConfiguracions);
app.MapGet("/api/elementosConfiguracion/{id:int}", (int id) => ElementoConfiguracionController.getElementById(id));
app.MapGet("/api/elementosConfiguracion/proyectos/usuario/{id:int}", (int id) => ElementoConfiguracionController.getElementsByProjectByUser(id));
app.MapGet("/api/elementosConfiguracion/proyecto/{id:int}", (int id) => ElementoConfiguracionController.getElementsByProject(id));
app.MapPost("/api/elementosConfiguracion", (ElementoConfiguracion _elementoConfiguracion) => ElementoConfiguracionController.createElementoConfiguracion(_elementoConfiguracion));
app.MapDelete("/api/elementosConfiguracion/{id:int}", (int id) => ElementoConfiguracionController.deleteElementoConfiguracion(id));
app.MapPut("/api/elementosConfiguracion/{id:int}", async ([FromRoute] int id,
                                        [FromBody] ElementoConfiguracion _elementoConfiguracion) => await ElementoConfiguracionController.updateElementoConfiguracion(id, _elementoConfiguracion));

/// <Model>
/// Rutas para el CRUD del modelo Entregable
/// </Model>
app.MapGet("/api/entregables", EntregableController.getEntregables);
app.MapGet("/api/entregables/{id:int}", (int id) => EntregableController.getEntregableById(id));
app.MapGet("/api/entregables/proyecto/{id:int}", (int id) => EntregableController.getEntregablesByProject(id));
app.MapPost("/api/entregables", (Entregable _entregable) => EntregableController.createEntregable(_entregable));
app.MapDelete("/api/entregables/{id:int}", (int id) => EntregableController.deleteEntregable(id));
app.MapPut("/api/entregables/{id:int}", async ([FromRoute] int id,
                                        [FromBody] Entregable _entregable) => await EntregableController.updateEntregable(id, _entregable));

/// <Model>
/// Rutas para el CRUD del modelo LineaBase
/// </Model>
app.MapGet("/api/lineasBase", LineaBaseController.getLineaBases);
app.MapGet("/api/lineasBase/{id:int}", (int id) => LineaBaseController.getLineaBaseById(id));
app.MapGet("/api/lineasBase/proyectos/usuario/{id:int}", (int id) => LineaBaseController.getLineasBaseByProjectByUser(id));
app.MapGet("/api/lineasBase/proyecto/{id:int}", (int id) => LineaBaseController.getLineasBaseByProject(id));
app.MapPost("/api/lineasBase", (LineaBase _lineaBase) => LineaBaseController.createLineaBase(_lineaBase));
app.MapDelete("/api/lineasBase/{id:int}", (int id) => LineaBaseController.deleteLineaBase(id));
app.MapPut("/api/lineasBase/{id:int}", async ([FromRoute] int id,
                                        [FromBody] LineaBase _lineaBase) => await LineaBaseController.updateLineaBase(id, _lineaBase));

/// <Model>
/// Rutas para el CRUD del modelo Metodologia
/// </Model>
app.MapGet("/api/metodologias", MetodologiaController.getMetodologias);
app.MapGet("/api/metodologias/{id:int}", (int id) => MetodologiaController.getMetodologiaById(id));
app.MapPost("/api/metodologias", (Metodologia _metodologia) => MetodologiaController.createMetodologia(_metodologia));
app.MapDelete("/api/metodologias/{id:int}", (int id) => MetodologiaController.deleteMetodologia(id));
app.MapPut("/api/metodologias/{id:int}", async ([FromRoute] int id,
                                        [FromBody] Metodologia _metodologia) => await MetodologiaController.updateMetodologia(id, _metodologia));

/// <Model>
/// Rutas para el CRUD del modelo Proyecto
/// </Model>
app.MapGet("/api/proyectos", ProyectoController.getProjects);
app.MapGet("/api/proyectos/{id:int}", (int id) => ProyectoController.getProjectById(id));
app.MapPost("/api/proyectos", (Proyecto _proyecto) => ProyectoController.createProject(_proyecto));
app.MapDelete("/api/proyectos/{id:int}", (int id) => ProyectoController.deleteProject(id));
app.MapPut("/api/proyectos/{id:int}", async ([FromRoute] int id,
                                        [FromBody] Proyecto _proyecto) => await ProyectoController.updateProject(id, _proyecto));

/// <Model>
/// Rutas para el CRUD del modelo Solicitud
/// </Model>
app.MapGet("/api/solicitudes", SolicitudController.getSolicituds);
app.MapGet("/api/solicitudes/{id:int}", (int id) => SolicitudController.getSolicitudById(id));
app.MapGet("/api/solicitudes/usuario/{id:int}", (int id) => SolicitudController.getSolicitudByUser(id));
app.MapPost("/api/solicitudes", (Solicitud _solicitud) => SolicitudController.createSolicitud(_solicitud));
app.MapDelete("/api/solicitudes/{id:int}", (int id) => SolicitudController.deleteSolicitud(id));
app.MapPut("/api/solicitudes/{id:int}", async ([FromRoute] int id,
                                        [FromBody] Solicitud _solicitud) => await SolicitudController.updateSolicitud(id, _solicitud));

/// <Model>
/// Rutas para los reportes
/// </Model>
app.MapPost("/api/reportes/totales", ([FromBody] Usuario usuario) => ReportController.getAllTotal(usuario));
app.MapGet("/api/reportes/diagrama/proyecto/{id:int}", (int id) => ReportController.getGanttDiagram(id));

app.Run();