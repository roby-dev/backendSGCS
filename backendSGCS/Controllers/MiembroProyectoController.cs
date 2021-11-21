using backendSGCS.Helpers;
using backendSGCS.Models;
using Microsoft.EntityFrameworkCore;

namespace backendSGCS.Controllers
{
    public class MiembroProyectoController
    {
        static dbSGCSContext context = new dbSGCSContext();

        public static Func<int, IResult> getMemberById = (int id) => {
            try {
                var miembroProyecto = context.MiembroProyecto.Where(x => x.IdMiembroProyecto == id).First();
                return Results.Ok(miembroProyecto);
            } catch (Exception) {
                return Results.NotFound(MessageHelper.createMessage(false, "No se encontró el miembro del proyecto"));
            }
        };

        public static Func<MiembroProyecto, IResult> createMember = (MiembroProyecto _member) => {
            try {
                context.MiembroProyecto.Add(_member);
                var savedMember = context.SaveChanges();
                return savedMember != 0 ? Results.Ok(_member) : Results.NotFound(MessageHelper.createMessage(false, "Error al crear el miembro del proyecto"));
            } catch (Exception) {
                return Results.NotFound(MessageHelper.createMessage(false, "Error interno del servidor"));
            }
        };

        public static Func<List<MiembroProyecto>> getMembers = () => context.MiembroProyecto.ToList();

        public static Func<int, IResult> deleteMember = (int id) => {
            var member = context.MiembroProyecto.Find(id);
            if (member == null) {
                return Results.NotFound(MessageHelper.createMessage(false, "No se encontró el miembro del proyecto"));
            }
            context.MiembroProyecto.Remove(member);
            context.SaveChanges();
            return Results.Ok(MessageHelper.createMessage(true, "Miembro del proyecto borrado exitosamente"));
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
                return Results.NotFound(MessageHelper.createMessage(false, "No se encontró el miembro del proyecto"));
            }
            try {
                context.Entry(_miembro).CurrentValues.SetValues(miembro);
                await context.SaveChangesAsync();
                return Results.Ok(_miembro);
            } catch (Exception e) {
                return Results.NotFound(MessageHelper.createMessage(false, "Error al intentar actualizar el miembro del proyecto"));
            }
        };
    }
}