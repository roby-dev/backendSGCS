using backendSGCS.Helpers;
using backendSGCS.Models;

namespace backendSGCS.Controllers {
    public class MetodologiaController {

        public static Func<List<Metodologia>> getMetodologias = () => {
            dbSGCSContext context = new dbSGCSContext();
            return context.Metodologia.ToList();
        };

        public static Func<int, IResult> getMetodologiaById = (int id) => {
            dbSGCSContext context = new dbSGCSContext();
            var metodologia = context.Metodologia.Find(id);
            return metodologia != null ? Results.Ok(metodologia) : Results.NotFound(MessageHelper.createMessage(false, "No se encontró la metodología"));
        };

        public static Func<Metodologia, IResult> createMetodologia = (Metodologia _metodologia) => {
            try {
                dbSGCSContext context = new dbSGCSContext();
                context.Metodologia.Add(_metodologia);
                var savedMetodologia = context.SaveChanges();
                return savedMetodologia != 0 ? Results.Ok(_metodologia) : Results.NotFound(MessageHelper.createMessage(false, "Error al crear la metodología"));
            } catch (Exception) {
                return Results.BadRequest(MessageHelper.createMessage(false, "Error interno del servidor"));
            }

        };

        public static Func<int, IResult> deleteMetodologia = (int id) => {
            dbSGCSContext context = new dbSGCSContext();
            var metodologia = context.Metodologia.Find(id);
            if (metodologia == null) {
                return Results.NotFound(MessageHelper.createMessage(false, "No se encontró la metodología"));
            }
            context.Metodologia.Remove(metodologia);
            context.SaveChanges();
            return Results.Ok(MessageHelper.createMessage(true, "Metodología borrado exitosamente"));
        };

        public static Func<int, Metodologia, Task<IResult>> updateMetodologia = async (int id, Metodologia metodologia) => {
            dbSGCSContext context = new dbSGCSContext();
            var _metodologia = context.Metodologia.Find(id);
            if (_metodologia == null) {
                return Results.NotFound(MessageHelper.createMessage(false, "No se encontró el metodología"));
            }
            try {
                context.Entry(_metodologia).CurrentValues.SetValues(metodologia);
                await context.SaveChangesAsync();
                return Results.Ok(_metodologia);
            } catch (Exception) {
                return Results.NotFound(MessageHelper.createMessage(false, "Error al intentar actualizar la metodología"));
            }
        };
    }
}