using backendSGCS.Helpers;
using backendSGCS.Models;
using Microsoft.EntityFrameworkCore;

namespace backendSGCS.Controllers {
    public class ProyectoController {

        static dbSGCSContext context = new dbSGCSContext();

        public static Func<List<Proyecto>> getProjects = () => context.Proyecto.Include("IdMetodologiaNavigation").ToList();

        public static Func<int, IResult> getProjectById = (int id) => {
            try {
                var proyecto = context.Proyecto.Include("IdMetodologiaNavigation").Where(x => x.IdProyecto == id).First();
                return Results.Ok(proyecto);
            } catch (Exception) {
                return Results.NotFound(MessageHelper.createMessage(false, "No se encontró el proyecto"));
            }
        };

        public static Func<Proyecto, IResult> createProject = (Proyecto _proyecto) => {
            try {
                context.Proyecto.Add(_proyecto);
                var savedProject = context.SaveChanges();
                return savedProject != 0 ? Results.Ok(_proyecto) : Results.NotFound(MessageHelper.createMessage(false, "Error al crear el proyecto"));
            } catch (Exception) {
                return Results.StatusCode(500);
                throw;
            }

        };
        public static Func<int, IResult> deleteProject = (int id) => {
            var project = context.Proyecto.Find(id);
            if (project == null) {
                return Results.NotFound(MessageHelper.createMessage(false, "No se encontró el proyecto"));
            }
            context.Proyecto.Remove(project);
            context.SaveChanges();
            return Results.Ok(MessageHelper.createMessage(true, "Proyecto borrado exitosamente"));
        };

        public static Func<int, Proyecto, Task<IResult>> updateProject = async (int id, Proyecto project) => {
            var _project = context.Proyecto.Find(id);
            if (_project == null) {
                return Results.NotFound(MessageHelper.createMessage(false, "No se encontró el proyecto"));
            }
            try {
                context.Entry(_project).CurrentValues.SetValues(project);
                await context.SaveChangesAsync();
                return Results.Ok(_project);
            } catch (Exception e) {
                return Results.NotFound(e);
            }
        };
    }
}
