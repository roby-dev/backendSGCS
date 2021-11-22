using backendSGCS.Models;

namespace backendSGCS.Controllers {
    public class ReportController {

        public static Func<IResult> getAllTotal = () => {
            dbSGCSContext context = new dbSGCSContext();
            dynamic Result = new System.Dynamic.ExpandoObject();
            Result.cantidadCargo = context.Cargo.ToList().Count;
            Result.cantidadElementoConfiguracion = context.ElementoConfiguracion.ToList().Count;
            Result.cantidadEntregable = context.Entregable.ToList().Count;
            Result.cantidadFaseMetodologia = context.FaseMetodologia.ToList().Count;
            Result.cantidadLineaBase = context.LineaBase.ToList().Count;
            Result.cantidadMetodologia = context.Metodologia.ToList().Count;
            Result.cantidadMiembroProyecto = context.MiembroProyecto.ToList().Count;
            Result.cantidadProyecto = context.Proyecto.ToList().Count;
            Result.cantidadSolicitud = context.Solicitud.ToList().Count;
            Result.cantidadUsuario = context.Usuario.ToList().Count;
            return Results.Ok(Result);
        };
    }
}