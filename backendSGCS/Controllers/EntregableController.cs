using backendSGCS.Helpers;
using backendSGCS.Models;
using Microsoft.EntityFrameworkCore;

namespace backendSGCS.Controllers {
    public class EntregableController {

        public static Func<Entregable, IResult> createEntregable = (Entregable _entregable) => {
            try {
                dbSGCSContext context = new dbSGCSContext();
                context.Entregable.Add(_entregable);
                context.SaveChanges();
                return Results.Ok(_entregable);
            } catch (Exception) {
                return Results.NotFound(MessageHelper.createMessage(false, "Error al crear el entregable"));
            }            
        };

        public static Func<List<Entregable>> getEntregables = () => {
            dbSGCSContext context = new dbSGCSContext();
            return context.Entregable.Include("IdFaseMetodologiaNavigation.IdMetodologiaNavigation").ToList();
        };

        public static Func<int,IResult> getEntregablesByProject = (int _id) => {
            dbSGCSContext context = new dbSGCSContext();
            Proyecto? proyecto = context.Proyecto.Include(x => x.IdMetodologiaNavigation).Where(x => x.IdProyecto == _id).FirstOrDefault();
            if(proyecto is null) {
                return Results.NotFound(MessageHelper.createMessage(false, "No se encontró proyecto"));
            }

            List<Entregable>? entregables = context.Entregable.Include(x=>x.IdFaseMetodologiaNavigation.IdMetodologiaNavigation).Where(x => x.IdFaseMetodologiaNavigation.IdMetodologia== proyecto.IdMetodologia).ToList();
            if (entregables.Count==0) {
                Results.NotFound(MessageHelper.createMessage(false, "No se encontró entregables para ese proyecto"));
            }
            return Results.Ok(entregables);
        };

        public static Func<int, IResult> getEntregableById = (int id) => {
            dbSGCSContext context = new dbSGCSContext();
            Entregable? entregable = context.Entregable.Include("IdFaseMetodologiaNavigation.IdMetodologiaNavigation").Where(x => x.IdEntregable == id).FirstOrDefault();
            if(entregable is null) {
                Results.NotFound(MessageHelper.createMessage(false, "No se encontró el entregable"));
            }
            return Results.Ok(entregable);
        };

        public static Func<int, IResult> deleteEntregable = (int id) => {
            dbSGCSContext context = new dbSGCSContext();
            var entregable = context.Entregable.Find(id);
            if (entregable is null) {
                return Results.NotFound(MessageHelper.createMessage(false, "No se encontró el entregable"));
            }
            context.Entregable.Remove(entregable);
            context.SaveChanges();
            return Results.Ok(MessageHelper.createMessage(true, "Entregable borrado exitosamente"));
        };

        public static Func<int, Entregable, Task<IResult>> updateEntregable = async (int _id, Entregable _entregable) => {            
            try {
                dbSGCSContext context = new dbSGCSContext();
                Entregable? entregable = context.Entregable.Include("IdFaseMetodologiaNavigation.IdMetodologiaNavigation").Where(x => x.IdEntregable == _id).FirstOrDefault();
                if (entregable is null) {
                    return Results.NotFound(MessageHelper.createMessage(false, "No se encontró el entregable"));
                }
                context.Entry(entregable).CurrentValues.SetValues(_entregable);
                await context.SaveChangesAsync();
                return Results.Ok(entregable);
            } catch (Exception) {
                return Results.NotFound(MessageHelper.createMessage(false, "Error al intentar actualizar el entregable"));
            }
        };
    }
}