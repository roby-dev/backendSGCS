using backendSGCS.Models;

namespace backendSGCS.Controllers {
    public class ReportController {
        static dbSGCSContext context = dbSGCSContext.getContext();

        public static Func<IResult> getAllTotal = () => {
            var cantidadCargo = context.Cargo.ToList().Count;
            var cantidadElementoConfiguracion = context.ElementoConfiguracion.ToList().Count;
            var cantidadEntregable = context.Entregable.ToList().Count;
            var cantidadFaseMetodologia = context.FaseMetodologia.ToList().Count;
            var cantidadLineaBase = context.LineaBase.ToList().Count;
            var cantidadMetodologia = context.Metodologia.ToList().Count;
            var cantidadMiembroProyecto = context.MiembroProyecto.ToList().Count;
            var cantidadProyecto = context.Proyecto.ToList().Count;
            var cantidadSolicitud = context.Solicitud.ToList().Count;
            var cantidadUsuario = context.Usuario.ToList().Count;
            dynamic Result = new System.Dynamic.ExpandoObject();
            Result.cantidadCargo = cantidadCargo;
            Result.cantidadElementoConfiguracion = cantidadElementoConfiguracion;
            Result.cantidadEntregable=cantidadEntregable;
            Result.cantidadFasemetodologia = cantidadFaseMetodologia;
            Result.cantidadLineaBase = cantidadLineaBase;
            Result.cantidadMetodologia = cantidadMetodologia;
            Result.cantidadMiembroProyecto = cantidadMiembroProyecto;
            Result.cantidadProyecto = cantidadProyecto;
            Result.cantidadSolicitud = cantidadSolicitud;
            Result.cantidadUsuario = cantidadUsuario;

            return Results.Ok(Result);
            //return savedCargo != 0 ? Results.Ok(_cargo) : Results.NotFound(MessageHelper.createMessage(false, "Error al crear cargo"));
        };
    }
}
