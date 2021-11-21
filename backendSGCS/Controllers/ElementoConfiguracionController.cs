using backendSGCS.Helpers;
using backendSGCS.Models;

namespace backendSGCS.Controllers
{
    public class ElementoConfiguracionController
    {


        static dbSGCSContext context = new dbSGCSContext();

        public static Func<ElementoConfiguracion, IResult> createElementoConfiguracion = (ElementoConfiguracion _elementoConfiguracion) => {
            context.ElementoConfiguracion.Add(_elementoConfiguracion);
            var savedElementoConfiguracion = context.SaveChanges();
            return savedElementoConfiguracion != 0 ? Results.Ok(_elementoConfiguracion) : Results.NotFound(MessageHelper.createMessage(false, "Error al crear el elemento de configuración"));
        };
        public static Func<List<ElementoConfiguracion>> getElementoConfiguracions = () => context.ElementoConfiguracion.ToList();
        public static Func<int, IResult> getElementoConfiguracionById = (int id) => {
            var elementoConfiguracion = context.ElementoConfiguracion.Find(id);
            return elementoConfiguracion != null ? Results.Ok(elementoConfiguracion) : Results.NotFound(MessageHelper.createMessage(false, "No se encontró el elemento de configuración"));
        };

        public static Func<int, IResult> deleteElementoConfiguracion = (int id) => {
            var elementoConfiguracion = context.ElementoConfiguracion.Find(id);
            if (elementoConfiguracion == null)
            {
                return Results.NotFound(MessageHelper.createMessage(false, "No se encontró el elemento de configuración"));
            }
            context.ElementoConfiguracion.Remove(elementoConfiguracion);
            context.SaveChanges();
            return Results.Ok(MessageHelper.createMessage(true, "Elemento de configuración borrado exitosamente"));
        };

        public static Func<int, ElementoConfiguracion, Task<IResult>> updateElementoConfiguracion = async (int id, ElementoConfiguracion elementoConfiguracion) => {
            var _elementoConfiguracion = context.ElementoConfiguracion.Find(id);
            if (_elementoConfiguracion == null)
            {
                return Results.NotFound(MessageHelper.createMessage(false, "No se encontró el elemento de configuración"));
            }
            try
            {
                context.Entry(_elementoConfiguracion).CurrentValues.SetValues(elementoConfiguracion);
                await context.SaveChangesAsync();
                return Results.Ok(_elementoConfiguracion);
            }
            catch (Exception e)
            {
                return Results.NotFound(MessageHelper.createMessage(false, "Error al intentar actualizar el elemento de configuración"));
            }
        };

    }
}
