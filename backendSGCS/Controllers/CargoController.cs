using backendSGCS.Helpers;
using backendSGCS.Models;

namespace backendSGCS.Controllers
{
    public class CargoController
    {
        static dbSGCSContext context = dbSGCSContext.getContext();
        public static Func<Cargo, IResult> createCargo = (Cargo _cargo) => {                
            context.Cargos.Add(_cargo);
            var savedCargo = context.SaveChanges();
            return savedCargo != 0 ? Results.Ok(_cargo) : Results.NotFound(MessageHelper.createMessage(false, "Error al crear cargo"));
        };
        public static Func<List<Cargo>> getCargos = () => context.Cargos.ToList();
        public static Func<int, IResult> getCargoById = (int id) =>
        {
            var cargo = context.Cargos.Find(id);
            return cargo != null ? Results.Ok(cargo) : Results.NotFound(MessageHelper.createMessage(false, "No se encontró el cargo"));
        };     

        public static Func<int, IResult> deleteCargo = (int id) =>
        {
            var cargo = context.Cargos.Find(id);
            if (cargo == null)
            {
                return Results.NotFound(MessageHelper.createMessage(false, "No se encontró el cargo"));
            }
            context.Cargos.Remove(cargo);
            context.SaveChanges();
            return Results.Ok(MessageHelper.createMessage(true, "Cargo borrado exitosamente"));
        };
    }
}
