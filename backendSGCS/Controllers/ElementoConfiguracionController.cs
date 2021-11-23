using backendSGCS.Helpers;
using backendSGCS.Models;
using Microsoft.EntityFrameworkCore;

namespace backendSGCS.Controllers {
    public class ElementoConfiguracionController {

        public static Func<ElementoConfiguracion, IResult> createElementoConfiguracion = (ElementoConfiguracion _elementoConfiguracion) => {
            try {
                dbSGCSContext context = new dbSGCSContext();
                context.ElementoConfiguracion.Add(_elementoConfiguracion);
                context.SaveChanges();
                return Results.Ok(_elementoConfiguracion);
            } catch (Exception) {
                return Results.NotFound(MessageHelper.createMessage(false, "Error al crear el elemento de configuración"));
            }
        };

        public static Func<List<ElementoConfiguracion>> getElementoConfiguracions = () => {
            dbSGCSContext context = new dbSGCSContext();
            return context.ElementoConfiguracion.Include("IdLineaBaseNavigation.IdEntregableNavigation.IdFaseMetodologiaNavigation")
                                                .Include("IdLineaBaseNavigation.IdProyectoNavigation.IdMetodologiaNavigation")
                                                .ToList();
        };

        public static Func<int,IResult> getElementsByProjectByUser = (int _id) => {
            try {
                dbSGCSContext context = new dbSGCSContext();
                var miembroProyectos = context.MiembroProyecto.Include("IdCargoNavigation")
                                                     .Include("IdProyectoNavigation.IdMetodologiaNavigation")
                                                     .Include("IdUsuarioNavigation")
                                                     .Where(x => x.IdUsuario == _id)                                                     
                                                     .ToList();
                if (miembroProyectos is null) {
                    return Results.NotFound(MessageHelper.createMessage(false, "No se encontraron proyectos"));
                }

                List<ElementoConfiguracion> elementos = new List<ElementoConfiguracion>();

                miembroProyectos.ForEach(miembro => {
                    if (miembro.IdUsuario == _id) {
                        var elemento = context.ElementoConfiguracion.Include("IdLineaBaseNavigation.IdEntregableNavigation.IdFaseMetodologiaNavigation")
                                                                    .Include("IdLineaBaseNavigation.IdProyectoNavigation.IdMetodologiaNavigation")
                                                                    .Where(x => x.IdLineaBaseNavigation.IdProyectoNavigation.IdProyecto == miembro.IdProyecto)
                                                                    .ToList();
                        if (elemento != null) {
                            elemento.ForEach(x => {
                                elementos.Add(x);
                            });                            
                        }                                                                    
                    }
                });

                if (elementos.Count==0) {
                    return Results.NotFound(MessageHelper.createMessage(false, "No se encontraron elementos para los proyectos de este usuario"));
                }
                return Results.Ok(elementos);
            } catch (Exception) {
                return Results.NotFound(MessageHelper.createMessage(false, "Error interno"));
            }
        };

        public static Func<int, IResult> getElementById = (int _id) => {
            using dbSGCSContext context = new();
            ElementoConfiguracion? elementoConfiguracion = context.ElementoConfiguracion
                                                                  .Include("IdLineaBaseNavigation.IdEntregableNavigation.IdFaseMetodologiaNavigation")
                                                                  .Include("IdLineaBaseNavigation.IdProyectoNavigation.IdMetodologiaNavigation")
                                                                  .Where(x => x.IdElementoConfiguracion == _id)
                                                                  .FirstOrDefault();
            if(elementoConfiguracion is null) {
                return Results.NotFound(MessageHelper.createMessage(false, "No se encontró el elemento de configuración"));
            }
            return Results.Ok(elementoConfiguracion);
        };

        public static Func<int, IResult> getElementsByProject = (int _id) => {
            using dbSGCSContext context = new();
            List<ElementoConfiguracion>? elementosConfiguracion = context.ElementoConfiguracion
                                                                  .Include("IdLineaBaseNavigation.IdEntregableNavigation.IdFaseMetodologiaNavigation")
                                                                  .Include("IdLineaBaseNavigation.IdProyectoNavigation.IdMetodologiaNavigation")
                                                                  .Where(x => x.IdLineaBaseNavigation.IdProyecto == _id)
                                                                  .ToList();
            if (elementosConfiguracion is null) {
                return Results.NotFound(MessageHelper.createMessage(false, "No se encontraron elementos de configuración para este proyecto"));
            }
            return Results.Ok(elementosConfiguracion);
        };

        public static Func<int, IResult> deleteElementoConfiguracion = (int id) => {
            dbSGCSContext context = new dbSGCSContext();
            var elementoConfiguracion = context.ElementoConfiguracion.Find(id);
            if (elementoConfiguracion == null) {
                return Results.NotFound(MessageHelper.createMessage(false, "No se encontró el elemento de configuración"));
            }
            context.ElementoConfiguracion.Remove(elementoConfiguracion);
            context.SaveChanges();
            return Results.Ok(MessageHelper.createMessage(true, "Elemento de configuración borrado exitosamente"));
        };

        public static Func<int, ElementoConfiguracion, Task<IResult>> updateElementoConfiguracion = async (int _id, ElementoConfiguracion _elementoConfiguracion) => {
            try {
                dbSGCSContext context = new dbSGCSContext();
                ElementoConfiguracion? elementoConfiguracion = context.ElementoConfiguracion
                                                      .Include("IdLineaBaseNavigation.IdEntregableNavigation.IdFaseMetodologiaNavigation")
                                                      .Include("IdLineaBaseNavigation.IdProyectoNavigation.IdMetodologiaNavigation")
                                                      .Where(x => x.IdElementoConfiguracion == _id)
                                                      .FirstOrDefault();
                if (elementoConfiguracion is null) {
                    return Results.NotFound(MessageHelper.createMessage(false, "No se encontró el elemento de configuración"));
                }
                context.Entry(elementoConfiguracion).CurrentValues.SetValues(_elementoConfiguracion);
                await context.SaveChangesAsync();
                return Results.Ok(elementoConfiguracion);
            } catch (Exception e) {
                return Results.NotFound(MessageHelper.createMessage(false, "Error al intentar actualizar el elemento de configuración"));
            }
        };
    }
}