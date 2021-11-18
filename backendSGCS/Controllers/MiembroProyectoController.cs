using backendSGCS.Models;
using Microsoft.EntityFrameworkCore;

namespace backendSGCS.Controllers
{
    public class MiembroProyectoController
    {
        static dbSGCSContext context = new dbSGCSContext();

        public static Func<int, IResult> getMemberById = (int id) =>
        {
            var miembroProyecto = context.MiembroProyectos.Include("IdUsuarioNavigation").Include("IdProyectoNavigation").Where(x => x.IdMiembroProyecto == id).First();
            return miembroProyecto != null ? Results.Ok(miembroProyecto) : Results.NotFound();
        };
    }
}