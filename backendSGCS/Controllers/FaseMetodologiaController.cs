using backendSGCS.Helpers;
using backendSGCS.Models;
using Microsoft.EntityFrameworkCore;

namespace backendSGCS.Controllers {
    public class FaseMetodologiaController {

        public static Func<FaseMetodologia, IResult> createFaseMetodologia = (FaseMetodologia _faseMetodologia) => {
            try {
                dbSGCSContext context = new dbSGCSContext();
                context.FaseMetodologia.Add(_faseMetodologia);
                context.SaveChanges();
                return Results.Ok(_faseMetodologia);
            } catch (Exception) {
                return Results.NotFound(MessageHelper.createMessage(false, "Error al crear la fase de metdología"));
            }            
        };

        public static Func<List<FaseMetodologia>> getFaseMetodologias = () => {
            dbSGCSContext context = new dbSGCSContext();
            return context.FaseMetodologia.Include("IdMetodologiaNavigation").ToList();
        };

        public static Func<int, IResult> getFaseMetodologiaById = (int id) => {
            dbSGCSContext context = new dbSGCSContext();
            var faseMetodologia = context.FaseMetodologia.Include("IdMetodologiaNavigation").Where(x => x.IdFaseMetodologia == id).FirstOrDefault();
            if(faseMetodologia is null) {
                Results.NotFound(MessageHelper.createMessage(false, "No se encontró la fase de metdología"));
            }
            return Results.Ok(faseMetodologia);
        };

        public static Func<int, IResult> deleteFaseMetodologia = (int id) => {
            dbSGCSContext context = new dbSGCSContext();
            var faseMetodologia = context.FaseMetodologia.Find(id);
            if (faseMetodologia == null) {
                return Results.NotFound(MessageHelper.createMessage(false, "No se encontró la fase de metdología"));
            }
            context.FaseMetodologia.Remove(faseMetodologia);
            context.SaveChanges();
            return Results.Ok(MessageHelper.createMessage(true, "Fase de metodología borrada exitosamente"));
        };

        public static Func<int, FaseMetodologia, Task<IResult>> updateFaseMetodologia = async (int _id, FaseMetodologia _faseMetodologia) => {            
            try {
                dbSGCSContext context = new dbSGCSContext();
                var faseMetodologia = context.FaseMetodologia.Include("IdMetodologiaNavigation").Where(x => x.IdFaseMetodologia == _id).FirstOrDefault();
                if (faseMetodologia == null) {
                    return Results.NotFound(MessageHelper.createMessage(false, "No se encontró la fase de metodología"));
                }
                context.Entry(faseMetodologia).CurrentValues.SetValues(_faseMetodologia);
                await context.SaveChangesAsync();
                return Results.Ok(faseMetodologia);
            } catch (Exception) {
                return Results.NotFound(MessageHelper.createMessage(false, "Error al intentar actualizar al fase de metodología"));
            }
        };
    }
}