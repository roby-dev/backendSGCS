using backendSGCS.Helpers;
using backendSGCS.Models;
using Microsoft.EntityFrameworkCore;

namespace backendSGCS.Controllers
{
    public class MiembroProyectoController
    {
        static dbSGCSContext context = dbSGCSContext.getContext();

        public static Func<int, IResult> getMemberById = (int id) =>
        {
            var miembroProyecto = context.MiembroProyectos.Include("cargo").Include("proyecto").Include("usuario").Where(x => x.IdMiembroProyecto == id).First();
            return miembroProyecto != null ? Results.Ok(miembroProyecto) : Results.NotFound();
        };
        
        public static Func<MiembroProyecto, IResult> createMember = (MiembroProyecto _member) => {
            try {
                context.MiembroProyectos.Add(_member);
                var savedMember = context.SaveChanges();
                return savedMember != 0 ? Results.Ok(_member) : Results.NotFound(MessageHelper.createMessage(false, "Error al crear cargo"));
            } catch (Exception) {
                return Results.StatusCode(500);
            }            
        };

        public static Func<List<MiembroProyecto>> getMembers = () => context.MiembroProyectos.ToList();     

        public static Func<int, IResult> deleteMember = (int id) =>
        {
            var member = context.MiembroProyectos.Find(id);
            if (member == null)
            {
                return Results.NotFound(MessageHelper.createMessage(false, "No se encontró el miembro de proyecto"));
            }
            context.MiembroProyectos.Remove(member);
            context.SaveChanges();
            return Results.Ok(MessageHelper.createMessage(true, "Miembro proyecto borrado exitosamente"));
        };
    }
}