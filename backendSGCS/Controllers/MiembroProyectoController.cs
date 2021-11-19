using backendSGCS.Helpers;
using backendSGCS.Models;
using Microsoft.EntityFrameworkCore;

namespace backendSGCS.Controllers
{
    public class MiembroProyectoController
    {
        static dbSGCSContext context = dbSGCSContext.getContext();

        public static Func<int, IResult> getMemberById = (int id) => {
            try {
                var miembroProyecto = context.MiembroProyecto.Where(x => x.IdMiembroProyecto == id).First();
                return Results.Ok(miembroProyecto);
            } catch (Exception) {
                return Results.NotFound(MessageHelper.createMessage(false,"No se encontró proyecto"));
            }            
        };

        public static Func<MiembroProyecto, IResult> createMember = (MiembroProyecto _member) => {
            try {
                context.MiembroProyecto.Add(_member);
                var savedMember = context.SaveChanges();
                return savedMember != 0 ? Results.Ok(_member) : Results.NotFound(MessageHelper.createMessage(false, "Error al crear cargo"));
            } catch (Exception) {
                return Results.StatusCode(500);
            }
        };

        public static Func<List<MiembroProyecto>> getMembers = () => context.MiembroProyecto.ToList();

        public static Func<int, IResult> deleteMember = (int id) => {
            var member = context.MiembroProyecto.Find(id);
            if (member == null) {
                return Results.NotFound(MessageHelper.createMessage(false, "No se encontró el miembro de proyecto"));
            }
            context.MiembroProyecto.Remove(member);
            context.SaveChanges();
            return Results.Ok(MessageHelper.createMessage(true, "Miembro proyecto borrado exitosamente"));
        };

        internal static object getMembersByProject(int id) {
            try {
                var miembrosProyecto = context.MiembroProyecto.Where(x => x.IdProyecto == id);
                return Results.Ok(miembrosProyecto);
            } catch (Exception) {
                return Results.NotFound(MessageHelper.createMessage(false, "No se encontraron miembros para este proyecto"));
            }
        }

        public static Func<int, MiembroProyecto, Task<IResult>> updateMember = async (int id, MiembroProyecto miembro) => {
            var _miembro = context.MiembroProyecto.Find(id);
            if (_miembro == null) {
                return Results.NotFound(MessageHelper.createMessage(false, "No se encontró el miembro"));
            }
            try {
                context.Entry(_miembro).CurrentValues.SetValues(miembro);
                await context.SaveChangesAsync();
                return Results.Ok(_miembro);
            } catch (Exception e) {
                return Results.NotFound(e);
            }
        };
    }
}