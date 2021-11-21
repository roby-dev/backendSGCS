using backendSGCS.Helpers;
using backendSGCS.Models;

namespace backendSGCS.Controllers
{
    public class LineaBaseController
    {
        static dbSGCSContext context = new dbSGCSContext();

        public static Func<LineaBase, IResult> createLineaBase = (LineaBase _lineaBase) => {
            context.LineaBase.Add(_lineaBase);
            var savedLineaBase = context.SaveChanges();
            return savedLineaBase != 0 ? Results.Ok(_lineaBase) : Results.NotFound(MessageHelper.createMessage(false, "Error al crear la linea base"));
        };
        public static Func<List<LineaBase>> getLineaBases = () => context.LineaBase.ToList();
        public static Func<int, IResult> getLineaBaseById = (int id) => {
            var lineaBase = context.LineaBase.Find(id);
            return lineaBase != null ? Results.Ok(lineaBase) : Results.NotFound(MessageHelper.createMessage(false, "No se encontró la linea base"));
        };

        public static Func<int, IResult> deleteLineaBase = (int id) => {
            var lineaBase = context.LineaBase.Find(id);
            if (lineaBase == null)
            {
                return Results.NotFound(MessageHelper.createMessage(false, "No se encontró la linea base"));
            }
            context.LineaBase.Remove(lineaBase);
            context.SaveChanges();
            return Results.Ok(MessageHelper.createMessage(true, "Linea base borrada exitosamente"));
        };

        public static Func<int, LineaBase, Task<IResult>> updateLineaBase = async (int id, LineaBase lineaBase) => {
            var _lineaBase = context.LineaBase.Find(id);
            if (_lineaBase == null)
            {
                return Results.NotFound(MessageHelper.createMessage(false, "No se encontró la linea base"));
            }
            try
            {
                context.Entry(_lineaBase).CurrentValues.SetValues(lineaBase);
                await context.SaveChangesAsync();
                return Results.Ok(_lineaBase);
            }
            catch (Exception e)
            {
                return Results.NotFound(MessageHelper.createMessage(false, "Error al intentar actualizar la linea base"));
            }
        };

    }
}
