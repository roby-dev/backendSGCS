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
            return context.LineaBase.Include("IdEntregableNavigation.IdFaseMetodologiaNavigation")
                                    .Include("IdProyectoNavigation.IdMetodologiaNavigation")
                                    .ToList();
        };

        public static Func<int, IResult> getLineaBaseById = (int _id) => {
            dbSGCSContext context = new dbSGCSContext();
            LineaBase? lineaBase = context.LineaBase
                                    .Include("IdEntregableNavigation.IdFaseMetodologiaNavigation")
                                    .Include("IdProyectoNavigation.IdMetodologiaNavigation")
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
                                    .Include("IdEntregableNavigation.IdFaseMetodologiaNavigation")
                                    .Include("IdProyectoNavigation.IdMetodologiaNavigation")
                                    .Where(x => x.IdLineaBase == _id)
                                    .FirstOrDefault();
                if (lineaBase == null) {
                    return Results.NotFound(MessageHelper.createMessage(false, "No se encontró la linea base"));
                }
                context.Entry(lineaBase).CurrentValues.SetValues(_lineaBase);
                await context.SaveChangesAsync();
                return Results.Ok(lineaBase);
            } catch (Exception e) {
                return Results.NotFound(MessageHelper.createMessage(false, "Error al intentar actualizar la linea base"));
            }
        };
    }
}