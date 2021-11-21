using backendSGCS.Helpers;
using backendSGCS.Models;

namespace backendSGCS.Controllers
{
    public class FaseMetodologiaController
    {
        static dbSGCSContext context = new dbSGCSContext();

        public static Func<FaseMetodologia, IResult> createFaseMetodologia = (FaseMetodologia _faseMetodologia) => {
            context.FaseMetodologia.Add(_faseMetodologia);
            var savedFaseMetodologia = context.SaveChanges();
            return savedFaseMetodologia != 0 ? Results.Ok(_faseMetodologia) : Results.NotFound(MessageHelper.createMessage(false, "Error al crear la fase de metdología"));
        };
        public static Func<List<FaseMetodologia>> getFaseMetodologias = () => context.FaseMetodologia.ToList();
        public static Func<int, IResult> getFaseMetodologiaById = (int id) => {
            var faseMetodologia = context.FaseMetodologia.Find(id);
            return faseMetodologia != null ? Results.Ok(faseMetodologia) : Results.NotFound(MessageHelper.createMessage(false, "No se encontró la fase de metdología"));
        };

        public static Func<int, IResult> deleteFaseMetodologia = (int id) => {
            var faseMetodologia = context.FaseMetodologia.Find(id);
            if (faseMetodologia == null)
            {
                return Results.NotFound(MessageHelper.createMessage(false, "No se encontró la fase de metdología"));
            }
            context.FaseMetodologia.Remove(faseMetodologia);
            context.SaveChanges();
            return Results.Ok(MessageHelper.createMessage(true, "Fase de metodología borrada exitosamente"));
        };

        public static Func<int, FaseMetodologia, Task<IResult>> updateFaseMetodologia = async (int id, FaseMetodologia faseMetodologia) => {
            var _faseMetodologia = context.FaseMetodologia.Find(id);
            if (_faseMetodologia == null)
            {
                return Results.NotFound(MessageHelper.createMessage(false, "No se encontró la fase de metodología"));
            }
            try
            {
                context.Entry(_faseMetodologia).CurrentValues.SetValues(faseMetodologia);
                await context.SaveChangesAsync();
                return Results.Ok(_faseMetodologia);
            }
            catch (Exception e)
            {
                return Results.NotFound(MessageHelper.createMessage(false, "Error al intentar actualizar al fase de metodología"));
            }
        };
    }
}
