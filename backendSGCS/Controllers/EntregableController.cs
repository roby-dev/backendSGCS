﻿using backendSGCS.Helpers;
using backendSGCS.Models;

namespace backendSGCS.Controllers
{
    public class EntregableController
    {
        static dbSGCSContext context = new dbSGCSContext();

        public static Func<Entregable, IResult> createEntregable = (Entregable _entregable) => {
            context.Entregable.Add(_entregable);
            var savedEntregable = context.SaveChanges();
            return savedEntregable != 0 ? Results.Ok(_entregable) : Results.NotFound(MessageHelper.createMessage(false, "Error al crear el entregable"));
        };
        public static Func<List<Entregable>> getEntregables = () => context.Entregable.ToList();
        public static Func<int, IResult> getEntregableById = (int id) => {
            var entregable = context.Entregable.Find(id);
            return entregable != null ? Results.Ok(entregable) : Results.NotFound(MessageHelper.createMessage(false, "No se encontró el entregable"));
        };

        public static Func<int, IResult> deleteEntregable = (int id) => {
            var entregable = context.Entregable.Find(id);
            if (entregable == null)
            {
                return Results.NotFound(MessageHelper.createMessage(false, "No se encontró el entregable"));
            }
            context.Entregable.Remove(entregable);
            context.SaveChanges();
            return Results.Ok(MessageHelper.createMessage(true, "Entregable borrado exitosamente"));
        };

        public static Func<int, Entregable, Task<IResult>> updateEntregable = async (int id, Entregable entregable) => {
            var _entregable = context.Entregable.Find(id);
            if (_entregable == null)
            {
                return Results.NotFound(MessageHelper.createMessage(false, "No se encontró el entregable"));
            }
            try
            {
                context.Entry(_entregable).CurrentValues.SetValues(entregable);
                await context.SaveChangesAsync();
                return Results.Ok(_entregable);
            }
            catch (Exception e)
            {
                return Results.NotFound(e);
            }
        };
    }
}
