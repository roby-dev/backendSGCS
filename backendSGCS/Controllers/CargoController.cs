using backendSGCS.Helpers;
using backendSGCS.Models;

namespace backendSGCS.Controllers {
    public class CargoController {

        public static Func<Cargo, IResult> createCargo = (Cargo _cargo) => {
            dbSGCSContext context = new dbSGCSContext();
            try {
                _cargo.Nombre = _cargo.Nombre.Trim();
                var cargo = context.Cargo.Where(x => x.Nombre == _cargo.Nombre).FirstOrDefault();
                if (cargo == null) {
                    context.Cargo.Add(_cargo);
                    context.SaveChanges();
                    return Results.Ok(_cargo);
                }
                return Results.NotFound(MessageHelper.createMessage(false, "Nombre de cargo ya registrado"));
            } catch (Exception) {
                return Results.NotFound(MessageHelper.createMessage(false, "Error al crear el cargo"));
            }
        };
        public static Func<List<Cargo>> getCargos = () => {
            dbSGCSContext context = new dbSGCSContext();
            return context.Cargo.ToList();
        };
        public static Func<int, IResult> getCargoById = (int id) => {
            dbSGCSContext context = new dbSGCSContext();
            var cargo = context.Cargo.Find(id);
            return cargo != null ? Results.Ok(cargo) : Results.NotFound(MessageHelper.createMessage(false, "No se encontró el cargo"));
        };
        public static Func<int, IResult> deleteCargo = (int id) => {
            dbSGCSContext context = new dbSGCSContext();
            var cargo = context.Cargo.Find(id);
            if (cargo == null) {
                return Results.NotFound(MessageHelper.createMessage(false, "No se encontró el cargo"));
            }
            context.Cargo.Remove(cargo);
            context.SaveChanges();
            return Results.Ok(MessageHelper.createMessage(true, "Cargo borrado exitosamente"));
        };
        public static Func<int, Cargo, Task<IResult>> updateCargo = async (int id, Cargo _cargo) => {
            dbSGCSContext context = new dbSGCSContext();
            var cargo = context.Cargo.Find(id);
            if (cargo == null) {
                return Results.NotFound(MessageHelper.createMessage(false, "No se encontró el cargo"));
            }
            try {
                _cargo.Nombre = _cargo.Nombre.Trim();
                context.Entry(cargo).CurrentValues.SetValues(_cargo);
                await context.SaveChangesAsync();
                return Results.Ok(_cargo);
            } catch (Exception e) {
                return Results.NotFound(MessageHelper.createMessage(false, "Error al intentar actualizar el cargo"));
            }
        };
    }
}