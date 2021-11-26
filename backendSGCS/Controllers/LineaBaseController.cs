using backendSGCS.Helpers;
using backendSGCS.Models;
using Microsoft.EntityFrameworkCore;

namespace backendSGCS.Controllers {
    public class LineaBaseController {

        public static Func<LineaBase, IResult> createLineaBase = (LineaBase _lineaBase) => {
            try {
                dbSGCSContext context = new dbSGCSContext();
                context.LineaBase.Add(_lineaBase);
                context.SaveChanges();
                return Results.Ok(_lineaBase);
            } catch (Exception e) {
                Console.WriteLine(e);
                return Results.NotFound(MessageHelper.createMessage(false, "Error al crear la linea base"));
            }
        };

        public static Func<List<LineaBase>> getLineaBases = () => {
            dbSGCSContext context = new dbSGCSContext();
            return context.LineaBase.Include(x => x.IdEntregableNavigation.IdFaseMetodologiaNavigation.IdMetodologiaNavigation)
                                    .Include(x => x.IdProyectoNavigation.IdMetodologiaNavigation)
                                    .ToList();
        };

        public static Func<int, IResult> getLineaBaseById = (int _id) => {
            dbSGCSContext context = new dbSGCSContext();
            LineaBase? lineaBase = context.LineaBase
                                    .Include(x=>x.IdEntregableNavigation.IdFaseMetodologiaNavigation.IdMetodologiaNavigation)
                                    .Include(x=>x.IdProyectoNavigation.IdMetodologiaNavigation)                                    
                                    .Where(x => x.IdLineaBase == _id)
                                    .FirstOrDefault();
            if (lineaBase is null) {
                Results.NotFound(MessageHelper.createMessage(false, "No se encontró la linea base"));
            }
            return Results.Ok(lineaBase);
        };

        public static Func<int, IResult> deleteLineaBase = (int id) => {
            dbSGCSContext context = new dbSGCSContext();
            var lineaBase = context.LineaBase.Find(id);
            if (lineaBase == null) {
                return Results.NotFound(MessageHelper.createMessage(false, "No se encontró la linea base"));
            }
            context.LineaBase.Remove(lineaBase);
            context.SaveChanges();
            return Results.Ok(MessageHelper.createMessage(true, "Linea base borrada exitosamente"));
        };

        public static Func<int, LineaBase, Task<IResult>> updateLineaBase = async (int _id, LineaBase _lineaBase) => {
            try {
                dbSGCSContext context = new dbSGCSContext();
                LineaBase? lineaBase = context.LineaBase
                                    .Include(x => x.IdEntregableNavigation.IdFaseMetodologiaNavigation.IdMetodologiaNavigation)
                                    .Include(x => x.IdProyectoNavigation.IdMetodologiaNavigation)
                                    .Where(x => x.IdLineaBase == _id)
                                    .FirstOrDefault();
                if (lineaBase == null) {
                    return Results.NotFound(MessageHelper.createMessage(false, "No se encontró la linea base"));
                }
                context.Entry(lineaBase).CurrentValues.SetValues(_lineaBase);
                await context.SaveChangesAsync();
                return Results.Ok(lineaBase);
            } catch (Exception) {
                return Results.NotFound(MessageHelper.createMessage(false, "Error al intentar actualizar la linea base"));
            }
        };

        public static Func<int, Task<IResult>> getLineasBaseByProjectByUser = async (int _id) => {
            try {
                dbSGCSContext context = new dbSGCSContext();
                var miembroProyectos = context.MiembroProyecto
                                                     .Include(x=>x.IdCargoNavigation)
                                                     .Include(x=>x.IdProyectoNavigation.IdMetodologiaNavigation)
                                                     .Include(x=>x.IdUsuarioNavigation)                                                     
                                                     .Where(x => x.IdUsuario == _id)
                                                     .ToList();
                if (miembroProyectos is null) {
                    return Results.NotFound(MessageHelper.createMessage(false, "No se encontraron proyectos"));
                }
                List<LineaBase> lineasBase = new List<LineaBase>();
                miembroProyectos.ForEach(miembro => {
                    var lineaBase = context.LineaBase.Include(x => x.IdEntregableNavigation.IdFaseMetodologiaNavigation.IdMetodologiaNavigation)
                                                     .Include(x => x.IdProyectoNavigation.IdMetodologiaNavigation)
                                                     .Where(x => x.IdProyecto == miembro.IdProyecto)
                                                     .ToList();
                    if (lineaBase != null) {
                        lineaBase.ForEach(x => {
                            lineasBase.Add(x);
                        });
                    }
                });
                if (lineasBase.Count == 0) {
                    return Results.NotFound(MessageHelper.createMessage(false, "No se encontraron lineas base para los proyectos de este usuario"));
                }
                return Results.Ok(lineasBase);
            } catch (Exception) {

                return Results.BadRequest(MessageHelper.createMessage(false, "Error al iniciar la consulta"));
            }
        };
    }
}