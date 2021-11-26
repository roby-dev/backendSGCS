using backendSGCS.Helpers;
using backendSGCS.Models;
using Microsoft.EntityFrameworkCore;

namespace backendSGCS.Controllers {
    public class MiembroProyectoController {

        public static Func<int, IResult> getMemberById = (int id) => {
            dbSGCSContext context = new dbSGCSContext();
            var miembroProyecto = context.MiembroProyecto.Where(x => x.IdMiembroProyecto == id)
                                                         .Include("IdCargoNavigation")
                                                         .Include("IdProyectoNavigation.IdMetodologiaNavigation")
                                                         .Include("IdUsuarioNavigation")
                                                         .FirstOrDefault();
            if (miembroProyecto is null) {
                Results.NotFound(MessageHelper.createMessage(false, "No se encontró el miembro del proyecto"));
            }
            return Results.Ok(miembroProyecto);
        };

        public static Func<MiembroProyecto, IResult> createMember = (MiembroProyecto _member) => {
            dbSGCSContext context = new dbSGCSContext();
            try {
                context.MiembroProyecto.Add(_member);
                var savedMember = context.SaveChanges();
                return savedMember != 0 ? Results.Ok(_member) : Results.NotFound(MessageHelper.createMessage(false, "Error al crear el miembro del proyecto"));
            } catch (Exception) {
                return Results.NotFound(MessageHelper.createMessage(false, "Error interno del servidor"));
            }
        };

        public static Func<List<MiembroProyecto>> getMembers = () => {
            dbSGCSContext context = new dbSGCSContext();
            return context.MiembroProyecto.Include("IdCargoNavigation")
                                          .Include("IdProyectoNavigation.IdMetodologiaNavigation")
                                          .Include("IdUsuarioNavigation")
                                          .ToList();
        };

        public static Func<int, IResult> deleteMember = (int _id) => {
            dbSGCSContext context = new dbSGCSContext();
            var member = context.MiembroProyecto.Find(_id);
            if (member is null) {
                return Results.NotFound(MessageHelper.createMessage(false, "No se encontró el miembro del proyecto"));
            }
            context.MiembroProyecto.Remove(member);
            context.SaveChanges();
            return Results.Ok(MessageHelper.createMessage(true, "Miembro del proyecto borrado exitosamente"));
        };

        public static Func<int, IResult> getMembersByProject = (int _id) => {           
            try {
                dbSGCSContext context = new dbSGCSContext();
                var miembroProyecto = context.MiembroProyecto.Where(x => x.IdProyecto == _id)
                                                        .Include("IdCargoNavigation")
                                                        .Include("IdProyectoNavigation.IdMetodologiaNavigation")
                                                        .Include("IdUsuarioNavigation")
                                                        .ToList();
                if(miembroProyecto.Count==0) {
                    return Results.NotFound(MessageHelper.createMessage(false, "No se encontro miembros para ese proyecto"));
                }
                return Results.Ok(miembroProyecto);
            } catch (Exception) {
                return Results.NotFound(MessageHelper.createMessage(false, "Error con la base de datos"));
            }
        };

        public static Func<int, IResult> getProyectsByUser = (int _id) => {
            try {
                dbSGCSContext context = new dbSGCSContext();
                var projects = context.MiembroProyecto.Include("IdCargoNavigation")
                                                      .Include("IdProyectoNavigation.IdMetodologiaNavigation")
                                                      .Include("IdUsuarioNavigation")
                                                      .Where(x => x.IdUsuario == _id)
                                                      .Select(x => x.IdProyectoNavigation)
                                                      .ToList();
                if (projects.Count==0) {
                    return Results.NotFound(MessageHelper.createMessage(false, "No se encontraron proyectos para ese usuario"));
                }
                return Results.Ok(projects);                
            } catch (Exception) {
                return Results.NotFound(MessageHelper.createMessage(false, "Error interno"));
            }
        };

        public static Func<int, MiembroProyecto, Task<IResult>> updateMember = async (int id, MiembroProyecto _miembro) => {          
            try {
                dbSGCSContext context = new dbSGCSContext();
                var miembroProyecto = context.MiembroProyecto.Where(x => x.IdMiembroProyecto == id)
                                                             .Include("IdCargoNavigation")
                                                             .Include("IdProyectoNavigation.IdMetodologiaNavigation")
                                                             .Include("IdUsuarioNavigation")
                                                             .FirstOrDefault();
                if (miembroProyecto is null) {
                    return Results.NotFound(MessageHelper.createMessage(false, "No se encontró el miembro del proyecto"));
                }
                context.Entry(miembroProyecto).CurrentValues.SetValues(_miembro);
                await context.SaveChangesAsync();
                return Results.Ok(miembroProyecto);
            } catch (Exception) {
                return Results.NotFound(MessageHelper.createMessage(false, "Error al intentar actualizar el miembro del proyecto"));
            }
        };
    }
}