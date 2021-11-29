using backendSGCS.Helpers;
using backendSGCS.Models;
using Microsoft.EntityFrameworkCore;

namespace backendSGCS.Controllers {
    public class ReportController {

        public static Func<Usuario, IResult> getAllTotal = (Usuario _usuario) => {
            dbSGCSContext context = new dbSGCSContext();
            dynamic Result = new System.Dynamic.ExpandoObject();
            if (_usuario.Rol == "ADMIN") {
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
                Result.cantidadVersiones = context.VersionElementoConfiguracion.ToList().Count;
            } else {
                Result.cantidadUsuario = 0;
                Result.cantidadMetodologia = 0;                
                Result.cantidadCargo = context.Cargo.ToList().Count;                
                Result.cantidadProyecto = getProyectsByUser(_usuario.IdUsuario).Count;
                Result.cantidadFaseMetodologia = 0;
                Result.cantidadEntregable = getEntregablesByUser(_usuario.IdUsuario).Where(x=>x.Estado==true).ToList().Count;                
                Result.cantidadElementoConfiguracion = getElementsByProjectByUser(_usuario.IdUsuario).Count;
                Result.cantidadVersiones = context.VersionElementoConfiguracion.ToList().Count;
                Result.cantidadLineaBase = getLineasBaseByProjectByUser(_usuario.IdUsuario).Count;
                Result.cantidadSolicitud = getSolicitudByProjectsByUser(_usuario.IdUsuario).Count;
                Result.cantidadMiembroProyecto = getMembersByUser(_usuario.IdUsuario).Count;
                if (_usuario.Rol == "USER") {
                    Result.cantidadSolicitud = getSolicitudByProjectsByUser(_usuario.IdUsuario).Where(x=>x.IdMiembroProyectoNavigation.IdUsuario==_usuario.IdUsuario).ToList().Count;
                    Result.cantidadCargo = 0;
                    Result.cantidadEntregable = 0;
                    Result.cantidadElementoConfiguracion = 0;
                    Result.cantidadVersiones = 0;
                    Result.cantidadMiembroProyecto = 0;
                }
            }
            return Results.Ok(Result);
        };

        private static Func<int, List<Proyecto>> getProyectsByUser = (int _id) => {
            List<Proyecto> proyectos = new List<Proyecto>();
            dbSGCSContext context = new dbSGCSContext();
            var tmpProyectos = context.MiembroProyecto.Include("IdCargoNavigation")
                                                      .Include("IdProyectoNavigation.IdMetodologiaNavigation")
                                                      .Include("IdUsuarioNavigation")
                                                      .Where(x => x.IdUsuario == _id)
                                                      .Select(x => x.IdProyectoNavigation)
                                                      .ToList();

            if(tmpProyectos is null) {
                return proyectos;
            }
            proyectos = tmpProyectos;
            return proyectos;
        };

        private static Func<int, List<ElementoConfiguracion>> getElementsByProjectByUser = (int _id) => {
            List<ElementoConfiguracion> elementoConfiguracions = new List<ElementoConfiguracion>();
            dbSGCSContext context = new dbSGCSContext();
            var miembroProyectos = context.MiembroProyecto.Include("IdCargoNavigation")
                                                 .Include("IdProyectoNavigation.IdMetodologiaNavigation")
                                                 .Include("IdUsuarioNavigation")
                                                 .Where(x => x.IdUsuario == _id)
                                                 .ToList();
            if (miembroProyectos is null) {
                return elementoConfiguracions;
            }

            miembroProyectos.ForEach(miembro => {
                if (miembro.IdUsuario == _id) {
                    var elemento = context.ElementoConfiguracion.Include("IdLineaBaseNavigation.IdEntregableNavigation.IdFaseMetodologiaNavigation")
                                                                .Include("IdLineaBaseNavigation.IdProyectoNavigation.IdMetodologiaNavigation")
                                                                .Where(x => x.IdLineaBaseNavigation.IdProyectoNavigation.IdProyecto == miembro.IdProyecto)
                                                                .ToList();
                    if (elemento != null) {
                        elemento.ForEach(x => {
                            elementoConfiguracions.Add(x);
                        });
                    }
                }
            });
            return elementoConfiguracions;
        };

        private static Func<int, List<Solicitud>> getSolicitudByProjectsByUser = (int _id) => {
            dbSGCSContext context = new dbSGCSContext();

            var miembroProyecto = context.MiembroProyecto.Include(X => X.IdProyectoNavigation).Include(X => X.IdUsuarioNavigation).Where(x => x.IdUsuario == _id).ToList();
            List<Solicitud> totalSolicitudes = new List<Solicitud>();

            miembroProyecto.ForEach(miembro => {
                var solicitudes = context.Solicitud
                                  .Include(x => x.IdElementoConfiguracionNavigation.IdLineaBaseNavigation.IdProyectoNavigation.IdMetodologiaNavigation)
                                  .Include(x => x.IdElementoConfiguracionNavigation.IdLineaBaseNavigation.IdEntregableNavigation.IdFaseMetodologiaNavigation.IdMetodologiaNavigation)
                                  .Include(x => x.IdMiembroProyectoNavigation.IdCargoNavigation)
                                  .Include(x => x.IdMiembroProyectoNavigation.IdProyectoNavigation)
                                  .Include(x => x.IdMiembroProyectoNavigation.IdUsuarioNavigation)
                                  .Where(x => x.IdMiembroProyectoNavigation.IdProyecto == miembro.IdProyecto)
                                  .ToList();
                solicitudes.ForEach(solicitud => {
                    totalSolicitudes.Add(solicitud);
                });
            });

            totalSolicitudes = totalSolicitudes.Distinct().ToList();

            
            return totalSolicitudes;
        };

        private static Func<int, List<Entregable>> getEntregablesByUser = (int _id) => {
            List<Entregable>? entregables = new List<Entregable>();
            dbSGCSContext context = new dbSGCSContext();
            List<Proyecto?> proyectos = context.MiembroProyecto.Where(x => x.IdUsuario == _id).Select(x => x.IdProyectoNavigation).ToList();
            if (proyectos.Count == 0) return entregables;
            proyectos.ForEach(proyecto => {
                var entregable = context.Entregable
                                    .Include(x => x.IdFaseMetodologiaNavigation.IdMetodologiaNavigation)
                                    .Where(x => x.IdFaseMetodologiaNavigation.IdMetodologia == proyecto.IdMetodologia)
                                    .ToList();
                if (entregable.Count != 0) {
                    entregable.ForEach(x => {
                        entregables.Add(x);
                    });
                }
            });
            return entregables;
        };

        private static Func<int, List<LineaBase>> getLineasBaseByProjectByUser = (int _id) => {
            List<LineaBase> lineasBase = new List<LineaBase>();
            try {
                dbSGCSContext context = new dbSGCSContext();
                var miembroProyectos = context.MiembroProyecto
                                                     .Include(x => x.IdCargoNavigation)
                                                     .Include(x => x.IdProyectoNavigation.IdMetodologiaNavigation)
                                                     .Include(x => x.IdUsuarioNavigation)
                                                     .Where(x => x.IdUsuario == _id)
                                                     .ToList();
                if (miembroProyectos is null) {
                    return lineasBase;
                }               
                miembroProyectos.ForEach(miembro => {
                    var lineaBase = context.LineaBase.Include(x => x.IdEntregableNavigation.IdFaseMetodologiaNavigation.IdMetodologiaNavigation)
                                                     .Include(x => x.IdProyectoNavigation.IdMetodologiaNavigation)
                                                     .Where(x => x.IdProyecto == miembro.IdProyecto)
                                                     .ToList();
                    if (lineaBase != null) {
                        lineaBase.ForEach(x => {
                            lineasBase.Add(x);
                        });
                    }
                });
                return lineasBase;
            } catch (Exception) {

                return lineasBase;
            }
        };

        private static Func<int, List<MiembroProyecto>> getMembersByUser = (int _id) => {
            List<MiembroProyecto> miembros = new List<MiembroProyecto>();
            dbSGCSContext context = new dbSGCSContext();
            var proyectos = context.MiembroProyecto.Include(x=>x.IdUsuarioNavigation).Where(x => x.IdUsuario == _id).Where(x=>x.IdUsuarioNavigation.Estado==true).ToList();
            proyectos.ForEach(proyecto => {
                var miembro = context.MiembroProyecto.Where(x => x.IdProyecto == proyecto.IdProyecto).ToList();
                miembro.ForEach(x => {
                    miembros.Add(x);
                });
            });
            return miembros;
        };

        public static Func<int, IResult> getGanttDiagram = (int _id) => {
            try {
                dbSGCSContext context = new dbSGCSContext();
                Proyecto? proyecto = context.Proyecto.Include(x => x.IdMetodologiaNavigation).Where(x => x.IdProyecto == _id).FirstOrDefault();
                if (proyecto is null) return Results.NotFound(MessageHelper.createMessage(false, "No se encontró proyecto"));
                List<FaseMetodologia> fases = context.FaseMetodologia.Where(x => x.IdMetodologia == proyecto.IdMetodologia).Where(x => x.Estado == true).ToList();
                List<LineaBase> lineasBase = context.LineaBase.Include(x => x.IdProyectoNavigation).Include(x => x.IdEntregableNavigation).Where(x => x.IdProyecto == proyecto.IdProyecto).ToList();

                lineasBase = lineasBase.OrderBy(x => DateTime.Parse(x.FechaInicio)).ToList();

                var inicioProyecto = convertTime(DateTime.Parse(proyecto.FechaInicio));
                var finProyecto = convertTime(DateTime.Parse(proyecto.FechaFin));

                string objetoHighchart = "";
                objetoHighchart += "" +
                    "title: {" +
                    "  text:'Diagrama de Gantt - "+proyecto.Nombre+"'" +
                    "}," +
                    "xAxis: {" +
                    "  min: "+inicioProyecto+" - (2 * day)," +
                    "  max: "+finProyecto+" + (2 * day)" +
                    "}," +
                    "series: [{" +
                    "  name: '"+proyecto.Nombre+"'," +
                    "  data: [{" +
                    "      name: '"+proyecto.Nombre+"'," +
                    "      id: 'proyecto'," +
                    "      start: "+inicioProyecto+"," +
                    "      end: "+finProyecto+
                    "  },";

                fases.ForEach(fase => {
                    objetoHighchart += "{" +
                    "      name: '"+fase.Nombre+"+'," +
                    "      id: '"+fase.IdFaseMetodologia+"'," +
                    "      parent: 'proyecto',";
                    bool isExistsLineaBase = lineasBase.Where(x => x.IdProyecto == proyecto.IdProyecto).Where(x => x.IdEntregableNavigation.IdFaseMetodologia == fase.IdFaseMetodologia).Any();
                    if (isExistsLineaBase) {
                        var lineaBasePrimera = lineasBase.Where(x => x.IdProyecto == proyecto.IdProyecto).Where(x => x.IdEntregableNavigation.IdFaseMetodologia == fase.IdFaseMetodologia).FirstOrDefault();
                        var lineaBaseFinal = lineasBase.Where(x => x.IdProyecto == proyecto.IdProyecto).Where(x => x.IdEntregableNavigation.IdFaseMetodologia == fase.IdFaseMetodologia).Reverse().FirstOrDefault();
                        objetoHighchart += "" +
                        "  start: "+convertTime(DateTime.Parse(lineaBasePrimera.FechaInicio))+"," +
                        "  end: "+convertTime(DateTime.Parse(lineaBaseFinal.FechaFin))+
                        "},";
                        var tmpLineasBase = lineasBase.Where(x => x.IdProyecto == proyecto.IdProyecto).Where(x => x.IdEntregableNavigation.IdFaseMetodologia == fase.IdFaseMetodologia).ToList();
                        tmpLineasBase.ForEach(linea => {
                            objetoHighchart += "{" +
                            "name: '"+linea.IdEntregableNavigation.Nomenclatura+" - "+linea.IdEntregableNavigation.Nombre+"'," +
                            "id: '"+linea.IdLineaBase+"'," +
                            "parent: '"+fase.IdFaseMetodologia+"'," +
                            "start: "+convertTime(DateTime.Parse(linea.FechaInicio))+"," +
                            "end: "+convertTime(DateTime.Parse(linea.FechaFin))+
                            "},";
                        });
                    } else {
                        objetoHighchart += "" +
                        "  start: ' '," +
                        "  end: ' '" +
                        "},";
                    }
                });

                objetoHighchart += "]" +
                "}]";

                return Results.Ok(objetoHighchart);
            }
            catch (Exception e) {
                return Results.BadRequest(e);
            }           
        };

        private static double convertTime(DateTime date) {
            date = date.Date.AddHours(8).AddMinutes(0).AddSeconds(0).AddMilliseconds(0);
            DateTime d1 = new DateTime(1970, 1, 1);
            DateTime d2 = date.ToUniversalTime();
            TimeSpan ts = new TimeSpan(d2.Ticks - d1.Ticks);
            return ts.TotalMilliseconds;
        }       
    }
}