using backendSGCS.Helpers;
using backendSGCS.Models;
using Microsoft.EntityFrameworkCore;

namespace backendSGCS.Controllers
{
    public class MetodologiaController
    {
        static dbSGCSContext context = dbSGCSContext.getContext();

        public static Func<List<Metodologium>> getMetodologias = () => context.Metodologia.ToList();
        public static Func<int, IResult> getMetodologiaById = (int id) =>
        {
            var metodologia= context.Metodologia.Find(id);
            return metodologia != null ? Results.Ok(metodologia) : Results.NotFound(MessageHelper.createMessage(false, "No se encontró la metodología"));
        };

        public static Func<Metodologium, IResult> createMetodologia = (Metodologium _metodologia) =>
        {
            try {
                context.Metodologia.Add(_metodologia);
                var savedMetodologia = context.SaveChanges();
                return savedMetodologia != 0 ? Results.Ok(savedMetodologia) : Results.NotFound(MessageHelper.createMessage(false, "Error al crear usuario"));
            } catch (Exception) {
                return Results.StatusCode(500);
                throw;
            }
            
        };
        public static Func<int, IResult> deleteMetodologia = (int id) =>
        {
            var metodologia = context.Metodologia.Find(id);
            if (metodologia == null)
            {
                return Results.NotFound(MessageHelper.createMessage(false, "No se encontró la metodología"));
            }
            context.Metodologia.Remove(metodologia);
            context.SaveChanges();
            return Results.Ok(MessageHelper.createMessage(true, "Metodología borrado exitosamente"));
        };
    }
}