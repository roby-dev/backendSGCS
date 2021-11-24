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