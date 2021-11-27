using backendSGCS.Helpers;
using backendSGCS.Models;
using Microsoft.EntityFrameworkCore;

namespace backendSGCS.Controllers {
    public class ProyectoController {

        public static Func<List<Proyecto>> getProjects = () => {
            dbSGCSContext context = new dbSGCSContext();
            return context.Proyecto.Include("IdMetodologiaNavigation").ToList();
        };

        public static Func<int, IResult> getProjectById = (int id) => {
            dbSGCSContext context = new dbSGCSContext();
            var proyecto = context.Proyecto.Include("IdMetodologiaNavigation").Where(x => x.IdProyecto == id).FirstOrDefault();
            if (proyecto is null) {
                return Results.NotFound(MessageHelper.createMessage(false, "No se encontró el proyecto"));
            }
            return Results.Ok(proyecto);
        };

        public static Func<Proyecto, IResult> createProject = (Proyecto _proyecto) => {
            try {
                dbSGCSContext context = new dbSGCSContext();
                context.Proyecto.Add(_proyecto);
                context.SaveChanges();
                return Results.Ok(_proyecto);
            } catch (Exception) {
                return Results.NotFound(MessageHelper.createMessage(false, "Error al crear el proyecto"));
            }
        };        

        public static Func<int, IResult> deleteProject = (int id) => {
            dbSGCSContext context = new dbSGCSContext();
            var project = context.Proyecto.Find(id);
            if (project is null) {
                return Results.NotFound(MessageHelper.createMessage(false, "No se encontró el proyecto"));
            }
            context.Proyecto.Remove(project);
            context.SaveChanges();
            return Results.Ok(MessageHelper.createMessage(true, "Proyecto borrado exitosamente"));
        };

        public static Func<int, Proyecto, Task<IResult>> updateProject = async (int _id, Proyecto _project) => {
            try {
                dbSGCSContext context = new dbSGCSContext();
                var project = context.Proyecto.Include("IdMetodologiaNavigation").Where(x => x.IdProyecto == _id).FirstOrDefault();
                if (project is null) {
                    return Results.NotFound(MessageHelper.createMessage(false, "No se encontró el proyecto"));
                }
                context.Entry(project).CurrentValues.SetValues(_project);
                await context.SaveChangesAsync();
                return Results.Ok(project);
            } catch (Exception) {
                return Results.NotFound(MessageHelper.createMessage(false, "Error al intentar actualizar el proyecto"));
            }
        };
    }
}