using backendSGCS.Helpers;
using backendSGCS.Models;
using Microsoft.EntityFrameworkCore;

namespace backendSGCS.Controllers {
    public class SolicitudController {

        public static Func<Solicitud, IResult> createSolicitud = (Solicitud _solicitud) => {
            try {
                dbSGCSContext context = new dbSGCSContext();
                context.Solicitud.Add(_solicitud);
                context.SaveChanges();
                return Results.Ok(_solicitud);
            } catch (Exception) {
                return Results.NotFound(MessageHelper.createMessage(false, "Error al crear la solicitud"));
            }            
        };

        public static Func<List<Solicitud>> getSolicituds = () => {
            dbSGCSContext context = new dbSGCSContext();            

            return context.Solicitud.Include("IdElementoConfiguracionNavigation.IdLineaBaseNavigation.IdProyectoNavigation.IdMetodologiaNavigation")
                                    .Include("IdElementoConfiguracionNavigation.IdLineaBaseNavigation.IdEntregableNavigation.IdFaseMetodologiaNavigation.IdMetodologiaNavigation")
                                    .Include("IdMiembroProyectoNavigation.IdCargoNavigation")
                                    .Include("IdMiembroProyectoNavigation.IdProyectoNavigation")
                                    .Include("IdMiembroProyectoNavigation.IdUsuarioNavigation")
                                    .ToList();
        };

        public static Func<int, IResult> getSolicitudById = (int _id) => {
            dbSGCSContext context = new dbSGCSContext();
            var solicitud = context.Solicitud.Include("IdElementoConfiguracionNavigation.IdLineaBaseNavigation.IdProyectoNavigation.IdMetodologiaNavigation")
                                    .Include("IdElementoConfiguracionNavigation.IdLineaBaseNavigation.IdEntregableNavigation.IdFaseMetodologiaNavigation.IdMetodologiaNavigation")
                                    .Include("IdMiembroProyectoNavigation.IdCargoNavigation")
                                    .Include("IdMiembroProyectoNavigation.IdProyectoNavigation")
                                    .Include("IdMiembroProyectoNavigation.IdUsuarioNavigation")
                                    .Where(x=>x.IdSolicitud==_id)
                                    .FirstOrDefault();
            if (solicitud is null) {
                Results.NotFound(MessageHelper.createMessage(false, "No se encontró la solicitud"));
            }
            return Results.Ok(solicitud);
        };

        public static Func<int, IResult> getSolicitudByUser = (int _id) => {
            dbSGCSContext context = new dbSGCSContext();
            var solicitudes = context.Solicitud
                                    .Include(x => x.IdElementoConfiguracionNavigation.IdLineaBaseNavigation.IdProyectoNavigation.IdMetodologiaNavigation)
                                    .Include(x => x.IdElementoConfiguracionNavigation.IdLineaBaseNavigation.IdEntregableNavigation.IdFaseMetodologiaNavigation.IdMetodologiaNavigation)
                                    .Include(x => x.IdMiembroProyectoNavigation.IdCargoNavigation)
                                    .Include(x => x.IdMiembroProyectoNavigation.IdProyectoNavigation)
                                    .Include(x=>x.IdMiembroProyectoNavigation.IdUsuarioNavigation)                                                                        
                                    .Where(x => x.IdMiembroProyectoNavigation.IdUsuario == _id)
                                    .ToList();
            if (solicitudes.Count == 0 ) {
                return Results.NotFound(MessageHelper.createMessage(false, "No se encontraron solicituds para ese usuario"));
            }
            return Results.Ok(solicitudes);
        };

        public static Func<int, IResult> getSolicitudByProjectsByUser = (int _id) => {
            dbSGCSContext context = new dbSGCSContext();

            var miembroProyecto = context.MiembroProyecto.Include(X => X.IdProyectoNavigation).Include(X => X.IdUsuarioNavigation).Where(x => x.IdUsuario == _id).ToList();
            List<Solicitud> totalSolicitudes = new List<Solicitud>();

            miembroProyecto.ForEach(miembro => {
                var solicitudes = context.Solicitud
                                  .Include(x => x.IdElementoConfiguracionNavigation.IdLineaBaseNavigation.IdProyectoNavigation.IdMetodologiaNavigation)
                                  .Include(x => x.IdElementoConfiguracionNavigation.IdLineaBaseNavigation.IdEntregableNavigation.IdFaseMetodologiaNavigation.IdMetodologiaNavigation)
                                  .Include(x => x.IdMiembroProyectoNavigation.IdCargoNavigation)
                                  .Include(x => x.IdMiembroProyectoNavigation.IdProyectoNavigation)
                                  .Include(x => x.IdMiembroProyectoNavigation.IdUsuarioNavigation)
                                  .Where(x => x.IdMiembroProyectoNavigation.IdProyecto == miembro.IdProyecto)
                                  .ToList();
                solicitudes.ForEach(solicitud => {
                    totalSolicitudes.Add(solicitud);
                });
            });

            totalSolicitudes = totalSolicitudes.Distinct().ToList();

            if (totalSolicitudes.Count == 0) {
                return Results.NotFound(MessageHelper.createMessage(false, "No se encontraron solicituds para ese usuario"));
            }
            return Results.Ok(totalSolicitudes);
        };

        public static Func<int, IResult> deleteSolicitud = (int _id) => {
            dbSGCSContext context = new dbSGCSContext();
            var solicitud = context.Solicitud.Find(_id);
            if (solicitud is null) {
                return Results.NotFound(MessageHelper.createMessage(false, "No se encontró la solicitud"));
            }
            context.Solicitud.Remove(solicitud);
            context.SaveChanges();
            return Results.Ok(MessageHelper.createMessage(true, "Solicitud borrada exitosamente"));
        };

        public static Func<int, Solicitud, Task<IResult>> updateSolicitud = async (int _id, Solicitud _solicitud) => {        
            try {
                dbSGCSContext context = new dbSGCSContext();
                var solicitud = context.Solicitud.Include("IdElementoConfiguracionNavigation.IdLineaBaseNavigation.IdProyectoNavigation.IdMetodologiaNavigation")
                                  .Include("IdElementoConfiguracionNavigation.IdLineaBaseNavigation.IdEntregableNavigation.IdFaseMetodologiaNavigation.IdMetodologiaNavigation")
                                  .Include("IdMiembroProyectoNavigation.IdCargoNavigation")
                                  .Include("IdMiembroProyectoNavigation.IdProyectoNavigation")
                                  .Include("IdMiembroProyectoNavigation.IdUsuarioNavigation")
                                  .Where(x => x.IdSolicitud == _id)
                                  .FirstOrDefault();
                if (solicitud is null) {
                    return Results.NotFound(MessageHelper.createMessage(false, "No se encontró la solicitud"));
                }
                context.Entry(solicitud).CurrentValues.SetValues(_solicitud);
                await context.SaveChangesAsync();
                return Results.Ok(solicitud);
            } catch (Exception) {
                return Results.NotFound(MessageHelper.createMessage(false, "Error al intentar actualizar la solicitud"));
            }
        };
    }
}