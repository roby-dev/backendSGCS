using backendSGCS.Helpers;
using backendSGCS.Models;

namespace backendSGCS.Controllers
{
    public class CargoController
    {
        static dbSGCSContext context = new dbSGCSContext();

        public static Func<Cargo, IResult> createCargo = (Cargo _cargo) => {
            context.Cargo.Add(_cargo);
            var savedCargo = context.SaveChanges();
            return savedCargo != 0 ? Results.Ok(_cargo) : Results.NotFound(MessageHelper.createMessage(false, "Error al crear cargo"));
        };
        public static Func<List<Cargo>> getCargos = () => context.Cargo.ToList();
        public static Func<int, IResult> getCargoById = (int id) => {
            var cargo = context.Cargo.Find(id);
            return cargo != null ? Results.Ok(cargo) : Results.NotFound(MessageHelper.createMessage(false, "No se encontró el cargo"));
        };

        public static Func<int, IResult> deleteCargo = (int id) => {
            var cargo = context.Cargo.Find(id);
            if (cargo == null) {
                return Results.NotFound(MessageHelper.createMessage(false, "No se encontró el cargo"));
            }
            context.Cargo.Remove(cargo);
            context.SaveChanges();
            return Results.Ok(MessageHelper.createMessage(true, "Cargo borrado exitosamente"));
        };

        public static Func<int, Cargo, Task<IResult>> updateCargo = async (int id, Cargo cargo) => {
            var _cargo = context.Cargo.Find(id);
            if (_cargo == null) {
                return Results.NotFound(MessageHelper.createMessage(false, "No se encontró el cargo"));
            }
            try {
                context.Entry(_cargo).CurrentValues.SetValues(cargo);
                await context.SaveChangesAsync();
                return Results.Ok(_cargo);
            } catch (Exception e) {
                return Results.NotFound(e);
            }
        };
    }
}
