﻿using backendSGCS.Helpers;
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

        public static Func<int, IResult> getElementoConfiguracionById = (int _id) => {
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