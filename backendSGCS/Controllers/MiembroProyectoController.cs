using backendSGCS.Helpers;
using backendSGCS.Models;
using Microsoft.EntityFrameworkCore;

namespace backendSGCS.Controllers
{
    public class MiembroProyectoController
    {
        static dbSGCSContext context = dbSGCSContext.getContext();

        public static Func<int, IResult> getMemberById = (int id) => {
            var miembroProyecto = context.MiembroProyecto.Include("IdCargoNavigation").Include("IdProyectoNavigation").Include("IdUsuarioNavigation").Where(x => x.IdMiembroProyecto == id).First();
            return miembroProyecto != null ? Results.Ok(miembroProyecto) : Results.NotFound();
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

        public static Func<List<MiembroProyecto>> getMembers = () => context.MiembroProyecto.Include("IdCargoNavigation").Include("IdProyectoNavigation").Include("IdUsuarioNavigation").ToList();

        public static Func<int, IResult> deleteMember = (int id) => {
            var member = context.MiembroProyecto.Find(id);
            if (member == null) {
                return Results.NotFound(MessageHelper.createMessage(false, "No se encontró el miembro de proyecto"));
            }
            context.MiembroProyecto.Remove(member);
            context.SaveChanges();
            return Results.Ok(MessageHelper.createMessage(true, "Miembro proyecto borrado exitosamente"));
        };

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