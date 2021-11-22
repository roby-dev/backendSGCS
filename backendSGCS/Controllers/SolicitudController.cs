using backendSGCS.Helpers;
using backendSGCS.Models;

namespace backendSGCS.Controllers {
    public class SolicitudController {

        public static Func<Solicitud, IResult> createSolicitud = (Solicitud _solicitud) => {
            dbSGCSContext context = new dbSGCSContext();
            context.Solicitud.Add(_solicitud);
            var savedSolicitud = context.SaveChanges();
            return savedSolicitud != 0 ? Results.Ok(_solicitud) : Results.NotFound(MessageHelper.createMessage(false, "Error al crear la solicitud"));
        };
        public static Func<List<Solicitud>> getSolicituds = () => {
            dbSGCSContext context = new dbSGCSContext();
            return context.Solicitud.ToList();
        };
        public static Func<int, IResult> getSolicitudById = (int id) => {
            dbSGCSContext context = new dbSGCSContext();
            var solicitud = context.Solicitud.Find(id);
            return solicitud != null ? Results.Ok(solicitud) : Results.NotFound(MessageHelper.createMessage(false, "No se encontró la solicitud"));
        };
        public static Func<int, IResult> deleteSolicitud = (int id) => {
            dbSGCSContext context = new dbSGCSContext();
            var solicitud = context.Solicitud.Find(id);
            if (solicitud == null) {
                return Results.NotFound(MessageHelper.createMessage(false, "No se encontró la solicitud"));
            }
            context.Solicitud.Remove(solicitud);
            context.SaveChanges();
            return Results.Ok(MessageHelper.createMessage(true, "Solicitud borrada exitosamente"));
        };
        public static Func<int, Solicitud, Task<IResult>> updateSolicitud = async (int id, Solicitud solicitud) => {
            dbSGCSContext context = new dbSGCSContext();
            var _solicitud = context.Solicitud.Find(id);
            if (_solicitud == null) {
                return Results.NotFound(MessageHelper.createMessage(false, "No se encontró la solicitud"));
            }
            try {
                context.Entry(_solicitud).CurrentValues.SetValues(solicitud);
                await context.SaveChangesAsync();
                return Results.Ok(_solicitud);
            } catch (Exception e) {
                return Results.NotFound(MessageHelper.createMessage(false, "Error al intentar actualizar la solicitud"));
            }
        };
    }
}